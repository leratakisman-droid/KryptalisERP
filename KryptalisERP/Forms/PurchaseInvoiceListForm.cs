using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class PurchaseInvoiceListForm : Form
    {
        private DataGridView dgvInvoices;
        private TextBox txtSearch;
        private ComboBox cmbDocType, cmbSeries, cmbSupplier, cmbStockStatus;
        private DateTimePicker dtpFrom, dtpTo;
        private CheckBox chkShowCancelled;
        private Button btnNew, btnEdit, btnDelete, btnGoodsReceipt, btnRefresh, btnClose;
        private Label lblTotalRecords, lblTotalAmount;

        public PurchaseInvoiceListForm()
        {
            InitializeUI();
            this.Load += PurchaseInvoiceListForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "🏭 Λίστα Τιμολογίων Αγοράς";
            this.Size = new Size(1600, 850);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1300, 700);

            var pnlToolbar = new Panel { Dock = DockStyle.Top, Height = 120, BackColor = Color.White, Padding = new Padding(15, 10, 15, 10), Margin = new Padding(0, 0, 0, 5) };

            btnNew = CreateButton("➕ Νέα Αγορά", 10, 10, 140, 38, Color.FromArgb(46, 204, 113));
            btnNew.Click += (s, e) => OpenInvoiceForm(null);
            pnlToolbar.Controls.Add(btnNew);

            btnEdit = CreateButton("✏️ Επεξεργασία", 160, 10, 130, 38, Color.FromArgb(52, 152, 219));
            btnEdit.Click += (s, e) => EditSelectedInvoice();
            pnlToolbar.Controls.Add(btnEdit);

            btnDelete = CreateButton("🗑️ Ακύρωση", 300, 10, 120, 38, Color.FromArgb(231, 76, 60));
            btnDelete.Click += (s, e) => DeleteSelectedInvoice();
            pnlToolbar.Controls.Add(btnDelete);

            btnGoodsReceipt = CreateButton("📦 Παραλαβή", 430, 10, 130, 38, Color.FromArgb(230, 126, 34));
            btnGoodsReceipt.Click += (s, e) =>
            {
                try { using (var frm = new GoodsReceiptForm()) frm.ShowDialog(); }
                catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
            };
            pnlToolbar.Controls.Add(btnGoodsReceipt);

            btnRefresh = CreateButton("🔄 Ανανέωση", 570, 10, 120, 38, Color.FromArgb(108, 117, 125));
            btnRefresh.Click += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(btnRefresh);

            btnClose = CreateButton("❌ Κλείσιμο", 700, 10, 110, 38, Color.FromArgb(149, 165, 166));
            btnClose.Click += (s, e) => this.Close();
            pnlToolbar.Controls.Add(btnClose);

            // Γραμμή 2 (✅ Labels με AutoSize)
            int y2 = 55, lx = 10;

            AddFilterLabel(pnlToolbar, "Από:", lx, y2);
            dtpFrom = new DateTimePicker { Location = new Point(lx + 40, y2), Size = new Size(120, 28), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpFrom.Value = DateTime.Now.AddMonths(-3);
            dtpFrom.ValueChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(dtpFrom);
            lx += 40 + 120 + 15;

            AddFilterLabel(pnlToolbar, "Έως:", lx, y2);
            dtpTo = new DateTimePicker { Location = new Point(lx + 40, y2), Size = new Size(120, 28), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpTo.ValueChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(dtpTo);
            lx += 40 + 120 + 15;

            AddFilterLabel(pnlToolbar, "Τύπος:", lx, y2);
            cmbDocType = new ComboBox { Location = new Point(lx + 50, y2), Size = new Size(170, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbDocType.SelectedIndexChanged += (s, e) => { LoadSeriesFilter(); LoadInvoices(); };
            pnlToolbar.Controls.Add(cmbDocType);
            lx += 50 + 170 + 15;

            AddFilterLabel(pnlToolbar, "Σειρά:", lx, y2);
            cmbSeries = new ComboBox { Location = new Point(lx + 50, y2), Size = new Size(150, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbSeries.SelectedIndexChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(cmbSeries);

            // Γραμμή 3
            int y3 = 88; lx = 10;

            AddFilterLabel(pnlToolbar, "Προμηθευτής:", lx, y3);
            cmbSupplier = new ComboBox { Location = new Point(lx + 95, y3), Size = new Size(230, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbSupplier.SelectedIndexChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(cmbSupplier);
            lx += 95 + 230 + 15;

            AddFilterLabel(pnlToolbar, "Κατ. Στοκ:", lx, y3);
            cmbStockStatus = new ComboBox { Location = new Point(lx + 75, y3), Size = new Size(170, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbStockStatus.Items.AddRange(new object[] { "Όλα", "Με Ενημερωμένο Στοκ", "Χωρίς Ενημέρωση" });
            cmbStockStatus.SelectedIndex = 0;
            cmbStockStatus.SelectedIndexChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(cmbStockStatus);
            lx += 75 + 170 + 15;

            AddFilterLabel(pnlToolbar, "🔍 Αναζήτηση:", lx, y3);
            txtSearch = new TextBox { Location = new Point(lx + 90, y3), Size = new Size(230, 28), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle };
            txtSearch.TextChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(txtSearch);
            lx += 90 + 230 + 15;

            chkShowCancelled = new CheckBox { Text = "Εμφάνιση Ακυρωμένων", Location = new Point(lx, y3 + 4), AutoSize = true, Font = new Font("Segoe UI", 9F) };
            chkShowCancelled.CheckedChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(chkShowCancelled);

            // STATUS BAR
            var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 35, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 5, 15, 5) };
            lblTotalRecords = new Label { Text = "Εγγραφές: 0", Location = new Point(15, 8), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) };
            pnlStatus.Controls.Add(lblTotalRecords);
            lblTotalAmount = new Label { Text = "Συνολική Αξία: 0,00 €", Location = new Point(200, 8), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(230, 126, 34) };
            pnlStatus.Controls.Add(lblTotalAmount);

            // GRID
            dgvInvoices = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 10F),
                RowTemplate = { Height = 32 },
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false
            };

            dgvInvoices.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
            };

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocNumber", DataPropertyName = "DocNumber", HeaderText = "Αριθμός", Width = 120 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocDate", DataPropertyName = "DocDate", HeaderText = "Ημερομηνία", Width = 110 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocTypeName", DataPropertyName = "DocTypeName", HeaderText = "Τύπος", Width = 150 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "SeriesName", DataPropertyName = "SeriesName", HeaderText = "Σειρά", Width = 120 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "SupplierName", DataPropertyName = "SupplierName", HeaderText = "Προμηθευτής", Width = 250 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "NetValue", DataPropertyName = "NetValue", HeaderText = "Καθαρή €", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "VatValue", DataPropertyName = "VatValue", HeaderText = "ΦΠΑ €", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "WithholdingAmount", DataPropertyName = "WithholdingAmount", HeaderText = "Παρακράτηση €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.FromArgb(231, 76, 60) } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "FinalAmount", DataPropertyName = "FinalAmount", HeaderText = "Τελικό €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockStatus", DataPropertyName = "StockStatus", HeaderText = "Στοκ", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "IsActive", DataPropertyName = "IsActive", HeaderText = "Κατάσταση", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            dgvInvoices.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSelectedInvoice(); };
            dgvInvoices.CellFormatting += DgvInvoices_CellFormatting;

            // ✅ ΣΩΣΤΗ ΣΕΙΡΑ + BRINGTOFRONT
            this.Controls.Add(pnlToolbar);
            this.Controls.Add(pnlStatus);
            this.Controls.Add(dgvInvoices);
            dgvInvoices.BringToFront();
        }

        private void AddFilterLabel(Panel p, string text, int x, int y)
        {
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 5), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(33, 37, 41) });
        }

        private void DgvInvoices_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvInvoices.Rows[e.RowIndex];

            if (row.Cells["IsActive"].Value?.ToString() == "❌ Ακυρωμένο")
            {
                row.DefaultCellStyle.ForeColor = Color.Gray;
                row.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            }

            if (e.ColumnIndex == dgvInvoices.Columns["StockStatus"].Index)
            {
                string status = e.Value?.ToString();
                if (status == "✅ Ενημερωμένο") { e.CellStyle.ForeColor = Color.FromArgb(46, 204, 113); e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold); }
                else { e.CellStyle.ForeColor = Color.FromArgb(241, 196, 15); e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold); }
            }
        }

        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
        {
            return new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
        }

        private void PurchaseInvoiceListForm_Load(object sender, EventArgs e)
        {
            LoadDocumentTypes();
            LoadSuppliers();
            LoadInvoices();
        }

        private void LoadDocumentTypes()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var types = conn.Query("SELECT Id, Name FROM DocumentTypes WHERE IsPurchase = 1 AND IsActive = 1 ORDER BY Name").ToList();
                cmbDocType.Items.Clear();
                cmbDocType.Items.Add("Όλοι οι Τύποι");
                foreach (var t in types) cmbDocType.Items.Add(t.Name);
                cmbDocType.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadSeriesFilter()
        {
            cmbSeries.Items.Clear();
            cmbSeries.Items.Add("Όλες οι Σειρές");
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var series = conn.Query("SELECT Id, Name FROM DocumentSeries WHERE IsActive = 1 ORDER BY Name").ToList();
                foreach (var s in series) cmbSeries.Items.Add(s.Name);
            }
            catch { }
            cmbSeries.SelectedIndex = 0;
        }

        private void LoadSuppliers()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var suppliers = conn.Query("SELECT Id, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName").ToList();
                cmbSupplier.Items.Clear();
                cmbSupplier.Items.Add("Όλοι οι Προμηθευτές");
                foreach (var s in suppliers) cmbSupplier.Items.Add(s.SupplierName);
                cmbSupplier.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadInvoices()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                string sql = @"
                    SELECT 
                        pi.Id, pi.DocNumber, pi.DocDate,
                        dt.Name AS DocTypeName,
                        ds.Name AS SeriesName,
                        pi.SupplierName,
                        pi.NetValue, pi.VatValue,
                        ISNULL(pi.WithholdingAmount, 0) AS WithholdingAmount,
                        ISNULL(pi.FinalAmount, pi.GrandTotal) AS FinalAmount,
                        CASE 
                            WHEN EXISTS (SELECT 1 FROM StockTransactions st WHERE st.DocumentId = pi.Id AND st.DocumentType = 'PurchaseInvoice') 
                            THEN '✅ Ενημερωμένο' ELSE '⏳ Εκκρεμεί'
                        END AS StockStatus,
                        CASE WHEN pi.IsActive = 1 THEN '✅ Ενεργό' ELSE '❌ Ακυρωμένο' END AS IsActive
                    FROM PurchaseInvoices pi
                    LEFT JOIN DocumentTypes dt ON pi.DocType = dt.Id
                    LEFT JOIN DocumentSeries ds ON pi.SeriesId = ds.Id
                    WHERE pi.DocDate >= @DateFrom AND pi.DocDate <= @DateTo";

                if (cmbDocType.SelectedIndex > 0) sql += " AND dt.Name = @DocTypeName";
                if (cmbSeries.SelectedIndex > 0) sql += " AND ds.Name = @SeriesName";
                if (cmbSupplier.SelectedIndex > 0) sql += " AND pi.SupplierName = @SupplierName";

                string stockFilter = cmbStockStatus.SelectedItem?.ToString();
                if (stockFilter == "Με Ενημερωμένο Στοκ") sql += " AND EXISTS (SELECT 1 FROM StockTransactions st WHERE st.DocumentId = pi.Id AND st.DocumentType = 'PurchaseInvoice')";
                else if (stockFilter == "Χωρίς Ενημέρωση") sql += " AND NOT EXISTS (SELECT 1 FROM StockTransactions st WHERE st.DocumentId = pi.Id AND st.DocumentType = 'PurchaseInvoice')";

                if (!chkShowCancelled.Checked) sql += " AND pi.IsActive = 1";

                string search = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(search)) sql += " AND (pi.DocNumber LIKE @Search OR pi.SupplierName LIKE @Search)";

                sql += " ORDER BY pi.DocDate DESC, pi.Id DESC";

                var invoices = conn.Query(sql, new
                {
                    DateFrom = dtpFrom.Value.Date,
                    DateTo = dtpTo.Value.Date.AddDays(1),
                    DocTypeName = cmbDocType.SelectedItem?.ToString(),
                    SeriesName = cmbSeries.SelectedItem?.ToString(),
                    SupplierName = cmbSupplier.SelectedItem?.ToString(),
                    Search = $"%{search}%"
                }).ToList();

                dgvInvoices.DataSource = invoices;
                lblTotalRecords.Text = $"Εγγραφές: {invoices.Count}";
                lblTotalAmount.Text = $"Συνολική Αξία: {invoices.Sum(i => (decimal?)i.FinalAmount) ?? 0:N2} €";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenInvoiceForm(int? invoiceId)
        {
            try { using (var frm = new PurchaseInvoiceForm(invoiceId)) frm.ShowDialog(); LoadInvoices(); }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void EditSelectedInvoice()
        {
            if (dgvInvoices.CurrentRow == null) { MessageBox.Show("Επίλεξε μια αγορά!"); return; }
            OpenInvoiceForm(Convert.ToInt32(dgvInvoices.CurrentRow.Cells["Id"].Value));
        }

        private void DeleteSelectedInvoice()
        {
            if (dgvInvoices.CurrentRow == null) { MessageBox.Show("Επίλεξε μια αγορά!"); return; }
            string stockStatus = dgvInvoices.CurrentRow.Cells["StockStatus"].Value?.ToString();
            if (stockStatus == "✅ Ενημερωμένο")
            {
                MessageBox.Show("Δεν μπορείς να ακυρώσεις αγορά με ενημερωμένο στοκ!\nΧρειάζεται πιστωτικό.");
                return;
            }

            int invoiceId = Convert.ToInt32(dgvInvoices.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Ακύρωση αγοράς;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    conn.Execute("UPDATE PurchaseInvoices SET IsActive = 0 WHERE Id = @Id", new { Id = invoiceId });
                    LoadInvoices();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
            }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}