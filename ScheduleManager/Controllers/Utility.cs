using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class Utility : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RoleManager()
        {
            ViewBag.RoleList = Models.Role.GetList();
            return View("RoleManager");
        }
        public IActionResult UpdateRole(int id)
        {
            Role theRole = new(id);
            theRole.Name = HttpContext.Request.Form["NewRoleName"];
            theRole.Save();
            return RoleManager();
        }
        public IActionResult EditRole(int id)
        {
            ViewData["RoleToEdit"] = id;
            return RoleManager();
        }
        public IActionResult AddRole()
        {
            new Role(HttpContext.Request.Form["NewRoleName"]).Save();
            return RoleManager();
        }
        public IActionResult RankList()
        {
            ViewBag.RankList=Models.Rank.GetList();
            return View("RankList");
        }
        public IActionResult DeleteRole(int id)
        {
            Models.Role.Delete(id);
            return RoleManager();
        }
    }
}
