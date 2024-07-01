using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DebraWebApp.Models
{
    public class SellService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SellService> _logger;

        public SellService(HttpClient httpClient, ILogger<SellService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Admin>> GetSalesByPartnerAsync(int partnerId)
        {
            var response = await _httpClient.GetAsync($"api/sell/partner/{partnerId}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error fetching sales for partner {partnerId}. Status Code: {response.StatusCode}");
                return new List<Admin>();
            }
            return await response.Content.ReadFromJsonAsync<IEnumerable<Admin>>();
        }

        public async Task<IEnumerable<Partner>> GetPartnersAsync()
        {
            var response = await _httpClient.GetAsync("api/partners");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Partner>>();
        }

        public async Task<IEnumerable<Admin>> GetAllSellAsync()
        {
            var response = await _httpClient.GetAsync("api/sell");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Admin>>();
        }

        public async Task<Sell> GetSellAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/sell/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Sell>();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error fetching sell by ID {Id}", id);
                return null;
            }
        }

        public async Task CreateSellAsync(Sell sell)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/sell", sell);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error creating sell");
            }
        }

        public async Task UpdateSellAsync(int id, Sell sell)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/sell/{id}", sell);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error updating sell ID {Id}", id);
            }
        }

        public async Task DeleteSellAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/sell/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error deleting sell ID {Id}", id);
            }
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
    }
}