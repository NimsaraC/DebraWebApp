namespace DebraWebApp.Models
{
    public class PartnerDashboard
    {
        public Partner Partner { get; set; }
        public IEnumerable<Event> ManagedEvents { get; set; }
        public IEnumerable<Sell> RecentSales { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
