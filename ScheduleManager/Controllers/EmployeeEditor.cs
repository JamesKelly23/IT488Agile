using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class EmployeeEditor : Controller
    {
        public IActionResult EditorIndex()
        {
            EmployeeDetails(6);
            return View("EmployeeDetails");

        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult EmployeeDetails(int a)
        {
            
            ViewBag.currentDetails = new Models.Employee(a);
            return View();
        }
        public IActionResult Update() 
        {

            EmployeeDetails(7);
            return View("EmployeeDetails");
        }
    }
}
