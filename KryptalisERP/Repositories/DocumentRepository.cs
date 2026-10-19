using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KryptalisERP.Models; // Χρειαζόμαστε τα Models μας

namespace KryptalisERP.Repositories
{
    public class DocumentRepository
    {
        private readonly string _connStr;

        public DocumentRepository(string connectionString)
        {
            _connStr = connectionString;
        }

        // ==========================================
        // 1. GET SERIES CONFIGURATION
        // ==========================================
        /// <summary>
        /// Fetches the series details including numbering logic and myDATA mappings.
        /// Uses UPDLOCK to prevent race conditions during number generation.
        /// </summary>
        public async Task<DocumentSeries> GetSeriesForGenerationAsync(int seriesId)
        {
            using var conn = new SqlConnection(_connStr);

            // We select all columns from our Model definition
            const string sql = @"
                SELECT Id, CompanyId, BranchId, DocumentTypeId, Code, Name, Prefix, 
                       NextNumber, NumberLength, IsActive, CreatedAt, UpdatedAt, 
                       AadeDocTypeCode, IsAnnualReset, PrefixFormat, IsDefault, 
                       IsManualNumbering, Category, AllowedRoles, Module
                FROM DocumentSeries 
                WHERE Id = @Id";

            return await conn.QueryFirstOrDefaultAsync<DocumentSeries>(sql, new { Id = seriesId });
        }

        // ==========================================
        // 2. GENERATE NEXT DOCUMENT NUMBER
        // ==========================================
        /// <summary>
        /// Calculates the next document number based on series rules and updates the DB atomically.
        /// </summary>
        public async Task<string> GenerateNextNumberAsync(DocumentSeries series, int? branchId = null)
        {
            if (series.IsManualNumbering)
                throw new InvalidOperationException("Η σειρά αυτή ορίζει χειροκίνητη αρίθμηση.");

            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                // Re-fetch with lock to ensure we have the latest NextNumber
                var currentSeries = await conn.QueryFirstOrDefaultAsync<DocumentSeries>(
                    "SELECT * FROM DocumentSeries WITH (UPDLOCK, ROWLOCK) WHERE Id = @Id",
                    new { Id = series.Id }, tran);

                if (currentSeries == null) throw new Exception("Σειρά δεν βρέθηκε.");

                int nextNum = currentSeries.NextNumber;

                // Handle Annual Reset Logic (Simplified: checks if year changed in prefix or separate field if implemented)
                // For now, assuming standard increment. If IsAnnualReset is true, 
                // you might need extra logic to check last issued date vs current year.
                // Here we just increment.

                string docNumber = FormatDocumentNumber(currentSeries, nextNum, branchId);

                // Update the counter in DB
                await conn.ExecuteAsync(
                    "UPDATE DocumentSeries SET NextNumber = @NewNum, UpdatedAt = GETDATE() WHERE Id = @Id",
                    new { NewNum = nextNum + 1, Id = currentSeries.Id }, tran);

                tran.Commit();
                return docNumber;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        private string FormatDocumentNumber(DocumentSeries series, int number, int? branchId)
        {
            string prefix = series.Prefix ?? "";

            // Apply PrefixFormat if exists (e.g., "{P}-{Y}-{N}")
            if (!string.IsNullOrEmpty(series.PrefixFormat))
            {
                string formatted = series.PrefixFormat
                    .Replace("{P}", prefix)
                    .Replace("{Y}", DateTime.Now.Year.ToString().Substring(2)) // e.g. 26 for 2026
                    .Replace("{B}", branchId?.ToString("D2") ?? "")
                    .Replace("{N}", number.ToString().PadLeft(series.NumberLength, '0'));
                return formatted;
            }

            // Default Fallback Logic
            string yearPart = series.IsAnnualReset ? DateTime.Now.Year.ToString().Substring(2) : "";
            string branchPart = branchId.HasValue ? $"-{branchId.Value:D2}" : "";
            string numPart = number.ToString().PadLeft(series.NumberLength, '0');

            return $"{prefix}{yearPart}{branchPart}-{numPart}";
        }

        // ==========================================
        // 3. SAVE NEW DOCUMENT (HEADER + LINES)
        // ==========================================
        /// <summary>
        /// Saves a complete document (Header and Lines) in a single transaction.
        /// Returns the ID of the newly created document.
        /// </summary>
        public async Task<int> SaveDocumentAsync(Document doc)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                // A. Insert Header
                const string headerSql = @"
                    INSERT INTO Documents (
                        CompanyId, BranchId, DocumentTypeId, SeriesId, Number, IssueDate, IssueTime,
                        CustomerId, SupplierId, Currency, ExchangeRate, PaymentTermsId,
                        TotalNet, TotalDiscount, TotalVat, TotalTax, TotalWithholding, TotalSurcharges, GrandTotal, Total,
                        Status, Notes, SalespersonId, CreatedBy, CreatedAt, UpdatedAt
                    ) OUTPUT INSERTED.Id VALUES (
                        @CompanyId, @BranchId, @DocumentTypeId, @SeriesId, @Number, @IssueDate, @IssueTime,
                        @CustomerId, @SupplierId, @Currency, @ExchangeRate, @PaymentTermsId,
                        @TotalNet, @TotalDiscount, @TotalVat, @TotalTax, @TotalWithholding, @TotalSurcharges, @GrandTotal, @Total,
                        @Status, @Notes, @SalespersonId, @CreatedBy, GETDATE(), GETDATE()
                    );";

                int newDocId = await conn.ExecuteScalarAsync<int>(headerSql, new
                {
                    doc.CompanyId,
                    doc.BranchId,
                    doc.DocumentTypeId,
                    doc.SeriesId,
                    doc.Number,
                    doc.IssueDate,
                    doc.IssueTime,
                    doc.CustomerId,
                    doc.SupplierId,
                    doc.Currency,
                    doc.ExchangeRate,
                    doc.PaymentTermsId,
                    doc.TotalNet,
                    doc.TotalDiscount,
                    doc.TotalVat,
                    doc.TotalTax,
                    doc.TotalWithholding,
                    doc.TotalSurcharges,
                    doc.GrandTotal,
                    doc.Total,
                    doc.Status,
                    doc.Notes,
                    doc.SalespersonId,
                    CreatedBy = 1 // TODO: Replace with actual User ID from session
                }, tran);

                // B. Insert Lines
                if (doc.Lines != null && doc.Lines.Any())
                {
                    const string lineSql = @"
                        INSERT INTO DocumentLines (
                            DocumentId, LineNumber, ItemId, Description, Quantity, UomId, UnitPrice,
                            GrossValue, DiscountPercent, DiscountValue, NetValue, VatCategory, VatRate, VatValue,
                            TaxValue, WithholdingValue, LineTotal, WarehouseId, LotId, SerialId,
                            ItemSize, ItemColor, MyDataCategory, Discount, CreatedAt, UpdatedAt
                        ) VALUES (
                            @DocumentId, @LineNumber, @ItemId, @Description, @Quantity, @UomId, @UnitPrice,
                            @GrossValue, @DiscountPercent, @DiscountValue, @NetValue, @VatCategory, @VatRate, @VatValue,
                            @TaxValue, @WithholdingValue, @LineTotal, @WarehouseId, @LotId, @SerialId,
                            @ItemSize, @ItemColor, @MyDataCategory, @Discount, GETDATE(), GETDATE()
                        );";

                    foreach (var line in doc.Lines)
                    {
                        line.DocumentId = newDocId; // Ensure FK is set
                        await conn.ExecuteAsync(lineSql, line, tran);
                    }
                }

                tran.Commit();
                return newDocId;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        // ==========================================
        // 4. GET DOCUMENT WITH LINES
        // ==========================================
        /// <summary>
        /// Retrieves a full document object including its lines.
        /// </summary>
        public async Task<Document> GetDocumentByIdAsync(int documentId)
        {
            using var conn = new SqlConnection(_connStr);

            // Fetch Header
            const string headerSql = "SELECT * FROM Documents WHERE Id = @Id";
            var doc = await conn.QueryFirstOrDefaultAsync<Document>(headerSql, new { Id = documentId });

            if (doc == null) return null;

            // Fetch Lines
            const string linesSql = "SELECT * FROM DocumentLines WHERE DocumentId = @DocId ORDER BY LineNumber";
            var lines = await conn.QueryAsync<DocumentLine>(linesSql, new { DocId = documentId });

            doc.Lines = lines.ToList();

            return doc;
        }

        // ==========================================
        // 5. UPDATE STATUS (e.g., ISSUED, PAID)
        // ==========================================
        public async Task<bool> UpdateDocumentStatusAsync(int documentId, string status)
        {
            using var conn = new SqlConnection(_connStr);
            const string sql = "UPDATE Documents SET Status = @Status, UpdatedAt = GETDATE() WHERE Id = @Id";
            int rows = await conn.ExecuteAsync(sql, new { Id = documentId, Status = status });
            return rows > 0;
        }
    }
}