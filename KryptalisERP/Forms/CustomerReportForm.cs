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
    public partial class ClientReportsForm : Form
    {
        private DataTable _reportData;
        private string _reportTitle;
        private int _currentRow;
        private Font _titleFont;
        private Font _headerFont;
        private Font _bodyFont;
        private Font _footerFont;
        private decimal _totalAmount;

        public ClientReportsForm()
        {
            InitializeComponent();
            LoadReportTypes();
            LoadClients();

            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;

            cmbClient.SelectedIndexChanged += CmbClient_SelectedIndexChanged;
        }

        private void CmbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranches();
        }

        private void LoadReportTypes()
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("📋 Υπόλοιπα ανά Πελάτη");
            cmbReportType.Items.Add("🏢 Υπόλοιπα ανά Υποκατάστημα");
            cmbReportType.Items.Add("💳 Καρτέλα Πελάτη");
            cmbReportType.Items.Add("💳 Καρτέλα ανά Υποκατάστημα");
            cmbReportType.SelectedIndex = 0;
        }

        private void LoadClients()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, ISNULL(Company, Epwnymia) AS Epwnymia FROM Clients WHERE IsActive = 1 ORDER BY Epwnymia");

                cmbClient.DataSource = dt;
                cmbClient.DisplayMember = "Epwnymia";
                cmbClient.ValueMember = "Id";
                cmbClient.SelectedIndex = -1;
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

                if (cmbClient.SelectedValue == null) return;

                int clientId = Convert.ToInt32(cmbClient.SelectedValue);
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, BranchName FROM ClientBranches WHERE ClientId = @Id AND IsActive = 1 ORDER BY BranchName",
                    new SqlParameter("@Id", clientId));

                foreach (DataRow row in dt.Rows)
                {
                    cmbBranch.Items.Add(row["BranchName"].ToString() + " [ID:" + row["Id"].ToString() + "]");
                }

                cmbBranch.SelectedIndex = 0;
            }
            catch { }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int selectedType = cmbReportType.SelectedIndex;
            int? clientId = cmbClient.SelectedValue != null ? (int?)Convert.ToInt32(cmbClient.SelectedValue) : null;
            string branchFilter = cmbBranch.SelectedIndex > 0 ? cmbBranch.SelectedItem.ToString() : null;
            DateTime dateFrom = dtpFrom.Value.Date;
            DateTime dateTo = dtpTo.Value.Date;

            try
            {
                _reportData = null;
                _totalAmount = 0;

                if (selectedType == 0) // Υπόλοιπα ανά Πελάτη
                {
                    _reportTitle = "ΥΠΟΛΟΙΠΑ ΑΝΑ ΠΕΛΑΤΗ";
                    _reportData = GetClientBalances(clientId, dateFrom, dateTo);
                }
                else if (selectedType == 1) // Υπόλοιπα ανά Υποκατάστημα
                {
                    _reportTitle = "ΥΠΟΛΟΙΠΑ ΑΝΑ ΥΠΟΚΑΤΑΣΤΗΜΑ";
                    _reportData = GetBranchBalances(clientId, branchFilter, dateFrom, dateTo);
                }
                else if (selectedType == 2) // Καρτέλα Πελάτη
                {
                    if (clientId == null)
                    {
                        MessageBox.Show("⚠️ Επίλεξε πελάτη για την καρτέλα!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _reportTitle = "ΚΑΡΤΕΛΑ ΠΕΛΑΤΗ";
                    _reportData = GetClientCard(clientId.Value, branchFilter, dateFrom, dateTo);
                }
                else if (selectedType == 3) // Καρτέλα ανά Υποκατάστημα
                {
                    if (clientId == null)
                    {
                        MessageBox.Show("⚠️ Επίλεξε πελάτη για την καρτέλα!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (cmbBranch.SelectedIndex == 0)
                    {
                        MessageBox.Show("⚠️ Επίλεξε συγκεκριμένο υποκατάστημα!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _reportTitle = "ΚΑΡΤΕΛΑ ΑΝΑ ΥΠΟΚΑΤΑΣΤΗΜΑ";
                    _reportData = GetClientCardByBranch(clientId.Value, branchFilter, dateFrom, dateTo);
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
                            if (col.ColumnName.Contains("Ποσό") || col.ColumnName.Contains("Υπόλοιπο") || col.ColumnName.Contains("Χρέωση") || col.ColumnName.Contains("Πίστωση") || col.ColumnName.Contains("Συνολικό"))
                            {
                                if (decimal.TryParse(row[col].ToString(), out decimal val))
                                    _totalAmount += val;
                            }
                        }
                    }
                    lblTotalAmount.Text = $"Συνολικό Ποσό: {_totalAmount:N2} €";

                    btnPrint.Enabled = true;
                    btnPrintPreview.Enabled = true;
                    btnExportExcel.Enabled = true;
                    btnExportPdf.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Δεν βρέθηκαν δεδομένα για τα επιλεγμένα φίλτρα.", "Πληροφόρηση", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvReport.DataSource = null;
                    lblCount.Text = "Συνολικά: 0 εγγραφές";
                    lblTotalAmount.Text = "Συνολικό Ποσό: 0,00 €";
                    btnPrint.Enabled = false;
                    btnPrintPreview.Enabled = false;
                    btnExportExcel.Enabled = false;
                    btnExportPdf.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα δημιουργίας αναφοράς: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ ΔΙΟΡΘΩΜΕΝΟ: Προσθήκη Ονομασίας και ΑΦΜ
        private DataTable GetClientBalances(int? clientId, DateTime dateFrom, DateTime dateTo)
        {
            string sql = @"
                SELECT 
                    ISNULL(c.Company, c.Epwnymia) AS [Ονομασία Πελάτη],
                    c.Afm AS [ΑΦΜ],
                    c.Poli AS [Πόλη],
                    c.Email AS [Email],
                    ISNULL(SUM(si.GrandTotal), 0) AS [Συνολικό Ποσό]
                FROM Clients c
                LEFT JOIN SalesInvoices si ON c.Id = si.ClientId 
                    AND si.DocDate >= @DateFrom AND si.DocDate <= @DateTo
                    AND si.IsActive = 1
                WHERE (@ClientId IS NULL OR c.Id = @ClientId)
                  AND c.IsActive = 1
                GROUP BY c.Company, c.Epwnymia, c.Afm, c.Poli, c.Email
                HAVING ISNULL(SUM(si.GrandTotal), 0) <> 0
                ORDER BY ISNULL(c.Company, c.Epwnymia)";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@ClientId", clientId.HasValue ? (object)clientId.Value : DBNull.Value),
                new SqlParameter("@DateFrom", dateFrom),
                new SqlParameter("@DateTo", dateTo));
        }

        // ✅ ΔΙΟΡΘΩΜΕΝΟ: Προσθήκη Ονομασίας και ΑΦΜ
        private DataTable GetBranchBalances(int? clientId, string branchFilter, DateTime dateFrom, DateTime dateTo)
        {
            string sql = @"
                SELECT 
                    ISNULL(c.Company, c.Epwnymia) AS [Ονομασία Πελάτη],
                    c.Afm AS [ΑΦΜ],
                    ISNULL(cb.BranchName, 'Έδρα') AS [Υποκατάστημα],
                    cb.BranchCode AS [Κωδικός],
                    cb.City AS [Πόλη],
                    cb.Phone1 AS [Τηλέφωνο],
                    cb.DOY AS [ΔΟΥ],
                    ISNULL(SUM(si.GrandTotal), 0) AS [Συνολικό Ποσό]
                FROM Clients c
                LEFT JOIN ClientBranches cb ON c.Id = cb.ClientId
                LEFT JOIN SalesInvoices si ON c.Id = si.ClientId 
                    AND (si.BranchId = cb.Id OR (si.BranchId IS NULL AND cb.Id IS NULL))
                    AND si.DocDate >= @DateFrom AND si.DocDate <= @DateTo
                    AND si.IsActive = 1
                WHERE (@ClientId IS NULL OR c.Id = @ClientId)
                  AND c.IsActive = 1
                GROUP BY c.Company, c.Epwnymia, c.Afm, cb.BranchName, cb.BranchCode, cb.City, cb.Phone1, cb.DOY
                HAVING ISNULL(SUM(si.GrandTotal), 0) <> 0
                ORDER BY ISNULL(c.Company, c.Epwnymia), cb.BranchName";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@ClientId", clientId.HasValue ? (object)clientId.Value : DBNull.Value),
                new SqlParameter("@DateFrom", dateFrom),
                new SqlParameter("@DateTo", dateTo));
        }

        // ✅ ΔΙΟΡΘΩΜΕΝΟ: Προσθήκη Ονομασίας και ΑΦΜ, Αφαίρεση Σειράς
        private DataTable GetClientCard(int clientId, string branchFilter, DateTime dateFrom, DateTime dateTo)
        {
            string sql = @"
                SELECT 
                    ISNULL(c.Company, c.Epwnymia) AS [Ονομασία Πελάτη],
                    c.Afm AS [ΑΦΜ],
                    CONVERT(VARCHAR, si.DocDate, 103) AS [Ημερομηνία],
                    si.DocNumber AS [Αριθμός Παραστατικού],
                    si.GrandTotal AS [Ποσό],
                    CASE WHEN si.IsActive = 1 THEN 'Ενεργό' ELSE 'Ακυρωμένο' END AS [Κατάσταση]
                FROM SalesInvoices si
                INNER JOIN Clients c ON si.ClientId = c.Id
                WHERE si.ClientId = @ClientId
                  AND si.DocDate >= @DateFrom AND si.DocDate <= @DateTo
                ORDER BY si.DocDate, si.DocNumber";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@ClientId", clientId),
                new SqlParameter("@DateFrom", dateFrom),
                new SqlParameter("@DateTo", dateTo));
        }

        // ✅ ΔΙΟΡΘΩΜΕΝΟ: Προσθήκη Ονομασίας και ΑΦΜ, Αφαίρεση Σειράς
        private DataTable GetClientCardByBranch(int clientId, string branchFilter, DateTime dateFrom, DateTime dateTo)
        {
            int? branchId = null;
            if (!string.IsNullOrEmpty(branchFilter) && branchFilter.Contains("[ID:"))
            {
                int startIdx = branchFilter.IndexOf("[ID:") + 4;
                int endIdx = branchFilter.IndexOf("]", startIdx);
                if (startIdx > 3 && endIdx > startIdx && int.TryParse(branchFilter.Substring(startIdx, endIdx - startIdx), out int parsedId))
                {
                    branchId = parsedId;
                }
            }

            string sql = @"
                SELECT 
                    ISNULL(c.Company, c.Epwnymia) AS [Ονομασία Πελάτη],
                    c.Afm AS [ΑΦΜ],
                    cb.BranchName AS [Υποκατάστημα],
                    CONVERT(VARCHAR, si.DocDate, 103) AS [Ημερομηνία],
                    si.DocNumber AS [Αριθμός Παραστατικού],
                    si.GrandTotal AS [Ποσό],
                    CASE WHEN si.IsActive = 1 THEN 'Ενεργό' ELSE 'Ακυρωμένο' END AS [Κατάσταση]
                FROM SalesInvoices si
                INNER JOIN Clients c ON si.ClientId = c.Id
                INNER JOIN ClientBranches cb ON si.BranchId = cb.Id
                WHERE si.ClientId = @ClientId
                  AND (@BranchId IS NULL OR si.BranchId = @BranchId)
                  AND si.DocDate >= @DateFrom AND si.DocDate <= @DateTo
                ORDER BY cb.BranchName, si.DocDate, si.DocNumber";

            return DatabaseHelper.ExecuteQuery(sql,
                new SqlParameter("@ClientId", clientId),
                new SqlParameter("@BranchId", branchId.HasValue ? (object)branchId.Value : DBNull.Value),
                new SqlParameter("@DateFrom", dateFrom),
                new SqlParameter("@DateTo", dateTo));
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
                    MessageBox.Show("✅ Εξήχθη επιτυχώς!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            MessageBox.Show("💡 Συμβουλή: Χρησιμοποίησε το κουμπί 'Προεπισκόπηση' και επίλεξε 'Microsoft Print to PDF' ως εκτυπωτή για να αποθηκεύσεις ως PDF.", "Πληροφόρηση", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (cmbClient.SelectedValue != null) filters += $" | Πελάτης: {cmbClient.Text}";
            if (cmbBranch.SelectedIndex > 0) filters += $" | Υποκατάστημα: {cmbBranch.Text}";

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
            e.Graphics.DrawString($"Συνολικό Ποσό: {_totalAmount:N2} €", _headerFont, Brushes.Black, leftMargin, pageHeight - 40);
            e.Graphics.DrawString($"Ημερομηνία: {DateTime.Now:dd/MM/yyyy HH:mm}", _footerFont, Brushes.Gray, leftMargin, pageHeight - 25);
            e.Graphics.DrawString("KRYPTALIS ERP", _footerFont, Brushes.Gray, pageWidth - 150, pageHeight - 25);

            e.HasMorePages = _currentRow < _reportData.Rows.Count;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}