using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration; // ✅ FIX: Πρόσθεσε αυτό
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KryptalisERP.Models;      // <-- ΝΕΑ IMPORT
using KryptalisERP.Services;    // <-- ΝΕΑ IMPORT

namespace KryptalisERP
{
    public class PurchaseInvoiceForm : Form
    {
        // UI Controls Declaration
        private ComboBox cmbDocType, cmbSeries, cmbWarehouse, cmbSupplier, cmbPaymentMethod, cmbPaymentTerms, cmbWithholding, cmbVatStatus;
        private TextBox txtDocNumber, txtNotes, txtSupplierAfm, txtSupplierAddress, txtSupplierCity, txtSupplierPhone;
        private TextBox txtWithholdingDetails;
        private DateTimePicker dtpDocDate;
        private DataGridView dgvItems;

        private Label lblTotalBeforeDiscount, lblTotalDiscount, lblTotalAfterDiscount;
        private Label lblTotalVat, lblWithholdingAmount, lblFinalAmount, lblGrandTotal;

        private Button btnSave, btnPost, btnPrint, btnCancel;

        // Logic State
        private int? _invoiceId;
        private int _companyId = 1;
        private int? _prePurchaseOrderId;

        // ✅ SERVICE LAYER INSTANCE
        private readonly DocumentService _docService;
        private readonly string _connStr;

        public int SavedInvoiceId => _invoiceId ?? 0;

        public void PreFillFromOrder(int purchaseOrderId)
        {
            _prePurchaseOrderId = purchaseOrderId;
        }

        public PurchaseInvoiceForm(int? invoiceId = null)
        {
            _invoiceId = invoiceId;

            // Initialize Connection & Service
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";

            _docService = new DocumentService(_connStr);

            InitializeUI();
            this.Load += PurchaseInvoiceForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "🏭 Τιμολόγιο Αγοράς (New Engine)";
            this.Size = new Size(1500, 900);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1200, 700);

            // --- 1. ΒΑΣΙΚΑ ---
            var pnlBasic = CreateHeaderPanel("📋 ΒΑΣΙΚΑ ΣΤΟΙΧΕΙΑ ΑΓΟΡΑΣ", 85);
            int y = 22, lx = 10, lw = 85, cw = 140, gap = 10;

            cmbDocType = AddCombo(pnlBasic, "Τύπος:", lx, y, lw, cw);
            cmbDocType.SelectedIndexChanged += (s, e) => LoadSeries();
            lx += lw + cw + gap;

            cmbSeries = AddCombo(pnlBasic, "Σειρά:", lx, y, lw, 120);
            cmbSeries.SelectedIndexChanged += (s, e) => GenerateNumberPreview();
            lx += lw + 120 + gap;

            txtDocNumber = AddText(pnlBasic, "Αριθμός:", lx, y, lw, 100, true);
            lx += lw + 100 + gap;

            dtpDocDate = AddDate(pnlBasic, "Ημερομηνία:", lx, y, lw, 120);
            lx += lw + 120 + gap;

            cmbWarehouse = AddCombo(pnlBasic, "Αποθήκη:", lx, y, lw, 140);
            lx += lw + 140 + gap;

            cmbVatStatus = AddCombo(pnlBasic, "ΦΠΑ:", lx, y, 45, 140);

            y = 52; lx = 10;
            cmbPaymentMethod = AddCombo(pnlBasic, "Πληρωμή:", lx, y, lw, 140);
            lx += lw + 140 + gap;

            cmbPaymentTerms = AddCombo(pnlBasic, "Όροι Πληρ.:", lx, y, lw + 10, 140);

            this.Controls.Add(pnlBasic);

            // --- 2. ΠΡΟΜΗΘΕΥΤΗΣ ---
            var pnlSupplier = CreateHeaderPanel("🏭 ΣΤΟΙΧΕΙΑ ΠΡΟΜΗΘΕΥΤΗ", 85);
            y = 22; lx = 10;

            cmbSupplier = AddCombo(pnlSupplier, "Προμηθευτής:", lx, y, lw + 10, 280);
            cmbSupplier.SelectedIndexChanged += (s, e) => LoadSupplierDetails();
            lx += lw + 10 + 280 + gap;

            txtSupplierAfm = AddText(pnlSupplier, "ΑΦΜ:", lx, y, 45, 100, true);
            lx += 45 + 100 + gap;

            txtSupplierPhone = AddText(pnlSupplier, "Τηλέφωνο:", lx, y, lw, 110, false);
            lx += lw + 110 + gap;

            txtSupplierCity = AddText(pnlSupplier, "Πόλη:", lx, y, 45, 120, false);

            y = 52; lx = 10;
            txtSupplierAddress = AddText(pnlSupplier, "Διεύθυνση:", lx, y, lw, 400, false);

            this.Controls.Add(pnlSupplier);

            // --- 3. ΠΑΡΑΚΡΑΤΗΣΕΙΣ ---
            var pnlWithholding = CreateHeaderPanel("💰 ΠΑΡΑΚΡΑΤΗΣΕΙΣ", 55);

            cmbWithholding = AddCombo(pnlWithholding, "Παρακράτηση:", 10, 22, lw + 10, 220);
            cmbWithholding.SelectedIndexChanged += (s, e) => CalculateTotals();

            txtWithholdingDetails = AddText(pnlWithholding, "Λεπτομέρειες:", 10 + lw + 10 + 220 + gap, 22, lw + 10, 300, false);

            this.Controls.Add(pnlWithholding);

            // --- 4. ΣΗΜΕΙΩΣΕΙΣ ---
            var pnlNotes = CreateHeaderPanel("📝 ΣΗΜΕΙΩΣΕΙΣ", 45);
            txtNotes = new TextBox
            {
                Location = new Point(110, 8),
                Size = new Size(1300, 28),
                Font = new Font("Segoe UI", 9.5F),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlNotes.Controls.Add(txtNotes);
            this.Controls.Add(pnlNotes);

            // --- 5. GRID ΓΡΑΜΜΩΝ ---
            dgvItems = new DataGridView
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

            dgvItems.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(3)
            };

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "LineNumber", HeaderText = "Α/Α", Width = 40, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", HeaderText = "Κωδικός", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Περιγραφή", Width = 280 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Qty", HeaderText = "Ποσότητα", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Unit", HeaderText = "ΜΜ", Width = 50 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitPrice", HeaderText = "Τιμή €", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiscountPercent", HeaderText = "Έκτ.%", Width = 55, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiscountValue", HeaderText = "Έκτ.€", Width = 70, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.FromArgb(231, 76, 60) } });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "NetValue", HeaderText = "Καθαρή €", Width = 85, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "VatRate", HeaderText = "ΦΠΑ%", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "VatValue", HeaderText = "ΦΠΑ €", Width = 70, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalValue", HeaderText = "Σύνολο €", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemSize", HeaderText = "Μέγεθος", Width = 60 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemColor", HeaderText = "Χρώμα", Width = 60 });

            dgvItems.CellValueChanged += DgvItems_CellValueChanged;
            dgvItems.RowsAdded += DgvItems_RowsAdded;
            dgvItems.UserDeletingRow += (s, e) => CalculateTotals();

            // --- 6. FOOTER ---
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 75,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(10, 8, 10, 8)
            };

            btnSave = CreateButton("💾 Αποθήκευση", 10, 15, 130, 38, Color.FromArgb(13, 110, 253));
            btnSave.Click += async (s, e) => await SaveInvoiceAsync(false);
            pnlFooter.Controls.Add(btnSave);

            btnPost = CreateButton("🚀 Οριστικοποίηση", 150, 15, 150, 38, Color.FromArgb(25, 135, 84));
            btnPost.Click += async (s, e) => await PostInvoiceAsync();
            pnlFooter.Controls.Add(btnPost);

            btnPrint = CreateButton("🖨️ Εκτύπωση", 310, 15, 120, 38, Color.FromArgb(155, 89, 182));
            btnPrint.Click += (s, e) => MessageBox.Show("Εκτύπωση - Υπό υλοποίηση", "Πληροφορία");
            pnlFooter.Controls.Add(btnPrint);

            btnCancel = CreateButton("❌ Ακύρωση", 440, 15, 110, 38, Color.FromArgb(108, 117, 125));
            btnCancel.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnCancel);

            var pnlTotals = new Panel
            {
                Location = new Point(570, 5),
                Size = new Size(900, 65),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            int tx = 8, ty = 4, tw = 165, th = 19;

            lblTotalBeforeDiscount = CreateTotalLabel("Πριν έκπτωση:", tx, ty, tw, th, Color.FromArgb(52, 152, 219));
            pnlTotals.Controls.Add(lblTotalBeforeDiscount);

            lblTotalDiscount = CreateTotalLabel("Έκπτωση:", tx + tw + 5, ty, tw, th, Color.FromArgb(231, 76, 60));
            pnlTotals.Controls.Add(lblTotalDiscount);

            lblTotalAfterDiscount = CreateTotalLabel("Καθαρή Αξία:", tx, ty + th + 2, tw, th, Color.FromArgb(52, 152, 219));
            pnlTotals.Controls.Add(lblTotalAfterDiscount);

            lblTotalVat = CreateTotalLabel("ΦΠΑ:", tx + tw + 5, ty + th + 2, tw, th, Color.FromArgb(241, 196, 15));
            pnlTotals.Controls.Add(lblTotalVat);

            lblWithholdingAmount = CreateTotalLabel("Παρακράτηση:", tx, ty + (th + 2) * 2, tw, th, Color.FromArgb(231, 76, 60));
            pnlTotals.Controls.Add(lblWithholdingAmount);

            lblGrandTotal = CreateTotalLabel("ΓΕΝΙΚΟ ΣΥΝΟΛΟ:", tx + tw + 5, ty + (th + 2) * 2, tw, th, Color.FromArgb(46, 204, 113), true);
            pnlTotals.Controls.Add(lblGrandTotal);

            lblFinalAmount = CreateTotalLabel("ΤΕΛΙΚΟ ΠΛΗΡΩΤΕΟ:", tx, ty + (th + 2) * 3, tw * 2 + 5, th + 2, Color.FromArgb(155, 89, 182), true);
            pnlTotals.Controls.Add(lblFinalAmount);

            pnlFooter.Controls.Add(pnlTotals);

            // ✅ ΣΩΣΤΗ ΣΕΙΡΑ DOCKING
            this.Controls.Add(pnlFooter);
            this.Controls.Add(dgvItems);
            this.Controls.Add(pnlNotes);
            this.Controls.Add(pnlWithholding);
            this.Controls.Add(pnlSupplier);
            this.Controls.Add(pnlBasic);
            dgvItems.BringToFront();
        }

        // ============================================
        // CORE LOGIC: SAVE USING SERVICE
        // ============================================
        private async Task SaveInvoiceAsync(bool isPost)
        {
            if (!ValidateForm()) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnSave.Enabled = false;

                // 1. Build Document Model from UI
                var doc = new Document
                {
                    CompanyId = _companyId,
                    BranchId = null,
                    DocumentTypeId = Convert.ToInt32(cmbDocType.SelectedValue),
                    SeriesId = Convert.ToInt32(cmbSeries.SelectedValue),
                    IssueDate = dtpDocDate.Value.Date,
                    SupplierId = Convert.ToInt32(cmbSupplier.SelectedValue), // ✅ Use SupplierId for Purchases
                    Currency = "EUR",
                    ExchangeRate = 1m,
                    Status = isPost ? "ISSUED" : "DRAFT",
                    Notes = txtNotes.Text,

                    TotalNet = 0,
                    TotalVat = 0,
                    GrandTotal = 0,
                    Lines = new List<DocumentLine>()
                };

                // 2. Map Grid Rows to Lines
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (string.IsNullOrWhiteSpace(row.Cells["ItemCode"].Value?.ToString())) continue;

                    decimal qty = ParseDecimal(row.Cells["Qty"].Value);
                    decimal price = ParseDecimal(row.Cells["UnitPrice"].Value);
                    decimal discount = ParseDecimal(row.Cells["DiscountPercent"].Value);
                    decimal vatRate = ParseDecimal(row.Cells["VatRate"].Value);

                    var line = new DocumentLine
                    {
                        LineNumber = Convert.ToInt32(row.Cells["LineNumber"].Value),
                        ItemId = GetItemIdByCode(row.Cells["ItemCode"].Value.ToString()),
                        Description = row.Cells["Description"].Value?.ToString(),
                        Quantity = qty,
                        UnitPrice = price,
                        DiscountPercent = discount,
                        VatRate = vatRate,

                        UomId = 1, // Default TEM
                        WarehouseId = cmbWarehouse.SelectedValue != null ? Convert.ToInt32(cmbWarehouse.SelectedValue) : null,
                        ItemSize = row.Cells["ItemSize"].Value?.ToString(),
                        ItemColor = row.Cells["ItemColor"].Value?.ToString()
                    };

                    doc.Lines.Add(line);
                }

                // 3. Call Service
                if (_invoiceId.HasValue)
                {
                    throw new NotImplementedException("UPDATE mode requires Repo implementation.");
                }
                else
                {
                    int newId = await _docService.CreateDocumentAsync(doc);
                    _invoiceId = newId;

                    MessageBox.Show($"✅ Παραστατικό αγοράς δημιουργήθηκε!\nID: {newId}", "Επιτυχία");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnSave.Enabled = true;
            }
        }

        // Helper to safely parse decimal from cell value
        private decimal ParseDecimal(object val)
        {
            if (val == null || val == DBNull.Value) return 0;
            if (decimal.TryParse(val.ToString(), out decimal res)) return res;
            return 0;
        }

        private int GetItemIdByCode(string code)
        {
            using var conn = new SqlConnection(_connStr);
            return conn.ExecuteScalar<int>("SELECT TOP 1 Id FROM Items WHERE Code = @Code OR SKU = @Code", new { Code = code });
        }

        // ============================================
        // OTHER METHODS
        // ============================================

        private async Task PostInvoiceAsync()
        {
            if (!_invoiceId.HasValue) { MessageBox.Show("Αποθήκευσε πρώτα!"); return; }
            if (MessageBox.Show("Οριστικοποίηση;\nΘα αυξηθεί το stock.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                // In production, call _docService.PostDocumentAsync(_invoiceId)
                // which handles stock increase logic separately
                MessageBox.Show("✅ Η αγορά οριστικοποιήθηκε (Mock).", "Success");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadSeries()
        {
            if (cmbDocType.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(_connStr);
                var s = conn.Query<DocumentSeries>(@"
                    SELECT * FROM DocumentSeries 
                    WHERE DocumentTypeId = @TypeId AND IsActive = 1 AND Category = 'PURCHASE'",
                    new { TypeId = Convert.ToInt32(cmbDocType.SelectedValue) }).ToList();

                cmbSeries.DataSource = s;
                cmbSeries.DisplayMember = "Name";
                cmbSeries.ValueMember = "Id";
                GenerateNumberPreview();
            }
            catch { }
        }

        private void GenerateNumberPreview()
        {
            if (cmbSeries.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(_connStr);
                var s = conn.QueryFirstOrDefault<DocumentSeries>("SELECT Prefix, NextNumber, NumberLength FROM DocumentSeries WHERE Id = @Id", new { Id = Convert.ToInt32(cmbSeries.SelectedValue) });
                if (s != null)
                {
                    txtDocNumber.Text = $"{s.Prefix}{(s.NextNumber).ToString().PadLeft(s.NumberLength, '0')}";
                }
            }
            catch { }
        }

        private void DgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var row = dgvItems.Rows[e.RowIndex];
            var col = dgvItems.Columns[e.ColumnIndex].Name;

            if (col == "ItemCode" && !row.IsNewRow)
            {
                string code = row.Cells["ItemCode"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(code))
                {
                    try
                    {
                        using var conn = new SqlConnection(_connStr);
                        // Fetch Item data - Note: Using PurchasePrice for purchases
                        var item = conn.QueryFirstOrDefault<Item>("SELECT Name, Unit, PurchasePrice, VatRate, Weight, Color, Size, ItemType FROM Items WHERE Code = @Code", new { Code = code });

                        if (item != null)
                        {
                            row.Cells["Description"].Value = item.Name;
                            row.Cells["Unit"].Value = item.Unit;

                            // Price Logic: Use PurchasePrice
                            if (row.Cells["UnitPrice"].Value == null || string.IsNullOrWhiteSpace(row.Cells["UnitPrice"].Value?.ToString()))
                                row.Cells["UnitPrice"].Value = item.PurchasePrice;

                            // VAT Logic
                            decimal vatFromDb = item.VatRate > 0 ? item.VatRate : 24m;
                            row.Cells["VatRate"].Value = vatFromDb;

                            CalculateRow(row);
                            CalculateTotals();
                        }
                    }
                    catch { }
                }
            }

            if ((col == "Qty" || col == "UnitPrice" || col == "DiscountPercent" || col == "VatRate") && !row.IsNewRow)
            {
                CalculateRow(row);
                CalculateTotals();
            }
        }

        private void CalculateRow(DataGridViewRow row)
        {
            decimal qty = ParseDecimal(row.Cells["Qty"].Value);
            decimal price = ParseDecimal(row.Cells["UnitPrice"].Value);
            decimal discount = ParseDecimal(row.Cells["DiscountPercent"].Value);
            decimal vatRate = ParseDecimal(row.Cells["VatRate"].Value);

            decimal gross = qty * price;
            decimal discVal = gross * discount / 100;
            decimal net = gross - discVal;
            decimal vat = net * vatRate / 100;

            row.Cells["DiscountValue"].Value = discVal;
            row.Cells["NetValue"].Value = net;
            row.Cells["VatValue"].Value = vat;
            row.Cells["TotalValue"].Value = net + vat;
        }

        private void DgvItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
                dgvItems.Rows[e.RowIndex + i].Cells["LineNumber"].Value = e.RowIndex + i + 1;
        }

        private void CalculateTotals()
        {
            decimal beforeDisc = 0, disc = 0, afterDisc = 0, vat = 0;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;
                beforeDisc += ParseDecimal(row.Cells["Qty"].Value) * ParseDecimal(row.Cells["UnitPrice"].Value);
                disc += ParseDecimal(row.Cells["DiscountValue"].Value);
                afterDisc += ParseDecimal(row.Cells["NetValue"].Value);
                vat += ParseDecimal(row.Cells["VatValue"].Value);
            }

            decimal withholding = 0;
            if (cmbWithholding.SelectedValue != null)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    var rate = conn.ExecuteScalar<decimal?>(
                        "SELECT Rate FROM TaxTypes WHERE Id = @Id",
                        new { Id = Convert.ToInt32(cmbWithholding.SelectedValue) });

                    if (rate.HasValue)
                        withholding = afterDisc * rate.Value / 100;
                }
                catch { }
            }

            decimal grand = afterDisc + vat;
            decimal final = grand - withholding;

            lblTotalBeforeDiscount.Text = $"Πριν έκπτωση: {beforeDisc:N2} €";
            lblTotalDiscount.Text = $"Έκπτωση: {disc:N2} €";
            lblTotalAfterDiscount.Text = $"Καθαρή Αξία: {afterDisc:N2} €";
            lblTotalVat.Text = $"ΦΠΑ: {vat:N2} €";
            lblWithholdingAmount.Text = $"Παρακράτηση: {withholding:N2} €";
            lblGrandTotal.Text = $"ΓΕΝΙΚΟ ΣΥΝΟΛΟ: {grand:N2} €";
            lblFinalAmount.Text = $"ΤΕΛΙΚΟ ΠΛΗΡΩΤΕΟ: {final:N2} €";
        }

        private bool ValidateForm()
        {
            if (cmbDocType.SelectedValue == null) { MessageBox.Show("Επίλεξε Τύπο!"); return false; }
            if (cmbSeries.SelectedValue == null) { MessageBox.Show("Επίλεξε Σειρά!"); return false; }
            if (cmbSupplier.SelectedValue == null) { MessageBox.Show("Επίλεξε Προμηθευτή!"); return false; }
            if (dgvItems.Rows.Count == 1 && dgvItems.Rows[0].IsNewRow) { MessageBox.Show("Πρόσθεσε είδη!"); return false; }
            return true;
        }

        // ============================================
        // HELPERS & LOADERS
        // ============================================
        private Panel CreateHeaderPanel(string title, int height)
        {
            var pnl = new Panel { Dock = DockStyle.Top, Height = height, BackColor = Color.White, Padding = new Padding(10, 8, 10, 8), Margin = new Padding(0, 0, 0, 3) };
            pnl.Controls.Add(new Label { Text = title, Location = new Point(10, 3), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(230, 126, 34) });
            return pnl;
        }
        private ComboBox AddCombo(Panel p, string lbl, int x, int y, int lw, int cw)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), Size = new Size(lw, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var c = new ComboBox { Location = new Point(x + lw, y), Size = new Size(cw, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            p.Controls.Add(c);
            return c;
        }
        private TextBox AddText(Panel p, string lbl, int x, int y, int lw, int cw, bool ro = false)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), Size = new Size(lw, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var t = new TextBox { Location = new Point(x + lw, y), Size = new Size(cw, 26), ReadOnly = ro, Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle, BackColor = ro ? Color.LightYellow : Color.White };
            p.Controls.Add(t);
            return t;
        }
        private DateTimePicker AddDate(Panel p, string lbl, int x, int y, int lw, int cw)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), Size = new Size(lw, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var d = new DateTimePicker { Location = new Point(x + lw, y), Size = new Size(cw, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            p.Controls.Add(d);
            return d;
        }
        private Label CreateTotalLabel(string text, int x, int y, int w, int h, Color color, bool bold = false)
        {
            return new Label { Text = $"{text} 0,00 €", Location = new Point(x, y), Size = new Size(w, h), Font = new Font("Segoe UI", bold ? 10F : 9F, bold ? FontStyle.Bold : FontStyle.Regular), ForeColor = color, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(4, 2, 4, 2) };
        }
        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
        {
            return new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
        }

        private void PurchaseInvoiceForm_Load(object sender, EventArgs e)
        {
            LoadDocumentTypes();
            LoadPaymentMethods();
            LoadPaymentTerms();
            LoadSuppliers();
            LoadWarehouses();
            LoadWithholdings();
            LoadVatStatuses();

            if (_invoiceId.HasValue)
            {
                // TODO: Implement LoadExistingInvoice
            }
            else
            {
                if (cmbDocType.Items.Count > 0) cmbDocType.SelectedIndex = 0;
                ApplyPurchaseOrderPreFill();
            }
        }

        private void LoadDocumentTypes()
        {
            try { using var conn = new SqlConnection(_connStr); var t = conn.Query("SELECT Id, Code, Name FROM DocumentTypes WHERE IsPurchase = 1 AND IsActive = 1 ORDER BY Name").ToList(); cmbDocType.DataSource = t; cmbDocType.DisplayMember = "Name"; cmbDocType.ValueMember = "Id"; }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }
        private void LoadPaymentMethods()
        {
            try { using var conn = new SqlConnection(_connStr); var m = conn.Query("SELECT Id, Code, Name FROM PaymentMethods WHERE IsActive = 1 ORDER BY Name").ToList(); cmbPaymentMethod.DataSource = m; cmbPaymentMethod.DisplayMember = "Name"; cmbPaymentMethod.ValueMember = "Id"; }
            catch { }
        }
        private void LoadPaymentTerms()
        {
            try { using var conn = new SqlConnection(_connStr); var t = conn.Query("SELECT Id, TermCode, TermName, DaysDue FROM PaymentTerms WHERE IsActive = 1 ORDER BY TermName").ToList(); cmbPaymentTerms.DataSource = t; cmbPaymentTerms.DisplayMember = "TermName"; cmbPaymentTerms.ValueMember = "Id"; }
            catch { }
        }
        private void LoadSuppliers()
        {
            try { using var conn = new SqlConnection(_connStr); var s = conn.Query("SELECT Id, SupplierName, AFM FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName").ToList(); cmbSupplier.DataSource = s; cmbSupplier.DisplayMember = "SupplierName"; cmbSupplier.ValueMember = "Id"; }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }
        private void LoadWarehouses()
        {
            try { using var conn = new SqlConnection(_connStr); var w = conn.Query("SELECT Id, Code, Name FROM Warehouses WHERE IsActive = 1 ORDER BY Name").ToList(); cmbWarehouse.DataSource = w; cmbWarehouse.DisplayMember = "Name"; cmbWarehouse.ValueMember = "Id"; }
            catch { }
        }
        private void LoadWithholdings()
        {
            try { using var conn = new SqlConnection(_connStr); var w = conn.Query("SELECT Id, Code, Name, Rate FROM TaxTypes WHERE TaxType = 'WithHolding' AND IsActive = 1 ORDER BY Name").ToList(); cmbWithholding.DataSource = w; cmbWithholding.DisplayMember = "Name"; cmbWithholding.ValueMember = "Id"; }
            catch { }
        }
        private void LoadVatStatuses()
        {
            cmbVatStatus.Items.Clear();
            cmbVatStatus.Items.Add("Κανονικός (24%)");
            cmbVatStatus.Items.Add("Μειωμένος (13%)");
            cmbVatStatus.Items.Add("Υπερμειωμένος (6%)");
            cmbVatStatus.Items.Add("Απαλλασσόμενο");
            cmbVatStatus.SelectedIndex = 0;
        }
        private void LoadSupplierDetails()
        {
            if (cmbSupplier.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(_connStr);
                var s = conn.QueryFirstOrDefault(
                    "SELECT AFM, Address, City, Phone FROM Suppliers WHERE Id = @Id",
                    new { Id = Convert.ToInt32(cmbSupplier.SelectedValue) });

                if (s != null)
                {
                    txtSupplierAfm.Text = s.AFM ?? "";
                    txtSupplierAddress.Text = s.Address ?? "";
                    txtSupplierCity.Text = s.City ?? "";
                    txtSupplierPhone.Text = s.Phone ?? "";
                }
            }
            catch { }
        }
        private void ApplyPurchaseOrderPreFill()
        {
            if (!_prePurchaseOrderId.HasValue) return;
            // ... [Keep existing logic if needed] ...
        }
        private string Str(object value) => value?.ToString() ?? "";
        private decimal Dec(object value) => decimal.TryParse(value?.ToString(), out var r) ? r : 0;
        private int? TryInt(object value) => int.TryParse(value?.ToString(), out var r) ? r : (int?)null;
    }
}