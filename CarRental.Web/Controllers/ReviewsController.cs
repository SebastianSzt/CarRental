using CarRental.Dto.Reviews;
using CarRental.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly UserService _userService;
        private readonly RentalService _rentalService;
        private readonly ReviewService _reviewService;

        public ReviewsController(UserService userService, RentalService rentalService, ReviewService reviewService)
        {
            _userService = userService;
            _rentalService = rentalService;
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> AddReview(int carId)
        {
            var userId = _userService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index", "Home");

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return RedirectToAction("Index", "Home");

            if (!await _rentalService.RentalExistsAsync(carId, userId))
                return RedirectToAction("Index", "Home");

            if (await _reviewService.ReviewExistsAsync(carId, userId))
                return RedirectToAction("Index", "Home");

            var model = new ReviewInputDto
            {
                CarId = carId,
                UserId = userId,
                Date = DateTime.Now
            };

            ViewData["Title"] = "Add Review";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewInputDto model)
        {
            ViewData["Title"] = "Add Review";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _reviewService.AddReviewAsync(model);
            if (result)
            {
                TempData["SuccessMessage"] = "Review added successfully. You can leave.";
                return View(model);
            }

            TempData["ErrorMessage"] = "Error adding review. Please try again later.";
            return View(model);
        }
    }
}
