namespace DebraWebApp.Models
{
    public class Sell
    {
        public int SellId { get; set; }
        public int TicketId { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Commission { get; set; }
    }
}
