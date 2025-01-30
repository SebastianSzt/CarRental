using System.Diagnostics;
using CarRental.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace CarRental.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult LearnMore()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetImageFiles()
        {
            var imagePath = Path.Combine(_env.WebRootPath, "images");
            var imageFiles = Directory.GetFiles(imagePath)
                                      .Select(Path.GetFileName)
                                      .Where(fileName => fileName != "noImage.jpg")
                                      .ToList();
            return Json(imageFiles);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
