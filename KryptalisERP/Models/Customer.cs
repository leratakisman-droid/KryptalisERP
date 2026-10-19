using System;

namespace KryptalisERP.Models
{
    public class Customer
    {
        public int Id { get; set; }

        // Basic Info
        public string Onoma { get; set; } // First Name
        public string Epitheto { get; set; } // Last Name
        public string Epwnymia { get; set; } // Legal Entity Name (if company)
        public string Afm { get; set; } // VAT Number (Critical for myDATA)
        public string DOY { get; set; } // Tax Office

        // Contact
        public string Dieuthinsi { get; set; } // Address
        public string Poli { get; set; } // City
        public string TK { get; set; } // Postal Code
        public string Periochi { get; set; } // Area/District
        public string Xora { get; set; } // Country
        public string Tilefono { get; set; } // Phone
        public string Kinhto { get; set; } // Mobile
        public string Email { get; set; }
        public string Istoselida { get; set; } // Website

        // Business Attributes
        public string Profession { get; set; } // Job Title
        public string Epargehma { get; set; } // Alternative profession field?
        public string TyposPelati { get; set; } // Client Type (Retail/Wholesale)
        public int? ClientTypeId { get; set; }
        public bool EinaiKaiPromitheutis { get; set; } // Is also supplier flag

        // Financials
        public decimal PosoProypologismou { get; set; } // Budget/Credit Limit estimate
        public decimal CreditLimit { get; set; }
        public string VATRegime { get; set; } // Regime code
        public int? VatExemptionId { get; set; }

        // Public Sector / ADAM/ADA
        public string AnathetousaArchi { get; set; } // Contracting Authority
        public string ADAM { get; set; }
        public string ADA { get; set; }
        public string TyposProypologismou { get; set; }

        // Assignment
        public int? SalespersonId { get; set; }
        public int? CollectorId { get; set; }
        public int? TerritoryId { get; set; }
        public int? PriceZoneId { get; set; }

        // Meta
        public bool Energo { get; set; } = true; // Active
        public string Tags { get; set; }
        public string Simeiwseis { get; set; } // Notes
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } // Denormalized or joined

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}