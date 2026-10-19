using KryptalisERP.Helpers;
using KryptalisERP.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class EmployeeEditForm : Form
    {
        private int _id;
        private bool _isNew;

        public EmployeeEditForm(int id = 0)
        {
            InitializeComponent();
            ThemeManager.ApplyToCurrentForm(this);
            _id = id;
            _isNew = (id == 0);

            if (_isNew)
            {
                this.Text = "➕ Νέος Πωλητής/Εισπράκτορας";
                chkActive.Checked = true;
                GenerateEmployeeCode(); // ✅ Αυτόματη δημιουργία κωδικού
            }
            else
            {
                this.Text = "✏️ Επεξεργασία Πωλητή/Εισπράκτορα";
                LoadData();
            }
        }

        // ✅ ΝΕΑ ΜΕΘΟΔΟΣ: Αυτόματη δημιουργία κωδικού
        private void GenerateEmployeeCode()
        {
            try
            {
                // Παίρνουμε τον τελευταίο κωδικό από τη βάση
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT TOP 1 EmployeeCode FROM Employees WHERE EmployeeCode IS NOT NULL AND EmployeeCode <> '' ORDER BY Id DESC");

                string newCode = "EMP001"; // Default αν δεν υπάρχουν εγγραφές

                if (dt.Rows.Count > 0)
                {
                    string lastCode = dt.Rows[0]["EmployeeCode"]?.ToString() ?? "";

                    if (!string.IsNullOrEmpty(lastCode) && lastCode.Length >= 3)
                    {
                        // Παίρνουμε το αριθμητικό μέρος (π.χ. από "EMP001" παίρνουμε "001")
                        string prefix = lastCode.Substring(0, 3);
                        string numberPart = lastCode.Substring(3);

                        if (int.TryParse(numberPart, out int lastNumber))
                        {
                            int newNumber = lastNumber + 1;
                            newCode = prefix + newNumber.ToString("D3"); // π.χ. "EMP002"
                        }
                    }
                }

                txtCode.Text = newCode;
            }
            catch
            {
                // Αν υπάρχει σφάλμα, βάζουμε default
                txtCode.Text = "EMP001";
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT * FROM Employees WHERE Id = @Id",
                    new SqlParameter("@Id", _id));

                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtCode.Text = r["EmployeeCode"]?.ToString() ?? "";
                    txtFullName.Text = r["FullName"]?.ToString() ?? "";
                    cmbRole.Text = r["Role"]?.ToString() ?? "Πωλητής";
                    txtPhone.Text = r["Phone"]?.ToString() ?? "";
                    txtMobile.Text = r["Mobile"]?.ToString() ?? "";
                    txtEmail.Text = r["Email"]?.ToString() ?? "";
                    txtAFM.Text = r["AFM"]?.ToString() ?? "";
                    numCommission.Value = Convert.ToDecimal(r["CommissionPercent"] ?? 0m);
                    txtRegion.Text = r["Region"]?.ToString() ?? "";
                    txtBankAccount.Text = r["BankAccount"]?.ToString() ?? "";
                    txtNotes.Text = r["Notes"]?.ToString() ?? "";
                    chkActive.Checked = Convert.ToBoolean(r["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("⚠️ Συμπλήρωσε το ονοματεπώνυμο!", "Προσοχή");
                txtFullName.Focus();
                return;
            }

            try
            {
                if (_id > 0)
                {
                    DatabaseHelper.ExecuteQuery(@"
                        UPDATE Employees SET 
                        EmployeeCode = @Code, FullName = @Name, Role = @Role,
                        Phone = @Phone, Mobile = @Mobile, Email = @Email, AFM = @AFM,
                        CommissionPercent = @Commission, Region = @Region,
                        BankAccount = @Bank, Notes = @Notes, IsActive = @Active,
                        UpdatedAt = GETDATE()
                        WHERE Id = @Id",
                        new SqlParameter("@Id", _id),
                        new SqlParameter("@Code", string.IsNullOrEmpty(txtCode.Text) ? (object)DBNull.Value : txtCode.Text.Trim()),
                        new SqlParameter("@Name", txtFullName.Text.Trim()),
                        new SqlParameter("@Role", cmbRole.Text),
                        new SqlParameter("@Phone", string.IsNullOrEmpty(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim()),
                        new SqlParameter("@Mobile", string.IsNullOrEmpty(txtMobile.Text) ? (object)DBNull.Value : txtMobile.Text.Trim()),
                        new SqlParameter("@Email", string.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim()),
                        new SqlParameter("@AFM", string.IsNullOrEmpty(txtAFM.Text) ? (object)DBNull.Value : txtAFM.Text.Trim()),
                        new SqlParameter("@Commission", numCommission.Value),
                        new SqlParameter("@Region", string.IsNullOrEmpty(txtRegion.Text) ? (object)DBNull.Value : txtRegion.Text.Trim()),
                        new SqlParameter("@Bank", string.IsNullOrEmpty(txtBankAccount.Text) ? (object)DBNull.Value : txtBankAccount.Text.Trim()),
                        new SqlParameter("@Notes", string.IsNullOrEmpty(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text.Trim()),
                        new SqlParameter("@Active", chkActive.Checked));
                }
                else
                {
                    DatabaseHelper.ExecuteQuery(@"
                        INSERT INTO Employees 
                        (EmployeeCode, FullName, Role, Phone, Mobile, Email, AFM, CommissionPercent, Region, BankAccount, Notes, IsActive, CreatedAt)
                        VALUES 
                        (@Code, @Name, @Role, @Phone, @Mobile, @Email, @AFM, @Commission, @Region, @Bank, @Notes, @Active, GETDATE())",
                        new SqlParameter("@Code", string.IsNullOrEmpty(txtCode.Text) ? (object)DBNull.Value : txtCode.Text.Trim()),
                        new SqlParameter("@Name", txtFullName.Text.Trim()),
                        new SqlParameter("@Role", cmbRole.Text),
                        new SqlParameter("@Phone", string.IsNullOrEmpty(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim()),
                        new SqlParameter("@Mobile", string.IsNullOrEmpty(txtMobile.Text) ? (object)DBNull.Value : txtMobile.Text.Trim()),
                        new SqlParameter("@Email", string.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim()),
                        new SqlParameter("@AFM", string.IsNullOrEmpty(txtAFM.Text) ? (object)DBNull.Value : txtAFM.Text.Trim()),
                        new SqlParameter("@Commission", numCommission.Value),
                        new SqlParameter("@Region", string.IsNullOrEmpty(txtRegion.Text) ? (object)DBNull.Value : txtRegion.Text.Trim()),
                        new SqlParameter("@Bank", string.IsNullOrEmpty(txtBankAccount.Text) ? (object)DBNull.Value : txtBankAccount.Text.Trim()),
                        new SqlParameter("@Notes", string.IsNullOrEmpty(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text.Trim()),
                        new SqlParameter("@Active", chkActive.Checked));
                }

                MessageBox.Show("✅ Αποθηκεύτηκε επιτυχώς!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα αποθήκευσης: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}