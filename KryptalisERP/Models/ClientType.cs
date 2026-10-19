using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("ClientTypes")]
    public class ClientType
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}