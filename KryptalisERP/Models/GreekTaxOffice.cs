using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("GreekTaxOffices")]
    public class GreekTaxOffice
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Address { get; set; }
        public string? Postcode { get; set; }
        public bool IsActive { get; set; } = true;

        // Εμφάνιση στο ComboBox: "1101 - Α΄ ΑΘΗΝΩΝ"
        public string DisplayName => $"{Code} - {Name}";
    }
}