using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("CustomerBranches")]
    public class CustomerBranch
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int? CompanyId { get; set; }

        // Βασικά Στοιχεία
        [MaxLength(200)]
        public string BranchName { get; set; } // Μόνο ένα BankName υπάρχει στη βάση σου

        [MaxLength(20)]
        public string BranchCode { get; set; }

        public bool IsMain { get; set; }
        public bool IsActive { get; set; }

        // Διεύθυνση
        [MaxLength(510)]
        public string Address { get; set; }

        [MaxLength(200)]
        public string City { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        // Επικοινωνία
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(20)]
        public string Phone2 { get; set; }
        [MaxLength(20)]
        public string Phone3 { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }
        [MaxLength(20)]
        public string Mobile2 { get; set; }

        [MaxLength(20)]
        public string Fax { get; set; }

        [MaxLength(300)]
        public string Email { get; set; }

        [MaxLength(510)]
        public string Website { get; set; }

        // Υπεύθυνος & Φορολογικά
        [MaxLength(200)]
        public string ManagerName { get; set; }

        [MaxLength(20)]
        public string VatNumber { get; set; }

        [MaxLength(200)]
        public string Doy { get; set; }

        [MaxLength(50)]
        public string MyDataBranchId { get; set; }

        // Τραπεζικά (Σύμφωνα με τη βάση που έστειλες)
        [MaxLength(200)]
        public string BankName { get; set; } // Υπάρχει μόνο ένα πεδίο BankName στη βάση

        [MaxLength(300)]
        public string BankAccountHolder { get; set; }

        [MaxLength(34)]
        public string BankIban { get; set; }
        [MaxLength(34)]
        public string BankIban2 { get; set; }
        [MaxLength(34)]
        public string BankIban3 { get; set; }
        [MaxLength(34)]
        public string BankIban4 { get; set; }
        [MaxLength(34)]
        public string BankIban5 { get; set; }
        [MaxLength(34)]
        public string BankIban6 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}