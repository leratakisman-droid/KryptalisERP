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
    public class StockMovementForm : Form
    {
        private ComboBox cmbSeries, cmbFromWarehouse, cmbToWarehouse, cmbStatus;
        private TextBox txtNumber, txtNotes;
        private DateTimePicker dtpDate;
        private DataGridView dgvLines;
        private Label lblTotalQty, lblTotalValue;
        private Button btnSave, btnPost, btnCancel;
        private int? _documentId;

        public StockMovementForm(int? documentId = null)
        {
            _documentId = documentId;
            InitializeUI();
            this.Load += StockMovementForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📦 Παραστατικό Κίνησης Αποθήκης (Μεταφορά)";
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

            // Row 1: Σειρά, Αριθμός, Ημερομηνία
            int y = 20;
            int labelWidth = 100;
            int controlWidth = 180;
            int gap = 30;

            // Σειρά
            var lblSeries = new Label { Text = "Σειρά:", Location = new Point(20, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(33, 37, 41) };
            pnlHeader.Controls.Add(lblSeries);
            cmbSeries = new ComboBox { Location = new Point(20 + labelWidth, y), Size = new Size(controlWidth, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbSeries.SelectedIndexChanged += (s, e) => GenerateNumber();
            pnlHeader.Controls.Add(cmbSeries);

            // Αριθμός
            var lblNumber = new Label { Text = "Αριθμός:", Location = new Point(20 + labelWidth + controlWidth + gap, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlHeader.Controls.Add(lblNumber);
            txtNumber = new TextBox { Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth, y), Size = new Size(150, 30), ReadOnly = true, BackColor = Color.LightYellow, Font = new Font("Segoe UI", 10F, FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle };
            pnlHeader.Controls.Add(txtNumber);

            // Ημερομηνία
            var lblDate = new Label { Text = "Ημερομηνία:", Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth + 150 + gap, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlHeader.Controls.Add(lblDate);
            dtpDate = new DateTimePicker { Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth + 150 + gap + labelWidth, y), Size = new Size(160, 30), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10F) };
            pnlHeader.Controls.Add(dtpDate);

            // Row 2: Από Αποθήκη, Προς Αποθήκη, Κατάσταση
            y = 65;

            var lblFrom = new Label { Text = "Από Αποθήκη:", Location = new Point(20, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(220, 53, 69) };
            pnlHeader.Controls.Add(lblFrom);
            cmbFromWarehouse = new ComboBox { Location = new Point(20 + labelWidth, y), Size = new Size(controlWidth, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            pnlHeader.Controls.Add(cmbFromWarehouse);

            var lblTo = new Label { Text = "Προς Αποθήκη:", Location = new Point(20 + labelWidth + controlWidth + gap, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(25, 135, 84) };
            pnlHeader.Controls.Add(lblTo);
            cmbToWarehouse = new ComboBox { Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth, y), Size = new Size(controlWidth, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            pnlHeader.Controls.Add(cmbToWarehouse);

            var lblStatus = new Label { Text = "Κατάσταση:", Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth + controlWidth + gap, y), Size = new Size(labelWidth, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlHeader.Controls.Add(lblStatus);
            cmbStatus = new ComboBox { Location = new Point(20 + labelWidth + controlWidth + gap + labelWidth + controlWidth + gap + labelWidth, y), Size = new Size(160, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbStatus.Items.AddRange(new object[] { "Πρόχειρο", "Οριστικοποιημένο" });
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

            var lblNotes = new Label { Text = "Παρατηρήσεις:", Location = new Point(20, 18), Size = new Size(120, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(108, 117, 125) };
            pnlNotes.Controls.Add(lblNotes);
            txtNotes = new TextBox { Location = new Point(150, 15), Size = new Size(1150, 32), Font = new Font("Segoe UI", 10F), BorderStyle = BorderStyle.FixedSingle };
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
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
            };

            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", HeaderText = "Κωδικός", Width = 140 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemName", HeaderText = "Περιγραφή", Width = 350, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Ποσότητα", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReceivedQty", HeaderText = "Ληφθείσα", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "PendingQty", HeaderText = "Εκκρεμότητα", Width = 120, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(255, 243, 205) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitCost", HeaderText = "Τιμή €", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalValue", HeaderText = "Σύνολο €", Width = 140, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });

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

            lblTotalQty = new Label { Text = "📊 Σύνολο Ποσότητας: 0,00", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(13, 110, 253), Location = new Point(20, 22), AutoSize = true, BackColor = Color.FromArgb(207, 226, 255), Padding = new Padding(15, 10, 15, 10) };
            pnlFooter.Controls.Add(lblTotalQty);

            lblTotalValue = new Label { Text = "💰 Συνολική Αξία: 0,00 €", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(25, 135, 84), Location = new Point(320, 22), AutoSize = true, BackColor = Color.FromArgb(209, 231, 221), Padding = new Padding(15, 10, 15, 10) };
            pnlFooter.Controls.Add(lblTotalValue);

            btnSave = new Button { Text = "💾 Αποθήκευση", Location = new Point(800, 20), Size = new Size(170, 45), BackColor = Color.FromArgb(13, 110, 253), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnSave.Click += (s, e) => SaveDocument(false);
            pnlFooter.Controls.Add(btnSave);

            btnPost = new Button { Text = "🚀 Οριστικοποίηση", Location = new Point(980, 20), Size = new Size(180, 45), BackColor = Color.FromArgb(25, 135, 84), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnPost.Click += (s, e) => PostDocument();
            pnlFooter.Controls.Add(btnPost);

            btnCancel = new Button { Text = "❌ Ακύρωση", Location = new Point(1170, 20), Size = new Size(150, 45), BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnCancel.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnCancel);

            this.Controls.Add(pnlFooter);
        }

        private void StockMovementForm_Load(object sender, EventArgs e) { LoadLookups(); if (_documentId.HasValue) LoadDocument(_documentId.Value); else GenerateNumber(); }

        private void LoadLookups()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                // 1. Φόρτωση Series
                try
                {
                    var s = conn.Query("SELECT Code, Name FROM DocumentSeries WHERE DocumentType='StockTransfer' AND IsActive=1").ToList();
                    cmbSeries.DataSource = s;
                    cmbSeries.DisplayMember = "Name";
                    cmbSeries.ValueMember = "Code";
                }
                catch
                {
                    // Αν δεν υπάρχει ο πίνακας, βάλε χειροκίνητα
                    cmbSeries.Items.Clear();
                    cmbSeries.Items.Add("TR - Μεταφορές");
                    cmbSeries.SelectedIndex = 0;
                }

                // 2. Φόρτωση Warehouses
                try
                {
                    var w = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive=1 ORDER BY Name").ToList();

                    if (w.Count == 0)
                    {
                        // Δημιουργία δοκιμαστικών αποθηκών
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

                    cmbFromWarehouse.DataSource = w;
                    cmbFromWarehouse.DisplayMember = "Name";
                    cmbFromWarehouse.ValueMember = "Id";

                    cmbToWarehouse.DataSource = w;
                    cmbToWarehouse.DisplayMember = "Name";
                    cmbToWarehouse.ValueMember = "Id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("⚠️ Σφάλμα φόρτωσης αποθηκών:\n" + ex.Message +
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
                var n = conn.ExecuteScalar<int>("SELECT ISNULL(MAX(CAST(SUBSTRING(TransferNumber, LEN(@S)+2, LEN(TransferNumber)-LEN(@S)-1) AS INT)), 0) + 1 FROM StockTransfers WHERE TransferNumber LIKE @S + '-%'", new { S = c });
                txtNumber.Text = $"{c}-{n:D4}";
            }
            catch { }
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
                    }
                    catch { }
                }
            }

            if ((col == "Quantity" || col == "UnitCost") && !row.IsNewRow)
            {
                decimal q = decimal.TryParse(row.Cells["Quantity"].Value?.ToString(), out var v1) ? v1 : 0;
                decimal p = decimal.TryParse(row.Cells["UnitCost"].Value?.ToString(), out var v2) ? v2 : 0;
                row.Cells["TotalValue"].Value = q * p;
                CalculateTotals();
            }
        }

        private void DgvLines_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) { for (int i = 0; i < e.RowCount; i++) dgvLines.Rows[e.RowIndex + i].Cells["RowNo"].Value = e.RowIndex + i + 1; }

        private void CalculateTotals()
        {
            decimal tQ = 0, tV = 0;
            foreach (DataGridViewRow r in dgvLines.Rows)
            {
                if (r.IsNewRow) continue;
                tQ += decimal.TryParse(r.Cells["Quantity"].Value?.ToString(), out var q) ? q : 0;
                tV += decimal.TryParse(r.Cells["TotalValue"].Value?.ToString(), out var v) ? v : 0;
            }
            lblTotalQty.Text = $"📊 Σύνολο Ποσότητας: {tQ:N2}";
            lblTotalValue.Text = $"💰 Συνολική Αξία: {tV:N2} €";
        }

        private void SaveDocument(bool isPost)
        {
            if (!ValidateForm()) return;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                int docId;
                string status = isPost ? "Οριστικοποιημένο" : "Πρόχειρο";

                if (_documentId.HasValue)
                {
                    conn.Execute(@"UPDATE StockTransfers SET TransferNumber=@Num, TransferDate=@Date, FromWarehouseId=@From, ToWarehouseId=@To, Status=@Status, Notes=@Notes, UpdatedAt=GETDATE() WHERE Id=@Id",
                        new { Id = _documentId, Num = txtNumber.Text, Date = dtpDate.Value, From = GetSelectedWarehouseId(cmbFromWarehouse), To = GetSelectedWarehouseId(cmbToWarehouse), Status = status, Notes = txtNotes.Text }, tran);
                    docId = _documentId.Value;
                    if (!isPost) conn.Execute("DELETE FROM StockTransferLines WHERE StockTransferId=@Id", new { Id = docId }, tran);
                }
                else
                {
                    docId = conn.ExecuteScalar<int>(@"INSERT INTO StockTransfers (CompanyId, TransferNumber, TransferDate, FromWarehouseId, ToWarehouseId, Status, TotalQty, Notes, CreatedAt, UpdatedAt) 
                        VALUES (1, @Num, @Date, @From, @To, @Status, 0, @Notes, GETDATE(), GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { Num = txtNumber.Text, Date = dtpDate.Value, From = GetSelectedWarehouseId(cmbFromWarehouse), To = GetSelectedWarehouseId(cmbToWarehouse), Status = status, Notes = txtNotes.Text }, tran);
                }

                foreach (DataGridViewRow row in dgvLines.Rows)
                {
                    if (row.IsNewRow) continue;
                    string code = row.Cells["ItemCode"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(code)) continue;

                    int itemId = conn.ExecuteScalar<int>("SELECT Id FROM Items WHERE Code = @Code", new { Code = code }, tran);
                    decimal qty = decimal.TryParse(row.Cells["Quantity"].Value?.ToString(), out var q) ? q : 0;
                    decimal recv = decimal.TryParse(row.Cells["ReceivedQty"].Value?.ToString(), out var r) ? r : 0;
                    decimal cost = decimal.TryParse(row.Cells["UnitCost"].Value?.ToString(), out var c) ? c : 0;

                    conn.Execute(@"INSERT INTO StockTransferLines (StockTransferId, ItemId, Quantity, ReceivedQty, PendingQty, UnitCost, Notes, CreatedAt) 
                        VALUES (@DocId, @ItemId, @Qty, @Recv, @Pend, @Cost, @LineNotes, GETDATE())",
                        new { DocId = docId, ItemId = itemId, Qty = qty, Recv = recv, Pend = qty - recv, Cost = cost, LineNotes = "" }, tran);
                }

                tran.Commit();
                _documentId = docId;
                if (!isPost) MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void PostDocument()
        {
            if (!ValidateForm()) return;
            if (MessageBox.Show("Η οριστικοποίηση θα μετακινήσει το στοκ. Συνέχεια;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                if (!_documentId.HasValue) SaveDocument(true);

                int docId = _documentId.Value;
                int fromWh = GetSelectedWarehouseId(cmbFromWarehouse);
                int toWh = GetSelectedWarehouseId(cmbToWarehouse);

                var lines = conn.Query("SELECT * FROM StockTransferLines WHERE StockTransferId=@Id", new { Id = docId }).ToList();

                foreach (var line in lines)
                {
                    decimal qtyToMove = line.Quantity;
                    conn.Execute("UPDATE StockVariants SET Quantity = Quantity - @Qty WHERE WarehouseId=@Wh AND ItemId=@ItemId", new { Qty = qtyToMove, Wh = fromWh, ItemId = line.ItemId }, tran);
                    conn.Execute("UPDATE StockVariants SET Quantity = Quantity + @Qty WHERE WarehouseId=@Wh AND ItemId=@ItemId", new { Qty = qtyToMove, Wh = toWh, ItemId = line.ItemId }, tran);

                    conn.Execute(@"INSERT INTO StockTransactions (CompanyId, WarehouseId, ItemId, TransactionDate, TransactionType, QuantityIn, QuantityOut, UnitCost, TotalCost, DocumentId, DocumentType, CreatedAt, UpdatedAt) 
                        VALUES (1, @Wh, @ItemId, @Date, 'TRANSFER_OUT', 0, @Qty, @Cost, @Cost*@Qty, @DocId, 'StockTransfer', GETDATE(), GETDATE())",
                        new { Wh = fromWh, ItemId = line.ItemId, Date = dtpDate.Value, Qty = qtyToMove, Cost = line.UnitCost, DocId = docId }, tran);

                    conn.Execute(@"INSERT INTO StockTransactions (CompanyId, WarehouseId, ItemId, TransactionDate, TransactionType, QuantityIn, QuantityOut, UnitCost, TotalCost, DocumentId, DocumentType, CreatedAt, UpdatedAt) 
                        VALUES (1, @Wh, @ItemId, @Date, 'TRANSFER_IN', @Qty, 0, @Cost, @Cost*@Qty, @DocId, 'StockTransfer', GETDATE(), GETDATE())",
                        new { Wh = toWh, ItemId = line.ItemId, Date = dtpDate.Value, Qty = qtyToMove, Cost = line.UnitCost, DocId = docId }, tran);
                }

                conn.Execute("UPDATE StockTransfers SET Status='Οριστικοποιημένο', ActualDate=GETDATE() WHERE Id=@Id", new { Id = docId }, tran);
                tran.Commit();

                cmbStatus.Text = "Οριστικοποιημένο";
                btnSave.Enabled = false;
                btnPost.Enabled = false;
                dgvLines.ReadOnly = true;
                dgvLines.AllowUserToAddRows = false;

                MessageBox.Show("✅ Οριστικοποιήθηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void LoadDocument(int id)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var doc = conn.QueryFirstOrDefault("SELECT * FROM StockTransfers WHERE Id=@Id", new { Id = id });
                if (doc == null) return;

                txtNumber.Text = doc.TransferNumber;
                dtpDate.Value = doc.TransferDate;
                cmbFromWarehouse.SelectedValue = doc.FromWarehouseId;
                cmbToWarehouse.SelectedValue = doc.ToWarehouseId;
                cmbStatus.Text = doc.Status;
                txtNotes.Text = doc.Notes;

                var lines = conn.Query("SELECT * FROM StockTransferLines WHERE StockTransferId=@Id", new { Id = id }).ToList();
                dgvLines.Rows.Clear();
                foreach (var l in lines)
                {
                    var item = conn.QueryFirstOrDefault("SELECT Code, Name FROM Items WHERE Id=@Id", new { Id = l.ItemId });
                    dgvLines.Rows.Add(item?.Code, item?.Name, l.Quantity, l.ReceivedQty, l.PendingQty, l.UnitCost, l.Quantity * l.UnitCost);
                }
                CalculateTotals();

                if (doc.Status == "Οριστικοποιημένο")
                {
                    btnSave.Enabled = false;
                    btnPost.Enabled = false;
                    dgvLines.ReadOnly = true;
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private bool ValidateForm()
        {
            if (cmbSeries.SelectedValue == null || string.IsNullOrWhiteSpace(txtNumber.Text)) { MessageBox.Show("Συμπλήρωσε Σειρά/Αριθμό!"); return false; }
            if (GetSelectedWarehouseId(cmbFromWarehouse) == 0 || GetSelectedWarehouseId(cmbToWarehouse) == 0) { MessageBox.Show("Επίλεξε Αποθήκες!"); return false; }
            if (GetSelectedWarehouseId(cmbFromWarehouse) == GetSelectedWarehouseId(cmbToWarehouse)) { MessageBox.Show("Οι αποθήκες πρέπει να διαφέρουν!"); return false; }
            if (dgvLines.Rows.Count == 1 && dgvLines.Rows[0].IsNewRow) { MessageBox.Show("Πρόσθεσε είδη!"); return false; }
            return true;
        }

        private int GetSelectedWarehouseId(ComboBox cmb)
        {
            if (cmb.SelectedValue == null) return 0;
            try { return Convert.ToInt32(cmb.SelectedValue); }
            catch { return 0; }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}