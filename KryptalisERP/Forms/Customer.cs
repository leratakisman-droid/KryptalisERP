using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class CustomerForm : Form
    {
        private readonly string _connStr;
        private bool _isLoading = false;

        public CustomerForm()
        {
            InitializeComponent();
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += CustomerForm_Load;
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _isLoading = true;
                CreateColumns();
                LoadCustomers();
                _isLoading = false;
            }
        }

        private void CreateColumns()
        {
            dgvCustomers.Columns.Clear();
            dgvCustomers.AutoGenerateColumns = false;

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Επωνυμία", DataPropertyName = "Επωνυμία", Width = 120 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", Width = 100 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Επίθετο", DataPropertyName = "Επίθετο", Width = 100 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "ΑΦΜ", DataPropertyName = "ΑΦΜ", Width = 90 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "ΔΟΥ", DataPropertyName = "ΔΟΥ", Width = 100 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Επάγγελμα", DataPropertyName = "Επάγγελμα", Width = 120 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τηλέφωνο", DataPropertyName = "Τηλέφωνο", Width = 100 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κινητό", DataPropertyName = "Κινητό", Width = 100 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", DataPropertyName = "Email", Width = 130 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Πόλη", DataPropertyName = "Πόλη", Width = 90 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Περιοχή", DataPropertyName = "Περιοχή", Width = 100 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ενεργός", DataPropertyName = "Ενεργός", Width = 70 });
        }

        private void LoadCustomers()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                string whereClause = chkShowInactive.Checked ? "1 = 1" : "Energo = 1";

                string query = $@"
                    SELECT 
                        Id,
                        ISNULL(Epwnymia, '') AS [Επωνυμία],
                        ISNULL(Onoma, '') AS [Όνομα],
                        ISNULL(Epitheto, '') AS [Επίθετο],
                        ISNULL(Afm, '') AS [ΑΦΜ],
                        ISNULL(DOY, '') AS [ΔΟΥ],
                        ISNULL(Epaggelma, '') AS [Επάγγελμα],
                        ISNULL(Tilefono, '') AS [Τηλέφωνο],
                        ISNULL(Kinhto, '') AS [Κινητό],
                        ISNULL(Email, '') AS [Email],
                        ISNULL(Poli, '') AS [Πόλη],
                        ISNULL(Periochi, '') AS [Περιοχή],
                        CASE WHEN Energo = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργός]
                    FROM Customers
                    WHERE {whereClause}
                    ORDER BY Epwnymia";

                var results = conn.Query(query).ToList();

                // ✅ ΣΩΣΤΟΣ ΚΑΘΑΡΙΣΜΟΣ - ΧΩΡΙΣ Rows.Clear()
                dgvCustomers.DataSource = null;
                dgvCustomers.Columns.Clear();
                CreateColumns(); // Ξαναδημιουργία στηλών
                dgvCustomers.DataSource = results;

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
                        SUM(CASE WHEN Energo = 1 THEN 1 ELSE 0 END) AS Active,
                        SUM(CASE WHEN Energo = 0 THEN 1 ELSE 0 END) AS Inactive
                    FROM Customers");

                if (stats != null)
                    lblSubtitle.Text = $"Συνολικοί: {stats.Total} | Ενεργοί: {stats.Active} | Ανενεργοί: {stats.Inactive}";
            }
            catch { }
        }

        private void chkShowInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                _isLoading = true;
                LoadCustomers();
                _isLoading = false;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch_Click(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText)) { LoadCustomers(); return; }

            try
            {
                using var conn = new SqlConnection(_connStr);
                string whereClause = chkShowInactive.Checked ? "1 = 1" : "Energo = 1";

                var query = $@"
                    SELECT 
                        Id,
                        ISNULL(Epwnymia, '') AS [Επωνυμία],
                        ISNULL(Onoma, '') AS [Όνομα],
                        ISNULL(Epitheto, '') AS [Επίθετο],
                        ISNULL(Afm, '') AS [ΑΦΜ],
                        ISNULL(DOY, '') AS [ΔΟΥ],
                        ISNULL(Epaggelma, '') AS [Επάγγελμα],
                        ISNULL(Tilefono, '') AS [Τηλέφωνο],
                        ISNULL(Kinhto, '') AS [Κινητό],
                        ISNULL(Email, '') AS [Email],
                        ISNULL(Poli, '') AS [Πόλη],
                        ISNULL(Periochi, '') AS [Περιοχή],
                        CASE WHEN Energo = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργός]
                    FROM Customers
                    WHERE {whereClause}
                      AND (Epwnymia LIKE @Search OR Afm LIKE @Search OR Onoma LIKE @Search)
                    ORDER BY Epwnymia";

                var results = conn.Query(query, new { Search = $"%{searchText}%" }).ToList();

                // ✅ ΣΩΣΤΟΣ ΚΑΘΑΡΙΣΜΟΣ
                dgvCustomers.DataSource = null;
                dgvCustomers.Columns.Clear();
                CreateColumns();
                dgvCustomers.DataSource = results;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using var form = new CustomerEditForm(null);
            if (form.ShowDialog() == DialogResult.OK) LoadCustomers();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) { MessageBox.Show("Επίλεξε έναν πελάτη!", "Προσοχή"); return; }
            var id = dgvCustomers.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            using var form = new CustomerEditForm(Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadCustomers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) { MessageBox.Show("Επίλεξε έναν πελάτη!", "Προσοχή"); return; }
            var id = dgvCustomers.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            var name = dgvCustomers.CurrentRow.Cells["Επωνυμία"].Value?.ToString() ?? "";

            if (MessageBox.Show($"Διαγραφή πελάτη '{name}';", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    conn.Open();
                    using var tran = conn.BeginTransaction();

                    conn.Execute("DELETE FROM CustomerPriceZones WHERE CustomerId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM CustomerCreditLimits WHERE CustomerId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM CustomerActivities WHERE CustomerId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM CustomerContacts WHERE CustomerId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM CustomerBranches WHERE CustomerId = @Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM Customers WHERE Id = @Id", new { Id = id }, tran);

                    tran.Commit();
                    MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); }
            }
        }

        private void btnReports_Click(object sender, EventArgs e) { MessageBox.Show("Λειτουργία αναφορών υπό ανάπτυξη."); }
        private void btnExport_Click(object sender, EventArgs e) { MessageBox.Show("Λειτουργία εξαγωγής υπό ανάπτυξη."); }
        private void btnBack_Click(object sender, EventArgs e) => this.Close();

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, EventArgs.Empty);
        }
    }
}