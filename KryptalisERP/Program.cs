using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient; // Χρησιμοποιούμε το νέο SqlClient για ADO.NET
using KryptalisERP.Forms;

namespace KryptalisERP
{
    internal static class Program
    {
        // ============================================
        // Configuration & DI (Γενικής χρήσης)
        // ============================================
        public static IConfiguration Configuration { get; private set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        // ============================================
        // Στοιχεία Session (Απαραίτητα για τα Forms σου)
        // ============================================
        public static int CurrentUserId { get; set; }
        public static string CurrentUserName { get; set; } = string.Empty;
        public static string CurrentUserRole { get; set; } = string.Empty;

        public static int CurrentCompanyId { get; set; }
        public static string CurrentCompanyName { get; set; } = string.Empty;

        public static int CurrentBranchId { get; set; }
        public static string CurrentBranchName { get; set; } = string.Empty;

        public static bool CanClients { get; set; }
        public static bool CanSales { get; set; }
        public static bool CanPurchases { get; set; }
        public static bool CanWarehouse { get; set; }
        public static bool CanSettings { get; set; }
        public static bool CanUsers { get; set; }
        public static bool CanReports { get; set; }
        public static bool CanFinance { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // 1. Φόρτωση ρυθμίσεων (appsettings.json)
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                Configuration = builder.Build();

                // 2. Ρύθμιση Dependency Injection (Γενική χρήση, όχι EF)
                var services = new ServiceCollection();

                // Εδώ μπορείς να προσθέσεις άλλες υπηρεσίες αν χρειαστεί στο μέλλον
                // π.χ. services.AddSingleton<IMyService, MyService>();

                ServiceProvider = services.BuildServiceProvider();

                // 3. Έλεγχος σύνδεσης με τη βάση (ΜΕ ΑΠΕΥΘΕΙΑΣ SQL / ADO.NET)
                string connectionString = Configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("❌ Δεν βρέθηκε Connection String στο appsettings.json.", "Σφάλμα Ρύθμισης", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        // Αν ανοίξει χωρίς σφάλμα, η βάση είναι διαθέσιμη
                        connection.Close();
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"❌ Αδυναμία σύνδεσης με τη βάση δεδομένων:\n\n{sqlEx.Message}\n\nΕλέγξτε το appsettings.json και την κατάσταση του SQL Server.",
                                    "Σφάλμα Σύνδεσης", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Γενικό σφάλμα:\n{ex.Message}", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4. ΣΩΣΤΗ ΡΟΗ LOGIN -> MAINFORM
                using (var loginForm = new LoginForm())
                {
                    // Αν το login ήταν επιτυχές (η LoginForm επιστρέφει DialogResult.OK)
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        // Τότε ξεκινάει η κύρια εφαρμογή
                        Application.Run(new MainForm());
                    }
                    else
                    {
                        // Ο χρήστης πάτησε Χ ή ακύρωσε → Κλείσιμο εφαρμογής
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Κρίσιμο σφάλμα κατά την εκκίνηση:\n{ex.Message}\n\n{ex.StackTrace}", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}