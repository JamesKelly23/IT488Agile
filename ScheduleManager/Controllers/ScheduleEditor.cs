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
            int currentID = HttpContext.Session.GetInt32("_LoggedINEmployeeID") ?? 0;
            Employee employee = new Employee(currentID);
            int loggedInRank = employee.RankID;
            return loggedInRank;
        }
        public IActionResult ScheduleEditorIndex()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            ViewBag.LoggedInEmployee = new Employee(loggedInEmployee);
            List<Shift> shiftList = Models.Shift.GetList();

            ViewBag.ShiftList = shiftList;
            return View();
        }
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
        public IActionResult ViewShifts()
        {

            return View();
        }
        public IActionResult CreateShift()
        {
            Shift newShift = new(true, 1,Convert.ToDateTime(HttpContext.Request.Form["ShiftDate"]),Convert.ToDateTime(HttpContext.Request.Form["ShiftStart"]),Convert.ToDateTime(HttpContext.Request.Form["ShiftEnd"]),HttpContext.Request.Form["ShiftRole"],HttpContext.Request.Form["ShiftNotes"]);
            /*
            newShift.IsOpen = true;
            newShift.Role = HttpContext.Request.Form["ShiftRole"];
            newShift.ShiftDate = Convert.ToDateTime(HttpContext.Request.Form["ShiftDate"]);
            newShift.StartTime = Convert.ToDateTime(HttpContext.Request.Form["ShiftStart"]);
            newShift.EndTime = Convert.ToDateTime(HttpContext.Request.Form["ShiftEnd"]);
            newShift.Notes = HttpContext.Request.Form["ShiftNotes"];
            
            newShift.EmployeeID = 0;
            */
            newShift.Save();
            
            ScheduleEditorIndex();
            return View("ScheduleEditorIndex");
        }
        public IActionResult ViewDetails(int ID)
        {
            Shift thisShift = new Models.Shift(ID);
            ViewBag.ThisShift = thisShift;
            return View();
        }
        public IActionResult DeleteShift(int id)
        {
            Shift thisShift = new(id);
            thisShift.Delete();
            ScheduleEditorIndex();
            return View("ScheduleEditorIndex");
        }
    }
}
