namespace DebraWebApp.Models
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByEvent(int eventId)
        {
            var response = await _httpClient.GetAsync($"api/ticket/event/{eventId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>();
        }

        public async Task<Ticket> GetTicket(int id)
        {
            var response = await _httpClient.GetAsync($"api/ticket/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Ticket>();
        }

        public async Task<Ticket> SetTicketDetails(CreateTicketDTO createTicketDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ticket/set", createTicketDTO);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Ticket>();
        }
    }
}
