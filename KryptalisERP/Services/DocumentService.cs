using KryptalisERP.Models;
using KryptalisERP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KryptalisERP.Services
{
    public class DocumentService
    {
        private readonly DocumentRepository _repo;
        private readonly string _connStr; // Needed if we inject other repos later

        public DocumentService(string connectionString)
        {
            _connStr = connectionString;
            _repo = new DocumentRepository(connectionString);
        }

        /// <summary>
        /// Main entry point to create a new document.
        /// Handles validation, numbering generation, and saving.
        /// </summary>
        public async Task<int> CreateDocumentAsync(Document doc, int currentUserId = 1)
        {
            // 1. VALIDATION
            ValidateDocument(doc);

            // 2. PREPARE HEADER DATA
            doc.CreatedBy = currentUserId;
            doc.Status = "DRAFT"; // Initial status

            // Set default dates if missing
            if (doc.IssueDate == DateTime.MinValue)
                doc.IssueDate = DateTime.Today;

            // 3. GENERATE NUMBER (If not manual)
            if (!string.IsNullOrWhiteSpace(doc.Number))
            {
                // User provided a number manually (or it was pre-filled in UI)
                // We still need to ensure the Series NextNumber is updated past this value?
                // For simplicity, let's assume if Number exists, we trust it, 
                // BUT best practice is always generate via service unless strictly manual mode.

                // If you want strict auto-numbering even if UI shows something:
                var series = await _repo.GetSeriesForGenerationAsync(doc.SeriesId);
                if (series != null && !series.IsManualNumbering)
                {
                    // Force regeneration to guarantee uniqueness against DB state
                    doc.Number = await _repo.GenerateNextNumberAsync(series, doc.BranchId);
                }
            }
            else
            {
                // Auto-generate
                var series = await _repo.GetSeriesForGenerationAsync(doc.SeriesId);
                if (series == null) throw new Exception("Η επιλεγμένη σειρά παραστατικών δεν υπάρχει.");

                doc.Number = await _repo.GenerateNextNumberAsync(series, doc.BranchId);
            }

            // 4. CALCULATE TOTALS (Safety net - recalculate from lines)
            RecalculateTotals(doc);

            // 5. SAVE TO DATABASE
            return await _repo.SaveDocumentAsync(doc);
        }

        /// <summary>
        /// Updates an existing document (only if Draft/Cancelled usually).
        /// </summary>
        public async Task UpdateDocumentAsync(Document doc)
        {
            // Fetch original to check status
            var original = await _repo.GetDocumentByIdAsync(doc.Id);
            if (original == null) throw new Exception("Το παραστατικό δεν βρέθηκε.");

            if (original.Status != "DRAFT" && original.Status != "CANCELLED")
            {
                throw new InvalidOperationException($"Δεν μπορείτε να τροποποιήσετε ένα παραστατικό με κατάσταση '{original.Status}'.");
            }

            // Validate & Recalc
            ValidateDocument(doc);
            RecalculateTotals(doc);

            // Note: Our current Repo SaveDocumentAsync does INSERT. 
            // You would need an UPDATE method in Repo for full edit support.
            // For now, this stub warns that Edit requires Repo expansion.
            throw new NotImplementedException("Η μέθοδος Επεξεργασίας απαιτεί UPDATE query στο Repository.");
        }

        // ==========================================
        // PRIVATE HELPERS
        // ==========================================

        private void ValidateDocument(Document doc)
        {
            if (doc.SeriesId <= 0) throw new ArgumentException("Πρέπει να επιλέξετε Σειρά Παραστατικού.");
            if (doc.CustomerId == null && doc.SupplierId == null)
                throw new ArgumentException("Πρέπει να καθορίσετε είτε Πελάτη είτε Προμηθευτή.");

            if (doc.Lines == null || !doc.Lines.Any())
                throw new ArgumentException("Το παραστατικό πρέπει να έχει τουλάχιστον μία γραμμή.");

            foreach (var line in doc.Lines)
            {
                if (line.Quantity <= 0) throw new ArgumentException($"Λάθος ποσότητα στη γραμμή {line.LineNumber}.");
                if (line.UnitPrice < 0) throw new ArgumentException($"Αρνητική τιμή στη γραμμή {line.LineNumber}.");
            }
        }

        private void RecalculateTotals(Document doc)
        {
            decimal totalNet = 0;
            decimal totalVat = 0;
            decimal totalDiscount = 0;
            decimal totalTax = 0;
            decimal totalWithholding = 0;
            decimal grandTotal = 0;

            foreach (var line in doc.Lines)
            {
                // Ensure Line calculations are correct based on inputs
                // GrossValue should be Qty * UnitPrice
                line.GrossValue = Math.Round(line.Quantity * line.UnitPrice, 2);

                // Discount Value calculation
                if (line.DiscountPercent > 0)
                    line.DiscountValue = Math.Round(line.GrossValue * (line.DiscountPercent / 100m), 2);
                else if (line.Discount > 0) // Fixed amount discount
                    line.DiscountValue = line.Discount;
                else
                    line.DiscountValue = 0;

                // Net Value
                line.NetValue = line.GrossValue - line.DiscountValue;

                // VAT Calculation
                // Assuming VatRate is percentage (e.g., 24)
                if (line.VatRate > 0)
                    line.VatValue = Math.Round(line.NetValue * (line.VatRate / 100m), 2);
                else
                    line.VatValue = 0;

                // Other Taxes (Placeholder logic)
                line.TaxValue = 0;
                line.WithholdingValue = 0;

                // Line Total
                line.LineTotal = line.NetValue + line.VatValue + line.TaxValue - line.WithholdingValue;

                // Accumulate Headers
                totalNet += line.NetValue;
                totalVat += line.VatValue;
                totalDiscount += line.DiscountValue;
                totalTax += line.TaxValue;
                totalWithholding += line.WithholdingValue;
                grandTotal += line.LineTotal;
            }

            doc.TotalNet = Math.Round(totalNet, 2);
            doc.TotalVat = Math.Round(totalVat, 2);
            doc.TotalDiscount = Math.Round(totalDiscount, 2);
            doc.TotalTax = Math.Round(totalTax, 2);
            doc.TotalWithholding = Math.Round(totalWithholding, 2);
            doc.GrandTotal = Math.Round(grandTotal, 2);
            doc.Total = doc.GrandTotal; // Alias
        }
    }
}