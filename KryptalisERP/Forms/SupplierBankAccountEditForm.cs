using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class SupplierBankAccountEditForm : Form
    {
        private readonly int _supplierId;
        private readonly int _bankId;
        private readonly string _connStr;

        public SupplierBankAccountEditForm(int supplierId, int bankId = 0)
        {
            InitializeComponent();
            _supplierId = supplierId;
            _bankId = bankId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";

            this.Load += SupplierBankAccountEditForm_Load;
        }

        private void SupplierBankAccountEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _bankId > 0 ? "✏️ Επεξεργασία Τράπεζας" : "➕ Νέα Τράπεζα";
            LoadBanks(); // ✅ Φόρτωση τραπεζών από πίνακα Bank
            if (_bankId > 0) LoadData();
        }

        private void LoadBanks()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var banks = conn.Query("SELECT Id, Name FROM Bank ORDER BY Name").ToList();
                cmbBank.DataSource = banks;
                cmbBank.DisplayMember = "Name";
                cmbBank.ValueMember = "Id";
            }
            catch
            {
                // Fallback αν δεν υπάρχει πίνακας Bank
                cmbBank.Items.Clear();
                cmbBank.Items.Add("Alpha Bank");
                cmbBank.Items.Add("Eurobank");
                cmbBank.Items.Add("National Bank");
                cmbBank.Items.Add("Piraeus Bank");
            }
        }

        private void LoadData()
        {
            using var conn = new SqlConnection(_connStr);
            var bank = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM SupplierBankAccounts WHERE Id = @Id", new { Id = _bankId });
            if (bank == null) return;

            txtAccountNumber.Text = bank.AccountNumber;
            txtIBAN.Text = bank.IBAN;
            txtSWIFT.Text = bank.SWIFT;
            txtCurrency.Text = bank.Currency ?? "EUR";
            chkIsDefault.Checked = bank.IsDefault;
            chkIsActive.Checked = bank.IsActive;
            txtNotes.Text = bank.Notes;

            // ✅ Επιλογή τράπεζας από ComboBox
            if (bank.BankId != null)
            {
                cmbBank.SelectedValue = bank.BankId;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text))
            {
                MessageBox.Show("⚠️ Ο Αριθμός Λογαριασμού είναι υποχρεωτικός!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                using var tran = conn.BeginTransaction();

                int? bankIdValue = cmbBank.SelectedValue as int?;

                if (_bankId > 0)
                {
                    conn.Execute(@"UPDATE SupplierBankAccounts SET BankId=@BankId, AccountNumber=@AccountNumber, IBAN=@IBAN, SWIFT=@SWIFT, Currency=@Currency, IsDefault=@IsDefault, IsActive=@IsActive, Notes=@Notes 
                                   WHERE Id=@Id AND SupplierId=@SupplierId",
                        new
                        {
                            Id = _bankId,
                            SupplierId = _supplierId,
                            BankId = bankIdValue,
                            AccountNumber = txtAccountNumber.Text.Trim(),
                            IBAN = txtIBAN.Text.Trim(),
                            SWIFT = txtSWIFT.Text.Trim(),
                            Currency = txtCurrency.Text.Trim(),
                            IsDefault = chkIsDefault.Checked,
                            IsActive = chkIsActive.Checked,
                            Notes = txtNotes.Text.Trim()
                        }, tran);
                }
                else
                {
                    conn.Execute(@"INSERT INTO SupplierBankAccounts (SupplierId, BankId, AccountNumber, IBAN, SWIFT, Currency, IsDefault, IsActive, Notes) 
                                   VALUES (@SupplierId, @BankId, @AccountNumber, @IBAN, @SWIFT, @Currency, @IsDefault, @IsActive, @Notes)",
                        new
                        {
                            SupplierId = _supplierId,
                            BankId = bankIdValue,
                            AccountNumber = txtAccountNumber.Text.Trim(),
                            IBAN = txtIBAN.Text.Trim(),
                            SWIFT = txtSWIFT.Text.Trim(),
                            Currency = txtCurrency.Text.Trim(),
                            IsDefault = chkIsDefault.Checked,
                            IsActive = chkIsActive.Checked,
                            Notes = txtNotes.Text.Trim()
                        }, tran);
                }

                tran.Commit();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}