namespace Bestellverwaltung.WPF.Entities {
    public class BillDetailsEntity {
        public int BillId { get; set; }
        public int ArticleId { get; set; }
        public int ArticleAmount { get; set; }
        public decimal Price { get; set; }
    }
}