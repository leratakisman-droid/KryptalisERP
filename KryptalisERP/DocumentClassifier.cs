using System;
using System.Data;
using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;

namespace KryptalisERP
{
    /// <summary>
    /// Βοηθητική κλάση για την κατηγοριοποίηση παραστατικών βάσει myDATA λογικής.
    /// </summary>
    public static class DocumentClassifier
    {
        // Κρυφή μνήμη (Cache) για αποφυγή επαναλαμβανόμενων queries στη βάση
        private static DataTable _docTypesCache = null;

        /// <summary>
        /// Ελέγχει αν ο τύπος παραστατικού ανήκει στις Πωλήσεις
        /// </summary>
        public static bool IsSales(int docTypeId)
        {
            var dt = GetDocType(docTypeId);
            return dt != null && dt["Category"]?.ToString() == "SALES";
        }

        /// <summary>
        /// Ελέγχει αν ο τύπος παραστατικού ανήκει στις Αγορές
        /// </summary>
        public static bool IsPurchase(int docTypeId)
        {
            var dt = GetDocType(docTypeId);
            return dt != null && dt["Category"]?.ToString() == "PURCHASE";
        }

        /// <summary>
        /// Ελέγχει αν ο τύπος παραστατικού απαιτεί παρακράτηση φόρου
        /// </summary>
        public static bool RequiresWithholding(int docTypeId)
        {
            var dt = GetDocType(docTypeId);
            if (dt == null) return false;

            // Εδώ μπορείς να προσθέσεις συγκεκριμένη λογική ή να ελέγξεις μια στήλη στη βάση
            // Προς το παρόν επιστρέφουμε true για συγκεκριμένους κωδικούς myDATA ως παράδειγμα
            string code = dt["MyDataCode"]?.ToString();
            return code == "2.2" || code == "5.2" || code == "12.1";
        }

        /// <summary>
        /// Επιστρέφει τον κωδικό κατηγορίας myDATA (π.χ. "2" για Sales, "5" για Purchase)
        /// </summary>
        public static string GetMyDataCategoryCode(int docTypeId)
        {
            var dt = GetDocType(docTypeId);
            string code = dt?["MyDataCode"]?.ToString();

            if (string.IsNullOrEmpty(code)) return "0";
            return code.Substring(0, 1);
        }

        /// <summary>
        /// Φορτώνει τα δεδομένα των τύπων παραστατικών από τη βάση (με caching)
        /// </summary>
        private static DataRow GetDocType(int id)
        {
            try
            {
                // Φόρτωση cache αν είναι άδειο
                if (_docTypesCache == null || _docTypesCache.Rows.Count == 0)
                {
                    _docTypesCache = DatabaseHelper.ExecuteQuery(
                        "SELECT Id, DocTypeCode AS MyDataCode, DocTypeCategory AS Category, RequiresWithholding FROM DocumentTypes WHERE IsActive = 1");
                }

                // Αναζήτηση στη cache
                DataRow[] rows = _docTypesCache.Select($"Id = {id}");
                return rows.Length > 0 ? rows[0] : null;
            }
            catch
            {
                // Σε περίπτωση σφάλματος DB, επιστρέφουμε null ασφαλείας
                return null;
            }
        }
    }
}