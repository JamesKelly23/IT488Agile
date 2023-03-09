using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScheduleManager.Models;


namespace ScheduleManager.Controllers
{
    public class RequestTimeOff : Controller
    {
        //GET:RequestTimeOff
        public IActionResult Index()
        {
            //Display the form for RequestTimeOff 
            //Code to retrieve data from database
            //Pass it to index
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInEmployee == 0)
            {

            }
            else
            {
                ViewBag.TORList = TimeOffRequest.GetAllByEmployee(HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0);
                ViewData["LoggedIn"] = 1;
                ViewBag.CurrentUser = new Employee(loggedInEmployee);
            }
            return View("Index");


        }
        //POST: RequestTimeoff request

        public IActionResult Submit()
        {
            TimeOffRequest TheRequest = new(HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0, Convert.ToDateTime(HttpContext.Request.Form["start-date"]), Convert.ToDateTime(HttpContext.Request.Form["end-date"]), HttpContext.Request.Form["reason"]);
            TheRequest.Save();
            
            return Index();
            
        } 
    }
}