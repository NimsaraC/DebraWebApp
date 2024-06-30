namespace DebraWebApp.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
