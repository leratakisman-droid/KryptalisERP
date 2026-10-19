using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP.Helpers
{
    public static class DashboardChartHelper
    {
        // ==========================================
        // 1. ΠΩΛΗΣΕΙΣ ΑΝΑ ΜΗΝΑ (Τρέχουσα vs Προηγούμενη Χρονιά)
        // ==========================================
        public static (List<string> labels, List<double> current, List<double> previous) GetMonthlySales()
        {
            var labels = new List<string>();
            var current = new List<double>();
            var previous = new List<double>();

            try
            {
                string query = @"
                    SELECT 
                        MONTH(DocDate) AS MonthNum,
                        SUM(GrandTotal) AS TotalSales,
                        YEAR(DocDate) AS YearNum
                    FROM SalesInvoices
                    WHERE IsActive = 1 
                      AND DocDate >= DATEADD(YEAR, -1, GETDATE())
                    GROUP BY MONTH(DocDate), YEAR(DocDate)
                    ORDER BY YearNum, MonthNum";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                var monthNames = new[] { "Ιαν", "Φεβ", "Μαρ", "Απρ", "Μαι", "Ιουν", "Ιουλ", "Αυγ", "Σεπ", "Οκτ", "Νοε", "Δεκ" };

                int currentYear = DateTime.Now.Year;
                int previousYear = currentYear - 1;

                for (int i = 0; i < 12; i++)
                {
                    labels.Add(monthNames[i]);

                    var currentRow = dt.Select($"MonthNum = {i + 1} AND YearNum = {currentYear}");
                    current.Add(currentRow.Length > 0 && !currentRow[0].IsNull("TotalSales") ? Convert.ToDouble(currentRow[0]["TotalSales"]) : 0);

                    var previousRow = dt.Select($"MonthNum = {i + 1} AND YearNum = {previousYear}");
                    previous.Add(previousRow.Length > 0 && !previousRow[0].IsNull("TotalSales") ? Convert.ToDouble(previousRow[0]["TotalSales"]) : 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα GetMonthlySales: " + ex.Message);
                var monthNames = new[] { "Ιαν", "Φεβ", "Μαρ", "Απρ", "Μαι", "Ιουν", "Ιουλ", "Αυγ", "Σεπ", "Οκτ", "Νοε", "Δεκ" };
                labels.AddRange(monthNames);
                current.AddRange(new double[12]);
                previous.AddRange(new double[12]);
            }

            return (labels, current, previous);
        }

        // ==========================================
        // 2. ΚΑΤΑΝΟΜΗ ΠΩΛΗΣΕΩΝ ΑΝΑ ΤΡΟΠΟ ΠΛΗΡΩΜΗΣ
        // ==========================================
        public static (List<string> names, List<double> values) GetPaymentMethodDistribution()
        {
            var names = new List<string>();
            var values = new List<double>();

            try
            {
                string query = @"
                    SELECT 
                        ISNULL(pm.MethodName, 'Άγνωστο') AS MethodName,
                        SUM(si.GrandTotal) AS TotalAmount
                    FROM SalesInvoices si
                    LEFT JOIN PaymentMethods pm ON si.PaymentMethodId = pm.Id
                    WHERE si.IsActive = 1 
                      AND si.DocDate >= DATEADD(MONTH, -6, GETDATE())
                    GROUP BY pm.MethodName
                    ORDER BY TotalAmount DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    names.Add(row["MethodName"].ToString());
                    values.Add(!row.IsNull("TotalAmount") ? Convert.ToDouble(row["TotalAmount"]) : 0);
                }

                if (names.Count == 0)
                {
                    names.AddRange(new[] { "Μετρητά", "Κάρτα", "Επιταγή" });
                    values.AddRange(new double[] { 1000, 500, 300 });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα GetPaymentMethodDistribution: " + ex.Message);
                names.AddRange(new[] { "Μετρητά", "Κάρτα" });
                values.AddRange(new double[] { 1000, 500 });
            }

            return (names, values);
        }

        // ==========================================
        // 3. TOP 10 ΠΕΛΑΤΕΣ (με φιλτράρισμα εταιρείας)
        // ==========================================
        public static (List<string> names, List<double> totals) GetTop10Clients(int? companyId = null)
        {
            var names = new List<string>();
            var totals = new List<double>();

            try
            {
                // Ασφαλής προσθήκη του CompanyId στο WHERE clause (είναι int, άρα no SQL injection)
                string whereClause = "si.IsActive = 1 AND si.DocDate >= DATEADD(YEAR, -1, GETDATE())";
                if (companyId.HasValue)
                {
                    whereClause += $" AND si.CompanyId = {companyId.Value}";
                }

                string query = $@"
                    SELECT TOP 10 
                        ISNULL(c.CompanyName, ISNULL(c.Epwnymia, 'Άγνωστος')) AS ClientName,
                        SUM(si.GrandTotal) AS TotalSales
                    FROM SalesInvoices si
                    INNER JOIN Customers c ON si.ClientId = c.Id
                    WHERE {whereClause}
                    GROUP BY c.CompanyName, c.Epwnymia
                    ORDER BY TotalSales DESC";

                // Καλούμε το DatabaseHelper χωρίς παραμέτρους, αφού το companyId είναι ήδη μέσα στο query
                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    names.Add(row["ClientName"].ToString());
                    totals.Add(!row.IsNull("TotalSales") ? Convert.ToDouble(row["TotalSales"]) : 0);
                }

                if (names.Count == 0)
                {
                    names.Add("Δεν υπάρχουν δεδομένα");
                    totals.Add(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα GetTop10Clients: " + ex.Message);
                names.Add("Σφάλμα");
                totals.Add(0);
            }

            return (names, totals);
        }

        // ==========================================
        // 4. TOP 10 ΠΡΟΪΟΝΤΑ
        // ==========================================
        public static (List<string> names, List<double> quantities) GetTop10Products()
        {
            var names = new List<string>();
            var quantities = new List<double>();

            try
            {
                string query = @"
                    SELECT TOP 10 
                        ISNULL(ii.Description, 'Άγνωστο') AS ProductName,
                        SUM(ii.Qty) AS TotalQty
                    FROM SalesInvoiceItems ii
                    INNER JOIN SalesInvoices si ON ii.InvoiceId = si.Id
                    WHERE si.IsActive = 1 
                      AND si.DocDate >= DATEADD(YEAR, -1, GETDATE())
                    GROUP BY ii.Description
                    ORDER BY TotalQty DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    names.Add(row["ProductName"].ToString());
                    quantities.Add(!row.IsNull("TotalQty") ? Convert.ToDouble(row["TotalQty"]) : 0);
                }

                if (names.Count == 0)
                {
                    names.Add("Δεν υπάρχουν δεδομένα");
                    quantities.Add(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα GetTop10Products: " + ex.Message);
                names.Add("Σφάλμα");
                quantities.Add(0);
            }

            return (names, quantities);
        }

        // ==========================================
        // 5. ΕΙΣΠΡΑΞΕΙΣ vs ΠΛΗΡΩΜΕΣ (Τελευταίοι 6 μήνες)
        // ==========================================
        public static (List<string> labels, List<double> income, List<double> expenses) GetCashFlow()
        {
            var labels = new List<string>();
            var incomeList = new List<double>();
            var expensesList = new List<double>();

            try
            {
                var monthNames = new[] { "Ιαν", "Φεβ", "Μαρ", "Απρ", "Μαι", "Ιουν", "Ιουλ", "Αυγ", "Σεπ", "Οκτ", "Νοε", "Δεκ" };
                int currentMonth = DateTime.Now.Month;

                // Αρχικοποίηση τελευταίων 6 μηνών με μηδενικά
                for (int i = 0; i < 6; i++)
                {
                    int monthIdx = ((currentMonth - 6 + i) % 12 + 12) % 12;
                    labels.Add(monthNames[monthIdx]);
                    incomeList.Add(0);
                    expensesList.Add(0);
                }

                // 1. ΕΙΣΠΡΑΞΕΙΣ (Receipts)
                // Χρησιμοποιούμε DocDate και Amount (όχι TotalAmount που είναι NULL)
                // GROUP BY εξασφαλίζει ότι δεν θα υπάρχουν διπλές εγγραφές
                string incomeQuery = @"
                    SELECT 
                        MONTH(DocDate) AS MonthNum, 
                        SUM(ISNULL(Amount, 0)) AS TotalAmount
                    FROM Receipts
                    WHERE IsActive = 1 
                      AND Type = 'Collection' 
                      AND DocDate >= DATEADD(MONTH, -6, GETDATE())
                    GROUP BY MONTH(DocDate)";

                DataTable dtIncome = DatabaseHelper.ExecuteQuery(incomeQuery);
                foreach (DataRow row in dtIncome.Rows)
                {
                    int monthNum = Convert.ToInt32(row["MonthNum"]) - 1; // 0-based index (0 = Ιαν)
                    double amount = !row.IsNull("TotalAmount") ? Convert.ToDouble(row["TotalAmount"]) : 0;

                    int idx = labels.FindIndex(l => l == monthNames[monthNum]);
                    if (idx >= 0) incomeList[idx] = amount;
                }

                // 2. ΠΛΗΡΩΜΕΣ / ΕΞΟΔΑ (Payments)
                // Χρησιμοποιούμε PaymentDate και TotalAmount
                // GROUP BY εξασφαλίζει ότι δεν θα υπάρχουν διπλές εγγραφές
                string expenseQuery = @"
                    SELECT 
                        MONTH(PaymentDate) AS MonthNum, 
                        SUM(ISNULL(TotalAmount, 0)) AS TotalAmount
                    FROM Payments 
                    WHERE PaymentDate >= DATEADD(MONTH, -6, GETDATE())
                    GROUP BY MONTH(PaymentDate)";

                DataTable dtExpense = DatabaseHelper.ExecuteQuery(expenseQuery);
                foreach (DataRow row in dtExpense.Rows)
                {
                    int monthNum = Convert.ToInt32(row["MonthNum"]) - 1; // 0-based index
                    double amount = !row.IsNull("TotalAmount") ? Convert.ToDouble(row["TotalAmount"]) : 0;

                    int idx = labels.FindIndex(l => l == monthNames[monthNum]);
                    if (idx >= 0) expensesList[idx] = amount;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα GetCashFlow: " + ex.Message, "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Fallback δεδομένα σε περίπτωση σφάλματος
                labels.Clear(); incomeList.Clear(); expensesList.Clear();
                var monthNames = new[] { "Ιαν", "Φεβ", "Μαρ", "Απρ", "Μαι", "Ιουν", "Ιουλ", "Αυγ", "Σεπ", "Οκτ", "Νοε", "Δεκ" };
                int currentMonth = DateTime.Now.Month;
                for (int i = 0; i < 6; i++)
                {
                    int monthIdx = ((currentMonth - 6 + i) % 12 + 12) % 12;
                    labels.Add(monthNames[monthIdx]);
                    incomeList.Add(0);
                    expensesList.Add(0);
                }
            }

            return (labels, incomeList, expensesList);
        }

        // ==========================================
        // 6. ΑΠΟΘΗΚΗ - ΕΠΙΠΕΔΑ ΣΤΟΚ
        // ==========================================
        public static (List<string> names, List<double> stock, List<int> statusColors) GetInventoryLevels()
        {
            var names = new List<string>();
            var stock = new List<double>();
            var statusColors = new List<int>();

            try
            {
                string query = @"
                    SELECT TOP 15 
                        ISNULL(ItemName, ISNULL(ItemCode, 'Άγνωστο')) AS ItemName,
                        ISNULL(Stock, 0) AS StockQuantity,
                        ISNULL(MinStock, 0) AS MinStock
                    FROM Items
                    WHERE IsActive = 1
                    ORDER BY Stock ASC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    double stockQty = !row.IsNull("StockQuantity") ? Convert.ToDouble(row["StockQuantity"]) : 0;
                    double minStock = !row.IsNull("MinStock") ? Convert.ToDouble(row["MinStock"]) : 0;

                    names.Add(row["ItemName"].ToString());
                    stock.Add(stockQty);

                    if (stockQty == 0) statusColors.Add(2);       // Κόκκινο (Out of stock)
                    else if (stockQty <= minStock) statusColors.Add(1); // Κίτρινο (Low stock)
                    else statusColors.Add(0);                     // Πράσινο (OK)
                }

                if (names.Count == 0)
                {
                    names.Add("Δεν υπάρχουν προϊόντα");
                    stock.Add(0);
                    statusColors.Add(2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα GetInventoryLevels: " + ex.Message);
                names.Add("Σφάλμα");
                stock.Add(0);
                statusColors.Add(2);
            }

            return (names, stock, statusColors);
        }

        // ==========================================
        // 7. ΠΩΛΗΣΕΙΣ ΑΝΑ ΠΩΛΗΤΗ
        // ==========================================
        public static (List<string> names, List<double> totals) GetSalesBySeller()
        {
            var names = new List<string>();
            var totals = new List<double>();

            try
            {
                string query = @"
                    SELECT 
                        ISNULL(e.FullName, 'Χωρίς Πωλητή') AS SellerName,
                        SUM(si.GrandTotal) AS TotalSales
                    FROM SalesInvoices si
                    LEFT JOIN Employees e ON si.SellerId = e.Id
                    WHERE si.IsActive = 1 
                      AND si.DocDate >= DATEADD(YEAR, -1, GETDATE())
                    GROUP BY e.FullName
                    ORDER BY TotalSales DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    names.Add(row["SellerName"].ToString());
                    totals.Add(!row.IsNull("TotalSales") ? Convert.ToDouble(row["TotalSales"]) : 0);
                }

                if (names.Count == 0)
                {
                    names.Add("Δεν υπάρχουν δεδομένα");
                    totals.Add(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα GetSalesBySeller: " + ex.Message);
                names.Add("Σφάλμα");
                totals.Add(0);
            }

            return (names, totals);
        }

        // ==========================================
        // 8. ΗΜΕΡΗΣΙΕΣ ΠΩΛΗΣΕΙΣ (Τρέχων Μήνας)
        // ==========================================
        public static (List<string> labels, List<double> dailySales) GetDailySales()
        {
            var labels = new List<string>();
            var dailySalesList = new List<double>();

            try
            {
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                for (int i = 1; i <= daysInMonth; i++)
                {
                    labels.Add(i.ToString());
                    dailySalesList.Add(0);
                }

                string query = @"
                    SELECT 
                        DAY(DocDate) AS DayNum,
                        SUM(GrandTotal) AS DailyTotal
                    FROM SalesInvoices
                    WHERE IsActive = 1 
                      AND YEAR(DocDate) = YEAR(GETDATE())
                      AND MONTH(DocDate) = MONTH(GETDATE())
                    GROUP BY DAY(DocDate)
                    ORDER BY DayNum";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    int day = Convert.ToInt32(row["DayNum"]) - 1; // 0-based index
                    if (day >= 0 && day < dailySalesList.Count)
                    {
                        dailySalesList[day] = !row.IsNull("DailyTotal") ? Convert.ToDouble(row["DailyTotal"]) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα GetDailySales: " + ex.Message);
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                Random rnd = new Random();
                for (int i = 1; i <= daysInMonth; i++)
                {
                    labels.Add(i.ToString());
                    dailySalesList.Add(rnd.Next(100, 1000));
                }
            }

            return (labels, dailySalesList);
        }
    }
}