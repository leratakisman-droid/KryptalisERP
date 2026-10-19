using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("CustomerCreditLimits")]
    public class CustomerCreditLimit
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        // ✅ ΝΕΑ ΠΕΔΙΑ ΑΠΟ SQL
        public decimal CreditLimit { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal AvailableCredit { get; set; }
        public bool Blocked { get; set; }
        public string? BlockReason { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}