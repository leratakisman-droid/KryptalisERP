using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class UnitsForm : Form
    {
        private readonly string _connStr;
        private bool _isLoading = false;

        public UnitsForm()
        {
            InitializeComponent();
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += UnitsForm_Load;
        }

        private void UnitsForm_Load(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _isLoading = true;
                CreateColumns();
                LoadUnits();
                _isLoading = false;
            }
        }

        private void CreateColumns()
        {
            dgvUnits.Columns.Clear();
            dgvUnits.AutoGenerateColumns = false;

            dgvUnits.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvUnits.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", Width = 100 });
            dgvUnits.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", Width = 200 });
            dgvUnits.Columns.Add(new DataGridViewTextBoxColumn { Name = "Σύμβολο", DataPropertyName = "Σύμβολο", Width = 100 });
            dgvUnits.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τύπος", DataPropertyName = "Τύπος", Width = 150 });
            dgvUnits.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ενεργό", DataPropertyName = "Ενεργό", Width = 70 });
        }

        private void LoadUnits()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                string whereClause = chkShowInactive.Checked ? "1 = 1" : "IsActive = 1";

                string query = $@"
                    SELECT 
                        Id,
                        ISNULL(Code, '') AS [Κωδικός],
                        ISNULL(Name, '') AS [Όνομα],
                        ISNULL(Symbol, '') AS [Σύμβολο],
                        ISNULL(UnitType, '') AS [Τύπος],
                        CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                    FROM UnitsOfMeasure
                    WHERE {whereClause}
                    ORDER BY Name";

                var results = conn.Query(query).ToList();

                dgvUnits.DataSource = null;
                dgvUnits.Columns.Clear();
                CreateColumns();
                dgvUnits.DataSource = results;

                UpdateStats();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void UpdateStats()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var stats = conn.QueryFirstOrDefault(@"
                    SELECT COUNT(*) AS Total,
                           SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) AS Active
                    FROM UnitsOfMeasure");
                if (stats != null)
                    lblSubtitle.Text = $"Συνολικές μονάδες: {stats.Total} | Ενεργές: {stats.Active}";
            }
            catch { }
        }

        private void chkShowInactive_CheckedChanged(object sender, EventArgs e) { if (!_isLoading) { _isLoading = true; LoadUnits(); _isLoading = false; } }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) btnSearch_Click(sender, e); }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                string searchText = txtSearch.Text.Trim();
                string whereClause = chkShowInactive.Checked ? "1 = 1" : "IsActive = 1";
                string searchFilter = !string.IsNullOrEmpty(searchText) ? " AND (Name LIKE @Search OR Code LIKE @Search)" : "";

                var query = $@"SELECT Id, ISNULL(Code, '') AS [Κωδικός], ISNULL(Name, '') AS [Όνομα], 
                              ISNULL(Symbol, '') AS [Σύμβολο], ISNULL(UnitType, '') AS [Τύπος],
                              CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                              FROM UnitsOfMeasure WHERE {whereClause}{searchFilter} ORDER BY Name";

                var results = conn.Query(query, new { Search = $"%{searchText}%" }).ToList();
                dgvUnits.DataSource = null; dgvUnits.Columns.Clear(); CreateColumns(); dgvUnits.DataSource = results;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnNew_Click(object sender, EventArgs e) { using var form = new UnitEditForm(null); if (form.ShowDialog() == DialogResult.OK) LoadUnits(); }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUnits.CurrentRow == null) { MessageBox.Show("Επίλεξε μονάδα!"); return; }
            var id = dgvUnits.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            using var form = new UnitEditForm(Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadUnits();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUnits.CurrentRow == null) { MessageBox.Show("Επίλεξε μονάδα!"); return; }
            var id = dgvUnits.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή μονάδας;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM UnitsOfMeasure WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); LoadUnits(); }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();
        private void dgvUnits_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) btnEdit_Click(sender, EventArgs.Empty); }
    }
}