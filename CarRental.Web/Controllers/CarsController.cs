using CarRental.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetCarsAsync();
            return View(cars);
        }

        public async Task<IActionResult> More(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
    }
}
