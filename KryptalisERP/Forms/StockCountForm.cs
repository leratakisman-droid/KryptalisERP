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
    public class StockCountForm : Form
    {
        private ComboBox cmbWarehouse, cmbCountType, cmbStatus;
        private TextBox txtReference, txtNotes;
        private DateTimePicker dtpCountDate;
        private DataGridView dgvLines;
        private Label lblTotalItems, lblDiscrepancies;
        private Button btnSave, btnComplete, btnCancel;
        private int? _documentId;

        public StockCountForm(int? documentId = null)
        {
            _documentId = documentId;
            InitializeUI();
            this.Load += StockCountForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📋 Απογραφή Αποθήκης";
            this.Size = new Size(1300, 780);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.Padding = new Padding(15);

            // HEADER
            var pnlHeader = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 120,
                ColumnCount = 6,
                RowCount = 2,
                BackColor = Color.White,
                Padding = new Padding(15),
                Margin = new Padding(0, 0, 0, 10)
            };
            for (int i = 0; i < 6; i++) pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            pnlHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            pnlHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            pnlHeader.Controls.Add(CreateLabel("Αποθήκη:", Color.FromArgb(253, 126, 20)), 0, 0);
            cmbWarehouse = CreateComboBox();
            cmbWarehouse.SelectedIndexChanged += (s, e) => RefreshCurrentStockInGrid();
            pnlHeader.Controls.Add(cmbWarehouse, 1, 0);

            pnlHeader.Controls.Add(CreateLabel("Ημερομηνία:"), 2, 0);
            dtpCountDate = new DateTimePicker { Size = new Size(130, 28), Format = DateTimePickerFormat.Short, Anchor = AnchorStyles.Left };
            pnlHeader.Controls.Add(dtpCountDate, 3, 0);

            pnlHeader.Controls.Add(CreateLabel("Τύπος Απογραφής:"), 4, 0);
            cmbCountType = new ComboBox { Size = new Size(180, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F), Anchor = AnchorStyles.Left };
            cmbCountType.Items.AddRange(new object[] { "Γενική Απογραφή", "Κυκλική Απογραφή", "Δειγματοληπτική", "Ετήσια" });
            cmbCountType.SelectedIndex = 0;
            pnlHeader.Controls.Add(cmbCountType, 5, 0);

            pnlHeader.Controls.Add(CreateLabel("Κατάσταση:"), 0, 1);
            cmbStatus = new ComboBox { Size = new Size(150, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F), Anchor = AnchorStyles.Left };
            cmbStatus.Items.AddRange(new object[] { "Σε Εξέλιξη", "Ολοκληρωμένη", "Επικυρωμένη" });
            cmbStatus.SelectedIndex = 0;
            pnlHeader.Controls.Add(cmbStatus, 1, 1);

            pnlHeader.Controls.Add(CreateLabel("Σημειώσεις:"), 2, 1);
            txtNotes = new TextBox { Size = new Size(200, 28), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle, Anchor = AnchorStyles.Left };
            pnlHeader.Controls.Add(txtNotes, 3, 1);

            pnlHeader.Controls.Add(CreateLabel("Ref:"), 4, 1);
            txtReference = new TextBox { Size = new Size(100, 28), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle, Anchor = AnchorStyles.Left };
            pnlHeader.Controls.Add(txtReference, 5, 1);

            this.Controls.Add(pnlHeader);

            // GRID
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
                RowTemplate = { Height = 32 }
            };

            dgvLines.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(253, 126, 20),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", HeaderText = " Κωδικός", Width = 120 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemName", HeaderText = "Είδος", Width = 300, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Location", HeaderText = "Θέση", Width = 120 });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "SystemQty", HeaderText = "Σύστημα (Θεωρητικό)", Width = 130, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.FromArgb(240, 240, 240) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "CountedQty", HeaderText = "Μετρημένο (Πραγματικό)", Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Difference", HeaderText = "Διαφορά", Width = 100, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });

            dgvLines.CellValueChanged += DgvLines_CellValueChanged;
            dgvLines.RowsAdded += DgvLines_RowsAdded;
            dgvLines.UserDeletingRow += (s, e) => CalculateTotals();

            this.Controls.Add(dgvLines);

            // FOOTER
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = Color.White, Padding = new Padding(15), Margin = new Padding(0, 10, 0, 0) };

            lblTotalItems = new Label { Text = "📦 Σύνολο Ειδών: 0", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219), Location = new Point(15, 15), AutoSize = true, BackColor = Color.FromArgb(207, 226, 255), Padding = new Padding(10, 5, 10, 5) };
            pnlFooter.Controls.Add(lblTotalItems);

            lblDiscrepancies = new Label { Text = "️ Αποκλίσεις: 0", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(220, 53, 69), Location = new Point(250, 15), AutoSize = true, BackColor = Color.FromArgb(248, 215, 218), Padding = new Padding(10, 5, 10, 5) };
            pnlFooter.Controls.Add(lblDiscrepancies);

            btnSave = CreateButton("💾 Αποθήκευση", 750, 15, 150, 40, Color.FromArgb(52, 152, 219));
            btnSave.Click += (s, e) => SaveDocument(false);
            pnlFooter.Controls.Add(btnSave);

            btnComplete = CreateButton("✅ Ολοκλήρωση", 910, 15, 150, 40, Color.FromArgb(253, 126, 20));
            btnComplete.Click += (s, e) => CompleteCount();
            pnlFooter.Controls.Add(btnComplete);

            btnCancel = CreateButton("❌ Ακύρωση", 1070, 15, 140, 40, Color.FromArgb(108, 117, 125));
            btnCancel.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnCancel);

            this.Controls.Add(pnlFooter);
        }

        private Label CreateLabel(string text, Color? color = null) => new Label { Text = text, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = color ?? Color.FromArgb(33, 37, 41), Anchor = AnchorStyles.Left, Margin = new Padding(0, 6, 0, 0) };
        private ComboBox CreateComboBox() => new ComboBox { Size = new Size(180, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F), Anchor = AnchorStyles.Left, FlatStyle = FlatStyle.Flat };
        private Button CreateButton(string text, int x, int y, int w, int h, Color color) => new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };

        private void StockCountForm_Load(object sender, EventArgs e) { LoadLookups(); if (_documentId.HasValue) LoadDocument(_documentId.Value); }

        private void LoadLookups()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var wh = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive=1 ORDER BY Name").ToList();
                cmbWarehouse.DataSource = wh; cmbWarehouse.DisplayMember = "Name"; cmbWarehouse.ValueMember = "Id";
            }
            catch { }
        }

        private int GetSelectedWarehouseId()
        {
            if (cmbWarehouse.SelectedValue == null) return 0;
            try { return Convert.ToInt32(cmbWarehouse.SelectedValue); } catch { return 0; }
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
                    row.Cells["SystemQty"].Value = currentQty;
                    decimal counted = decimal.TryParse(row.Cells["CountedQty"].Value?.ToString(), out var c) ? c : 0;
                    row.Cells["Difference"].Value = counted - currentQty;
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
                            row.Cells["SystemQty"].Value = stock != null ? Convert.ToDecimal(stock.Quantity) : 0;
                        }
                    }
                    catch { }
                }
            }

            if (col == "CountedQty" && !row.IsNewRow)
            {
                decimal system = decimal.TryParse(row.Cells["SystemQty"].Value?.ToString(), out var s) ? s : 0;
                decimal counted = decimal.TryParse(row.Cells["CountedQty"].Value?.ToString(), out var c) ? c : 0;
                row.Cells["Difference"].Value = counted - system;
                CalculateTotals();
            }
        }

        private void DgvLines_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) { for (int i = 0; i < e.RowCount; i++) dgvLines.Rows[e.RowIndex + i].Cells["RowNo"].Value = e.RowIndex + i + 1; }

        private void CalculateTotals()
        {
            int count = 0, diffs = 0;
            foreach (DataGridViewRow r in dgvLines.Rows)
            {
                if (r.IsNewRow) continue;
                count++;
                if (decimal.TryParse(r.Cells["Difference"].Value?.ToString(), out var d) && d != 0) diffs++;
            }
            lblTotalItems.Text = $"📦 Σύνολο Ειδών: {count}";
            lblDiscrepancies.Text = $"⚠️ Αποκλίσεις: {diffs}";
        }

        private void SaveDocument(bool isComplete)
        {
            if (!ValidateForm()) return;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                int docId;
                string status = isComplete ? "Ολοκληρωμένη" : "Σε Εξέλιξη";
                int userId = 1; try { userId = Convert.ToInt32(Program.CurrentUserId); } catch { }

                if (_documentId.HasValue)
                {
                    conn.Execute(@"UPDATE StockCounts SET WarehouseId=@Wh, CountDate=@Date, CountType=@Type, Status=@Status, Notes=@Notes, Reference=@Ref WHERE Id=@Id",
                        new { Id = _documentId, Wh = GetSelectedWarehouseId(), Date = dtpCountDate.Value, Type = cmbCountType.Text, Status = status, Notes = txtNotes.Text, Ref = txtReference.Text }, tran);
                    docId = _documentId.Value;
                    if (!isComplete) conn.Execute("DELETE FROM StockCountLines WHERE StockCountId=@Id", new { Id = docId }, tran);
                }
                else
                {
                    docId = conn.ExecuteScalar<int>(@"INSERT INTO StockCounts (WarehouseId, CountDate, Status, UserId, CountType, Reference, TotalItems, CountedItems, DiscrepancyItems, Notes, CreatedAt, UpdatedAt) 
                        VALUES (@Wh, @Date, @Status, @User, @Type, @Ref, 0, 0, 0, @Notes, GETDATE(), GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { Wh = GetSelectedWarehouseId(), Date = dtpCountDate.Value, Status = status, User = userId, Type = cmbCountType.Text, Ref = txtReference.Text, Notes = txtNotes.Text }, tran);
                }

                int lineNum = 1;
                foreach (DataGridViewRow row in dgvLines.Rows)
                {
                    if (row.IsNewRow) continue;
                    string code = row.Cells["ItemCode"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(code)) continue;

                    int itemId = conn.ExecuteScalar<int>("SELECT Id FROM Items WHERE Code = @Code", new { Code = code }, tran);
                    decimal system = decimal.TryParse(row.Cells["SystemQty"].Value?.ToString(), out var s) ? s : 0;
                    decimal counted = decimal.TryParse(row.Cells["CountedQty"].Value?.ToString(), out var c) ? c : 0;
                    decimal diff = counted - system;

                    conn.Execute(@"INSERT INTO StockCountLines (StockCountId, LineNumber, ItemId, SystemQty, CountedQty, Difference, Notes, Status, CreatedAt) 
                        VALUES (@DocId, @LineNum, @ItemId, @System, @Counted, @Diff, @Notes, @LineStatus, GETDATE())",
                        new { DocId = docId, LineNum = lineNum++, ItemId = itemId, System = system, Counted = counted, Diff = diff, Notes = diff != 0 ? "Discrepancy" : "", LineStatus = isComplete ? "Verified" : "Draft" }, tran);
                }

                tran.Commit();
                _documentId = docId;
                if (!isComplete) MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CompleteCount()
        {
            if (!ValidateForm()) return;
            if (MessageBox.Show("Ολοκλήρωση απογραφής;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            try { SaveDocument(true); MessageBox.Show("✅ Η απογραφή Ολοκληρώθηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private bool ValidateForm()
        {
            if (GetSelectedWarehouseId() == 0) { MessageBox.Show("Επίλεξε Αποθήκη!"); return false; }
            if (dgvLines.Rows.Count == 1 && dgvLines.Rows[0].IsNewRow) { MessageBox.Show("Πρόσθεσε είδη!"); return false; }
            return true;
        }

        private void LoadDocument(int id)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var doc = conn.QueryFirstOrDefault("SELECT * FROM StockCounts WHERE Id=@Id", new { Id = id });
                if (doc == null) return;

                cmbWarehouse.SelectedValue = doc.WarehouseId;
                dtpCountDate.Value = doc.CountDate;
                cmbCountType.Text = doc.CountType;
                cmbStatus.Text = doc.Status;
                txtNotes.Text = doc.Notes;
                txtReference.Text = doc.Reference;

                var lines = conn.Query("SELECT * FROM StockCountLines WHERE StockCountId=@Id ORDER BY LineNumber", new { Id = id }).ToList();
                dgvLines.Rows.Clear();
                foreach (var l in lines)
                {
                    var item = conn.QueryFirstOrDefault("SELECT Code, Name FROM Items WHERE Id=@Id", new { Id = l.ItemId });
                    dgvLines.Rows.Add(item?.Code, item?.Name, null, l.SystemQty, l.CountedQty, l.Difference);
                }
                CalculateTotals();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}