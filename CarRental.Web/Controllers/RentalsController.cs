using CarRental.Dto.Rentals;
using CarRental.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class RentalsController : Controller
    {
        private readonly RentalService _rentalService;
        private readonly UserService _userService;

        public RentalsController(RentalService rentalService, UserService userService)
        {
            _rentalService = rentalService;
            _userService = userService;
        }

        public async Task<IActionResult> YourRentals()
        {
            var userId = _userService.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var rentals = await _rentalService.GetRentalsByUserIdAsync(userId);
            return View(rentals);
        }

        [HttpPost]
        public async Task<IActionResult> CancelRental(int rentalId)
        {
            var rental = await _rentalService.GetRentalByIdAsync(rentalId);
            if (rental == null)
            {
                TempData["ErrorMessage"] = "Rental not found.";
                return RedirectToAction("YourRentals");
            }

            if (rental.StartDate < DateTime.Now.AddDays(1))
            {
                TempData["ErrorMessage"] = "Cannot cancel rental within one day before start.";
                return RedirectToAction("YourRentals");
            }

            RentalAllInputsDto rentalAllInputsDto = new RentalAllInputsDto
            {
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                TotalPrice = rental.TotalPrice,
                Status = "Cancelled",
                CarId = rental.CarId,
                UserId = rental.UserId
            };

            var (success, errorMessage) = await _rentalService.CancelRentalAsync(rentalId, rentalAllInputsDto);
            if (success)
            {
                TempData["SuccessMessage"] = "Rental cancelled successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to cancel rental.";
            }

            return RedirectToAction("YourRentals");
        }
    }
}
