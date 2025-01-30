﻿using System.Net.Http;
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
            var response = await _httpClient.GetAsync("api/Cars");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<CarDto>>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<CarDto>();
            }
            else
            {
                response.EnsureSuccessStatusCode();
                return null;
            }
        }

        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Cars/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CarDto>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new CarDto();
            }
            else
            {
                response.EnsureSuccessStatusCode();
                return null;
            }
        }
    }
}
