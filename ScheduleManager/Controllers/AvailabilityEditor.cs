using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;
using System.Diagnostics;
using System.Threading;

namespace ScheduleManager.Controllers
{
    public class AvailabilityEditor : Controller
    {
        public IActionResult Index()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            ViewBag.CurrentUser = new Employee(loggedInEmployee);
            ViewBag.CurrentAvailability =  new Availability(loggedInEmployee);

            //@ViewBag.CurrentAvailability.mondayStart
            ///ViewData["Name"]=ViewBag.CurrentUser.Name;

            return View();
        }


        public IActionResult Undo()
        {
            //int theID = 0;
            //int theID = ViewBag.CurrentUser.ID;

            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            ViewData["Message"] = "Reloaded Screen";

            ViewBag.CurrentUser = new Employee(loggedInEmployee);
            ViewBag.CurrentAvailability = new Availability(loggedInEmployee);

           // HttpContext.Session.SetInt32("_LoggedInEmployeeID", loggedInEmployee);
          // HttpContext.Session.SetInt32("_LoggedInRank", new Employee(loggedInEmployee).RankID);
            return View("Index");
        }

        public IActionResult Update()
        {//this update the users availibility in the database

            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            //HttpContext.Session.SetInt32("_LoggedInEmployeeID", theID);
            // HttpContext.Session.SetInt32("_LoggedInRank", new Employee(theID).RankID);


            ViewBag.CurrentUser = new Employee(loggedInEmployee);
            ViewBag.CurrentAvailability = new Availability(loggedInEmployee);
            ViewData["Message"] = "Update Complete!";
            return View("Index");
        }
      




    }
}
