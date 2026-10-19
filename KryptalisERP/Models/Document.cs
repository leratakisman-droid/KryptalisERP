using System;

namespace KryptalisERP.Models
{
    public class Document
    {
        public int Id { get; set; } // Primary Key

        // Σύνδεση Ρυθμίσεων
        public int CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int DocumentTypeId { get; set; }
        public int SeriesId { get; set; }

        // Αρίθμηση & Ημερομηνία
        public string Number { get; set; }
        public DateTime IssueDate { get; set; }
        public TimeSpan? IssueTime { get; set; } // Αν υπάρχει ώρες/λεπτά

        // Συμβαλλόμενοι (Πελάτης ή Προμηθευτής)
        public int? CustomerId { get; set; }
        public int? SupplierId { get; set; }

        // Νομισματικά
        public string Currency { get; set; } = "EUR";
        public decimal ExchangeRate { get; set; } = 1m;
        public int? PaymentTermsId { get; set; }

        // Οικονομικά Σύνολα
        public decimal TotalNet { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalWithholding { get; set; }
        public decimal TotalSurcharges { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; } // Alias ή αποθηκευμένο value

        // Κατάσταση & Meta
        public string Status { get; set; } = "DRAFT"; // DRAFT, ISSUED, PAID, CANCELLED
        public string Notes { get; set; }
        public int? SalespersonId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties (Για UI Binding)
        public List<DocumentLine> Lines { get; set; } = new List<DocumentLine>();
        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
        public DocumentSeries Series { get; set; }
    }
}