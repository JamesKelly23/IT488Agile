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

            /*
            if (Convert.ToDateTime(HttpContext.Request.Form["fake"]) == ViewBag.CurrentAvailability.SundayEnd)
            {
                ViewData["Message"] = "The Same";
            }
            else
            {
                ViewData["Message"] = "Not the Same!";
            }
            //@ViewBag.CurrentAvailability.mondayStart
            ///ViewData["Name"]=ViewBag.CurrentUser.Name;
            */
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
            DateTime timeStart = Convert.ToDateTime("1/1/2001 19:00:00");
            DateTime timeEnd = Convert.ToDateTime("1/1/2001 19:00:00");
            timeStart = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
            timeEnd = Convert.ToDateTime(HttpContext.Request.Form["m_end"]);

            if (timeStart == timeEnd && timeEnd!= Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {//this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                ViewData["Message"] = "Unless its 12AM for not available, start time cannot be end time. Error on monday.";
                return View("Index");
            }  
           else if (Convert.ToDateTime(HttpContext.Request.Form["m_start"]) == Convert.ToDateTime(HttpContext.Request.Form["m_end"]) && Convert.ToDateTime(HttpContext.Request.Form["m_end"]) == Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {//this means they are not available that day. 
                ViewData["Message"] = "Not Available.";
                theTime = Convert.ToDateTime("1/13/2001 00:00:00");
                ViewBag.CurrentAvailability.MondayStart = theTime;
                ViewBag.CurrentAvailability.MondayEnd = theTime;
            }
            else if(Convert.ToDateTime(HttpContext.Request.Form["m_end"]) == Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {//this means they can close, but do not open
                ViewData["Message"] = "Close, not open.";
                ViewBag.CurrentAvailability.MondayStart = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
                theTime = Convert.ToDateTime("1/13/2001 23:00:00");
                ViewBag.CurrentAvailability.MondayEnd = theTime;
            }
            else if (Convert.ToDateTime(HttpContext.Request.Form["m_start"]) == Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {//this means they can open, but do not close
                ViewData["Message"] = "Open, not close.";
                ViewBag.CurrentAvailability.MondayEnd = Convert.ToDateTime(HttpContext.Request.Form["m_end"]);
                theTime = Convert.ToDateTime("1/13/2001 00:00:00");
                ViewBag.CurrentAvailability.MondayStart = theTime;
            }
            else if (timeStart > timeEnd)
            {//this means the start time is before the end time. This is bad data
                ViewData["Message"] = "Start time cannot be before end time. Error on monday.";
                return View("Index");
            }
            else
            {//this means they start and end at neither open or close
                ViewData["Message"] = "Neither open or close.";
                theTime = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
                ViewBag.CurrentAvailability.MondayStart = theTime;
               theTime= Convert.ToDateTime(HttpContext.Request.Form["m_end"]);
                ViewBag.CurrentAvailability.MondayEnd = theTime;
            }

            // theTime = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);


           // ViewBag.CurrentAvailability.MondayEnd = theTime;



            //This works
            theTime = Convert.ToDateTime(HttpContext.Request.Form["su_end"]);
            if(theTime== Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {
                theTime = Convert.ToDateTime("1/13/2001 00:00:00");
            }

            ViewBag.CurrentAvailability.SundayEnd = theTime;
            //  Availability theAvail = new(loggedInEmployee);
            //ViewBag.CurrentAvailability.MondayEnd = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
            //  theAvail.MondayEnd = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);

            //theAvail.Save();
            ViewBag.CurrentAvailability.Save();


            /*
             * ViewData["Message"] = ViewBag.CurrentAvailability.SundayEnd;
             *  ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["su_end"]);
            if(Convert.ToDateTime(HttpContext.Request.Form["fake"])==Convert.ToDateTime(HttpContext.Request.Form["su_end"])){
                 ViewData["Message"] = "True";
            }
            else
            {
                ViewData["Message"] = "False!";
            }
            //  ViewBag.CurrentAvailability =theAvail;

            // ViewData["Message"] = "Update Complete!";

            
            if (HttpContext.Request.Form["m_op"]==true)
            {
                ViewData["Message"] = "mondya open is checked !";
            }
            else
            {
                ViewData["Message"] = "monday open isn't checked";
            }
            
            if(Convert.ToDateTime(HttpContext.Request.Form["m_start"])> Convert.ToDateTime(HttpContext.Request.Form["m_end"]))
            {
                ViewData["Message"] = "Start is after close!";
            }
            else if(Convert.ToDateTime(HttpContext.Request.Form["m_start"]) < Convert.ToDateTime(HttpContext.Request.Form["m_end"]))
            {
                ViewData["Message"] = "Start is before close";
            }
            else {
                ViewData["Message"] = "Equal";
            }
            

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
