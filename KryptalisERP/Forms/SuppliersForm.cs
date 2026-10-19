using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class SuppliersForm : Form
    {
        private readonly string _connStr;
        private bool _isLoading = false;
        private int? _initialSupplierId;

        public SuppliersForm() : this(null) { }
        public SuppliersForm(int? supplierId = null)
        {
            InitializeComponent();
            _initialSupplierId = supplierId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += SuppliersForm_Load;
        }

        private void SuppliersForm_Load(object sender, EventArgs e)
        {
            if (!_isLoading) { _isLoading = true; AddCardButton(); CreateColumns(); LoadSuppliers(); SelectInitialSupplier(); _isLoading = false; }
        }

        private void AddCardButton()
        {
            if (this.Controls.Find("btnCard", true).Length > 0) return;
            var btnCard = new Button { Name = "btnCard", Text = "📇 Καρτέλα", BackColor = Color.FromArgb(155, 89, 182), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            if (btnEdit != null) { btnCard.Location = new Point(btnEdit.Right + 10, btnEdit.Top); btnCard.Size = btnEdit.Size; btnEdit.Parent?.Controls.Add(btnCard); btnCard.BringToFront(); }
            else { btnCard.Location = new Point(200, 10); btnCard.Size = new Size(130, 40); this.Controls.Add(btnCard); }
            btnCard.Click += (s, e) => OpenSupplierCard();
        }

        private void SelectInitialSupplier()
        {
            if (!_initialSupplierId.HasValue) return;
            foreach (DataGridViewRow row in dgvSuppliers.Rows) if (row.Cells["Id"].Value != null && Convert.ToInt32(row.Cells["Id"].Value) == _initialSupplierId.Value) { row.Selected = true; dgvSuppliers.CurrentCell = row.Cells[0]; break; }
        }

        private void CreateColumns()
        {
            dgvSuppliers.Columns.Clear();
            dgvSuppliers.AutoGenerateColumns = false;
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Επωνυμία", DataPropertyName = "Επωνυμία", Width = 200 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Υπεύθυνος", DataPropertyName = "Υπεύθυνος", Width = 120 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "ΑΦΜ", DataPropertyName = "ΑΦΜ", Width = 100 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "ΔΟΥ", DataPropertyName = "ΔΟΥ", Width = 100 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τηλέφωνο", DataPropertyName = "Τηλέφωνο", Width = 110 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", DataPropertyName = "Email", Width = 180 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Διεύθυνση", DataPropertyName = "Διεύθυνση", Width = 200 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Πόλη", DataPropertyName = "Πόλη", Width = 100 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Περιοχή", DataPropertyName = "Περιοχή", Width = 100 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Υπόλοιπο", DataPropertyName = "Υπόλοιπο", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) } });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ενεργός", DataPropertyName = "Ενεργός", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvSuppliers.CellFormatting += DgvSuppliers_CellFormatting;
        }

        private void DgvSuppliers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvSuppliers.Rows[e.RowIndex];
            if (row.Cells["Ενεργός"].Value?.ToString() == "Όχι") { row.DefaultCellStyle.ForeColor = Color.Gray; row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); }
            if (e.ColumnIndex == dgvSuppliers.Columns["Υπόλοιπο"].Index) { decimal b = 0; decimal.TryParse(e.Value?.ToString(), out b); e.CellStyle.ForeColor = b > 0 ? Color.FromArgb(52, 152, 219) : (b < 0 ? Color.FromArgb(46, 204, 113) : Color.Black); e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); }
        }

        private void LoadSuppliers()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                string where = chkShowInactive.Checked ? "1=1" : "s.IsActive = 1";
                // ✅ ΣΩΣΤΟ: Payments.TotalAmount + PaymentAllocations.AllocatedAmount
                string sql = $@"
                    SELECT s.Id, ISNULL(s.SupplierName,'') AS [Επωνυμία], ISNULL(s.ContactPerson,'') AS [Υπεύθυνος], ISNULL(s.Afm,'') AS [ΑΦΜ],
                        ISNULL(s.TaxOffice,'') AS [ΔΟΥ], ISNULL(s.Phone,'') AS [Τηλέφωνο], ISNULL(s.Email,'') AS [Email],
                        ISNULL(s.Address,'') AS [Διεύθυνση], ISNULL(s.City,'') AS [Πόλη], ISNULL(s.Area,'') AS [Περιοχή],
                        (ISNULL((SELECT SUM(ISNULL(FinalAmount,GrandTotal)) FROM PurchaseInvoices WHERE SupplierId=s.Id AND IsActive=1),0) 
                         - ISNULL((SELECT SUM(TotalAmount) FROM Payments WHERE SupplierId=s.Id AND Status<>'Cancelled'),0)) AS [Υπόλοιπο],
                        CASE WHEN s.IsActive=1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργός]
                    FROM Suppliers s WHERE {where} ORDER BY s.SupplierName";
                var results = conn.Query(sql).ToList();
                dgvSuppliers.Rows.Clear();
                dgvSuppliers.DataSource = results;
                UpdateStats(conn);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void UpdateStats(SqlConnection conn)
        {
            try
            {
                var s = conn.QueryFirstOrDefault("SELECT COUNT(*) AS Total, SUM(CASE WHEN IsActive=1 THEN 1 ELSE 0 END) AS Active, SUM(CASE WHEN IsActive=0 THEN 1 ELSE 0 END) AS Inactive FROM Suppliers");
                if (s != null) lblSubtitle.Text = $"Συνολικοί: {s.Total} | Ενεργοί: {s.Active} | Ανενεργοί: {s.Inactive}";
            }
            catch { }
        }

        private void chkShowInactive_CheckedChanged(object sender, EventArgs e) { if (!_isLoading) { _isLoading = true; LoadSuppliers(); _isLoading = false; } }

        private void btnNew_Click(object sender, EventArgs e) { using var f = new SupplierEditForm(0); if (f.ShowDialog() == DialogResult.OK) LoadSuppliers(); }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null) { MessageBox.Show("Επίλεξε προμηθευτή!"); return; }
            var id = dgvSuppliers.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return;
            using var f = new SupplierEditForm(Convert.ToInt32(id)); if (f.ShowDialog() == DialogResult.OK) LoadSuppliers();
        }

        private void OpenSupplierCard()
        {
            if (dgvSuppliers.CurrentRow == null) { MessageBox.Show("Επίλεξε προμηθευτή!"); return; }
            var id = dgvSuppliers.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return;
            try { using (var f = new SupplierCardForm(Convert.ToInt32(id))) f.ShowDialog(); LoadSuppliers(); }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null) { MessageBox.Show("Επίλεξε προμηθευτή!"); return; }
            var id = dgvSuppliers.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return;
            var name = dgvSuppliers.CurrentRow.Cells["Επωνυμία"].Value?.ToString() ?? "";
            if (MessageBox.Show($"Διαγραφή '{name}';", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    conn.Open(); using var tran = conn.BeginTransaction();
                    conn.Execute("DELETE FROM SupplierPriceLists WHERE SupplierId=@Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM SupplierItems WHERE SupplierId=@Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM SupplierPaymentTerms WHERE SupplierId=@Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM SupplierBankAccounts WHERE SupplierId=@Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM SupplierContacts WHERE SupplierId=@Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM SupplierBranches WHERE SupplierId=@Id", new { Id = id }, tran);
                    conn.Execute("DELETE FROM Suppliers WHERE Id=@Id", new { Id = id }, tran);
                    tran.Commit();
                    MessageBox.Show("✅ Διαγράφηκε!");
                    LoadSuppliers();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();
        private void dgvSuppliers_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) btnEdit_Click(sender, EventArgs.Empty); }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string s = txtSearch.Text.Trim(); if (string.IsNullOrEmpty(s)) { LoadSuppliers(); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                string where = chkShowInactive.Checked ? "1=1" : "s.IsActive=1";
                var sql = $@"
                    SELECT s.Id, ISNULL(s.SupplierName,'') AS [Επωνυμία], ISNULL(s.ContactPerson,'') AS [Υπεύθυνος], ISNULL(s.Afm,'') AS [ΑΦΜ],
                        ISNULL(s.TaxOffice,'') AS [ΔΟΥ], ISNULL(s.Phone,'') AS [Τηλέφωνο], ISNULL(s.Email,'') AS [Email],
                        ISNULL(s.Address,'') AS [Διεύθυνση], ISNULL(s.City,'') AS [Πόλη], ISNULL(s.Area,'') AS [Περιοχή],
                        (ISNULL((SELECT SUM(ISNULL(FinalAmount,GrandTotal)) FROM PurchaseInvoices WHERE SupplierId=s.Id AND IsActive=1),0) 
                         - ISNULL((SELECT SUM(TotalAmount) FROM Payments WHERE SupplierId=s.Id AND Status<>'Cancelled'),0)) AS [Υπόλοιπο],
                        CASE WHEN s.IsActive=1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργός]
                    FROM Suppliers s WHERE {where} AND (s.SupplierName LIKE @S OR s.Afm LIKE @S OR s.ContactPerson LIKE @S OR s.Email LIKE @S)
                    ORDER BY s.SupplierName";
                var r = conn.Query(sql, new { S = $"%{s}%" }).ToList();
                dgvSuppliers.Rows.Clear(); dgvSuppliers.DataSource = r;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }
    }
}