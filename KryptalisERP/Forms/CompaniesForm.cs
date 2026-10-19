using KryptalisERP.Helpers;
using KryptalisERP.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class CompaniesForm : Form
    {
        private int _selectedCompanyId = 0;
        private int _selectedBranchId = 0;

        public CompaniesForm()
        {
            InitializeComponent();
            ThemeManager.ApplyToCurrentForm(this);
            LoadCompanies();
        }

        private string GetCol(DataRow r, string col)
        {
            if (r.Table.Columns.Contains(col) && r[col] != DBNull.Value) return r[col].ToString();
            return "";
        }

        // ==========================================
        // ΕΤΑΙΡΕΙΕΣ
        // ==========================================
        private void LoadCompanies()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, CompanyName, VatNumber, City, IsDefault, IsActive FROM Companies ORDER BY IsDefault DESC, CompanyName");
                dgvCompanies.DataSource = dt;
                if (dgvCompanies.Columns.Contains("Id")) dgvCompanies.Columns["Id"].Visible = false;
                if (dgvCompanies.Columns.Contains("IsDefault")) dgvCompanies.Columns["IsDefault"].HeaderText = "Προεπιλογή";
                if (dgvCompanies.Columns.Contains("IsActive")) dgvCompanies.Columns["IsActive"].HeaderText = "Ενεργή";
                dgvCompanies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης εταιρειών: " + ex.Message); }
        }

        private void dgvCompanies_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCompanies.CurrentRow == null || dgvCompanies.CurrentRow.IsNewRow) return;
            if (dgvCompanies.CurrentRow.Cells["Id"].Value == null) return;

            _selectedCompanyId = Convert.ToInt32(dgvCompanies.CurrentRow.Cells["Id"].Value);
            string companyName = dgvCompanies.CurrentRow.Cells["CompanyName"].Value?.ToString() ?? "";

            grpBranches.Text = $"🏢 Υποκαταστήματα Εταιρείας: {companyName}";

            LoadCompanyFields(_selectedCompanyId);
            LoadBranches(_selectedCompanyId);
        }

        private void LoadCompanyFields(int companyId)
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT * FROM Companies WHERE Id = @Id", new SqlParameter("@Id", companyId));
                if (dt.Rows.Count == 0) return;
                DataRow r = dt.Rows[0];

                txtCompanyName.Text = GetCol(r, "CompanyName");
                txtTradeName.Text = GetCol(r, "TradeName");
                txtVatNumber.Text = GetCol(r, "VatNumber");
                txtDoy.Text = GetCol(r, "Doy");
                txtAddress.Text = GetCol(r, "Address");
                txtCity.Text = GetCol(r, "City");
                txtPostalCode.Text = GetCol(r, "PostalCode");

                txtPhone.Text = GetCol(r, "Phone");
                txtPhone2.Text = GetCol(r, "Phone2");
                txtPhone3.Text = GetCol(r, "Phone3");
                txtMobile.Text = GetCol(r, "Mobile");
                txtMobile2.Text = GetCol(r, "Mobile2");
                txtEmail.Text = GetCol(r, "Email");
                txtWebsite.Text = GetCol(r, "Website");

                txtBankName.Text = GetCol(r, "BankName");
                txtBankHolder.Text = GetCol(r, "BankAccountHolder");
                txtBankIban.Text = GetCol(r, "BankIban");
                txtBankIban2.Text = GetCol(r, "BankIban2");
                txtBankIban3.Text = GetCol(r, "BankIban3");
                txtBankIban4.Text = GetCol(r, "BankIban4");
                txtBankIban5.Text = GetCol(r, "BankIban5");
                txtBankIban6.Text = GetCol(r, "BankIban6");

                picLogo.Image = null;
                string logoBase64 = GetCol(r, "LogoBase64");
                if (!string.IsNullOrEmpty(logoBase64))
                {
                    try { picLogo.Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(logoBase64))); } catch { }
                }

                chkActive.Checked = r["IsActive"] != DBNull.Value && Convert.ToBoolean(r["IsActive"]);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης στοιχείων: " + ex.Message); }
        }

        private void btnNewCompany_Click(object sender, EventArgs e)
        {
            _selectedCompanyId = 0;
            ClearCompanyFields();
            txtCompanyName.Focus();
        }

        private void ClearCompanyFields()
        {
            foreach (var t in new[] { txtCompanyName, txtTradeName, txtVatNumber, txtDoy, txtAddress, txtCity, txtPostalCode,
                txtPhone, txtPhone2, txtPhone3, txtMobile, txtMobile2, txtEmail, txtWebsite,
                txtBankName, txtBankHolder, txtBankIban, txtBankIban2, txtBankIban3, txtBankIban4, txtBankIban5, txtBankIban6 })
                t.Text = "";
            picLogo.Image = null;
            chkActive.Checked = true;
        }

        private void btnSaveCompany_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text) || string.IsNullOrWhiteSpace(txtVatNumber.Text))
            { MessageBox.Show("⚠️ Επωνυμία και ΑΦΜ είναι υποχρεωτικά!", "Προσοχή"); return; }

            string logoBase64 = GetLogoBase64(picLogo);

            try
            {
                if (_selectedCompanyId == 0)
                {
                    DataTable res = DatabaseHelper.ExecuteQuery(@"
                        INSERT INTO Companies (CompanyName, TradeName, VatNumber, Doy, Address, City, PostalCode,
                            Phone, Phone2, Phone3, Mobile, Mobile2, Email, Website,
                            BankName, BankAccountHolder, BankIban, BankIban2, BankIban3, BankIban4, BankIban5, BankIban6,
                            LogoBase64, IsActive, IsDefault)
                        OUTPUT INSERTED.Id
                        VALUES (@Name,@Trade,@Vat,@Doy,@Addr,@City,@PC,@Phone,@Phone2,@Phone3,@Mobile,@Mobile2,@Email,@Web,
                            @BankName,@Holder,@Iban,@Iban2,@Iban3,@Iban4,@Iban5,@Iban6,@Logo,@Active,0)",
                        PCompany(logoBase64));
                    _selectedCompanyId = Convert.ToInt32(res.Rows[0][0]);
                }
                else
                {
                    var ps = new List<SqlParameter>(PCompany(logoBase64));
                    ps.Add(new SqlParameter("@Id", _selectedCompanyId));
                    DatabaseHelper.ExecuteQuery(@"
                        UPDATE Companies SET CompanyName=@Name, TradeName=@Trade, VatNumber=@Vat, Doy=@Doy,
                            Address=@Addr, City=@City, PostalCode=@PC,
                            Phone=@Phone, Phone2=@Phone2, Phone3=@Phone3, Mobile=@Mobile, Mobile2=@Mobile2, Email=@Email, Website=@Web,
                            BankName=@BankName, BankAccountHolder=@Holder,
                            BankIban=@Iban, BankIban2=@Iban2, BankIban3=@Iban3, BankIban4=@Iban4, BankIban5=@Iban5, BankIban6=@Iban6,
                            LogoBase64=@Logo, IsActive=@Active, UpdatedAt=GETDATE()
                        WHERE Id=@Id", ps.ToArray());
                }
                MessageBox.Show("✅ Η εταιρεία αποθηκεύτηκε!", "Επιτυχία");
                LoadCompanies();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα αποθήκευσης: " + ex.Message); }
        }

        private string GetLogoBase64(PictureBox pic)
        {
            if (pic.Image == null) return "";
            using (MemoryStream ms = new MemoryStream())
            {
                pic.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private SqlParameter[] PCompany(string logoBase64)
        {
            return new SqlParameter[] {
                new SqlParameter("@Name", txtCompanyName.Text.Trim()), new SqlParameter("@Trade", txtTradeName.Text.Trim()),
                new SqlParameter("@Vat", txtVatNumber.Text.Trim()), new SqlParameter("@Doy", txtDoy.Text.Trim()),
                new SqlParameter("@Addr", txtAddress.Text.Trim()), new SqlParameter("@City", txtCity.Text.Trim()),
                new SqlParameter("@PC", txtPostalCode.Text.Trim()),
                new SqlParameter("@Phone", txtPhone.Text.Trim()), new SqlParameter("@Phone2", txtPhone2.Text.Trim()),
                new SqlParameter("@Phone3", txtPhone3.Text.Trim()), new SqlParameter("@Mobile", txtMobile.Text.Trim()),
                new SqlParameter("@Mobile2", txtMobile2.Text.Trim()), new SqlParameter("@Email", txtEmail.Text.Trim()),
                new SqlParameter("@Web", txtWebsite.Text.Trim()),
                new SqlParameter("@BankName", txtBankName.Text.Trim()), new SqlParameter("@Holder", txtBankHolder.Text.Trim()),
                new SqlParameter("@Iban", txtBankIban.Text.Trim()), new SqlParameter("@Iban2", txtBankIban2.Text.Trim()),
                new SqlParameter("@Iban3", txtBankIban3.Text.Trim()), new SqlParameter("@Iban4", txtBankIban4.Text.Trim()),
                new SqlParameter("@Iban5", txtBankIban5.Text.Trim()), new SqlParameter("@Iban6", txtBankIban6.Text.Trim()),
                new SqlParameter("@Logo", string.IsNullOrEmpty(logoBase64) ? (object)DBNull.Value : logoBase64),
                new SqlParameter("@Active", chkActive.Checked)
            };
        }

        private void btnUploadLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Εικόνες|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                { try { picLogo.Image = Image.FromFile(ofd.FileName); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); } }
            }
        }

        private void btnRemoveLogo_Click(object sender, EventArgs e) { picLogo.Image = null; }

        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            if (_selectedCompanyId == 0) { MessageBox.Show("⚠️ Επίλεξε εταιρεία!", "Προσοχή"); return; }
            try
            {
                DatabaseHelper.ExecuteQuery("UPDATE Companies SET IsDefault = 0");
                DatabaseHelper.ExecuteQuery("UPDATE Companies SET IsDefault = 1 WHERE Id = @Id", new SqlParameter("@Id", _selectedCompanyId));
                MessageBox.Show("✅ Ορίστηκε ως προεπιλεγμένη!", "Επιτυχία");
                LoadCompanies();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnDeleteCompany_Click(object sender, EventArgs e)
        {
            if (_selectedCompanyId == 0) { MessageBox.Show("⚠️ Επίλεξε εταιρεία!", "Προσοχή"); return; }
            if (MessageBox.Show("⚠️ Απενεργοποίηση εταιρείας;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try { DatabaseHelper.ExecuteQuery("UPDATE Companies SET IsActive = 0 WHERE Id = @Id", new SqlParameter("@Id", _selectedCompanyId)); LoadCompanies(); }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        // ==========================================
        // ΥΠΟΚΑΤΑΣΤΗΜΑΤΑ (με ΟΛΑ τα πεδία)
        // ==========================================
        private void LoadBranches(int companyId)
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    @"SELECT b.Id, b.BranchName AS [Υποκατάστημα], b.BranchCode AS [Κωδικός], b.City AS [Πόλη],
                             b.Phone AS [Τηλέφωνο], b.ManagerName AS [Υπεύθυνος],
                             CASE WHEN b.IsMain = 1 THEN '✓' ELSE '' END AS [Κεντρικό], c.CompanyName AS [Εταιρεία]
                      FROM CompanyBranches b INNER JOIN Companies c ON b.CompanyId = c.Id
                      WHERE b.CompanyId = @Id AND b.IsActive = 1
                      ORDER BY b.IsMain DESC, b.BranchName",
                    new SqlParameter("@Id", companyId));

                dgvBranches.DataSource = dt;
                if (dgvBranches.Columns.Contains("Id")) dgvBranches.Columns["Id"].Visible = false;
                dgvBranches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (dgvBranches.Columns.Contains("Εταιρεία"))
                {
                    dgvBranches.Columns["Εταιρεία"].DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220);
                    dgvBranches.Columns["Εταιρεία"].DefaultCellStyle.Font = new Font(dgvBranches.Font, FontStyle.Bold);
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης υποκαταστημάτων: " + ex.Message); }
        }

        private void dgvBranches_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBranches.CurrentRow == null || dgvBranches.CurrentRow.IsNewRow) return;
            if (dgvBranches.CurrentRow.Cells["Id"].Value == null) return;

            _selectedBranchId = Convert.ToInt32(dgvBranches.CurrentRow.Cells["Id"].Value);
            LoadBranchFields(_selectedBranchId);
        }

        private void LoadBranchFields(int branchId)
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT * FROM CompanyBranches WHERE Id = @Id", new SqlParameter("@Id", branchId));
                if (dt.Rows.Count == 0) return;
                DataRow r = dt.Rows[0];

                // Γενικά
                txtBranchName.Text = GetCol(r, "BranchName");
                txtBranchCode.Text = GetCol(r, "BranchCode");
                txtBranchVat.Text = GetCol(r, "VatNumber");
                txtBranchDoy.Text = GetCol(r, "Doy");
                txtBranchAddress.Text = GetCol(r, "Address");
                txtBranchCity.Text = GetCol(r, "City");
                txtBranchPostal.Text = GetCol(r, "PostalCode");
                chkBranchMain.Checked = r["IsMain"] != DBNull.Value && Convert.ToBoolean(r["IsMain"]);

                // Επικοινωνία
                txtBranchPhone.Text = GetCol(r, "Phone");
                txtBranchPhone2.Text = GetCol(r, "Phone2");
                txtBranchPhone3.Text = GetCol(r, "Phone3");
                txtBranchMobile.Text = GetCol(r, "Mobile");
                txtBranchMobile2.Text = GetCol(r, "Mobile2");
                txtBranchEmail.Text = GetCol(r, "Email");
                txtBranchWebsite.Text = GetCol(r, "Website");
                txtBranchManager.Text = GetCol(r, "ManagerName");

                // Τράπεζα
                txtBranchBankName.Text = GetCol(r, "BankName");
                txtBranchBankHolder.Text = GetCol(r, "BankAccountHolder");
                txtBranchIban.Text = GetCol(r, "BankIban");
                txtBranchIban2.Text = GetCol(r, "BankIban2");
                txtBranchIban3.Text = GetCol(r, "BankIban3");
                txtBranchIban4.Text = GetCol(r, "BankIban4");
                txtBranchIban5.Text = GetCol(r, "BankIban5");
                txtBranchIban6.Text = GetCol(r, "BankIban6");

                // Logo
                picBranchLogo.Image = null;
                string logoBase64 = GetCol(r, "LogoBase64");
                if (!string.IsNullOrEmpty(logoBase64))
                { try { picBranchLogo.Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(logoBase64))); } catch { } }
            }
            catch { }
        }

        private void btnNewBranch_Click(object sender, EventArgs e)
        {
            if (_selectedCompanyId == 0) { MessageBox.Show("⚠️ Επίλεξε πρώτα εταιρεία!", "Προσοχή"); return; }
            _selectedBranchId = 0;
            ClearBranchFields();
            txtBranchName.Focus();
        }

        private void ClearBranchFields()
        {
            foreach (var t in new[] { txtBranchName, txtBranchCode, txtBranchVat, txtBranchDoy, txtBranchAddress, txtBranchCity, txtBranchPostal,
                txtBranchPhone, txtBranchPhone2, txtBranchPhone3, txtBranchMobile, txtBranchMobile2, txtBranchEmail, txtBranchWebsite, txtBranchManager,
                txtBranchBankName, txtBranchBankHolder, txtBranchIban, txtBranchIban2, txtBranchIban3, txtBranchIban4, txtBranchIban5, txtBranchIban6 })
                t.Text = "";
            picBranchLogo.Image = null;
            chkBranchMain.Checked = false;
        }

        private void btnSaveBranch_Click(object sender, EventArgs e)
        {
            if (_selectedCompanyId == 0) { MessageBox.Show("⚠️ Επίλεξε πρώτα εταιρεία!", "Προσοχή"); return; }
            if (string.IsNullOrWhiteSpace(txtBranchName.Text)) { MessageBox.Show("⚠️ Όνομα υποκαταστήματος υποχρεωτικό!", "Προσοχή"); return; }

            string logoBase64 = GetLogoBase64(picBranchLogo);

            try
            {
                if (chkBranchMain.Checked)
                    DatabaseHelper.ExecuteQuery("UPDATE CompanyBranches SET IsMain = 0 WHERE CompanyId = @Id", new SqlParameter("@Id", _selectedCompanyId));

                if (_selectedBranchId == 0)
                {
                    DatabaseHelper.ExecuteQuery(@"
                        INSERT INTO CompanyBranches (CompanyId, BranchName, BranchCode, VatNumber, Doy, Address, City, PostalCode,
                            Phone, Phone2, Phone3, Mobile, Mobile2, Email, Website, ManagerName,
                            BankName, BankAccountHolder, BankIban, BankIban2, BankIban3, BankIban4, BankIban5, BankIban6,
                            LogoBase64, IsMain, IsActive)
                        VALUES (@CompId,@Name,@Code,@Vat,@Doy,@Addr,@City,@PC,@Phone,@Phone2,@Phone3,@Mobile,@Mobile2,@Email,@Web,@Manager,
                            @BankName,@Holder,@Iban,@Iban2,@Iban3,@Iban4,@Iban5,@Iban6,@Logo,@Main,1)",
                        PBranch(logoBase64));
                }
                else
                {
                    var ps = new List<SqlParameter>(PBranch(logoBase64));
                    ps.Add(new SqlParameter("@Id", _selectedBranchId));
                    DatabaseHelper.ExecuteQuery(@"
                        UPDATE CompanyBranches SET BranchName=@Name, BranchCode=@Code, VatNumber=@Vat, Doy=@Doy,
                            Address=@Addr, City=@City, PostalCode=@PC,
                            Phone=@Phone, Phone2=@Phone2, Phone3=@Phone3, Mobile=@Mobile, Mobile2=@Mobile2, Email=@Email, Website=@Web, ManagerName=@Manager,
                            BankName=@BankName, BankAccountHolder=@Holder,
                            BankIban=@Iban, BankIban2=@Iban2, BankIban3=@Iban3, BankIban4=@Iban4, BankIban5=@Iban5, BankIban6=@Iban6,
                            LogoBase64=@Logo, IsMain=@Main
                        WHERE Id=@Id", ps.ToArray());
                }

                MessageBox.Show("✅ Το υποκατάστημα αποθηκεύτηκε!", "Επιτυχία");
                LoadBranches(_selectedCompanyId);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα αποθήκευσης: " + ex.Message); }
        }

        private SqlParameter[] PBranch(string logoBase64)
        {
            return new SqlParameter[] {
                new SqlParameter("@CompId", _selectedCompanyId),
                new SqlParameter("@Name", txtBranchName.Text.Trim()), new SqlParameter("@Code", txtBranchCode.Text.Trim()),
                new SqlParameter("@Vat", txtBranchVat.Text.Trim()), new SqlParameter("@Doy", txtBranchDoy.Text.Trim()),
                new SqlParameter("@Addr", txtBranchAddress.Text.Trim()), new SqlParameter("@City", txtBranchCity.Text.Trim()),
                new SqlParameter("@PC", txtBranchPostal.Text.Trim()),
                new SqlParameter("@Phone", txtBranchPhone.Text.Trim()), new SqlParameter("@Phone2", txtBranchPhone2.Text.Trim()),
                new SqlParameter("@Phone3", txtBranchPhone3.Text.Trim()), new SqlParameter("@Mobile", txtBranchMobile.Text.Trim()),
                new SqlParameter("@Mobile2", txtBranchMobile2.Text.Trim()), new SqlParameter("@Email", txtBranchEmail.Text.Trim()),
                new SqlParameter("@Web", txtBranchWebsite.Text.Trim()), new SqlParameter("@Manager", txtBranchManager.Text.Trim()),
                new SqlParameter("@BankName", txtBranchBankName.Text.Trim()), new SqlParameter("@Holder", txtBranchBankHolder.Text.Trim()),
                new SqlParameter("@Iban", txtBranchIban.Text.Trim()), new SqlParameter("@Iban2", txtBranchIban2.Text.Trim()),
                new SqlParameter("@Iban3", txtBranchIban3.Text.Trim()), new SqlParameter("@Iban4", txtBranchIban4.Text.Trim()),
                new SqlParameter("@Iban5", txtBranchIban5.Text.Trim()), new SqlParameter("@Iban6", txtBranchIban6.Text.Trim()),
                new SqlParameter("@Logo", string.IsNullOrEmpty(logoBase64) ? (object)DBNull.Value : logoBase64),
                new SqlParameter("@Main", chkBranchMain.Checked)
            };
        }

        private void btnDeleteBranch_Click(object sender, EventArgs e)
        {
            if (_selectedBranchId == 0) { MessageBox.Show("⚠️ Επίλεξε υποκατάστημα!", "Προσοχή"); return; }
            if (MessageBox.Show("⚠️ Απενεργοποίηση υποκαταστήματος;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try { DatabaseHelper.ExecuteQuery("UPDATE CompanyBranches SET IsActive = 0 WHERE Id = @Id", new SqlParameter("@Id", _selectedBranchId)); LoadBranches(_selectedCompanyId); }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnUploadBranchLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Εικόνες|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                { try { picBranchLogo.Image = Image.FromFile(ofd.FileName); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); } }
            }
        }

        private void btnRemoveBranchLogo_Click(object sender, EventArgs e) { picBranchLogo.Image = null; }

        // ==========================================
        // 📦 ΑΠΟΘΗΚΕΣ ΕΤΑΙΡΕΙΑΣ
        // ==========================================
        private void btnWarehouses_Click(object sender, EventArgs e)
        {
            if (_selectedCompanyId == 0)
            {
                MessageBox.Show("⚠️ Επίλεξε πρώτα εταιρεία!", "Προσοχή");
                return;
            }

            using var form = new WarehousesForm();
            {
                form.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}