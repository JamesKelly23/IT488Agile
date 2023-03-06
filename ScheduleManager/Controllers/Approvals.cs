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
            return View();
        }
    }
}
