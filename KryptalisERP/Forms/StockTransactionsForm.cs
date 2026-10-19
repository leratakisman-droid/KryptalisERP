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
    public class StockTransactionsForm : Form
    {
        private DataGridView dgvTransactions;
        private DateTimePicker dtpFrom, dtpTo;
        private ComboBox cmbWarehouse, cmbTransactionType;
        private TextBox txtItemSearch;
        private Button btnRefresh, btnExport;
        private Label lblTotalRecords;

        public StockTransactionsForm()
        {
            InitializeUI();
            this.Load += StockTransactionsForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📜 Ιστορικό Κινήσεων Αποθήκης";
            this.Size = new Size(1400, 850);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Padding = new Padding(15);

            // ============================================
            // 1. FILTERS PANEL
            // ============================================
            var pnlFilters = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White,
                Padding = new Padding(20),
                Margin = new Padding(0, 0, 0, 10)
            };

            // Row 1
            int y = 20;
            var lblFrom = new Label { Text = "Από Ημερ.:", Location = new Point(20, y), Size = new Size(90, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlFilters.Controls.Add(lblFrom);

            dtpFrom = new DateTimePicker { Location = new Point(120, y), Size = new Size(140, 30), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10F) };
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            pnlFilters.Controls.Add(dtpFrom);

            var lblTo = new Label { Text = "Έως Ημερ.:", Location = new Point(280, y), Size = new Size(90, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlFilters.Controls.Add(lblTo);

            dtpTo = new DateTimePicker { Location = new Point(380, y), Size = new Size(140, 30), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10F) };
            dtpTo.Value = DateTime.Today;
            pnlFilters.Controls.Add(dtpTo);

            var lblWarehouse = new Label { Text = "Αποθήκη:", Location = new Point(540, y), Size = new Size(80, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlFilters.Controls.Add(lblWarehouse);

            cmbWarehouse = new ComboBox { Location = new Point(630, y), Size = new Size(200, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbWarehouse.Items.Add("Όλες οι Αποθήκες");
            cmbWarehouse.SelectedIndex = 0;
            pnlFilters.Controls.Add(cmbWarehouse);

            var lblType = new Label { Text = "Τύπος:", Location = new Point(850, y), Size = new Size(60, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlFilters.Controls.Add(lblType);

            cmbTransactionType = new ComboBox { Location = new Point(920, y), Size = new Size(180, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbTransactionType.Items.AddRange(new object[] {
                "Όλοι οι Τύποι",
                "Μεταφορά Εισόδου",
                "Μεταφορά Εξόδου",
                "Διόρθωση Εισόδου",
                "Διόρθωση Εξόδου",
                "Πώληση",
                "Αγορά"
            });
            cmbTransactionType.SelectedIndex = 0;
            pnlFilters.Controls.Add(cmbTransactionType);

            // Row 2
            y = 60;
            var lblSearch = new Label { Text = "Αναζήτηση Είδους:", Location = new Point(20, y), Size = new Size(130, 30), Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlFilters.Controls.Add(lblSearch);

            txtItemSearch = new TextBox { Location = new Point(160, y), Size = new Size(300, 30), Font = new Font("Segoe UI", 10F), BorderStyle = BorderStyle.FixedSingle };
            txtItemSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) LoadTransactions(); };
            pnlFilters.Controls.Add(txtItemSearch);

            btnRefresh = new Button { Text = "🔄 Ανανέωση", Location = new Point(480, y), Size = new Size(140, 35), BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnRefresh.Click += (s, e) => LoadTransactions();
            pnlFilters.Controls.Add(btnRefresh);

            btnExport = new Button { Text = "📊 Export CSV", Location = new Point(630, y), Size = new Size(140, 35), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
            btnExport.Click += (s, e) => ExportToExcel();
            pnlFilters.Controls.Add(btnExport);

            this.Controls.Add(pnlFilters);

            // ============================================
            // 2. GRID
            // ============================================
            dgvTransactions = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 10F),
                RowTemplate = { Height = 32 }
            };

            dgvTransactions.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
            };

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "TransactionDate", DataPropertyName = "TransactionDate", HeaderText = "Ημερομηνία", Width = 120 });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "TransactionType", DataPropertyName = "TransactionType", HeaderText = "Τύπος Κίνησης", Width = 160 });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "WarehouseName", DataPropertyName = "WarehouseName", HeaderText = "Αποθήκη", Width = 160 });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemCode", DataPropertyName = "ItemCode", HeaderText = "Κωδικός", Width = 130 });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemName", DataPropertyName = "ItemName", HeaderText = "Είδος", Width = 300 });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantityIn", DataPropertyName = "QuantityIn", HeaderText = "Είσοδος", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantityOut", DataPropertyName = "QuantityOut", HeaderText = "Έξοδος", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.FromArgb(231, 76, 60) } });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitCost", DataPropertyName = "UnitCost", HeaderText = "Τιμή €", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalCost", DataPropertyName = "TotalCost", HeaderText = "Συνολική Αξία €", Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold) } });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocumentType", DataPropertyName = "DocumentType", HeaderText = "Παραστατικό", Width = 140 });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocumentNumber", DataPropertyName = "DocumentNumber", HeaderText = "Αριθμός", Width = 120 });

            dgvTransactions.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) ShowTransactionDetails(); };

            this.Controls.Add(dgvTransactions);

            // ============================================
            // 3. FOOTER
            // ============================================
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20, 10, 20, 10),
                Margin = new Padding(0, 10, 0, 0)
            };

            lblTotalRecords = new Label { Text = "📊 Σύνολο Εγγραφών: 0", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219), Dock = DockStyle.Left, AutoSize = true };
            pnlFooter.Controls.Add(lblTotalRecords);

            this.Controls.Add(pnlFooter);
        }

        private void StockTransactionsForm_Load(object sender, EventArgs e)
        {
            LoadWarehouses();
            LoadTransactions();
        }

        private void LoadWarehouses()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var warehouses = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive=1 ORDER BY Name").ToList();

                cmbWarehouse.Items.Clear();
                cmbWarehouse.Items.Add("Όλες οι Αποθήκες");
                foreach (var wh in warehouses)
                {
                    cmbWarehouse.Items.Add($"{wh.Id}|{wh.Name}");
                }
                cmbWarehouse.SelectedIndex = 0;
            }
            catch { }
        }

        private int? GetSelectedWarehouseId()
        {
            if (cmbWarehouse.SelectedIndex <= 0) return null;
            string selected = cmbWarehouse.SelectedItem.ToString();
            if (selected.Contains("|")) return Convert.ToInt32(selected.Split('|')[0]);
            return null;
        }

        private string GetTransactionTypeFilter()
        {
            string selectedType = cmbTransactionType.Text;
            return selectedType switch
            {
                "Μεταφορά Εισόδου" => "TRANSFER_IN",
                "Μεταφορά Εξόδου" => "TRANSFER_OUT",
                "Διόρθωση Εισόδου" => "ADJUSTMENT_IN",
                "Διόρθωση Εξόδου" => "ADJUSTMENT_OUT",
                "Πώληση" => "SALE",
                "Αγορά" => "PURCHASE",
                _ => null
            };
        }

        private void LoadTransactions()
        {
            try
            {
                dgvTransactions.DataSource = null;

                using var conn = new SqlConnection(GetConnectionString());

                int? warehouseId = GetSelectedWarehouseId();
                string transactionType = GetTransactionTypeFilter();
                string searchQuery = string.IsNullOrWhiteSpace(txtItemSearch.Text) ? null : txtItemSearch.Text.Trim();

                string sql = @"
                    SELECT 
                        st.Id,
                        st.TransactionDate,
                        CASE st.TransactionType
                            WHEN 'TRANSFER_IN' THEN 'Μεταφορά Εισόδου'
                            WHEN 'TRANSFER_OUT' THEN 'Μεταφορά Εξόδου'
                            WHEN 'ADJUSTMENT_IN' THEN 'Διόρθωση Εισόδου'
                            WHEN 'ADJUSTMENT_OUT' THEN 'Διόρθωση Εξόδου'
                            WHEN 'SALE' THEN 'Πώληση'
                            WHEN 'PURCHASE' THEN 'Αγορά'
                            ELSE st.TransactionType
                        END AS TransactionType,
                        w.Name AS WarehouseName,
                        i.Code AS ItemCode,
                        i.Name AS ItemName,
                        st.QuantityIn,
                        st.QuantityOut,
                        st.UnitCost,
                        st.TotalCost,
                        st.DocumentType,
                        CASE 
                            WHEN st.DocumentType = 'StockTransfer' THEN stt.TransferNumber
                            WHEN st.DocumentType = 'StockAdjustment' THEN sa.AdjustmentNumber
                            ELSE CAST(st.DocumentId AS VARCHAR)
                        END AS DocumentNumber
                    FROM StockTransactions st
                    INNER JOIN Warehouses w ON st.WarehouseId = w.Id
                    INNER JOIN Items i ON st.ItemId = i.Id
                    LEFT JOIN StockTransfers stt ON st.DocumentId = stt.Id AND st.DocumentType = 'StockTransfer'
                    LEFT JOIN StockAdjustments sa ON st.DocumentId = sa.Id AND st.DocumentType = 'StockAdjustment'
                    WHERE st.TransactionDate >= @FromDate AND st.TransactionDate < @ToDate";

                var parameters = new { FromDate = dtpFrom.Value.Date, ToDate = dtpTo.Value.Date.AddDays(1), WarehouseId = warehouseId, TransactionType = transactionType, SearchQuery = searchQuery };

                if (warehouseId.HasValue) sql += " AND st.WarehouseId = @WarehouseId";
                if (!string.IsNullOrEmpty(transactionType)) sql += " AND st.TransactionType = @TransactionType";
                if (!string.IsNullOrEmpty(searchQuery)) sql += " AND (i.Code LIKE '%' + @SearchQuery + '%' OR i.Name LIKE '%' + @SearchQuery + '%')";
                sql += " ORDER BY st.TransactionDate DESC, st.Id DESC";

                var transactions = conn.Query(sql, parameters).ToList();
                dgvTransactions.DataSource = transactions;
                lblTotalRecords.Text = $"📊 Σύνολο Εγγραφών: {transactions.Count}";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ShowTransactionDetails()
        {
            if (dgvTransactions.CurrentRow == null) return;
            var row = dgvTransactions.CurrentRow;
            string msg = $"Λεπτομέρειες Κίνησης\n\n" +
                $"Ημερομηνία: {row.Cells["TransactionDate"].Value}\n" +
                $"Τύπος: {row.Cells["TransactionType"].Value}\n" +
                $"Αποθήκη: {row.Cells["WarehouseName"].Value}\n" +
                $"Είδος: {row.Cells["ItemName"].Value}\n" +
                $"Κωδικός: {row.Cells["ItemCode"].Value}\n" +
                $"Είσοδος: {row.Cells["QuantityIn"].Value}\n" +
                $"Έξοδος: {row.Cells["QuantityOut"].Value}\n" +
                $"Τιμή: {row.Cells["UnitCost"].Value} €\n" +
                $"Σύνολο: {row.Cells["TotalCost"].Value} €\n" +
                $"Παραστατικό: {row.Cells["DocumentType"].Value} - {row.Cells["DocumentNumber"].Value}";
            MessageBox.Show(msg, "Λεπτομέρειες Κίνησης", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportToExcel()
        {
            if (dgvTransactions.Rows.Count == 0) { MessageBox.Show("Δεν υπάρχουν δεδομένα για export!"); return; }
            try
            {
                var saveDialog = new SaveFileDialog { Filter = "CSV Files|*.csv", Title = "Αποθήκευση CSV", FileName = $"StockTransactions_{DateTime.Now:yyyyMMdd_HHmmss}.csv" };
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new System.IO.StreamWriter(saveDialog.FileName, false, System.Text.Encoding.UTF8))
                    {
                        string headerLine = string.Join(";", dgvTransactions.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => c.HeaderText));
                        writer.WriteLine(headerLine);
                        foreach (DataGridViewRow row in dgvTransactions.Rows)
                        {
                            string line = string.Join(";", dgvTransactions.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => row.Cells[c.Index].Value?.ToString() ?? ""));
                            writer.WriteLine(line);
                        }
                    }
                    MessageBox.Show("✅ Το αρχείο CSV δημιουργήθηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα export:\n" + ex.Message); }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}