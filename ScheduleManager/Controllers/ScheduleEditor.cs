﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ScheduleManager.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ScheduleManager.Controllers
{
    public class ScheduleEditor : Controller
    {
        public int LogonID ()
        {
            //Serves as a guide for limiting actions to certain login users
            int currentID = HttpContext.Session.GetInt32("_LoggedINEmployeeID") ?? 0;
            Employee employee = new Employee(currentID);
            int loggedInRank = employee.RankID;
            return loggedInRank;
        }
        [AuthenticateManager]
        public IActionResult ScheduleEditorIndex()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee employee = new Employee(loggedInEmployee);
            int currentRank = employee.RankID;
            if(currentRank <2)
            {
                return View("Error");
            }
            else
            {
                return View();
            }
            // List<Shift> shiftList = Models.Shift.GetList();

            //ViewBag.ShiftList = shiftList;
        }
        [AuthenticateManager]
        public IActionResult AddShift()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee employee = new Employee(loggedInEmployee);
            int currentRank = employee.RankID;
            if (currentRank < 2)
            {
                return View("Error");
            }
            else
            {
                
                return View();
            }

            //return View();
        }
        [AuthenticateManager]
        public IActionResult ViewShifts(int a)
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee employee = new Employee(loggedInEmployee);
            int currentRank = employee.RankID;
            if (currentRank < 2)
            {
                return View("Error");
            }
            else if (a == 1)
            {
                ViewBag.control = 1;
                return View();
            }
            else if (a == 2)
            {
                ViewBag.control = 2;
                List<Employee> employees = Models.Employee.GetList();
                ViewBag.EmplyList = employees;
                return View();
            }
            else if (a == 3)
            {
                ViewBag.control = 3;
                List<Shift> shiftList = Models.Shift.GetList();

                ViewBag.ShiftList = shiftList;

                return View();
            }
            else if (a == 4)
            {
                ViewBag.control = 4;
                List<Shift> openShifts = Models.Shift.GetOpenShifts();

                ViewBag.ShiftList = openShifts;
                return View();
            }
            else
            {
                return View("Error");
            } 
        }
        [AuthenticateManager]
        public IActionResult CreateShift()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee employee = new Employee(loggedInEmployee);
            int currentRank = employee.RankID;

            //Shift newShift = new(true, 1,Convert.ToDateTime(HttpContext.Request.Form["ShiftDate"]),Convert.ToDateTime(HttpContext.Request.Form["ShiftStart"]),Convert.ToDateTime(HttpContext.Request.Form["ShiftEnd"]),HttpContext.Request.Form["ShiftRole"],HttpContext.Request.Form["ShiftNotes"]);
            Shift newShift = new(0);
            if (Convert.ToInt32(HttpContext.Request.Form["ShiftEmployee"]) == 0)
            {
                newShift.IsOpen = true;
                newShift.EmployeeID = Convert.ToInt32(null);
            }
            else
            {
                newShift.IsOpen = false;
            }
            newShift.Role = HttpContext.Request.Form["ShiftRole"];
            newShift.ShiftDate = Convert.ToDateTime(HttpContext.Request.Form["ShiftDate"]);
            newShift.StartTime = Convert.ToDateTime(HttpContext.Request.Form["ShiftStart"]);
            newShift.EndTime = Convert.ToDateTime(HttpContext.Request.Form["ShiftEnd"]);
            newShift.EmployeeID = Convert.ToInt32(HttpContext.Request.Form["ShiftEmployee"]);


            newShift.Notes = HttpContext.Request.Form["ShiftNotes"];



            //newShift.EmployeeID = 0;
            //we don't want bad shifts to be created, so lets prevent that
             if (!(Shift.GetScheduleByEmployee(newShift.ShiftDate, newShift.ShiftDate, (HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0)).Count > 0)&&currentRank!=3)
            {//if the person isn't a manager the day they are adding to and not GM
                ViewData["Error"] = "Error: You are not the manager this day, you cannot edit it.";
                return View("AddShift");
            }
           else if (newShift.EndTime <= newShift.StartTime) {
                //end is before start
                ViewData["Error"] = "Error: End time cannot be before start time.";
                return View("AddShift");
            }
            else if (newShift.ShiftDate<DateTime.Today){
                //adding a shift for a day that has already passed

                ViewData["Error"] = "Error: Cannot create a shift for day before today.";
                return View("AddShift");

            }
            

            //if we reach here we are good.
            newShift.Save();
            
            ScheduleEditorIndex();
            return View("ScheduleEditorIndex");
        }
        [AuthenticateManager]
        public IActionResult ViewDetails(int ID)
        {
            Shift thisShift = new Models.Shift(ID);
            ViewBag.ThisShift = thisShift;
            return View();
        }
        [AuthenticateManager]
        public IActionResult DeleteShift(int id)
        {
            Shift thisShift = new(id);
            thisShift.Delete();
            ScheduleEditorIndex();
            return View("ScheduleEditorIndex");
        }
        [AuthenticateManager]
        public IActionResult EditShift(int id) 
        {
            ViewData["EditShift"] = id;
            ViewDetails(id);
            return View("ViewDetails");
        }
        [AuthenticateManager]
        public IActionResult UpdateShift(int id)
        {

            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee employee = new Employee(loggedInEmployee);
            int currentRank = employee.RankID;

            Shift updateShift = new Shift(id);
            updateShift.Role = HttpContext.Request.Form["NewRole"];
            updateShift.ShiftDate = Convert.ToDateTime(HttpContext.Request.Form["NewDate"]);
            updateShift.StartTime = Convert.ToDateTime(HttpContext.Request.Form["NewStartTime"]);
            updateShift.EndTime = Convert.ToDateTime(HttpContext.Request.Form["NewEndTime"]);
            
            updateShift.EmployeeID = Convert.ToInt32(HttpContext.Request.Form["NewEmployee"]);
            if(updateShift.EmployeeID == 0)
            {
                updateShift.IsOpen = true;
                updateShift.EmployeeID = Convert.ToInt32(null);
            }
            else
            {
                updateShift.IsOpen = false;
            }
            updateShift.Notes = HttpContext.Request.Form["NewNotes"];

            //now make sure things are good
            if(!(Shift.GetScheduleByEmployee(updateShift.ShiftDate, updateShift.ShiftDate, (HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0)).Count > 0 ) && currentRank != 3)
            {//if not the manager, then they can't update this.
                ViewData["Error"] = "Error: You are not manager this day, you cannot edit this shift.";
                return View("AddShift");

            }
            else if (updateShift.EndTime <= updateShift.StartTime)
            {
                //end is before start
                ViewData["Error"] = "Error: End time cannot be before start time.";
                return View("AddShift");
            }
            else if (updateShift.ShiftDate < DateTime.Today)
            {
                //adding a shift for a day that has already passed

                ViewData["Error"] = "Error: Cannot change a shift to a day before today.";
                return View("AddShift");

            }


            //if we reach here, the shift is good to go
            updateShift.Save();
            ViewDetails(id);
            return View("ViewDetails");
        }
        [AuthenticateManager]
        public IActionResult ByDate()
        {
            DateTime a = Convert.ToDateTime(HttpContext.Request.Form["StartDate"]);
            DateTime b = Convert.ToDateTime(HttpContext.Request.Form["EndDate"]);
            List<Shift> shiftList = Models.Shift.GetScheduleByDate(a, b);
            ViewBag.ShiftList = shiftList;
            ViewShifts(1);
            return View ("ViewShifts");
        }
        [AuthenticateManager]
        public IActionResult ByEmployee(int id)
        {
            
            List<Shift> employeeShift = Models.Shift.GetScheduleByEmployee(id);
            ViewBag.ShiftList = employeeShift;
            ViewShifts(2);
            return View("ViewShifts");
        }
    }
}
