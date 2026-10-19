using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class GoodsReceiptForm : Form
    {
        private ComboBox cmbPurchaseOrder, cmbWarehouse;
        private DateTimePicker dtpReceiptDate;
        private TextBox txtReceiptNumber, txtNotes, txtSupplier;
        private DataGridView dgvLines;
        private Label lblTotalQty, lblTotalValue;
        private Button btnSave, btnCancel;
        private int? _receiptId;
        public int? PreSelectedPurchaseOrderId { get; set; }
        public GoodsReceiptForm(int? receiptId = null)
        {
            _receiptId = receiptId;
            InitializeUI();
            this.Load += GoodsReceiptForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📦 Παραλαβή Εμπορευμάτων";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1100, 650);

            // ============================================
            // HEADER
            // ============================================
            var pnlHeader = new Panel { Dock = DockStyle.Top, Height = 110, BackColor = Color.White, Padding = new Padding(15, 10, 15, 10), Margin = new Padding(0, 0, 0, 5) };
            pnlHeader.Controls.Add(new Label { Text = "📦 ΣΤΟΙΧΕΙΑ ΠΑΡΑΛΑΒΗΣ", Location = new Point(10, 3), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(230, 126, 34) });

            int y = 25, lx = 10;

            AddLabel(pnlHeader, "Αριθμός:", lx, y);
            txtReceiptNumber = new TextBox { Location = new Point(lx + 70, y), Size = new Size(130, 26), ReadOnly = true, BackColor = Color.LightYellow, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle };
            pnlHeader.Controls.Add(txtReceiptNumber);
            lx += 70 + 130 + 15;

            AddLabel(pnlHeader, "Ημερομηνία:", lx, y);
            dtpReceiptDate = new DateTimePicker { Location = new Point(lx + 85, y), Size = new Size(130, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            pnlHeader.Controls.Add(dtpReceiptDate);
            lx += 85 + 130 + 15;

            AddLabel(pnlHeader, "Αποθήκη:", lx, y);
            cmbWarehouse = new ComboBox { Location = new Point(lx + 70, y), Size = new Size(180, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            pnlHeader.Controls.Add(cmbWarehouse);

            // Γραμμή 2
            y = 62; lx = 10;

            AddLabel(pnlHeader, "Παραγγελία:*", lx, y);
            cmbPurchaseOrder = new ComboBox { Location = new Point(lx + 90, y), Size = new Size(340, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbPurchaseOrder.SelectedIndexChanged += (s, e) => LoadOrderLines();
            pnlHeader.Controls.Add(cmbPurchaseOrder);
            lx += 90 + 340 + 15;

            AddLabel(pnlHeader, "Προμηθευτής:", lx, y);
            txtSupplier = new TextBox { Location = new Point(lx + 100, y), Size = new Size(250, 26), ReadOnly = true, BackColor = Color.FromArgb(250, 250, 250), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle };
            pnlHeader.Controls.Add(txtSupplier);
            lx += 100 + 250 + 15;

            AddLabel(pnlHeader, "Σημειώσεις:", lx, y);
            txtNotes = new TextBox { Location = new Point(lx + 85, y), Size = new Size(250, 26), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle };
            pnlHeader.Controls.Add(txtNotes);

            this.Controls.Add(pnlHeader);

            // ============================================
            // GRID ΓΡΑΜΜΩΝ
            // ============================================
            dgvLines = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 9.5F),
                RowTemplate = { Height = 30 }
            };

            dgvLines.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(3)
            };

            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "POLineId", HeaderText = "Γραμμή ΠΑ", Width = 80, Visible = false });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemId", HeaderText = "ItemId", Width = 50, Visible = false });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Uom", HeaderText = "Uom", Width = 50, Visible = false }); // ✅ κρυφή για UnitOfMeasureId lookup
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", HeaderText = "Κωδικός", Width = 110, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Περιγραφή", Width = 300, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderedQty", HeaderText = "Παραγγελθέν", Width = 100, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(240, 240, 240) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReceivedSoFar", HeaderText = "Παραληφθέν", Width = 100, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(240, 240, 240) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "PendingQty", HeaderText = "Εκκρεμεί", Width = 100, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(255, 243, 205) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReceiptQty", HeaderText = "Ποσότητα Παραλαβής", Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), BackColor = Color.FromArgb(212, 237, 218) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitCost", HeaderText = "Κόστος €", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalCost", HeaderText = "Σύνολο €", Width = 110, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });

            dgvLines.CellValueChanged += DgvLines_CellValueChanged;

            // ============================================
            // FOOTER
            // ============================================
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 12, 15, 12) };

            lblTotalQty = new Label { Text = "📊 Συνολική Ποσότητα: 0,00", Location = new Point(15, 18), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(230, 126, 34), BackColor = Color.FromArgb(253, 235, 208), Padding = new Padding(12, 8, 12, 8) };
            pnlFooter.Controls.Add(lblTotalQty);

            lblTotalValue = new Label { Text = "💰 Συνολική Αξία: 0,00 €", Location = new Point(300, 18), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113), BackColor = Color.FromArgb(209, 231, 221), Padding = new Padding(12, 8, 12, 8) };
            pnlFooter.Controls.Add(lblTotalValue);

            btnSave = new Button { Text = "💾 Ολοκλήρωση Παραλαβής", Location = new Point(900, 15), Size = new Size(220, 42), BackColor = Color.FromArgb(25, 135, 84), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnSave.Click += (s, e) => SaveReceipt();
            pnlFooter.Controls.Add(btnSave);

            btnCancel = new Button { Text = "❌ Ακύρωση", Location = new Point(1130, 15), Size = new Size(120, 42), BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnCancel.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnCancel);

            this.Controls.Add(pnlFooter);
            this.Controls.Add(dgvLines);
            this.Controls.Add(pnlHeader);
            dgvLines.BringToFront();
        }

        private void AddLabel(Panel p, string text, int x, int y)
        {
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 4), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
        }

        // ✅ Safe helper για CurrentUserId (fallback 1 αν δεν ορίζεται)
        private int GetUserId()
        {
            try { return Program.CurrentUserId; }
            catch { return 1; }
        }

        private void GoodsReceiptForm_Load(object sender, EventArgs e)
        {
            LoadWarehouses();
            LoadPurchaseOrders();
            GenerateNumber();
        }

        private void LoadWarehouses()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var w = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive = 1 ORDER BY Name").ToList();
                cmbWarehouse.DataSource = w;
                cmbWarehouse.DisplayMember = "Name";
                cmbWarehouse.ValueMember = "Id";
            }
            catch { }
        }

        private void LoadPurchaseOrders()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var orders = conn.Query(@"
                    SELECT po.Id, po.OrderNumber, s.SupplierName,
                           (SELECT ISNULL(SUM(pol.Quantity - ISNULL(pol.ReceivedQty,0)), 0) FROM PurchaseOrderLines pol WHERE pol.OrderId = po.Id) AS Pending
                    FROM PurchaseOrders po
                    LEFT JOIN Suppliers s ON po.SupplierId = s.Id
                    WHERE po.Status NOT IN ('Received', 'Closed', 'Cancelled')
                    ORDER BY po.OrderDate DESC").ToList();

                cmbPurchaseOrder.Items.Clear();
                cmbPurchaseOrder.Items.Add(new OrderItem { Id = 0, Display = "— Επίλεξε Παραγγελία —" });
                foreach (var o in orders)
                    cmbPurchaseOrder.Items.Add(new OrderItem { Id = o.Id, Display = $"{o.OrderNumber} - {o.SupplierName} (Εκκρεμεί: {o.Pending:N2})" });
                cmbPurchaseOrder.DisplayMember = "Display";
                if (PreSelectedPurchaseOrderId.HasValue)
                {
                    OrderItem match = null;

                    foreach (object obj in cmbPurchaseOrder.Items)
                    {
                        if (obj is OrderItem oi && oi.Id == PreSelectedPurchaseOrderId.Value)
                        {
                            match = oi;
                            break;
                        }
                    }

                    if (match != null)
                    {
                        cmbPurchaseOrder.SelectedItem = match;
                        return;
                    }
                }
            }
            catch { }
        }

        private class OrderItem
        {
            public int Id { get; set; }
            public string Display { get; set; }
            public override string ToString() => Display;
        }

        private void GenerateNumber()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                int next = conn.ExecuteScalar<int>("SELECT ISNULL(MAX(Id), 0) + 1 FROM GoodsReceipts");
                txtReceiptNumber.Text = $"ΠΑΡ-{DateTime.Now:yyyy}-{next:D4}";
            }
            catch { }
        }

        private void LoadOrderLines()
        {
            dgvLines.Rows.Clear();
            txtSupplier.Text = "";
            var order = cmbPurchaseOrder.SelectedItem as OrderItem;
            if (order == null || order.Id == 0) return;

            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                var po = conn.QueryFirstOrDefault("SELECT SupplierId FROM PurchaseOrders WHERE Id = @Id", new { Id = order.Id });
                if (po?.SupplierId != null)
                {
                    var sup = conn.QueryFirstOrDefault("SELECT SupplierName FROM Suppliers WHERE Id = @Id", new { Id = po.SupplierId });
                    txtSupplier.Text = sup?.SupplierName ?? "";
                }

                // ✅ Φέρνω και Uom (για UnitOfMeasureId lookup)
                var lines = conn.Query(@"
                    SELECT pol.Id, pol.ItemId, pol.Description, pol.Quantity, ISNULL(pol.ReceivedQty,0) AS ReceivedQty, pol.UnitPrice, pol.Uom, i.Code AS ItemCode
                    FROM PurchaseOrderLines pol
                    LEFT JOIN Items i ON pol.ItemId = i.Id
                    WHERE pol.OrderId = @Id AND (pol.Quantity - ISNULL(pol.ReceivedQty,0)) > 0
                    ORDER BY pol.Id", new { Id = order.Id }).ToList();

                if (lines.Count == 0)
                {
                    MessageBox.Show("Δεν υπάρχουν εκκρεμείς γραμμές σε αυτή την παραγγελία!", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var l in lines)
                {
                    decimal pending = l.Quantity - l.ReceivedQty;
                    // ✅ Σειρά: POLineId, ItemId, Uom, ItemCode, Description, Ordered, Received, Pending, ReceiptQty, UnitCost, TotalCost
                    dgvLines.Rows.Add(l.Id, l.ItemId, l.Uom ?? "", l.ItemCode, l.Description, l.Quantity, l.ReceivedQty, pending, pending, l.UnitPrice, pending * l.UnitPrice);
                }

                CalculateTotals();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης γραμμών:\n" + ex.Message); }
        }

        private void DgvLines_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var row = dgvLines.Rows[e.RowIndex];
            var col = dgvLines.Columns[e.ColumnIndex].Name;

            if (col == "ReceiptQty")
            {
                decimal qty = decimal.TryParse(row.Cells["ReceiptQty"].Value?.ToString(), out var q) ? q : 0;
                decimal pending = decimal.TryParse(row.Cells["PendingQty"].Value?.ToString(), out var p) ? p : 0;
                if (qty > pending)
                {
                    row.Cells["ReceiptQty"].Value = pending;
                    MessageBox.Show($"Η ποσότητα δεν μπορεί να ξεπερνά τα εκκρεμή ({pending:N2})!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    qty = pending;
                }
                if (qty < 0) row.Cells["ReceiptQty"].Value = 0;
            }

            if (col == "ReceiptQty" || col == "UnitCost")
            {
                decimal qty = decimal.TryParse(row.Cells["ReceiptQty"].Value?.ToString(), out var q) ? q : 0;
                decimal cost = decimal.TryParse(row.Cells["UnitCost"].Value?.ToString(), out var c) ? c : 0;
                row.Cells["TotalCost"].Value = qty * cost;
                CalculateTotals();
            }
        }

        private void CalculateTotals()
        {
            decimal qty = 0, val = 0;
            foreach (DataGridViewRow r in dgvLines.Rows)
            {
                qty += decimal.TryParse(r.Cells["ReceiptQty"].Value?.ToString(), out var q) ? q : 0;
                val += decimal.TryParse(r.Cells["TotalCost"].Value?.ToString(), out var v) ? v : 0;
            }
            lblTotalQty.Text = $"📊 Συνολική Ποσότητα: {qty:N2}";
            lblTotalValue.Text = $"💰 Συνολική Αξία: {val:N2} €";
        }

        private void SaveReceipt()
        {
            var order = cmbPurchaseOrder.SelectedItem as OrderItem;
            if (order == null || order.Id == 0) { MessageBox.Show("Επίλεξε Παραγγελία Προμηθευτή!\n\nΗ παραλαβή απαιτεί παραγγελία.", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (cmbWarehouse.SelectedValue == null) { MessageBox.Show("Επίλεξε Αποθήκη!"); return; }
            if (dgvLines.Rows.Count == 0) { MessageBox.Show("Δεν υπάρχουν γραμμές παραλαβής!"); return; }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                int poId = order.Id;
                int supplierId = conn.ExecuteScalar<int>("SELECT SupplierId FROM PurchaseOrders WHERE Id = @Id", new { Id = poId }, tran);

                decimal totalQty = 0, totalValue = 0;
                foreach (DataGridViewRow r in dgvLines.Rows)
                {
                    totalQty += decimal.TryParse(r.Cells["ReceiptQty"].Value?.ToString(), out var q) ? q : 0;
                    totalValue += decimal.TryParse(r.Cells["TotalCost"].Value?.ToString(), out var v) ? v : 0;
                }

                int warehouseId = Convert.ToInt32(cmbWarehouse.SelectedValue);

                // 1. Δημιουργία Παραλαβής (✅ σωστά πεδία GoodsReceipts)
                int receiptId = conn.ExecuteScalar<int>(@"
                    INSERT INTO GoodsReceipts (CompanyId, PurchaseOrderId, SupplierId, DocumentNumber, ReceiptDate, WarehouseId, TotalQty, TotalNet, TotalVat, Total, Status, Notes, ReceivedBy, CreatedAt, UpdatedAt)
                    VALUES (1, @PoId, @SupplierId, @DocNum, @Date, @Wh, @Qty, @Total, 0, @Total, 'Posted', @Notes, @UserId, GETDATE(), GETDATE());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new { PoId = poId, SupplierId = supplierId, DocNum = txtReceiptNumber.Text, Date = dtpReceiptDate.Value, Wh = warehouseId, Qty = totalQty, Total = totalValue, Notes = txtNotes.Text, UserId = GetUserId() }, tran);

                // 2. Γραμμές (✅ σωστά πεδία GoodsReceiptLines)
                int lineNum = 1;
                foreach (DataGridViewRow r in dgvLines.Rows)
                {
                    decimal qty = decimal.TryParse(r.Cells["ReceiptQty"].Value?.ToString(), out var q) ? q : 0;
                    if (qty <= 0) continue;

                    int itemId = r.Cells["ItemId"].Value != null ? Convert.ToInt32(r.Cells["ItemId"].Value) : 0;
                    if (itemId == 0) continue;

                    decimal cost = decimal.TryParse(r.Cells["UnitCost"].Value?.ToString(), out var c) ? c : 0;
                    int? poLineId = r.Cells["POLineId"].Value != null && int.TryParse(r.Cells["POLineId"].Value.ToString(), out var pli) ? pli : (int?)null;
                    string uomCode = r.Cells["Uom"].Value?.ToString() ?? "";

                    // ✅ ItemVariantId (προαιρετικό)
                    int? variantId = conn.ExecuteScalar<int?>("SELECT TOP 1 Id FROM ItemVariants WHERE ItemId = @ItemId", new { ItemId = itemId }, tran);

                    // ✅ UnitOfMeasureId από το πραγματικό Uom της παραγγελίας
                    int? uomId = null;
                    if (!string.IsNullOrWhiteSpace(uomCode))
                    {
                        try { uomId = conn.ExecuteScalar<int?>("SELECT TOP 1 Id FROM UnitsOfMeasure WHERE (Code = @Code OR Name = @Code OR Symbol = @Code) AND IsActive = 1", new { Code = uomCode }, tran); }
                        catch { uomId = null; }
                    }

                    // ✅ Γραμμή παραλαβής - επιστρέφουμε Id για DocumentLineId
                    int grLineId = conn.ExecuteScalar<int>(@"
                        INSERT INTO GoodsReceiptLines (GoodsReceiptId, LineNumber, ItemId, ItemVariantId, Quantity, UnitOfMeasureId, UnitCost, TotalCost, PurchaseOrderLineId, Notes, CreatedAt)
                        VALUES (@Rid, @Ln, @ItemId, @VarId, @Qty, @UomId, @Cost, @Total, @PoLineId, '', GETDATE());
                        SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { Rid = receiptId, Ln = lineNum++, ItemId = itemId, VarId = variantId, Qty = qty, UomId = uomId, Cost = cost, Total = qty * cost, PoLineId = poLineId }, tran);

                    // ✅ Ενημέρωση γραμμής παραγγελίας
                    if (poLineId.HasValue)
                        conn.Execute("UPDATE PurchaseOrderLines SET ReceivedQty = ISNULL(ReceivedQty,0) + @Qty, UpdatedAt = GETDATE() WHERE Id = @Id", new { Qty = qty, Id = poLineId }, tran);

                    // ✅ ΑΥΞΗΣΗ ΣΤΟΚ
                    conn.Execute("UPDATE Items SET Stock = ISNULL(Stock, 0) + @Qty WHERE Id = @ItemId", new { Qty = qty, ItemId = itemId }, tran);
                    try { conn.Execute("UPDATE Items SET CurrentStock = ISNULL(CurrentStock, 0) + @Qty WHERE Id = @ItemId", new { Qty = qty, ItemId = itemId }, tran); } catch { }

                    // ✅ StockVariants
                    try
                    {
                        if (variantId.HasValue)
                        {
                            int exists = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM StockVariants WHERE WarehouseId = @Wh AND ItemVariantId = @Vid", new { Wh = warehouseId, Vid = variantId }, tran);
                            if (exists > 0)
                                conn.Execute("UPDATE StockVariants SET Quantity = Quantity + @Qty, AvailableQty = AvailableQty + @Qty WHERE WarehouseId = @Wh AND ItemVariantId = @Vid", new { Qty = qty, Wh = warehouseId, Vid = variantId }, tran);
                            else
                                conn.Execute("INSERT INTO StockVariants (WarehouseId, ItemVariantId, Quantity, ReservedQty, AvailableQty, CreatedAt, UpdatedAt) VALUES (@Wh, @Vid, @Qty, 0, @Qty, GETDATE(), GETDATE())", new { Wh = warehouseId, Vid = variantId, Qty = qty }, tran);
                        }
                    }
                    catch { }

                    // ✅✅✅ StockTransactions - ΤΩΡΑ ΜΕ ΠΡΑΓΜΑΤΙΚΑ ΠΕΔΙΑ
                    // (ΧΩΡΙΣ DocumentType, ΧΩΡΙΣ UpdatedAt - ΜΕ DocumentLineId & CreatedAt)
                    try
                    {
                        conn.Execute(@"
                            INSERT INTO StockTransactions (CompanyId, WarehouseId, ItemId, TransactionDate, TransactionType, QuantityIn, QuantityOut, UnitCost, TotalCost, DocumentId, DocumentLineId, CreatedAt)
                            VALUES (1, @Wh, @ItemId, @Date, 'PURCHASE_IN', @Qty, 0, @Cost, @Cost * @Qty, @DocId, @DocLineId, GETDATE())",
                            new { Wh = warehouseId, ItemId = itemId, Date = dtpReceiptDate.Value, Qty = qty, Cost = cost, DocId = receiptId, DocLineId = grLineId }, tran);
                    }
                    catch (Exception stEx)
                    {
                        System.Diagnostics.Debug.WriteLine("StockTransactions skip: " + stEx.Message);
                    }
                }

                // 3. Ενημέρωση κατάστασης παραγγελίας
                decimal remaining = conn.ExecuteScalar<decimal>("SELECT ISNULL(SUM(Quantity - ISNULL(ReceivedQty,0)), 0) FROM PurchaseOrderLines WHERE OrderId = @Id", new { Id = poId }, tran);
                if (remaining <= 0)
                    conn.Execute("UPDATE PurchaseOrders SET Status = 'Received', ActualDeliveryDate = @Date, UpdatedAt = GETDATE() WHERE Id = @Id", new { Date = dtpReceiptDate.Value, Id = poId }, tran);
                else
                    conn.Execute("UPDATE PurchaseOrders SET Status = 'PartiallyReceived', UpdatedAt = GETDATE() WHERE Id = @Id", new { Id = poId }, tran);

                tran.Commit();
                MessageBox.Show($"✅ Η παραλαβή ολοκληρώθηκε!\n\nΑριθμός: {txtReceiptNumber.Text}\nΠοσότητα: {totalQty:N2}\nΑξία: {totalValue:N2} €\n\nΤο στοκ ενημερώθηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα αποθήκευσης:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}