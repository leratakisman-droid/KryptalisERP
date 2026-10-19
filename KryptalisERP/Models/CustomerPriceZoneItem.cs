using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("CustomerPriceZoneItems")]
    public class CustomerPriceZoneItem
    {
        public int Id { get; set; }
        public int PriceZoneId { get; set; }
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual CustomerPriceZone PriceZone { get; set; } = null!;
    }
}