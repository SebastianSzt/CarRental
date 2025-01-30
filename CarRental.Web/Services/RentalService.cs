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

        public async Task<RentalDto> GetRentalByIdAsync(int rentalId)
        {
            var response = await _httpClient.GetFromJsonAsync<RentalDto>($"api/Rentals/{rentalId}");
            if (response == null)
            {
                return null;
            }
            return response;
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

        public async Task<List<RentalDto>> GetRentalsByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"api/Rentals/user/{userId}/rentals");
            if (!response.IsSuccessStatusCode)
            {
                return new List<RentalDto>();
            }
            return await response.Content.ReadFromJsonAsync<List<RentalDto>>();
        }

        public async Task<(bool Success, string ErrorMessage)> CancelRentalAsync(int rentalId, RentalAllInputsDto rental)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Rentals/{rentalId}", rental);
            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, errorMessage);
            }
        }

        public async Task<bool> RentalExistsAsync(int carId, string userId)
        {
            var response = await _httpClient.GetAsync($"api/Rentals/exists?carId={carId}&userId={userId}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}