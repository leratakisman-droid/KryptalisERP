using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemColorsForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemColorsForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemColorsForm_Load;
        }

        private void ItemColorsForm_Load(object sender, EventArgs e) { LoadColors(); }

        private void LoadColors()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                dgvColors.DataSource = conn.Query(@"
                    SELECT Id, ColorCode AS [Κωδικός], ColorName AS [Όνομα], 
                           ColorHex AS [Hex], 
                           CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                    FROM ItemColors WHERE IsActive = 1 ORDER BY ColorName",
                    new { }).ToList();
                if (dgvColors.Columns.Contains("Id")) dgvColors.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtColorName.Text)) { MessageBox.Show("Όνομα υποχρεωτικό!"); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute(@"INSERT INTO ItemColors (ColorCode, ColorName, ColorHex, IsActive, CreatedAt) 
                    VALUES (@Code, @Name, @Hex, 1, GETDATE())", new
                {
                    Code = txtColorCode.Text.Trim(),
                    Name = txtColorName.Text.Trim(),
                    Hex = txtColorHex.Text.Trim()
                });
                MessageBox.Show("✅ Προστέθηκε!");
                LoadColors();
                txtColorCode.Clear(); txtColorName.Clear(); txtColorHex.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvColors.CurrentRow == null) { MessageBox.Show("Επίλεξε!"); return; }
            var id = dgvColors.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute("DELETE FROM ItemColors WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                LoadColors();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}