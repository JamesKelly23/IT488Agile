using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;
using System.Diagnostics;
using System.Threading;

namespace ScheduleManager.Controllers
{
    public class AvailabilityEditor : Controller
    {
        [AuthenticateUser]
        public IActionResult Index()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            ViewBag.CurrentUser = new Employee(loggedInEmployee);
            ViewBag.CurrentAvailability =  ViewBag.CurrentUser.GetCurrentAvailability();
            return View("Index");
        }
        [AuthenticateUser]
        public IActionResult Update()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee currentUser = new Employee(loggedInEmployee);
            Availability theAvail = currentUser.GetCurrentAvailability();
            bool success = true; //Defaults to true, any validation error detected throughout this process will change the value to false
            foreach(DayOfWeek theDay in Models.Availability.GetDaysOfWeek()) //Loop through each of the 7 days of the week.
            {
                //First two lines get the values from the form input.
                theAvail.SetStart(theDay, Convert.ToDateTime("1/1/2001 " + Convert.ToDateTime(HttpContext.Request.Form[Models.Availability.DayString(theDay) + "Start"]).ToString("t")));
                theAvail.SetEnd(theDay, Convert.ToDateTime("1/1/2001 " + Convert.ToDateTime(HttpContext.Request.Form[Models.Availability.DayString(theDay) + "End"]).ToString("t")));
                if (HttpContext.Request.Form["CanOpen"].Contains(Availability.DayString(theDay))) //CanOpen is checked. Set start time to midnight
                {
                    theAvail.SetStart(theDay, Convert.ToDateTime("1/1/2001 00:00:00"));
                }
                if (HttpContext.Request.Form["CanClose"].Contains(Availability.DayString(theDay))) //CanClose is checked. Set end time to 11PM
                {
                    theAvail.SetEnd(theDay, Convert.ToDateTime("1/1/2001 23:00:00"));
                }
                if (HttpContext.Request.Form["NotAvailable"].Contains(Availability.DayString(theDay))) //Not Available checkbox is checked. Set both times to midnight
                {
                    theAvail.SetStart(theDay, Convert.ToDateTime("1/1/2001 00:00:00"));
                    theAvail.SetEnd(theDay, Convert.ToDateTime("1/1/2001 00:00:00"));
                }
                if(theAvail.GetStart(theDay) > theAvail.GetEnd(theDay)) //End time is before start time
                {
                    ViewData["Message"] += Availability.DayString(theDay) + ": End Time cannot be before Start Time.<br>";
                    success = false;
                }
                if ((theAvail.GetStart(theDay) == theAvail.GetEnd(theDay)) && !HttpContext.Request.Form["NotAvailable"].Contains(Availability.DayString(theDay))) //Start and end times are the same, and the Not Available checkbox is not checked
                {
                    ViewData["Message"] += Availability.DayString(theDay) + ": Start and End time cannot be the same.<br>";
                    success = false;
                }
            }
            if(success) //Only save the availability if it passes all validation checks.
            {
                ViewData["SuccessMessage"] = "Availability Successfully Updated.";
                theAvail.Save();
            } else
            {
                ViewData["Message"] = "Save Unsuccessful: Resolve errors listed below.<br>" + ViewData["Message"];
            }
            return Index();
        }
    }
}
