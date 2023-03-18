﻿using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class Composer : Controller
    {
        public IActionResult Index(int? modifier, bool? showavailabilities)
        {
            ViewBag.Modifier = modifier ?? 0;
            ViewBag.ShowAvailabilities = showavailabilities ?? false;
            DateTime theDate = DateTime.Today.AddDays((modifier ?? 0) * 7); //Add weeks based on the passed modifier argument
            while (theDate.DayOfWeek != DayOfWeek.Monday) //Roll backwards to the previous Monday
            {
               theDate = theDate.AddDays(-1);
            }
            ViewData["DateRange"] = theDate.ToString("d") + " - " + theDate.AddDays(6).ToString("d");
            ViewBag.StartDate = theDate;
            int loggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInID == 0)
            {
                ViewData["Message"] = "You are not logged in. In order to view this page, you must be logged in and privileged.";
                return View("Error");
            }
            int loggedInRank = HttpContext.Session.GetInt32("_LoggedInRank") ?? 0;
            if (loggedInRank < 3)
            {
                ViewData["Message"] = "You are not logged in. In order to view this page, you must be logged in and privileged.";
                return View("Error");
            }
            List<Models.Employee> employeeList = Models.Employee.GetList();
            List<Models.Shift> shiftList = Models.Shift.GetScheduleByDate(theDate, theDate.AddDays(6));
            ViewBag.EmployeeList = employeeList;
            ViewBag.ShiftList = shiftList;
            return View("Index");
        }
        public IActionResult Painter(int? id, DateTime? date) 
        {
            if(date == null)
            {
                ViewData["Message"] = "There was no date supplied to the Painter Action";
                return View("Error");
            }
            int loggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInID == 0)
            {
                ViewData["Message"] = "You are not logged in. In order to view this page, you must be logged in and privileged.";
                return View("Error");
            }
            int loggedInRank = HttpContext.Session.GetInt32("_LoggedInRank") ?? 0;
            if (loggedInRank < 3)
            {
                ViewData["Message"] = "You are not logged in. In order to view this page, you must be logged in and privileged.";
                return View("Error");
            }
            ViewBag.TargetDate = date;
            try
            {
                ViewBag.StartTime = Convert.ToDateTime(HttpContext.Request.Form["StartTime"]);
            }
            catch { }
            if(HttpContext.Request.Method == "POST")
            {
                ViewBag.RoleText = HttpContext.Request.Form["RoleTextBox"];
                if(ViewBag.RoleText == "")
                {
                    ViewBag.SelectedRole = HttpContext.Request.Form["RoleSelector"];
                }
                ViewBag.NotesText = HttpContext.Request.Form["NotesTextBox"];
            }
            ViewBag.EmployeeID = id ?? 0;
            ViewBag.EmployeeList = Models.Employee.GetList();
            ViewBag.ShiftList = Models.Shift.GetScheduleByDate(ViewBag.TargetDate, ViewBag.TargetDate);
            return View("Painter");
        }
        public IActionResult AddShift()
        {
            int loggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInID == 0)
            {
                ViewData["Message"] = "You are not logged in. In order to view this page, you must be logged in and privileged.";
                return View("Error");
            }
            int loggedInRank = HttpContext.Session.GetInt32("_LoggedInRank") ?? 0;
            if (loggedInRank < 3)
            {
                ViewData["Message"] = "You are not logged in. In order to view this page, you must be logged in and privileged.";
                return View("Error");
            }
            int empID = Convert.ToInt32(HttpContext.Request.Form["EmployeeID"]);
            DateTime theDate = Convert.ToDateTime(HttpContext.Request.Form["TargetDate"]);
            DateTime startTime = Convert.ToDateTime(HttpContext.Request.Form["StartTime"]);
            DateTime endTime = Convert.ToDateTime(HttpContext.Request.Form["EndTime"]);
            String Role = HttpContext.Request.Form["RoleSelector"];
            String Notes = HttpContext.Request.Form["NotesTextBox"];
            if (HttpContext.Request.Form["RoleTextBox"]!="")
            {
                Role = HttpContext.Request.Form["RoleTextBox"];
            }
            Shift theShift = new Shift(false, empID, theDate, startTime, endTime, Role, Notes);
            theShift.Save();
            return Painter(null, theDate);
        }
        public IActionResult DeleteShift(int id)
        {
            Shift theShift = new Shift(id);
            DateTime theDate = theShift.ShiftDate;
            theShift.Delete();
            return Painter(null, theDate);
        }
    }
}
