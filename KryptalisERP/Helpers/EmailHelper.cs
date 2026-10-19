using System;
using System.Data;
using System.Net.Mail;
using System.Windows.Forms;

namespace KryptalisERP.Helpers
{
    public static class EmailHelper
    {
        /// <summary>
        /// Αποστέλλει email με συνημμένο αρχείο
        /// TODO: Μελλοντικά αντικατάσταση με MailKit για καλύτερη υποστήριξη
        /// </summary>
        public static void SendEmailWithAttachment(string to, string subject, string body, byte[] attachmentBytes, string attachmentName)
        {
            try
            {
                // Ανάκτηση SMTP ρυθμίσεων από τη βάση
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT TOP 1 * FROM EmailSettings");
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("❌ Δεν έχουν ρυθμιστεί οι ρυθμίσεις Email!", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow r = dt.Rows[0];
                string smtpServer = r["SmtpServer"]?.ToString() ?? "";
                int smtpPort = Convert.ToInt32(r["SmtpPort"]);
                string username = r["Username"]?.ToString() ?? "";
                string password = r["Password"]?.ToString() ?? "";
                bool useSSL = Convert.ToBoolean(r["UseSSL"]);
                string fromEmail = r["FromEmail"]?.ToString() ?? "";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail);
                    mail.To.Add(to);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = false;

                    // Προσθήκη συνημμένου
                    if (attachmentBytes != null && attachmentBytes.Length > 0)
                    {
                        mail.Attachments.Add(new Attachment(new System.IO.MemoryStream(attachmentBytes), attachmentName));
                    }

                    using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(username, password);
                        smtp.EnableSsl = useSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα αποστολής email: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}