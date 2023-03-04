using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;
using System.Diagnostics;

namespace ScheduleManager.Controllers
{
    public class AvailabilityEditor : Controller
    {
        public IActionResult Index()
        {
            //ViewData["Name"]=ViewBag.CurrentUser.Name;
           
            return View();
        }


        public IActionResult Undo()
        {
            ViewData["Message"] = "Reloaded Screen";

            return View("Index");
        }

        public IActionResult Update()
        {
            ViewData["Message"] = "Update Complete!";
            return View("Index");
        }
      




    }
}
