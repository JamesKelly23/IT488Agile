using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class ClassTester : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RoleList()
        {
            ViewData["ClassType"] = "Role";
            ViewBag.RoleList = Models.Role.GetList();
            return View("List");
        }
        public IActionResult EmployeeList()
        {
            ViewData["ClassType"] = "Employee";
            ViewBag.EmployeeList = Models.Employee.GetList();
            return View("List");
        }
        public IActionResult RankList()
        {
            ViewData["ClassType"] = "Rank";
            ViewBag.RankList = Models.Rank.GetList();
            return View("List");
        }
        public IActionResult ShiftList()
        {
            ViewData["ClassType"] = "Shift";
            ViewBag.ShiftList = Models.Shift.GetList();
            return View("List");
        }
        public IActionResult PickupRequestList()
        {
            ViewData["ClassType"] = "PickupRequest";
            ViewBag.PickupRequestList = Models.PickupRequest.GetList();
            return View("List");
        }
        public IActionResult TimeOffRequestList()
        {
            ViewData["ClassType"] = "TimeOffRequest";
            ViewBag.TimeOffRequestList = Models.TimeOffRequest.GetList();
            return View("List");
        }
        public IActionResult AvailabilityList()
        {
            ViewData["ClassType"] = "Availability";
            ViewBag.AvailabilityList = Models.Availability.GetList();
            return View("List");
        }
        public IActionResult PickupRequestDetails(int shiftid, int empid)
        {
            ViewData["ClassType"] = "PickupRequest";
            ViewBag.thePickupRequest = new PickupRequest(shiftid, empid);
            return View("Details");
        }
        public IActionResult Details(int id, string type)
        {
            ViewData["ClassType"] = type;
            switch(type)
            {
                case "Employee":
                    ViewBag.theEmployee = new Models.Employee(id);
                    break;
                case "Availability":
                    ViewBag.theAvailability = new Models.Availability(id);
                    break;
                case "EmployeeLogin":
                    ViewData["ClassType"] = "Employee";
                    string userName = HttpContext.Request.Form["Username"];
                    string passWord = HttpContext.Request.Form["Password"];
                    int theID = Models.Employee.ValidateLogin(userName, passWord);
                    ViewBag.theEmployee = new Models.Employee(theID);
                    break;
                case "Shift":
                    ViewBag.theShift = new Models.Shift(id);
                    break;
                case "TimeOffRequest":
                    ViewBag.theTimeOffRequest = new Models.TimeOffRequest(id);
                    break;
            }
            return View("Details");
        }
        public IActionResult UpdatePickupRequest(int shiftid, int empid)
        {
            ViewData["ClassType"] = "PickupRequest";
            PickupRequest theRequest = new(shiftid, empid);
            theRequest.IsApproved = !theRequest.IsApproved;
            theRequest.ManagerID = theRequest.ManagerID==0 ? 1 : 0;
            ViewData["Message"] = theRequest.Save();
            ViewBag.thePickupRequest = new PickupRequest(shiftid, empid);
            return View("Details");
        }
        public IActionResult Update(int id, string type)
        {
            switch(type)
            {
                case "Employee":
                    Models.Employee emp = new(id);
                    emp.Password = HttpContext.Request.Form["NewPassword"];
                    ViewData["Message"]=emp.Save();
                    ViewData["ClassType"] = "Employee";
                    ViewBag.theEmployee = new Models.Employee(id);
                    return View("Details");
                case "Availability":
                    Models.Availability avail = new(id)
                    {
                        SundayStart = Convert.ToDateTime(HttpContext.Request.Form["NewSundayStart"]),
                        SundayEnd = Convert.ToDateTime(HttpContext.Request.Form["NewSundayEnd"])
                    };
                    ViewData["Message"] = avail.Save();
                    ViewData["ClassType"] = "Availability";
                    ViewBag.theAvailability = new Models.Availability(id);
                    return View("Details");
                case "Shift":
                    Models.Shift theShift = new(id)
                    {
                        EmployeeID = Convert.ToInt32(HttpContext.Request.Form["NewEmployeeID"])
                    };
                    ViewData["Message"]=theShift.Save();
                    ViewData["ClassType"] = "Shift";
                    ViewBag.theShift = new Models.Shift(id);
                    return View("Details");
                case "TimeOffRequest":
                    Models.TimeOffRequest theTimeOffRequest = new(id);
                    theTimeOffRequest.IsApproved=!theTimeOffRequest.IsApproved;
                    theTimeOffRequest.ManagerID = theTimeOffRequest.ManagerID == 0 ? 1 : 0;
                    ViewData["Message"] = theTimeOffRequest.Save();
                    ViewData["ClassType"] = "TimeOffRequest";
                    ViewBag.theTimeOffRequest = new TimeOffRequest(id);
                    return View("Details");
            }
            return View("Index");
        }
    }
}
