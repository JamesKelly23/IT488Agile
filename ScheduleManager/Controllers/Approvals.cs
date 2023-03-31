using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class Approvals : Controller
    {
        [AuthenticateManager]
        public IActionResult Index() //Render the index view and pass it the Pending Time Off Request List and Pending Pickup Request List from the database
        {
            int loggedInID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee CurrentUser = new(loggedInID);
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.TOList = TimeOffRequest.GetPending();
            ViewBag.PUList = PickupRequest.GetPending();
            return View("Index");
        }
        [AuthenticateManager]
        public IActionResult TimeOffDetails(int id) //Show overlapping time off requests to give the manager more details about the request
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
            ViewData["DetailsTORID"] = id; //Tell the view which request to show details for
            return Index();
        }
        [AuthenticateManager]
        public IActionResult PickupDetails(int empid, int shiftid) //Show hours information for the employee so the manager can make an informed decision about the pickup request
        {
            ViewData["DetailsEmpID"] = empid;//Tell the view which request these details are for
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
        public IActionResult ActionPickup(int empid, int shiftid, bool approved) //Update the status and ManagerID of the Shift Pickup Request
        {
            Shift theShift = new Shift(shiftid); //Instantiate a Shift object for the related Shift object
            PickupRequest theRequest = new PickupRequest(shiftid, empid); //Instantiate the PickupRequest object
            if(approved==true)
            {
                theShift.EmployeeID = empid; //Assign the shift to its new Employee
                theShift.IsOpen = false; //Close the shift
                theShift.Save(); //Update the shift in the database
            }
            theRequest.IsApproved = approved; //Update the request to show the appropriate approved status
            theRequest.ManagerID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0; //Mark that the currently logged in user is the actioning manager
            theRequest.Save(); //Update the request in the database
            return Index();
        }
        [AuthenticateManager]
        public IActionResult ActionTimeOff(int id, bool approved) //Update the status and ManagerID of the TimeOffRequest
        {
            TimeOffRequest theRequest = new(id); //Instantiate the TimeOffRequest object based on the GET data
            theRequest.IsApproved = approved; //update the approved status based on the GET data
            theRequest.ManagerID = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0; //assign the currently logged in user as the actioning manager
            theRequest.Save(); //Update the request in the database
            return Index();
        }
    }
}
