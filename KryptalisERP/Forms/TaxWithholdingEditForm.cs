using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class TaxWithholdingEditForm : Form
    {
        private int _id;
        private DataTable _allCategories;
        private bool _isLoading = false;

        public TaxWithholdingEditForm(int id = 0)
        {
            InitializeComponent();
            _id = id;

            LoadAllCategories();
            LoadSectors();

            if (_id > 0)
            {
                this.Text = "✏️ Επεξεργασία Κράτησης/Φόρου/Επιβάρυνσης";
                _isLoading = true;
                LoadData();
                _isLoading = false;
            }
            else
            {
                this.Text = "➕ Νέα Κράτηση/Φόρος/Επιβάρυνση";
                chkActive.Checked = true;
                chkAppliesSales.Checked = false;
                chkAppliesPurchases.Checked = false;
            }
        }

        private void LoadAllCategories()
        {
            try
            {
                _allCategories = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, CategoryCode, CategoryName, CategoryType, MyDataCode, DefaultRate FROM TaxWithholdingCategories WHERE IsActive = 1 ORDER BY CategoryType, CategoryName");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης κατηγοριών: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSectors()
        {
            DataTable sectors = new DataTable();
            sectors.Columns.Add("Id", typeof(int));
            sectors.Columns.Add("CategoryName", typeof(string));
            sectors.Columns.Add("CategoryType", typeof(string));

            sectors.Rows.Add(1, "Κρατήσεις", "WITHHOLDING");
            sectors.Rows.Add(2, "Φόροι", "TAX");
            sectors.Rows.Add(3, "Επιβαρύνσεις", "SURCHARGE");
            sectors.Rows.Add(4, "Φόρος Χαρτοσήμου", "STAMP_DUTY");

            cmbSector.DataSource = sectors;
            cmbSector.DisplayMember = "CategoryName";
            cmbSector.ValueMember = "Id";
            cmbSector.Tag = sectors;
        }

        private void LoadSubCategories(string categoryType)
        {
            if (_allCategories == null) return;

            DataRow[] filtered = _allCategories.Select($"CategoryType = '{categoryType}'");
            DataTable dt = _allCategories.Clone();
            foreach (DataRow row in filtered)
            {
                dt.ImportRow(row);
            }

            cmbSubCategory.DataSource = dt;
            cmbSubCategory.DisplayMember = "CategoryName";
            cmbSubCategory.ValueMember = "Id";
            cmbSubCategory.Tag = dt;
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT * FROM TaxWithholdingRates WHERE Id = @Id",
                    new SqlParameter("@Id", _id));

                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtCode.Text = r["Code"]?.ToString() ?? "";
                    txtName.Text = r["Name"]?.ToString() ?? "";
                    numRate.Value = Convert.ToDecimal(r["Rate"]);
                    txtMyData.Text = r["MyDataCode"]?.ToString() ?? "";

                    chkAppliesSales.Checked = Convert.ToBoolean(r["AppliesToSales"]);
                    chkAppliesPurchases.Checked = Convert.ToBoolean(r["AppliesToPurchases"]);
                    chkActive.Checked = Convert.ToBoolean(r["IsActive"]);

                    if (r["CategoryId"] != DBNull.Value)
                    {
                        int catId = Convert.ToInt32(r["CategoryId"]);
                        DataRow[] catRows = _allCategories.Select($"Id = {catId}");
                        if (catRows.Length > 0)
                        {
                            string categoryType = catRows[0]["CategoryType"].ToString();
                            SelectSectorByType(categoryType);
                            LoadSubCategories(categoryType);
                            cmbSubCategory.SelectedValue = catId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectSectorByType(string categoryType)
        {
            DataTable sectors = cmbSector.Tag as DataTable;
            if (sectors == null) return;

            int sectorId = 1;
            if (categoryType == "TAX") sectorId = 2;
            else if (categoryType == "SURCHARGE") sectorId = 3;
            else if (categoryType == "STAMP_DUTY") sectorId = 4;

            cmbSector.SelectedValue = sectorId;
        }

        private void cmbSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            if (cmbSector.SelectedValue == null || cmbSector.SelectedValue == DBNull.Value) return;

            int sectorId = GetSafeIntValue(cmbSector);
            string categoryType = "WITHHOLDING";

            if (sectorId == 2) categoryType = "TAX";
            else if (sectorId == 3) categoryType = "SURCHARGE";
            else if (sectorId == 4) categoryType = "STAMP_DUTY";

            LoadSubCategories(categoryType);
            cmbSubCategory.SelectedIndex = -1;

            txtCode.Clear();
            txtName.Clear();
            txtMyData.Clear();
            numRate.Value = 0;
        }

        private void cmbSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            if (cmbSubCategory.SelectedValue == null || cmbSubCategory.SelectedValue == DBNull.Value) return;

            int catId = GetSafeIntValue(cmbSubCategory);
            DataRow[] catRows = _allCategories.Select($"Id = {catId}");

            if (catRows.Length > 0)
            {
                string categoryName = catRows[0]["CategoryName"].ToString();
                string myDataCode = catRows[0]["MyDataCode"]?.ToString() ?? "";
                decimal defaultRate = 0;

                if (catRows[0]["DefaultRate"] != DBNull.Value)
                {
                    defaultRate = Convert.ToDecimal(catRows[0]["DefaultRate"]);
                }

                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    txtName.Text = categoryName;
                }

                if (string.IsNullOrEmpty(txtMyData.Text.Trim()))
                {
                    txtMyData.Text = myDataCode;
                }

                if (numRate.Value == 0)
                {
                    numRate.Value = defaultRate;
                }

                if (string.IsNullOrEmpty(txtCode.Text.Trim()))
                {
                    string categoryCode = catRows[0]["CategoryCode"]?.ToString() ?? "";
                    txtCode.Text = $"{categoryCode}_{defaultRate:0}";
                }
            }
        }

        private int GetSafeIntValue(ComboBox combo)
        {
            if (combo.SelectedValue == null || combo.SelectedValue == DBNull.Value) return 0;
            if (combo.SelectedValue is DataRowView rowView)
                return Convert.ToInt32(rowView[0]);
            return Convert.ToInt32(combo.SelectedValue);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()) || string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Συμπλήρωσε Κωδικό και Όνομα!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbSubCategory.SelectedValue == null || cmbSubCategory.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Επίλεξε Κατηγορία!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ ΝΕΟΣ ΕΛΕΓΧΟΣ: Έλεγχος για διπλότυπο κωδικό πριν την αποθήκευση
            string codeToCheck = txtCode.Text.Trim();
            try
            {
                DataTable checkDt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id FROM TaxWithholdingRates WHERE Code = @Code",
                    new SqlParameter("@Code", codeToCheck));

                if (checkDt.Rows.Count > 0)
                {
                    int existingId = Convert.ToInt32(checkDt.Rows[0]["Id"]);

                    // Αν είναι νέο record (_id == 0) Ή το βρεθέν ID είναι διαφορετικό από το τρέχον (_id)
                    if (_id == 0 || existingId != _id)
                    {
                        MessageBox.Show($"⚠️ Ο κωδικός '{codeToCheck}' υπάρχει ήδη!\nΠαρακαλώ χρησιμοποιήστε διαφορετικό κωδικό.",
                            "Διπλότυπος Κωδικός", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCode.Focus();
                        txtCode.SelectAll();
                        return; // Σταματάμε την αποθήκευση
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα ελέγχου κωδικού: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int categoryId = GetSafeIntValue(cmbSubCategory);

            try
            {
                var res = DatabaseHelper.ExecuteQuery(
                    @"EXEC sp_SaveTaxWithholdingRate @Id, @Code, @Name, @CategoryId, @Rate, @MyData, @AppliesSales, @AppliesPurchases, @Active",
                    new SqlParameter("@Id", _id > 0 ? (object)_id : DBNull.Value),
                    new SqlParameter("@Code", codeToCheck),
                    new SqlParameter("@Name", txtName.Text.Trim()),
                    new SqlParameter("@CategoryId", categoryId),
                    new SqlParameter("@Rate", numRate.Value),
                    new SqlParameter("@MyData", string.IsNullOrEmpty(txtMyData.Text.Trim()) ? (object)DBNull.Value : txtMyData.Text.Trim()),
                    new SqlParameter("@AppliesSales", chkAppliesSales.Checked),
                    new SqlParameter("@AppliesPurchases", chkAppliesPurchases.Checked),
                    new SqlParameter("@Active", chkActive.Checked)
                );

                if (Convert.ToInt32(res.Rows[0]["Success"]) == 1)
                {
                    MessageBox.Show("✅ " + res.Rows[0]["Message"], "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα αποθήκευσης: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}