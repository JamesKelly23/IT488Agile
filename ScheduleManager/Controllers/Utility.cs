using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class Utility : Controller
    {
        [AuthenticateManager]
        public IActionResult Index() //Generate the view unfiltered. No data is required
        {
            return View();
        }
        [AuthenticateManager]
        public IActionResult RoleManager() //Show the Role Manager View and pass it the list of roles from the database
        {
            ViewBag.RoleList = Models.Role.GetList();
            return View("RoleManager");
        }
        [AuthenticateManager]
        public IActionResult UpdateRole(int id) //Update the selected role with the content from the form
        {
            Role theRole = new(id);
            theRole.Name = HttpContext.Request.Form["NewRoleName"];
            theRole.Save();
            return RoleManager();
        }
        [AuthenticateManager]
        public IActionResult EditRole(int id) //Show the RoleManager view but pass it the ID of the role that needs an edit form rendered
        {
            ViewData["RoleToEdit"] = id;
            return RoleManager();
        }
        [AuthenticateManager]
        public IActionResult AddRole() //Add a new role to the database and generate the RoleManager View
        {
            new Role(HttpContext.Request.Form["NewRoleName"]).Save(); //Instantiate a new Role object with the data from the POST and save it to the database, then call the RoleManager Action
            return RoleManager();
        }
        [AuthenticateManager]
        public IActionResult RankList() //Show the RankList view and pass it the dlist of Ranks from the database
        {
            ViewBag.RankList=Models.Rank.GetList();
            return View("RankList");
        }
        [AuthenticateManager]
        public IActionResult DeleteRole(int id) //Delete a role from the database and then call the RoleManager Action
        {
            Models.Role.Delete(id);
            return RoleManager();
        }
    }
}
