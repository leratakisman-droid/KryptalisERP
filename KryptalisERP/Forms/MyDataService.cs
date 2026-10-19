using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace KryptalisERP.Services
{
    public class MyDataResult
    {
        public bool Success { get; set; }
        public string Uid { get; set; }
        public string Mark { get; set; }
        public string ErrorMessage { get; set; }
        public int DocumentDbId { get; set; } // ID στο MyDataDocuments
        public int TransmissionDbId { get; set; } // ID στο MyDataTransmissions
    }

    public static class MyDataService
    {
        private static readonly HttpClient _client = new HttpClient();

        // TODO: Βάλε εδώ τα πραγματικά credentials από τη ρύθμιση
        private const string ApiUrl = "https://mydata.aade.gr/mydata/v1/send";
        private const string Username = "";
        private const string Password = "";

        /// <summary>
        /// Στέλνει ένα Τιμολόγιο Πώλησης στο myDATA
        /// </summary>
        public static async Task<MyDataResult> SendSalesInvoiceAsync(int invoiceId)
        {
            return await ProcessDocumentAsync("SALES_INVOICE", invoiceId);
        }

        /// <summary>
        /// Στέλνει ένα Τιμολόγιο Αγοράς στο myDATA
        /// </summary>
        public static async Task<MyDataResult> SendPurchaseInvoiceAsync(int invoiceId)
        {
            return await ProcessDocumentAsync("PURCHASE_INVOICE", invoiceId);
        }

        private static async Task<MyDataResult> ProcessDocumentAsync(string docType, int docId)
        {
            var result = new MyDataResult();
            DateTime startTime = DateTime.Now;
            int myDataDocId = 0;
            int transmissionId = 0;

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                // 1. Έλεγχος αν υπάρχει ήδη επιτυχής αποστολή
                var existingSuccess = conn.QueryFirstOrDefault<int?>(@"
                    SELECT TOP 1 Id FROM MyDataDocuments 
                    WHERE DocumentId = @DocId AND InvoiceType = @Type 
                    AND TransmissionStatus = 'SUCCESS'", new { DocId = docId, Type = docType }, tran);

                if (existingSuccess.HasValue)
                {
                    result.Success = true;
                    result.ErrorMessage = "Το παραστατικό έχει ήδη σταλεί επιτυχώς.";
                    result.DocumentDbId = existingSuccess.Value;
                    return result;
                }

                // 2. Δημιουργία/Ενημέρωση Record στο MyDataDocuments ως PENDING
                myDataDocId = conn.ExecuteScalar<int>(@"
                    IF EXISTS(SELECT 1 FROM MyDataDocuments WHERE DocumentId=@DocId AND InvoiceType=@Type)
                        UPDATE MyDataDocuments SET TransmissionStatus='PENDING', UpdatedAt=GETDATE(), RetryCount=ISNULL(RetryCount,0)+1
                        OUTPUT INSERTED.Id WHERE DocumentId=@DocId AND InvoiceType=@Type
                    ELSE
                        INSERT INTO MyDataDocuments (DocumentId, InvoiceType, TransmissionStatus, CreatedAt, UpdatedAt)
                        VALUES (@DocId, @Type, 'PENDING', GETDATE(), GETDATE())
                        SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new { DocId = docId, Type = docType }, tran);

                // 3. Δημιουργία Record στο MyDataTransmissions για το log της προσπάθειας
                transmissionId = conn.ExecuteScalar<int>(@"
                    INSERT INTO MyDataTransmissions (DocumentId, Method, RequestDate, Status, Attempt, CreatedAt)
                    VALUES (@DocId, 'POST', GETDATE(), 'SENDING', 1, GETDATE());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new { DocId = myDataDocId }, tran);

                // 4. MOCK HTTP CALL (Αντικατάστησε με πραγματική κλήση API)
                await Task.Delay(500); // Simulate network latency
                bool isMockSuccess = Random.Shared.NextDouble() > 0.2; // 80% success rate for testing

                if (isMockSuccess)
                {
                    string mockUid = Guid.NewGuid().ToString("N").ToUpper();
                    string mockMark = "MOCK-" + Guid.NewGuid().ToString("N").Substring(0, 16).ToUpper();

                    // Update MyDataDocuments to SUCCESS
                    conn.Execute(@"
                        UPDATE MyDataDocuments 
                        SET UID=@Uid, MARK=@Mark, TransmissionStatus='SUCCESS', AcceptedAt=GETDATE(), TransmittedAt=GETDATE(), UpdatedAt=GETDATE()
                        WHERE Id=@Id",
                        new { Id = myDataDocId, Uid = mockUid, Mark = mockMark }, tran);

                    // Update MyDataTransmissions to COMPLETED/SUCCESS
                    conn.Execute(@"
                        UPDATE MyDataTransmissions 
                        SET Status='COMPLETED', HttpStatusCode=200, CompletedAt=GETDATE(), ResponsePayload='{""status"":""ok""}'
                        WHERE Id=@Id",
                        new { Id = transmissionId }, tran);

                    result.Success = true;
                    result.Uid = mockUid;
                    result.Mark = mockMark;
                    result.DocumentDbId = myDataDocId;
                    result.TransmissionDbId = transmissionId;
                }
                else
                {
                    string errorMsg = "Απόρριψη από ΑΑΔΕ: Λανθασμένο πεδίο VAT ή Missing Data.";

                    // Update MyDataDocuments to FAILED
                    conn.Execute(@"
                        UPDATE MyDataDocuments 
                        SET TransmissionStatus='FAILED', ErrorMessage=@Err, ErrorResponse=@Resp, UpdatedAt=GETDATE()
                        WHERE Id=@Id",
                        new { Id = myDataDocId, Err = errorMsg, Resp = "{error}" }, tran);

                    // Update MyDataTransmissions to ERROR
                    conn.Execute(@"
                        UPDATE MyDataTransmissions 
                        SET Status='ERROR', HttpStatusCode=400, ErrorCode='MYD-ERR-001', ErrorMessage=@Err, CompletedAt=GETDATE()
                        WHERE Id=@Id",
                        new { Id = transmissionId, Err = errorMsg }, tran);

                    // Log Error in MyDataErrors
                    conn.Execute(@"
                        INSERT INTO MyDataErrors (MyDataDocumentId, MyDataTransmissionId, ErrorCode, ErrorMessage, ErrorCategory, Severity, OccurredAt, Status, CreatedAt)
                        VALUES (@DocId, @TrId, 'MYD-ERR-001', @Msg, 'VALIDATION', 'HIGH', GETDATE(), 'OPEN', GETDATE())",
                        new { DocId = myDataDocId, TrId = transmissionId, Msg = errorMsg }, tran);

                    result.Success = false;
                    result.ErrorMessage = errorMsg;
                    result.DocumentDbId = myDataDocId;
                    result.TransmissionDbId = transmissionId;
                }

                tran.Commit();
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;

                // Try to update status to ERROR even if transaction failed partially
                try
                {
                    using var conn2 = new SqlConnection(GetConnectionString());
                    if (myDataDocId > 0)
                    {
                        conn2.Execute("UPDATE MyDataDocuments SET TransmissionStatus='ERROR', ErrorMessage=@Msg WHERE Id=@Id", new { Id = myDataDocId, Msg = ex.Message });
                    }
                    if (transmissionId > 0)
                    {
                        conn2.Execute("UPDATE MyDataTransmissions SET Status='ERROR', ErrorMessage=@Msg WHERE Id=@Id", new { Id = transmissionId, Msg = ex.Message });
                    }
                }
                catch { }

                return result;
            }
        }

        /// <summary>
        /// Ακύρωση Παραστατικού (Στέλνει cancellation request)
        /// </summary>
        public static async Task<MyDataResult> CancelDocumentAsync(int documentDbId, string reason)
        {
            // Εδώ θα έβαζες τη λογική για POST /cancel
            // Προς το παρόν απλά ενημερώνουμε τη βάση
            using var conn = new SqlConnection(GetConnectionString());

            // 1. Βρες το Original Mark
            var orig = conn.QueryFirstOrDefault<dynamic>("SELECT UID, MARK FROM MyDataDocuments WHERE Id = @Id", new { Id = documentDbId });
            if (orig == null) return new MyDataResult { Success = false, ErrorMessage = "Δεν βρέθηκε το έγγραφο." };

            // 2. Δημιούργησε Cancellation Record
            int cancelId = conn.ExecuteScalar<int>(@"
                INSERT INTO MyDataCancellations (CompanyId, OriginalDocumentId, OriginalMark, CancellationReason, StatusCode, Status, CreatedAt, UpdatedAt)
                VALUES (1, @OrigId, @OrigMark, @Reason, 'PENDING', 'REQUESTED', GETDATE(), GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                new { OrigId = documentDbId, OrigMark = orig.MARK, Reason = reason });

            // 3. Mock Success
            await Task.Delay(300);
            string cancelMark = "CANCEL-" + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();

            conn.Execute(@"
                UPDATE MyDataCancellations 
                SET CancellationMark=@Mark, CancellationUid=@Uid, AadeResponseStatus='SUCCESS', StatusCode='ACCEPTED', Status='COMPLETED', UpdatedAt=GETDATE()
                WHERE Id=@Id",
                new { Id = cancelId, Mark = cancelMark, Uid = Guid.NewGuid().ToString("N") });

            // 4. Update Main Document
            conn.Execute(@"
                UPDATE MyDataDocuments 
                SET CancelledByMark=@Mark, TransmissionStatus='CANCELLED', CancelledAt=GETDATE(), UpdatedAt=GETDATE()
                WHERE Id=@Id",
                new { Id = documentDbId, Mark = cancelMark });

            return new MyDataResult { Success = true, Mark = cancelMark, DocumentDbId = documentDbId };
        }

        private static string GetConnectionString() =>
            Program.Configuration?.GetConnectionString("DefaultConnection")
            ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}