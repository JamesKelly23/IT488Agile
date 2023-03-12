using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
	public class ScheduleViewer : Controller
	{
		public IActionResult Index(int? modifier)
		{
			ViewBag.Modifier = modifier ?? 0;
            int loggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInID == 0)
            {
                ViewData["Message"] = "You are not logged in. In order to view this page, you must be logged in and privileged.";
                return View("Error");
            }
            DateTime theDate = DateTime.Today.AddDays(7 * (modifier ?? 0));
			ViewBag.ShiftList = Shift.GetScheduleByEmployee(theDate, loggedInID);
			ViewBag.CurrentAvailability = new Employee(loggedInID).GetCurrentAvailability();
			return View("Index");
		}
		public IActionResult UpdateShift(int id, bool isOpen, int modifier)
		{
			Shift theShift = new Shift(id);
			theShift.IsOpen = isOpen;
			theShift.Save();
			return Index(modifier);
		}
	}
}
			
