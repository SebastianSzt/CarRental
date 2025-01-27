using CarRental.Dto.Users;
using CarRental.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputDto loginInputDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginInputDto);
            }

            var result = await _userService.LoginUserAsync(loginInputDto);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginInputDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _userService.LogoutUser();
            return RedirectToAction("Index", "Home");
        }
    }
}
