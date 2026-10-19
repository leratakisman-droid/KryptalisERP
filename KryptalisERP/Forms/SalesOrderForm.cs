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
    public class SalesOrderForm : Form
    {
        private ComboBox cmbCustomer, cmbWarehouse, cmbPaymentTerms, cmbStatus, cmbVatStatus, cmbCurrency;
        private TextBox txtOrderNumber, txtNotes, txtCustomerAfm, txtCustomerAddress, txtCustomerCity, txtCustomerPhone;
        private DateTimePicker dtpOrderDate, dtpDeliveryDate;
        private DataGridView dgvLines;
        private Label lblTotalNet, lblTotalVat, lblGrandTotal, lblStockWarning;
        private Button btnSave, btnConfirm, btnConvertToInvoice, btnCancel;

        private int? _orderId;
        private int _companyId = 1;
        private bool _isLoading = false;

        public static readonly Dictionary<string, string> StatusMap = new Dictionary<string, string>
        {
            { "NEW", "🆕 Νέα" },
            { "CONFIRMED", "✅ Επιβεβαιωμένη" },
            { "PARTIALLY_DELIVERED", "📦 Μερική Παράδοση" },
            { "DELIVERED", "🚚 Παραδόθηκε" },
            { "INVOICED", "🛒 Τιμολογήθηκε" },
            { "CANCELLED", "❌ Ακυρωμένη" }
        };

        public SalesOrderForm(int? orderId = null)
        {
            _orderId = orderId;
            InitializeUI();
            this.Load += SalesOrderForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📋 Παραγγελία Πελάτη";
            this.Size = new Size(1450, 820);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1150, 650);

            // --- 1. ΒΑΣΙΚΑ ---
            var pnlBasic = CreateHeaderPanel("📋 ΒΑΣΙΚΑ ΣΤΟΙΧΕΙΑ ΠΑΡΑΓΓΕΛΙΑΣ", 85, Color.FromArgb(13, 110, 253));
            int y = 22, lx = 10, lw = 85, gap = 10;

            txtOrderNumber = AddText(pnlBasic, "Αριθμός:", lx, y, lw, 140, true);
            lx += lw + 140 + gap;
            dtpOrderDate = AddDate(pnlBasic, "Ημ/νία:", lx, y, lw, 120);
            lx += lw + 120 + gap;
            dtpDeliveryDate = AddDate(pnlBasic, "Παράδοση:", lx, y, lw + 5, 120);
            lx += lw + 5 + 120 + gap;
            cmbStatus = AddCombo(pnlBasic, "Κατάσταση:", lx, y, lw + 5, 170);
            cmbStatus.SelectedIndexChanged += (s, e) => OnStatusChanged();

            y = 52; lx = 10;
            cmbWarehouse = AddCombo(pnlBasic, "Αποθήκη:", lx, y, lw, 150);
            cmbWarehouse.SelectedIndexChanged += (s, e) => RefreshStockColumn();
            lx += lw + 150 + gap;
            cmbPaymentTerms = AddCombo(pnlBasic, "Όροι Πληρ.:", lx, y, lw + 5, 150);
            lx += lw + 5 + 150 + gap;
            cmbCurrency = AddCombo(pnlBasic, "Νόμισμα:", lx, y, lw, 100);
            cmbCurrency.Items.AddRange(new object[] { "EUR", "USD", "GBP" });
            cmbCurrency.SelectedIndex = 0;
            this.Controls.Add(pnlBasic);

            // --- 2. ΠΕΛΑΤΗΣ ---
            var pnlCustomer = CreateHeaderPanel("👤 ΠΕΛΑΤΗΣ", 85, Color.FromArgb(13, 110, 253));
            y = 22; lx = 10;
            cmbCustomer = AddCombo(pnlCustomer, "Πελάτης:", lx, y, lw, 280);
            cmbCustomer.SelectedIndexChanged += (s, e) => LoadCustomerDetails();
            lx += lw + 280 + gap;
            txtCustomerAfm = AddText(pnlCustomer, "ΑΦΜ:", lx, y, 45, 100, true);
            lx += 45 + 100 + gap;
            txtCustomerPhone = AddText(pnlCustomer, "Τηλ.:", lx, y, 45, 120, false);
            lx += 45 + 120 + gap;
            cmbVatStatus = AddCombo(pnlCustomer, "ΦΠΑ:", lx, y, 45, 150);

            y = 52; lx = 10;
            txtCustomerAddress = AddText(pnlCustomer, "Διεύθυνση:", lx, y, lw, 280, false);
            lx += lw + 280 + gap;
            txtCustomerCity = AddText(pnlCustomer, "Πόλη:", lx, y, 45, 130, false);
            this.Controls.Add(pnlCustomer);

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
                BackColor = Color.FromArgb(13, 110, 253),
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
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitPrice", HeaderText = "Τιμή €", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Discount", HeaderText = "Έκτ.%", Width = 55, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "NetValue", HeaderText = "Καθαρή €", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "VatRate", HeaderText = "ΦΠΑ%", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Vat", HeaderText = "ΦΠΑ €", Width = 75, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Σύνολο €", Width = 95, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "DeliveredQty", HeaderText = "Παραδόθηκε", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(240, 240, 240) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockAvail", HeaderText = "Διαθ. Στοκ", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(240, 240, 240) } });

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

            btnConvertToInvoice = CreateButton("🛒 → Τιμολόγιο", 300, 15, 150, 40, Color.FromArgb(155, 89, 182));
            btnConvertToInvoice.Click += (s, e) => ConvertToInvoice();
            pnlFooter.Controls.Add(btnConvertToInvoice);

            btnCancel = CreateButton("❌ Κλείσιμο", 460, 15, 110, 40, Color.FromArgb(108, 117, 125));
            btnCancel.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnCancel);

            var pnlTotals = new Panel { Location = new Point(600, 5), Size = new Size(820, 65), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            int tx = 8, ty = 6, tw = 190, th = 22;
            lblTotalNet = CreateTotalLabel("Καθαρή Αξία:", tx, ty, tw, th, Color.FromArgb(52, 152, 219)); pnlTotals.Controls.Add(lblTotalNet);
            lblTotalVat = CreateTotalLabel("ΦΠΑ:", tx + tw + 5, ty, tw, th, Color.FromArgb(241, 196, 15)); pnlTotals.Controls.Add(lblTotalVat);
            lblGrandTotal = CreateTotalLabel("ΣΥΝΟΛΟ ΠΑΡΑΓΓΕΛΙΑΣ:", tx, ty + th + 4, tw, th, Color.FromArgb(46, 204, 113), true); pnlTotals.Controls.Add(lblGrandTotal);
            lblStockWarning = new Label { Text = "", Location = new Point(tx + tw + 5, ty + th + 4), Size = new Size(tw, th), Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60), AutoSize = false, TextAlign = ContentAlignment.MiddleLeft };
            pnlTotals.Controls.Add(lblStockWarning);
            pnlFooter.Controls.Add(pnlTotals);

            // ✅ ΣΩΣΤΗ ΣΕΙΡΑ DOCKING
            this.Controls.Add(pnlFooter);
            this.Controls.Add(dgvLines);
            this.Controls.Add(pnlNotes);
            this.Controls.Add(pnlCustomer);
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

        // ============================================
        // LOAD
        // ============================================
        private void SalesOrderForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            LoadStatuses(); LoadCustomers(); LoadWarehouses(); LoadPaymentTerms(); LoadVatStatuses();
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
        private void LoadCustomers()
        {
            try { using var conn = new SqlConnection(GetConnectionString()); var c = conn.Query("SELECT Id, Epwnymia FROM Customers WHERE Energo = 1 ORDER BY Epwnymia").ToList(); cmbCustomer.DataSource = c; cmbCustomer.DisplayMember = "Epwnymia"; cmbCustomer.ValueMember = "Id"; } catch { }
        }
        private void LoadWarehouses()
        {
            try { using var conn = new SqlConnection(GetConnectionString()); var w = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive = 1 ORDER BY Name").ToList(); cmbWarehouse.DataSource = w; cmbWarehouse.DisplayMember = "Name"; cmbWarehouse.ValueMember = "Id"; } catch { }
        }
        private void LoadPaymentTerms()
        {
            try { using var conn = new SqlConnection(GetConnectionString()); var t = conn.Query("SELECT Id, TermName FROM PaymentTerms WHERE IsActive = 1 ORDER BY TermName").ToList(); cmbPaymentTerms.DataSource = t; cmbPaymentTerms.DisplayMember = "TermName"; cmbPaymentTerms.ValueMember = "Id"; } catch { }
        }
        private void LoadVatStatuses()
        {
            cmbVatStatus.Items.AddRange(new object[] { "Κανονικός (24%)", "Μειωμένος (13%)", "Υπερμειωμένος (6%)", "Απαλλασσόμενο" });
            cmbVatStatus.SelectedIndex = 0;
        }
        private void LoadCustomerDetails()
        {
            if (_isLoading || cmbCustomer.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var c = conn.QueryFirstOrDefault("SELECT Afm, Dieuthinsi, Poli, Tilefono FROM Customers WHERE Id = @Id", new { Id = Convert.ToInt32(cmbCustomer.SelectedValue) });
                if (c != null) { txtCustomerAfm.Text = c.Afm ?? ""; txtCustomerAddress.Text = c.Dieuthinsi ?? ""; txtCustomerCity.Text = c.Poli ?? ""; txtCustomerPhone.Text = c.Tilefono ?? ""; }
            }
            catch { }
        }
        private void GenerateNumber()
        {
            try { using var conn = new SqlConnection(GetConnectionString()); int next = conn.ExecuteScalar<int>("SELECT ISNULL(MAX(Id), 0) + 1 FROM SalesOrders"); txtOrderNumber.Text = $"ΠΑΠ-{DateTime.Now:yyyy}-{next:D4}"; } catch { }
        }
        private void OnStatusChanged()
        {
            if (_isLoading) return;
            string status = cmbStatus.SelectedValue?.ToString();
            bool locked = status == "INVOICED" || status == "CANCELLED";
            dgvLines.ReadOnly = locked; dgvLines.AllowUserToAddRows = !locked; dgvLines.AllowUserToDeleteRows = !locked;
            btnSave.Enabled = !locked; btnConfirm.Enabled = status == "NEW";
            btnConvertToInvoice.Enabled = status == "CONFIRMED" || status == "PARTIALLY_DELIVERED" || status == "DELIVERED";
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
                        var item = conn.QueryFirstOrDefault(@"SELECT i.Id, i.Name, i.Unit, i.SalePrice, i.VatRate,
                            ISNULL(i.Stock, 0) AS Avail FROM Items i WHERE i.Code = @Code", new { Code = code });
                        if (item != null)
                        {
                            row.Cells["ItemId"].Value = item.Id;
                            row.Cells["Description"].Value = item.Name;
                            row.Cells["Uom"].Value = item.Unit;
                            row.Cells["StockAvail"].Value = item.Avail;
                            if (row.Cells["UnitPrice"].Value == null) row.Cells["UnitPrice"].Value = item.SalePrice;
                            if (row.Cells["VatRate"].Value == null) row.Cells["VatRate"].Value = item.VatRate ?? 24;
                        }
                    }
                    catch { }
                }
            }
            if (col == "Quantity" || col == "UnitPrice" || col == "Discount" || col == "VatRate")
            { CalcRow(row); CalculateTotals(); CheckStock(); }
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
        private void CheckStock()
        {
            int shortCount = 0;
            foreach (DataGridViewRow r in dgvLines.Rows) { if (r.IsNewRow) continue; if (Dec(r, "Quantity") > Dec(r, "StockAvail") && Dec(r, "StockAvail") >= 0) shortCount++; }
            lblStockWarning.Text = shortCount > 0 ? $"⚠️ Έλλειψη στοκ: {shortCount} είδη" : "✅ Διαθέσιμο στοκ";
            lblStockWarning.ForeColor = shortCount > 0 ? Color.FromArgb(231, 76, 60) : Color.FromArgb(46, 204, 113);
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
            CheckStock();
        }

        // ============================================
        // SAVE (Σωστά πεδία: CustomerId, DeliveryDate, Discount, Vat, DeliveredQty)
        // ============================================
        private void SaveOrder(bool isConfirm)
        {
            if (cmbCustomer.SelectedValue == null) { MessageBox.Show("Επίλεξε Πελάτη!"); return; }
            if (dgvLines.Rows.Count == 1 && dgvLines.Rows[0].IsNewRow) { MessageBox.Show("Πρόσθεσε είδη!"); return; }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                int? whId = cmbWarehouse.SelectedValue != null ? Convert.ToInt32(cmbWarehouse.SelectedValue) : null;
                int? ptId = cmbPaymentTerms.SelectedValue != null ? Convert.ToInt32(cmbPaymentTerms.SelectedValue) : null;
                string status = isConfirm ? "CONFIRMED" : (cmbStatus.SelectedValue?.ToString() ?? "NEW");

                decimal net = 0, vat = 0;
                foreach (DataGridViewRow r in dgvLines.Rows) { if (r.IsNewRow) continue; net += Dec(r, "NetValue"); vat += Dec(r, "Vat"); }
                decimal total = net + vat;

                int orderId;
                if (_orderId.HasValue)
                {
                    conn.Execute(@"UPDATE SalesOrders SET CustomerId=@Cust, WarehouseId=@Wh, PaymentTermsId=@Pt, Currency=@Cur, OrderDate=@ODate, DeliveryDate=@DelDate, Status=@Status, Notes=@Notes, TotalNet=@Net, TotalVat=@Vat, Total=@Total, UpdatedAt=GETDATE() WHERE Id=@Id",
                        new { Id = _orderId, Cust = customerId, Wh = whId, Pt = ptId, Cur = cmbCurrency.Text, ODate = dtpOrderDate.Value, DelDate = dtpDeliveryDate.Value, Status = status, Notes = txtNotes.Text, Net = net, Vat = vat, Total = total }, tran);
                    orderId = _orderId.Value;
                    conn.Execute("DELETE FROM SalesOrderLines WHERE OrderId=@Id", new { Id = orderId }, tran);
                    ReleaseReservations(conn, tran, orderId);
                }
                else
                {
                    orderId = conn.ExecuteScalar<int>(@"INSERT INTO SalesOrders (CompanyId, CustomerId, WarehouseId, PaymentTermsId, Currency, OrderNumber, OrderDate, DeliveryDate, Status, Notes, TotalNet, TotalVat, Total, CreatedAt, UpdatedAt) VALUES (1, @Cust, @Wh, @Pt, @Cur, @Num, @ODate, @DelDate, @Status, @Notes, @Net, @Vat, @Total, GETDATE(), GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { Cust = customerId, Wh = whId, Pt = ptId, Cur = cmbCurrency.Text, Num = txtOrderNumber.Text, ODate = dtpOrderDate.Value, DelDate = dtpDeliveryDate.Value, Status = status, Notes = txtNotes.Text, Net = net, Vat = vat, Total = total }, tran);
                }

                foreach (DataGridViewRow r in dgvLines.Rows)
                {
                    if (r.IsNewRow) continue;
                    int? itemId = r.Cells["ItemId"].Value != null ? Convert.ToInt32(r.Cells["ItemId"].Value) : (int?)null;
                    if (!itemId.HasValue) continue;
                    decimal qty = Dec(r, "Quantity"), price = Dec(r, "UnitPrice"), disc = Dec(r, "Discount"), vatR = Dec(r, "VatRate");
                    decimal gross = qty * price, discVal = gross * disc / 100, n = gross - discVal, v = n * vatR / 100;

                    int lineId = conn.ExecuteScalar<int>(@"INSERT INTO SalesOrderLines (OrderId, ItemId, Description, Quantity, Uom, UnitPrice, Discount, NetValue, Vat, Total, WarehouseId, DeliveredQty, InvoicedQty, CreatedAt, UpdatedAt) VALUES (@Oid, @ItemId, @Desc, @Qty, @Uom, @Price, @Disc, @Net, @Vat, @Total, @Wh, 0, 0, GETDATE(), GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { Oid = orderId, ItemId = itemId, Desc = r.Cells["Description"].Value?.ToString() ?? "", Qty = qty, Uom = r.Cells["Uom"].Value?.ToString() ?? "τεμ", Price = price, Disc = disc, Net = n, Vat = v, Total = n + v, Wh = whId }, tran);

                    if (status == "CONFIRMED" && whId.HasValue)
                        ReserveStock(conn, tran, orderId, lineId, itemId.Value, whId.Value, customerId, qty);
                }

                tran.Commit();
                _orderId = orderId;
                cmbStatus.SelectedValue = status; OnStatusChanged();
                MessageBox.Show(isConfirm ? "✅ Η παραγγελία επιβεβαιώθηκε και το στοκ κρατήθηκε!" : "💾 Η παραγγελία αποθηκεύτηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ReserveStock(SqlConnection conn, SqlTransaction tran, int orderId, int lineId, int itemId, int whId, int customerId, decimal qty)
        {
            try
            {
                int? variantId = conn.ExecuteScalar<int?>("SELECT TOP 1 Id FROM ItemVariants WHERE ItemId = @ItemId", new { ItemId = itemId }, tran);
                conn.Execute(@"INSERT INTO StockReservations (CompanyId, WarehouseId, ItemId, ItemVariantId, DocumentType, DocumentId, DocumentLineId, CustomerId, Quantity, ReservedQty, ReleasedQty, AvailableQty, Status, CreatedAt, UpdatedAt)
                    VALUES (1, @Wh, @ItemId, @Var, 'SalesOrder', @DocId, @LineId, @Cust, @Qty, @Qty, 0, 0, 'Active', GETDATE(), GETDATE())",
                    new { Wh = whId, ItemId = itemId, Var = variantId, DocId = orderId, LineId = lineId, Cust = customerId, Qty = qty }, tran);
            }
            catch { }
        }
        private void ReleaseReservations(SqlConnection conn, SqlTransaction tran, int orderId)
        {
            try { conn.Execute("UPDATE StockReservations SET Status = 'Released', ReleasedQty = Quantity, AvailableQty = 0 WHERE DocumentType = 'SalesOrder' AND DocumentId = @Id AND Status = 'Active'", new { Id = orderId }, tran); } catch { }
        }
        private void ConfirmOrder()
        {
            if (MessageBox.Show("Επιβεβαίωση παραγγελίας;\n\nΘα κρατηθεί το στοκ για τα είδη.", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SaveOrder(true);
        }

        // ============================================
        // ΜΕΤΑΤΡΟΠΗ ΣΕ ΤΙΜΟΛΟΓΙΟ
        // ✅ ΔΙΟΡΘΩΜΕΝΟ - δεν εξαρτάται από PreFillFromOrder/SavedInvoiceId
        // ============================================
        // ============================================
        // ΜΕΤΑΤΡΟΠΗ ΣΕ ΤΙΜΟΛΟΓΙΟ
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
                "Μετατροπή παραγγελίας σε Τιμολόγιο Πώλησης;\n\nΘα ανοίξει νέο τιμολόγιο με τα στοιχεία και τα είδη της παραγγελίας.",
                "Επιβεβαίωση",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                var order = conn.QueryFirstOrDefault(
                    "SELECT * FROM SalesOrders WHERE Id = @Id",
                    new { Id = _orderId });

                var lines = conn.Query(
                    @"SELECT sol.*, i.Code AS ItemCode
              FROM SalesOrderLines sol
              LEFT JOIN Items i ON sol.ItemId = i.Id
              WHERE sol.OrderId = @Id
              ORDER BY sol.Id",
                    new { Id = _orderId }).ToList();

                using (var invForm = new SalesInvoiceForm())
                {
                    invForm.PreFillFromOrder(order, lines);
                    invForm.ShowDialog();

                    if (invForm.SavedInvoiceId > 0)
                    {
                        // Σύνδεση Παραγγελία -> Τιμολόγιο
                        conn.Execute(
                            @"INSERT INTO DocumentRelations (SourceDocumentId, TargetDocumentId, RelationType, CreatedAt)
                      VALUES (@Src, @Tgt, 'OrderToInvoice', GETDATE())",
                            new { Src = _orderId, Tgt = invForm.SavedInvoiceId });

                        // Ενημέρωση κατάστασης παραγγελίας
                        conn.Execute(
                            "UPDATE SalesOrders SET Status = 'INVOICED', UpdatedAt = GETDATE() WHERE Id = @Id",
                            new { Id = _orderId });

                        // Απελευθέρωση κρατήσεων στοκ
                        ReleaseReservations(conn, null, _orderId.Value);

                        cmbStatus.SelectedValue = "INVOICED";
                        OnStatusChanged();

                        MessageBox.Show(
                            "✅ Η παραγγελία μετατράπηκε επιτυχώς σε τιμολόγιο!\n\n" +
                            $"Τιμολόγιο Id: {invForm.SavedInvoiceId}",
                            "Επιτυχία",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Δεν αποθηκεύτηκε νέο τιμολόγιο.\n\nΗ παραγγελία παρέμεινε στην προηγούμενη κατάστασή της.",
                            "Πληροφορία",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Σφάλμα μετατροπής:\n" + ex.Message,
                    "Σφάλμα",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================
        // LOAD ORDER (με ROW_NUMBER για Α/Α, JOIN για ItemCode)
        // ============================================
        private void LoadOrder(int id)
        {
            _isLoading = true;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var o = conn.QueryFirstOrDefault("SELECT * FROM SalesOrders WHERE Id = @Id", new { Id = id });
                if (o == null) return;

                txtOrderNumber.Text = o.OrderNumber ?? "";
                dtpOrderDate.Value = o.OrderDate ?? DateTime.Now;
                if (o.DeliveryDate != null) dtpDeliveryDate.Value = o.DeliveryDate;
                cmbCustomer.SelectedValue = o.CustomerId;
                if (o.WarehouseId != null) cmbWarehouse.SelectedValue = o.WarehouseId;
                if (o.PaymentTermsId != null) cmbPaymentTerms.SelectedValue = o.PaymentTermsId;
                if (!string.IsNullOrEmpty(o.Currency)) cmbCurrency.Text = o.Currency;
                cmbStatus.SelectedValue = o.Status ?? "NEW";
                txtNotes.Text = o.Notes ?? "";
                LoadCustomerDetails();

                var lines = conn.Query(@"SELECT ROW_NUMBER() OVER (ORDER BY sol.Id) AS LineNum, sol.Id, sol.ItemId, i.Code AS ItemCode, sol.Description, sol.Quantity, sol.Uom, sol.UnitPrice, sol.Discount, sol.NetValue, sol.Vat, sol.Total, sol.DeliveredQty, ISNULL((SELECT SUM(sv.AvailableQty) FROM StockVariants sv WHERE sv.ItemVariantId IN (SELECT Id FROM ItemVariants WHERE ItemId = sol.ItemId)), 0) AS StockAvail FROM SalesOrderLines sol LEFT JOIN Items i ON sol.ItemId = i.Id WHERE sol.OrderId = @Id ORDER BY sol.Id", new { Id = id }).ToList();

                dgvLines.Rows.Clear();
                foreach (var l in lines)
                    dgvLines.Rows.Add(l.LineNum, l.Id, l.ItemCode, l.Description, l.Quantity, l.Uom, l.UnitPrice, l.Discount, l.NetValue, 24, l.Vat, l.Total, l.DeliveredQty, l.StockAvail);

                CalculateTotals(); CheckStock(); OnStatusChanged();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message); }
            _isLoading = false;
        }
        // ============================================
        // ✅ PRE-FILL ΑΠΟ ΠΡΟΣΦΟΡΑ
        // ============================================
        private dynamic _preQuotation;
        private List<dynamic> _preQuotationLines;
        public int SavedOrderId => _orderId ?? 0;

        public void PreFillFromQuotation(dynamic quotation, List<dynamic> lines)
        {
            _preQuotation = quotation;
            _preQuotationLines = lines ?? new List<dynamic>();
        }

        // Κάλεσε αυτή τη μέθοδο στο τέλος του SalesOrderForm_Load:
        private void ApplyQuotationPreFill()
        {
            if (_preQuotation == null) return;
            try
            {
                dynamic q = _preQuotation;

                // Πελάτης
                if (q.CustomerId != null) cmbCustomer.SelectedValue = (int)q.CustomerId;

                // Ημερομηνία παράδοσης = ValidUntil της προσφοράς
                if (q.ValidUntil != null) dtpDeliveryDate.Value = (DateTime)q.ValidUntil;

                // Σημείωση προέλευσης
                string qNum = q.QuotationNumber?.ToString() ?? "";
                if (!string.IsNullOrWhiteSpace(qNum))
                    txtNotes.Text = $"[Από προσφορά {qNum}] {txtNotes.Text ?? ""}".Trim();

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

                        dgvLines.Rows.Add(ln++, l.ItemId, code, l.Description?.ToString() ?? "", qty, l.Uom?.ToString() ?? "τεμ", price, disc,
                            Math.Round(qty * price * (1 - disc / 100), 2), vatR,
                            Math.Round(qty * price * (1 - disc / 100) * vatR / 100, 2),
                            Math.Round(qty * price * (1 - disc / 100) * (1 + vatR / 100), 2), 0, 0);
                    }
                    CalculateTotals();
                }
            }
            catch (Exception ex) { MessageBox.Show("Προσοχή prefill:\n" + ex.Message); }
        }

        private decimal Dec2(object v) => v == null || v == DBNull.Value ? 0 : (decimal.TryParse(v.ToString(), out var r) ? r : 0);
        public int SavedInvoiceId { get; private set; }
        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}