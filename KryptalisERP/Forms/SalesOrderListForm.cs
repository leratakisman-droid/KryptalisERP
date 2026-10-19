using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class SalesOrderListForm : Form
    {
        private DataGridView dgvOrders;
        private ComboBox cmbStatus, cmbCustomer;
        private DateTimePicker dtpFrom, dtpTo;
        private TextBox txtSearch;
        private Button btnNew, btnEdit, btnConvert, btnDelete, btnRefresh, btnClose;
        private Label lblTotal, lblNew, lblConfirmed, lblInvoiced;

        public SalesOrderListForm() { InitializeUI(); this.Load += (s, e) => { LoadFilters(); LoadOrders(); }; }

        private void InitializeUI()
        {
            this.Text = "📋 Λίστα Παραγγελιών Πελατών";
            this.Size = new Size(1500, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            var pnlToolbar = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.White, Padding = new Padding(15, 8, 15, 8) };
            btnNew = CreateButton("➕ Νέα Παραγγελία", 10, 8, 160, 38, Color.FromArgb(46, 204, 113)); btnNew.Click += (s, e) => OpenOrder(null); pnlToolbar.Controls.Add(btnNew);
            btnEdit = CreateButton("✏️ Επεξεργασία", 180, 8, 130, 38, Color.FromArgb(52, 152, 219)); btnEdit.Click += (s, e) => EditSelected(); pnlToolbar.Controls.Add(btnEdit);
            btnConvert = CreateButton("🛒 → Τιμολόγιο", 320, 8, 150, 38, Color.FromArgb(155, 89, 182)); btnConvert.Click += (s, e) => ConvertSelected(); pnlToolbar.Controls.Add(btnConvert);
            btnDelete = CreateButton("🗑️ Ακύρωση", 480, 8, 120, 38, Color.FromArgb(231, 76, 60)); btnDelete.Click += (s, e) => CancelSelected(); pnlToolbar.Controls.Add(btnDelete);
            btnRefresh = CreateButton("🔄 Ανανέωση", 610, 8, 120, 38, Color.FromArgb(108, 117, 125)); btnRefresh.Click += (s, e) => LoadOrders(); pnlToolbar.Controls.Add(btnRefresh);
            btnClose = CreateButton("❌ Κλείσιμο", 740, 8, 110, 38, Color.FromArgb(149, 165, 166)); btnClose.Click += (s, e) => this.Close(); pnlToolbar.Controls.Add(btnClose);
            this.Controls.Add(pnlToolbar);

            var pnlFilters = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.White, Padding = new Padding(15, 8, 15, 8) };
            int lx = 10;
            pnlFilters.Controls.Add(new Label { Text = "Κατάσταση:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 80;
            cmbStatus = new ComboBox { Location = new Point(lx, 8), Size = new Size(160, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) }; cmbStatus.SelectedIndexChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(cmbStatus); lx += 170;
            pnlFilters.Controls.Add(new Label { Text = "Πελάτης:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 60;
            cmbCustomer = new ComboBox { Location = new Point(lx, 8), Size = new Size(200, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) }; cmbCustomer.SelectedIndexChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(cmbCustomer); lx += 210;
            pnlFilters.Controls.Add(new Label { Text = "Από:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpFrom = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(115, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) }; dtpFrom.Value = DateTime.Now.AddMonths(-6); dtpFrom.ValueChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(dtpFrom); lx += 125;
            pnlFilters.Controls.Add(new Label { Text = "Έως:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpTo = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(115, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) }; dtpTo.ValueChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(dtpTo); lx += 125;
            txtSearch = new TextBox { Location = new Point(lx, 8), Size = new Size(200, 26), Font = new Font("Segoe UI", 9.5F) }; txtSearch.TextChanged += (s, e) => LoadOrders(); pnlFilters.Controls.Add(txtSearch);
            this.Controls.Add(pnlFilters);

            dgvOrders = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false, ReadOnly = true, RowHeadersVisible = false, BackgroundColor = Color.White, SelectionMode = DataGridViewSelectionMode.FullRowSelect, Font = new Font("Segoe UI", 10F), RowTemplate = { Height = 32 } };
            dgvOrders.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(13, 110, 253), ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleCenter };
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderNumber", HeaderText = "Αριθμός", Width = 140 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderDate", HeaderText = "Ημερομηνία", Width = 110 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "DeliveryDate", HeaderText = "Ημ/νία Παράδοσης", Width = 130 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerName", HeaderText = "Πελάτης", Width = 250 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalNet", HeaderText = "Καθαρή €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Σύνολο €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "LinesCount", HeaderText = "Γραμμές", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusDisplay", HeaderText = "Κατάσταση", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });
            dgvOrders.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSelected(); };
            dgvOrders.CellFormatting += DgvOrders_CellFormatting;
            this.Controls.Add(dgvOrders);

            var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 40, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 8, 15, 8) };
            lblTotal = new Label { Text = "Σύνολο: 0", Location = new Point(15, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) }; pnlStatus.Controls.Add(lblTotal);
            lblNew = new Label { Text = "🆕 Νέες: 0", Location = new Point(150, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(108, 117, 125) }; pnlStatus.Controls.Add(lblNew);
            lblConfirmed = new Label { Text = "✅ Επιβεβαιωμένες: 0", Location = new Point(300, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(25, 135, 84) }; pnlStatus.Controls.Add(lblConfirmed);
            lblInvoiced = new Label { Text = "🛒 Τιμολογημένες: 0", Location = new Point(520, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(155, 89, 182) }; pnlStatus.Controls.Add(lblInvoiced);
            this.Controls.Add(pnlStatus);
        }

        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvOrders.Columns["StatusDisplay"].Index) return;
            string status = dgvOrders.Rows[e.RowIndex].Cells["StatusDisplay"].Value?.ToString() ?? "";
            if (status.Contains("Νέα")) e.CellStyle.ForeColor = Color.FromArgb(108, 117, 125);
            else if (status.Contains("Επιβεβαιωμένη")) e.CellStyle.ForeColor = Color.FromArgb(25, 135, 84);
            else if (status.Contains("Παράδοση")) e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34);
            else if (status.Contains("Τιμολογήθηκε")) e.CellStyle.ForeColor = Color.FromArgb(155, 89, 182);
            else if (status.Contains("Ακυρωμένη")) e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
        }

        private Button CreateButton(string t, int x, int y, int w, int h, Color c) => new Button { Text = t, Location = new Point(x, y), Size = new Size(w, h), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), FlatAppearance = { BorderSize = 0 } };

        private void LoadFilters()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "Όλες", "🆕 Νέα", "✅ Επιβεβαιωμένη", "📦 Μερική Παράδοση", "🚚 Παραδόθηκε", "🛒 Τιμολογήθηκε", "❌ Ακυρωμένη" });
            cmbStatus.SelectedIndex = 0;
            try { using var conn = new SqlConnection(GetConnectionString()); var c = conn.Query("SELECT Id, Epwnymia FROM Customers WHERE Energo=1 ORDER BY Epwnymia").ToList(); cmbCustomer.Items.Clear(); cmbCustomer.Items.Add("Όλοι"); foreach (var x in c) cmbCustomer.Items.Add(new Item { Id = x.Id, Name = x.Epwnymia }); cmbCustomer.DisplayMember = "Name"; cmbCustomer.SelectedIndex = 0; } catch { }
        }
        private class Item { public int Id; public string Name; public override string ToString() => Name; }

        private void LoadOrders()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // ✅ ΣΩΣΤΑ: CustomerId, DeliveryDate
                string sql = @"SELECT so.Id, so.OrderNumber, so.OrderDate, so.DeliveryDate, c.Epwnymia AS CustomerName, so.TotalNet, so.Total,
                    (SELECT COUNT(*) FROM SalesOrderLines WHERE OrderId=so.Id) AS LinesCount,
                    CASE so.Status WHEN 'NEW' THEN '🆕 Νέα' WHEN 'CONFIRMED' THEN '✅ Επιβεβαιωμένη' WHEN 'PARTIALLY_DELIVERED' THEN '📦 Μερική Παράδοση' WHEN 'DELIVERED' THEN '🚚 Παραδόθηκε' WHEN 'INVOICED' THEN '🛒 Τιμολογήθηκε' WHEN 'CANCELLED' THEN '❌ Ακυρωμένη' ELSE so.Status END AS StatusDisplay
                    FROM SalesOrders so LEFT JOIN Customers c ON so.CustomerId=c.Id WHERE 1=1";
                if (cmbStatus.SelectedIndex > 0) sql += " AND so.Status = @St";
                if (cmbCustomer.SelectedIndex > 0) sql += " AND so.CustomerId = @Cust";
                sql += " AND so.OrderDate >= @From AND so.OrderDate <= @To";
                string s = txtSearch.Text.Trim(); if (!string.IsNullOrEmpty(s)) sql += " AND (so.OrderNumber LIKE @S OR c.Epwnymia LIKE @S)";
                sql += " ORDER BY so.OrderDate DESC";
                var list = conn.Query(sql, new { St = (cmbStatus.SelectedIndex > 0) ? GetStatusCode() : null, Cust = (cmbCustomer.SelectedItem as Item)?.Id, From = dtpFrom.Value.Date, To = dtpTo.Value.Date.AddDays(1), S = $"%{s}%" }).ToList();
                dgvOrders.DataSource = list;
                lblTotal.Text = $"Σύνολο: {list.Count}";
                lblNew.Text = $"🆕 Νέες: {list.Count(x => x.StatusDisplay.Contains("Νέα"))}";
                lblConfirmed.Text = $"✅ Επιβεβαιωμένες: {list.Count(x => x.StatusDisplay.Contains("Επιβεβαιωμένη"))}";
                lblInvoiced.Text = $"🛒 Τιμολογημένες: {list.Count(x => x.StatusDisplay.Contains("Τιμολογήθηκε"))}";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private string GetStatusCode() { string m = cmbStatus.SelectedItem.ToString(); return m.Contains("Νέα") ? "NEW" : m.Contains("Επιβεβαιωμένη") ? "CONFIRMED" : m.Contains("Μερική") ? "PARTIALLY_DELIVERED" : m.Contains("Παραδόθηκε") ? "DELIVERED" : m.Contains("Τιμολογήθηκε") ? "INVOICED" : "CANCELLED"; }

        private void OpenOrder(int? id) { try { using (var f = new SalesOrderForm(id)) f.ShowDialog(); LoadOrders(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); } }
        private void EditSelected() { if (dgvOrders.CurrentRow == null) { MessageBox.Show("Επίλεξε παραγγελία!"); return; } OpenOrder(Convert.ToInt32(dgvOrders.CurrentRow.Cells["Id"].Value)); }

        private void ConvertSelected()
        {
            if (dgvOrders.CurrentRow == null) { MessageBox.Show("Επίλεξε παραγγελία!"); return; }
            string status = dgvOrders.CurrentRow.Cells["StatusDisplay"].Value?.ToString() ?? "";
            if (!status.Contains("Επιβεβαιωμένη") && !status.Contains("Παράδοση")) { MessageBox.Show("Μόνο επιβεβαιωμένες παραγγελίες μπορούν να τιμολογηθούν!"); return; }
            int orderId = Convert.ToInt32(dgvOrders.CurrentRow.Cells["Id"].Value);
            try { using (var f = new SalesOrderForm(orderId)) f.ShowDialog(); LoadOrders(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void CancelSelected()
        {
            if (dgvOrders.CurrentRow == null) { MessageBox.Show("Επίλεξε παραγγελία!"); return; }
            int id = Convert.ToInt32(dgvOrders.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Ακύρωση παραγγελίας;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try { using var conn = new SqlConnection(GetConnectionString()); conn.Execute("UPDATE SalesOrders SET Status='CANCELLED', UpdatedAt=GETDATE() WHERE Id=@Id", new { Id = id }); conn.Execute("UPDATE StockReservations SET Status='Released', ReleasedQty=Quantity, AvailableQty=0 WHERE DocumentType='SalesOrder' AND DocumentId=@Id", new { Id = id }); LoadOrders(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
            }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}