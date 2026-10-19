using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace KryptalisERP.Helpers
{
    public static class PdfPrintHelper
    {
        /// <summary>
        /// Στέλνει το PDF προς εκτύπωση στον default printer
        /// TODO: Μελλοντική υλοποίηση με RawPrint ή Spire.PDF για άμεση εκτύπωση χωρίς viewer
        /// </summary>
        public static void Print(byte[] pdfBytes, PaperKind paperKind, int widthMm = 0)
        {
            try
            {
                // Προσωρινή υλοποίηση: Αποθήκευση σε temp και άνοιγμα με print dialog
                string tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"Print_{Guid.NewGuid().ToString().Substring(0, 8)}.pdf");
                System.IO.File.WriteAllBytes(tempFile, pdfBytes);

                // Άνοιγμα του PDF με την εντολή print
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = tempFile,
                    Verb = "print",
                    CreateNoWindow = true,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                };

                System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);
                if (proc != null)
                {
                    proc.WaitForExit(10000); // Περιμένουμε έως 10 δευτ.
                    proc.Close();
                }

                // Διαγραφή temp file μετά την εκτύπωση
                if (System.IO.File.Exists(tempFile))
                    System.IO.File.Delete(tempFile);

                MessageBox.Show($"✅ Απεστάλη προς εκτύπωση ({paperKind})", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα εκτύπωσης: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}