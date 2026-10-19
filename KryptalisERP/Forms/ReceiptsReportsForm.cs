using KryptalisERP.Helpers;
using KryptalisERP.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ReceiptsReportsForm : Form
    {
        // ✅ Προσθήκη lblStats που λείπει από τον Designer
        private System.Windows.Forms.Label lblStats;

        public ReceiptsReportsForm()
        {
            InitializeComponent();
            ThemeManager.ApplyToCurrentForm(this);

            // ✅ Δημιουργία lblStats runtime αφού δεν υπάρχει στον Designer
            lblStats = new Label();
            lblStats.AutoSize = true;
            lblStats.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStats.ForeColor = Color.FromArgb(44, 62, 80);
            lblStats.Location = new Point(20, 100); // Κάτω από τα κουμπιά
            this.Controls.Add(lblStats);
            lblStats.BringToFront();

            // Προεπιλογή ημερομηνιών
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;

            // ✅ Τα events συνδέονται ΑΥΤΟΜΑΤΑ από τον Designer
            // (btnByDate.Click -> btnReceiptsByDate_Click, κλπ.)
            // Δεν χρειάζεται χειροκίνητη σύνδεση εδώ!
        }

        // ==========================================
        // ✅ ΜΕΘΟΔΟΙ ΠΟΥ ΖΗΤΑΕΙ Ο DESIGNER
        // ==========================================

        // ✅ "Ανά Ημερομηνία" - Ταιριάζει με το btnByDate.Click του Designer
        private void btnReceiptsByDate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"
                    SELECT 
                        CONVERT(VARCHAR, r.DocDate, 103) AS [Ημερομηνία],
                        r.DocNumber AS [Αριθμός],
                        CASE WHEN r.Type='Collection' THEN 'Είσπραξη' ELSE 'Πληρωμή' END AS [Τύπος],
                        ISNULL(c.CompanyName, s.CompanyName) AS [Πελάτης/Προμηθευτής],
                        r.Amount AS [Ποσό],
                        pm.MethodName AS [Τρόπος]
                    FROM Receipts r
                    LEFT JOIN Clients c ON r.ClientId = c.Id
                    LEFT JOIN Suppliers s ON r.SupplierId = s.Id
                    LEFT JOIN PaymentMethods pm ON r.PaymentMethodId = pm.Id
                    WHERE r.IsActive = 1
                      AND r.DocDate BETWEEN @From AND @To
                      AND r.CompanyId = @CompId
                      AND r.BranchId = @BranchId
                    ORDER BY r.DocDate DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query,
                    new SqlParameter("@From", dtpFrom.Value.Date),
                    new SqlParameter("@To", dtpTo.Value.Date),
                    new SqlParameter("@CompId", Program.CurrentCompanyId),
                    new SqlParameter("@BranchId", Program.CurrentBranchId));

                dgvReport.DataSource = dt;
                lblStats.Text = $"Σύνολο εγγραφών: {dt.Rows.Count} | Ποσό: {ComputeTotal(dt):N2} €";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        // ✅ "Ανά Μέρος" - Ταιριάζει με το btnByParty.Click του Designer
        private void btnByParty_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"
                    SELECT 
                        CASE WHEN r.ClientId IS NOT NULL THEN 'Πελάτης' ELSE 'Προμηθευτής' END AS [Τύπος Μέρους],
                        ISNULL(c.CompanyName, s.CompanyName) AS [Όνομα],
                        ISNULL(c.AFM, s.AFM) AS [ΑΦΜ],
                        COUNT(*) AS [Πλήθος Κινήσεων],
                        SUM(CASE WHEN r.Type='Collection' THEN r.Amount ELSE -r.Amount END) AS [Υπόλοιπο]
                    FROM Receipts r
                    LEFT JOIN Clients c ON r.ClientId = c.Id
                    LEFT JOIN Suppliers s ON r.SupplierId = s.Id
                    WHERE r.IsActive = 1
                      AND r.DocDate BETWEEN @From AND @To
                      AND r.CompanyId = @CompId
                      AND r.BranchId = @BranchId
                    GROUP BY r.ClientId, r.SupplierId, c.CompanyName, s.CompanyName, c.AFM, s.AFM
                    ORDER BY [Τύπος Μέρους], [Όνομα]";

                DataTable dt = DatabaseHelper.ExecuteQuery(query,
                    new SqlParameter("@From", dtpFrom.Value.Date),
                    new SqlParameter("@To", dtpTo.Value.Date),
                    new SqlParameter("@CompId", Program.CurrentCompanyId),
                    new SqlParameter("@BranchId", Program.CurrentBranchId));

                dgvReport.DataSource = dt;
                lblStats.Text = $"Μέρη: {dt.Rows.Count}";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        // ✅ CSV Export - Ταιριάζει με το btnExportCsv.Click του Designer
        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null || dgvReport.Rows.Count == 0) return;

            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV|*.csv", FileName = "Report.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dgvReport.Columns.Count; i++)
                    sb.Append($"\"{dgvReport.Columns[i].HeaderText}\";");
                sb.AppendLine();

                foreach (DataGridViewRow row in dgvReport.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                        sb.Append($"\"{cell.Value?.ToString()}\";");
                    sb.AppendLine();
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("✅ Εξήχθη επιτυχώς!");
            }
        }

        // ✅ Print - Ταιριάζει με το btnPrint.Click του Designer
        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Χρησιμοποίησε το 'Print to PDF' των Windows.", "Πληροφόρηση");
        }

        // ✅ Close - Ταιριάζει με το btnClose.Click του Designer
        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        // ==========================================
        // ✅ ΒΟΗΘΗΤΙΚΗ ΜΕΘΟΔΟΣ
        // ==========================================
        private decimal ComputeTotal(DataTable dt)
        {
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
                if (decimal.TryParse(row["Ποσό"]?.ToString(), out decimal val)) total += val;
            return total;
        }
    }
}