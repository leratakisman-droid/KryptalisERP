using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KryptalisERP
{
    public partial class SizesForm : Form
    {
        public SizesForm()
        {
            InitializeComponent();
            LoadSizes();
        }

        private void LoadSizes()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, SizeCode AS [Κωδικός], SizeName AS [Μέγεθος], SortOrder AS [Σειρά] FROM ItemSizes ORDER BY SortOrder, SizeName");
                dgvSizes.DataSource = dt;
                if (dgvSizes.Columns.Contains("Id")) dgvSizes.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("️ Συμπλήρωσε όνομα μεγέθους!", "Προσοχή");
                return;
            }
            try
            {
                int order = int.TryParse(txtOrder.Text, out int o) ? o : 0;
                DatabaseHelper.ExecuteQuery(
                    "INSERT INTO ItemSizes (SizeCode, SizeName, SortOrder, IsActive) VALUES (@Code, @Name, @Order, 1)",
                    new SqlParameter("@Code", txtCode.Text.Trim()),
                    new SqlParameter("@Name", txtName.Text.Trim()),
                    new SqlParameter("@Order", order));
                MessageBox.Show("✅ Το μέγεθος προστέθηκε!");
                LoadSizes();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSizes.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvSizes.SelectedRows[0].Cells["Id"].Value);
            try
            {
                int order = int.TryParse(txtOrder.Text, out int o) ? o : 0;
                DatabaseHelper.ExecuteQuery(
                    "UPDATE ItemSizes SET SizeCode=@Code, SizeName=@Name, SortOrder=@Order WHERE Id=@Id",
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Code", txtCode.Text.Trim()),
                    new SqlParameter("@Name", txtName.Text.Trim()),
                    new SqlParameter("@Order", order));
                MessageBox.Show("✅ Το μέγεθος ενημερώθηκε!");
                LoadSizes();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSizes.SelectedRows.Count == 0) return;
            if (MessageBox.Show("Θέλεις σίγουρα να ΑΠΕΝΕΡΓΟΠΟΙΗΣΕΙΣ το μέγεθος;\n(Θα παραμείνει στη βάση αλλά δεν θα εμφανίζεται στα είδη)",
                "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.No) return;

            int id = Convert.ToInt32(dgvSizes.SelectedRows[0].Cells["Id"].Value);
            try
            {
                DatabaseHelper.ExecuteQuery(
                    "UPDATE ItemSizes SET IsActive = 0 WHERE Id = @Id",
                    new SqlParameter("@Id", id));
                MessageBox.Show("✅ Το μέγεθος απενεργοποιήθηκε!");
                LoadSizes();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void dgvSizes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvSizes.Rows[e.RowIndex];
            txtCode.Text = row.Cells["Κωδικός"].Value?.ToString() ?? "";
            txtName.Text = row.Cells["Μέγεθος"].Value?.ToString() ?? "";
            txtOrder.Text = row.Cells["Σειρά"].Value?.ToString() ?? "0";
        }

        private void ClearFields()
        {
            txtCode.Clear();
            txtName.Clear();
            txtOrder.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}