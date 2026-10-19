using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class StockAdjustmentForm : Form
    {
        private ComboBox cmbSeries, cmbWarehouse, cmbAdjustmentType, cmbStatus;
        private TextBox txtNumber, txtNotes;
        private DateTimePicker dtpDate;
        private DataGridView dgvLines;
        private Label lblTotalDiff, lblTotalValue;
        private Button btnSave, btnApprove, btnCancel;
        private int? _documentId;

        public StockAdjustmentForm(int? documentId = null)
        {
            _documentId = documentId;
            InitializeUI();
            this.Load += StockAdjustmentForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📉 Διόρθωση / Απώλεια Αποθέματος";
            this.Size = new Size(1400, 850);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            // ============================================
            // 1. HEADER PANEL
            // ============================================
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.White,
                Padding = new Padding(20),
                Margin = new Padding(0, 0, 0, 10)
            };

            int y = 20;
            int labelWidth = 100;
            int controlWidth = 180;
            int gap = 30;

            // Row 1
            var lblSeries = new Label { Text = "Σειρά:", Location = new Point(20, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlHeader.Controls.Add(lblSeries);
            cmbSeries = new ComboBox { Location = new Point(20 + labelWidth, y), Size = new Size(controlWidth, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbSeries.SelectedIndexChanged += (s, e) => GenerateNumber();
            pnlHeader.Controls.Add(cmbSeries);

            var lblNumber = new Label { Text = "Αριθμός:", Location = new Point(20 + labelWidth + controlWidth + gap, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlHeader.Controls.Add(lblNumber);
            txtNumber = new TextBox { Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth, y), Size = new Size(150, 30), ReadOnly = true, BackColor = Color.LightYellow, Font = new Font("Segoe UI", 10F, FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle };
            pnlHeader.Controls.Add(txtNumber);

            var lblDate = new Label { Text = "Ημερομηνία:", Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth + 150 + gap, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlHeader.Controls.Add(lblDate);
            dtpDate = new DateTimePicker { Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth + 150 + gap + labelWidth, y), Size = new Size(160, 30), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10F) };
            pnlHeader.Controls.Add(dtpDate);

            // Row 2
            y = 65;

            var lblWarehouse = new Label { Text = "Αποθήκη:", Location = new Point(20, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(220, 53, 69) };
            pnlHeader.Controls.Add(lblWarehouse);
            cmbWarehouse = new ComboBox { Location = new Point(20 + labelWidth, y), Size = new Size(controlWidth, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbWarehouse.SelectedIndexChanged += (s, e) => RefreshCurrentStockInGrid();
            pnlHeader.Controls.Add(cmbWarehouse);

            var lblType = new Label { Text = "Τύπος:", Location = new Point(20 + labelWidth + controlWidth + gap, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(253, 126, 20) };
            pnlHeader.Controls.Add(lblType);
            cmbAdjustmentType = new ComboBox { Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth, y), Size = new Size(controlWidth, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbAdjustmentType.Items.AddRange(new object[] { "📉 Απώλεια", "📈 Εύρεμα", "🔧 Φθορά", " Λήξη", "🔄 Διόρθωση" });
            cmbAdjustmentType.SelectedIndex = 0;
            pnlHeader.Controls.Add(cmbAdjustmentType);

            var lblStatus = new Label { Text = "Κατάσταση:", Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth + controlWidth + gap, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlHeader.Controls.Add(lblStatus);
            cmbStatus = new ComboBox { Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth + controlWidth + gap + labelWidth, y), Size = new Size(160, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbStatus.Items.AddRange(new object[] { "Πρόχειρο", "Σε Έγκριση", "Εγκεκριμένο", "Απορριφθέν" });
            cmbStatus.SelectedIndex = 0;
            pnlHeader.Controls.Add(cmbStatus);

            this.Controls.Add(pnlHeader);

            // ============================================
            // 2. NOTES PANEL
            // ============================================
            var pnlNotes = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 15),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblNotes = new Label { Text = "Σημειώσεις / Αιτιολόγηση:", Location = new Point(20, 18), Size = new Size(180, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(108, 117, 125) };
            pnlNotes.Controls.Add(lblNotes);
            txtNotes = new TextBox { Location = new Point(210, 15), Size = new Size(1090, 32), Font = new Font("Segoe UI", 10F), BorderStyle = BorderStyle.FixedSingle };
            pnlNotes.Controls.Add(txtNotes);
            this.Controls.Add(pnlNotes);

            // ============================================
            // 3. GRID
            // ============================================
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
                Font = new Font("Segoe UI", 10F),
                RowTemplate = { Height = 35 }
            };

            dgvLines.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
            };

            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", HeaderText = "Κωδικός", Width = 140 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemName", HeaderText = "Περιγραφή", Width = 350, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantityBefore", HeaderText = "Πριν", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(240, 240, 240) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantityAfter", HeaderText = "Μετά", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(212, 237, 218) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantityDiff", HeaderText = "Διαφορά", Width = 120, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(220, 53, 69) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitCost", HeaderText = "Τιμή €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalValue", HeaderText = "Αξία €", Width = 130, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Reason", HeaderText = "Αιτιολόγηση", Width = 180 });

            dgvLines.CellValueChanged += DgvLines_CellValueChanged;
            dgvLines.RowsAdded += DgvLines_RowsAdded;
            dgvLines.UserDeletingRow += (s, e) => CalculateTotals();

            this.Controls.Add(dgvLines);

            // ============================================
            // 4. FOOTER
            // ============================================
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20),
                Margin = new Padding(0, 10, 0, 0)
            };

            lblTotalDiff = new Label { Text = "📊 Συνολική Διαφορά: 0,00", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(220, 53, 69), Location = new Point(20, 22), AutoSize = true, BackColor = Color.FromArgb(255, 240, 242), Padding = new Padding(15, 10, 15, 10) };
            pnlFooter.Controls.Add(lblTotalDiff);

            lblTotalValue = new Label { Text = "💰 Συνολική Αξία: 0,00 €", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(220, 53, 69), Location = new Point(320, 22), AutoSize = true, BackColor = Color.FromArgb(255, 240, 242), Padding = new Padding(15, 10, 15, 10) };
            pnlFooter.Controls.Add(lblTotalValue);

            btnSave = new Button { Text = "💾 Αποθήκευση", Location = new Point(800, 20), Size = new Size(170, 45), BackColor = Color.FromArgb(13, 110, 253), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnSave.Click += (s, e) => SaveDocument(false);
            pnlFooter.Controls.Add(btnSave);

            btnApprove = new Button { Text = "✅ Έγκριση", Location = new Point(980, 20), Size = new Size(160, 45), BackColor = Color.FromArgb(25, 135, 84), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnApprove.Click += (s, e) => ApproveDocument();
            pnlFooter.Controls.Add(btnApprove);

            btnCancel = new Button { Text = "❌ Ακύρωση", Location = new Point(1150, 20), Size = new Size(150, 45), BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnCancel.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnCancel);

            this.Controls.Add(pnlFooter);
        }

        private void StockAdjustmentForm_Load(object sender, EventArgs e) { LoadLookups(); if (_documentId.HasValue) LoadDocument(_documentId.Value); else GenerateNumber(); }

        private void LoadLookups()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                // 1. Φόρτωση Series
                try
                {
                    var s = conn.Query("SELECT Code, Name FROM DocumentSeries WHERE DocumentType='StockAdjustment' AND IsActive=1").ToList();
                    cmbSeries.DataSource = s;
                    cmbSeries.DisplayMember = "Name";
                    cmbSeries.ValueMember = "Code";
                }
                catch
                {
                    cmbSeries.Items.Clear();
                    cmbSeries.Items.Add("ADJ - Διορθώσεις");
                    cmbSeries.SelectedIndex = 0;
                }

                // 2. Φόρτωση Warehouses
                try
                {
                    var w = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive=1 ORDER BY Name").ToList();

                    if (w.Count == 0)
                    {
                        conn.Execute(@"
                    IF NOT EXISTS (SELECT * FROM Warehouses)
                    BEGIN
                        INSERT INTO Warehouses (CompanyId, Name, Code, IsActive, CreatedAt, UpdatedAt) VALUES 
                        (1, 'Κεντρική Αποθήκη', 'WH001', 1, GETDATE(), GETDATE()),
                        (1, 'Αποθήκη Βορρά', 'WH002', 1, GETDATE(), GETDATE()),
                        (1, 'Αποθήκη Νότου', 'WH003', 1, GETDATE(), GETDATE());
                    END");
                        w = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive=1 ORDER BY Name").ToList();
                    }

                    cmbWarehouse.DataSource = w;
                    cmbWarehouse.DisplayMember = "Name";
                    cmbWarehouse.ValueMember = "Id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("️ Σφάλμα φόρτωσης αποθηκών:\n" + ex.Message +
                        "\n\nΒεβαιώσου ότι υπάρχει ο πίνακας Warehouses στη βάση.",
                        "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα σύνδεσης:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateNumber()
        {
            if (cmbSeries.SelectedValue == null) return;
            string c = cmbSeries.SelectedValue.ToString();
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var n = conn.ExecuteScalar<int>("SELECT ISNULL(MAX(CAST(SUBSTRING(AdjustmentNumber, LEN(@S)+2, LEN(AdjustmentNumber)-LEN(@S)-1) AS INT)), 0) + 1 FROM StockAdjustments WHERE AdjustmentNumber LIKE @S + '-%'", new { S = c });
                txtNumber.Text = $"{c}-{n:D4}";
            }
            catch { }
        }

        private int GetSelectedWarehouseId()
        {
            if (cmbWarehouse.SelectedValue == null) return 0;
            try { return Convert.ToInt32(cmbWarehouse.SelectedValue); }
            catch { return 0; }
        }

        private void RefreshCurrentStockInGrid()
        {
            int whId = GetSelectedWarehouseId();
            if (whId == 0) return;

            foreach (DataGridViewRow row in dgvLines.Rows)
            {
                if (row.IsNewRow) continue;
                string code = row.Cells["ItemCode"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(code)) continue;

                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    int itemId = conn.ExecuteScalar<int>("SELECT Id FROM Items WHERE Code = @Code", new { Code = code });
                    var stock = conn.QueryFirstOrDefault("SELECT Quantity FROM StockVariants WHERE ItemId = @ItemId AND WarehouseId = @Wh", new { ItemId = itemId, Wh = whId });
                    decimal currentQty = stock != null ? Convert.ToDecimal(stock.Quantity) : 0;
                    row.Cells["QuantityBefore"].Value = currentQty;
                    decimal after = decimal.TryParse(row.Cells["QuantityAfter"].Value?.ToString(), out var a) ? a : currentQty;
                    row.Cells["QuantityDiff"].Value = after - currentQty;
                }
                catch { }
            }
            CalculateTotals();
        }

        private void DgvLines_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
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
                        var item = conn.QueryFirstOrDefault("SELECT Name FROM Items WHERE Code = @Code", new { Code = code });
                        row.Cells["ItemName"].Value = item?.Name ?? "";
                        int whId = GetSelectedWarehouseId();
                        if (whId > 0)
                        {
                            int itemId = conn.ExecuteScalar<int>("SELECT Id FROM Items WHERE Code = @Code", new { Code = code });
                            var stock = conn.QueryFirstOrDefault("SELECT Quantity FROM StockVariants WHERE ItemId = @ItemId AND WarehouseId = @Wh", new { ItemId = itemId, Wh = whId });
                            row.Cells["QuantityBefore"].Value = stock != null ? Convert.ToDecimal(stock.Quantity) : 0;
                        }
                    }
                    catch { }
                }
            }

            if ((col == "QuantityAfter" || col == "QuantityBefore") && !row.IsNewRow)
            {
                decimal before = decimal.TryParse(row.Cells["QuantityBefore"].Value?.ToString(), out var b) ? b : 0;
                decimal after = decimal.TryParse(row.Cells["QuantityAfter"].Value?.ToString(), out var a) ? a : 0;
                row.Cells["QuantityDiff"].Value = after - before;
                decimal cost = decimal.TryParse(row.Cells["UnitCost"].Value?.ToString(), out var c) ? c : 0;
                row.Cells["TotalValue"].Value = Math.Abs(after - before) * cost;
                CalculateTotals();
            }
        }

        private void DgvLines_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) { for (int i = 0; i < e.RowCount; i++) dgvLines.Rows[e.RowIndex + i].Cells["RowNo"].Value = e.RowIndex + i + 1; }

        private void CalculateTotals()
        {
            decimal totalDiff = 0, totalVal = 0;
            foreach (DataGridViewRow r in dgvLines.Rows)
            {
                if (r.IsNewRow) continue;
                totalDiff += decimal.TryParse(r.Cells["QuantityDiff"].Value?.ToString(), out var d) ? d : 0;
                totalVal += decimal.TryParse(r.Cells["TotalValue"].Value?.ToString(), out var v) ? v : 0;
            }
            lblTotalDiff.Text = $"📊 Συνολική Διαφορά: {totalDiff:N2}";
            lblTotalValue.Text = $"💰 Συνολική Αξία: {totalVal:N2} €";
        }

        private void SaveDocument(bool isApproval)
        {
            if (!ValidateForm()) return;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                int docId;
                string status = isApproval ? "Εγκεκριμένο" : (cmbStatus.Text == "Σε Έγκριση" ? "Σε Έγκριση" : "Πρόχειρο");

                if (_documentId.HasValue)
                {
                    conn.Execute(@"UPDATE StockAdjustments SET AdjustmentNumber=@Num, AdjustmentDate=@Date, WarehouseId=@Wh, AdjustmentType=@Type, Status=@Status, Notes=@Notes, UpdatedAt=GETDATE() WHERE Id=@Id",
                        new { Id = _documentId, Num = txtNumber.Text, Date = dtpDate.Value, Wh = GetSelectedWarehouseId(), Type = cmbAdjustmentType.Text, Status = status, Notes = txtNotes.Text }, tran);
                    docId = _documentId.Value;
                    if (!isApproval) conn.Execute("DELETE FROM StockAdjustmentLines WHERE StockAdjustmentId=@Id", new { Id = docId }, tran);
                }
                else
                {
                    int userId = 1; try { userId = Convert.ToInt32(Program.CurrentUserId); } catch { }
                    docId = conn.ExecuteScalar<int>(@"INSERT INTO StockAdjustments (CompanyId, AdjustmentNumber, AdjustmentDate, WarehouseId, AdjustmentType, Reason, TotalQty, TotalValue, Status, Notes, CreatedAt, UpdatedAt) 
                        VALUES (1, @Num, @Date, @Wh, @Type, @Reason, 0, 0, @Status, @Notes, GETDATE(), GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { Num = txtNumber.Text, Date = dtpDate.Value, Wh = GetSelectedWarehouseId(), Type = cmbAdjustmentType.Text, Reason = txtNotes.Text, Status = status, Notes = txtNotes.Text }, tran);
                }

                foreach (DataGridViewRow row in dgvLines.Rows)
                {
                    if (row.IsNewRow) continue;
                    string code = row.Cells["ItemCode"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(code)) continue;

                    int itemId = conn.ExecuteScalar<int>("SELECT Id FROM Items WHERE Code = @Code", new { Code = code }, tran);
                    decimal before = decimal.TryParse(row.Cells["QuantityBefore"].Value?.ToString(), out var b) ? b : 0;
                    decimal after = decimal.TryParse(row.Cells["QuantityAfter"].Value?.ToString(), out var a) ? a : 0;
                    decimal diff = after - before;
                    decimal cost = decimal.TryParse(row.Cells["UnitCost"].Value?.ToString(), out var c) ? c : 0;

                    conn.Execute(@"INSERT INTO StockAdjustmentLines (StockAdjustmentId, ItemId, QuantityBefore, QuantityAfter, QuantityDiff, UnitCost, TotalValue, Reason, CreatedAt) 
                        VALUES (@DocId, @ItemId, @Before, @After, @Diff, @Cost, @Total, @LineReason, GETDATE())",
                        new { DocId = docId, ItemId = itemId, Before = before, After = after, Diff = diff, Cost = cost, Total = Math.Abs(diff) * cost, LineReason = row.Cells["Reason"].Value?.ToString() }, tran);
                }

                tran.Commit();
                _documentId = docId;
                if (!isApproval) MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void ApproveDocument()
        {
            if (!ValidateForm()) return;
            if (MessageBox.Show("Έγκριση διόρθωσης; Θα ενημερωθεί το στοκ.", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            try
            {
                SaveDocument(true);
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                int docId = _documentId.Value;
                int whId = GetSelectedWarehouseId();
                var lines = conn.Query("SELECT * FROM StockAdjustmentLines WHERE StockAdjustmentId=@Id", new { Id = docId }, tran).ToList();

                foreach (var line in lines)
                {
                    conn.Execute("UPDATE StockVariants SET Quantity = @After WHERE WarehouseId=@Wh AND ItemId=@ItemId", new { After = line.QuantityAfter, Wh = whId, ItemId = line.ItemId }, tran);
                    string transType = line.QuantityDiff > 0 ? "ADJUSTMENT_IN" : "ADJUSTMENT_OUT";
                    decimal qty = Math.Abs(line.QuantityDiff);
                    conn.Execute(@"INSERT INTO StockTransactions (CompanyId, WarehouseId, ItemId, TransactionDate, TransactionType, QuantityIn, QuantityOut, UnitCost, TotalCost, DocumentId, DocumentType, CreatedAt, UpdatedAt) 
                        VALUES (1, @Wh, @ItemId, @Date, @Type, @QtyIn, @QtyOut, @Cost, @Cost*@Qty, @DocId, 'StockAdjustment', GETDATE(), GETDATE())",
                        new { Wh = whId, ItemId = line.ItemId, Date = dtpDate.Value, Type = transType, QtyIn = line.QuantityDiff > 0 ? (decimal?)qty : null, QtyOut = line.QuantityDiff < 0 ? (decimal?)qty : null, Cost = line.UnitCost, DocId = docId }, tran);
                }

                conn.Execute("UPDATE StockAdjustments SET Status='Εγκεκριμένο', ApprovalDate=GETDATE() WHERE Id=@Id", new { Id = docId }, tran);
                tran.Commit();

                cmbStatus.Text = "Εγκεκριμένο";
                btnSave.Enabled = false;
                btnApprove.Enabled = false;
                dgvLines.ReadOnly = true;
                dgvLines.AllowUserToAddRows = false;

                MessageBox.Show("✅ Εγκρίθηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private bool ValidateForm()
        {
            if (cmbSeries.SelectedValue == null || string.IsNullOrWhiteSpace(txtNumber.Text)) { MessageBox.Show("Συμπλήρωσε Σειρά/Αριθμό!"); return false; }
            if (GetSelectedWarehouseId() == 0) { MessageBox.Show("Επίλεξε Αποθήκη!"); return false; }
            if (dgvLines.Rows.Count == 1 && dgvLines.Rows[0].IsNewRow) { MessageBox.Show("Πρόσθεσε είδη!"); return false; }
            return true;
        }

        private void LoadDocument(int id)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var doc = conn.QueryFirstOrDefault("SELECT * FROM StockAdjustments WHERE Id=@Id", new { Id = id });
                if (doc == null) return;

                txtNumber.Text = doc.AdjustmentNumber;
                dtpDate.Value = doc.AdjustmentDate;
                cmbWarehouse.SelectedValue = doc.WarehouseId;
                cmbAdjustmentType.Text = doc.AdjustmentType;
                cmbStatus.Text = doc.Status;
                txtNotes.Text = doc.Notes;

                var lines = conn.Query("SELECT * FROM StockAdjustmentLines WHERE StockAdjustmentId=@Id", new { Id = id }).ToList();
                dgvLines.Rows.Clear();
                foreach (var l in lines)
                {
                    var item = conn.QueryFirstOrDefault("SELECT Code, Name FROM Items WHERE Id=@Id", new { Id = l.ItemId });
                    dgvLines.Rows.Add(item?.Code, item?.Name, l.QuantityBefore, l.QuantityAfter, l.QuantityDiff, l.UnitCost, l.TotalValue, l.Reason);
                }
                CalculateTotals();

                if (doc.Status == "Εγκεκριμένο")
                {
                    btnSave.Enabled = false;
                    btnApprove.Enabled = false;
                    dgvLines.ReadOnly = true;
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}