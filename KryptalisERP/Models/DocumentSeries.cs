using System;

namespace KryptalisERP.Models
{
    public class DocumentSeries
    {
        public int Id { get; set; }

        // Scope
        public int CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int DocumentTypeId { get; set; }

        // Identification
        public string Code { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int NextNumber { get; set; }
        public int NumberLength { get; set; }

        // Configuration Flags
        public bool IsActive { get; set; } = true;
        public bool IsDefault { get; set; } = false;
        public bool IsManualNumbering { get; set; } = false;
        public bool IsAnnualReset { get; set; } = true;

        // Formatting
        public string PrefixFormat { get; set; } // e.g., "{PREFIX}-{YEAR}-{NUM}"

        // Access Control & Module
        public string Category { get; set; } // SALES, PURCHASE, INVENTORY...
        public string AllowedRoles { get; set; } // CSV: "Admin,Sales"
        public string Module { get; set; } // ERP, POS

        // myDATA Mapping
        public string AadeDocTypeCode { get; set; } // e.g., "1.1"

        // Optional: If you added Income/Expense Class IDs in previous steps
        // public int? IncomeClassificationId { get; set; } 
        // public int? ExpenseClassificationId { get; set; }
        // public string PrintTemplatePath { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}