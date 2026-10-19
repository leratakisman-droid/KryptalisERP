using System.ComponentModel.DataAnnotations.Schema;

namespace KryptalisERP.Models
{
    [Table("Countries")]
    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Για εμφάνιση "GR - ΕΛΛΑΔΑ" στο ComboBox
        public string DisplayName => $"{Code} - {Name}";
    }
}