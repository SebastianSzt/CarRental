using CarRental.Dto.Reviews;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarRental.Web.Services
{
    public class ReviewService
    {
        private readonly HttpClient _httpClient;

        public ReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReviewDto>> GetReviewsByCarIdAsync(int carId)
        {
            var response = await _httpClient.GetAsync($"api/Reviews/car/{carId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ReviewDto>>();
        }
    }
}