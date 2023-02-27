using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;
using System.Diagnostics;

namespace ScheduleManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            ViewData["UserName"] = HttpContext.Request.Form["UserName"];
            ViewData["LoggedIn"] = 1;
            ViewData["AnyData"] = "This is some random data.";
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            ViewData["LoggedIn"] = 0;
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}