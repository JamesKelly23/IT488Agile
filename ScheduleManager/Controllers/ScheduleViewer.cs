using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Controllers
{
    public class ScheduleViewer : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
