using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("CustomerActivities")]
    public class CustomerActivity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? UserId { get; set; }
        public int? ContactId { get; set; }

        // ✅ ΝΕΑ ΠΕΔΙΑ ΑΠΟ SQL
        public string? ActivityType { get; set; }
        public DateTime ActivityDate { get; set; } = DateTime.Now;
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public DateTime? NextActionDate { get; set; }
        public string? NextActionDescription { get; set; }
        public string? Status { get; set; } = "Open";
        public int? DurationMinutes { get; set; }
        public string? Outcome { get; set; }

        // ✅ ΠΡΟΣΘΗΚΗ ΤΟΥ ΧΑΜΕΝΟΥ ΠΕΔΙΟΥ
        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}