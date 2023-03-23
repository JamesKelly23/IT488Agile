using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class Utility : Controller
    {
        [AuthenticateManager]
        public IActionResult Index()
        {
            return View();
        }
        [AuthenticateManager]
        public IActionResult RoleManager()
        {
            ViewBag.RoleList = Models.Role.GetList();
            return View("RoleManager");
        }
        [AuthenticateManager]
        public IActionResult UpdateRole(int id)
        {
            Role theRole = new(id);
            theRole.Name = HttpContext.Request.Form["NewRoleName"];
            theRole.Save();
            return RoleManager();
        }
        [AuthenticateManager]
        public IActionResult EditRole(int id)
        {
            ViewData["RoleToEdit"] = id;
            return RoleManager();
        }
        [AuthenticateManager]
        public IActionResult AddRole()
        {
            new Role(HttpContext.Request.Form["NewRoleName"]).Save();
            return RoleManager();
        }
        [AuthenticateManager]
        public IActionResult RankList()
        {
            ViewBag.RankList=Models.Rank.GetList();
            return View("RankList");
        }
        [AuthenticateManager]
        public IActionResult DeleteRole(int id)
        {
            Models.Role.Delete(id);
            return RoleManager();
        }
    }
}
