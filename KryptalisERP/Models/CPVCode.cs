using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("CPVCodes")]
    public class CPVCode
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}