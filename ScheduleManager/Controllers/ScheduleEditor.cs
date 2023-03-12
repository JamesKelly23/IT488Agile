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
            return View();
        }
    }
}
