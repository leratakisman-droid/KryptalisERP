using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemCategoriesForm : Form
    {
        private readonly string _connStr;
        private bool _isLoading = false;

        public ItemCategoriesForm()
        {
            InitializeComponent();
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemCategoriesForm_Load;
        }

        private void ItemCategoriesForm_Load(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _isLoading = true;
                CreateColumns();
                LoadCategories();
                _isLoading = false;
            }
        }

        private void CreateColumns()
        {
            dgvCategories.Columns.Clear();
            dgvCategories.AutoGenerateColumns = false;

            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", Width = 100 });
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", Width = 250 });
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "Περιγραφή", DataPropertyName = "Περιγραφή", Width = 300 });
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ενεργό", DataPropertyName = "Ενεργό", Width = 70 });
        }

        private void LoadCategories()
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
                        ISNULL(Description, '') AS [Περιγραφή],
                        CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                    FROM ItemCategories
                    WHERE {whereClause}
                    ORDER BY Name";

                var results = conn.Query(query).ToList();

                dgvCategories.DataSource = null;
                dgvCategories.Columns.Clear();
                CreateColumns();
                dgvCategories.DataSource = results;

                UpdateStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStats()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var stats = conn.QueryFirstOrDefault(@"
                    SELECT 
                        COUNT(*) AS Total,
                        SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) AS Active,
                        SUM(CASE WHEN IsActive = 0 OR IsActive IS NULL THEN 1 ELSE 0 END) AS Inactive
                    FROM ItemCategories");

                if (stats != null)
                    lblSubtitle.Text = $"Συνολικές κατηγορίες: {stats.Total} | Ενεργές: {stats.Active} | Ανενεργές: {stats.Inactive}";
            }
            catch { }
        }

        private void chkShowInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _isLoading = true;
                LoadCategories();
                _isLoading = false;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch_Click(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                string searchText = txtSearch.Text.Trim();
                string whereClause = chkShowInactive.Checked ? "1 = 1" : "IsActive = 1";
                string searchFilter = "";

                if (!string.IsNullOrEmpty(searchText))
                    searchFilter = " AND (Name LIKE @Search OR Code LIKE @Search OR Description LIKE @Search)";

                var query = $@"
                    SELECT Id, ISNULL(Code, '') AS [Κωδικός], ISNULL(Name, '') AS [Όνομα], 
                           ISNULL(Description, '') AS [Περιγραφή],
                           CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                    FROM ItemCategories
                    WHERE {whereClause}{searchFilter}
                    ORDER BY Name";

                var results = conn.Query(query, new { Search = $"%{searchText}%" }).ToList();

                dgvCategories.DataSource = null;
                dgvCategories.Columns.Clear();
                CreateColumns();
                dgvCategories.DataSource = results;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using var form = new ItemCategoryEditForm(null);
            if (form.ShowDialog() == DialogResult.OK) LoadCategories();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null) { MessageBox.Show("Επίλεξε κατηγορία!"); return; }
            var id = dgvCategories.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            using var form = new ItemCategoryEditForm(Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadCategories();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null) { MessageBox.Show("Επίλεξε κατηγορία!"); return; }
            var id = dgvCategories.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            var name = dgvCategories.CurrentRow.Cells["Όνομα"].Value?.ToString() ?? "";

            if (MessageBox.Show($"Διαγραφή κατηγορίας '{name}';", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    conn.Execute("DELETE FROM ItemCategories WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία");
                    LoadCategories();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();

        private void dgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, EventArgs.Empty);
        }
    }
}