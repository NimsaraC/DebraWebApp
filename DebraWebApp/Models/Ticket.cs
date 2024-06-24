namespace DebraWebApp.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
