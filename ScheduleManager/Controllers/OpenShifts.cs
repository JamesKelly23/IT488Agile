using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class OpenShifts : Controller
    {
        public IActionResult Index()
        {

            List<Shift> AvailableShifts = new List<Shift>();
            List<Shift> RequestedShifts = new List<Shift>();
            int loggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInID == 0)
            {
                ViewData["Message"] = "You are not logged in. In order to view this page, you must be logged in and privileged.";
                return View("Error");
            }
            foreach(Shift theShift in Shift.GetOpenShifts())
            {
                if(PickupRequest.Exists(loggedInID, theShift.ID))
                {
                    RequestedShifts.Add(theShift);
                } else
                {
                    AvailableShifts.Add(theShift);
                }
            }
            ViewBag.OpenShiftList = AvailableShifts;
            ViewBag.RequestedShiftList = RequestedShifts;
            return View("Index");
        }
        public IActionResult ShiftPickupRequest(int id)
        {
            PickupRequest theRequest = new(id, Convert.ToInt32(HttpContext.Request.Form["EmployeeID"]),false,0);
            theRequest.Save();
            return Index();
        }
        public IActionResult DeleteRequest(int id)
        {
            PickupRequest theRequest = new(id, Convert.ToInt32(HttpContext.Request.Form["EmployeeID"]));
            theRequest.Delete();
            return Index();
        }
    }
}
