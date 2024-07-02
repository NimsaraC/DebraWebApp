using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DebraWebApp.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace DebraWebApp.Services
{
    public class PartnerService
    {
        private readonly HttpClient _httpClient;

        public PartnerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Partner>> GetPartnersAsync()
        {
            var response = await _httpClient.GetAsync("api/partners");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Partner>>();
        }

        public async Task<Partner> GetPartnerAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/partners/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Partner>();
        }

        public async Task<Partner> GetPartnerAsync(string email)
        {
            var response = await _httpClient.GetAsync($"api/partners/email?email={email}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Partner>();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null; // Or handle as needed, e.g., throw custom exception
            }
            else
            {
                // Handle other status codes
                throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
            }
        }


        public async Task<Partner> RegisterPartnerAsync(Partner partner)
        {
            var response = await _httpClient.PostAsJsonAsync("api/partners/register", partner);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Partner>();
        }

        public async Task UpdatePartnerAsync(int id, Partner partner)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/partners/{id}", partner);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePartnerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/partners/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AuthenticatePartnerAsync(string email, string password)
        {
            var loginRequest = new LoginRequest { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/partners/authenticate", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return (false, errorMessage);
        }

        public async Task<IEnumerable<Event>> GetManagedEventsAsync(int partnerId)
        {
            var response = await _httpClient.GetAsync($"api/events/partner/{partnerId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Event>>();
        }

        public async Task<IEnumerable<Sell>> GetRecentSalesAsync(int partnerId)
        {
            var response = await _httpClient.GetAsync($"api/sales/partner/{partnerId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Sell>>();
        }
    }
}
