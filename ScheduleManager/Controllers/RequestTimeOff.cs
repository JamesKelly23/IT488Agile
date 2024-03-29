﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScheduleManager.Models;


namespace ScheduleManager.Controllers
{
    public class RequestTimeOff : Controller
    {
        [AuthenticateUser]
        public IActionResult Index()
        {
            //Display the form for RequestTimeOff 
            //Code to retrieve data from database
            //Pass it to index
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInEmployee == 0)
            {
                ViewData["Message"] = "You must be logged in to view this page.";
                return View("Error");
            }
            else
            {
                ViewBag.TORList = TimeOffRequest.GetAllByEmployee(HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0);
                ViewData["LoggedIn"] = 1;
                ViewBag.CurrentUser = new Employee(loggedInEmployee);
            }
            return View("Index");
        }
        [AuthenticateUser]

        public IActionResult Submit()

        {
            if (Convert.ToDateTime(HttpContext.Request.Form["start-date"]) > Convert.ToDateTime(HttpContext.Request.Form["end-date"]))
            {
                ViewData["Message"] = "Start date cannot be after the end date.";
                return Index();
            }
            TimeOffRequest TheRequest = new(HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0, Convert.ToDateTime(HttpContext.Request.Form["start-date"]), Convert.ToDateTime(HttpContext.Request.Form["end-date"]), HttpContext.Request.Form["reason"]);
            TheRequest.Save();
            return Index();
        }
        [AuthenticateUser]
        public IActionResult Delete(int id)
        {
            if(id==null)
            {
                ViewData["Message"] = "No ID Supplied to Delete command.";
            }
            TimeOffRequest theRequest = new TimeOffRequest(id);
            if(theRequest.EmployeeID != HttpContext.Session.GetInt32("_LoggedInEmployeeID"))
            {
                ViewData["Message"] = "You cannot delete someone else's Time Off Request.";
            }
            theRequest.Delete();
            return Index();
        }
    }
}