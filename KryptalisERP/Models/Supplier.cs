using System;

namespace KryptalisERP.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        // Basic Info
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Afm { get; set; }
        public string TaxOffice { get; set; }
        public string Profession { get; set; }

        // Contact
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Country { get; set; }

        // Banking
        public string Iban1 { get; set; }
        public string Iban2 { get; set; }
        public string Iban3 { get; set; }
        public int? BankId { get; set; }
        public int? BankId2 { get; set; }
        public int? BankId3 { get; set; }

        // Public Sector / Contracts
        public string ContractingAuthority { get; set; }
        public string AdamContract { get; set; }
        public string AdaCommitment { get; set; }
        public decimal BudgetAmount { get; set; }

        // Flags & Types
        public bool IsAlsoClient { get; set; }
        public bool IsActive { get; set; } = true;
        public int? SupplierTypeId { get; set; }

        // Meta
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}