using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class Approvals : Controller
    {
        [AuthenticateManager]
        public IActionResult Index()
        {
            int loggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee CurrentUser = new(loggedInID);
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.TOList = TimeOffRequest.GetPending();
            ViewBag.PUList = PickupRequest.GetPending();
            return View("Index");
        }
        [AuthenticateManager]
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
        [AuthenticateManager]
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
        [AuthenticateManager]
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
        [AuthenticateManager]
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
