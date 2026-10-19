using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("CustomerContacts")]
    public class CustomerContact
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        // ✅ ΝΕΑ ΠΕΔΙΑ ΑΠΟ SQL
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsDecisionMaker { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}