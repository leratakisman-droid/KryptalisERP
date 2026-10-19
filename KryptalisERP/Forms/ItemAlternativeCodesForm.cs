using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemAlternativeCodesForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemAlternativeCodesForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemAlternativeCodesForm_Load;
        }

        private void ItemAlternativeCodesForm_Load(object sender, EventArgs e) { LoadCodes(); }

        private void LoadCodes()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                dgvCodes.DataSource = conn.Query(@"
                    SELECT Id, AltCode AS [Εναλλακτικός Κωδικός], 
                           AltDescription AS [Περιγραφή],
                           CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                    FROM ItemAlternativeCodes WHERE ItemId = @Id",
                    new { Id = _itemId }).ToList();
                if (dgvCodes.Columns.Contains("Id")) dgvCodes.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAltCode.Text)) { MessageBox.Show("Κωδικός υποχρεωτικός!"); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute(@"INSERT INTO ItemAlternativeCodes (ItemId, AltCode, AltDescription, IsActive, CreatedAt) 
                    VALUES (@ItemId, @Code, @Desc, 1, GETDATE())", new
                {
                    ItemId = _itemId,
                    Code = txtAltCode.Text.Trim(),
                    Desc = txtAltDescription.Text.Trim()
                });
                MessageBox.Show("✅ Προστέθηκε!");
                LoadCodes();
                txtAltCode.Clear(); txtAltDescription.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCodes.CurrentRow == null) { MessageBox.Show("Επίλεξε!"); return; }
            var id = dgvCodes.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute("DELETE FROM ItemAlternativeCodes WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                LoadCodes();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}