using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class VatExemptionsForm : Form
    {
        public VatExemptionsForm() { InitializeComponent(); LoadVatExemptions(); }

        private void LoadVatExemptions()
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(
                "SELECT Id, ExemptionCode AS [Κωδικός], ExemptionName AS [Άρθρο], Description AS [Περιγραφή] FROM VatExemptions ORDER BY ExemptionCode");
            dgvExemptions.DataSource = dt;
            if (dgvExemptions.Columns.Contains("Id")) dgvExemptions.Columns["Id"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("⚠️ Συμπλήρωσε κωδικό και όνομα!", "Προσοχή");
                return;
            }
            DatabaseHelper.ExecuteQuery(
                "INSERT INTO VatExemptions (ExemptionCode, ExemptionName, Description) VALUES (@Code, @Name, @Desc)",
                new SqlParameter("@Code", txtCode.Text.Trim()),
                new SqlParameter("@Name", txtName.Text.Trim()),
                new SqlParameter("@Desc", txtDescription.Text.Trim()));
            MessageBox.Show("✅ Το άρθρο προστέθηκε!");
            LoadVatExemptions();
            ClearFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvExemptions.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvExemptions.SelectedRows[0].Cells["Id"].Value);
            DatabaseHelper.ExecuteQuery(
                "UPDATE VatExemptions SET ExemptionCode=@Code, ExemptionName=@Name, Description=@Desc WHERE Id=@Id",
                new SqlParameter("@Id", id),
                new SqlParameter("@Code", txtCode.Text.Trim()),
                new SqlParameter("@Name", txtName.Text.Trim()),
                new SqlParameter("@Desc", txtDescription.Text.Trim()));
            MessageBox.Show("✅ Το άρθρο ενημερώθηκε!");
            LoadVatExemptions();
            ClearFields();
        }

        // ✅ Πραγματική διαγραφή
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExemptions.SelectedRows.Count == 0) return;
            if (MessageBox.Show("Θέλεις σίγουρα να ΔΙΑΓΡΑΨΕΙΣ οριστικά;",
                "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.No) return;

            int id = Convert.ToInt32(dgvExemptions.SelectedRows[0].Cells["Id"].Value);
            DatabaseHelper.ExecuteQuery("DELETE FROM VatExemptions WHERE Id=@Id", new SqlParameter("@Id", id));
            MessageBox.Show("✅ Το άρθρο διαγράφηκε οριστικά!");
            LoadVatExemptions();
            ClearFields();
        }

        private void dgvExemptions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvExemptions.Rows[e.RowIndex];
            txtCode.Text = row.Cells["Κωδικός"].Value.ToString();
            txtName.Text = row.Cells["Άρθρο"].Value.ToString();
            txtDescription.Text = row.Cells["Περιγραφή"].Value.ToString();
        }

        private void ClearFields() { txtCode.Clear(); txtName.Clear(); txtDescription.Clear(); }
        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}