using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class PurchaseOrderListForm : Form
    {
        private DataGridView dgvOrders;
        private ComboBox cmbStatus, cmbSupplier;
        private DateTimePicker dtpFrom, dtpTo;
        private TextBox txtSearch;
        private Button btnNew, btnEdit, btnReceipt, btnInvoice, btnDelete, btnRefresh, btnClose;
        private Label lblTotal, lblNew, lblConfirmed, lblReceived;

        public PurchaseOrderListForm() { InitializeUI(); this.Load += (s, e) => { LoadFilters(); LoadOrders(); }; }

        private void InitializeUI()
        {
            this.Text = "🏭 Λίστα Παραγγελιών Προμηθευτών";
            this.Size = new Size(1500, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            var pnlToolbar = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.White, Padding = new Padding(15, 8, 15, 8) };
            btnNew = CreateButton("➕ Νέα Παραγγελία", 10, 8, 160, 38, Color.FromArgb(46, 204, 113)); btnNew.Click += (s, e) => OpenOrder(null); pnlToolbar.Controls.Add(btnNew);
            btnEdit = CreateButton("✏️ Επεξεργασία", 180, 8, 130, 38, Color.FromArgb(52, 152, 219)); btnEdit.Click += (s, e) => EditSelected(); pnlToolbar.Controls.Add(btnEdit);
            btnReceipt = CreateButton("📦 → Παραλαβή", 320, 8, 140, 38, Color.FromArgb(230, 126, 34)); btnReceipt.Click += (s, e) => ConvertToReceipt(); pnlToolbar.Controls.Add(btnReceipt);
            btnInvoice = CreateButton("🛒 → Τιμολόγιο", 470, 8, 150, 38, Color.FromArgb(155, 89, 182)); btnInvoice.Click += (s, e) => ConvertToInvoice(); pnlToolbar.Controls.Add(btnInvoice);
            btnDelete = CreateButton("🗑️ Ακύρωση", 630, 8, 120, 38, Color.FromArgb(231, 76, 60)); btnDelete.Click += (s, e) => CancelSelected(); pnlToolbar.Controls.Add(btnDelete);
            btnRefresh = CreateButton("🔄 Ανανέωση", 760, 8, 120, 38, Color.FromArgb(108, 117, 125)); btnRefresh.Click += (s, e) => LoadOrders(); pnlToolbar.Controls.Add(btnRefresh);
            btnClose = CreateButton("❌ Κλείσιμο", 890, 8, 110, 38, Color.FromArgb(149, 165, 166)); btnClose.Click += (s, e) => this.Close(); pnlToolbar.Controls.Add(btnClose);
            this.Controls.Add(pnlToolbar);

            var pnlFilters = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.White, Padding = new Padding(15, 8, 15, 8) };
            int lx = 10;
            pnlFilters.Controls.Add(new Label { Text = "Κατάσταση:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 80;
            cmbStatus = new ComboBox { Location = new Point(lx, 8), Size = new Size(170, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) }; cmbStatus.SelectedIndexChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(cmbStatus); lx += 180;
            pnlFilters.Controls.Add(new Label { Text = "Προμηθευτής:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 85;
            cmbSupplier = new ComboBox { Location = new Point(lx, 8), Size = new Size(200, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) }; cmbSupplier.SelectedIndexChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(cmbSupplier); lx += 210;
            pnlFilters.Controls.Add(new Label { Text = "Από:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpFrom = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(115, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) }; dtpFrom.Value = DateTime.Now.AddMonths(-6); dtpFrom.ValueChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(dtpFrom); lx += 125;
            pnlFilters.Controls.Add(new Label { Text = "Έως:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpTo = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(115, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) }; dtpTo.ValueChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(dtpTo); lx += 125;
            txtSearch = new TextBox { Location = new Point(lx, 8), Size = new Size(200, 26), Font = new Font("Segoe UI", 9.5F) }; txtSearch.TextChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(txtSearch);
            this.Controls.Add(pnlFilters);

            dgvOrders = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false, ReadOnly = true, RowHeadersVisible = false, BackgroundColor = Color.White, SelectionMode = DataGridViewSelectionMode.FullRowSelect, Font = new Font("Segoe UI", 10F), RowTemplate = { Height = 32 } };
            dgvOrders.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(230, 126, 34), ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleCenter };
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderNumber", HeaderText = "Αριθμός", Width = 140 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderDate", HeaderText = "Ημερομηνία", Width = 110 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "ExpectedDeliveryDate", HeaderText = "Προβλ. Παραλαβή", Width = 130 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "SupplierName", HeaderText = "Προμηθευτής", Width = 250 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalNet", HeaderText = "Καθαρή €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Σύνολο €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "LinesCount", HeaderText = "Γραμμές", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReceivedPct", HeaderText = "% Παραλαβή", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusDisplay", HeaderText = "Κατάσταση", Width = 160, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });
            dgvOrders.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSelected(); };
            dgvOrders.CellFormatting += DgvOrders_CellFormatting;
            this.Controls.Add(dgvOrders);

            var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 40, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 8, 15, 8) };
            lblTotal = new Label { Text = "Σύνολο: 0", Location = new Point(15, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) }; pnlStatus.Controls.Add(lblTotal);
            lblNew = new Label { Text = "🆕 Νέες: 0", Location = new Point(150, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(108, 117, 125) }; pnlStatus.Controls.Add(lblNew);
            lblConfirmed = new Label { Text = "✅ Επιβεβαιωμένες: 0", Location = new Point(300, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(25, 135, 84) }; pnlStatus.Controls.Add(lblConfirmed);
            lblReceived = new Label { Text = "📥 Παραληφθείσες: 0", Location = new Point(520, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(230, 126, 34) }; pnlStatus.Controls.Add(lblReceived);
            this.Controls.Add(pnlStatus);
        }

        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvOrders.Rows[e.RowIndex];
            if (e.ColumnIndex == dgvOrders.Columns["StatusDisplay"].Index)
            {
                string status = row.Cells["StatusDisplay"].Value?.ToString() ?? "";
                if (status.Contains("Νέα")) e.CellStyle.ForeColor = Color.FromArgb(108, 117, 125);
                else if (status.Contains("Επιβεβαιωμένη")) e.CellStyle.ForeColor = Color.FromArgb(25, 135, 84);
                else if (status.Contains("Μερική")) e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                else if (status.Contains("Παραλήφθηκε")) e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                else if (status.Contains("Τιμολογήθηκε")) e.CellStyle.ForeColor = Color.FromArgb(155, 89, 182);
                else if (status.Contains("Ακυρωμένη")) e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
            }
            // % Παραλαβή - χρωματισμός μπάρας
            if (e.ColumnIndex == dgvOrders.Columns["ReceivedPct"].Index)
            {
                decimal pct = 0; decimal.TryParse(e.Value?.ToString(), out pct);
                if (pct >= 100) { e.CellStyle.BackColor = Color.FromArgb(209, 231, 221); e.CellStyle.ForeColor = Color.FromArgb(25, 135, 84); }
                else if (pct > 0) { e.CellStyle.BackColor = Color.FromArgb(255, 243, 205); e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34); }
                else { e.CellStyle.ForeColor = Color.Gray; }
                e.Value = $"{pct:N0}%";
            }
        }

        private Button CreateButton(string t, int x, int y, int w, int h, Color c) => new Button { Text = t, Location = new Point(x, y), Size = new Size(w, h), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), FlatAppearance = { BorderSize = 0 } };

        private void LoadFilters()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "Όλες", "🆕 Νέα", "✅ Επιβεβαιωμένη", "📦 Μερική Παραλαβή", "📥 Παραλήφθηκε", "🛒 Τιμολογήθηκε", "❌ Ακυρωμένη" });
            cmbStatus.SelectedIndex = 0;
            try { using var conn = new SqlConnection(GetConnectionString()); var s = conn.Query("SELECT Id, SupplierName FROM Suppliers WHERE IsActive=1 ORDER BY SupplierName").ToList(); cmbSupplier.Items.Clear(); cmbSupplier.Items.Add("Όλοι"); foreach (var x in s) cmbSupplier.Items.Add(new Item { Id = x.Id, Name = x.SupplierName }); cmbSupplier.DisplayMember = "Name"; cmbSupplier.SelectedIndex = 0; } catch { }
        }
        private class Item { public int Id; public string Name; public override string ToString() => Name; }

        private void LoadOrders()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // ✅ ΣΩΣΤΑ πεδία + % παραλαβής από ReceivedQty/Quantity
                string sql = @"SELECT po.Id, ISNULL(po.OrderNumber, po.DocumentNumber) AS OrderNumber, po.OrderDate, po.ExpectedDeliveryDate, s.SupplierName, po.TotalNet, po.Total,
                    (SELECT COUNT(*) FROM PurchaseOrderLines WHERE OrderId=po.Id) AS LinesCount,
                    CASE WHEN (SELECT SUM(Quantity) FROM PurchaseOrderLines WHERE OrderId=po.Id) > 0 
                         THEN (SELECT SUM(ISNULL(ReceivedQty,0)) FROM PurchaseOrderLines WHERE OrderId=po.Id) * 100.0 / (SELECT SUM(Quantity) FROM PurchaseOrderLines WHERE OrderId=po.Id)
                         ELSE 0 END AS ReceivedPct,
                    CASE po.Status WHEN 'NEW' THEN '🆕 Νέα' WHEN 'CONFIRMED' THEN '✅ Επιβεβαιωμένη' WHEN 'PARTIALLY_RECEIVED' THEN '📦 Μερική Παραλαβή' WHEN 'RECEIVED' THEN '📥 Παραλήφθηκε' WHEN 'INVOICED' THEN '🛒 Τιμολογήθηκε' WHEN 'CANCELLED' THEN '❌ Ακυρωμένη' ELSE po.Status END AS StatusDisplay
                    FROM PurchaseOrders po LEFT JOIN Suppliers s ON po.SupplierId=s.Id WHERE 1=1";
                if (cmbStatus.SelectedIndex > 0) sql += " AND po.Status = @St";
                if (cmbSupplier.SelectedIndex > 0) sql += " AND po.SupplierId = @Sup";
                sql += " AND po.OrderDate >= @From AND po.OrderDate <= @To";
                string srch = txtSearch.Text.Trim(); if (!string.IsNullOrEmpty(srch)) sql += " AND (po.OrderNumber LIKE @S OR po.DocumentNumber LIKE @S OR s.SupplierName LIKE @S)";
                sql += " ORDER BY po.OrderDate DESC";
                var list = conn.Query(sql, new { St = (cmbStatus.SelectedIndex > 0) ? GetStatusCode() : null, Sup = (cmbSupplier.SelectedItem as Item)?.Id, From = dtpFrom.Value.Date, To = dtpTo.Value.Date.AddDays(1), S = $"%{srch}%" }).ToList();
                dgvOrders.DataSource = list;
                lblTotal.Text = $"Σύνολο: {list.Count}";
                lblNew.Text = $"🆕 Νέες: {list.Count(x => x.StatusDisplay.Contains("Νέα"))}";
                lblConfirmed.Text = $"✅ Επιβεβαιωμένες: {list.Count(x => x.StatusDisplay.Contains("Επιβεβαιωμένη"))}";
                lblReceived.Text = $"📥 Παραληφθείσες: {list.Count(x => x.StatusDisplay.Contains("Παραλήφθηκε") || x.StatusDisplay.Contains("Τιμολογήθηκε"))}";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private string GetStatusCode() { string m = cmbStatus.SelectedItem.ToString(); return m.Contains("Νέα") ? "NEW" : m.Contains("Επιβεβαιωμένη") ? "CONFIRMED" : m.Contains("Μερική") ? "PARTIALLY_RECEIVED" : m.Contains("Παραλήφθηκε") ? "RECEIVED" : m.Contains("Τιμολογήθηκε") ? "INVOICED" : "CANCELLED"; }

        private void OpenOrder(int? id) { try { using (var f = new PurchaseOrderForm(id)) f.ShowDialog(); LoadOrders(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); } }
        private void EditSelected() { if (dgvOrders.CurrentRow == null) { MessageBox.Show("Επίλεξε παραγγελία!"); return; } OpenOrder(Convert.ToInt32(dgvOrders.CurrentRow.Cells["Id"].Value)); }

        private void ConvertToReceipt()
        {
            if (dgvOrders.CurrentRow == null) { MessageBox.Show("Επίλεξε παραγγελία!"); return; }
            string status = dgvOrders.CurrentRow.Cells["StatusDisplay"].Value?.ToString() ?? "";
            if (!status.Contains("Επιβεβαιωμένη") && !status.Contains("Μερική")) { MessageBox.Show("Μόνο επιβεβαιωμένες παραγγελίες μπορούν να παραληφθούν!"); return; }
            int poId = Convert.ToInt32(dgvOrders.CurrentRow.Cells["Id"].Value);
            try { using (var f = new GoodsReceiptForm()) { f.PreSelectedPurchaseOrderId = poId; f.ShowDialog(); } LoadOrders(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void ConvertToInvoice()
        {
            if (dgvOrders.CurrentRow == null) { MessageBox.Show("Επίλεξε παραγγελία!"); return; }
            int poId = Convert.ToInt32(dgvOrders.CurrentRow.Cells["Id"].Value);
            try { using (var f = new PurchaseOrderForm(poId)) f.ShowDialog(); LoadOrders(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void CancelSelected()
        {
            if (dgvOrders.CurrentRow == null) { MessageBox.Show("Επίλεξε παραγγελία!"); return; }
            int id = Convert.ToInt32(dgvOrders.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Ακύρωση παραγγελίας;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try { using var conn = new SqlConnection(GetConnectionString()); conn.Execute("UPDATE PurchaseOrders SET Status='CANCELLED', UpdatedAt=GETDATE() WHERE Id=@Id", new { Id = id }); LoadOrders(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
            }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}