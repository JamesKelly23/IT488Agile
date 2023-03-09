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
            List<Shift> shiftList = Shift.GetScheduleByEmployee(loggedInID);
            List<String> dateList = new();
            foreach(Shift theShift in shiftList)
            {
                dateList.Add(theShift.ShiftDate.ToString("d"));
            }
            ViewBag.ConflictingDates = dateList;
            ViewBag.OpenShiftList = AvailableShifts;
            ViewBag.RequestedShiftList = RequestedShifts;
            return View("Index");
        }
        public IActionResult ShiftPickupRequest(int id, int empid)
        {
            PickupRequest theRequest = new(id, empid, false, 0);
            theRequest.Save();
            return Index();
        }
        public IActionResult DeleteRequest(int id, int empid)
        {
            PickupRequest theRequest = new(id, empid);
            theRequest.Delete();
            return Index();
        }
    }
}
