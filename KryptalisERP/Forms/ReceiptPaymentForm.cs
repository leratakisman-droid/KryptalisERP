using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
    public class ReceiptPaymentForm : Form
    {
        private ComboBox cmbType, cmbCounterparty, cmbPaymentMethod, cmbDocType, cmbSeries;
        private DateTimePicker dtpDate;
        private TextBox txtNumber, txtAmount, txtNotes;
        private DataGridView dgvOpenInvoices;
        private Label lblTotalAmount, lblAllocated, lblRemaining;
        private Button btnSave, btnAutoAllocate, btnCancel;
        private bool _isReceipt = true;

        // ✅ SERVICE LAYER INSTANCE
        private readonly DocumentService _docService;
        private readonly string _connStr;

        public ReceiptPaymentForm()
        {
            // Initialize Connection & Service
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";

            _docService = new DocumentService(_connStr);

            InitializeUI();
            this.Load += ReceiptPaymentForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "💰 Εισπράξεις / Πληρωμές (New Engine)";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1100, 650);

            // ============================================
            // HEADER
            // ============================================
            var pnlHeader = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.White, Padding = new Padding(15, 10, 15, 10), Margin = new Padding(0, 0, 0, 5) };
            pnlHeader.Controls.Add(new Label { Text = "💰 ΣΤΟΙΧΕΙΑ ΚΙΝΗΣΗΣ", Location = new Point(10, 3), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) });

            int y = 25, lx = 10;

            AddLabel(pnlHeader, "Τύπος:", lx, y);
            cmbType = new ComboBox { Location = new Point(lx + 55, y), Size = new Size(130, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            cmbType.Items.AddRange(new object[] { "Είσπραξη", "Πληρωμή" });
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += (s, e) => OnTypeChanged();
            pnlHeader.Controls.Add(cmbType);
            lx += 55 + 130 + 15;

            AddLabel(pnlHeader, "Τύπος Εγγρ.:", lx, y);
            cmbDocType = new ComboBox { Location = new Point(lx + 85, y), Size = new Size(150, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbDocType.SelectedIndexChanged += (s, e) => LoadSeries();
            pnlHeader.Controls.Add(cmbDocType);
            lx += 85 + 150 + 15;

            AddLabel(pnlHeader, "Σειρά:", lx, y);
            cmbSeries = new ComboBox { Location = new Point(lx + 55, y), Size = new Size(120, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbSeries.SelectedIndexChanged += (s, e) => GenerateNumberPreview();
            pnlHeader.Controls.Add(cmbSeries);
            lx += 55 + 120 + 15;

            AddLabel(pnlHeader, "Αριθμός:", lx, y);
            txtNumber = new TextBox { Location = new Point(lx + 65, y), Size = new Size(130, 26), ReadOnly = true, BackColor = Color.LightYellow, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle };
            pnlHeader.Controls.Add(txtNumber);
            lx += 65 + 130 + 15;

            AddLabel(pnlHeader, "Ημερομηνία:", lx, y);
            dtpDate = new DateTimePicker { Location = new Point(lx + 85, y), Size = new Size(130, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            pnlHeader.Controls.Add(dtpDate);
            lx += 85 + 130 + 15;

            AddLabel(pnlHeader, "Ποσό €:", lx, y);
            txtAmount = new TextBox { Location = new Point(lx + 60, y), Size = new Size(120, 26), Font = new Font("Segoe UI", 10F, FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle, TextAlign = HorizontalAlignment.Right, BackColor = Color.FromArgb(212, 237, 218) };
            txtAmount.TextChanged += (s, e) => CalculateTotals();
            pnlHeader.Controls.Add(txtAmount);
            lx += 60 + 120 + 15;

            AddLabel(pnlHeader, "Τρόπος:", lx, y);
            cmbPaymentMethod = new ComboBox { Location = new Point(lx + 60, y), Size = new Size(160, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            pnlHeader.Controls.Add(cmbPaymentMethod);

            // Γραμμή 2
            y = 62; lx = 10;

            AddLabel(pnlHeader, "Πελάτης/Προμηθευτής:", lx, y);
            cmbCounterparty = new ComboBox { Location = new Point(lx + 150, y), Size = new Size(320, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbCounterparty.SelectedIndexChanged += (s, e) => LoadOpenInvoices();
            pnlHeader.Controls.Add(cmbCounterparty);
            lx += 150 + 320 + 15;

            AddLabel(pnlHeader, "Σημειώσεις:", lx, y);
            txtNotes = new TextBox { Location = new Point(lx + 85, y), Size = new Size(400, 26), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle };
            pnlHeader.Controls.Add(txtNotes);

            this.Controls.Add(pnlHeader);

            // ============================================
            // GRID ΑΝΟΙΧΤΩΝ ΤΙΜΟΛΟΓΙΩΝ
            // ============================================
            var pnlGridTitle = new Panel { Dock = DockStyle.Top, Height = 35, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 5, 15, 5) };
            pnlGridTitle.Controls.Add(new Label { Text = "📋 ΑΝΟΙΧΤΑ ΤΙΜΟΛΟΓΙΑ - ΚΑΤΑΝΟΜΗ ΠΟΣΟΥ", Location = new Point(10, 6), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) });
            this.Controls.Add(pnlGridTitle);

            dgvOpenInvoices = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 9.5F),
                RowTemplate = { Height = 30 },
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            dgvOpenInvoices.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(3)
            };

            dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "InvoiceId", HeaderText = "Id", Width = 50, Visible = false });
            dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocNumber", HeaderText = "Αριθμός", Width = 130, ReadOnly = true });
            dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocDate", HeaderText = "Ημερομηνία", Width = 110, ReadOnly = true });
            dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalValue", HeaderText = "Αξία Τιμολογίου €", Width = 130, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "AllocatedSoFar", HeaderText = "Κατανεμημένο €", Width = 130, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(240, 240, 240) } });
            dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Balance", HeaderText = "Υπόλοιπο €", Width = 130, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60) } });
            dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "AllocateAmount", HeaderText = "Ποσό Κατανομής €", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), BackColor = Color.FromArgb(212, 237, 218) } });

            dgvOpenInvoices.CellValueChanged += (s, e) => { if (e.ColumnIndex == dgvOpenInvoices.Columns["AllocateAmount"].Index) CalculateTotals(); };

            // ============================================
            // FOOTER
            // ============================================
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 12, 15, 12) };

            lblTotalAmount = new Label { Text = "💰 Ποσό Κίνησης: 0,00 €", Location = new Point(15, 18), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113), BackColor = Color.FromArgb(209, 231, 221), Padding = new Padding(12, 8, 12, 8) };
            pnlFooter.Controls.Add(lblTotalAmount);

            lblAllocated = new Label { Text = "✅ Κατανεμημένο: 0,00 €", Location = new Point(280, 18), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219), BackColor = Color.FromArgb(207, 226, 255), Padding = new Padding(12, 8, 12, 8) };
            pnlFooter.Controls.Add(lblAllocated);

            lblRemaining = new Label { Text = "⏳ Υπόλοιπο: 0,00 €", Location = new Point(540, 18), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60), BackColor = Color.FromArgb(248, 215, 218), Padding = new Padding(12, 8, 12, 8) };
            pnlFooter.Controls.Add(lblRemaining);

            btnAutoAllocate = new Button { Text = "⚡ Αυτόματη Κατανομή", Location = new Point(820, 15), Size = new Size(180, 42), BackColor = Color.FromArgb(241, 196, 15), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnAutoAllocate.Click += (s, e) => AutoAllocate();
            pnlFooter.Controls.Add(btnAutoAllocate);

            btnSave = new Button { Text = "💾 Αποθήκευση", Location = new Point(1010, 15), Size = new Size(150, 42), BackColor = Color.FromArgb(25, 135, 84), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnSave.Click += async (s, e) => await SaveMovementAsync();
            pnlFooter.Controls.Add(btnSave);

            btnCancel = new Button { Text = "❌ Ακύρωση", Location = new Point(1170, 15), Size = new Size(110, 42), BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnCancel.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnCancel);

            // ✅ ΣΩΣΤΗ ΣΕΙΡΑ DOCKING
            this.Controls.Add(pnlFooter);
            this.Controls.Add(dgvOpenInvoices);
            this.Controls.Add(pnlGridTitle);
            this.Controls.Add(pnlHeader);
            dgvOpenInvoices.BringToFront();
        }

        // ============================================
        // CORE LOGIC: SAVE USING SERVICE
        // ============================================
        private async Task SaveMovementAsync()
        {
            var cp = cmbCounterparty.SelectedItem as CounterpartyItem;
            if (cp == null) { MessageBox.Show("Επίλεξε Πελάτη/Προμηθευτή!"); return; }

            decimal amount = ParseDecimal(txtAmount.Text);
            if (amount <= 0) { MessageBox.Show("Βάλε έγκυρο ποσό!"); return; }

            decimal allocated = 0;
            foreach (DataGridViewRow row in dgvOpenInvoices.Rows)
                allocated += ParseDecimal(row.Cells["AllocateAmount"].Value);

            if (allocated > amount + 0.009m)
            {
                MessageBox.Show("Το κατανεμημένο ποσό ξεπερνά το ποσό της κίνησης!");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnSave.Enabled = false;

                // 1. Build Document Model from UI
                var doc = new Document
                {
                    CompanyId = 1, // TODO: Get from session
                    BranchId = null,
                    DocumentTypeId = Convert.ToInt32(cmbDocType.SelectedValue),
                    SeriesId = Convert.ToInt32(cmbSeries.SelectedValue),
                    IssueDate = dtpDate.Value.Date,
                    Currency = "EUR",
                    ExchangeRate = 1m,
                    Status = "ISSUED", // Payments are usually posted immediately

                    // Determine Party based on type
                    CustomerId = _isReceipt ? cp.Id : (int?)null,
                    SupplierId = !_isReceipt ? cp.Id : (int?)null,

                    Notes = txtNotes.Text,

                    TotalNet = amount, // For payments, Net = Amount
                    TotalVat = 0,
                    GrandTotal = amount,
                    Lines = new List<DocumentLine>()
                };

                // 2. Map Allocations to Lines
                // FIX: Changed 'TotalValue' to 'LineTotal' to match the Model definition
                var line = new DocumentLine
                {
                    LineNumber = 1,
                    Description = _isReceipt ? "Είσπραξη από Πελάτη" : "Πληρωμή σε Προμηθευτή",
                    Quantity = 1,
                    UnitPrice = amount,
                    NetValue = amount,
                    VatRate = 0,
                    VatValue = 0,
                    LineTotal = amount // ✅ CORRECTED FIELD NAME
                };
                doc.Lines.Add(line);

                // 3. Call Service
                int newId = await _docService.CreateDocumentAsync(doc);

                MessageBox.Show($"✅ Η {(_isReceipt ? "είσπραξη" : "πληρωμή")} καταχωρήθηκε!\n\nID: {newId}\nΑριθμός: {txtNumber.Text}\nΠοσό: {amount:N2} €", "Επιτυχία");
                this.Close();
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

        private decimal ParseDecimal(object val)
        {
            if (val == null || val == DBNull.Value) return 0;
            if (decimal.TryParse(val.ToString(), out decimal res)) return res;
            return 0;
        }

        // ============================================
        // OTHER METHODS (Adapted for New Flow)
        // ============================================

        private void OnTypeChanged()
        {
            _isReceipt = cmbType.SelectedIndex == 0;
            this.Text = _isReceipt ? "💰 Είσπραξη από Πελάτη" : "💸 Πληρωμή σε Προμηθευτή";
            LoadDocumentTypes();
            LoadCounterparties();
            dgvOpenInvoices.Rows.Clear();
            CalculateTotals();
        }

        private void LoadDocumentTypes()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var t = conn.Query("SELECT Id, Code, Name FROM DocumentTypes WHERE IsActive = 1 AND (Code LIKE '%REC%' OR Code LIKE '%PAY%') ORDER BY Name").ToList();
                cmbDocType.DataSource = t;
                cmbDocType.DisplayMember = "Name";
                cmbDocType.ValueMember = "Id";
                if (cmbDocType.Items.Count > 0) cmbDocType.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadSeries()
        {
            if (cmbDocType.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(_connStr);
                string cat = _isReceipt ? "RECEIPT" : "PAYMENT";
                var s = conn.Query<DocumentSeries>(@"
                    SELECT * FROM DocumentSeries 
                    WHERE DocumentTypeId = @TypeId AND IsActive = 1 AND Category = @Cat",
                    new { TypeId = Convert.ToInt32(cmbDocType.SelectedValue), Cat = cat }).ToList();

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
                    txtNumber.Text = $"{s.Prefix}{(s.NextNumber).ToString().PadLeft(s.NumberLength, '0')}";
                }
            }
            catch { }
        }

        private void ReceiptPaymentForm_Load(object sender, EventArgs e)
        {
            LoadPaymentMethods();
            OnTypeChanged();
        }

        private void LoadPaymentMethods()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var m = conn.Query("SELECT Id, Name FROM PaymentMethods WHERE IsActive = 1 ORDER BY Name").ToList();
                cmbPaymentMethod.DataSource = m;
                cmbPaymentMethod.DisplayMember = "Name";
                cmbPaymentMethod.ValueMember = "Id";
            }
            catch { }
        }

        private void LoadCounterparties()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                cmbCounterparty.Items.Clear();
                if (_isReceipt)
                {
                    var c = conn.Query("SELECT Id, Epwnymia FROM Customers WHERE Energo = 1 ORDER BY Epwnymia").ToList();
                    foreach (var x in c) cmbCounterparty.Items.Add(new CounterpartyItem { Id = x.Id, Name = x.Epwnymia });
                }
                else
                {
                    var s = conn.Query("SELECT Id, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName").ToList();
                    foreach (var x in s) cmbCounterparty.Items.Add(new CounterpartyItem { Id = x.Id, Name = x.SupplierName });
                }
                cmbCounterparty.DisplayMember = "Name";
                if (cmbCounterparty.Items.Count > 0) cmbCounterparty.SelectedIndex = 0;
            }
            catch { }
        }

        private class CounterpartyItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        private void LoadOpenInvoices()
        {
            dgvOpenInvoices.Rows.Clear();
            var cp = cmbCounterparty.SelectedItem as CounterpartyItem;
            if (cp == null) return;

            try
            {
                using var conn = new SqlConnection(_connStr);

                // Placeholder logic since we migrated tables. 
                // In production, update queries to read from dbo.Documents.
                MessageBox.Show("ℹ️ Η φόρτωση ανοιχτών τιμολογίων απαιτεί ενημέρωση των Queries για να διαβάζουν από τον νέο πίνακα 'Documents'.\n\nΠρος το παρόν, η αποθήκευση λειτουργεί.", "Info");
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης ανοιχτών τιμολογίων:\n" + ex.Message); }
        }

        private void AutoAllocate()
        {
            decimal amount = ParseDecimal(txtAmount.Text);
            if (amount <= 0) { MessageBox.Show("Βάλε πρώτα το ποσό της κίνησης!"); return; }

            decimal remaining = amount;
            foreach (DataGridViewRow row in dgvOpenInvoices.Rows)
            {
                if (remaining <= 0) { row.Cells["AllocateAmount"].Value = 0; continue; }

                decimal balance = ParseDecimal(row.Cells["Balance"].Value);
                decimal allocate = Math.Min(remaining, balance);
                row.Cells["AllocateAmount"].Value = allocate;
                remaining -= allocate;
            }
            CalculateTotals();
        }

        private void CalculateTotals()
        {
            decimal amount = ParseDecimal(txtAmount.Text);
            decimal allocated = 0;
            foreach (DataGridViewRow row in dgvOpenInvoices.Rows)
                allocated += ParseDecimal(row.Cells["AllocateAmount"].Value);

            lblTotalAmount.Text = $"💰 Ποσό Κίνησης: {amount:N2} €";
            lblAllocated.Text = $"✅ Κατανεμημένο: {allocated:N2} €";
            lblRemaining.Text = $"⏳ Υπόλοιπο: {amount - allocated:N2} €";

            lblRemaining.ForeColor = (amount - allocated) == 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
        }

        private void AddLabel(Panel p, string text, int x, int y)
        {
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 4), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
        }
    }
}