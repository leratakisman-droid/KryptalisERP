using ClosedXML.Excel;
using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemReportsForm : Form
    {
        private DataTable _reportData;
        private string _reportTitle;
        private int _currentRow;
        private Font _titleFont, _headerFont, _bodyFont, _footerFont;
        private decimal _totalAmount;

        public ItemReportsForm()
        {
            InitializeComponent();
            LoadReportTypes();
            LoadFilters();

            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;
        }

        private void LoadReportTypes()
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("📋 Είδη ανά Προμηθευτή");
            cmbReportType.Items.Add("🏭 Προμηθευτές ανά Είδος");
            cmbReportType.Items.Add(" Ημερολόγιο Αποθήκης");
            cmbReportType.Items.Add("️ Είδη με Χαμηλό Απόθεμα");
            cmbReportType.Items.Add(" Τιμολόγηση Ειδών");
            cmbReportType.SelectedIndex = 0;
        }

        private void LoadFilters()
        {
            // Κατηγορίες
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT CategoryName FROM ItemCategories WHERE IsActive = 1 ORDER BY CategoryName");
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("Όλες");
                foreach (DataRow row in dt.Rows) cmbCategory.Items.Add(row["CategoryName"].ToString());
                cmbCategory.SelectedIndex = 0;
            }
            catch { }

            // Προμηθευτές
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName");
                cmbSupplier.Items.Clear();
                cmbSupplier.Items.Add("Όλοι");
                foreach (DataRow row in dt.Rows) cmbSupplier.Items.Add(row["SupplierName"].ToString());
                cmbSupplier.SelectedIndex = 0;
            }
            catch { }

            // Τύπος
            cmbItemType.Items.Clear();
            cmbItemType.Items.Add("Όλοι");
            cmbItemType.Items.Add("Εμπόρευμα");
            cmbItemType.Items.Add("Υπηρεσία");
            cmbItemType.Items.Add("Ανταλλακτικό");
            cmbItemType.SelectedIndex = 0;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int selectedType = cmbReportType.SelectedIndex;
            string category = cmbCategory.SelectedIndex > 0 ? cmbCategory.SelectedItem.ToString() : null;
            string supplier = cmbSupplier.SelectedIndex > 0 ? cmbSupplier.SelectedItem.ToString() : null;
            string itemType = cmbItemType.SelectedIndex > 0 ? cmbItemType.SelectedItem.ToString() : null;
            DateTime dateFrom = dtpFrom.Value.Date;
            DateTime dateTo = dtpTo.Value.Date;

            try
            {
                _reportData = null;
                _totalAmount = 0;

                switch (selectedType)
                {
                    case 0: // Είδη ανά Προμηθευτή
                        _reportTitle = "ΕΙΔΗ ΑΝΑ ΠΡΟΜΗΘΕΥΤΗ";
                        _reportData = GetItemsBySupplier(category, supplier, itemType);
                        break;
                    case 1: // Προμηθευτές ανά Είδος
                        _reportTitle = "ΠΡΟΜΗΘΕΥΤΕΣ ΑΝΑ ΕΙΔΟΣ";
                        _reportData = GetSuppliersByItem(category, supplier, itemType);
                        break;
                    case 2: // Ημερολόγιο Αποθήκης
                        _reportTitle = "ΗΜΕΡΟΛΟΓΙΟ ΑΠΟΘΗΚΗΣ";
                        _reportData = GetStockCalendar(dateFrom, dateTo, category, supplier);
                        break;
                    case 3: // Χαμηλό Απόθεμα
                        _reportTitle = "ΕΙΔΗ ΜΕ ΧΑΜΗΛΟ ΑΠΟΘΕΜΑ";
                        _reportData = GetLowStockItems(category);
                        break;
                    case 4: // Τιμολόγηση
                        _reportTitle = "ΤΙΜΟΛΟΓΗΣΗ ΕΙΔΩΝ";
                        _reportData = GetItemPricing(category, supplier, itemType);
                        break;
                }

                if (_reportData != null && _reportData.Rows.Count > 0)
                {
                    dgvReport.DataSource = _reportData;
                    dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    lblCount.Text = $"Συνολικά: {_reportData.Rows.Count} εγγραφές";

                    foreach (DataRow row in _reportData.Rows)
                    {
                        foreach (DataColumn col in _reportData.Columns)
                        {
                            if (col.ColumnName.Contains("Ποσό") || col.ColumnName.Contains("Αξία") ||
                                col.ColumnName.Contains("Τιμή") || col.ColumnName.Contains("Ποσότητα"))
                            {
                                if (decimal.TryParse(row[col].ToString(), out decimal val))
                                    _totalAmount += val;
                            }
                        }
                    }
                    lblTotalAmount.Text = $"Συνολική Αξία: {_totalAmount:N2} €";

                    btnPrint.Enabled = true;
                    btnPrintPreview.Enabled = true;
                    btnExportExcel.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Δεν βρέθηκαν δεδομένα!", "Πληροφόρηση");
                    dgvReport.DataSource = null;
                    lblCount.Text = "Συνολικά: 0 εγγραφές";
                    lblTotalAmount.Text = "Συνολική Αξία: 0,00 €";
                    btnPrint.Enabled = false;
                    btnPrintPreview.Enabled = false;
                    btnExportExcel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private DataTable GetItemsBySupplier(string category, string supplier, string itemType)
        {
            string sql = @"SELECT 
                s.SupplierName AS [Προμηθευτής],
                i.ItemCode AS [Κωδικός],
                i.ItemName AS [Όνομα],
                i.ItemFamily AS [Κατηγορία],
                i.Classification AS [Τύπος],
                i.PrimaryUnit AS [Μ.Μ.],
                i.RetailPrice AS [Λιανική],
                i.WholesalePrice AS [Χονδρική],
                i.PurchasePrice AS [Αγορά],
                i.CurrentStock AS [Απόθεμα]
            FROM Items i
            LEFT JOIN Suppliers s ON i.SupplierId = s.Id
            WHERE i.IsActive = 1
              AND (@Category IS NULL OR i.ItemFamily = @Category)
              AND (@Supplier IS NULL OR s.SupplierName = @Supplier)
              AND (@ItemType IS NULL OR i.Classification = @ItemType)
            ORDER BY s.SupplierName, i.ItemCode";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@Category", (object)category ?? DBNull.Value),
                new SqlParameter("@Supplier", (object)supplier ?? DBNull.Value),
                new SqlParameter("@ItemType", (object)itemType ?? DBNull.Value));
        }

        private DataTable GetSuppliersByItem(string category, string supplier, string itemType)
        {
            string sql = @"SELECT 
                i.ItemCode AS [Κωδικός],
                i.ItemName AS [Όνομα],
                i.ItemFamily AS [Κατηγορία],
                s.SupplierName AS [Προμηθευτής],
                i.PurchasePrice AS [Τιμή Αγοράς],
                i.CurrentStock AS [Απόθεμα]
            FROM Items i
            LEFT JOIN Suppliers s ON i.SupplierId = s.Id
            WHERE i.IsActive = 1
              AND (@Category IS NULL OR i.ItemFamily = @Category)
              AND (@Supplier IS NULL OR s.SupplierName = @Supplier)
              AND (@ItemType IS NULL OR i.Classification = @ItemType)
            ORDER BY i.ItemCode";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@Category", (object)category ?? DBNull.Value),
                new SqlParameter("@Supplier", (object)supplier ?? DBNull.Value),
                new SqlParameter("@ItemType", (object)itemType ?? DBNull.Value));
        }

        private DataTable GetStockCalendar(DateTime dateFrom, DateTime dateTo, string category, string supplier)
        {
            string sql = @"SELECT 
                sm.MovementDate AS [Ημερομηνία],
                i.ItemCode AS [Κωδικός],
                i.ItemName AS [Όνομα],
                sm.MovementType AS [Τύπος Κίνησης],
                sm.Quantity AS [Ποσότητα],
                sm.UnitPrice AS [Τ.Μ.],
                sm.TotalValue AS [Αξία],
                sm.ReferenceType AS [Παραστατικό],
                sm.Notes AS [Σημειώσεις]
            FROM StockMovements sm
            INNER JOIN Items i ON sm.ItemId = i.Id
            LEFT JOIN Suppliers s ON i.SupplierId = s.Id
            WHERE sm.MovementDate >= @DateFrom AND sm.MovementDate <= @DateTo
              AND (@Category IS NULL OR i.ItemFamily = @Category)
              AND (@Supplier IS NULL OR s.SupplierName = @Supplier)
            ORDER BY sm.MovementDate DESC, i.ItemCode";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@DateFrom", dateFrom),
                new SqlParameter("@DateTo", dateTo),
                new SqlParameter("@Category", (object)category ?? DBNull.Value),
                new SqlParameter("@Supplier", (object)supplier ?? DBNull.Value));
        }

        private DataTable GetLowStockItems(string category)
        {
            string sql = @"SELECT 
                i.ItemCode AS [Κωδικός],
                i.ItemName AS [Όνομα],
                i.ItemFamily AS [Κατηγορία],
                i.PrimaryUnit AS [Μ.Μ.],
                i.CurrentStock AS [Τρέχον],
                i.SafetyStock AS [Ασφάλεια],
                (i.SafetyStock - i.CurrentStock) AS [Έλλειμμα],
                i.RetailPrice AS [Λιανική]
            FROM Items i
            WHERE i.IsActive = 1
              AND i.CurrentStock <= i.SafetyStock
              AND (@Category IS NULL OR i.ItemFamily = @Category)
            ORDER BY (i.SafetyStock - i.CurrentStock) DESC";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@Category", (object)category ?? DBNull.Value));
        }

        private DataTable GetItemPricing(string category, string supplier, string itemType)
        {
            string sql = @"SELECT 
                i.ItemCode AS [Κωδικός],
                i.ItemName AS [Όνομα],
                i.ItemFamily AS [Κατηγορία],
                i.Classification AS [Τύπος],
                s.SupplierName AS [Προμηθευτής],
                i.PurchasePrice AS [Κόστος],
                i.WholesalePrice AS [Χονδρική],
                i.RetailPrice AS [Λιανική],
                CASE WHEN i.PurchasePrice > 0 
                    THEN ROUND((i.RetailPrice - i.PurchasePrice) / i.PurchasePrice * 100, 2) 
                    ELSE 0 END AS [MarkUp %]
            FROM Items i
            LEFT JOIN Suppliers s ON i.SupplierId = s.Id
            WHERE i.IsActive = 1
              AND (@Category IS NULL OR i.ItemFamily = @Category)
              AND (@Supplier IS NULL OR s.SupplierName = @Supplier)
              AND (@ItemType IS NULL OR i.Classification = @ItemType)
            ORDER BY i.ItemCode";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@Category", (object)category ?? DBNull.Value),
                new SqlParameter("@Supplier", (object)supplier ?? DBNull.Value),
                new SqlParameter("@ItemType", (object)itemType ?? DBNull.Value));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0) return;
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;
            printDoc.BeginPrint += PrintDoc_BeginPrint;
            PrintDialog printDialog = new PrintDialog { Document = printDoc };
            if (printDialog.ShowDialog() == DialogResult.OK) printDoc.Print();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0) return;
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;
            printDoc.BeginPrint += PrintDoc_BeginPrint;
            PrintPreviewDialog previewDialog = new PrintPreviewDialog { Document = printDoc, Width = 900, Height = 700 };
            previewDialog.ShowDialog();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0) return;
            try
            {
                SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel Files|*.xlsx", FileName = "Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx" };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Αναφορά");
                        for (int i = 0; i < dgvReport.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgvReport.Columns[i].HeaderText;
                            worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(212, 175, 55);
                        }
                        for (int i = 0; i < dgvReport.Rows.Count; i++)
                            for (int j = 0; j < dgvReport.Columns.Count; j++)
                                worksheet.Cell(i + 2, j + 1).Value = dgvReport.Rows[i].Cells[j].Value?.ToString() ?? "";

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                    }
                    MessageBox.Show("✅ Εξήχθη επιτυχώς!", "Επιτυχία");
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void PrintDoc_BeginPrint(object sender, PrintEventArgs e)
        {
            _currentRow = 0;
            _titleFont = new Font("Arial", 14, FontStyle.Bold);
            _headerFont = new Font("Arial", 9, FontStyle.Bold);
            _bodyFont = new Font("Arial", 8);
            _footerFont = new Font("Arial", 7, FontStyle.Italic);
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            float pageWidth = e.PageBounds.Width;
            float pageHeight = e.PageBounds.Height;
            float leftMargin = 40;
            float currentY = 40;

            e.Graphics.DrawString(_reportTitle, _titleFont, Brushes.Black, leftMargin, currentY);
            currentY += 30;

            string filters = $"Ημερομηνία: {dtpFrom.Value:dd/MM/yyyy} - {dtpTo.Value:dd/MM/yyyy}";
            if (cmbCategory.SelectedIndex > 0) filters += $" | Κατηγορία: {cmbCategory.Text}";
            if (cmbSupplier.SelectedIndex > 0) filters += $" | Προμηθευτής: {cmbSupplier.Text}";
            if (cmbItemType.SelectedIndex > 0) filters += $" | Τύπος: {cmbItemType.Text}";

            e.Graphics.DrawString(filters, _bodyFont, Brushes.Gray, leftMargin, currentY);
            currentY += 25;

            float colX = leftMargin;
            float colWidth = (pageWidth - 80) / _reportData.Columns.Count;

            foreach (DataColumn col in _reportData.Columns)
            {
                e.Graphics.DrawString(col.ColumnName, _headerFont, Brushes.Black, colX, currentY);
                colX += colWidth;
            }
            currentY += 20;
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, pageWidth - 40, currentY);
            currentY += 10;

            while (_currentRow < _reportData.Rows.Count && currentY < pageHeight - 60)
            {
                DataRow row = _reportData.Rows[_currentRow];
                colX = leftMargin;
                foreach (DataColumn col in _reportData.Columns)
                {
                    e.Graphics.DrawString(row[col].ToString(), _bodyFont, Brushes.Black, colX, currentY);
                    colX += colWidth;
                }
                currentY += 18;
                _currentRow++;
            }

            e.Graphics.DrawLine(Pens.Black, leftMargin, pageHeight - 50, pageWidth - 40, pageHeight - 50);
            e.Graphics.DrawString($"Συνολική Αξία: {_totalAmount:N2} €", _headerFont, Brushes.Black, leftMargin, pageHeight - 40);
            e.Graphics.DrawString($"Ημερομηνία: {DateTime.Now:dd/MM/yyyy HH:mm}", _footerFont, Brushes.Gray, leftMargin, pageHeight - 25);
            e.Graphics.DrawString("KRYPTALIS ERP", _footerFont, Brushes.Gray, pageWidth - 150, pageHeight - 25);

            e.HasMorePages = _currentRow < _reportData.Rows.Count;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}