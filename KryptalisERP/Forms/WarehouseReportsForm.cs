using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;

namespace KryptalisERP
{
    public partial class WarehouseReportsForm : Form
    {
        private DataTable _reportData;

        public WarehouseReportsForm()
        {
            InitializeComponent();
            LoadReportTypes();
            LoadFilters();
        }

        private void LoadReportTypes()
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("📊 Υπόλοιπα Αποθήκης");
            cmbReportType.Items.Add("📊 Υπόλοιπα ανά Αποθήκη");
            cmbReportType.Items.Add("💰 Αξία Αποθέματος");
            cmbReportType.Items.Add("⚠️ Χαμηλό/Αρνητικό Απόθεμα");
            cmbReportType.Items.Add("🎨 Ανάλυση Χρώμα/Μέγεθος");
            cmbReportType.Items.Add("🏷️ Τιμολόγηση Ειδών");
            cmbReportType.Items.Add("📦 Είδη ανά Προμηθευτή");
            cmbReportType.SelectedIndex = 0;
        }

        private void LoadFilters()
        {
            try
            {
                // ΑΠΟΘΗΚΕΣ
                DataTable warehouses = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, WarehouseName FROM CompanyWarehouses WHERE IsActive = 1 ORDER BY IsDefault DESC, WarehouseName");
                DataRow emptyWh = warehouses.NewRow();
                emptyWh["Id"] = 0;
                emptyWh["WarehouseName"] = "Όλες";
                warehouses.Rows.InsertAt(emptyWh, 0);
                cmbWarehouse.DataSource = warehouses;
                cmbWarehouse.DisplayMember = "WarehouseName";
                cmbWarehouse.ValueMember = "Id";

                // ΚΑΤΗΓΟΡΙΕΣ
                DataTable categories = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, CategoryName FROM ItemCategories ORDER BY CategoryName");
                DataRow emptyCat = categories.NewRow();
                emptyCat["Id"] = 0;
                emptyCat["CategoryName"] = "Όλες";
                categories.Rows.InsertAt(emptyCat, 0);
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "Id";

                // ΠΡΟΜΗΘΕΥΤΕΣ
                DataTable suppliers = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName");
                DataRow emptySup = suppliers.NewRow();
                emptySup["Id"] = 0;
                emptySup["SupplierName"] = "Όλοι";
                suppliers.Rows.InsertAt(emptySup, 0);
                cmbSupplier.DataSource = suppliers;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "Id";

                // ΤΥΠΟΣ ΕΙΔΟΥΣ
                cmbItemType.Items.Clear();
                cmbItemType.Items.Add("Όλοι");
                cmbItemType.Items.Add("Εμπόρευμα");
                cmbItemType.Items.Add("Υπηρεσία");
                cmbItemType.Items.Add("Ανταλλακτικό");
                cmbItemType.SelectedIndex = 0;

                dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpTo.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης φίλτρων: " + ex.Message);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string reportType = cmbReportType.Text;
            int warehouseId = cmbWarehouse.SelectedValue != null ? Convert.ToInt32(cmbWarehouse.SelectedValue) : 0;
            int categoryId = cmbCategory.SelectedValue != null ? Convert.ToInt32(cmbCategory.SelectedValue) : 0;
            string itemType = cmbItemType.Text;

            try
            {
                // ==========================================
                // 1. ΥΠΟΛΟΙΠΑ ΑΠΟΘΗΚΗΣ
                // ==========================================
                if (reportType.Contains("Υπόλοιπα Αποθήκης") && !reportType.Contains("ανά"))
                {
                    lblTitle.Text = "📊 Υπόλοιπα Αποθήκης";
                    string query = @"
                        SELECT i.ItemCode AS [Κωδικός], i.ItemName AS [Είδος], c.CategoryName AS [Κατηγορία],
                               i.Classification AS [Τύπος], u.UnitName AS [Μ.Μ.],
                               COALESCE(ws.Qty, i.CurrentStock, 0) AS [Ποσότητα],
                               i.RetailPrice AS [Λιανική], 
                               COALESCE(ws.Qty, i.CurrentStock, 0) * i.RetailPrice AS [Αξία]
                        FROM Items i
                        LEFT JOIN ItemCategories c ON i.CategoryId = c.Id
                        LEFT JOIN Units u ON i.PrimaryUnitId = u.Id
                        LEFT JOIN WarehouseStock ws ON ws.ItemId = i.Id";

                    if (warehouseId > 0) query += " AND ws.WarehouseId = @WarehouseId";
                    query += " WHERE i.IsActive = 1";
                    if (categoryId > 0) query += " AND i.CategoryId = @CategoryId";
                    if (itemType != "Όλοι") query += " AND i.Classification = @ItemType";
                    query += " ORDER BY i.ItemCode";

                    _reportData = DatabaseHelper.ExecuteQuery(query,
                        new SqlParameter("@WarehouseId", warehouseId > 0 ? (object)warehouseId : DBNull.Value),
                        new SqlParameter("@CategoryId", categoryId > 0 ? (object)categoryId : DBNull.Value),
                        new SqlParameter("@ItemType", itemType));
                }
                // ==========================================
                // 2. ΥΠΟΛΟΙΠΑ ΑΝΑ ΑΠΟΘΗΚΗ
                // ==========================================
                else if (reportType.Contains("Υπόλοιπα ανά Αποθήκη"))
                {
                    lblTitle.Text = "📊 Υπόλοιπα ανά Αποθήκη";
                    string query = @"
                        SELECT i.ItemCode AS [Κωδικός], i.ItemName AS [Είδος], cw.WarehouseName AS [Αποθήκη],
                               c.CategoryName AS [Κατηγορία], u.UnitName AS [Μ.Μ.],
                               COALESCE(ws.Qty, 0) AS [Ποσότητα],
                               i.RetailPrice AS [Λιανική], 
                               COALESCE(ws.Qty, 0) * i.RetailPrice AS [Αξία]
                        FROM Items i
                        LEFT JOIN ItemCategories c ON i.CategoryId = c.Id
                        LEFT JOIN Units u ON i.PrimaryUnitId = u.Id
                        CROSS JOIN CompanyWarehouses cw
                        LEFT JOIN WarehouseStock ws ON ws.ItemId = i.Id AND ws.WarehouseId = cw.Id
                        WHERE i.IsActive = 1 AND cw.IsActive = 1";

                    if (categoryId > 0) query += " AND i.CategoryId = @CategoryId";
                    if (itemType != "Όλοι") query += " AND i.Classification = @ItemType";
                    query += " ORDER BY i.ItemCode, cw.WarehouseName";

                    _reportData = DatabaseHelper.ExecuteQuery(query,
                        new SqlParameter("@CategoryId", categoryId > 0 ? (object)categoryId : DBNull.Value),
                        new SqlParameter("@ItemType", itemType));
                }
                // ==========================================
                // 3. ΑΞΙΑ ΑΠΟΘΕΜΑΤΟΣ
                // ==========================================
                else if (reportType.Contains("Αξία"))
                {
                    lblTitle.Text = "💰 Αξία Αποθέματος";
                    string query = @"
                        SELECT c.CategoryName AS [Κατηγορία], COUNT(i.Id) AS [Πλήθος Ειδών],
                               SUM(COALESCE(ws.Qty, i.CurrentStock, 0)) AS [Συνολική Ποσότητα],
                               SUM(COALESCE(ws.Qty, i.CurrentStock, 0) * i.PurchasePrice) AS [Αξία Αγοράς],
                               SUM(COALESCE(ws.Qty, i.CurrentStock, 0) * i.RetailPrice) AS [Αξία Λιανικής]
                        FROM Items i
                        LEFT JOIN ItemCategories c ON i.CategoryId = c.Id
                        LEFT JOIN WarehouseStock ws ON ws.ItemId = i.Id";

                    if (warehouseId > 0) query += " AND ws.WarehouseId = @WarehouseId";
                    query += " WHERE i.IsActive = 1 GROUP BY c.CategoryName ORDER BY c.CategoryName";

                    _reportData = DatabaseHelper.ExecuteQuery(query,
                        new SqlParameter("@WarehouseId", warehouseId > 0 ? (object)warehouseId : DBNull.Value));
                }
                // ==========================================
                // 4. ΧΑΜΗΛΟ / ΑΡΝΗΤΙΚΟ ΑΠΟΘΕΜΑ
                // ==========================================
                else if (reportType.Contains("Χαμηλό"))
                {
                    lblTitle.Text = "⚠️ Χαμηλό/Αρνητικό Απόθεμα";
                    string query = @"
                        SELECT i.ItemCode AS [Κωδικός], i.ItemName AS [Είδος], u.UnitName AS [Μ.Μ.],
                               i.CurrentStock AS [Τρέχον], i.SafetyStock AS [Ελάχιστο],
                               (i.SafetyStock - i.CurrentStock) AS [Έλλειψη]
                        FROM Items i 
                        LEFT JOIN Units u ON i.PrimaryUnitId = u.Id
                        WHERE i.IsActive = 1 AND (i.CurrentStock <= i.SafetyStock OR i.CurrentStock < 0)
                        ORDER BY i.CurrentStock ASC";

                    _reportData = DatabaseHelper.ExecuteQuery(query);
                }
                // ==========================================
                // 5. ΑΝΑΛΥΣΗ ΧΡΩΜΑΤΟΣ / ΜΕΓΕΘΟΥΣ (✅ ΠΛΗΡΩΣ ΔΙΟΡΘΩΜΕΝΟ)
                // ==========================================
                else if (reportType.Contains("Χρώμα"))
                {
                    lblTitle.Text = "🎨 Ανάλυση Χρώμα / Μέγεθος";
                    string query = @"
                        SELECT i.ItemCode AS [Κωδικός], i.ItemName AS [Είδος], c.CategoryName AS [Κατηγορία],
                               COALESCE(ic_ws.ColorName, ic_item.ColorName, i.Color, 'Μη καθορισμένο') AS [Χρώμα], 
                               COALESCE(isz_ws.SizeName, isz_item.SizeName, i.Size, 'Μη καθορισμένο') AS [Μέγεθος], 
                               COALESCE(ws.Qty, i.CurrentStock, 0) AS [Ποσότητα], 
                               i.RetailPrice AS [Τιμή], 
                               COALESCE(ws.Qty, i.CurrentStock, 0) * i.RetailPrice AS [Αξία]
                        FROM Items i
                        LEFT JOIN ItemCategories c ON i.CategoryId = c.Id
                        LEFT JOIN WarehouseStock ws ON ws.ItemId = i.Id";

                    if (warehouseId > 0) query += " AND ws.WarehouseId = @WarehouseId";

                    query += @"
                        -- 1η προτεραιότητα: Χρώμα/Μέγεθος από τη γραμμή αποθήκης
                        LEFT JOIN ItemColors ic_ws ON ws.ColorId = ic_ws.Id
                        LEFT JOIN ItemSizes isz_ws ON ws.SizeId = isz_ws.Id
                        -- 2η προτεραιότητα: Χρώμα/Μέγεθος από τις προεπιλογές του είδους (ID)
                        LEFT JOIN ItemColors ic_item ON i.ColorId = ic_item.Id
                        LEFT JOIN ItemSizes isz_item ON i.SizeId = isz_item.Id
                        WHERE i.IsActive = 1
                        ORDER BY i.ItemCode, [Χρώμα], [Μέγεθος]";

                    _reportData = DatabaseHelper.ExecuteQuery(query,
                        new SqlParameter("@WarehouseId", warehouseId > 0 ? (object)warehouseId : DBNull.Value));
                }
                // ==========================================
                // 6. ΤΙΜΟΛΟΓΗΣΗ ΕΙΔΩΝ
                // ==========================================
                else if (reportType.Contains("Τιμολόγηση"))
                {
                    lblTitle.Text = "🏷️ Τιμολόγηση Ειδών";
                    string query = @"
                        SELECT i.ItemCode AS [Κωδικός], i.ItemName AS [Όνομα], c.CategoryName AS [Κατηγορία],
                               i.Classification AS [Τύπος], s.SupplierName AS [Προμηθευτής],
                               i.PurchasePrice AS [Κόστος], i.WholesalePrice AS [Χονδρική], i.RetailPrice AS [Λιανική]
                        FROM Items i
                        LEFT JOIN ItemCategories c ON i.CategoryId = c.Id
                        LEFT JOIN Suppliers s ON i.SupplierId = s.Id
                        WHERE i.IsActive = 1";

                    if (categoryId > 0) query += " AND i.CategoryId = @CategoryId";
                    if (itemType != "Όλοι") query += " AND i.Classification = @ItemType";
                    query += " ORDER BY i.ItemCode";

                    _reportData = DatabaseHelper.ExecuteQuery(query,
                        new SqlParameter("@CategoryId", categoryId > 0 ? (object)categoryId : DBNull.Value),
                        new SqlParameter("@ItemType", itemType));
                }
                // ==========================================
                // 7. ΕΙΔΗ ΑΝΑ ΠΡΟΜΗΘΕΥΤΗ
                // ==========================================
                else if (reportType.Contains("Προμηθευτή"))
                {
                    lblTitle.Text = "📦 Είδη ανά Προμηθευτή";
                    int supplierId = cmbSupplier.SelectedValue != null ? Convert.ToInt32(cmbSupplier.SelectedValue) : 0;
                    string query = @"
                        SELECT s.SupplierName AS [Προμηθευτής], i.ItemCode AS [Κωδικός], i.ItemName AS [Όνομα],
                               c.CategoryName AS [Κατηγορία], i.Classification AS [Τύπος], u.UnitName AS [Μ.Μ.],
                               i.RetailPrice AS [Λιανική], i.WholesalePrice AS [Χονδρική],
                               i.PurchasePrice AS [Αγορά], i.CurrentStock AS [Απόθεμα]
                        FROM Items i
                        LEFT JOIN Suppliers s ON i.SupplierId = s.Id
                        LEFT JOIN ItemCategories c ON i.CategoryId = c.Id
                        LEFT JOIN Units u ON i.PrimaryUnitId = u.Id
                        WHERE i.IsActive = 1";

                    if (supplierId > 0) query += " AND i.SupplierId = @SupplierId";
                    query += " ORDER BY s.SupplierName, i.ItemCode";

                    _reportData = DatabaseHelper.ExecuteQuery(query,
                        new SqlParameter("@SupplierId", supplierId > 0 ? (object)supplierId : DBNull.Value));
                }

                dgvReport.DataSource = _reportData;
                FormatGrid();
                UpdateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα εκτέλεσης αναφοράς: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            dgvReport.RowsDefaultCellStyle.BackColor = Color.White;
            dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(212, 175, 55);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvReport.Columns)
            {
                if (col.HeaderText.Contains("Αξία") || col.HeaderText.Contains("Τιμή") ||
                    col.HeaderText.Contains("Λιανική") || col.HeaderText.Contains("Αγορά") ||
                    col.HeaderText.Contains("Κόστος") || col.HeaderText.Contains("Χονδρική"))
                {
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (col.HeaderText.Contains("Ποσότητα") || col.HeaderText.Contains("Πλήθος") ||
                    col.HeaderText.Contains("Τρέχον") || col.HeaderText.Contains("Ελάχιστο") ||
                    col.HeaderText.Contains("Απόθεμα"))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // ✅ Κόκκινο χρώμα και Bold για ΑΡΝΗΤΙΚΕΣ ποσότητες
            dgvReport.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvReport.Columns[e.ColumnIndex].HeaderText.Contains("Ποσότητα"))
                {
                    if (dgvReport.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        decimal val = Convert.ToDecimal(dgvReport.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        if (val < 0)
                        {
                            dgvReport.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Red;
                            dgvReport.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font(dgvReport.Font, FontStyle.Bold);
                        }
                    }
                }
            };
        }

        private void UpdateTotals()
        {
            if (_reportData == null) return;
            int totalRows = _reportData.Rows.Count;
            decimal totalValue = 0;
            decimal totalQty = 0;

            foreach (DataRow row in _reportData.Rows)
            {
                foreach (DataColumn col in _reportData.Columns)
                {
                    if (row[col] != DBNull.Value)
                    {
                        if (col.ColumnName.Contains("Αξία"))
                            totalValue += Convert.ToDecimal(row[col]);
                        if (col.ColumnName.Contains("Ποσότητα") || col.ColumnName.Contains("Απόθεμα") || col.ColumnName.Contains("Τρέχον"))
                            totalQty += Convert.ToDecimal(row[col]);
                    }
                }
            }

            lblTotalRecords.Text = $"Συνολικά: {totalRows} εγγραφές | Ποσότητα: {totalQty:N2}";
            lblTotalValue.Text = $"Συνολική Αξία: {totalValue:N2} €";
            lblTotalRecords.ForeColor = totalQty < 0 ? Color.Red : Color.Black;
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0)
            {
                MessageBox.Show("Δεν υπάρχουν δεδομένα για εξαγωγή!", "Προσοχή");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "CSV Files|*.csv",
                FileName = $"WarehouseReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < _reportData.Columns.Count; i++)
                {
                    sb.Append("\"" + _reportData.Columns[i].ColumnName + "\"");
                    if (i < _reportData.Columns.Count - 1) sb.Append(",");
                }
                sb.AppendLine();

                foreach (DataRow row in _reportData.Rows)
                {
                    for (int i = 0; i < _reportData.Columns.Count; i++)
                    {
                        string val = row[i].ToString().Replace("\"", "\"\"");
                        sb.Append("\"" + val + "\"");
                        if (i < _reportData.Columns.Count - 1) sb.Append(",");
                    }
                    sb.AppendLine();
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show($"✅ Η αναφορά εξήχθη επιτυχώς στο:\n{sfd.FileName}", "Επιτυχία");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e) => MessageBox.Show("Η εκτύπωση θα υλοποιηθεί στο επόμενο βήμα!", "Πληροφόρηση");
        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}