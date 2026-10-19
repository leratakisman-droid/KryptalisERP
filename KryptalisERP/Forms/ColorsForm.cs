using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KryptalisERP
{
    public partial class ColorsForm : Form
    {
        public ColorsForm()
        {
            InitializeComponent();
            LoadColors();
        }

        private void LoadColors()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, ColorCode AS [Κωδικός], ColorName AS [Χρώμα], ColorHex AS [Hex] FROM ItemColors ORDER BY ColorName");
                dgvColors.DataSource = dt;
                if (dgvColors.Columns.Contains("Id")) dgvColors.Columns["Id"].Visible = false;

                // Χρωματισμός γραμμών με βάση το Hex
                foreach (DataGridViewRow row in dgvColors.Rows)
                {
                    string hex = row.Cells["Hex"].Value?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(hex) && hex.StartsWith("#"))
                    {
                        try
                        {
                            row.Cells["Χρώμα"].Style.BackColor = ColorTranslator.FromHtml(hex);
                            row.Cells["Χρώμα"].Style.ForeColor = IsLightColor(ColorTranslator.FromHtml(hex)) ? Color.Black : Color.White;
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message);
            }
        }

        private bool IsLightColor(Color color)
        {
            int brightness = (color.R * 299 + color.G * 587 + color.B * 114) / 1000;
            return brightness > 128;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("⚠️ Συμπλήρωσε όνομα χρώματος!", "Προσοχή");
                return;
            }
            try
            {
                DatabaseHelper.ExecuteQuery(
                    "INSERT INTO ItemColors (ColorCode, ColorName, ColorHex, IsActive) VALUES (@Code, @Name, @Hex, 1)",
                    new SqlParameter("@Code", txtCode.Text.Trim()),
                    new SqlParameter("@Name", txtName.Text.Trim()),
                    new SqlParameter("@Hex", txtHex.Text.Trim()));
                MessageBox.Show("✅ Το χρώμα προστέθηκε!");
                LoadColors();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvColors.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvColors.SelectedRows[0].Cells["Id"].Value);
            try
            {
                DatabaseHelper.ExecuteQuery(
                    "UPDATE ItemColors SET ColorCode=@Code, ColorName=@Name, ColorHex=@Hex WHERE Id=@Id",
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Code", txtCode.Text.Trim()),
                    new SqlParameter("@Name", txtName.Text.Trim()),
                    new SqlParameter("@Hex", txtHex.Text.Trim()));
                MessageBox.Show("✅ Το χρώμα ενημερώθηκε!");
                LoadColors();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvColors.SelectedRows.Count == 0) return;
            if (MessageBox.Show("Θέλεις σίγουρα να ΑΠΕΝΕΡΓΟΠΟΙΗΣΕΙΣ το χρώμα;\n(Θα παραμείνει στη βάση αλλά δεν θα εμφανίζεται στα είδη)",
                "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.No) return;

            int id = Convert.ToInt32(dgvColors.SelectedRows[0].Cells["Id"].Value);
            try
            {
                DatabaseHelper.ExecuteQuery(
                    "UPDATE ItemColors SET IsActive = 0 WHERE Id = @Id",
                    new SqlParameter("@Id", id));
                MessageBox.Show("✅ Το χρώμα απενεργοποιήθηκε!");
                LoadColors();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void dgvColors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvColors.Rows[e.RowIndex];
            txtCode.Text = row.Cells["Κωδικός"].Value?.ToString() ?? "";
            txtName.Text = row.Cells["Χρώμα"].Value?.ToString() ?? "";
            txtHex.Text = row.Cells["Hex"].Value?.ToString() ?? "";
        }

        private void ClearFields()
        {
            txtCode.Clear();
            txtName.Clear();
            txtHex.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}