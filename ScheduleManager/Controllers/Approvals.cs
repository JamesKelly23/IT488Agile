using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class Approvals : Controller
    {
        public IActionResult Index()
        {
            int loggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInID == 0)
            {
                ViewData["Message"] = "You are not logged in. In order to view this page, you must be logged in and privileged.";
                return View("Error");
            }
            Employee CurrentUser = new(loggedInID);
            if (CurrentUser.RankID < 2)
            {
                ViewData["Message"] = "You do not have sufficient privileges to view this screen.";
                return View("Error");
            }
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.TOList = TimeOffRequest.GetPending();
            ViewBag.PUList = PickupRequest.GetPending();
            return View("Index");
        }
        public IActionResult TimeOffDetails(int id)
        {
            TimeOffRequest theRequest = new TimeOffRequest(id);
            IEnumerable<TimeOffRequest> OverlapQuery = from theOverlappingRequest in TimeOffRequest.GetList()
                                               where (((theOverlappingRequest.EndDate >= theRequest.StartDate
                                               && theOverlappingRequest.EndDate <= theRequest.EndDate)
                                               || (theOverlappingRequest.StartDate >= theRequest.StartDate
                                               && theOverlappingRequest.StartDate <= theRequest.EndDate))
                                               && theOverlappingRequest.ID != id)
                                               select theOverlappingRequest;
            List<TimeOffRequest> OverlapList = OverlapQuery.ToList();
            OverlapList.Remove(theRequest);
            ViewBag.OverlapList = OverlapList;
            ViewData["DetailsTORID"] = id;
            return Index();
        }
        public IActionResult PickupDetails(int empid, int shiftid)
        {
            ViewData["DetailsEmpID"] = empid;
            ViewData["DetailsShiftID"] = shiftid;
            PickupRequest theRequest = new PickupRequest(shiftid, empid);
            int shiftLength = (theRequest.GetShift().EndTime - theRequest.GetShift().StartTime).Hours;
            ViewData["NewShiftLength"] = shiftLength;
            int previousHours = 0;
            foreach(Shift theShift in Shift.GetScheduleByEmployee(theRequest.GetShift().ShiftDate,empid))
            {
                previousHours += (theShift.EndTime - theShift.StartTime).Hours;
            }
            ViewData["PreviousScheduledHours"] = previousHours;
            ViewData["NewScheduledHours"] = previousHours + shiftLength;
            return Index();
        }
        public IActionResult ActionPickup(int empid, int shiftid, bool approved)
        {
            Shift theShift = new Shift(shiftid);
            PickupRequest theRequest = new PickupRequest(shiftid, empid);
            if(approved==true)
            {
                theShift.EmployeeID = empid;
                theShift.IsOpen = false;
                theShift.Save();
            }
            theRequest.IsApproved = approved;
            theRequest.ManagerID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            theRequest.Save();
            return Index();
        }
        public IActionResult ActionTimeOff(int id, bool approved)
        {
            TimeOffRequest theRequest = new(id);
            theRequest.IsApproved = approved;
            theRequest.ManagerID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            theRequest.Save();
            return Index();
        }
    }
}
