using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class WarehousesForm : Form
    {
        private readonly string _connStr;
        private bool _isLoading = false;

        public WarehousesForm()
        {
            InitializeComponent();
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += WarehousesForm_Load;
        }

        private void WarehousesForm_Load(object sender, EventArgs e)
        {
            if (!_isLoading) { _isLoading = true; CreateColumns(); LoadWarehouses(); _isLoading = false; }
        }

        private void CreateColumns()
        {
            dgvWarehouses.Columns.Clear();
            dgvWarehouses.AutoGenerateColumns = false;
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", Width = 100 });
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", Width = 250 });
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τοποθεσία", DataPropertyName = "Τοποθεσία", Width = 200 });
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { Name = "Υπεύθυνος", DataPropertyName = "Υπεύθυνος", Width = 150 });
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τηλέφωνο", DataPropertyName = "Τηλέφωνο", Width = 120 });
            dgvWarehouses.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ενεργό", DataPropertyName = "Ενεργό", Width = 70 });
        }

        private void LoadWarehouses()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                string whereClause = chkShowInactive.Checked ? "1 = 1" : "IsActive = 1";
                string query = $@"SELECT Id, ISNULL(Code, '') AS [Κωδικός], ISNULL(Name, '') AS [Όνομα], 
                    ISNULL(Location, '') AS [Τοποθεσία], ISNULL(Manager, '') AS [Υπεύθυνος], 
                    ISNULL(Phone, '') AS [Τηλέφωνο], CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                    FROM Warehouses WHERE {whereClause} ORDER BY Name";

                var results = conn.Query(query).ToList();
                dgvWarehouses.DataSource = null; dgvWarehouses.Columns.Clear(); CreateColumns(); dgvWarehouses.DataSource = results;
                UpdateStats();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void UpdateStats()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var stats = conn.QueryFirstOrDefault("SELECT COUNT(*) AS Total, SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) AS Active FROM Warehouses");
                if (stats != null) lblSubtitle.Text = $"Συνολικές αποθήκες: {stats.Total} | Ενεργές: {stats.Active}";
            }
            catch { }
        }

        private void chkShowInactive_CheckedChanged(object sender, EventArgs e) { if (!_isLoading) { _isLoading = true; LoadWarehouses(); _isLoading = false; } }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) btnSearch_Click(sender, e); }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                string searchText = txtSearch.Text.Trim();
                string whereClause = chkShowInactive.Checked ? "1 = 1" : "IsActive = 1";
                string searchFilter = !string.IsNullOrEmpty(searchText) ? " AND (Name LIKE @Search OR Code LIKE @Search OR Location LIKE @Search)" : "";
                var query = $@"SELECT Id, ISNULL(Code, '') AS [Κωδικός], ISNULL(Name, '') AS [Όνομα], ISNULL(Location, '') AS [Τοποθεσία], 
                    ISNULL(Manager, '') AS [Υπεύθυνος], ISNULL(Phone, '') AS [Τηλέφωνο], CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                    FROM Warehouses WHERE {whereClause}{searchFilter} ORDER BY Name";
                var results = conn.Query(query, new { Search = $"%{searchText}%" }).ToList();
                dgvWarehouses.DataSource = null; dgvWarehouses.Columns.Clear(); CreateColumns(); dgvWarehouses.DataSource = results;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnNew_Click(object sender, EventArgs e) { using var form = new WarehouseEditForm(null); if (form.ShowDialog() == DialogResult.OK) LoadWarehouses(); }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvWarehouses.CurrentRow == null) { MessageBox.Show("Επίλεξε αποθήκη!"); return; }
            var id = dgvWarehouses.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            using var form = new WarehouseEditForm(Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadWarehouses();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvWarehouses.CurrentRow == null) { MessageBox.Show("Επίλεξε αποθήκη!"); return; }
            var id = dgvWarehouses.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή αποθήκης;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM Warehouses WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); LoadWarehouses(); }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();
        private void dgvWarehouses_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) btnEdit_Click(sender, EventArgs.Empty); }
    }
}