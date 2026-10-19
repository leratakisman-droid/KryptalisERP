using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class CustomerBranchEditForm : Form
    {
        private readonly int _customerId;
        private readonly int _branchId;
        private readonly string _connStr;

        public CustomerBranchEditForm(int customerId, int branchId = 0)
        {
            InitializeComponent();
            _customerId = customerId;
            _branchId = branchId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += CustomerBranchEditForm_Load;
        }

        private void CustomerBranchEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _branchId > 0 ? "✏️ Επεξεργασία Υποκαταστήματος" : " Νέο Υποκατάστημα";
            if (_branchId > 0) LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var branch = conn.QueryFirstOrDefault<dynamic>(
                    "SELECT * FROM CustomerBranches WHERE Id = @Id",
                    new { Id = _branchId });

                if (branch == null) return;

                txtBranchName.Text = branch.BranchName ?? "";
                txtBranchCode.Text = branch.BranchCode ?? "";
                txtAddress.Text = branch.Address ?? "";
                txtCity.Text = branch.City ?? "";
                txtPostalCode.Text = branch.PostalCode ?? "";
                txtPhone.Text = branch.Phone ?? "";
                txtMobile.Text = branch.Mobile ?? "";
                txtEmail.Text = branch.Email ?? "";
                txtManagerName.Text = branch.ManagerName ?? "";
                txtVatNumber.Text = branch.VatNumber ?? "";
                txtDoy.Text = branch.Doy ?? "";
                txtBankName.Text = branch.BankName ?? "";
                txtBankIban.Text = branch.BankIban ?? "";
                txtBankIban2.Text = branch.BankIban2 ?? "";
                txtBankIban3.Text = branch.BankIban3 ?? "";
                txtBankIban4.Text = branch.BankIban4 ?? "";
                txtBankIban5.Text = branch.BankIban5 ?? "";
                txtBankIban6.Text = branch.BankIban6 ?? "";
                chkIsMain.Checked = branch.IsMain;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBranchName.Text))
            {
                MessageBox.Show("⚠️ Το Όνομα Υποκαταστήματος είναι υποχρεωτικό.", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBranchName.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                if (_branchId > 0)
                {
                    conn.Execute(@"UPDATE CustomerBranches SET 
                        BranchName=@BranchName, BranchCode=@BranchCode, Address=@Address, 
                        City=@City, PostalCode=@PostalCode, Phone=@Phone, Mobile=@Mobile, 
                        Email=@Email, ManagerName=@ManagerName, VatNumber=@VatNumber, Doy=@Doy, 
                        BankName=@BankName, BankIban=@BankIban, BankIban2=@BankIban2, 
                        BankIban3=@BankIban3, BankIban4=@BankIban4, BankIban5=@BankIban5, BankIban6=@BankIban6, 
                        IsMain=@IsMain, IsActive=1, UpdatedAt=GETDATE()
                        WHERE Id=@Id AND CustomerId=@CustomerId",
                        new
                        {
                            Id = _branchId,
                            CustomerId = _customerId,
                            BranchName = txtBranchName.Text.Trim(),
                            BranchCode = txtBranchCode.Text.Trim(),
                            Address = txtAddress.Text.Trim(),
                            City = txtCity.Text.Trim(),
                            PostalCode = txtPostalCode.Text.Trim(),
                            Phone = txtPhone.Text.Trim(),
                            Mobile = txtMobile.Text.Trim(),
                            Email = txtEmail.Text.Trim(),
                            ManagerName = txtManagerName.Text.Trim(),
                            VatNumber = txtVatNumber.Text.Trim(),
                            Doy = txtDoy.Text.Trim(),
                            BankName = txtBankName.Text.Trim(),
                            BankIban = txtBankIban.Text.Trim(),
                            BankIban2 = txtBankIban2.Text.Trim(),
                            BankIban3 = txtBankIban3.Text.Trim(),
                            BankIban4 = txtBankIban4.Text.Trim(),
                            BankIban5 = txtBankIban5.Text.Trim(),
                            BankIban6 = txtBankIban6.Text.Trim(),
                            IsMain = chkIsMain.Checked
                        });
                }
                else
                {
                    conn.Execute(@"INSERT INTO CustomerBranches 
                        (CustomerId, BranchName, BranchCode, Address, City, PostalCode, Phone, Mobile, 
                         Email, ManagerName, VatNumber, Doy, BankName, BankIban, BankIban2, BankIban3, 
                         BankIban4, BankIban5, BankIban6, IsMain, IsActive, CreatedAt) 
                        VALUES 
                        (@CustomerId, @BranchName, @BranchCode, @Address, @City, @PostalCode, @Phone, @Mobile, 
                         @Email, @ManagerName, @VatNumber, @Doy, @BankName, @BankIban, @BankIban2, @BankIban3, 
                         @BankIban4, @BankIban5, @BankIban6, @IsMain, 1, GETDATE())",
                        new
                        {
                            CustomerId = _customerId,
                            BranchName = txtBranchName.Text.Trim(),
                            BranchCode = txtBranchCode.Text.Trim(),
                            Address = txtAddress.Text.Trim(),
                            City = txtCity.Text.Trim(),
                            PostalCode = txtPostalCode.Text.Trim(),
                            Phone = txtPhone.Text.Trim(),
                            Mobile = txtMobile.Text.Trim(),
                            Email = txtEmail.Text.Trim(),
                            ManagerName = txtManagerName.Text.Trim(),
                            VatNumber = txtVatNumber.Text.Trim(),
                            Doy = txtDoy.Text.Trim(),
                            BankName = txtBankName.Text.Trim(),
                            BankIban = txtBankIban.Text.Trim(),
                            BankIban2 = txtBankIban2.Text.Trim(),
                            BankIban3 = txtBankIban3.Text.Trim(),
                            BankIban4 = txtBankIban4.Text.Trim(),
                            BankIban5 = txtBankIban5.Text.Trim(),
                            BankIban6 = txtBankIban6.Text.Trim(),
                            IsMain = chkIsMain.Checked
                        });
                }

                MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}