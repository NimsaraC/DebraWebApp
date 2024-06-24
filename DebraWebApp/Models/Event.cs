namespace DebraWebApp.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public int PartnerId { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public decimal CommissionRate { get; set; }
    }
}
