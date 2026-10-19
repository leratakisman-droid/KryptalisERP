using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class WithholdingEditForm : Form
    {
        private int _id;
        public WithholdingEditForm(int id = 0)
        {
            InitializeComponent();
            _id = id;
            if (_id > 0) { this.Text = "✏️ Επεξεργασία Κράτησης"; LoadData(); }
            else { this.Text = "➕ Νέα Κράτηση / Φόρος"; }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT * FROM WithholdingTaxes WHERE Id = @Id", new SqlParameter("@Id", _id));
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtCode.Text = r["WithholdingCode"]?.ToString() ?? "";
                    txtName.Text = r["Description"]?.ToString() ?? "";
                    numRate.Value = Convert.ToDecimal(r["Rate"]);
                    chkActive.Checked = Convert.ToBoolean(r["IsActive"]);
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()) || string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Συμπλήρωσε Κωδικό και Περιγραφή!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var res = DatabaseHelper.ExecuteQuery(
                    "EXEC sp_SaveWithholdingTax @Id, @Code, @Desc, @Rate, @Active",
                    new SqlParameter("@Id", _id > 0 ? (object)_id : DBNull.Value),
                    new SqlParameter("@Code", txtCode.Text.Trim()),
                    new SqlParameter("@Desc", txtName.Text.Trim()),
                    new SqlParameter("@Rate", numRate.Value),
                    new SqlParameter("@Active", chkActive.Checked));

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