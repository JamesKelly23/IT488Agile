using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class ScheduleEditor : Controller
    {
        public IActionResult ScheduleEditorIndex()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            ViewBag.LoggedInEmployee = new Employee(loggedInEmployee);
            return View();
        }
        public IActionResult AddShift()
        {
            return View();
        }
        public IActionResult ViewShifts()
        {
            return View();
        }
    }
}
