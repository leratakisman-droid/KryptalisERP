using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KryptalisERP.Services
{
    public static class PosService
    {
        public static DataRow GetDefaultPosSettings()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT TOP 1 * FROM PosTerminals WHERE IsActive = 1 AND IsDefault = 1");
                if (dt.Rows.Count == 0) dt = DatabaseHelper.ExecuteQuery("SELECT TOP 1 * FROM PosTerminals WHERE IsActive = 1 ORDER BY Id");
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch { return null; }
        }

        public static DataRow GetPosSettingsByPaymentMethod(int paymentMethodId)
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT PosTerminalId FROM PaymentMethods WHERE Id = @Id", new SqlParameter("@Id", paymentMethodId));
                if (dt.Rows.Count == 0 || dt.Rows[0]["PosTerminalId"] == DBNull.Value) return GetDefaultPosSettings();

                int terminalId = Convert.ToInt32(dt.Rows[0]["PosTerminalId"]);
                DataTable terminalDt = DatabaseHelper.ExecuteQuery("SELECT * FROM PosTerminals WHERE Id = @Id AND IsActive = 1", new SqlParameter("@Id", terminalId));
                return terminalDt.Rows.Count > 0 ? terminalDt.Rows[0] : null;
            }
            catch { return null; }
        }

        public static bool IsPosPayment(string methodName)
        {
            if (string.IsNullOrEmpty(methodName)) return false;
            string lower = methodName.ToLower();
            return lower.Contains("pos") || lower.Contains("κάρτα") || lower.Contains("card") || lower.Contains("χρεωστική") || lower.Contains("debit") || lower.Contains("πιστωτική") || lower.Contains("credit");
        }

        public static async Task<PosResponse> ChargeAsync(decimal amount, string referenceNumber, int paymentMethodId = 0)
        {
            DataRow settings = paymentMethodId > 0 ? GetPosSettingsByPaymentMethod(paymentMethodId) : GetDefaultPosSettings();

            if (settings == null)
            {
                return new PosResponse { Success = false, Message = "❌ Δεν βρέθηκαν ρυθμίσεις POS!\nΠήγαινε στις Ρυθμίσεις → POS Τερματικά" };
            }

            string ipAddress = settings["IpAddress"].ToString();
            int port = Convert.ToInt32(settings["Port"]);
            string terminalId = settings["TerminalId"]?.ToString() ?? "TERM001";
            string provider = settings["Provider"]?.ToString() ?? "Generic";
            int timeout = Convert.ToInt32(settings["TimeoutSeconds"]);

            var webEcrRequest = new
            {
                RequestType = "SALE",
                Amount = amount,
                Currency = "EUR",
                TerminalID = terminalId,
                Provider = provider,
                ReferenceNumber = referenceNumber,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(timeout);
                    client.BaseAddress = new Uri($"http://{ipAddress}:{port}/");

                    var json = JsonSerializer.Serialize(webEcrRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // ΚΛΗΣΗ ΠΡΟΣ POS
                    var response = await client.PostAsync("api/pos/charge", content);
                    var responseJson = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonSerializer.Deserialize<PosResponse>(responseJson);
                        return result ?? new PosResponse { Success = false, Message = "Κενή απάντηση" };
                    }
                    else
                    {
                        return new PosResponse { Success = false, Message = $"POS Error {(int)response.StatusCode}: {responseJson}" };
                    }
                }
            }
            catch (TaskCanceledException) { return new PosResponse { Success = false, Message = "⏱️ Timeout - Το POS δεν απάντησε" }; }
            catch (Exception ex) { return new PosResponse { Success = false, Message = "Σφάλμα σύνδεσης: " + ex.Message }; }
        }
    }

    public class PosResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        // ✅ Στοιχεία που επιστρέφει το POS (Βήμα 3)
        public string ApprovalCode { get; set; }      // Authorization Code
        public string RRN { get; set; }               // Retrieval Reference Number
        public string TID { get; set; }               // Terminal ID
        public string MaskedCardNumber { get; set; }  // π.χ. 1234 **** **** 5678
        public string CardType { get; set; }          // VISA, MASTERCARD, κλπ.
        public DateTime? Timestamp { get; set; }      // Η ώρα της συναλλαγής από το POS
        public decimal Amount { get; set; }
        public string Provider { get; set; }
    }
}