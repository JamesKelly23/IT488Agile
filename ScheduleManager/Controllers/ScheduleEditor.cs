using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

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
            List<Shift> shifts = Models.Shift.GetList();
            ViewBag.SL = shifts;
            return View();
        }
        public IActionResult CreateShift()
        {
            Shift newShift = new(false, 1,Convert.ToDateTime(HttpContext.Request.Form["ShiftDate"]),Convert.ToDateTime(HttpContext.Request.Form["ShiftStart"]),Convert.ToDateTime(HttpContext.Request.Form["ShiftEnd"]),HttpContext.Request.Form["ShiftRole"],HttpContext.Request.Form["ShiftNotes"]);
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
    }
}
