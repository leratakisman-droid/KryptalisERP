using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("PostalCodes")]  // ✅ ΑΛΛΑΞΕ ΤΟ ΜΕ ΤΟ ΠΡΑΓΜΑΤΙΚΟ ΟΝΟΜΑ ΠΙΝΑΚΑ
    public class Postcode
    {
        public int Id { get; set; }

        // ⚠️ ΒΕΒΑΙΩΣΟΥ ΟΤΙ ΤΑ ΟΝΟΜΑΤΑ ΤΑΙΡΙΑΖΟΥΝ ΜΕ ΤΙΣ ΣΤΗΛΕΣ ΤΗΣ ΒΑΣΗΣ
        [Column("Code")]
        public string Code { get; set; } = string.Empty;

        [Column("Area")]
        public string Area { get; set; } = string.Empty;

        [Column("City")]
        public string City { get; set; } = string.Empty;

        [Column("IsActive")]
        public bool IsActive { get; set; } = true;

        public string DisplayName => $"{Code} - {Area}";
    }
}