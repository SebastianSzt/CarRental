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

        public async Task<bool> AddReviewAsync(ReviewInputDto review)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reviews", review);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ReviewDto>> GetReviewsByCarIdAsync(int carId)
        {
            var response = await _httpClient.GetAsync($"api/Reviews/car/{carId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ReviewDto>();
            }
            return await response.Content.ReadFromJsonAsync<List<ReviewDto>>();
        }

        public async Task<bool> ReviewExistsAsync(int carId, string userId)
        {
            var response = await _httpClient.GetAsync($"api/Reviews/exists?carId={carId}&userId={userId}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}