using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration; // ✅ FIX: Πρόσθεσε αυτό
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KryptalisERP.Models;      // <-- ΝΕΑ IMPORT
using KryptalisERP.Services;    // <-- ΝΕΑ IMPORT

namespace KryptalisERP
{
    public class DeliveryNoteForm : Form
    {
        private ComboBox cmbCustomer, cmbWarehouse, cmbStatus, cmbTransportCo, cmbDocType, cmbSeries;
        private TextBox txtDocNumber, txtShippingAddr, txtContactPerson, txtPhone, txtVehiclePlate, txtDriverName, txtNotes;
        private DateTimePicker dtpDocDate;
        private DataGridView dgvLines;
        private Label lblTotalQty, lblTotalWeight, lblWarning;
        private Button btnSave, btnPost, btnClose, btnPickFromOrder;

        private int? _noteId;
        private int? _sourceOrderId;
        private bool _isLoading = false;

        // ✅ SERVICE LAYER INSTANCE
        private readonly DocumentService _docService;
        private readonly string _connStr;

        public static readonly Dictionary<string, string> StatusMap = new Dictionary<string, string>
        {
            { "DRAFT", "📝 Πρόχειρο" },
            { "SENT_TO_MYDATA", "🏛️ Στάλθηκε στη Φάση Β" },
            { "ACCEPTED", "✅ Εγκρίθηκε από ΑΑΔΕ" },
            { "REJECTED", "❌ Απορρίφθηκε" },
            { "CANCELLED", "❌ Ακυρωμένο" }
        };

        public DeliveryNoteForm(int? noteId = null, int? sourceOrderId = null)
        {
            _noteId = noteId;
            _sourceOrderId = sourceOrderId;

            // Initialize Connection & Service
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";

            _docService = new DocumentService(_connStr);

            InitializeUI();
            this.Load += DeliveryNoteForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📦 Δελτίο Αποστολής (New Engine)";
            this.Size = new Size(1300, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1000, 600);

            // --- 1. HEADER INFO ---
            var pnlHeader = CreatePanel("📋 ΣΤΟΙΧΕΙΑ ΑΠΟΣΤΟΛΗΣ", 110, Color.FromArgb(230, 126, 34));
            int y = 22, lx = 10, lw = 90, gap = 10;

            cmbDocType = AddCombo(pnlHeader, "Τύπος:", lx, y, lw, 140);
            cmbDocType.SelectedIndexChanged += (s, e) => LoadSeries();
            lx += lw + 140 + gap;

            cmbSeries = AddCombo(pnlHeader, "Σειρά:", lx, y, lw, 120);
            cmbSeries.SelectedIndexChanged += (s, e) => GenerateNumberPreview();
            lx += lw + 120 + gap;

            txtDocNumber = AddText(pnlHeader, "Αριθμός:", lx, y, lw, 140, true);
            lx += lw + 140 + gap;

            dtpDocDate = AddDate(pnlHeader, "Ημερομηνία:", lx, y, lw, 120);
            lx += lw + 120 + gap;

            cmbCustomer = AddCombo(pnlHeader, "Πελάτης:", lx, y, lw, 250);
            cmbCustomer.SelectedIndexChanged += (s, e) => LoadCustomerDefaults();
            lx += lw + 250 + gap;

            cmbWarehouse = AddCombo(pnlHeader, "Αποθήκη:", lx, y, lw, 150);

            y = 52; lx = 10;
            txtShippingAddr = AddText(pnlHeader, "Τόπος Παράδ.:", lx, y, lw + 10, 300, false);
            lx += lw + 10 + 300 + gap;
            txtContactPerson = AddText(pnlHeader, "Υπεύθυνος:", lx, y, lw, 150, false);
            lx += lw + 150 + gap;
            txtPhone = AddText(pnlHeader, "Τηλ.:", lx, y, 45, 120, false);

            y = 82; lx = 10;
            cmbTransportCo = AddCombo(pnlHeader, "Μεταφορική:", lx, y, lw + 10, 200);
            cmbTransportCo.Items.AddRange(new object[] { "IDIOTIKI METAFORA", "ACS COURIER", "GENIKI TAUXIDROMIKEI", "ALLAGE STON PELATI" });
            cmbTransportCo.SelectedIndex = 0;
            lx += lw + 10 + 200 + gap;
            txtVehiclePlate = AddText(pnlHeader, "Πινακίδα:", lx, y, lw, 100, false);
            lx += lw + 100 + gap;
            txtDriverName = AddText(pnlHeader, "Οδηγός:", lx, y, lw, 150, false);
            lx += lw + 150 + gap;
            cmbStatus = AddCombo(pnlHeader, "Κατάσταση:", lx, y, lw, 170);
            cmbStatus.SelectedIndexChanged += (s, e) => OnStatusChanged();

            this.Controls.Add(pnlHeader);

            // --- 2. GRID ΓΡΑΜΜΩΝ ---
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
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "LineNumber", HeaderText = "Α/Α", Width = 40, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemId", HeaderText = "ItemId", Width = 50, Visible = false });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", HeaderText = "Κωδικός", Width = 100 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Περιγραφή", Width = 300 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "QtyOrdered", HeaderText = "Ζητήθηκε", Width = 80, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.LightYellow } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "QtyShipped", HeaderText = "Στέλνεται", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Uom", HeaderText = "ΜΜ", Width = 50 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "LotNo", HeaderText = "Παρτίδα", Width = 100 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "SerialNo", HeaderText = "Serial", Width = 100 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Weight", HeaderText = "Βάρος kg", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockAvail", HeaderText = "Διαθ. Στοκ", Width = 80, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.Gray } });

            dgvLines.CellValueChanged += DgvLines_CellValueChanged;
            dgvLines.RowsAdded += (s, e) => { for (int i = 0; i < e.RowCount; i++) dgvLines.Rows[e.RowIndex + i].Cells["LineNumber"].Value = e.RowIndex + i + 1; };
            dgvLines.UserDeletingRow += (s, e) => CalculateTotals();

            // --- 3. FOOTER ---
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 80, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(10) };

            btnPickFromOrder = CreateButton("📋 ← Από Παραγγελία", 10, 15, 160, 40, Color.FromArgb(52, 152, 219));
            btnPickFromOrder.Click += (s, e) => PickItemsFromOrder();
            pnlFooter.Controls.Add(btnPickFromOrder);

            btnSave = CreateButton("💾 Αποθήκευση", 180, 15, 130, 40, Color.FromArgb(13, 110, 253));
            btnSave.Click += async (s, e) => await SaveNoteAsync(false);
            pnlFooter.Controls.Add(btnSave);

            btnPost = CreateButton("🚀 Οριστικοποίηση & myDATA", 320, 15, 200, 40, Color.FromArgb(25, 135, 84));
            btnPost.Click += async (s, e) => await PostAndSendToMyData();
            pnlFooter.Controls.Add(btnPost);

            btnClose = CreateButton("❌ Κλείσιμο", 530, 15, 110, 40, Color.FromArgb(108, 117, 125));
            btnClose.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnClose);

            var pnlStats = new Panel { Location = new Point(650, 5), Size = new Size(600, 70), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            int sx = 10, sy = 10;
            lblTotalQty = new Label { Text = "Συν. Ποσότητα: 0", Location = new Point(sx, sy), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) };
            lblTotalWeight = new Label { Text = "Συν. Βάρος: 0 kg", Location = new Point(sx, sy + 25), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(155, 89, 182) };
            lblWarning = new Label { Text = "", Location = new Point(sx + 200, sy), AutoSize = false, Size = new Size(380, 50), Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.Red, TextAlign = ContentAlignment.MiddleLeft };

            pnlStats.Controls.Add(lblTotalQty);
            pnlStats.Controls.Add(lblTotalWeight);
            pnlStats.Controls.Add(lblWarning);
            pnlFooter.Controls.Add(pnlStats);

            this.Controls.Add(pnlFooter);
            this.Controls.Add(dgvLines);
            this.Controls.Add(pnlHeader);
            dgvLines.BringToFront();
        }

        // ============================================
        // CORE LOGIC: SAVE USING SERVICE
        // ============================================
        private async Task SaveNoteAsync(bool isPost)
        {
            if (!ValidateForm()) return;

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
                    IssueDate = dtpDocDate.Value.Date,
                    CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue),
                    Currency = "EUR", // Dummy currency for delivery notes usually
                    ExchangeRate = 1m,
                    Status = isPost ? "ISSUED" : "DRAFT",
                    Notes = txtNotes.Text,

                    TotalNet = 0,
                    TotalVat = 0,
                    GrandTotal = 0,
                    Lines = new List<DocumentLine>()
                };

                // 2. Map Grid Rows to Lines
                foreach (DataGridViewRow row in dgvLines.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (string.IsNullOrWhiteSpace(row.Cells["ItemCode"].Value?.ToString())) continue;

                    decimal qty = ParseDecimal(row.Cells["QtyShipped"].Value);
                    if (qty <= 0) continue;

                    var line = new DocumentLine
                    {
                        LineNumber = Convert.ToInt32(row.Cells["LineNumber"].Value),
                        ItemId = Convert.ToInt32(row.Cells["ItemId"].Value),
                        Description = row.Cells["Description"].Value?.ToString(),
                        Quantity = qty,
                        UnitPrice = 0, // Delivery notes often have no price or cost basis
                        DiscountPercent = 0,
                        VatRate = 0,

                        UomId = 1, // Default TEM
                        WarehouseId = cmbWarehouse.SelectedValue != null ? Convert.ToInt32(cmbWarehouse.SelectedValue) : null,
                        LotId = null, // Can be mapped if LotNo exists in DB
                        SerialId = null
                    };

                    doc.Lines.Add(line);
                }

                // 3. Call Service
                if (_noteId.HasValue)
                {
                    throw new NotImplementedException("UPDATE mode requires Repo implementation.");
                }
                else
                {
                    int newId = await _docService.CreateDocumentAsync(doc);
                    _noteId = newId;

                    MessageBox.Show($"✅ Δελτίο δημιουργήθηκε!\nID: {newId}", "Επιτυχία");
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

        private bool ValidateForm()
        {
            if (cmbDocType.SelectedValue == null) { MessageBox.Show("Επίλεξε Τύπο!"); return false; }
            if (cmbSeries.SelectedValue == null) { MessageBox.Show("Επίλεξε Σειρά!"); return false; }
            if (cmbCustomer.SelectedValue == null) { MessageBox.Show("Επίλεξε Πελάτη!"); return false; }
            if (dgvLines.Rows.Count == 1 && dgvLines.Rows[0].IsNewRow) { MessageBox.Show("Πρόσθεσε είδη!"); return false; }
            return true;
        }

        private decimal ParseDecimal(object val)
        {
            if (val == null || val == DBNull.Value) return 0;
            if (decimal.TryParse(val.ToString(), out decimal res)) return res;
            return 0;
        }

        // ============================================
        // OTHER METHODS
        // ============================================

        private async Task PostAndSendToMyData()
        {
            if (!_noteId.HasValue) { MessageBox.Show("Αποθήκευσε πρώτα!"); return; }
            if (MessageBox.Show("Οριστικοποίηση;\nΘα μειωθεί το stock.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                // In production, call _docService.PostDocumentAsync(_noteId)
                // which handles stock decrease logic separately
                MessageBox.Show("✅ Το δελτίο οριστικοποιήθηκε (Mock).", "Success");
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
                    WHERE DocumentTypeId = @TypeId AND IsActive = 1 AND Category = 'INVENTORY'",
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
                        using var conn = new SqlConnection(_connStr);
                        var item = conn.QueryFirstOrDefault(@"SELECT i.Id, i.Name, i.Unit, i.Weight, 
                                    ISNULL((SELECT SUM(AvailableQty) FROM StockVariants sv JOIN ItemVariants iv ON sv.ItemVariantId=iv.Id WHERE iv.ItemId=i.Id AND sv.WarehouseId=@Wh),0) AS Avail
                                  FROM Items i WHERE i.Code = @Code",
                                  new { Code = code, Wh = cmbWarehouse.SelectedValue != null ? Convert.ToInt32(cmbWarehouse.SelectedValue) : 1 });

                        if (item != null)
                        {
                            row.Cells["ItemId"].Value = item.Id;
                            row.Cells["Description"].Value = item.Name;
                            row.Cells["Uom"].Value = item.Unit;
                            row.Cells["Weight"].Value = item.Weight;
                            row.Cells["StockAvail"].Value = item.Avail;

                            if (row.Cells["QtyShipped"].Value == null)
                                row.Cells["QtyShipped"].Value = row.Cells["QtyOrdered"].Value ?? 0;
                        }
                    }
                    catch { }
                }
            }

            if (col == "QtyShipped" || col == "Weight")
            {
                CheckStockAndCalc(row);
                CalculateTotals();
            }
        }

        private void CheckStockAndCalc(DataGridViewRow row)
        {
            decimal shipped = Dec(row, "QtyShipped");
            decimal avail = Dec(row, "StockAvail");
            decimal weightPerUnit = Dec(row, "Weight");

            if (shipped > avail)
            {
                row.DefaultCellStyle.BackColor = Color.MistyRose;
                lblWarning.Text = $"⚠️ Προσοχή: Το είδος '{row.Cells["ItemCode"].Value}' υπερβαίνει το διαθέσιμο στοκ ({avail}).";
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
                lblWarning.Text = "";
            }
        }

        private decimal Dec(DataGridViewRow row, string col) => decimal.TryParse(row.Cells[col].Value?.ToString(), out var v) ? v : 0;

        private void CalculateTotals()
        {
            decimal totalQty = 0, totalWeight = 0;
            foreach (DataGridViewRow r in dgvLines.Rows)
            {
                if (r.IsNewRow) continue;
                totalQty += Dec(r, "QtyShipped");
                totalWeight += Dec(r, "QtyShipped") * Dec(r, "Weight");
            }
            lblTotalQty.Text = $"Συν. Ποσότητα: {totalQty:N2}";
            lblTotalWeight.Text = $"Συν. Βάρος: {totalWeight:N2} kg";
        }

        private void OnStatusChanged()
        {
            if (_isLoading) return;
            string status = cmbStatus.SelectedValue?.ToString();
            bool locked = status == "ACCEPTED" || status == "CANCELLED";
            dgvLines.ReadOnly = locked;
            dgvLines.AllowUserToAddRows = !locked;
            dgvLines.AllowUserToDeleteRows = !locked;
            btnSave.Enabled = !locked;
            btnPost.Enabled = status == "DRAFT";
            btnPickFromOrder.Enabled = !_noteId.HasValue && !_sourceOrderId.HasValue;
        }

        // ============================================
        // PICK FROM ORDER FEATURE
        // ============================================
        private void PickItemsFromOrder()
        {
            if (cmbCustomer.SelectedValue == null) { MessageBox.Show("Επίλεξε πρώτα Πελάτη!"); return; }

            try
            {
                using var conn = new SqlConnection(_connStr);
                int custId = Convert.ToInt32(cmbCustomer.SelectedValue);

                var orders = conn.Query(@"
                    SELECT so.Id, so.OrderNumber, so.OrderDate, so.Total 
                    FROM SalesOrders so 
                    WHERE so.CustomerId = @Cust 
                      AND so.Status IN ('CONFIRMED', 'PARTIALLY_DELIVERED')
                      AND EXISTS (SELECT 1 FROM SalesOrderLines sol WHERE sol.OrderId = so.Id AND sol.Quantity > ISNULL(sol.DeliveredQty, 0))
                    ORDER BY so.OrderDate DESC", new { Cust = custId }).ToList();

                if (orders.Count == 0)
                {
                    MessageBox.Show("Δεν βρέθηκαν ανοιχτές παραγγελίες για αυτόν τον πελάτη.", "Πληροφορία");
                    return;
                }

                var selForm = new Form { Text = "Επιλογή Παραγγελίας", Size = new Size(400, 300), StartPosition = FormStartPosition.CenterParent };
                var lbx = new ListBox { Dock = DockStyle.Fill };
                foreach (var o in orders) lbx.Items.Add($"{o.OrderNumber} ({o.OrderDate:d}) - {o.Total:C}");

                var btnOk = new Button { Text = "OK", Dock = DockStyle.Bottom, Height = 40 };
                btnOk.Click += (s, e) => {
                    if (lbx.SelectedIndex >= 0)
                    {
                        var selectedOrder = orders[lbx.SelectedIndex];
                        LoadOrderLines(selectedOrder.Id);
                        _sourceOrderId = selectedOrder.Id;
                        selForm.DialogResult = DialogResult.OK;
                        selForm.Close();
                    }
                };

                selForm.Controls.Add(lbx);
                selForm.Controls.Add(btnOk);
                selForm.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadOrderLines(int orderId)
        {
            _isLoading = true;
            try
            {
                using var conn = new SqlConnection(_connStr);
                var lines = conn.Query(@"
                    SELECT sol.ItemId, i.Code, sol.Description, sol.Quantity as QtyOrd, 
                           sol.Uom, i.VatRate, i.Weight,
                           ISNULL(sol.DeliveredQty, 0) as AlreadyDelivered
                    FROM SalesOrderLines sol
                    LEFT JOIN Items i ON sol.ItemId = i.Id
                    WHERE sol.OrderId = @Id AND sol.Quantity > ISNULL(sol.DeliveredQty, 0)", new { Id = orderId }).ToList();

                dgvLines.Rows.Clear();
                int ln = 1;
                foreach (var l in lines)
                {
                    decimal remaining = l.QtyOrd - l.AlreadyDelivered;

                    decimal avail = 0;
                    try
                    {
                        avail = conn.ExecuteScalar<decimal?>($@"
                            SELECT ISNULL(SUM(sv.AvailableQty),0) 
                            FROM StockVariants sv 
                            JOIN ItemVariants iv ON sv.ItemVariantId = iv.Id 
                            WHERE iv.ItemId = @ItemId AND sv.WarehouseId = @Wh",
                           new { ItemId = l.ItemId, Wh = cmbWarehouse.SelectedValue != null ? Convert.ToInt32(cmbWarehouse.SelectedValue) : 1 }) ?? 0;
                    }
                    catch { }

                    dgvLines.Rows.Add(ln++, l.ItemId, l.Code, l.Description, l.QtyOrd, remaining, l.Uom, "", "", l.Weight, avail);
                }
                CalculateTotals();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης παραγγελίας:\n" + ex.Message); }
            _isLoading = false;
        }

        private void LoadExistingNote(int id)
        {
            _isLoading = true;
            try
            {
                // Note: Since we are migrating to Documents table, this load method needs adaptation
                // For now, keeping it simple or commenting out until full migration is done
                /*
                using var conn = new SqlConnection(GetConnectionString());
                var n = conn.QueryFirstOrDefault("SELECT * FROM DeliveryNotes WHERE Id = @Id", new { Id = id });
                if (n == null) return;
                ...
                */
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message); }
            _isLoading = false;
        }

        private void LoadFromOrder(int orderId)
        {
            // Same as above, keep existing logic but ensure it uses correct connection strings if needed
            // Or refactor to use the same helper methods
        }

        // ============================================
        // HELPERS UI
        // ============================================
        private Panel CreatePanel(string title, int height, Color color)
        {
            var p = new Panel { Dock = DockStyle.Top, Height = height, BackColor = Color.White, Padding = new Padding(10, 8, 10, 8) };
            p.Controls.Add(new Label { Text = title, Location = new Point(10, 3), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = color });
            return p;
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
        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
            => new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };

        // ============================================
        // LOAD LOGIC
        // ============================================
        private void DeliveryNoteForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            LoadStatuses();
            LoadCustomers();
            LoadWarehouses();
            LoadDocumentTypes(); // New loader for DocType

            if (_noteId.HasValue) LoadExistingNote(_noteId.Value);
            else if (_sourceOrderId.HasValue) LoadFromOrder(_sourceOrderId.Value);
            else
            {
                if (cmbDocType.Items.Count > 0) cmbDocType.SelectedIndex = 0;
            }

            _isLoading = false;
        }

        private void LoadDocumentTypes()
        {
            try { using var conn = new SqlConnection(_connStr); var t = conn.Query("SELECT Id, Code, Name FROM DocumentTypes WHERE IsInventory = 1 AND IsActive = 1 ORDER BY Name").ToList(); cmbDocType.DataSource = t; cmbDocType.DisplayMember = "Name"; cmbDocType.ValueMember = "Id"; }
            catch { }
        }

        private void LoadStatuses()
        {
            cmbStatus.DataSource = new BindingList<KeyValuePair<string, string>>(StatusMap.ToList());
            cmbStatus.DisplayMember = "Value";
            cmbStatus.ValueMember = "Key";
            cmbStatus.SelectedValue = "DRAFT";
        }

        private void LoadCustomers()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var c = conn.Query("SELECT Id, Epwnymia FROM Customers WHERE Energo = 1 ORDER BY Epwnymia").ToList();
                cmbCustomer.DataSource = c;
                cmbCustomer.DisplayMember = "Epwnymia";
                cmbCustomer.ValueMember = "Id";
            }
            catch { }
        }

        private void LoadWarehouses()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var w = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive = 1 ORDER BY Name").ToList();
                cmbWarehouse.DataSource = w;
                cmbWarehouse.DisplayMember = "Name";
                cmbWarehouse.ValueMember = "Id";
                if (cmbWarehouse.Items.Count > 0) cmbWarehouse.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadCustomerDefaults()
        {
            if (_isLoading || cmbCustomer.SelectedValue == null) return;
            try
            {
                using var conn = new SqlConnection(_connStr);
                var cust = conn.QueryFirstOrDefault("SELECT Dieuthinsi, Poli, Tilefono FROM Customers WHERE Id = @Id", new { Id = Convert.ToInt32(cmbCustomer.SelectedValue) });
                if (cust != null)
                {
                    txtShippingAddr.Text = $"{cust.Dieuthinsi}, {cust.Poli}";
                    txtPhone.Text = cust.Tilefono ?? "";
                }
            }
            catch { }
        }
    }
}