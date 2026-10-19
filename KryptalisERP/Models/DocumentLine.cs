using System;

namespace KryptalisERP.Models
{
    public class DocumentLine
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int LineNumber { get; set; }

        // Είδος & Περιγραφή
        public int? ItemId { get; set; }
        public string Description { get; set; }

        // Ποσότητα & Μονάδα
        public decimal Quantity { get; set; }
        public int? UomId { get; set; }

        // Τιμές
        public decimal UnitPrice { get; set; }
        public decimal GrossValue { get; set; } // Qty * Price before discount

        // Εκπτώσεις
        public decimal DiscountPercent { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal NetValue { get; set; } // After discount

        // ΦΠΑ & Φόροι
        public string VatCategory { get; set; } // Π.χ. "E3", "V8" ή απλός κωδικός κατηγορίας
        public decimal VatRate { get; set; }
        public decimal VatValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal WithholdingValue { get; set; }

        // Τελικό Ποσό Γραμμής
        public decimal LineTotal { get; set; }

        // Stock / Tracking Details
        public int? WarehouseId { get; set; }
        public int? LotId { get; set; }
        public int? SerialId { get; set; }
        public string ItemSize { get; set; }
        public string ItemColor { get; set; }

        // myDATA Specifics per Line
        public string MyDataCategory { get; set; } // Κωδικός χαρακτηρισμού εσόδου/εξόδου
        public decimal Discount { get; set; } // Extra field if needed

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public Item Item { get; set; }
        public Unit  Unit { get; set; }
    }
}