using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Controllers
{
    public class AvailabilityEditor : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
