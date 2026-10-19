using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class DeliveryManagementForm : Form
    {
        private ComboBox cmbOrder;
        private DataGridView dgvLines;
        private TextBox txtReceiverName, txtNotes;
        private DateTimePicker dtpDate;
        private Button btnLoadOrder, btnSaveReceipt, btnClose;
        private Label lblSummary;

        private int? _currentOrderId;
        private bool _isLoading = false;

        public DeliveryManagementForm()
        {
            InitializeUI();
            this.Load += DeliveryManagementForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📦 Διαχείριση Παραλαβών (Delivery Outcomes)";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            // --- HEADER ---
            var pnlHeader = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = Color.White, Padding = new Padding(10) };

            pnlHeader.Controls.Add(new Label { Text = "Παραγγελία:", Location = new Point(10, 15), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            cmbOrder = new ComboBox { Location = new Point(90, 12), Size = new Size(300, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbOrder.SelectedIndexChanged += (s, e) => LoadOrderDetails();
            pnlHeader.Controls.Add(cmbOrder);

            btnLoadOrder = CreateButton("🔄 Φόρτωση", 400, 10, 100, 30, Color.Gray);
            btnLoadOrder.Click += (s, e) => LoadOpenOrders();
            pnlHeader.Controls.Add(btnLoadOrder);

            pnlHeader.Controls.Add(new Label { Text = "Ημερομηνία:", Location = new Point(520, 15), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            dtpDate = new DateTimePicker { Location = new Point(600, 12), Size = new Size(120, 26), Format = DateTimePickerFormat.Short };
            pnlHeader.Controls.Add(dtpDate);

            pnlHeader.Controls.Add(new Label { Text = "Παραλήπτης:", Location = new Point(10, 45), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtReceiverName = new TextBox { Location = new Point(90, 42), Size = new Size(200, 26) };
            pnlHeader.Controls.Add(txtReceiverName);

            pnlHeader.Controls.Add(new Label { Text = "Σημειώσεις:", Location = new Point(300, 45), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtNotes = new TextBox { Location = new Point(380, 42), Size = new Size(400, 26) };
            pnlHeader.Controls.Add(txtNotes);

            this.Controls.Add(pnlHeader);

            // --- GRID ---
            dgvLines = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10F),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            dgvLines.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(13, 110, 253), ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };

            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", HeaderText = "Κωδικός", Width = 100, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Περιγραφή", Width = 300, ReadOnly = true });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderedQty", HeaderText = "Ζητήθηκε", Width = 80, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "AlreadyReceived", HeaderText = "Ήδη Παρ.", Width = 80, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.LightGray } });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn { Name = "Remaining", HeaderText = "Υπόλοιπο", Width = 80, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.Red } });

            // EDITABLE COLUMN
            var colNewRec = new DataGridViewTextBoxColumn { Name = "NewReceivedQty", HeaderText = "Νέα Ποσότητα", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", BackColor = Color.LightYellow, Font = new Font("Segoe UI", 10F, FontStyle.Bold) } };
            dgvLines.Columns.Add(colNewRec);

            dgvLines.CellValueChanged += DgvLines_CellValueChanged;
            this.Controls.Add(dgvLines);

            // --- FOOTER ---
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(10) };

            lblSummary = new Label { Text = "Συνολική Νέα Παραλαβή: 0", Location = new Point(10, 15), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Blue };
            pnlFooter.Controls.Add(lblSummary);

            btnSaveReceipt = CreateButton("💾 Καταχώρηση Παραλαβής", 700, 10, 200, 40, Color.Green);
            btnSaveReceipt.Click += (s, e) => SaveDeliveryOutcome();
            pnlFooter.Controls.Add(btnSaveReceipt);

            btnClose = CreateButton("❌ Κλείσιμο", 920, 10, 100, 40, Color.Gray);
            btnClose.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnClose);

            this.Controls.Add(pnlFooter);
        }

        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
            => new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };

        private void DeliveryManagementForm_Load(object sender, EventArgs e)
        {
            LoadOpenOrders();
        }

        private void LoadOpenOrders()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // Fetch orders that are CONFIRMED or PARTIALLY_DELIVERED and have remaining quantity
                var orders = conn.Query(@"
                    SELECT DISTINCT so.Id, so.OrderNumber, c.Epwnymia 
                    FROM SalesOrders so 
                    JOIN Customers c ON so.CustomerId = c.Id 
                    WHERE so.Status IN ('CONFIRMED', 'PARTIALLY_DELIVERED')
                      AND EXISTS (SELECT 1 FROM SalesOrderLines sol WHERE sol.OrderId = so.Id AND sol.Quantity > ISNULL(sol.DeliveredQty, 0))
                    ORDER BY so.OrderDate DESC").ToList();

                cmbOrder.DataSource = orders;
                cmbOrder.DisplayMember = "Epwnymia"; // Or OrderNumber + Customer
                cmbOrder.ValueMember = "Id";

                if (orders.Count == 0)
                {
                    MessageBox.Show("Δεν βρέθηκαν ανοιχτές παραγγελίες με υπόλοιπο ποσότητας.");
                    dgvLines.Rows.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void LoadOrderDetails()
        {
            if (_isLoading || cmbOrder.SelectedValue == null) return;
            _isLoading = true;
            _currentOrderId = Convert.ToInt32(cmbOrder.SelectedValue);
            dgvLines.Rows.Clear();

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var lines = conn.Query(@"
                    SELECT sol.ItemId, i.Code AS ItemCode, sol.Description, 
                           sol.Quantity AS OrderedQty, 
                           ISNULL(sol.DeliveredQty, 0) AS AlreadyReceived,
                           (sol.Quantity - ISNULL(sol.DeliveredQty, 0)) AS Remaining
                    FROM SalesOrderLines sol
                    LEFT JOIN Items i ON sol.ItemId = i.Id
                    WHERE sol.OrderId = @OrderId AND sol.Quantity > ISNULL(sol.DeliveredQty, 0)
                    ORDER BY sol.LineNumber", new { OrderId = _currentOrderId }).ToList();

                foreach (var l in lines)
                {
                    int idx = dgvLines.Rows.Add(l.ItemCode, l.Description, l.OrderedQty, l.AlreadyReceived, l.Remaining, 0); // NewReceived starts at 0
                    dgvLines.Rows[idx].Cells["ItemId"].Value = l.ItemId; // Store hidden ID if needed later
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης γραμμών: " + ex.Message); }

            _isLoading = false;
            CalculateSummary();
        }

        private void DgvLines_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0) return;
            if (dgvLines.Columns[e.ColumnIndex].Name == "NewReceivedQty")
            {
                // Validation: Cannot exceed Remaining
                decimal remaining = Dec(e.RowIndex, "Remaining");
                decimal newVal = Dec(e.RowIndex, "NewReceivedQty");

                if (newVal > remaining)
                {
                    dgvLines.Rows[e.RowIndex].Cells["NewReceivedQty"].Value = remaining;
                    MessageBox.Show($"Η ποσότητα δεν μπορεί να υπερβαίνει το υπόλοιπο ({remaining}).", "Προσοχή");
                }
                CalculateSummary();
            }
        }

        private void CalculateSummary()
        {
            decimal total = 0;
            foreach (DataGridViewRow r in dgvLines.Rows)
            {
                if (!r.IsNewRow) total += Dec(r.Index, "NewReceivedQty");
            }
            lblSummary.Text = $"Συνολική Νέα Παραλαβή: {total:N2} τεμάχια";
        }

        private decimal Dec(int rowIndex, string colName)
        {
            var val = dgvLines.Rows[rowIndex].Cells[colName].Value;
            return decimal.TryParse(val?.ToString(), out var d) ? d : 0;
        }

        private void SaveDeliveryOutcome()
        {
            if (!_currentOrderId.HasValue) { MessageBox.Show("Φόρτωσε παραγγελία!"); return; }

            // Check if any line has qty > 0
            bool hasData = dgvLines.Rows.Cast<DataGridViewRow>().Any(r => !r.IsNewRow && Dec(r.Index, "NewReceivedQty") > 0);
            if (!hasData) { MessageBox.Show("Δεν δήλωσες καμία νέα παραλαβή."); return; }

            if (MessageBox.Show("Καταχώρηση παραλαβής;\n\nΘα ενημερωθούν τα StockTransactions και οι SalesOrderLines.", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                // 1. Insert into DeliveryOutcomes (Header)
                int outcomeId = conn.ExecuteScalar<int>(@"
                    INSERT INTO DeliveryOutcomes (DocumentId, ReceivedDate, Receiver, Status, Accepted, Notes, CreatedAt)
                    VALUES (@DocId, @Date, @Recv, 'RECEIVED', 1, @Notes, GETDATE());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new { DocId = _currentOrderId, Date = dtpDate.Value.Date, Recv = txtReceiverName.Text, Notes = txtNotes.Text }, tran);

                // 2. Loop through lines
                foreach (DataGridViewRow r in dgvLines.Rows)
                {
                    if (r.IsNewRow) continue;

                    decimal newQty = Dec(r.Index, "NewReceivedQty");
                    if (newQty <= 0) continue;

                    int itemId = Convert.ToInt32(r.Cells["ItemId"].Value); // Assuming we stored it or fetched by code

                    // Find the corresponding SalesOrderLineId to link correctly in OutcomeLines if needed, 
                    // BUT your table structure uses DocumentLineId. Let's assume DocumentLineId refers to SalesOrderLines.Id

                    // Get SalesOrderLine Id for this item in this order
                    int solId = conn.ExecuteScalar<int>("SELECT TOP 1 Id FROM SalesOrderLines WHERE OrderId=@Oid AND ItemId=@Itm", new { Oid = _currentOrderId, Itm = itemId }, tran);

                    // 3. Insert into DeliveryOutcomeLines
                    conn.Execute(@"
                        INSERT INTO DeliveryOutcomeLines (OutcomeId, DocumentLineId, SentQty, ReceivedQty, Difference, CreatedAt)
                        VALUES (@OId, @SolId, @Sent, @Rec, @Diff, GETDATE())",
                        new { OId = outcomeId, SolId = solId, Sent = newQty, Rec = newQty, Diff = 0 }, tran);

                    // 4. Update SalesOrderLines.DeliveredQty
                    conn.Execute("UPDATE SalesOrderLines SET DeliveredQty = ISNULL(DeliveredQty,0) + @Qty WHERE Id = @SolId", new { Qty = newQty, SolId = solId }, tran);

                    // 5. Decrease Stock (Sale OUT)
                    // Note: Depending on your flow, stock might decrease here or at Invoice stage. 
                    // Usually Delivery reduces available stock/reservation.
                    conn.Execute(@"
                        UPDATE StockVariants SET AvailableQty = AvailableQty - @Qty, Quantity = Quantity - @Qty
                        WHERE ItemVariantId IN (SELECT Id FROM ItemVariants WHERE ItemId = @ItemId)",
                        new { Qty = newQty, ItemId = itemId }, tran);

                    conn.Execute("UPDATE Items SET Stock = Stock - @Qty WHERE Id = @ItemId", new { Qty = newQty, ItemId = itemId }, tran);

                    // 6. Log Transaction
                    conn.Execute(@"
                        INSERT INTO StockTransactions (CompanyId, WarehouseId, ItemId, TransactionDate, TransactionType, QuantityOut, DocumentId, DocumentType, CreatedAt)
                        VALUES (1, 1, @ItemId, @Date, 'SALE_OUT', @Qty, @DocId, 'DELIVERY', GETDATE())",
                        new { ItemId = itemId, Date = dtpDate.Value, Qty = newQty, DocId = _currentOrderId }, tran);
                }

                // 7. Update Order Status if fully delivered
                conn.Execute(@"
                    UPDATE SalesOrders SET Status = CASE 
                        WHEN NOT EXISTS(SELECT 1 FROM SalesOrderLines WHERE OrderId=@Oid AND Quantity > ISNULL(DeliveredQty,0)) THEN 'DELIVERED'
                        ELSE 'PARTIALLY_DELIVERED' END
                    WHERE Id = @Oid", new { Oid = _currentOrderId }, tran);

                tran.Commit();
                MessageBox.Show("✅ Η παραλαβή καταχωρήθηκε επιτυχώς!", "Επιτυχία");
                LoadOpenOrders(); // Refresh list
                dgvLines.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}