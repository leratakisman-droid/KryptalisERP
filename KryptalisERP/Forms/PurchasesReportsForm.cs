using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;

namespace KryptalisERP
{
    public partial class PurchasesReportsForm : Form
    {
        private DataTable _reportData;

        public PurchasesReportsForm()
        {
            InitializeComponent();

            // Προεπιλογή ημερομηνιών (τελευταίος μήνας)
            dtpFrom.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;
        }

        private void btnPurchasesByDate_Click(object sender, EventArgs e)
        {
            try
            {
                _reportData = DatabaseHelper.ExecuteQuery(
                    @"SELECT 
                        CONVERT(VARCHAR, pi.DocDate, 103) AS [Ημερομηνία], 
                        pi.DocNumber AS [Αριθμός], 
                        ps.SeriesName AS [Σειρά],
                        ISNULL(s.Company, ISNULL(s.Epwnymia, '')) AS [Προμηθευτής], 
                        pi.TotalBeforeDiscount AS [Καθαρή], 
                        pi.TotalVat AS [ΦΠΑ], 
                        pi.GrandTotal AS [Σύνολο] 
                      FROM PurchaseInvoices pi 
                      LEFT JOIN PurchaseSeries ps ON ps.Id = pi.SeriesId
                      LEFT JOIN Suppliers s ON pi.SupplierId = s.Id 
                      WHERE pi.DocDate >= @From AND pi.DocDate <= @To AND pi.IsActive = 1
                      ORDER BY pi.DocDate DESC",
                    new SqlParameter("@From", dtpFrom.Value.Date),
                    new SqlParameter("@To", dtpTo.Value.Date));

                dgvReport.DataSource = _reportData;
                lblTitle.Text = "🛒 Αγορές ανά Ημερομηνία";
                FormatGrid();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnPurchasesBySupplier_Click(object sender, EventArgs e)
        {
            try
            {
                _reportData = DatabaseHelper.ExecuteQuery(
                    @"SELECT 
                        ISNULL(s.Company, ISNULL(s.Epwnymia, '')) AS [Προμηθευτής], 
                        ISNULL(s.Afm, '') AS [ΑΦΜ], 
                        COUNT(pi.Id) AS [Πλήθος], 
                        SUM(ISNULL(pi.TotalBeforeDiscount, 0)) AS [Καθαρή], 
                        SUM(ISNULL(pi.TotalVat, 0)) AS [ΦΠΑ], 
                        SUM(ISNULL(pi.GrandTotal, 0)) AS [Σύνολο] 
                      FROM PurchaseInvoices pi 
                      INNER JOIN Suppliers s ON pi.SupplierId = s.Id 
                      WHERE pi.DocDate >= @From AND pi.DocDate <= @To AND pi.IsActive = 1
                      GROUP BY ISNULL(s.Company, ISNULL(s.Epwnymia, '')), ISNULL(s.Afm, '') 
                      ORDER BY [Σύνολο] DESC",
                    new SqlParameter("@From", dtpFrom.Value.Date),
                    new SqlParameter("@To", dtpTo.Value.Date));

                dgvReport.DataSource = _reportData;
                lblTitle.Text = "🏭 Αγορές ανά Προμηθευτή";
                FormatGrid();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void FormatGrid()
        {
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.RowsDefaultCellStyle.BackColor = Color.White;
            dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(212, 175, 55);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            dgvReport.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Μορφοποίηση αριθμητικών στηλών
            foreach (DataGridViewColumn col in dgvReport.Columns)
            {
                if (col.HeaderText == "Καθαρή" || col.HeaderText == "ΦΠΑ" || col.HeaderText == "Σύνολο")
                {
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (col.HeaderText == "Πλήθος")
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0) { MessageBox.Show("Δεν υπάρχουν δεδομένα!", "Προσοχή"); return; }
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV Files|*.csv", FileName = $"Purchases_{DateTime.Now:yyyyMMdd}.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();

                // Επικεφαλίδες
                foreach (DataColumn col in _reportData.Columns)
                    sb.Append($"\"{col.ColumnName}\",");
                sb.AppendLine();

                // Δεδομένα
                foreach (DataRow row in _reportData.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        string val = item?.ToString()?.Replace("\"", "\"\"") ?? "";
                        sb.Append($"\"{val}\",");
                    }
                    sb.AppendLine();
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("✅ Η εξαγωγή CSV ολοκληρώθηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0)
            {
                MessageBox.Show("Δεν υπάρχουν δεδομένα για εκτύπωση!", "Προσοχή");
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPage;
            pd.DefaultPageSettings.Landscape = true; // Οριζόντια εκτύπωση για καλύτερη προβολή

            PrintDialog printDialog = new PrintDialog() { Document = pd, AllowPrintToFile = true };
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pd.Print();
                    MessageBox.Show("✅ Η εκτύπωση στάλθηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Σφάλμα εκτύπωσης: " + ex.Message, "Σφάλμα");
                }
            }
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headerFont = new Font("Arial", 10, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 9);
            Font dateFont = new Font("Arial", 9, FontStyle.Italic);

            float y = 50;

            // Τίτλος
            g.DrawString(lblTitle.Text, titleFont, Brushes.Black, 50, y);
            y += 30;

            // Περίοδος
            g.DrawString($"Περίοδος: {dtpFrom.Value:dd/MM/yyyy} έως {dtpTo.Value:dd/MM/yyyy}", dateFont, Brushes.Gray, 50, y);
            y += 30;

            float x = 50;
            float colWidth = 150;

            // Επικεφαλίδες
            foreach (DataColumn col in _reportData.Columns)
            {
                g.DrawString(col.ColumnName, headerFont, Brushes.Black, x, y);
                x += colWidth;
            }
            y += 25;
            g.DrawLine(Pens.Black, 50, y, 1000, y);
            y += 10;

            // Δεδομένα (μέγιστο 35 γραμμές ανά σελίδα για αυτό το απλό παράδειγμα)
            int maxRows = 35;
            int currentRow = 0;

            foreach (DataRow row in _reportData.Rows)
            {
                if (currentRow >= maxRows) break;

                x = 50;
                foreach (var item in row.ItemArray)
                {
                    string text = item?.ToString() ?? "";
                    if (text.Length > 20) text = text.Substring(0, 17) + "..."; // Περιορισμός μήκους

                    g.DrawString(text, bodyFont, Brushes.Black, x, y);
                    x += colWidth;
                }
                y += 20;
                currentRow++;
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}