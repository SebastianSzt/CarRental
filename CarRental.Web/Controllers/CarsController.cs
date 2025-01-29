using CarRental.Dto.Rentals;
using CarRental.Web.Models;
using CarRental.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarService _carService;
        private readonly RentalService _rentalService;
        private readonly UserService _userService;

        public CarsController(CarService carService, RentalService rentalService, UserService userService)
        {
            _carService = carService;
            _rentalService = rentalService;
            _userService = userService;
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

        [HttpGet]
        public async Task<IActionResult> Rent(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return RedirectToAction("Index");
            }

            var userId = _userService.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var model = new RentalInputDto
            {
                StartDate = DateTime.Today.Date,
                EndDate = DateTime.Today.Date.AddDays(1),
                CarId = car.Id,
                UserId = userId
            };

            ViewData["Title"] = "Rent Car";
            ViewData["Car"] = car;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(RentalInputDto model)
        {
            var car = await _carService.GetCarByIdAsync(model.CarId);
            if (car == null)
            {
                TempData["ErrorMessage"] = "Car not found.";
                return View(model);
            }

            ViewData["Title"] = "Rent Car";
            ViewData["Car"] = car;

            if (model.StartDate < DateTime.Now)
            {
                TempData["ErrorMessage"] = "Start date cannot be in the past.";
                return View(model);
            }

            if ((model.EndDate - model.StartDate).Days > 7)
            {
                TempData["ErrorMessage"] = "Reservation cannot exceed one week.";
                return View(model);
            }

            var (success, errorMessage) = await _rentalService.CreateRentalAsync(model);

            if (success)
            {
                TempData["SuccessMessage"] = "Reservation successful. Check your email for details.";
                return View(model);
            }
            else
            {
                TempData["ErrorMessage"] = errorMessage ?? "Reservation failed. Please try again.";
                return View(model);
            }
        }
    }
}
