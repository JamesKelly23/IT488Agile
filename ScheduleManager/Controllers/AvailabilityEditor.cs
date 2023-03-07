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


        public IActionResult Reset()
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

            // ViewBag.CurrentAvailability.MondayEnd = "1/1/2001 14:00:00";
             DateTime theTime = Convert.ToDateTime("1/1/2001 19:00:00");
            theTime = Convert.ToDateTime(HttpContext.Request.Form["m_end"]);


            ViewBag.CurrentAvailability.MondayEnd = theTime;
          //  Availability theAvail = new(loggedInEmployee);
            //ViewBag.CurrentAvailability.MondayEnd = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
          //  theAvail.MondayEnd = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);

            //theAvail.Save();
            ViewBag.CurrentAvailability.Save();


            //  ViewBag.CurrentAvailability =theAvail;

            ViewData["Message"] = "Update Complete!";

            /*

            //The problem is that I can't read from the view
            ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["m_end"]); 
            if (Convert.ToDateTime(HttpContext.Request.Form["m_end"])== Convert.ToDateTime(HttpContext.Request.Form["m_start"]))
            {
                ViewData["Message"] = "Same";
            }
           // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["m_end"]); 
            // ViewData["Message"] = theAvail.MondayEnd.ToString();
            */
            return View("Index");
        }
      




    }
}
