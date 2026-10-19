using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class PurchaseOrderForm : Form
    {
        private ComboBox cmbSupplier, cmbWarehouse, cmbPaymentTerms, cmbStatus, cmbCurrency;
        private TextBox txtOrderNumber, txtNotes, txtSupplierAfm, txtSupplierPhone, txtSupplierEmail;
        private DateTimePicker dtpOrderDate, dtpExpectedDate;
        private DataGridView dgvLines;
        private Label lblTotalNet, lblTotalVat, lblGrandTotal, lblStockWarning;
        private Button btnSave, btnConfirm, btnConvertToReceipt, btnConvertToInvoice, btnCancel;

        private int? _orderId;
        private bool _isLoading = false;

        // ✅ PreFill από προσφορά αγοράς
        private dynamic _preQuotation;
        private List<dynamic> _preQuotationLines;
        public int SavedOrderId => _orderId ?? 0;

        public void PreFillFromPurchaseQuotation(dynamic quotation, List<dynamic> lines)
        {
            _preQuotation = quotation;
            _preQuotationLines = lines ?? new List<dynamic>();
        }
        public static readonly Dictionary<string, string> StatusMap = new Dictionary<string, string>
        {
            { "NEW", "🆕 Νέα" },
            { "CONFIRMED", "✅ Επιβεβαιωμένη" },
            { "PARTIALLY_RECEIVED", "📦 Μερική Παραλαβή" },
            { "RECEIVED", "📥 Παραλήφθηκε" },
            { "INVOICED", "🛒 Τιμολογήθηκε" },
            { "CANCELLED", "❌ Ακυρωμένη" }
        };

        public PurchaseOrderForm(int? orderId = null)
        {
            _orderId = orderId;
            InitializeUI();
            this.Load += PurchaseOrderForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "🏭 Παραγγελία Προς Προμηθευτή";
            this.Size = new Size(1450, 820);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1150, 650);

            // --- 1. ΒΑΣΙΚΑ ---
            var pnlBasic = CreateHeaderPanel("📋 ΒΑΣΙΚΑ ΣΤΟΙΧΕΙΑ ΠΑΡΑΓΓΕΛΙΑΣ ΑΓΟΡΑΣ", 85, Color.FromArgb(230, 126, 34));
            int y = 22, lx = 10, lw = 90, gap = 10;

            txtOrderNumber = AddText(pnlBasic, "Αριθμός:", lx, y, lw, 140, true);
            lx += lw + 140 + gap;
            dtpOrderDate = AddDate(pnlBasic, "Ημ/νία:", lx, y, lw, 120);
            lx += lw + 120 + gap;
            dtpExpectedDate = AddDate(pnlBasic, "Παραλαβή:", lx, y, lw, 120);
            lx += lw + 120 + gap;
            cmbStatus = AddCombo(pnlBasic, "Κατάσταση:", lx, y, lw, 170);
            cmbStatus.SelectedIndexChanged += (s, e) => OnStatusChanged();

            y = 52; lx = 10;
            cmbWarehouse = AddCombo(pnlBasic, "Αποθήκη:", lx, y, lw, 150);
            cmbWarehouse.SelectedIndexChanged += (s, e) => RefreshStockColumn();
            lx += lw + 150 + gap;
            cmbPaymentTerms = AddCombo(pnlBasic, "Όροι Πληρ.:", lx, y, lw, 150);
            lx += lw + 150 + gap;
            cmbCurrency = AddCombo(pnlBasic, "Νόμισμα:", lx, y, lw, 100);
            cmbCurrency.Items.AddRange(new object[] { "EUR", "USD", "GBP" });
            cmbCurrency.SelectedIndex = 0;
            this.Controls.Add(pnlBasic);

            // --- 2. ΠΡΟΜΗΘΕΥΤΗΣ ---
            var pnlSupplier = CreateHeaderPanel("🏭 ΠΡΟΜΗΘΕΥΤΗΣ", 85, Color.FromArgb(230, 126, 34));
            y = 22; lx = 10;
            cmbSupplier = AddCombo(pnlSupplier, "Προμηθευτής:", lx, y, lw, 280);
            cmbSupplier.SelectedIndexChanged += (s, e) => LoadSupplierDetails();
            lx += lw + 280 + gap;
            txtSupplierAfm = AddText(pnlSupplier, "ΑΦΜ:", lx, y, 45, 100, true);
            lx += 45 + 100 + gap;
            txtSupplierPhone = AddText(pnlSupplier, "Τηλ.:", lx, y, 45, 120, false);
            lx += 45 + 120 + gap;
            txtSupplierEmail = AddText(pnlSupplier, "Email:", lx, y, 50, 180, false);
            this.Controls.Add(pnlSupplier);

            // --- 3. GRID ΓΡΑΜΜΩΝ ---
            dgvLines = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 9.5F),
                RowTemplate = { Height = 28 }
            };

            dgvLines.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(3)
            };

            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "LineNum", HeaderText = "Α/Α", Width = 40, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemId", HeaderText = "ItemId", Width = 50, Visible = false });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", HeaderText = "Κωδικός", Width = 100 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Περιγραφή", Width = 280 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Ποσότητα", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Uom", HeaderText = "ΜΜ", Width = 50 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitPrice", HeaderText = "Κόστος €", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Discount", HeaderText = "Έκτ.%", Width = 55, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "NetValue", HeaderText = "Καθαρή €", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "VatRate", HeaderText = "ΦΠΑ%", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Vat", HeaderText = "ΦΠΑ €", Width = 75, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Σύνολο €", Width = 95, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReceivedQty", HeaderText = "Παραλήφθηκε", Width = 100, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(240, 240, 240) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockAvail", HeaderText = "Τρέχον Στοκ", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(240, 240, 240) } });

            dgvLines.CellValueChanged += DgvLines_CellValueChanged;
            dgvLines.RowsAdded += (s, e) => { for (int i = 0; i < e.RowCount; i++) dgvLines.Rows[e.RowIndex + i].Cells["LineNum"].Value = e.RowIndex + i + 1; };
            dgvLines.UserDeletingRow += (s, e) => CalculateTotals();

            // --- 4. ΣΗΜΕΙΩΣΕΙΣ ---
            var pnlNotes = CreateHeaderPanel("📝 ΣΗΜΕΙΩΣΕΙΣ", 45, Color.FromArgb(108, 117, 125));
            txtNotes = new TextBox { Location = new Point(110, 8), Size = new Size(1250, 28), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle };
            pnlNotes.Controls.Add(txtNotes);

            // --- 5. FOOTER ---
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 75, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(10, 8, 10, 8) };

            btnSave = CreateButton("💾 Αποθήκευση", 10, 15, 130, 40, Color.FromArgb(13, 110, 253));
            btnSave.Click += (s, e) => SaveOrder(false);
            pnlFooter.Controls.Add(btnSave);

            btnConfirm = CreateButton("✅ Επιβεβαίωση", 150, 15, 140, 40, Color.FromArgb(25, 135, 84));
            btnConfirm.Click += (s, e) => ConfirmOrder();
            pnlFooter.Controls.Add(btnConfirm);

            btnConvertToReceipt = CreateButton("📦 → Παραλαβή", 300, 15, 140, 40, Color.FromArgb(230, 126, 34));
            btnConvertToReceipt.Click += (s, e) => ConvertToGoodsReceipt();
            pnlFooter.Controls.Add(btnConvertToReceipt);

            btnConvertToInvoice = CreateButton("🛒 → Τιμολόγιο", 450, 15, 150, 40, Color.FromArgb(155, 89, 182));
            btnConvertToInvoice.Click += (s, e) => ConvertToInvoice();
            pnlFooter.Controls.Add(btnConvertToInvoice);

            btnCancel = CreateButton("❌ Κλείσιμο", 610, 15, 110, 40, Color.FromArgb(108, 117, 125));
            btnCancel.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnCancel);

            var pnlTotals = new Panel { Location = new Point(740, 5), Size = new Size(680, 65), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            int tx = 8, ty = 6, tw = 160, th = 22;
            lblTotalNet = CreateTotalLabel("Καθαρή Αξία:", tx, ty, tw, th, Color.FromArgb(52, 152, 219)); pnlTotals.Controls.Add(lblTotalNet);
            lblTotalVat = CreateTotalLabel("ΦΠΑ:", tx + tw + 5, ty, tw, th, Color.FromArgb(241, 196, 15)); pnlTotals.Controls.Add(lblTotalVat);
            lblGrandTotal = CreateTotalLabel("ΣΥΝΟΛΟ ΠΑΡΑΓΓΕΛΙΑΣ:", tx, ty + th + 4, tw, th, Color.FromArgb(46, 204, 113), true); pnlTotals.Controls.Add(lblGrandTotal);
            lblStockWarning = new Label { Text = "", Location = new Point(tx + tw + 5, ty + th + 4), Size = new Size(tw, th), Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60), AutoSize = false, TextAlign = ContentAlignment.MiddleLeft };
            pnlTotals.Controls.Add(lblStockWarning);
            pnlFooter.Controls.Add(pnlTotals);

            this.Controls.Add(pnlFooter);
            this.Controls.Add(dgvLines);
            this.Controls.Add(pnlNotes);
            this.Controls.Add(pnlSupplier);
            this.Controls.Add(pnlBasic);
            dgvLines.BringToFront();
        }

        // ============================================
        // HELPERS UI
        // ============================================
        private Panel CreateHeaderPanel(string title, int height, Color color)
        {
            var pnl = new Panel { Dock = DockStyle.Top, Height = height, BackColor = Color.White, Padding = new Padding(10, 8, 10, 8), Margin = new Padding(0, 0, 0, 3) };
            pnl.Controls.Add(new Label { Text = title, Location = new Point(10, 3), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = color });
            return pnl;
        }
        private ComboBox AddCombo(Panel p, string lbl, int x, int y, int lw, int cw)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var c = new ComboBox { Location = new Point(x + lw, y), Size = new Size(cw, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            p.Controls.Add(c); return c;
        }
        private TextBox AddText(Panel p, string lbl, int x, int y, int lw, int cw, bool ro = false)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var t = new TextBox { Location = new Point(x + lw, y), Size = new Size(cw, 26), ReadOnly = ro, Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle, BackColor = ro ? Color.LightYellow : Color.White };
            p.Controls.Add(t); return t;
        }
        private DateTimePicker AddDate(Panel p, string lbl, int x, int y, int lw, int cw)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var d = new DateTimePicker { Location = new Point(x + lw, y), Size = new Size(cw, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            p.Controls.Add(d); return d;
        }
        private Label CreateTotalLabel(string text, int x, int y, int w, int h, Color color, bool bold = false)
            => new Label { Text = $"{text} 0,00 €", Location = new Point(x, y), Size = new Size(w, h), Font = new Font("Segoe UI", bold ? 10F : 9F, bold ? FontStyle.Bold : FontStyle.Regular), ForeColor = color, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(4, 2, 4, 2) };
        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
            => new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
        private int GetUserId() { try { return Program.CurrentUserId; } catch { return 1; } }

        // ============================================
        // LOAD
        // ============================================
        private void PurchaseOrderForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            LoadStatuses(); LoadSuppliers(); LoadWarehouses(); LoadPaymentTerms();
            GenerateNumber();
            _isLoading = false;
            if (_orderId.HasValue) LoadOrder(_orderId.Value);
        }

        private void LoadStatuses()
        {
            cmbStatus.DataSource = new BindingList<KeyValuePair<string, string>>(StatusMap.ToList());
            cmbStatus.DisplayMember = "Value"; cmbStatus.ValueMember = "Key";
            cmbStatus.SelectedValue = "NEW";
        }
        private void LoadSuppliers()
        {
            try { using var conn = new SqlConnection(GetConnectionString()); var s = conn.Query("SELECT Id, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName").ToList(); cmbSupplier.DataSource = s; cmbSupplier.DisplayMember = "SupplierName"; cmbSupplier.ValueMember = "Id"; } catch { }
        }
        private void LoadWarehouses()
        {
            try { using var conn = new SqlConnection(GetConnectionString()); var w = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive = 1 ORDER BY Name").ToList(); cmbWarehouse.DataSource = w; cmbWarehouse.DisplayMember = "Name"; cmbWarehouse.ValueMember = "Id"; } catch { }
        }
        private void LoadPaymentTerms()
        {
            try { using var conn = new SqlConnection(GetConnectionString()); var t = conn.Query("SELECT Id, TermName FROM PaymentTerms WHERE IsActive = 1 ORDER BY TermName").ToList(); cmbPaymentTerms.DataSource = t; cmbPaymentTerms.DisplayMember = "TermName"; cmbPaymentTerms.ValueMember = "Id"; } catch { }
        }
        private void LoadSupplierDetails()
        {
            if (_isLoading || cmbSupplier.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var s = conn.QueryFirstOrDefault("SELECT Afm, Phone, Email FROM Suppliers WHERE Id = @Id", new { Id = Convert.ToInt32(cmbSupplier.SelectedValue) });
                if (s != null) { txtSupplierAfm.Text = s.Afm ?? ""; txtSupplierPhone.Text = s.Phone ?? ""; txtSupplierEmail.Text = s.Email ?? ""; }
            }
            catch { }
        }
        private void GenerateNumber()
        {
            try { using var conn = new SqlConnection(GetConnectionString()); int next = conn.ExecuteScalar<int>("SELECT ISNULL(MAX(Id), 0) + 1 FROM PurchaseOrders"); txtOrderNumber.Text = $"ΠΑΑ-{DateTime.Now:yyyy}-{next:D4}"; } catch { }
        }
        private void OnStatusChanged()
        {
            if (_isLoading) return;
            string status = cmbStatus.SelectedValue?.ToString();
            bool locked = status == "INVOICED" || status == "CANCELLED" || status == "RECEIVED";
            dgvLines.ReadOnly = locked; dgvLines.AllowUserToAddRows = !locked; dgvLines.AllowUserToDeleteRows = !locked;
            btnSave.Enabled = !locked; btnConfirm.Enabled = status == "NEW";
            btnConvertToReceipt.Enabled = status == "CONFIRMED" || status == "PARTIALLY_RECEIVED";
            btnConvertToInvoice.Enabled = status == "CONFIRMED" || status == "PARTIALLY_RECEIVED" || status == "RECEIVED";
        }

        // ============================================
        // GRID LOGIC
        // ============================================
        private void DgvLines_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var row = dgvLines.Rows[e.RowIndex];
            var col = dgvLines.Columns[e.ColumnIndex].Name;

            if (col == "ItemCode" && !row.IsNewRow)
            {
                string code = row.Cells["ItemCode"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(code))
                {
                    try
                    {
                        using var conn = new SqlConnection(GetConnectionString());
                        var item = conn.QueryFirstOrDefault(@"SELECT i.Id, i.Name, i.Unit, i.PurchasePrice, i.VatRate, ISNULL(i.Stock,0) AS Avail FROM Items i WHERE i.Code = @Code", new { Code = code });
                        if (item != null)
                        {
                            row.Cells["ItemId"].Value = item.Id;
                            row.Cells["Description"].Value = item.Name;
                            row.Cells["Uom"].Value = item.Unit;
                            row.Cells["StockAvail"].Value = item.Avail;
                            if (row.Cells["UnitPrice"].Value == null) row.Cells["UnitPrice"].Value = item.PurchasePrice;
                            if (row.Cells["VatRate"].Value == null) row.Cells["VatRate"].Value = item.VatRate ?? 24;
                        }
                    }
                    catch { }
                }
            }
            if (col == "Quantity" || col == "UnitPrice" || col == "Discount" || col == "VatRate")
            { CalcRow(row); CalculateTotals(); }
        }

        private void CalcRow(DataGridViewRow row)
        {
            decimal qty = Dec(row, "Quantity"), price = Dec(row, "UnitPrice"), disc = Dec(row, "Discount"), vatR = Dec(row, "VatRate");
            decimal gross = qty * price, discVal = gross * disc / 100, net = gross - discVal, vatVal = net * vatR / 100;
            row.Cells["NetValue"].Value = net; row.Cells["Vat"].Value = vatVal; row.Cells["Total"].Value = net + vatVal;
        }
        private decimal Dec(DataGridViewRow row, string col) => decimal.TryParse(row.Cells[col].Value?.ToString(), out var v) ? v : 0;

        private void CalculateTotals()
        {
            decimal net = 0, vat = 0;
            foreach (DataGridViewRow r in dgvLines.Rows) { if (r.IsNewRow) continue; net += Dec(r, "NetValue"); vat += Dec(r, "Vat"); }
            lblTotalNet.Text = $"Καθαρή Αξία: {net:N2} €"; lblTotalVat.Text = $"ΦΠΑ: {vat:N2} €"; lblGrandTotal.Text = $"ΣΥΝΟΛΟ ΠΑΡΑΓΓΕΛΙΑΣ: {net + vat:N2} €";
        }
        private void RefreshStockColumn()
        {
            if (_isLoading) return;
            foreach (DataGridViewRow r in dgvLines.Rows)
            {
                if (r.IsNewRow) continue;
                int? itemId = r.Cells["ItemId"].Value != null ? Convert.ToInt32(r.Cells["ItemId"].Value) : (int?)null;
                if (itemId.HasValue)
                {
                    try { using var conn = new SqlConnection(GetConnectionString()); r.Cells["StockAvail"].Value = conn.ExecuteScalar<decimal?>("SELECT ISNULL(SUM(AvailableQty),0) FROM StockVariants WHERE ItemVariantId IN (SELECT Id FROM ItemVariants WHERE ItemId=@ItemId) AND (@Wh IS NULL OR WarehouseId=@Wh)", new { ItemId = itemId, Wh = cmbWarehouse.SelectedValue != null ? Convert.ToInt32(cmbWarehouse.SelectedValue) : (int?)null }) ?? 0; } catch { }
                }
            }
        }

        // ============================================
        // SAVE
        // ============================================
        private void SaveOrder(bool isConfirm)
        {
            if (cmbSupplier.SelectedValue == null) { MessageBox.Show("Επίλεξε Προμηθευτή!"); return; }
            if (dgvLines.Rows.Count == 1 && dgvLines.Rows[0].IsNewRow) { MessageBox.Show("Πρόσθεσε είδη!"); return; }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                int supplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
                int? whId = cmbWarehouse.SelectedValue != null ? Convert.ToInt32(cmbWarehouse.SelectedValue) : null;
                int? ptId = cmbPaymentTerms.SelectedValue != null ? Convert.ToInt32(cmbPaymentTerms.SelectedValue) : null;
                string status = isConfirm ? "CONFIRMED" : (cmbStatus.SelectedValue?.ToString() ?? "NEW");

                decimal net = 0, vat = 0, discTotal = 0;
                foreach (DataGridViewRow r in dgvLines.Rows) { if (r.IsNewRow) continue; net += Dec(r, "NetValue"); vat += Dec(r, "Vat"); discTotal += (Dec(r, "Quantity") * Dec(r, "UnitPrice")) * Dec(r, "Discount") / 100; }
                decimal total = net + vat;

                int orderId;
                if (_orderId.HasValue)
                {
                    conn.Execute(@"UPDATE PurchaseOrders SET SupplierId=@Sup, WarehouseId=@Wh, PaymentTermsId=@Pt, Currency=@Cur, OrderDate=@ODate, ExpectedDeliveryDate=@ExpDate, Status=@Status, Notes=@Notes, TotalNet=@Net, TotalVat=@Vat, TotalDiscount=@Disc, Total=@Total, UpdatedAt=GETDATE() WHERE Id=@Id",
                        new { Id = _orderId, Sup = supplierId, Wh = whId, Pt = ptId, Cur = cmbCurrency.Text, ODate = dtpOrderDate.Value, ExpDate = dtpExpectedDate.Value, Status = status, Notes = txtNotes.Text, Net = net, Vat = vat, Disc = discTotal, Total = total }, tran);
                    orderId = _orderId.Value;
                    conn.Execute("DELETE FROM PurchaseOrderLines WHERE OrderId=@Id", new { Id = orderId }, tran);
                }
                else
                {
                    orderId = conn.ExecuteScalar<int>(@"INSERT INTO PurchaseOrders (CompanyId, SupplierId, WarehouseId, PaymentTermsId, Currency, OrderNumber, DocumentNumber, OrderDate, ExpectedDeliveryDate, Status, Notes, TotalNet, TotalVat, TotalDiscount, Total, CreatedBy, CreatedAt, UpdatedAt) VALUES (1, @Sup, @Wh, @Pt, @Cur, @Num, @Num, @ODate, @ExpDate, @Status, @Notes, @Net, @Vat, @Disc, @Total, @UserId, GETDATE(), GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { Sup = supplierId, Wh = whId, Pt = ptId, Cur = cmbCurrency.Text, Num = txtOrderNumber.Text, ODate = dtpOrderDate.Value, ExpDate = dtpExpectedDate.Value, Status = status, Notes = txtNotes.Text, Net = net, Vat = vat, Disc = discTotal, Total = total, UserId = GetUserId() }, tran);
                }

                foreach (DataGridViewRow r in dgvLines.Rows)
                {
                    if (r.IsNewRow) continue;
                    int? itemId = r.Cells["ItemId"].Value != null ? Convert.ToInt32(r.Cells["ItemId"].Value) : (int?)null;
                    if (!itemId.HasValue) continue;
                    decimal qty = Dec(r, "Quantity"), price = Dec(r, "UnitPrice"), disc = Dec(r, "Discount"), vatR = Dec(r, "VatRate");
                    decimal gross = qty * price, discVal = gross * disc / 100, n = gross - discVal, v = n * vatR / 100;

                    conn.Execute(@"INSERT INTO PurchaseOrderLines (OrderId, ItemId, Description, Quantity, Uom, UnitPrice, Discount, NetValue, Vat, Total, WarehouseId, ReceivedQty, InvoicedQty, CreatedAt, UpdatedAt) VALUES (@Oid, @ItemId, @Desc, @Qty, @Uom, @Price, @Disc, @Net, @Vat, @Total, @Wh, 0, 0, GETDATE(), GETDATE())",
                        new { Oid = orderId, ItemId = itemId, Desc = r.Cells["Description"].Value?.ToString() ?? "", Qty = qty, Uom = r.Cells["Uom"].Value?.ToString() ?? "τεμ", Price = price, Disc = disc, Net = n, Vat = v, Total = n + v, Wh = whId }, tran);
                }

                tran.Commit();
                _orderId = orderId;
                cmbStatus.SelectedValue = status; OnStatusChanged();
                MessageBox.Show(isConfirm ? "✅ Η παραγγελία επιβεβαιώθηκε!" : "💾 Η παραγγελία αποθηκεύτηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ConfirmOrder()
        {
            if (MessageBox.Show("Επιβεβαίωση παραγγελίας προς προμηθευτή;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SaveOrder(true);
        }

        // ============================================
        // 📦 ΜΕΤΑΤΡΟΠΗ ΣΕ ΠΑΡΑΛΑΒΗ
        // ============================================
        private void ConvertToGoodsReceipt()
        {
            if (!_orderId.HasValue) { MessageBox.Show("Αποθήκευσε πρώτα την παραγγελία!"); return; }
            try
            {
                using (var frm = new GoodsReceiptForm())
                {
                    frm.PreSelectedPurchaseOrderId = _orderId.Value;
                    frm.ShowDialog();
                }
                LoadOrder(_orderId.Value);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        // ============================================
        // 🛒 ΜΕΤΑΤΡΟΠΗ ΣΕ ΤΙΜΟΛΟΓΙΟ ΑΓΟΡΑΣ
        // ✅ ΠΛΗΡΗΣ ΛΕΙΤΟΥΡΓΙΑ ΜΕ PRE-FILL
        // ============================================
        private void ConvertToInvoice()
        {
            if (!_orderId.HasValue)
            {
                MessageBox.Show("Αποθήκευσε πρώτα την παραγγελία!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                "Μετατροπή παραγγελίας σε Τιμολόγιο Αγοράς;\n\nΘα ανοίξει νέο τιμολόγιο με τα είδη της παραγγελίας.",
                "Επιβεβαίωση",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using (var invForm = new PurchaseInvoiceForm())
                {
                    invForm.PreFillFromOrder(_orderId.Value);
                    invForm.ShowDialog();

                    if (invForm.SavedInvoiceId > 0)
                    {
                        using var conn = new SqlConnection(GetConnectionString());

                        // 1) Σύνδεση Παραγγελία -> Τιμολόγιο
                        conn.Execute(@"
                    INSERT INTO DocumentRelations
                    (SourceDocumentId, TargetDocumentId, RelationType, CreatedAt)
                    VALUES
                    (@Src, @Tgt, 'PurchaseOrderToInvoice', GETDATE())",
                            new { Src = _orderId, Tgt = invForm.SavedInvoiceId });

                        // 2) Ενημέρωση InvoicedQty στις γραμμές της παραγγελίας
                        conn.Execute(@"
                    UPDATE pol
                    SET pol.InvoicedQty = ISNULL(pol.InvoicedQty, 0) + x.Qty
                    FROM PurchaseOrderLines pol
                    INNER JOIN
                    (
                        SELECT ItemId, SUM(Qty) AS Qty
                        FROM PurchaseInvoiceItems
                        WHERE PurchaseId = @InvId
                        GROUP BY ItemId
                    ) x ON x.ItemId = pol.ItemId
                    WHERE pol.OrderId = @PoId",
                            new { InvId = invForm.SavedInvoiceId, PoId = _orderId });

                        // 3) Κλείσιμο παραγγελίας ως τιμολογημένη
                        conn.Execute(
                            "UPDATE PurchaseOrders SET Status = 'INVOICED', UpdatedAt = GETDATE() WHERE Id = @Id",
                            new { Id = _orderId });

                        cmbStatus.SelectedValue = "INVOICED";
                        OnStatusChanged();

                        MessageBox.Show(
                            $"✅ Η παραγγελία μετατράπηκε σε τιμολόγιο!\n\nΤιμολόγιο Id: {invForm.SavedInvoiceId}",
                            "Επιτυχία",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Δεν αποθηκεύτηκε νέο τιμολόγιο.\nΗ παραγγελία παρέμεινε στην προηγούμενη κατάστασή της.",
                            "Πληροφορία",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα μετατροπής:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================
        // LOAD ORDER
        // ============================================
        private void LoadOrder(int id)
        {
            _isLoading = true;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var o = conn.QueryFirstOrDefault("SELECT * FROM PurchaseOrders WHERE Id = @Id", new { Id = id });
                if (o == null) return;

                txtOrderNumber.Text = o.OrderNumber ?? o.DocumentNumber ?? "";
                dtpOrderDate.Value = o.OrderDate ?? DateTime.Now;
                if (o.ExpectedDeliveryDate != null) dtpExpectedDate.Value = o.ExpectedDeliveryDate;
                cmbSupplier.SelectedValue = o.SupplierId;
                if (o.WarehouseId != null) cmbWarehouse.SelectedValue = o.WarehouseId;
                if (o.PaymentTermsId != null) cmbPaymentTerms.SelectedValue = o.PaymentTermsId;
                if (!string.IsNullOrEmpty(o.Currency)) cmbCurrency.Text = o.Currency;
                cmbStatus.SelectedValue = o.Status ?? "NEW";
                txtNotes.Text = o.Notes ?? "";
                LoadSupplierDetails();

                var lines = conn.Query(@"SELECT ROW_NUMBER() OVER (ORDER BY pol.Id) AS LineNum, pol.Id AS LineId, pol.ItemId, i.Code AS ItemCode, pol.Description, pol.Quantity, pol.Uom, pol.UnitPrice, pol.Discount, pol.NetValue, pol.Vat, pol.Total, ISNULL(pol.ReceivedQty,0) AS ReceivedQty, ISNULL(i.VatRate,24) AS VatRate, ISNULL(i.Stock,0) AS StockAvail FROM PurchaseOrderLines pol LEFT JOIN Items i ON pol.ItemId = i.Id WHERE pol.OrderId = @Id ORDER BY pol.Id", new { Id = id }).ToList();

                dgvLines.Rows.Clear();
                foreach (var l in lines)
                    dgvLines.Rows.Add(l.LineNum, l.ItemId, l.ItemCode, l.Description, l.Quantity, l.Uom, l.UnitPrice, l.Discount, l.NetValue, l.VatRate, l.Vat, l.Total, l.ReceivedQty, l.StockAvail);

                CalculateTotals(); OnStatusChanged();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message); }
            _isLoading = false;
        }

        // ✅ ΑΦΑΙΡΕΘΗΚΕ: public int SavedInvoiceId { get; private set; } (δεν χρειαζόταν εδώ)
        // ============================================
        // ✅ PRE-FILL ΑΠΟ ΠΡΟΣΦΟΡΑ ΑΓΟΡΑΣ
        // ============================================
        private void ApplyPurchaseQuotationPreFill()
        {
            if (_preQuotation == null) return;
            try
            {
                dynamic q = _preQuotation;

                // Προμηθευτής
                if (q.SupplierId != null) cmbSupplier.SelectedValue = (int)q.SupplierId;

                // Ημερομηνία αναμενόμενης παραλαβής = ValidUntil
                if (q.ValidUntil != null) dtpExpectedDate.Value = (DateTime)q.ValidUntil;

                // Νόμισμα
                if (!string.IsNullOrEmpty((string)q.Currency)) cmbCurrency.Text = q.Currency;

                // Σημείωση προέλευσης
                string qNum = q.QuotationNumber?.ToString() ?? "";
                if (!string.IsNullOrWhiteSpace(qNum))
                    txtNotes.Text = $"[Από προσφορά αγοράς {qNum}] {txtNotes.Text ?? ""}".Trim();

                // Γραμμές
                if (_preQuotationLines != null && _preQuotationLines.Count > 0)
                {
                    dgvLines.Rows.Clear();
                    int ln = 1;
                    foreach (dynamic l in _preQuotationLines)
                    {
                        string code = l.ItemCode?.ToString() ?? "";
                        if (string.IsNullOrWhiteSpace(code)) continue;

                        decimal qty = Dec2(l.Quantity), price = Dec2(l.UnitPrice), disc = Dec2(l.Discount);
                        decimal vatR = Dec2(l.VatRate);
                        if (vatR == 0) vatR = 24;

                        decimal net = Math.Round(qty * price * (1 - disc / 100), 2);
                        decimal vat = Math.Round(net * vatR / 100, 2);

                        dgvLines.Rows.Add(ln++, l.ItemId, code, l.Description?.ToString() ?? "", qty, l.Uom?.ToString() ?? "τεμ", price, disc,
                            net, vatR, vat, Math.Round(net + vat, 2), 0, 0);
                    }
                    CalculateTotals();
                }
            }
            catch (Exception ex) { MessageBox.Show("Προσοχή prefill:\n" + ex.Message); }
        }

        private decimal Dec2(object v) => v == null || v == DBNull.Value ? 0 : (decimal.TryParse(v.ToString(), out var r) ? r : 0);
        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}