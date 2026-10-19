using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class VatExemptionEditForm : Form
    {
        private int _id;
        public VatExemptionEditForm(int id = 0)
        {
            InitializeComponent();
            _id = id;
            if (_id > 0) { this.Text = "✏️ Επεξεργασία Απαλλαγής"; LoadData(); }
            else { this.Text = "➕ Νέα Απαλλαγή ΦΠΑ"; }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT * FROM VatExemptions WHERE Id = @Id", new SqlParameter("@Id", _id));
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtCode.Text = r["ExemptionCode"]?.ToString() ?? "";
                    txtName.Text = r["Description"]?.ToString() ?? ""; // Χάρταξε το Name στο Description
                    txtMyData.Text = r["LegalReference"]?.ToString() ?? ""; // Χάρταξε το MyData στο LegalReference
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
                    "EXEC sp_SaveVatExemption @Id, @Code, @Desc, @Law, @Active",
                    new SqlParameter("@Id", _id > 0 ? (object)_id : DBNull.Value),
                    new SqlParameter("@Code", txtCode.Text.Trim()),
                    new SqlParameter("@Desc", txtName.Text.Trim()),
                    new SqlParameter("@Law", string.IsNullOrEmpty(txtMyData.Text.Trim()) ? (object)DBNull.Value : txtMyData.Text.Trim()),
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