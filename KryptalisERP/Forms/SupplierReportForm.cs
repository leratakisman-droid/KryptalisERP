using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class SupplierReportsForm : Form
    {
        private DataTable _reportData;
        private string _reportTitle;
        private int _currentRow;
        private Font _titleFont;
        private Font _headerFont;
        private Font _bodyFont;
        private Font _footerFont;
        private decimal _totalAmount;

        public SupplierReportsForm()
        {
            InitializeComponent();
            LoadReportTypes();
            LoadSuppliers();

            // Default ημερομηνίες (τρέχοντα μήνα)
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;

            // Event για αλλαγή προμηθευτή -> φόρτωση υποκαταστημάτων
            cmbSupplier.SelectedIndexChanged += CmbSupplier_SelectedIndexChanged;
        }

        private void CmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranches();
        }

        private void LoadReportTypes()
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("📋 Υπόλοιπα ανά Προμηθευτή");
            cmbReportType.Items.Add("🏢 Υπόλοιπα ανά Υποκατάστημα");
            cmbReportType.Items.Add("💳 Καρτέλα Προμηθευτή");
            cmbReportType.SelectedIndex = 0;
        }

        private void LoadSuppliers()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, SupplierName FROM Suppliers WHERE Energo = 1 ORDER BY SupplierName");

                cmbSupplier.DataSource = dt;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "Id";
                cmbSupplier.SelectedIndex = -1;
            }
            catch { }
        }

        private void LoadBranches()
        {
            try
            {
                cmbBranch.DataSource = null;
                cmbBranch.Items.Clear();
                cmbBranch.Items.Add("Όλα τα Υποκαταστήματα");

                if (cmbSupplier.SelectedValue == null) return;

                int supplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, BranchName FROM SupplierBranches WHERE SupplierId = @Id AND IsActive = 1 ORDER BY BranchName",
                    new SqlParameter("@Id", supplierId));

                foreach (DataRow row in dt.Rows)
                {
                    cmbBranch.Items.Add(row["BranchName"].ToString() + " [ID:" + row["Id"].ToString() + "]");
                }

                cmbBranch.SelectedIndex = 0;
            }
            catch { }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            int selectedType = cmbReportType.SelectedIndex;
            int? supplierId = cmbSupplier.SelectedValue != null ? (int?)Convert.ToInt32(cmbSupplier.SelectedValue) : null;
            string branchFilter = cmbBranch.SelectedIndex > 0 ? cmbBranch.SelectedItem.ToString() : null;
            DateTime dateFrom = dtpFrom.Value.Date;
            DateTime dateTo = dtpTo.Value.Date;

            try
            {
                _reportData = null;
                _totalAmount = 0;

                if (selectedType == 0) // Υπόλοιπα ανά Προμηθευτή
                {
                    _reportTitle = "ΥΠΟΛΟΙΠΑ ΑΝΑ ΠΡΟΜΗΘΕΥΤΗ";
                    _reportData = GetSupplierBalances(supplierId, dateFrom, dateTo);
                }
                else if (selectedType == 1) // Υπόλοιπα ανά Υποκατάστημα
                {
                    _reportTitle = "ΥΠΟΛΟΙΠΑ ΑΝΑ ΥΠΟΚΑΤΑΣΤΗΜΑ";
                    _reportData = GetBranchBalances(supplierId, branchFilter, dateFrom, dateTo);
                }
                else if (selectedType == 2) // Καρτέλα Προμηθευτή
                {
                    if (supplierId == null)
                    {
                        MessageBox.Show("⚠️ Επίλεξε προμηθευτή για την καρτέλα!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _reportTitle = "ΚΑΡΤΕΛΑ ΠΡΟΜΗΘΕΥΤΗ";
                    _reportData = GetSupplierCard(supplierId.Value, branchFilter, dateFrom, dateTo);
                }

                if (_reportData != null && _reportData.Rows.Count > 0)
                {
                    dgvReport.DataSource = _reportData;
                    dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    lblCount.Text = $"Συνολικά: {_reportData.Rows.Count} εγγραφές";

                    // Υπολογισμός συνολικού ποσού
                    foreach (DataRow row in _reportData.Rows)
                    {
                        foreach (DataColumn col in _reportData.Columns)
                        {
                            if (col.ColumnName.Contains("Ποσό") || col.ColumnName.Contains("Υπόλοιπο") || col.ColumnName.Contains("Χρέωση") || col.ColumnName.Contains("Πίστωση"))
                            {
                                if (decimal.TryParse(row[col].ToString(), out decimal val))
                                    _totalAmount += val;
                            }
                        }
                    }
                    lblTotalAmount.Text = $"Συνολικό Ποσό: {_totalAmount:N2} €";

                    btnPrint.Enabled = true;
                    btnPrintPreview.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Δεν βρέθηκαν δεδομένα για τα επιλεγμένα φίλτρα.", "Πληροφόρηση", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvReport.DataSource = null;
                    lblCount.Text = "Συνολικά: 0 εγγραφές";
                    lblTotalAmount.Text = "Συνολικό Ποσό: 0,00 €";
                    btnPrint.Enabled = false;
                    btnPrintPreview.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα δημιουργίας αναφοράς: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ ΥΠΟΛΟΙΠΑ ΑΝΑ ΠΡΟΜΗΘΕΥΤΗ
        private DataTable GetSupplierBalances(int? supplierId, DateTime dateFrom, DateTime dateTo)
        {
            string sql = @"
                SELECT 
                    s.SupplierName AS [Προμηθευτής],
                    s.AFM AS [ΑΦΜ],
                    s.City AS [Πόλη],
                    s.Email AS [Email],
                    ISNULL(SUM(CASE WHEN i.InvoiceType = 'Αγορά' THEN i.TotalAmount ELSE 0 END), 0) AS [Συν. Αγορών],
                    ISNULL(SUM(CASE WHEN i.InvoiceType = 'Πίστωση' THEN i.TotalAmount ELSE 0 END), 0) AS [Συν. Πιστώσεων],
                    ISNULL(SUM(CASE WHEN i.InvoiceType = 'Αγορά' THEN i.TotalAmount ELSE 0 END), 0) - 
                    ISNULL(SUM(CASE WHEN i.InvoiceType = 'Πίστωση' THEN i.TotalAmount ELSE 0 END), 0) AS [Υπόλοιπο]
                FROM Suppliers s
                LEFT JOIN Invoices i ON s.Id = i.SupplierId 
                    AND i.InvoiceDate >= @DateFrom AND i.InvoiceDate <= @DateTo
                WHERE (@SupplierId IS NULL OR s.Id = @SupplierId)
                  AND s.Energo = 1
                GROUP BY s.SupplierName, s.AFM, s.City, s.Email
                HAVING ISNULL(SUM(CASE WHEN i.InvoiceType = 'Αγορά' THEN i.TotalAmount ELSE 0 END), 0) - 
                       ISNULL(SUM(CASE WHEN i.InvoiceType = 'Πίστωση' THEN i.TotalAmount ELSE 0 END), 0) <> 0
                ORDER BY s.SupplierName";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@SupplierId", supplierId.HasValue ? (object)supplierId.Value : DBNull.Value),
                new SqlParameter("@DateFrom", dateFrom),
                new SqlParameter("@DateTo", dateTo));
        }

        // ✅ ΥΠΟΛΟΙΠΑ ΑΝΑ ΥΠΟΚΑΤΑΣΤΗΜΑ
        private DataTable GetBranchBalances(int? supplierId, string branchFilter, DateTime dateFrom, DateTime dateTo)
        {
            string sql = @"
                SELECT 
                    s.SupplierName AS [Προμηθευτής],
                    ISNULL(sb.BranchName, 'Έδρα') AS [Υποκατάστημα],
                    sb.BranchCode AS [Κωδικός],
                    sb.City AS [Πόλη],
                    sb.Phone1 AS [Τηλέφωνο],
                    sb.DOY AS [ΔΟΥ],
                    ISNULL(SUM(CASE WHEN i.InvoiceType = 'Αγορά' THEN i.TotalAmount ELSE 0 END), 0) AS [Συν. Αγορών],
                    ISNULL(SUM(CASE WHEN i.InvoiceType = 'Πίστωση' THEN i.TotalAmount ELSE 0 END), 0) AS [Συν. Πιστώσεων],
                    ISNULL(SUM(CASE WHEN i.InvoiceType = 'Αγορά' THEN i.TotalAmount ELSE 0 END), 0) - 
                    ISNULL(SUM(CASE WHEN i.InvoiceType = 'Πίστωση' THEN i.TotalAmount ELSE 0 END), 0) AS [Υπόλοιπο]
                FROM Suppliers s
                LEFT JOIN SupplierBranches sb ON s.Id = sb.SupplierId
                LEFT JOIN Invoices i ON s.Id = i.SupplierId 
                    AND (i.BranchId = sb.Id OR (i.BranchId IS NULL AND sb.Id IS NULL))
                    AND i.InvoiceDate >= @DateFrom AND i.InvoiceDate <= @DateTo
                WHERE (@SupplierId IS NULL OR s.Id = @SupplierId)
                  AND s.Energo = 1
                GROUP BY s.SupplierName, sb.BranchName, sb.BranchCode, sb.City, sb.Phone1, sb.DOY
                HAVING ISNULL(SUM(CASE WHEN i.InvoiceType = 'Αγορά' THEN i.TotalAmount ELSE 0 END), 0) - 
                       ISNULL(SUM(CASE WHEN i.InvoiceType = 'Πίστωση' THEN i.TotalAmount ELSE 0 END), 0) <> 0
                ORDER BY s.SupplierName, sb.BranchName";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@SupplierId", supplierId.HasValue ? (object)supplierId.Value : DBNull.Value),
                new SqlParameter("@DateFrom", dateFrom),
                new SqlParameter("@DateTo", dateTo));
        }

        // ✅ ΚΑΡΤΕΛΑ ΠΡΟΜΗΘΕΥΤΗ
        private DataTable GetSupplierCard(int supplierId, string branchFilter, DateTime dateFrom, DateTime dateTo)
        {
            string sql = @"
                SELECT 
                    i.InvoiceDate AS [Ημερομηνία],
                    i.InvoiceNumber AS [Αριθμός Παραστατικού],
                    i.InvoiceType AS [Τύπος],
                    i.Description AS [Περιγραφή],
                    i.TotalAmount AS [Ποσό],
                    CASE WHEN i.IsPaid = 1 THEN 'Πληρωμένο' ELSE 'Απλήρωτο' END AS [Κατάσταση]
                FROM Invoices i
                WHERE i.SupplierId = @SupplierId
                  AND i.InvoiceDate >= @DateFrom AND i.InvoiceDate <= @DateTo
                ORDER BY i.InvoiceDate, i.InvoiceNumber";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@SupplierId", supplierId),
                new SqlParameter("@DateFrom", dateFrom),
                new SqlParameter("@DateTo", dateTo));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0)
            {
                MessageBox.Show("Δεν υπάρχουν δεδομένα για εκτύπωση!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;
            printDoc.BeginPrint += PrintDoc_BeginPrint;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0)
            {
                MessageBox.Show("Δεν υπάρχουν δεδομένα για προεπισκόπηση!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;
            printDoc.BeginPrint += PrintDoc_BeginPrint;

            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDoc;
            previewDialog.Width = 900;
            previewDialog.Height = 700;
            previewDialog.ShowDialog();
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

            // Τίτλος
            e.Graphics.DrawString(_reportTitle, _titleFont, Brushes.Black, leftMargin, currentY);
            currentY += 30;

            // Φίλτρα
            string filters = $"Ημερομηνία: {dtpFrom.Value:dd/MM/yyyy} - {dtpTo.Value:dd/MM/yyyy}";
            if (cmbSupplier.SelectedValue != null)
                filters += $" | Προμηθευτής: {cmbSupplier.Text}";
            if (cmbBranch.SelectedIndex > 0)
                filters += $" | Υποκατάστημα: {cmbBranch.Text}";

            e.Graphics.DrawString(filters, _bodyFont, Brushes.Gray, leftMargin, currentY);
            currentY += 25;

            // Headers
            float colX = leftMargin;
            float colWidth = (pageWidth - 80) / _reportData.Columns.Count;

            foreach (DataColumn col in _reportData.Columns)
            {
                e.Graphics.DrawString(col.ColumnName, _headerFont, Brushes.Black, colX, currentY);
                colX += colWidth;
            }
            currentY += 20;

            // Γραμμή
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, pageWidth - 40, currentY);
            currentY += 10;

            // Δεδομένα
            while (_currentRow < _reportData.Rows.Count && currentY < pageHeight - 60)
            {
                DataRow row = _reportData.Rows[_currentRow];
                colX = leftMargin;

                foreach (DataColumn col in _reportData.Columns)
                {
                    string cellValue = row[col].ToString();
                    e.Graphics.DrawString(cellValue, _bodyFont, Brushes.Black, colX, currentY);
                    colX += colWidth;
                }

                currentY += 18;
                _currentRow++;
            }

            // Υποσέλιδο
            e.Graphics.DrawLine(Pens.Black, leftMargin, pageHeight - 50, pageWidth - 40, pageHeight - 50);
            e.Graphics.DrawString($"Συνολικό Ποσό: {_totalAmount:N2} €", _headerFont, Brushes.Black, leftMargin, pageHeight - 40);
            e.Graphics.DrawString($"Ημερομηνία Εκτύπωσης: {DateTime.Now:dd/MM/yyyy HH:mm}", _footerFont, Brushes.Gray, leftMargin, pageHeight - 25);
            e.Graphics.DrawString("KRYPTALIS ERP", _footerFont, Brushes.Gray, pageWidth - 150, pageHeight - 25);

            e.HasMorePages = _currentRow < _reportData.Rows.Count;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}