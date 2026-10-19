using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class PurchaseQuotationListForm : Form
    {
        private DataGridView dgvQuotations;
        private ComboBox cmbStatus, cmbSupplier;
        private DateTimePicker dtpFrom, dtpTo;
        private TextBox txtSearch;
        private CheckBox chkShowExpired;
        private Button btnNew, btnEdit, btnConvert, btnDelete, btnRefresh, btnClose;
        private Label lblTotal, lblDraft, lblSent, lblAccepted, lblExpired, lblConverted;

        public PurchaseQuotationListForm() { InitializeUI(); this.Load += (s, e) => { LoadFilters(); AutoExpireOldOnes(); LoadQuotations(); }; }

        private void InitializeUI()
        {
            this.Text = "🏭 Λίστα Προσφορών Αγορών (RFQ)";
            this.Size = new Size(1550, 820);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1200, 650);

            var pnlToolbar = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.White, Padding = new Padding(15, 8, 15, 8) };
            btnNew = CreateButton("➕ Νέα RFQ", 10, 8, 130, 38, Color.FromArgb(46, 204, 113)); btnNew.Click += (s, e) => OpenQuotation(null); pnlToolbar.Controls.Add(btnNew);
            btnEdit = CreateButton("✏️ Επεξεργασία", 150, 8, 130, 38, Color.FromArgb(52, 152, 219)); btnEdit.Click += (s, e) => EditSelected(); pnlToolbar.Controls.Add(btnEdit);
            btnConvert = CreateButton("🏭 → Παραγγελία", 290, 8, 155, 38, Color.FromArgb(25, 135, 84)); btnConvert.Click += (s, e) => ConvertSelected(); pnlToolbar.Controls.Add(btnConvert);
            btnDelete = CreateButton("🗑️ Ακύρωση", 455, 8, 120, 38, Color.FromArgb(231, 76, 60)); btnDelete.Click += (s, e) => CancelSelected(); pnlToolbar.Controls.Add(btnDelete);
            btnRefresh = CreateButton("🔄 Ανανέωση", 585, 8, 120, 38, Color.FromArgb(108, 117, 125)); btnRefresh.Click += (s, e) => LoadQuotations(); pnlToolbar.Controls.Add(btnRefresh);
            btnClose = CreateButton("❌ Κλείσιμο", 715, 8, 110, 38, Color.FromArgb(149, 165, 166)); btnClose.Click += (s, e) => this.Close(); pnlToolbar.Controls.Add(btnClose);
            this.Controls.Add(pnlToolbar);

            var pnlFilters = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.White, Padding = new Padding(15, 8, 15, 8) };
            int lx = 10;
            pnlFilters.Controls.Add(new Label { Text = "Κατάσταση:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 80;
            cmbStatus = new ComboBox { Location = new Point(lx, 8), Size = new Size(170, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) }; cmbStatus.SelectedIndexChanged += (s, e) => LoadQuotations(); pnlFilters.Controls.Add(cmbStatus); lx += 180;
            pnlFilters.Controls.Add(new Label { Text = "Προμηθευτής:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 85;
            cmbSupplier = new ComboBox { Location = new Point(lx, 8), Size = new Size(200, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) }; cmbSupplier.SelectedIndexChanged += (s, e) => LoadQuotations(); pnlFilters.Controls.Add(cmbSupplier); lx += 210;
            pnlFilters.Controls.Add(new Label { Text = "Από:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpFrom = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(115, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) }; dtpFrom.Value = DateTime.Now.AddMonths(-6); dtpFrom.ValueChanged += (s, e) => LoadQuotations(); pnlFilters.Controls.Add(dtpFrom); lx += 125;
            pnlFilters.Controls.Add(new Label { Text = "Έως:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpTo = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(115, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) }; dtpTo.ValueChanged += (s, e) => LoadQuotations(); pnlFilters.Controls.Add(dtpTo); lx += 125;
            txtSearch = new TextBox { Location = new Point(lx, 8), Size = new Size(180, 26), Font = new Font("Segoe UI", 9.5F) }; txtSearch.TextChanged += (s, e) => LoadQuotations(); pnlFilters.Controls.Add(txtSearch); lx += 190;
            chkShowExpired = new CheckBox { Text = "Εμφάνιση Ληγμένων", Location = new Point(lx, 11), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Checked = true }; chkShowExpired.CheckedChanged += (s, e) => LoadQuotations(); pnlFilters.Controls.Add(chkShowExpired);
            this.Controls.Add(pnlFilters);

            dgvQuotations = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false, ReadOnly = true, RowHeadersVisible = false, BackgroundColor = Color.White, SelectionMode = DataGridViewSelectionMode.FullRowSelect, Font = new Font("Segoe UI", 10F), RowTemplate = { Height = 32 } };
            dgvQuotations.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(230, 126, 34), ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleCenter };
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuotationNumber", HeaderText = "Αριθμός", Width = 130 });
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuotationDate", HeaderText = "Ημερομηνία", Width = 100 });
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "ValidUntil", HeaderText = "Ισχύει Έως", Width = 100 });
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "DaysLeft", HeaderText = "Ημέρες", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "SupplierName", HeaderText = "Προμηθευτής", Width = 250 });
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalNet", HeaderText = "Καθαρή €", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Σύνολο €", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "LinesCount", HeaderText = "Είδη", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvQuotations.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusDisplay", HeaderText = "Κατάσταση", Width = 170, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });
            dgvQuotations.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSelected(); };
            dgvQuotations.CellFormatting += DgvQuotations_CellFormatting;
            this.Controls.Add(dgvQuotations);

            var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 42, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 8, 15, 8) };
            lblTotal = CreateStatLabel(pnlStatus, "Σύνολο: 0", 15, Color.FromArgb(52, 152, 219));
            lblDraft = CreateStatLabel(pnlStatus, "📝 Πρόχειρα: 0", 130, Color.FromArgb(108, 117, 125));
            lblSent = CreateStatLabel(pnlStatus, "📤 Εσταλμένες: 0", 310, Color.FromArgb(13, 110, 253));
            lblAccepted = CreateStatLabel(pnlStatus, "✅ Αποδέχτηκε: 0", 510, Color.FromArgb(25, 135, 84));
            lblExpired = CreateStatLabel(pnlStatus, "⏰ Έληξαν: 0", 720, Color.FromArgb(231, 76, 60));
            lblConverted = CreateStatLabel(pnlStatus, "🏭 Μετατράπηκαν: 0", 880, Color.FromArgb(155, 89, 182));
            this.Controls.Add(pnlStatus);
        }

        private Label CreateStatLabel(Panel p, string text, int x, Color color)
        {
            var lbl = new Label { Text = text, Location = new Point(x, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = color };
            p.Controls.Add(lbl); return lbl;
        }

        private void DgvQuotations_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvQuotations.Rows[e.RowIndex];

            if (e.ColumnIndex == dgvQuotations.Columns["DaysLeft"].Index)
            {
                int days = 0; int.TryParse(e.Value?.ToString(), out days);
                if (days < 0) { e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60); e.CellStyle.BackColor = Color.FromArgb(248, 215, 218); e.Value = $"ΕΛΗΞΕ {-days}μ"; }
                else if (days == 0) { e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34); e.CellStyle.BackColor = Color.FromArgb(255, 243, 205); e.Value = "ΣΗΜΕΡΑ!"; }
                else if (days <= 7) { e.CellStyle.ForeColor = Color.FromArgb(241, 196, 15); e.CellStyle.BackColor = Color.FromArgb(255, 251, 230); e.Value = $"{days}μ ⚠️"; }
                else { e.CellStyle.ForeColor = Color.FromArgb(46, 204, 113); e.Value = $"{days}μ"; }
            }

            if (e.ColumnIndex == dgvQuotations.Columns["StatusDisplay"].Index)
            {
                string status = row.Cells["StatusDisplay"].Value?.ToString() ?? "";
                if (status.Contains("Πρόχειρο")) e.CellStyle.ForeColor = Color.FromArgb(108, 117, 125);
                else if (status.Contains("Εστάλη")) e.CellStyle.ForeColor = Color.FromArgb(13, 110, 253);
                else if (status.Contains("Αποδέχτηκε")) e.CellStyle.ForeColor = Color.FromArgb(25, 135, 84);
                else if (status.Contains("Απορρίφθηκε")) e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
                else if (status.Contains("Έληξε")) { e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60); e.CellStyle.BackColor = Color.FromArgb(248, 215, 218); }
                else if (status.Contains("Μετατράπηκε")) e.CellStyle.ForeColor = Color.FromArgb(155, 89, 182);
            }

            if (row.Cells["StatusDisplay"].Value?.ToString()?.Contains("Έληξε") == true)
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 245);
        }

        private Button CreateButton(string t, int x, int y, int w, int h, Color c) => new Button { Text = t, Location = new Point(x, y), Size = new Size(w, h), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), FlatAppearance = { BorderSize = 0 } };

        private void LoadFilters()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "Όλες", "📝 Πρόχειρο", "📤 Εστάλη", "✅ Αποδέχτηκε", "❌ Απορρίφθηκε", "⏰ Έληξε", "🏭 Μετατράπηκε" });
            cmbStatus.SelectedIndex = 0;
            try { using var conn = new SqlConnection(GetConnectionString()); var s = conn.Query("SELECT Id, SupplierName FROM Suppliers WHERE IsActive=1 ORDER BY SupplierName").ToList(); cmbSupplier.Items.Clear(); cmbSupplier.Items.Add("Όλοι"); foreach (var x in s) cmbSupplier.Items.Add(new Item { Id = x.Id, Name = x.SupplierName }); cmbSupplier.DisplayMember = "Name"; cmbSupplier.SelectedIndex = 0; } catch { }
        }

        private class Item { public int Id; public string Name; public override string ToString() => Name; }

        private void AutoExpireOldOnes()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Execute("UPDATE Quotations SET Status = 'EXPIRED', UpdatedAt = GETDATE() WHERE QuotationType='PURCHASE' AND Status = 'SENT' AND ValidUntil < CAST(GETDATE() AS DATE)");
            }
            catch { }
        }

        private void LoadQuotations()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // ✅ FILTER: QuotationType='PURCHASE' + JOIN Suppliers
                string sql = @"SELECT q.Id, q.QuotationNumber, q.QuotationDate, q.ValidUntil,
                    DATEDIFF(DAY, GETDATE(), q.ValidUntil) AS DaysLeft,
                    s.SupplierName AS SupplierName, q.TotalNet, q.Total,
                    (SELECT COUNT(*) FROM QuotationLines WHERE QuotationId=q.Id) AS LinesCount,
                    CASE q.Status WHEN 'DRAFT' THEN '📝 Πρόχειρο' WHEN 'SENT' THEN '📤 Εστάλη' WHEN 'ACCEPTED' THEN '✅ ΕAcceptήθηκε' WHEN 'REJECTED' THEN '❌ Απορρίφθηκε' WHEN 'EXPIRED' THEN '⏰ Έληξε' WHEN 'CONVERTED' THEN '🏭 Μετατράπηκε' ELSE q.Status END AS StatusDisplay
                    FROM Quotations q LEFT JOIN Suppliers s ON q.SupplierId=s.Id WHERE q.QuotationType='PURCHASE'";

                if (cmbStatus.SelectedIndex > 0) sql += " AND q.Status = @St";
                if (cmbSupplier.SelectedIndex > 0) sql += " AND q.SupplierId = @Sup";
                if (!chkShowExpired.Checked) sql += " AND (q.ValidUntil >= CAST(GETDATE() AS DATE) OR q.Status IN ('CONVERTED','REJECTED'))";
                sql += " AND q.QuotationDate >= @From AND q.QuotationDate <= @To";
                string s = txtSearch.Text.Trim(); if (!string.IsNullOrEmpty(s)) sql += " AND (q.QuotationNumber LIKE @S OR s.SupplierName LIKE @S)";
                sql += " ORDER BY q.QuotationDate DESC";

                var list = conn.Query(sql, new { St = (cmbStatus.SelectedIndex > 0) ? GetStatusCode() : null, Sup = (cmbSupplier.SelectedItem as Item)?.Id, From = dtpFrom.Value.Date, To = dtpTo.Value.Date.AddDays(1), S = $"%{s}%" }).ToList();
                dgvQuotations.DataSource = list;

                lblTotal.Text = $"Σύνολο: {list.Count}";
                lblDraft.Text = $"📝 Πρόχειρα: {list.Count(x => x.StatusDisplay.Contains("Πρόχειρο"))}";
                lblSent.Text = $"📤 Εσταλμένες: {list.Count(x => x.StatusDisplay.Contains("Εστάλη"))}";
                lblAccepted.Text = $"✅ Αποδέχτηκε: {list.Count(x => x.StatusDisplay.Contains("ΕAcceptήθηκε"))}";
                lblExpired.Text = $"⏰ Έληξαν: {list.Count(x => x.StatusDisplay.Contains("Έληξε"))}";
                lblConverted.Text = $"🏭 Μετατράπηκαν: {list.Count(x => x.StatusDisplay.Contains("Μετατράπηκε"))}";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private string GetStatusCode()
        {
            string m = cmbStatus.SelectedItem.ToString();
            return m.Contains("Πρόχειρο") ? "DRAFT" : m.Contains("Εστάλη") ? "SENT" : m.Contains("Αποδέχτηκε") ? "ACCEPTED" : m.Contains("Απορρίφθηκε") ? "REJECTED" : m.Contains("Έληξε") ? "EXPIRED" : "CONVERTED";
        }

        private void OpenQuotation(int? id) { try { using (var f = new PurchaseQuotationForm(id)) f.ShowDialog(); LoadQuotations(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); } }
        private void EditSelected() { if (dgvQuotations.CurrentRow == null) { MessageBox.Show("Επίλεξε προσφορά!"); return; } OpenQuotation(Convert.ToInt32(dgvQuotations.CurrentRow.Cells["Id"].Value)); }

        private void ConvertSelected()
        {
            if (dgvQuotations.CurrentRow == null) { MessageBox.Show("Επίλεξε προσφορά!"); return; }
            string status = dgvQuotations.CurrentRow.Cells["StatusDisplay"].Value?.ToString() ?? "";
            if (status.Contains("Μετατράπηκε") || status.Contains("Απορρίφθηκε")) { MessageBox.Show("Αυτή η προσφορά δεν μπορεί να μετατραπεί!"); return; }
            int id = Convert.ToInt32(dgvQuotations.CurrentRow.Cells["Id"].Value);
            try { using (var f = new PurchaseQuotationForm(id)) f.ShowDialog(); LoadQuotations(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void CancelSelected()
        {
            if (dgvQuotations.CurrentRow == null) { MessageBox.Show("Επίλεξε προσφορά!"); return; }
            int id = Convert.ToInt32(dgvQuotations.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Ακύρωση προσφοράς;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try { using var conn = new SqlConnection(GetConnectionString()); conn.Execute("UPDATE Quotations SET Status='REJECTED', UpdatedAt=GETDATE() WHERE Id=@Id", new { Id = id }); LoadQuotations(); } catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
            }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}