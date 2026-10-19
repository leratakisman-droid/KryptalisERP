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
    public class PurchaseQuotationForm : Form
    {
        private ComboBox cmbSupplier, cmbCurrency, cmbStatus;
        private TextBox txtQuotationNumber, txtNotes, txtSupplierAfm, txtSupplierAddress, txtSupplierCity, txtSupplierPhone, txtSupplierEmail, txtValidDays;
        private DateTimePicker dtpQuotationDate, dtpValidUntil;
        private DataGridView dgvLines;
        private Label lblTotalNet, lblTotalVat, lblGrandTotal, lblExpiryWarning, lblDaysLeft;
        private Button btnSave, btnConvertToOrder, btnPrint, btnClose;

        private int? _quotationId;
        private bool _isLoading = false;

        public static readonly Dictionary<string, string> StatusMap = new Dictionary<string, string>
        {
            { "DRAFT", "📝 Πρόχειρο" },
            { "SENT", "📤 Εστάλη" },
            { "ACCEPTED", "✅ Αποδέχτηκε" },
            { "REJECTED", "❌ Απορρίφθηκε" },
            { "EXPIRED", "⏰ Έληξε" },
            { "CONVERTED", "🏭 Μετατράπηκε σε ΠΑ" }
        };

        public PurchaseQuotationForm(int? quotationId = null)
        {
            _quotationId = quotationId;
            InitializeUI();
            this.Load += PurchaseQuotationForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "🏭 Προσφορά Αγοράς (RFQ)";
            this.Size = new Size(1450, 850);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1150, 680);

            // --- 1. ΒΑΣΙΚΑ ---
            var pnlBasic = CreateHeaderPanel("📄 ΒΑΣΙΚΑ ΣΤΟΙΧΕΙΑ ΠΡΟΣΦΟΡΑΣ ΑΓΟΡΑΣ", 85, Color.FromArgb(230, 126, 34));
            int y = 22, lx = 10, lw = 85, gap = 10;

            txtQuotationNumber = AddText(pnlBasic, "Αριθμός:", lx, y, lw, 140, true);
            lx += lw + 140 + gap;
            dtpQuotationDate = AddDate(pnlBasic, "Ημ/νία:", lx, y, lw, 120);
            lx += lw + 120 + gap;
            txtValidDays = AddText(pnlBasic, "Ημέρες Ισχύος:", lx, y, lw + 15, 60, false);
            txtValidDays.Text = "30";
            txtValidDays.TextAlign = HorizontalAlignment.Center;
            txtValidDays.TextChanged += (s, e) => RecalcValidUntil();
            lx += lw + 15 + 60 + gap;
            dtpValidUntil = AddDate(pnlBasic, "Ισχύει έως:", lx, y, lw + 5, 120);
            dtpValidUntil.ValueChanged += (s, e) => UpdateExpiryWarning();
            lx += lw + 5 + 120 + gap;
            cmbStatus = AddCombo(pnlBasic, "Κατάσταση:", lx, y, lw, 180);
            cmbStatus.SelectedIndexChanged += (s, e) => OnStatusChanged();

            y = 52; lx = 10;
            cmbCurrency = AddCombo(pnlBasic, "Νόμισμα:", lx, y, lw, 100);
            cmbCurrency.Items.AddRange(new object[] { "EUR", "USD", "GBP" });
            cmbCurrency.SelectedIndex = 0;
            this.Controls.Add(pnlBasic);

            // --- 2. ΠΡΟΜΗΘΕΥΤΗΣ ---
            var pnlSupplier = CreateHeaderPanel("🏭 ΠΡΟΜΗΘΕΥΤΗΣ", 85, Color.FromArgb(230, 126, 34));
            y = 22; lx = 10;
            cmbSupplier = AddCombo(pnlSupplier, "Προμηθευτής:", lx, y, lw + 10, 280);
            cmbSupplier.SelectedIndexChanged += (s, e) => LoadSupplierDetails();
            lx += lw + 10 + 280 + gap;
            txtSupplierAfm = AddText(pnlSupplier, "ΑΦΜ:", lx, y, 45, 100, true);
            lx += 45 + 100 + gap;
            txtSupplierPhone = AddText(pnlSupplier, "Τηλ.:", lx, y, 45, 120, false);
            lx += 45 + 120 + gap;
            txtSupplierEmail = AddText(pnlSupplier, "Email:", lx, y, 50, 180, false);

            y = 52; lx = 10;
            txtSupplierAddress = AddText(pnlSupplier, "Διεύθυνση:", lx, y, lw, 280, false);
            lx += lw + 280 + gap;
            txtSupplierCity = AddText(pnlSupplier, "Πόλη:", lx, y, 45, 130, false);
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

            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "LineNumber", HeaderText = "Α/Α", Width = 40, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemId", HeaderText = "ItemId", Width = 50, Visible = false });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", HeaderText = "Κωδικός", Width = 100 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Περιγραφή", Width = 300 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Ποσότητα", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Uom", HeaderText = "ΜΜ", Width = 50 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitPrice", HeaderText = "Κόστος €", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Discount", HeaderText = "Έκτ.%", Width = 55, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "NetValue", HeaderText = "Καθαρή €", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "VatRate", HeaderText = "ΦΠΑ%", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Vat", HeaderText = "ΦΠΑ €", Width = 75, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Σύνολο €", Width = 95, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "LineNotes", HeaderText = "Σημειώσεις", Width = 180 });

            dgvLines.CellValueChanged += DgvLines_CellValueChanged;
            dgvLines.RowsAdded += (s, e) => { for (int i = 0; i < e.RowCount; i++) dgvLines.Rows[e.RowIndex + i].Cells["LineNumber"].Value = e.RowIndex + i + 1; };
            dgvLines.UserDeletingRow += (s, e) => CalculateTotals();

            // --- 4. ΣΗΜΕΙΩΣΕΙΣ ---
            var pnlNotes = CreateHeaderPanel("📝 ΓΕΝΙΚΕΣ ΣΗΜΕΙΩΣΕΙΣ / ΟΡΟΙ", 75, Color.FromArgb(108, 117, 125));
            txtNotes = new TextBox
            {
                Location = new Point(110, 8),
                Size = new Size(1250, 58),
                Font = new Font("Segoe UI", 9.5F),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            txtNotes.Text = "Ζητούμενη προσφορά για τα παρακάτω είδη. Παρακαλώ αναφέρετε χρόνους παράδοσης και όρους πληρωμής.";
            pnlNotes.Controls.Add(txtNotes);

            // --- 5. FOOTER ---
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 85, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(10, 8, 10, 8) };

            btnSave = CreateButton("💾 Αποθήκευση", 10, 15, 130, 40, Color.FromArgb(13, 110, 253));
            btnSave.Click += (s, e) => SaveQuotation();
            pnlFooter.Controls.Add(btnSave);

            btnConvertToOrder = CreateButton("🏭 → Παραγγελία", 150, 15, 150, 40, Color.FromArgb(25, 135, 84));
            btnConvertToOrder.Click += (s, e) => ConvertToPurchaseOrder();
            pnlFooter.Controls.Add(btnConvertToOrder);

            btnPrint = CreateButton("🖨️ Εκτύπωση", 310, 15, 120, 40, Color.FromArgb(155, 89, 182));
            btnPrint.Click += (s, e) => MessageBox.Show("Εκτύπωση προσφοράς - Υπό υλοποίηση", "Πληροφορία");
            pnlFooter.Controls.Add(btnPrint);

            btnClose = CreateButton("❌ Κλείσιμο", 440, 15, 110, 40, Color.FromArgb(108, 117, 125));
            btnClose.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnClose);

            var pnlExpiry = new Panel { Location = new Point(570, 5), Size = new Size(280, 72), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            lblExpiryWarning = new Label { Text = "⏰ ΗΜΕΡΕΣ ΙΣΧΥΟΣ", Location = new Point(10, 5), AutoSize = true, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = Color.FromArgb(108, 117, 125) };
            pnlExpiry.Controls.Add(lblExpiryWarning);
            lblDaysLeft = new Label { Text = "—", Location = new Point(10, 25), AutoSize = true, Font = new Font("Segoe UI", 22F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) };
            pnlExpiry.Controls.Add(lblDaysLeft);
            pnlFooter.Controls.Add(pnlExpiry);

            var pnlTotals = new Panel { Location = new Point(870, 5), Size = new Size(550, 72), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            int tx = 8, ty = 6, tw = 170, th = 20;
            lblTotalNet = CreateTotalLabel("Καθαρή Αξία:", tx, ty, tw, th, Color.FromArgb(52, 152, 219)); pnlTotals.Controls.Add(lblTotalNet);
            lblTotalVat = CreateTotalLabel("ΦΠΑ:", tx + tw + 5, ty, tw, th, Color.FromArgb(241, 196, 15)); pnlTotals.Controls.Add(lblTotalVat);
            lblGrandTotal = CreateTotalLabel("ΣΥΝΟΛΟ ΠΡΟΣΦΟΡΑΣ:", tx, ty + th + 4, tw + 50, th + 4, Color.FromArgb(46, 204, 113), true); pnlTotals.Controls.Add(lblGrandTotal);
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
            => new Label { Text = $"{text} 0,00 €", Location = new Point(x, y), Size = new Size(w, h), Font = new Font("Segoe UI", bold ? 11F : 9F, bold ? FontStyle.Bold : FontStyle.Regular), ForeColor = color, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(4, 2, 4, 2) };
        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
            => new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };

        // ============================================
        // LOAD
        // ============================================
        private void PurchaseQuotationForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            LoadStatuses();
            LoadSuppliers();
            GenerateNumber();
            RecalcValidUntil();
            _isLoading = false;

            if (_quotationId.HasValue) LoadQuotation(_quotationId.Value);
            else UpdateExpiryWarning();
        }

        private void LoadStatuses()
        {
            cmbStatus.DataSource = new BindingList<KeyValuePair<string, string>>(StatusMap.ToList());
            cmbStatus.DisplayMember = "Value";
            cmbStatus.ValueMember = "Key";
            cmbStatus.SelectedValue = "DRAFT";
        }

        private void LoadSuppliers()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var s = conn.Query("SELECT Id, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName").ToList();
                cmbSupplier.DataSource = s;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "Id";
            }
            catch { }
        }

        private void LoadSupplierDetails()
        {
            if (_isLoading || cmbSupplier.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var s = conn.QueryFirstOrDefault("SELECT Afm, Address, City, Phone, Email FROM Suppliers WHERE Id = @Id", new { Id = Convert.ToInt32(cmbSupplier.SelectedValue) });
                if (s != null)
                {
                    txtSupplierAfm.Text = s.Afm ?? "";
                    txtSupplierAddress.Text = s.Address ?? "";
                    txtSupplierCity.Text = s.City ?? "";
                    txtSupplierPhone.Text = s.Phone ?? "";
                    txtSupplierEmail.Text = s.Email ?? "";
                }
            }
            catch { }
        }

        private void GenerateNumber()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                int next = conn.ExecuteScalar<int>("SELECT ISNULL(MAX(Id), 0) + 1 FROM Quotations");
                txtQuotationNumber.Text = $"RFO-{DateTime.Now:yyyy}-{next:D4}";
            }
            catch { }
        }

        // ============================================
        // ValidUntil LOGIC
        // ============================================
        private void RecalcValidUntil()
        {
            if (_isLoading) return;
            int days = int.TryParse(txtValidDays.Text, out var d) && d > 0 ? d : 30;
            dtpValidUntil.Value = dtpQuotationDate.Value.AddDays(days);
            UpdateExpiryWarning();
        }

        private void UpdateExpiryWarning()
        {
            if (dtpValidUntil == null) return;
            TimeSpan diff = dtpValidUntil.Value.Date - DateTime.Today;
            int daysLeft = diff.Days;

            if (daysLeft > 7) { lblDaysLeft.Text = $"{daysLeft} ημέρες"; lblDaysLeft.ForeColor = Color.FromArgb(46, 204, 113); lblExpiryWarning.Text = "✅ ΕΝ ΙΣΧΥΙ"; lblExpiryWarning.ForeColor = Color.FromArgb(46, 204, 113); }
            else if (daysLeft > 0) { lblDaysLeft.Text = $"{daysLeft} ημέρες"; lblDaysLeft.ForeColor = Color.FromArgb(241, 196, 15); lblExpiryWarning.Text = "⚠️ ΛΗΓΕΙ ΣΥΝΤΟΜΑ"; lblExpiryWarning.ForeColor = Color.FromArgb(241, 196, 15); }
            else if (daysLeft == 0) { lblDaysLeft.Text = "ΣΗΜΕΡΑ!"; lblDaysLeft.ForeColor = Color.FromArgb(230, 126, 34); lblExpiryWarning.Text = "⏰ ΛΗΓΕΙ ΣΗΜΕΡΑ"; lblExpiryWarning.ForeColor = Color.FromArgb(230, 126, 34); }
            else
            {
                lblDaysLeft.Text = $"{-daysLeft} ημέρες"; lblDaysLeft.ForeColor = Color.FromArgb(231, 76, 60); lblExpiryWarning.Text = "❌ ΕΛΗΞΕ"; lblExpiryWarning.ForeColor = Color.FromArgb(231, 76, 60);
                if (!_isLoading && cmbStatus.SelectedValue?.ToString() == "SENT") { _isLoading = true; cmbStatus.SelectedValue = "EXPIRED"; OnStatusChanged(); _isLoading = false; }
            }
        }

        private void OnStatusChanged()
        {
            if (_isLoading) return;
            string status = cmbStatus.SelectedValue?.ToString();
            bool locked = status == "CONVERTED" || status == "REJECTED";
            dgvLines.ReadOnly = locked; dgvLines.AllowUserToAddRows = !locked; dgvLines.AllowUserToDeleteRows = !locked;
            btnSave.Enabled = !locked;
            btnConvertToOrder.Enabled = status == "ACCEPTED" || status == "SENT";
        }

        // ============================================
        // GRID LOGIC (PurchasePrice αντί SalePrice)
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
                        var item = conn.QueryFirstOrDefault(@"SELECT i.Id, i.Name, i.Unit, i.PurchasePrice, i.VatRate FROM Items i WHERE i.Code = @Code", new { Code = code });
                        if (item != null)
                        {
                            row.Cells["ItemId"].Value = item.Id;
                            row.Cells["Description"].Value = item.Name;
                            row.Cells["Uom"].Value = item.Unit;
                            if (row.Cells["UnitPrice"].Value == null) row.Cells["UnitPrice"].Value = item.PurchasePrice;
                            if (row.Cells["VatRate"].Value == null) row.Cells["VatRate"].Value = item.VatRate ?? 24;
                        }
                    }
                    catch { }
                }
            }

            if (col == "Quantity" || col == "UnitPrice" || col == "Discount" || col == "VatRate") { CalcRow(row); CalculateTotals(); }
        }

        private void CalcRow(DataGridViewRow row)
        {
            decimal qty = Dec(row, "Quantity"), price = Dec(row, "UnitPrice"), disc = Dec(row, "Discount"), vatR = Dec(row, "VatRate");
            decimal gross = qty * price, discVal = gross * disc / 100, net = gross - discVal, vatVal = net * vatR / 100;
            row.Cells["NetValue"].Value = Math.Round(net, 2);
            row.Cells["Vat"].Value = Math.Round(vatVal, 2);
            row.Cells["Total"].Value = Math.Round(net + vatVal, 2);
        }
        private decimal Dec(DataGridViewRow row, string col) => decimal.TryParse(row.Cells[col].Value?.ToString(), out var v) ? v : 0;

        private void CalculateTotals()
        {
            decimal net = 0, vat = 0;
            foreach (DataGridViewRow r in dgvLines.Rows) { if (r.IsNewRow) continue; net += Dec(r, "NetValue"); vat += Dec(r, "Vat"); }
            lblTotalNet.Text = $"Καθαρή Αξία: {net:N2} €"; lblTotalVat.Text = $"ΦΠΑ: {vat:N2} €"; lblGrandTotal.Text = $"ΣΥΝΟΛΟ ΠΡΟΣΦΟΡΑΣ: {net + vat:N2} €";
        }

        // ============================================
        // SAVE (✅ QuotationType='PURCHASE', SupplierId)
        // ============================================
        private void SaveQuotation()
        {
            if (cmbSupplier.SelectedValue == null) { MessageBox.Show("Επίλεξε Προμηθευτή!"); return; }
            if (dgvLines.Rows.Count == 1 && dgvLines.Rows[0].IsNewRow) { MessageBox.Show("Πρόσθεσε είδη!"); return; }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                int supplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
                string status = cmbStatus.SelectedValue?.ToString() ?? "DRAFT";

                decimal net = 0, vat = 0;
                foreach (DataGridViewRow r in dgvLines.Rows) { if (r.IsNewRow) continue; net += Dec(r, "NetValue"); vat += Dec(r, "Vat"); }
                decimal total = net + vat;

                int quotationId;
                if (_quotationId.HasValue)
                {
                    conn.Execute(@"UPDATE Quotations SET SupplierId=@Sup, CustomerId=NULL, QuotationType='PURCHASE', QuotationNumber=@Num, QuotationDate=@QDate, ValidUntil=@Valid, Currency=@Cur, Status=@Status, Notes=@Notes, TotalNet=@Net, TotalVat=@Vat, Total=@Total, UpdatedAt=GETDATE() WHERE Id=@Id",
                        new { Id = _quotationId, Sup = supplierId, Num = txtQuotationNumber.Text, QDate = dtpQuotationDate.Value, Valid = dtpValidUntil.Value, Cur = cmbCurrency.Text, Status = status, Notes = txtNotes.Text, Net = net, Vat = vat, Total = total }, tran);
                    quotationId = _quotationId.Value;
                    conn.Execute("DELETE FROM QuotationLines WHERE QuotationId=@Id", new { Id = quotationId }, tran);
                }
                else
                {
                    quotationId = conn.ExecuteScalar<int>(@"INSERT INTO Quotations (CustomerId, SupplierId, QuotationType, QuotationNumber, QuotationDate, ValidUntil, Currency, Status, Notes, TotalNet, TotalVat, Total, CreatedAt, UpdatedAt) VALUES (NULL, @Sup, 'PURCHASE', @Num, @QDate, @Valid, @Cur, @Status, @Notes, @Net, @Vat, @Total, GETDATE(), GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { Sup = supplierId, Num = txtQuotationNumber.Text, QDate = dtpQuotationDate.Value, Valid = dtpValidUntil.Value, Cur = cmbCurrency.Text, Status = status, Notes = txtNotes.Text, Net = net, Vat = vat, Total = total }, tran);
                }

                foreach (DataGridViewRow r in dgvLines.Rows)
                {
                    if (r.IsNewRow) continue;
                    int? itemId = r.Cells["ItemId"].Value != null ? Convert.ToInt32(r.Cells["ItemId"].Value) : (int?)null;
                    if (!itemId.HasValue) continue;

                    decimal qty = Dec(r, "Quantity"), price = Dec(r, "UnitPrice"), disc = Dec(r, "Discount"), vatR = Dec(r, "VatRate");
                    decimal gross = qty * price, discVal = gross * disc / 100, n = gross - discVal, v = n * vatR / 100;

                    conn.Execute(@"INSERT INTO QuotationLines (QuotationId, LineNumber, ItemId, Description, Quantity, Uom, UnitPrice, Discount, NetValue, VatRate, Vat, Total, Notes, CreatedAt, UpdatedAt) VALUES (@Qid, @Ln, @ItemId, @Desc, @Qty, @Uom, @Price, @Disc, @Net, @VatRate, @Vat, @Total, @Notes, GETDATE(), GETDATE())",
                        new { Qid = quotationId, Ln = Convert.ToInt32(r.Cells["LineNumber"].Value ?? 0), ItemId = itemId, Desc = r.Cells["Description"].Value?.ToString() ?? "", Qty = qty, Uom = r.Cells["Uom"].Value?.ToString() ?? "τεμ", Price = price, Disc = disc, Net = Math.Round(n, 2), VatRate = vatR, Vat = Math.Round(v, 2), Total = Math.Round(n + v, 2), Notes = r.Cells["LineNotes"].Value?.ToString() ?? "" }, tran);
                }

                tran.Commit();
                _quotationId = quotationId;
                MessageBox.Show($"✅ Η προσφορά αγοράς αποθηκεύτηκε!\n\nΑριθμός: {txtQuotationNumber.Text}\nΙσχύει έως: {dtpValidUntil.Value:dd/MM/yyyy}", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ============================================
        // 🏭 ΜΕΤΑΤΡΟΠΗ ΣΕ ΠΑΡΑΓΓΕΛΙΑ ΠΡΟΜΗΘΕΥΤΗ
        // ============================================
        private void ConvertToPurchaseOrder()
        {
            if (!_quotationId.HasValue) { MessageBox.Show("Αποθήκευσε πρώτα την προσφορά!"); return; }

            if (dtpValidUntil.Value.Date < DateTime.Today)
            {
                if (MessageBox.Show("Η προσφορά ΕΧΕΙ ΛΗΞΕΙ!\n\nΘέλετε να προχωρήσετε ούτως ή άλλως;", "Προσοχή - Λήξη", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }

            if (MessageBox.Show("Μετατροπή προσφοράς αγοράς σε Παραγγελία Προμηθευτή;\n\nΘα δημιουργηθεί νέα παραγγελία με τα ίδια είδη.", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var q = conn.QueryFirstOrDefault("SELECT * FROM Quotations WHERE Id = @Id", new { Id = _quotationId });
                var lines = conn.Query(@"SELECT ql.*, i.Code AS ItemCode FROM QuotationLines ql LEFT JOIN Items i ON ql.ItemId = i.Id WHERE ql.QuotationId = @Id ORDER BY ql.LineNumber", new { Id = _quotationId }).ToList();

                using (var orderForm = new PurchaseOrderForm())
                {
                    orderForm.PreFillFromPurchaseQuotation(q, lines);
                    orderForm.ShowDialog();

                    if (orderForm.SavedOrderId > 0)
                    {
                        conn.Execute(@"INSERT INTO DocumentRelations (SourceDocumentId, TargetDocumentId, RelationType, CreatedAt) VALUES (@Src, @Tgt, 'PurchaseQuotationToOrder', GETDATE())",
                            new { Src = _quotationId, Tgt = orderForm.SavedOrderId });
                        conn.Execute("UPDATE Quotations SET Status = 'CONVERTED', UpdatedAt = GETDATE() WHERE Id = @Id", new { Id = _quotationId });

                        _isLoading = true;
                        cmbStatus.SelectedValue = "CONVERTED";
                        OnStatusChanged();
                        _isLoading = false;

                        MessageBox.Show($"✅ Η προσφορά μετατράπηκε σε παραγγελία!\n\nΠαραγγελία Id: {orderForm.SavedOrderId}", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα μετατροπής:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ============================================
        // LOAD QUOTATION
        // ============================================
        private void LoadQuotation(int id)
        {
            _isLoading = true;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var q = conn.QueryFirstOrDefault("SELECT * FROM Quotations WHERE Id = @Id", new { Id = id });
                if (q == null) return;

                txtQuotationNumber.Text = q.QuotationNumber ?? "";
                dtpQuotationDate.Value = q.QuotationDate ?? DateTime.Now;
                if (q.ValidUntil != null) dtpValidUntil.Value = q.ValidUntil;
                if (q.SupplierId != null) cmbSupplier.SelectedValue = q.SupplierId;
                if (!string.IsNullOrEmpty(q.Currency)) cmbCurrency.Text = q.Currency;
                cmbStatus.SelectedValue = q.Status ?? "DRAFT";
                txtNotes.Text = q.Notes ?? "";
                LoadSupplierDetails();

                if (q.QuotationDate != null && q.ValidUntil != null)
                {
                    int days = (int)((DateTime)q.ValidUntil - (DateTime)q.QuotationDate).TotalDays;
                    txtValidDays.Text = days > 0 ? days.ToString() : "30";
                }

                var lines = conn.Query(@"SELECT ql.LineNumber, ql.ItemId, i.Code AS ItemCode, ql.Description, ql.Quantity, ql.Uom, ql.UnitPrice, ql.Discount, ql.NetValue, ql.VatRate, ql.Vat, ql.Total, ql.Notes FROM QuotationLines ql LEFT JOIN Items i ON ql.ItemId = i.Id WHERE ql.QuotationId = @Id ORDER BY ql.LineNumber", new { Id = id }).ToList();

                dgvLines.Rows.Clear();
                foreach (var l in lines)
                    dgvLines.Rows.Add(l.LineNumber, l.ItemId, l.ItemCode, l.Description, l.Quantity, l.Uom, l.UnitPrice, l.Discount, l.NetValue, l.VatRate, l.Vat, l.Total, l.Notes ?? "");

                CalculateTotals();
                UpdateExpiryWarning();
                OnStatusChanged();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message); }
            _isLoading = false;
        }

        public int SavedQuotationId => _quotationId ?? 0;
        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}