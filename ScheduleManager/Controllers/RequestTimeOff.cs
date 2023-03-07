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
                ViewData["LoggedIn"] = 1;
                ViewBag.CurrentUser = new Employee(loggedInEmployee);
            }
            return View("Index");


        }
        //POST: RequestTimeoff request

        public IActionResult Submit()
        {

            TimeOffRequest TheRequest = new(employeeID, HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0)
            return View("Index");

        } 
    }
}