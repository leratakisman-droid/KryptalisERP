using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("CustomerAddresses")]
    public class CustomerAddress
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? AddressType { get; set; } // Κύρια, Αποστολής, Τιμολόγησης
        public string? Dieuthinsi { get; set; }
        public string? Address { get; set; }
        public string? Poli { get; set; }
        public string? City { get; set; }
        public string? Periochi { get; set; }
        public string? Area { get; set; }
        public string? TK { get; set; }
        public string? Postcode { get; set; }
        public string? Xora { get; set; }
        public string? Country { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}