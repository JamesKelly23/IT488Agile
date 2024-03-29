﻿using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class Composer : Controller
    {
        [AuthenticateGM]
        public IActionResult Index(int? modifier, bool? showavailabilities) //Show a week at a time and generate links to the painter action for each day
        {
            ViewBag.Modifier = modifier ?? 0;
            ViewBag.ShowAvailabilities = showavailabilities ?? false;
            DateTime theDate = DateTime.Today.AddDays((modifier ?? 0) * 7); //Add weeks based on the passed modifier argument
            while (theDate.DayOfWeek != DayOfWeek.Monday) //Roll backwards to the previous Monday
            {
               theDate = theDate.AddDays(-1);
            }
            ViewData["DateRange"] = theDate.ToString("d") + " - " + theDate.AddDays(6).ToString("d"); //Supply view with the date range to display at the time of the screen as a string
            ViewBag.StartDate = theDate;
            List<Models.Employee> employeeList = Models.Employee.GetList();
            List<Models.Shift> shiftList = Models.Shift.GetScheduleByDate(theDate, theDate.AddDays(6));
            ViewBag.EmployeeList = employeeList;
            ViewBag.ShiftList = shiftList;
            return View("Index");
        }
        [AuthenticateGM]
        public IActionResult Painter(int? id, DateTime? date) //Show a grid. X-Axis:Hours, Y-Axis:Employees and allow the user to paint shifts onto the day using two clicks
        {
            if(date == null) //URL has most likely been tampered with.
            {
                ViewData["Message"] = "There was no date supplied to the Painter Action";
                return View("Error");
            }
            ViewBag.TargetDate = date;
            try
            {
                ViewBag.StartTime = Convert.ToDateTime(HttpContext.Request.Form["StartTime"]);
            }
            catch { }
            if(HttpContext.Request.Method == "POST") //A Shift has been started, pass the known values to the view
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
        [AuthenticateGM]
        public IActionResult AddShift() //The End time has been selected, finalize and insert the shift into the database
        {
            //Pull the shift details from the POST Request
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
            //Use the POST values to instantiate a Shift object
            Shift theShift = new Shift(false, empID, theDate, startTime, endTime, Role, Notes);
            theShift.Save(); //Insert record into database
            return Painter(null, theDate);
        }
        [AuthenticateGM]
        public IActionResult DeleteShift(int id) //Delete button has been clicked, delete the requested shift.
        {
            //Use GET data to instantiate the shift object
            Shift theShift = new Shift(id);
            DateTime theDate = theShift.ShiftDate;
            theShift.Delete(); //Delete the record from the database.
            return Painter(null, theDate);
        }
    }
}
