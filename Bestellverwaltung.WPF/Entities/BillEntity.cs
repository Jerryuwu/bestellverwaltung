using System;

namespace Bestellverwaltung.WPF.Entities {
    public class BillEntity {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Bonus { get; set; }
        public decimal Fee { get; set; }
        public int CompanyId { get; set; }
        public int TaxId { get; set; }
    }
}