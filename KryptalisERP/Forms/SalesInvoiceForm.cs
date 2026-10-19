using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration; // ✅ FIX 1: Πρόσθεσε αυτό
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KryptalisERP.Models;
using KryptalisERP.Services;

namespace KryptalisERP
{
    public class ChargeLine
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal Amount { get; set; }
    }

    public class SalesInvoiceForm : Form
    {
        // UI Controls Declaration
        private ComboBox cmbDocType, cmbSeries, cmbWarehouse, cmbSeller, cmbPaymentMethod, cmbPaymentTerms;
        private ComboBox cmbCustomer, cmbVatStatus, cmbVatExemption, cmbDeliveryPurpose, cmbBudgetType;
        private TextBox txtDocNumber, txtNotes, txtCustomerAfm, txtCustomerAddress, txtCustomerCity, txtCustomerZip;
        private TextBox txtCustomerPhone, txtCustomerEmail, txtCustomerDoy, txtLoadingPlace, txtDeliveryPlace;
        private TextBox txtTransportCompany, txtTransportVehicle, txtContractNumber;
        private TextBox txtADA, txtADAM, txtBudgetAmount, txtContractingAuthority;
        private DateTimePicker dtpDocDate, dtpDeliveryDate;
        private DateTimePicker dtpDispatchDate = new DateTimePicker();
        private CheckBox chkIsCreditNote, chkIsReturn, chkIsPublicContract;
        private Panel pnlB2G;
        private DataGridView dgvItems;
        private Label lblTotalBeforeDiscount, lblTotalDiscount, lblTotalAfterDiscount;
        private Label lblTotalVat, lblTotalSurcharges, lblTotalCharges;
        private Label lblGrandTotal, lblRounding;
        private Button btnSave, btnPost, btnPrint, btnDelivery, btnCharges, btnMyData, btnCancel;

        // Logic State
        private List<ChargeLine> _charges = new List<ChargeLine>();
        private int? _invoiceId;
        private int _companyId = 1;
        private dynamic _preOrder;
        private List<dynamic> _preLines;

        // Service Layer Instance
        private readonly DocumentService _docService;
        private readonly string _connStr;

        public int SavedInvoiceId => _invoiceId ?? 0;

        public void PreFillFromOrder(dynamic order, List<dynamic> lines)
        {
            _preOrder = order;
            _preLines = lines ?? new List<dynamic>();
        }

        public SalesInvoiceForm(int? invoiceId = null)
        {
            _invoiceId = invoiceId;

            // Initialize Connection & Service
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";

            _docService = new DocumentService(_connStr);

            InitializeUI();
            this.Load += SalesInvoiceForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "🛒 Τιμολόγιο Πώλησης (New Engine)";
            this.Size = new Size(1500, 900);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1200, 700);

            // --- PANELS CREATION ---
            var pnlBasic = CreateHeaderPanel("📋 ΒΑΣΙΚΑ ΣΤΟΙΧΕΙΑ ΠΑΡΑΣΤΑΤΙΚΟΥ", 85);
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
            cmbSeller = AddCombo(pnlBasic, "Πωλητής:", lx, y, lw, 140);

            y = 52; lx = 10;
            cmbPaymentMethod = AddCombo(pnlBasic, "Πληρωμή:", lx, y, lw, 140);
            lx += lw + 140 + gap;
            cmbPaymentTerms = AddCombo(pnlBasic, "Όροι Πληρ.:", lx, y, lw + 10, 140);
            lx += lw + 10 + 140 + gap;
            cmbVatStatus = AddCombo(pnlBasic, "ΦΠΑ:", lx, y, 45, 140);
            lx += 45 + 140 + gap;
            cmbVatExemption = AddCombo(pnlBasic, "Απαλλαγή:", lx, y, lw, 140);

            var pnlCustomer = CreateHeaderPanel("👤 ΣΤΟΙΧΕΙΑ ΠΕΛΑΤΗ", 85);
            y = 22; lx = 10;
            cmbCustomer = AddCombo(pnlCustomer, "Πελάτης:", lx, y, lw, 280);
            cmbCustomer.SelectedIndexChanged += (s, e) => LoadCustomerDetails();
            lx += lw + 280 + gap;
            txtCustomerAfm = AddText(pnlCustomer, "ΑΦΜ:", lx, y, 45, 100, true);
            lx += 45 + 100 + gap;
            txtCustomerDoy = AddText(pnlCustomer, "ΔΟΥ:", lx, y, 45, 120, false);
            lx += 45 + 120 + gap;
            txtCustomerPhone = AddText(pnlCustomer, "Τηλέφωνο:", lx, y, lw, 110, false);
            lx += lw + 110 + gap;
            txtCustomerEmail = AddText(pnlCustomer, "Email:", lx, y, 50, 180, false);

            y = 52; lx = 10;
            txtCustomerAddress = AddText(pnlCustomer, "Διεύθυνση:", lx, y, lw, 280, false);
            lx += lw + 280 + gap;
            txtCustomerCity = AddText(pnlCustomer, "Πόλη:", lx, y, 45, 120, false);
            lx += 45 + 120 + gap;
            txtCustomerZip = AddText(pnlCustomer, "ΤΚ:", lx, y, 30, 70, false);

            var pnlTransport = CreateHeaderPanel("🚚 ΜΕΤΑΦΟΡΑ & ΛΟΙΠΑ ΣΤΟΙΧΕΙΑ", 85);
            y = 22; lx = 10;
            txtLoadingPlace = AddText(pnlTransport, "Τόπος Φόρτ.:", lx, y, lw, 130, false);
            lx += lw + 130 + gap;
            txtDeliveryPlace = AddText(pnlTransport, "Τόπος Παράδ.:", lx, y, lw + 5, 130, false);
            lx += lw + 5 + 130 + gap;
            txtTransportCompany = AddText(pnlTransport, "Μεταφορέας:", lx, y, lw, 150, false);
            lx += lw + 150 + gap;
            txtTransportVehicle = AddText(pnlTransport, "Όχημα:", lx, y, 60, 100, false);
            lx += 60 + 100 + gap;
            dtpDeliveryDate = AddDate(pnlTransport, "Ημ/νία Παράδ.:", lx, y, lw + 5, 120);

            y = 52; lx = 10;
            cmbDeliveryPurpose = AddCombo(pnlTransport, "Σκοπός Διακ.:", lx, y, lw + 5, 130);
            lx += lw + 5 + 130 + gap;
            txtContractNumber = AddText(pnlTransport, "Συμβόλαιο:", lx, y, lw, 120, false);
            lx += lw + 120 + gap;
            chkIsCreditNote = new CheckBox { Text = "Πιστωτικό", Location = new Point(lx, y + 3), AutoSize = true };
            pnlTransport.Controls.Add(chkIsCreditNote);
            lx += 90;
            chkIsReturn = new CheckBox { Text = "Επιστροφή", Location = new Point(lx, y + 3), AutoSize = true };
            pnlTransport.Controls.Add(chkIsReturn);
            lx += 95;
            chkIsPublicContract = new CheckBox { Text = "🏛️ Δημόσιο (B2G)", Location = new Point(lx, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(230, 126, 34) };
            chkIsPublicContract.CheckedChanged += (s, e) => { pnlB2G.Visible = chkIsPublicContract.Checked; };
            pnlTransport.Controls.Add(chkIsPublicContract);

            pnlB2G = CreateHeaderPanel("🏛️ ΣΤΟΙΧΕΙΑ ΔΗΜΟΣΙΟΥ (B2G)", 80);
            pnlB2G.BackColor = Color.FromArgb(255, 250, 240);
            pnlB2G.Visible = false;
            y = 22; lx = 10; lw = 130;
            pnlB2G.Controls.Add(new Label { Text = "Αναθέτουσα Αρχή:", Location = new Point(lx, y + 3), Size = new Size(lw, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtContractingAuthority = new TextBox { Location = new Point(lx + lw, y), Size = new Size(250, 26), BorderStyle = BorderStyle.FixedSingle };
            pnlB2G.Controls.Add(txtContractingAuthority);
            lx += lw + 250 + gap;
            pnlB2G.Controls.Add(new Label { Text = "ΑΔΑ:", Location = new Point(lx, y + 3), Size = new Size(45, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtADA = new TextBox { Location = new Point(lx + 45, y), Size = new Size(140, 26), BorderStyle = BorderStyle.FixedSingle };
            pnlB2G.Controls.Add(txtADA);
            lx += 45 + 140 + gap;
            pnlB2G.Controls.Add(new Label { Text = "ΑΔΑΜ:", Location = new Point(lx, y + 3), Size = new Size(50, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtADAM = new TextBox { Location = new Point(lx + 50, y), Size = new Size(140, 26), BorderStyle = BorderStyle.FixedSingle };
            pnlB2G.Controls.Add(txtADAM);
            lx += 50 + 140 + gap;
            cmbBudgetType = new ComboBox { Location = new Point(lx, y), Size = new Size(150, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbBudgetType.Items.AddRange(new object[] { "Τακτικός", "Επενδυτικός", "Εξοπλισμός" });
            cmbBudgetType.SelectedIndex = 0;
            pnlB2G.Controls.Add(cmbBudgetType);
            lx += 150 + gap;
            pnlB2G.Controls.Add(new Label { Text = "Προϋπολογισμός €:", Location = new Point(lx, y + 3), Size = new Size(110, 22), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtBudgetAmount = new TextBox { Location = new Point(lx + 110, y), Size = new Size(100, 26), BorderStyle = BorderStyle.FixedSingle, TextAlign = HorizontalAlignment.Right };
            pnlB2G.Controls.Add(txtBudgetAmount);

            var pnlNotes = CreateHeaderPanel("📝 ΣΗΜΕΙΩΣΕΙΣ", 45);
            txtNotes = new TextBox { Location = new Point(110, 8), Size = new Size(1300, 28), BorderStyle = BorderStyle.FixedSingle };
            pnlNotes.Controls.Add(txtNotes);

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
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(3)
            };

            dgvItems.Columns.Add(new DataGridViewComboBoxColumn { Name = "LineType", HeaderText = "Τύπος", Width = 85, Items = { "Είδος", "Υπηρεσία" } });
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
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Weight", HeaderText = "Βάρος", Width = 55, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Color", HeaderText = "Χρώμα", Width = 60 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Size", HeaderText = "Μέγεθος", Width = 60 });

            dgvItems.CellValueChanged += DgvItems_CellValueChanged;
            dgvItems.RowsAdded += DgvItems_RowsAdded;
            dgvItems.UserDeletingRow += (s, e) => CalculateTotals();

            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 75, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(10, 8, 10, 8) };

            btnSave = CreateButton("💾 Αποθήκευση", 10, 15, 125, 38, Color.FromArgb(13, 110, 253));
            btnSave.Click += async (s, e) => await SaveInvoiceAsync(false);
            pnlFooter.Controls.Add(btnSave);

            btnPost = CreateButton("🚀 Οριστικοποίηση", 145, 15, 140, 38, Color.FromArgb(25, 135, 84));
            btnPost.Click += async (s, e) => await PostInvoiceAsync();
            pnlFooter.Controls.Add(btnPost);

            btnCharges = CreateButton("💳 Πρόσθετες Χρεώσεις", 295, 15, 165, 38, Color.FromArgb(241, 196, 15));
            btnCharges.Click += (s, e) => OpenChargesPopup(); // ✅ FIX 3: Η μέθοδος υπάρχει παρακάτω
            pnlFooter.Controls.Add(btnCharges);

            btnPrint = CreateButton("🖨️ Εκτύπωση", 470, 15, 110, 38, Color.FromArgb(155, 89, 182));
            btnPrint.Click += (s, e) => MessageBox.Show("Εκτύπωση - Υπό υλοποίηση", "Πληροφορία");
            pnlFooter.Controls.Add(btnPrint);

            btnDelivery = CreateButton("📦 Παράδοση", 590, 15, 110, 38, Color.FromArgb(230, 126, 34));
            btnDelivery.Click += (s, e) => MessageBox.Show("Δελτίο Αποστολής - Υπό υλοποίηση", "Πληροφορία");
            pnlFooter.Controls.Add(btnDelivery);

            btnMyData = CreateButton("🏛️ myDATA", 710, 15, 110, 38, Color.FromArgb(52, 73, 94));
            btnMyData.Click += async (s, e) => await SendToMyDataAsync();
            pnlFooter.Controls.Add(btnMyData);

            btnCancel = CreateButton("❌ Ακύρωση", 830, 15, 100, 38, Color.FromArgb(108, 117, 125));
            btnCancel.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnCancel);

            var pnlTotals = new Panel { Location = new Point(940, 5), Size = new Size(530, 65), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            int tx = 8, ty = 4, tw = 155, th = 19;

            lblTotalBeforeDiscount = CreateTotalLabel("Πριν έκπτωση:", tx, ty, tw, th, Color.FromArgb(52, 152, 219));
            pnlTotals.Controls.Add(lblTotalBeforeDiscount);
            lblTotalDiscount = CreateTotalLabel("Έκπτωση:", tx + tw + 5, ty, tw, th, Color.FromArgb(231, 76, 60));
            pnlTotals.Controls.Add(lblTotalDiscount);
            lblTotalAfterDiscount = CreateTotalLabel("Καθαρή Αξία:", tx, ty + th + 2, tw, th, Color.FromArgb(52, 152, 219));
            pnlTotals.Controls.Add(lblTotalAfterDiscount);
            lblTotalVat = CreateTotalLabel("ΦΠΑ:", tx + tw + 5, ty + th + 2, tw, th, Color.FromArgb(241, 196, 15));
            pnlTotals.Controls.Add(lblTotalVat);
            lblTotalSurcharges = CreateTotalLabel("Επιβαρύνσεις:", tx, ty + (th + 2) * 2, tw, th, Color.FromArgb(230, 126, 34));
            pnlTotals.Controls.Add(lblTotalSurcharges);
            lblTotalCharges = CreateTotalLabel("Χρεώσεις:", tx + tw + 5, ty + (th + 2) * 2, tw, th, Color.FromArgb(241, 196, 15));
            pnlTotals.Controls.Add(lblTotalCharges);
            lblGrandTotal = CreateTotalLabel("ΓΕΝΙΚΟ ΣΥΝΟΛΟ:", tx, ty + (th + 2) * 3, tw, th + 2, Color.FromArgb(46, 204, 113), true);
            pnlTotals.Controls.Add(lblGrandTotal);
            lblRounding = CreateTotalLabel("Στρογγυλ.:", tx + tw + 5, ty + (th + 2) * 3, tw, th + 2, Color.FromArgb(108, 117, 125));
            pnlTotals.Controls.Add(lblRounding);
            pnlFooter.Controls.Add(pnlTotals);

            this.Controls.Add(pnlFooter);
            this.Controls.Add(dgvItems);
            this.Controls.Add(pnlNotes);
            this.Controls.Add(pnlB2G);
            this.Controls.Add(pnlTransport);
            this.Controls.Add(pnlCustomer);
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
                    IssueDate = dtpDocDate.Value.Date, // Use Date only for consistency
                    CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue),
                    Currency = "EUR",
                    ExchangeRate = 1m,
                    Status = isPost ? "ISSUED" : "DRAFT",
                    Notes = txtNotes.Text,
                    SalespersonId = cmbSeller.SelectedValue != null ? Convert.ToInt32(cmbSeller.SelectedValue) : null,

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

                    // ✅ FIX 2: Safe parsing for decimals to avoid type mismatch issues later
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
                        ItemSize = row.Cells["Size"].Value?.ToString(),
                        ItemColor = row.Cells["Color"].Value?.ToString()
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

                    // Optional: Reload data to show final number/status
                    // For now, simple success message
                    MessageBox.Show($"✅ Παραστατικό δημιουργήθηκε!\nID: {newId}", "Επιτυχία");
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
            if (MessageBox.Show("Οριστικοποίηση;\nΘα μειωθεί το stock.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                // In production, call _docService.PostDocumentAsync(_invoiceId)
                MessageBox.Show("✅ Οριστικοποιήθηκε (Mock).", "Success");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task SendToMyDataAsync()
        {
            if (!_invoiceId.HasValue) { MessageBox.Show("Αποθήκευσε πρώτα!"); return; }
            MessageBox.Show("Αποστολή στο myDATA (Mock)...", "Info");
        }

        private void LoadSeries()
        {
            if (cmbDocType.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(_connStr);
                var s = conn.Query<DocumentSeries>(@"
                    SELECT * FROM DocumentSeries 
                    WHERE DocumentTypeId = @TypeId AND IsActive = 1 AND Category = 'SALES'",
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
                        var item = conn.QueryFirstOrDefault<Item>("SELECT Name, Unit, SalePrice, VatRate, Weight, Color, Size, ItemType FROM Items WHERE Code = @Code", new { Code = code });
                        if (item != null)
                        {
                            row.Cells["Description"].Value = item.Name;
                            row.Cells["Unit"].Value = item.Unit;
                            row.Cells["Weight"].Value = item.Weight;
                            row.Cells["Color"].Value = item.Color;
                            row.Cells["Size"].Value = item.Size;
                            if (row.Cells["LineType"].Value == null) row.Cells["LineType"].Value = item.ItemType ?? "Είδος";
                            if (row.Cells["UnitPrice"].Value == null || string.IsNullOrWhiteSpace(row.Cells["UnitPrice"].Value?.ToString()))
                                row.Cells["UnitPrice"].Value = item.SalePrice;
                            if (row.Cells["VatRate"].Value == null || string.IsNullOrWhiteSpace(row.Cells["VatRate"].Value?.ToString()))
                                
                                row.Cells["VatRate"].Value = (item.VatRate > 0) ? item.VatRate : 24m;

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
            decimal surcharges = _charges.Sum(c => c.Amount);
            decimal grand = afterDisc + vat + surcharges;

            lblTotalBeforeDiscount.Text = $"Πριν έκπτωση: {beforeDisc:N2} €";
            lblTotalDiscount.Text = $"Έκπτωση: {disc:N2} €";
            lblTotalAfterDiscount.Text = $"Καθαρή Αξία: {afterDisc:N2} €";
            lblTotalVat.Text = $"ΦΠΑ: {vat:N2} €";
            lblTotalSurcharges.Text = $"Επιβαρύνσεις: {surcharges:N2} €";
            lblTotalCharges.Text = $"Χρεώσεις: {_charges.Count}";
            lblGrandTotal.Text = $"ΓΕΝΙΚΟ ΣΥΝΟΛΟ: {grand:N2} €";
            lblRounding.Text = $"Στρογγυλ.: {(Math.Round(grand, 2) - grand):N2} €";
        }

        private bool ValidateForm()
        {
            if (cmbDocType.SelectedValue == null) { MessageBox.Show("Επίλεξε Τύπο!"); return false; }
            if (cmbSeries.SelectedValue == null) { MessageBox.Show("Επίλεξε Σειρά!"); return false; }
            if (cmbCustomer.SelectedValue == null) { MessageBox.Show("Επίλεξε Πελάτη!"); return false; }
            if (dgvItems.Rows.Count == 1 && dgvItems.Rows[0].IsNewRow) { MessageBox.Show("Πρόσθεσε είδη!"); return false; }
            return true;
        }

        // ============================================
        // POPUP: ΠΡΟΣΘΕΤΕΣ ΧΡΕΩΣΕΙΣ (FIXED)
        // ============================================
        private void OpenChargesPopup()
        {
            var popup = new Form
            {
                Text = "💳 Πρόσθετες Χρεώσεις / Φόροι / Παρακρατήσεις",
                Size = new Size(850, 500),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblInfo = new Label
            {
                Text = "Πρόσθεσε φόρους, παρακρατήσεις, κρατήσεις, χαρτόσημο, τέλη ή οποιαδήποτε άλλη επιβάρυνση:",
                Location = new Point(15, 10),
                Size = new Size(800, 25),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(230, 126, 34)
            };
            popup.Controls.Add(lblInfo);

            var dgv = new DataGridView
            {
                Location = new Point(15, 40),
                Size = new Size(800, 350),
                AutoGenerateColumns = false,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5F),
                RowTemplate = { Height = 28 }
            };

            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(241, 196, 15),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgv.Columns.Add(new DataGridViewComboBoxColumn { Name = "Type", HeaderText = "Τύπος", Width = 180, Items = { "Φόρος Εισοδήματος", "Παρακράτηση", "Κράτηση", "Χαρτόσημο", "Τέλος Επιτηδεύματος", "Επιβάρυνση", "Έξοδα Μεταφορικών", "Λοιπά" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Περιγραφή", Width = 250 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Rate", HeaderText = "Ποσοστό %", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "BaseAmount", HeaderText = "Βάση €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Amount", HeaderText = "Αξία €", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });

            dgv.CellValueChanged += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                var row = dgv.Rows[e.RowIndex];
                var col = dgv.Columns[e.ColumnIndex].Name;
                if ((col == "Rate" || col == "BaseAmount") && !row.IsNewRow)
                {
                    decimal rate = ParseDecimal(row.Cells["Rate"].Value);
                    decimal baseAmt = ParseDecimal(row.Cells["BaseAmount"].Value);
                    if (rate > 0 && baseAmt > 0)
                        row.Cells["Amount"].Value = baseAmt * rate / 100;
                }
            };

            foreach (var c in _charges)
                dgv.Rows.Add(c.Type, c.Description, c.Rate, c.BaseAmount, c.Amount);

            popup.Controls.Add(dgv);

            var btnOk = new Button { Text = "✅ Εφαρμογή", Location = new Point(560, 405), Size = new Size(120, 38), BackColor = Color.FromArgb(25, 135, 84), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), FlatAppearance = { BorderSize = 0 } };
            btnOk.Click += (s, e) =>
            {
                _charges.Clear();
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    string type = row.Cells["Type"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(type)) continue;
                    _charges.Add(new ChargeLine
                    {
                        Type = type,
                        Description = row.Cells["Description"].Value?.ToString() ?? "",
                        Rate = ParseDecimal(row.Cells["Rate"].Value),
                        BaseAmount = ParseDecimal(row.Cells["BaseAmount"].Value),
                        Amount = ParseDecimal(row.Cells["Amount"].Value)
                    });
                }
                CalculateTotals();
                popup.DialogResult = DialogResult.OK;
                popup.Close();
            };
            popup.Controls.Add(btnOk);

            var btnCancelPopup = new Button { Text = "❌ Ακύρωση", Location = new Point(695, 405), Size = new Size(120, 38), BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), FlatAppearance = { BorderSize = 0 } };
            btnCancelPopup.Click += (s, e) => popup.Close();
            popup.Controls.Add(btnCancelPopup);

            popup.ShowDialog();
        }

        // ============================================
        // HELPERS & LOADERS
        // ============================================
        private Panel CreateHeaderPanel(string title, int height)
        {
            var pnl = new Panel { Dock = DockStyle.Top, Height = height, BackColor = Color.White, Padding = new Padding(10, 8, 10, 8), Margin = new Padding(0, 0, 0, 3) };
            pnl.Controls.Add(new Label { Text = title, Location = new Point(10, 3), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(13, 110, 253) });
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

        private void SalesInvoiceForm_Load(object sender, EventArgs e)
        {
            LoadDocumentTypes();
            LoadPaymentMethods();
            LoadPaymentTerms();
            LoadCustomers();
            LoadWarehouses();
            LoadSellers();
            LoadVatStatuses();
            LoadDeliveryPurposes();

            if (_invoiceId.HasValue)
            {
                // TODO: Implement LoadExistingInvoice
            }
            else
            {
                if (cmbDocType.Items.Count > 0) cmbDocType.SelectedIndex = 0;
                ApplyOrderPreFill();
            }
        }

        private void LoadDocumentTypes()
        {
            try { using var conn = new SqlConnection(_connStr); var t = conn.Query("SELECT Id, Code, Name FROM DocumentTypes WHERE IsSales = 1 AND IsActive = 1 ORDER BY Name").ToList(); cmbDocType.DataSource = t; cmbDocType.DisplayMember = "Name"; cmbDocType.ValueMember = "Id"; }
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
        private void LoadCustomers()
        {
            try { using var conn = new SqlConnection(_connStr); var c = conn.Query("SELECT Id, Epwnymia, Afm FROM Customers WHERE Energo = 1 ORDER BY Epwnymia").ToList(); cmbCustomer.DataSource = c; cmbCustomer.DisplayMember = "Epwnymia"; cmbCustomer.ValueMember = "Id"; }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }
        private void LoadWarehouses()
        {
            try { using var conn = new SqlConnection(_connStr); var w = conn.Query("SELECT Id, Code, Name FROM Warehouses WHERE IsActive = 1 ORDER BY Name").ToList(); cmbWarehouse.DataSource = w; cmbWarehouse.DisplayMember = "Name"; cmbWarehouse.ValueMember = "Id"; }
            catch { }
        }
        private void LoadSellers()
        {
            try { using var conn = new SqlConnection(_connStr); var s = conn.Query("SELECT Id, Code, Name FROM Salespersons WHERE IsActive = 1 ORDER BY Name").ToList(); cmbSeller.DataSource = s; cmbSeller.DisplayMember = "Name"; cmbSeller.ValueMember = "Id"; }
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
        private void LoadDeliveryPurposes()
        {
            try { using var conn = new SqlConnection(_connStr); var p = conn.Query("SELECT Id, Code, Name FROM DeliveryPurposes WHERE IsActive = 1 ORDER BY Name").ToList(); cmbDeliveryPurpose.DataSource = p; cmbDeliveryPurpose.DisplayMember = "Name"; cmbDeliveryPurpose.ValueMember = "Id"; }
            catch { }
        }
        private void LoadCustomerDetails()
        {
            if (cmbCustomer.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(_connStr);
                var c = conn.QueryFirstOrDefault("SELECT Afm, Dieuthinsi, Poli, TK, Tilefono, Email, DOY FROM Customers WHERE Id = @Id", new { Id = Convert.ToInt32(cmbCustomer.SelectedValue) });
                if (c != null)
                {
                    txtCustomerAfm.Text = c.Afm ?? "";
                    txtCustomerAddress.Text = c.Dieuthinsi ?? "";
                    txtCustomerCity.Text = c.Poli ?? "";
                    txtCustomerZip.Text = c.TK ?? "";
                    txtCustomerPhone.Text = c.Tilefono ?? "";
                    txtCustomerEmail.Text = c.Email ?? "";
                    txtCustomerDoy.Text = c.DOY ?? "";
                }
            }
            catch { }
        }
        private void ApplyOrderPreFill()
        {
            if (_preOrder == null) return;
            // ... [Keep existing logic if needed] ...
        }
        private string StrFromObject(object value) => value?.ToString() ?? "";
        private int? IntFromObject(object value) => int.TryParse(value?.ToString(), out var r) ? r : (int?)null;
        private decimal DecFromObject(object value) => decimal.TryParse(value?.ToString(), out var r) ? r : 0;
        private DateTime? DtFromObject(object value) => DateTime.TryParse(value?.ToString(), out var r) ? r : (DateTime?)null;
    }
}