using System;

namespace KryptalisERP.Models
{
    // Αντιστοιχεί ΑΚΡΙΒΩΣ στον πίνακα [dbo].[UnitsOfMeasure]
    public class Unit
    {
        public int Id { get; set; }

        // Κωδικός Μονάδας (π.χ. TEM, KG, LTR, ML)
        public string Code { get; set; }

        // Ονομασία (Μπορεί να είναι NULL ή άδειο σε παλιές εγγραφές)
        public string Name { get; set; }

        // Σύμβολο για εμφάνιση (π.χ. τεμ, kg, ml)
        public string Symbol { get; set; }

        // Δεκαδικά ψηφία (συνήθως 2)
        public int DecimalPlaces { get; set; } = 2;

        // Κωδικός myDATA (π.χ. TEM, KGM) - Μπορεί να είναι NULL
        public string MyDataCode { get; set; }

        // Κατάσταση ενεργότητας
        public bool IsActive { get; set; } = true;

        // Χρονικές στιγμές
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Κωδικός ΑΑΔΕ (UN/ECE Recommendation 20) (π.χ. EA, GRM, MTR, HRS)
        public string AadeCode { get; set; }

        // Συντελεστής μετατροπής (π.χ. 1.0 για βασική, 0.001 για χιλιοστόλιτρο)
        public decimal ConversionFactor { get; set; } = 1.0m;

        // Ελληνική Ονομασία (Η κύρια πηγή ονόματος στα νεότερα δεδομένα)
        public string NameGR { get; set; }

        // Helper για ComboBox Display / UI Binding
        // Προτιμά το NameGR, αλλιώς το Name, αλλιώς το Code
        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(NameGR)) return NameGR;
            if (!string.IsNullOrWhiteSpace(Name)) return Name;
            return Code ?? "?";
        }
    }
}