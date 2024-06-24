using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DebraWebApp.Models;

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
    }
}
