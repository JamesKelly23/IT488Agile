using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Controllers
{
    public class ScheduleEditor : Controller
    {
        public IActionResult ScheduleEditorIndex()
        {
            return View();
        }
    }
}
