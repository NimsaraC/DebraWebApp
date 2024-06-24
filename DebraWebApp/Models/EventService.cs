namespace DebraWebApp.Models
{
    public class EventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            var response = await _httpClient.GetAsync("api/event");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Event>>();
        }

        public async Task<Event> GetEventAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/event/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task<IEnumerable<Event>> GetEventsByPartnerAsync(int partnerId)
        {
            var response = await _httpClient.GetAsync($"api/event/partner/{partnerId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Event>>();
        }

        public async Task<Event> CreateEventAsync(Event eventModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/event", eventModel);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        public async Task UpdateEventAsync(int id, Event eventModel)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/event/{id}", eventModel);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEventAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/event/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
