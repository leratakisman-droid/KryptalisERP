using System;
using System.Data;
using System.Windows.Forms;
using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;

namespace KryptalisERP
{
    public partial class UserEditForm : Form
    {
        private int _id;

        public UserEditForm(int id = 0)
        {
            InitializeComponent();
            _id = id;
            if (_id > 0) { this.Text = "✏️ Επεξεργασία Χρήστη"; LoadData(); }
            else { this.Text = "➕ Νέος Χρήστης"; }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT * FROM Users WHERE Id = @Id", new SqlParameter("@Id", _id));
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtUsername.Text = r["Username"]?.ToString() ?? "";
                    txtFullName.Text = r["FullName"]?.ToString() ?? "";
                    txtEmail.Text = r["Email"]?.ToString() ?? "";
                    txtPhone.Text = r["Phone"]?.ToString() ?? "";
                    cmbRole.Text = r["Role"]?.ToString() ?? "";
                    txtPassword.Text = ""; // Δεν φορτώνουμε τον κωδικό

                    // Φόρτωση δικαιωμάτων
                    if (dt.Columns.Contains("CanClients")) chkClients.Checked = Convert.ToBoolean(r["CanClients"]);
                    if (dt.Columns.Contains("CanSuppliers")) chkSuppliers.Checked = Convert.ToBoolean(r["CanSuppliers"]);
                    if (dt.Columns.Contains("CanSales")) chkSales.Checked = Convert.ToBoolean(r["CanSales"]);
                    if (dt.Columns.Contains("CanPurchases")) chkPurchases.Checked = Convert.ToBoolean(r["CanPurchases"]);
                    if (dt.Columns.Contains("CanSettings")) chkSettings.Checked = Convert.ToBoolean(r["CanSettings"]);

                    chkActive.Checked = Convert.ToBoolean(r["IsActive"]);
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                MessageBox.Show("Συμπλήρωσε Username!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string password = txtPassword.Text.Trim();
                if (_id == 0 && string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Συμπλήρωσε κωδικό για τον νέο χρήστη!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var res = DatabaseHelper.ExecuteQuery(
                    "EXEC sp_SaveUser @Id, @Username, @FullName, @Role, @Email, @Phone, @Password, @Active, @CanClients, @CanSuppliers, @CanSales, @CanPurchases, @CanSettings",
                    new SqlParameter("@Id", _id > 0 ? (object)_id : DBNull.Value),
                    new SqlParameter("@Username", txtUsername.Text.Trim()),
                    new SqlParameter("@FullName", txtFullName.Text.Trim()),
                    new SqlParameter("@Role", cmbRole.Text.Trim()),
                    new SqlParameter("@Email", string.IsNullOrEmpty(txtEmail.Text.Trim()) ? (object)DBNull.Value : txtEmail.Text.Trim()),
                    new SqlParameter("@Phone", string.IsNullOrEmpty(txtPhone.Text.Trim()) ? (object)DBNull.Value : txtPhone.Text.Trim()),
                    new SqlParameter("@Password", string.IsNullOrEmpty(password) ? (object)DBNull.Value : password),
                    new SqlParameter("@Active", chkActive.Checked),
                    new SqlParameter("@CanClients", chkClients.Checked),
                    new SqlParameter("@CanSuppliers", chkSuppliers.Checked),
                    new SqlParameter("@CanSales", chkSales.Checked),
                    new SqlParameter("@CanPurchases", chkPurchases.Checked),
                    new SqlParameter("@CanSettings", chkSettings.Checked));

                if (Convert.ToInt32(res.Rows[0]["Success"]) == 1)
                {
                    MessageBox.Show("✅ " + res.Rows[0]["Message"], "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}