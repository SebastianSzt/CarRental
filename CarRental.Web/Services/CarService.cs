using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarRental.Dto.Cars;

namespace CarRental.Web.Services
{
    public class CarService
    {
        private readonly HttpClient _httpClient;

        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CarDto>> GetCarsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CarDto>>("api/Cars");
        }
    }
}
