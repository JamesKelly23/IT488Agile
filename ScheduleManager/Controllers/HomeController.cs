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
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if(loggedInEmployee == 0)
            {
               
            } else
            {
                ViewData["LoggedIn"] = 1;
                ViewBag.CurrentUser = new Employee(loggedInEmployee);
            }
            return View("Index");
        }

        public IActionResult LogIn()
        {
            int theID = Models.Employee.ValidateLogin(HttpContext.Request.Form["Username"], HttpContext.Request.Form["Password"]);
            if(theID ==0)
            {
                ViewData["Message"] = "Login Failed! Please Try again!";
            }
            else
            {
                HttpContext.Session.SetInt32("_LoggedInEmployeeID", theID);
                HttpContext.Session.SetInt32("_LoggedInRank", new Employee(theID).RankID);
            }
            return Index();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetInt32("_LoggedInEmployeeID",0);
            ViewData["Message"] = "Successfully Logged Out.";
            return Index();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}