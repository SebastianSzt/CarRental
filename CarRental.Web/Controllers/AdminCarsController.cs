using CarRental.Dto.Cars;
using CarRental.Model.Entities;
using CarRental.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class AdminCarsController : Controller
    {
        private readonly CarService _carService;
        private readonly UserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminCarsController(CarService carService, UserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _carService = carService;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        private async Task<IActionResult> CheckAdminAsync()
        {
            var userId = _userService.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null || user.Role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            return null;
        }

        public async Task<IActionResult> Index()
        {
            var checkAdminResult = await CheckAdminAsync();
            if (checkAdminResult != null)
            {
                return checkAdminResult;
            }

            var cars = await _carService.GetCarsAsync();
            ViewData["Title"] = "Admin Car Management";
            return View(cars);
        }

        public async Task<IActionResult> Create()
        {
            var checkAdminResult = await CheckAdminAsync();
            if (checkAdminResult != null)
            {
                return checkAdminResult;
            }

            ViewData["Title"] = "Create Car";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarInputDto car)
        {
            var checkAdminResult = await CheckAdminAsync();
            if (checkAdminResult != null)
            {
                return checkAdminResult;
            }

            if (ModelState.IsValid)
            {
                var success = await _carService.AddCarAsync(car);
                if (success)
                {
                    TempData["SuccessMessage"] = "Car created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Failed to create car.";
            }
            return View(car);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var checkAdminResult = await CheckAdminAsync();
            if (checkAdminResult != null)
            {
                return checkAdminResult;
            }

            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Edit Car";
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarInputDto car)
        {
            var checkAdminResult = await CheckAdminAsync();
            if (checkAdminResult != null)
            {
                return checkAdminResult;
            }

            if (ModelState.IsValid)
            {
                var success = await _carService.UpdateCarAsync(id, car);
                if (success)
                {
                    TempData["SuccessMessage"] = "Car updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Failed to update car.";
            }

            var editedCar = await _carService.GetCarByIdAsync(id);

            return View(editedCar);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                TempData["ErrorMessage"] = "Car not found.";
                return RedirectToAction(nameof(Index));
            }

            var success = await _carService.DeleteCarAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Car deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Failed to delete car.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            var checkAdminResult = await CheckAdminAsync();
            if (checkAdminResult != null)
            {
                return checkAdminResult;
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var uniqueFileName = imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                TempData["SuccessMessage"] = "Image uploaded successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Failed to upload image.";
            return RedirectToAction(nameof(Index));
        }
    }
}
