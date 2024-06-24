namespace DebraWebApp.Models
{
    public class CreateTicketDTO
    {
        public int EventId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }
}
