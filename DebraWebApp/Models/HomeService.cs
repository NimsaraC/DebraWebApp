namespace DebraWebApp.Models
{
    public class HomeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeService> _logger;
        public HomeService(HttpClient httpClient, ILogger<HomeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

        }
        public async Task<IEnumerable<Admin>> GetAllEventsAsync()
        {
            var response = await _httpClient.GetAsync("api/event");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Admin>>();
        }
        public async Task<IEnumerable<Admin>> GetSalesByEventAsync(int eventId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/sell/event/{eventId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<Admin>>();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error fetching sales by event ID {EventId}", eventId);
                return new List<Admin>();
            }
        }

        public async Task<decimal> GetEarningsByPartnerAsync(int partnerId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/sell/earnings/partner/{partnerId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<decimal>();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error fetching earnings by partner ID {PartnerId}", partnerId);
                return 0m;
            }
        }
        public async Task<decimal> GetEarningsByEventAsync(int eventId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/sell/earnings/event/{eventId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<decimal>();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error fetching earnings by event ID {EventId}", eventId);
                return 0m;
            }
        }
    }
}
