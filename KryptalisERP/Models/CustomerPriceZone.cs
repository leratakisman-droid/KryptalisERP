using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("CustomerPriceZones")]
    public class CustomerPriceZone
    {
        public int Id { get; set; }

        // ✅ ΝΕΑ ΠΕΔΙΑ ΑΠΟ SQL
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Currency { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}