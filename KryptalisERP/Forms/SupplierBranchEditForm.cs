using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class SupplierBranchEditForm : Form
    {
        private readonly int _supplierId;
        private readonly int _branchId;
        private readonly string _connStr;

        public SupplierBranchEditForm(int supplierId, int branchId = 0)
        {
            InitializeComponent();
            _supplierId = supplierId;
            _branchId = branchId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += SupplierBranchEditForm_Load;
        }

        private void SupplierBranchEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _branchId > 0 ? "️ Επεξεργασία Υποκαταστήματος" : "➕ Νέο Υποκατάστημα";
            LoadDOY();
            LoadBanks();
            if (_branchId > 0) LoadData();
        }

        private void LoadDOY()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var doys = conn.Query("SELECT Id, Name FROM GreekTaxOffices WHERE IsActive = 1 ORDER BY Name").ToList();
                cmbDoy.DataSource = doys;
                cmbDoy.DisplayMember = "Name";
                cmbDoy.ValueMember = "Id";
                cmbDoy.DropDownStyle = ComboBoxStyle.DropDown;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης ΔΟΥ: " + ex.Message); }
        }

        private void LoadBanks()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var banks = conn.Query("SELECT Id, Name FROM Bank ORDER BY Name").ToList();
                cmbBank.DataSource = banks.ToList(); cmbBank.DisplayMember = "Name"; cmbBank.ValueMember = "Id";
                cmbBank2.DataSource = banks.ToList(); cmbBank2.DisplayMember = "Name"; cmbBank2.ValueMember = "Id";
                cmbBank3.DataSource = banks.ToList(); cmbBank3.DisplayMember = "Name"; cmbBank3.ValueMember = "Id";
            }
            catch
            {
                string[] bankNames = { "Alpha Bank", "Eurobank", "National Bank", "Piraeus Bank" };
                cmbBank.Items.Clear(); cmbBank.Items.AddRange(bankNames);
                cmbBank2.Items.Clear(); cmbBank2.Items.AddRange(bankNames);
                cmbBank3.Items.Clear(); cmbBank3.Items.AddRange(bankNames);
            }
        }

        private void LoadData()
        {
            using var conn = new SqlConnection(_connStr);
            var branch = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM SupplierBranches WHERE Id = @Id", new { Id = _branchId });
            if (branch == null) return;

            txtBranchName.Text = branch.BranchName ?? "";
            txtBranchCode.Text = branch.BranchCode ?? "";
            txtAddress.Text = branch.Address ?? "";
            txtCity.Text = branch.City ?? "";
            txtArea.Text = branch.Area ?? "";
            txtPostalCode.Text = branch.PostalCode ?? "";
            if (!string.IsNullOrEmpty(branch.TaxOffice)) cmbDoy.Text = branch.TaxOffice;
            txtPhone1.Text = branch.Phone1 ?? "";
            txtPhone2.Text = branch.Phone2 ?? "";
            txtPhone3.Text = branch.Phone3 ?? "";
            txtEmail.Text = branch.Email ?? "";
            txtIban1.Text = branch.Iban1 ?? "";
            txtIban2.Text = branch.Iban2 ?? "";
            txtIban3.Text = branch.Iban3 ?? "";
            txtContactPerson.Text = branch.ContactPerson ?? "";
            txtNotes.Text = branch.Notes ?? "";
            chkIsActive.Checked = branch.IsActive;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBranchName.Text.Trim()))
            {
                MessageBox.Show("⚠️ Το όνομα είναι υποχρεωτικό!");
                txtBranchName.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                using var tran = conn.BeginTransaction();

                if (_branchId > 0)
                {
                    conn.Execute(@"UPDATE SupplierBranches SET 
                        BranchName=@BranchName, BranchCode=@BranchCode, Address=@Address, City=@City, 
                        PostalCode=@PostalCode, Area=@Area, TaxOffice=@TaxOffice, 
                        Phone1=@Phone1, Phone2=@Phone2, Phone3=@Phone3, 
                        Email=@Email, Iban1=@Iban1, Iban2=@Iban2, Iban3=@Iban3, 
                        ContactPerson=@ContactPerson, Notes=@Notes, IsActive=@IsActive, UpdatedAt=GETDATE()
                        WHERE Id=@Id AND SupplierId=@SupplierId",
                        new
                        {
                            Id = _branchId,
                            SupplierId = _supplierId,
                            BranchName = txtBranchName.Text.Trim(),
                            BranchCode = string.IsNullOrWhiteSpace(txtBranchCode.Text) ? (object)DBNull.Value : txtBranchCode.Text.Trim(),
                            Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim(),
                            City = string.IsNullOrWhiteSpace(txtCity.Text) ? (object)DBNull.Value : txtCity.Text.Trim(),
                            PostalCode = string.IsNullOrWhiteSpace(txtPostalCode.Text) ? (object)DBNull.Value : txtPostalCode.Text.Trim(),
                            Area = string.IsNullOrWhiteSpace(txtArea.Text) ? (object)DBNull.Value : txtArea.Text.Trim(),
                            TaxOffice = string.IsNullOrWhiteSpace(cmbDoy.Text) ? (object)DBNull.Value : cmbDoy.Text.Trim(),
                            Phone1 = string.IsNullOrWhiteSpace(txtPhone1.Text) ? (object)DBNull.Value : txtPhone1.Text.Trim(),
                            Phone2 = string.IsNullOrWhiteSpace(txtPhone2.Text) ? (object)DBNull.Value : txtPhone2.Text.Trim(),
                            Phone3 = string.IsNullOrWhiteSpace(txtPhone3.Text) ? (object)DBNull.Value : txtPhone3.Text.Trim(),
                            Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim(),
                            Iban1 = string.IsNullOrWhiteSpace(txtIban1.Text) ? (object)DBNull.Value : txtIban1.Text.Trim(),
                            Iban2 = string.IsNullOrWhiteSpace(txtIban2.Text) ? (object)DBNull.Value : txtIban2.Text.Trim(),
                            Iban3 = string.IsNullOrWhiteSpace(txtIban3.Text) ? (object)DBNull.Value : txtIban3.Text.Trim(),
                            ContactPerson = string.IsNullOrWhiteSpace(txtContactPerson.Text) ? (object)DBNull.Value : txtContactPerson.Text.Trim(),
                            Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text.Trim(),
                            IsActive = chkIsActive.Checked
                        }, tran);
                }
                else
                {
                    conn.Execute(@"INSERT INTO SupplierBranches 
                        (SupplierId, BranchName, BranchCode, Address, City, PostalCode, Area, TaxOffice, 
                         Phone1, Phone2, Phone3, Email, Iban1, Iban2, Iban3, 
                         ContactPerson, Notes, IsActive, CreatedAt) 
                        VALUES 
                        (@SupplierId, @BranchName, @BranchCode, @Address, @City, @PostalCode, @Area, @TaxOffice, 
                         @Phone1, @Phone2, @Phone3, @Email, @Iban1, @Iban2, @Iban3, 
                         @ContactPerson, @Notes, @IsActive, GETDATE())",
                        new
                        {
                            SupplierId = _supplierId,
                            BranchName = txtBranchName.Text.Trim(),
                            BranchCode = string.IsNullOrWhiteSpace(txtBranchCode.Text) ? (object)DBNull.Value : txtBranchCode.Text.Trim(),
                            Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim(),
                            City = string.IsNullOrWhiteSpace(txtCity.Text) ? (object)DBNull.Value : txtCity.Text.Trim(),
                            PostalCode = string.IsNullOrWhiteSpace(txtPostalCode.Text) ? (object)DBNull.Value : txtPostalCode.Text.Trim(),
                            Area = string.IsNullOrWhiteSpace(txtArea.Text) ? (object)DBNull.Value : txtArea.Text.Trim(),
                            TaxOffice = string.IsNullOrWhiteSpace(cmbDoy.Text) ? (object)DBNull.Value : cmbDoy.Text.Trim(),
                            Phone1 = string.IsNullOrWhiteSpace(txtPhone1.Text) ? (object)DBNull.Value : txtPhone1.Text.Trim(),
                            Phone2 = string.IsNullOrWhiteSpace(txtPhone2.Text) ? (object)DBNull.Value : txtPhone2.Text.Trim(),
                            Phone3 = string.IsNullOrWhiteSpace(txtPhone3.Text) ? (object)DBNull.Value : txtPhone3.Text.Trim(),
                            Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim(),
                            Iban1 = string.IsNullOrWhiteSpace(txtIban1.Text) ? (object)DBNull.Value : txtIban1.Text.Trim(),
                            Iban2 = string.IsNullOrWhiteSpace(txtIban2.Text) ? (object)DBNull.Value : txtIban2.Text.Trim(),
                            Iban3 = string.IsNullOrWhiteSpace(txtIban3.Text) ? (object)DBNull.Value : txtIban3.Text.Trim(),
                            ContactPerson = string.IsNullOrWhiteSpace(txtContactPerson.Text) ? (object)DBNull.Value : txtContactPerson.Text.Trim(),
                            Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text.Trim(),
                            IsActive = chkIsActive.Checked
                        }, tran);
                }

                tran.Commit();
                MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}