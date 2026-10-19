using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class CopySeriesForm : Form
    {
        private DataTable _sourceSeries;
        private int _selectedCompanyId;

        public CopySeriesForm()
        {
            InitializeComponent();
            LoadCompanies();
        }

        private void LoadCompanies()
        {
            try
            {
                // ✅ ΧΩΡΙΣ ΦΙΛΤΡΟ IsActive - δείχνει ΟΛΕΣ τις εταιρείες
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, CompanyName FROM Companies ORDER BY CompanyName");

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("⚠️ Δεν βρέθηκαν εταιρείες!", "Προσοχή");
                    return;
                }

                cmbSourceCompany.DataSource = dt.Copy();
                cmbSourceCompany.DisplayMember = "CompanyName";
                cmbSourceCompany.ValueMember = "Id";

                cmbDestCompany.DataSource = dt.Copy();
                cmbDestCompany.DisplayMember = "CompanyName";
                cmbDestCompany.ValueMember = "Id";

                cmbSourceCompany.SelectedIndexChanged += CmbSourceCompany_SelectedIndexChanged;
                cmbDestCompany.SelectedIndexChanged += CmbDestCompany_SelectedIndexChanged;

                if (cmbSourceCompany.Items.Count > 0)
                    cmbSourceCompany.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void CmbSourceCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSourceCompany.SelectedValue == null) return;

            try
            {
                _selectedCompanyId = Convert.ToInt32(cmbSourceCompany.SelectedValue);
                LoadBranches(cmbSourceCompany, cmbSourceBranch);
                LoadSourceSeries();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void CmbDestCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDestCompany.SelectedValue == null) return;
            LoadBranches(cmbDestCompany, cmbDestBranch);
        }

        private void LoadBranches(ComboBox cmbCompany, ComboBox cmbBranch)
        {
            try
            {
                if (cmbCompany.SelectedValue == null) return;

                int companyId = Convert.ToInt32(cmbCompany.SelectedValue);

                // ✅ ΧΩΡΙΣ ΦΙΛΤΡΟ IsActive
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, BranchName, ISNULL(BranchCode, '') AS BranchCode FROM CompanyBranches WHERE CompanyId = @Id ORDER BY IsMain DESC, BranchName",
                    new SqlParameter("@Id", companyId));

                DataRow empty = dt.NewRow();
                empty["Id"] = DBNull.Value;
                empty["BranchName"] = "-- Όλα τα Υποκαταστήματα --";
                dt.Rows.InsertAt(empty, 0);

                cmbBranch.DataSource = dt;
                cmbBranch.DisplayMember = "BranchName";
                cmbBranch.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void LoadSourceSeries()
        {
            try
            {
                if (_selectedCompanyId == 0) return;

                object branchId = cmbSourceBranch.SelectedValue;

                string query = @"
                    SELECT 
                        s.Id,
                        s.SeriesName AS [Όνομα],
                        s.SeriesCode AS [Κωδικός],
                        s.SeriesType AS [Τύπος],
                        s.Prefix AS [Prefix],
                        s.NextNumber AS [Επόμενος]
                    FROM Series s
                    WHERE s.CompanyId = @CompanyId 
                      AND (s.BranchId = @BranchId OR s.BranchId IS NULL OR @BranchId IS NULL)
                    ORDER BY s.SeriesType, s.SeriesName";

                _sourceSeries = DatabaseHelper.ExecuteQuery(query,
                    new SqlParameter("@CompanyId", _selectedCompanyId),
                    new SqlParameter("@BranchId", branchId == null || branchId == DBNull.Value ? DBNull.Value : branchId));

                dgvSeries.DataSource = _sourceSeries;

                if (!dgvSeries.Columns.Contains("Select"))
                {
                    DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
                    chkCol.Name = "Select";
                    chkCol.HeaderText = "Επιλογή";
                    chkCol.Width = 60;
                    dgvSeries.Columns.Insert(0, chkCol);
                }

                if (dgvSeries.Columns.Contains("Id"))
                    dgvSeries.Columns["Id"].Visible = false;

                dgvSeries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (cmbDestCompany.SelectedValue == null)
            {
                MessageBox.Show("️ Επίλεξε εταιρεία προορισμού!", "Προσοχή");
                return;
            }

            string customPrefix = txtCustomPrefix.Text.Trim();
            if (string.IsNullOrEmpty(customPrefix))
            {
                MessageBox.Show("⚠️ Συμπλήρωσε το prefix (π.χ. ΤΠ, ΤΔ, ΑΓ, κλπ)!", "Προσοχή");
                txtCustomPrefix.Focus();
                return;
            }

            List<int> selectedIds = new List<int>();
            foreach (DataGridViewRow row in dgvSeries.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["Select"].Value != null && Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    if (row.Cells["Id"].Value != null)
                        selectedIds.Add(Convert.ToInt32(row.Cells["Id"].Value));
                }
            }

            if (selectedIds.Count == 0)
            {
                MessageBox.Show("⚠️ Επίλεξε σειρές!", "Προσοχή");
                return;
            }

            int destCompanyId = Convert.ToInt32(cmbDestCompany.SelectedValue);
            object destBranchId = cmbDestBranch.SelectedValue;
            if (destBranchId == null || destBranchId == DBNull.Value) destBranchId = DBNull.Value;

            int copied = 0;
            int skipped = 0;
            int failed = 0;
            List<string> errors = new List<string>();

            try
            {
                foreach (int seriesId in selectedIds)
                {
                    try
                    {
                        DataRow sourceRow = _sourceSeries.Select($"Id = {seriesId}")[0];
                        string seriesName = sourceRow["Όνομα"]?.ToString() ?? "";
                        string originalCode = sourceRow["Κωδικός"]?.ToString() ?? "";

                        // ✅ ΝΕΟ: Απλό SeriesCode = prefix + αρχικός κωδικός
                        string newSeriesCode = $"{customPrefix}{originalCode}";

                        // Έλεγχος αν υπάρχει ήδη σειρά με το ίδιο όνομα  κωδικό
                        DataTable check = DatabaseHelper.ExecuteQuery(
                            "SELECT COUNT(*) FROM Series WHERE CompanyId = @CompanyId AND (SeriesName = @Name OR SeriesCode = @Code)",
                            new SqlParameter("@CompanyId", destCompanyId),
                            new SqlParameter("@Name", seriesName),
                            new SqlParameter("@Code", newSeriesCode));

                        if (Convert.ToInt32(check.Rows[0][0]) > 0)
                        {
                            skipped++;
                            continue;
                        }

                        // ✅ Αντιγραφή με ΝΕΟ SeriesCode και ΝΕΟ Prefix
                        DatabaseHelper.ExecuteQuery(@"
                    INSERT INTO Series (
                        SeriesName, SeriesCode, SeriesType, Prefix, NextNumber, NumberLength,
                        DocTypeId, Category, IsIncome, IsExpense, AffectsStock,
                        IncomeClassificationId, ExpenseClassificationId,
                        IncomeCategoryId, ExpenseCategoryId,
                        IsDefault, IsActive, YearReset, InvoiceSubType,
                        CompanyId, BranchId
                    )
                    SELECT 
                        SeriesName, @NewCode, SeriesType, @NewPrefix, NextNumber, NumberLength,
                        DocTypeId, Category, IsIncome, IsExpense, AffectsStock,
                        IncomeClassificationId, ExpenseClassificationId,
                        IncomeCategoryId, ExpenseCategoryId,
                        IsDefault, IsActive, YearReset, InvoiceSubType,
                        @DestCompanyId, @DestBranchId
                    FROM Series
                    WHERE Id = @SourceId",
                            new SqlParameter("@NewCode", newSeriesCode),
                            new SqlParameter("@NewPrefix", customPrefix), // ✅ Μόνο το prefix, χωρίς branch code
                            new SqlParameter("@DestCompanyId", destCompanyId),
                            new SqlParameter("@DestBranchId", destBranchId == DBNull.Value ? DBNull.Value : destBranchId),
                            new SqlParameter("@SourceId", seriesId));

                        copied++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        errors.Add($"Σειρά ID {seriesId}: {ex.Message}");
                        continue;
                    }
                }

                string msg = $"✅ Αντιγράφηκαν: {copied}\n⚠️ Παραλείφθηκαν (υπήρχαν): {skipped}\n❌ Απέτυχαν: {failed}";

                if (errors.Count > 0)
                {
                    msg += "\n\nΛεπτομέρειες σφαλμάτων:\n" + string.Join("\n", errors);
                }

                MessageBox.Show(msg, "Αποτέλεσμα Αντιγραφής", MessageBoxButtons.OK,
                    failed > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                LoadSourceSeries();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}