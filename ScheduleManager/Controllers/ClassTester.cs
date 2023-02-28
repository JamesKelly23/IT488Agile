﻿using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Controllers
{
    public class ClassTester : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Employee()
        {
            ViewData["ClassType"] = "Employee";
            ViewBag.EmployeeList = ScheduleManager.Models.Employee.GetList();
            return View("List");
        }
        public IActionResult Rank()
        {
            ViewData["ClassType"] = "Rank";
            ViewBag.RankList = ScheduleManager.Models.Rank.GetList();
            return View("List");
        }
        public IActionResult Shift()
        {
            return View();
        }
        public IActionResult PickupRequest()
        {
            return View();
        }
        public IActionResult TimeOffRequest()
        {
            return View();
        }
        public IActionResult Availability()
        {
            ViewData["ClassType"] = "Availability";
            ViewBag.AvailabilityList = ScheduleManager.Models.Availability.GetList();
            return View("List");
        }
        public IActionResult Details(int id, string type)
        {
            ViewData["ClassType"] = type;
            switch(type)
            {
                case "Employee":
                    ViewBag.theEmployee = new ScheduleManager.Models.Employee(id);
                    break;
                case "Availability":
                    ViewBag.theAvailability = new ScheduleManager.Models.Availability(id);
                    break;
                case "EmployeeLogin":
                    ViewData["ClassType"] = "Employee";
                    string userName = HttpContext.Request.Form["Username"];
                    string passWord = HttpContext.Request.Form["Password"];
                    int theID = ScheduleManager.Models.Employee.ValidateLogin(userName, passWord);
                    ViewBag.theEmployee = new ScheduleManager.Models.Employee(theID);
                    break;
            }
            return View("Details");
        }
    }
}
