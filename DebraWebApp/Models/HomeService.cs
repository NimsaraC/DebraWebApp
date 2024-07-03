using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;
using System.Text;

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
        /*public async Task<(bool IsSuccess, string ErrorMessage)> AuthenticatePartnerAsync(string username, string password)
        {
            try
            {
                string url = $"api/Admin/authenticate?username={Uri.EscapeDataString(username)}&password={Uri.EscapeDataString(password)}";

                var response = await _httpClient.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Authentication request succeeded for username: {username}", username);
                    return (true, null);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError("Authentication request failed for username: {username} with message: {errorMessage}", username, errorMessage);
                return (false, errorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during authentication for username: {username}", username);
                return (false, "Exception occurred while trying to authenticate.");
            }
        }*/

        public async Task<(bool IsSuccess, string ErrorMessage)> AuthenticatePartnerAsync(string username, string password)
        {
            try
            {
                string url = "api/Admin/authenticate";

                var formData = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        });

                var response = await _httpClient.PostAsync(url, formData);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Authentication request succeeded for username: {username}", username);
                    return (true, null);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError("Authentication request failed for username: {username} with message: {errorMessage}", username, errorMessage);
                return (false, errorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during authentication for username: {username}", username);
                return (false, "Exception occurred while trying to authenticate.");
            }
        }






    }
}
