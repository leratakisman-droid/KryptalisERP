using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemsForm : Form
    {
        private readonly string _connStr;
        private bool _isLoading = false;

        public ItemsForm()
        {
            InitializeComponent();
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemsForm_Load;
        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _isLoading = true;
                CreateColumns();
                LoadItems();
                _isLoading = false;
            }
        }

        private void CreateColumns()
        {
            dgvItems.Columns.Clear();
            dgvItems.AutoGenerateColumns = false;

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", Width = 200 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κατηγορία", DataPropertyName = "Κατηγορία", Width = 120 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Μονάδα", DataPropertyName = "Μονάδα", Width = 80 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τιμή Αγοράς", DataPropertyName = "Τιμή Αγοράς", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τιμή Πώλησης", DataPropertyName = "Τιμή Πώλησης", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ΦΠΑ %", DataPropertyName = "ΦΠΑ %", Width = 70 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Stock", DataPropertyName = "Stock", Width = 80 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Barcode", DataPropertyName = "Barcode", Width = 120 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ενεργό", DataPropertyName = "Ενεργό", Width = 70 });
        }

        private void LoadItems()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                string whereClause = chkShowInactive.Checked ? "1 = 1" : "IsActive = 1";
                string searchText = txtSearch.Text.Trim();

                string searchFilter = "";
                if (!string.IsNullOrEmpty(searchText))
                {
                    searchFilter = " AND (Code LIKE @Search OR Name LIKE @Search OR ItemCode LIKE @Search OR Description LIKE @Search OR Barcode LIKE @Search)";
                }

                string query = $@"
                    SELECT TOP 500
                        Id,
                        ISNULL(Code, ISNULL(ItemCode, '')) AS [Κωδικός],
                        ISNULL(Name, ISNULL(ItemName, ISNULL(Description, ''))) AS [Όνομα],
                        ISNULL(Category, '') AS [Κατηγορία],
                        ISNULL(Unit, ISNULL(PrimaryUnit, '')) AS [Μονάδα],
                        ISNULL(PurchasePrice, 0) AS [Τιμή Αγοράς],
                        ISNULL(SalePrice, ISNULL(RetailPrice, 0)) AS [Τιμή Πώλησης],
                        ISNULL(VatRate, 0) AS [ΦΠΑ %],
                        ISNULL(CurrentStock, ISNULL(Stock, 0)) AS [Stock],
                        ISNULL(Barcode, '') AS [Barcode],
                        CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                    FROM Items
                    WHERE {whereClause}{searchFilter}
                    ORDER BY Name";

                var results = conn.Query(query, new { Search = $"%{searchText}%" }).ToList();

                dgvItems.DataSource = null;
                dgvItems.Columns.Clear();
                CreateColumns();
                dgvItems.DataSource = results;

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
                    FROM Items");

                if (stats != null)
                    lblSubtitle.Text = $"Συνολικά είδη: {stats.Total} | Ενεργά: {stats.Active} | Ανενεργά: {stats.Inactive}";
            }
            catch { }
        }

        private void chkShowInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _isLoading = true;
                LoadItems();
                _isLoading = false;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch_Click(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using var form = new ItemEditForm(null);
            if (form.ShowDialog() == DialogResult.OK) LoadItems();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null) { MessageBox.Show("Επίλεξε ένα είδος!", "Προσοχή"); return; }
            var id = dgvItems.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            using var form = new ItemEditForm(Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadItems();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null) { MessageBox.Show("Επίλεξε ένα είδος!", "Προσοχή"); return; }
            var id = dgvItems.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            var name = dgvItems.CurrentRow.Cells["Όνομα"].Value?.ToString() ?? "";

            if (MessageBox.Show($"Διαγραφή είδους '{name}';", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    conn.Open();
                    using var tran = conn.BeginTransaction();

                    conn.Execute("DELETE FROM ItemVariantBarcodes WHERE VariantId IN (SELECT Id FROM ItemVariants WHERE ItemId = @Id)", new { Id = id }, tran);
                    conn.Execute("DELETE FROM ItemVariants WHERE ItemId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM ItemBarcodes WHERE ItemId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM ItemAttributes WHERE ItemId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM ItemPriceHistory WHERE ItemId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM ItemRelated WHERE ItemId = @Id OR RelatedItemId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM ItemAlternativeCodes WHERE ItemId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM ItemCPVCodes WHERE ItemId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM Items WHERE Id = @Id", new { Id = id }, tran);

                    tran.Commit();
                    MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadItems();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); }
            }
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, EventArgs.Empty);
        }
    }
}