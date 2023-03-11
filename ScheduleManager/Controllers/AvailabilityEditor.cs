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
            ViewBag.CurrentAvailability =  ViewBag.CurrentUser.GetCurrentAvailability();
            return View("Index");
        }

        public IActionResult Update()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee currentUser = new Employee(loggedInEmployee);
            Availability theAvail = currentUser.GetCurrentAvailability();
            bool success = true;
            foreach(DayOfWeek theDay in Models.Availability.GetDaysOfWeek())
            {
                theAvail.SetStart(theDay, Convert.ToDateTime("1/1/2001 " + Convert.ToDateTime(HttpContext.Request.Form[Models.Availability.DayString(theDay) + "Start"]).ToString("t")));
                theAvail.SetEnd(theDay, Convert.ToDateTime("1/1/2001 " + Convert.ToDateTime(HttpContext.Request.Form[Models.Availability.DayString(theDay) + "End"]).ToString("t")));
                if (HttpContext.Request.Form["CanOpen"].Contains(Availability.DayString(theDay)))
                {
                    theAvail.SetStart(theDay, Convert.ToDateTime("1/1/2001 00:00:00"));
                }
                if (HttpContext.Request.Form["CanClose"].Contains(Availability.DayString(theDay)))
                {
                    theAvail.SetEnd(theDay, Convert.ToDateTime("1/1/2001 23:00:00"));
                }
                if (HttpContext.Request.Form["NotAvailable"].Contains(Availability.DayString(theDay)))
                {
                    theAvail.SetStart(theDay, Convert.ToDateTime("1/1/2001 00:00:00"));
                    theAvail.SetEnd(theDay, Convert.ToDateTime("1/1/2001 00:00:00"));
                }
                if(theAvail.GetStart(theDay) > theAvail.GetEnd(theDay))
                {
                    ViewData["Message"] += Availability.DayString(theDay) + ": End Time cannot be before Start Time.<br>";
                    success = false;
                }
                if ((theAvail.GetStart(theDay) == theAvail.GetEnd(theDay)) && !HttpContext.Request.Form["NotAvailable"].Contains(Availability.DayString(theDay)))
                {
                    ViewData["Message"] += Availability.DayString(theDay) + ": Start and End time cannot be the same.<br>";
                    success = false;
                }
            }
            if(success)
            {
                theAvail.Save();
            }
            return Index();
        }
      




    }
}
