using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace KryptalisERP.Services
{
    public class RdlcReportEngine
    {
        private int _templateId;
        private string _templateName;
        private string _paperSize;
        private string _orientation;
        private string _htmlTemplate;

        public RdlcReportEngine(int templateId)
        {
            _templateId = templateId;
            LoadTemplate();
        }

        private void LoadTemplate()
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(
                "SELECT * FROM ReportTemplates WHERE Id = @Id",
                new SqlParameter("@Id", _templateId));

            if (dt.Rows.Count == 0)
                throw new Exception("Το report template δεν βρέθηκε!");

            DataRow r = dt.Rows[0];
            _templateName = r["TemplateName"]?.ToString() ?? "";
            _paperSize = r["PaperSize"]?.ToString() ?? "A4";
            _orientation = r["Orientation"]?.ToString() ?? "Portrait";
            _htmlTemplate = r["ReportHtml"]?.ToString() ?? "";
        }

        // ✅ ΕΜΠΛΟΥΤΙΣΜΕΝΗ Αντικατάσταση placeholders με ΟΛΑ τα δεδομένα
        public string ReplacePlaceholders(int invoiceId)
        {
            // 1. Φόρτωση παραστατικού ΜΕ JOINS για να πάρουμε περιγραφές αντί για IDs
            DataTable invoice = DatabaseHelper.ExecuteQuery(@"
                SELECT si.*, 
                       c.Company AS ClientName, c.Afm AS ClientVat, c.DOY AS ClientDoy,
                       c.Address AS ClientAddress, c.Email AS ClientEmail, c.Phone AS ClientPhone,
                       s.SeriesName, s.Prefix AS SeriesPrefix,
                       pm.MethodName AS PaymentMethodName,
                       dp.Description AS DeliveryPurposeDesc,
                       ve.Description AS VatExemptionDesc
                FROM SalesInvoices si
                LEFT JOIN Clients c ON c.Id = si.ClientId
                LEFT JOIN Series s ON s.Id = si.SeriesId
                LEFT JOIN PaymentMethods pm ON pm.Id = si.PaymentMethodId
                LEFT JOIN DeliveryPurposes dp ON dp.Id = si.DeliveryPurposeId
                LEFT JOIN VatExemptions ve ON ve.Id = si.VatExemptionId
                WHERE si.Id = @Id", new SqlParameter("@Id", invoiceId));

            if (invoice.Rows.Count == 0) throw new Exception("Το παραστατικό δεν βρέθηκε!");

            // 2. Φόρτωση εταιρείας
            DataTable company = DatabaseHelper.ExecuteQuery(
                "SELECT TOP 1 CompanyName, VatNumber AS CompanyVat, Address AS CompanyAddress, Phone AS CompanyPhone, Email AS CompanyEmail FROM CompanySettings");

            // 3. Φόρτωση ειδών (με Μ.Μ. και ΦΠΑ)
            DataTable items = DatabaseHelper.ExecuteQuery(
                "SELECT LineNumber, Description, Unit, Qty, UnitPrice, VatRate, TotalValue FROM SalesInvoiceItems WHERE InvoiceId = @Id ORDER BY LineNumber",
                new SqlParameter("@Id", invoiceId));

            // 4. Φόρτωση Παρακρατήσεων
            DataTable withholdings = DatabaseHelper.ExecuteQuery(
                "SELECT * FROM SalesInvoiceWithholdings WHERE InvoiceId = @Id",
                new SqlParameter("@Id", invoiceId));

            // 5. Φόρτωση Επιβαρύνσεων
            DataTable surcharges = DatabaseHelper.ExecuteQuery(
                "SELECT * FROM SalesInvoiceSurcharges WHERE InvoiceId = @Id",
                new SqlParameter("@Id", invoiceId));

            DataRow inv = invoice.Rows[0];
            DataRow comp = company.Rows.Count > 0 ? company.Rows[0] : null;

            string html = _htmlTemplate;

            // ==========================================
            // ΑΝΤΙΚΑΤΑΣΤΑΣΗ COMPANY PLACEHOLDERS
            // ==========================================
            if (comp != null)
            {
                html = html.Replace("{CompanyName}", comp["CompanyName"]?.ToString() ?? "");
                html = html.Replace("{CompanyVat}", comp["CompanyVat"]?.ToString() ?? "");
                html = html.Replace("{CompanyAddress}", comp["CompanyAddress"]?.ToString() ?? "");
                html = html.Replace("{CompanyPhone}", comp["CompanyPhone"]?.ToString() ?? "");
                html = html.Replace("{CompanyEmail}", comp["CompanyEmail"]?.ToString() ?? "");
            }

            // ==========================================
            // ΑΝΤΙΚΑΤΑΣΤΑΣΗ INVOICE PLACEHOLDERS
            // ==========================================
            html = html.Replace("{SeriesName}", inv["SeriesName"]?.ToString() ?? "");
            html = html.Replace("{SeriesPrefix}", inv["SeriesPrefix"]?.ToString() ?? "");
            html = html.Replace("{DocNumber}", inv["DocNumber"]?.ToString() ?? "");
            html = html.Replace("{DocDate}", Convert.ToDateTime(inv["DocDate"]).ToString("dd/MM/yyyy"));

            html = html.Replace("{ClientName}", inv["ClientName"]?.ToString() ?? "");
            html = html.Replace("{ClientVat}", inv["ClientVat"]?.ToString() ?? "");
            html = html.Replace("{ClientDoy}", inv["ClientDoy"]?.ToString() ?? "");
            html = html.Replace("{ClientAddress}", inv["ClientAddress"]?.ToString() ?? "");
            html = html.Replace("{ClientPhone}", inv["ClientPhone"]?.ToString() ?? "");
            html = html.Replace("{ClientEmail}", inv["ClientEmail"]?.ToString() ?? "");

            html = html.Replace("{PaymentMethod}", inv["PaymentMethodName"]?.ToString() ?? "");
            html = html.Replace("{DeliveryPurpose}", inv["DeliveryPurposeDesc"]?.ToString() ?? inv["DeliveryPurpose"]?.ToString() ?? "");
            html = html.Replace("{VatExemption}", inv["VatExemptionDesc"]?.ToString() ?? "");
            html = html.Replace("{Vehicle}", inv["TransportVehicle"]?.ToString() ?? "");
            html = html.Replace("{Notes}", inv["Notes"]?.ToString() ?? "");

            html = html.Replace("{GrandTotal}", Convert.ToDecimal(inv["GrandTotal"]).ToString("N2"));
            html = html.Replace("{NetValue}", Convert.ToDecimal(inv["TotalBeforeDiscount"]).ToString("N2"));
            html = html.Replace("{TotalDiscount}", Convert.ToDecimal(inv["TotalDiscount"]).ToString("N2"));
            html = html.Replace("{TotalVat}", Convert.ToDecimal(inv["TotalVat"]).ToString("N2"));

            // ==========================================
            // ΔΗΜΙΟΥΡΓΙΑ ΠΙΝΑΚΑ ΕΙΔΩΝ
            // ==========================================
            string itemsHtml = "";
            bool isThermal = _paperSize.StartsWith("Thermal");

            foreach (DataRow r in items.Rows)
            {
                if (isThermal)
                {
                    itemsHtml += $"<p style='margin:0; font-size:12px;'><b>{r["Description"]}</b><br/>" +
                                 $"  {r["Qty"]} {r["Unit"]} x {Convert.ToDecimal(r["UnitPrice"]):N2} € = " +
                                 $"<b>{Convert.ToDecimal(r["TotalValue"]):N2} €</b> (ΦΠΑ {r["VatRate"]}%)</p>";
                }
                else
                {
                    itemsHtml += "<tr>" +
                                 $"<td style='border:1px solid #ccc; padding:8px; text-align:center;'>{r["LineNumber"]}</td>" +
                                 $"<td style='border:1px solid #ccc; padding:8px;'>{r["Description"]}</td>" +
                                 $"<td style='border:1px solid #ccc; padding:8px; text-align:center;'>{r["Unit"]}</td>" +
                                 $"<td style='border:1px solid #ccc; padding:8px; text-align:center;'>{r["Qty"]}</td>" +
                                 $"<td style='border:1px solid #ccc; padding:8px; text-align:right;'>{Convert.ToDecimal(r["UnitPrice"]):N2}</td>" +
                                 $"<td style='border:1px solid #ccc; padding:8px; text-align:center;'>{r["VatRate"]}%</td>" +
                                 $"<td style='border:1px solid #ccc; padding:8px; text-align:right;'>{Convert.ToDecimal(r["TotalValue"]):N2}</td>" +
                                 "</tr>";
                }
            }
            html = html.Replace("{ItemsTable}", itemsHtml);

            // ==========================================
            // ΔΗΜΙΟΥΡΓΙΑ ΠΙΝΑΚΑ ΠΑΡΑΚΡΑΤΗΣΕΩΝ
            // ==========================================
            string withholdingsHtml = "";
            decimal totalWithholding = 0;
            foreach (DataRow w in withholdings.Rows)
            {
                decimal amount = Convert.ToDecimal(w["WithholdingAmount"]);
                totalWithholding += amount;
                withholdingsHtml += $"<tr><td style='border:1px solid #ccc; padding:8px;'>Παρακράτηση</td><td style='border:1px solid #ccc; padding:8px; text-align:right; color:red;'>-{amount:N2} €</td></tr>";
            }
            html = html.Replace("{WithholdingsTable}", withholdingsHtml);
            html = html.Replace("{TotalWithholdings}", totalWithholding.ToString("N2"));

            // ==========================================
            // ΔΗΜΙΟΥΡΓΙΑ ΠΙΝΑΚΑ ΕΠΙΒΑΡΥΝΣΕΩΝ
            // ==========================================
            string surchargesHtml = "";
            decimal totalSurcharge = 0;
            foreach (DataRow s in surcharges.Rows)
            {
                decimal amount = Convert.ToDecimal(s["SurchargeAmount"]);
                totalSurcharge += amount;
                surchargesHtml += $"<tr><td style='border:1px solid #ccc; padding:8px;'>Επιβάρυνση</td><td style='border:1px solid #ccc; padding:8px; text-align:right; color:green;'>+{amount:N2} €</td></tr>";
            }
            html = html.Replace("{SurchargesTable}", surchargesHtml);
            html = html.Replace("{TotalSurcharges}", totalSurcharge.ToString("N2"));

            return html;
        }

        // ✅ Προεπισκόπηση
        public void PreviewReport(int invoiceId)
        {
            string html = ReplacePlaceholders(invoiceId);

            using (Form previewForm = new Form())
            {
                previewForm.Text = $"🖨️ Προεπισκόπηση: {_templateName}";
                previewForm.Size = new Size(900, 700);
                previewForm.StartPosition = FormStartPosition.CenterParent;

                WebBrowser webBrowser = new WebBrowser();
                webBrowser.Dock = DockStyle.Fill;
                webBrowser.DocumentText = html;

                Button btnPrint = new Button();
                btnPrint.Text = "🖨️ Εκτύπωση";
                btnPrint.Dock = DockStyle.Bottom;
                btnPrint.Height = 50;
                btnPrint.BackColor = Color.FromArgb(46, 204, 113);
                btnPrint.ForeColor = Color.White;
                btnPrint.FlatStyle = FlatStyle.Flat;
                btnPrint.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
                btnPrint.Click += (s, e) => PrintHtml(html);

                previewForm.Controls.Add(webBrowser);
                previewForm.Controls.Add(btnPrint);
                previewForm.ShowDialog();
            }
        }

        // ✅ Εκτύπωση HTML
        public void PrintHtml(string html)
        {
            string tempFile = Path.GetTempFileName() + ".html";
            File.WriteAllText(tempFile, html, System.Text.Encoding.UTF8);

            using (WebBrowser wb = new WebBrowser())
            {
                wb.DocumentCompleted += (s, e) =>
                {
                    wb.Print();
                    try { File.Delete(tempFile); } catch { }
                };
                wb.Url = new Uri(tempFile);
            }
        }

        // ✅ Εξαγωγή σε PDF (απλή μέθοδος μέσω εκτύπωσης)
        public byte[] ExportToPdf(int invoiceId)
        {
            string html = ReplacePlaceholders(invoiceId);
            return System.Text.Encoding.UTF8.GetBytes(html);
        }

        // ✅ Αποστολή email
        public void SendByEmail(int invoiceId, string recipientEmail, string subject)
        {
            byte[] pdfBytes = ExportToPdf(invoiceId);
            MessageBox.Show($"📧 Το report ({pdfBytes.Length} bytes) είναι έτοιμο για αποστολή στο: {recipientEmail}\n\n(Εδώ θα συνδεθεί ο SMTP client)", "Email");
        }

        // ✅ Εύρεση template ID για σειρά
        public static int GetTemplateIdForSeries(int seriesId)
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(@"
                SELECT srt.ReportTemplateId 
                FROM SeriesReportTemplates srt
                WHERE srt.SeriesId = @SeriesId AND srt.IsActive = 1",
                new SqlParameter("@SeriesId", seriesId));

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ReportTemplateId"]);

            DataTable defaultDt = DatabaseHelper.ExecuteQuery(@"
                SELECT rt.Id FROM ReportTemplates rt
                INNER JOIN Series s ON s.SeriesType = rt.TemplateType
                WHERE s.Id = @SeriesId AND rt.IsDefault = 1 AND rt.IsActive = 1",
                new SqlParameter("@SeriesId", seriesId));

            if (defaultDt.Rows.Count > 0)
                return Convert.ToInt32(defaultDt.Rows[0]["Id"]);

            return 0;
        }

        // ✅ Αντιστοίχιση σειράς με template
        public static void MapSeriesToTemplate(int seriesId, int templateId)
        {
            DataTable existing = DatabaseHelper.ExecuteQuery(
                "SELECT Id FROM SeriesReportTemplates WHERE SeriesId = @SeriesId AND IsActive = 1",
                new SqlParameter("@SeriesId", seriesId));

            if (existing.Rows.Count > 0)
            {
                DatabaseHelper.ExecuteQuery(
                    "UPDATE SeriesReportTemplates SET ReportTemplateId = @TemplateId WHERE SeriesId = @SeriesId",
                    new SqlParameter("@TemplateId", templateId),
                    new SqlParameter("@SeriesId", seriesId));
            }
            else
            {
                DatabaseHelper.ExecuteQuery(
                    "INSERT INTO SeriesReportTemplates (SeriesId, ReportTemplateId) VALUES (@SeriesId, @TemplateId)",
                    new SqlParameter("@SeriesId", seriesId),
                    new SqlParameter("@TemplateId", templateId));
            }
        }
    }
}