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
    public partial class SalesReportsForm : Form
    {
        private DataTable _reportData;
        public SalesReportsForm() { InitializeComponent(); }

        private void btnSalesByDate_Click(object sender, EventArgs e)
        {
            try
            {
                _reportData = DatabaseHelper.ExecuteQuery(
                    "SELECT si.DocDate, si.DocNumber, si.Series, c.Company AS Client, si.NetValue, si.VatValue, si.GrossValue FROM SalesInvoices si LEFT JOIN Clients c ON si.ClientId = c.Id WHERE si.DocDate >= @From AND si.DocDate <= @To ORDER BY si.DocDate DESC",
                    new SqlParameter("@From", dtpFrom.Value.Date),
                    new SqlParameter("@To", dtpTo.Value.Date));
                dgvReport.DataSource = _reportData;
                lblTitle.Text = "🧾 Πωλήσεις ανά Ημερομηνία";
                FormatGrid();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSalesByClient_Click(object sender, EventArgs e)
        {
            try
            {
                _reportData = DatabaseHelper.ExecuteQuery(
                    "SELECT c.Company AS Client, c.Afm, COUNT(si.Id) AS InvoiceCount, SUM(si.NetValue) AS TotalNet, SUM(si.VatValue) AS TotalVat, SUM(si.GrossValue) AS TotalGross FROM SalesInvoices si INNER JOIN Clients c ON si.ClientId = c.Id WHERE si.DocDate >= @From AND si.DocDate <= @To GROUP BY c.Company, c.Afm ORDER BY TotalGross DESC",
                    new SqlParameter("@From", dtpFrom.Value.Date),
                    new SqlParameter("@To", dtpTo.Value.Date));
                dgvReport.DataSource = _reportData;
                lblTitle.Text = "👥 Πωλήσεις ανά Πελάτη";
                FormatGrid();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void FormatGrid()
        {
            dgvReport.RowsDefaultCellStyle.BackColor = Color.White;
            dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(212, 175, 55);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0) { MessageBox.Show("Δεν υπάρχουν δεδομένα!", "Προσοχή"); return; }
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV Files|*.csv", FileName = $"Sales_{DateTime.Now:yyyyMMdd}.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataColumn col in _reportData.Columns) sb.Append(col.ColumnName + ",");
                sb.AppendLine();
                foreach (DataRow row in _reportData.Rows) { foreach (var item in row.ItemArray) sb.Append(item.ToString().Replace(",", ";") + ","); sb.AppendLine(); }
                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("✅ CSV exported!", "Επιτυχία");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_reportData == null || _reportData.Rows.Count == 0) return;
            PrintDocument pd = new PrintDocument(); pd.PrintPage += Pd_PrintPage;
            PrintDialog printDialog = new PrintDialog() { Document = pd, AllowPrintToFile = true };
            if (printDialog.ShowDialog() == DialogResult.OK) pd.Print();
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headerFont = new Font("Arial", 10, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 9);
            float y = 50;
            g.DrawString(lblTitle.Text, titleFont, Brushes.Black, 50, y); y += 40;
            float x = 50;
            foreach (DataColumn col in _reportData.Columns) { g.DrawString(col.ColumnName, headerFont, Brushes.Black, x, y); x += 150; }
            y += 25; g.DrawLine(Pens.Black, 50, y, 1000, y); y += 10;
            foreach (DataRow row in _reportData.Rows) { x = 50; foreach (var item in row.ItemArray) { g.DrawString(item.ToString(), bodyFont, Brushes.Black, x, y); x += 150; } y += 20; }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}