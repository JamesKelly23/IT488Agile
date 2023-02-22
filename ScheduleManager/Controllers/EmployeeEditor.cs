using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Controllers
{
    public class EmployeeEditor : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
