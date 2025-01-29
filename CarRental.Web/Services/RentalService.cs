using CarRental.Dto.Cars;
using CarRental.Dto.Rentals;
using CarRental.Dto.Reviews;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarRental.Web.Services
{
    public class RentalService
    {
        private readonly HttpClient _httpClient;

        public RentalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateRentalAsync(RentalInputDto rental)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Rentals", rental);
            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, errorMessage);
            }
            else
            {
                return (false, "An unexpected error occurred.");
            }
        }

        public async Task<List<RentalDto>> GetTakenRentalsByCarIdAsync(int carId)
        {
            var response = await _httpClient.GetAsync($"api/Rentals/car/{carId}/taken-rentals");
            if (!response.IsSuccessStatusCode)
            {
                return new List<RentalDto>();
            }
            return await response.Content.ReadFromJsonAsync<List<RentalDto>>();
        }
    }
}