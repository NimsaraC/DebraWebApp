namespace DebraWebApp.Models
{
    public class Admin
    {
        public int SaleId { get; set; }
        public int TicketId { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Commission { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public int PartnerId { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public decimal CommissionRate { get; set; }
    }
}
