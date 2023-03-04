using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ScheduleManager.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ScheduleManager.Controllers
{
    public class EmployeeEditor : Controller
    {
  
        public IActionResult EditorIndex()
        {

            List<Employee> theEmployeeList = Models.Employee.GetList();

            ViewBag.EmployeeList = theEmployeeList;

            return View();

        }
        public IActionResult Edit()
        {
            return View();
        }
        //[HttpPost]
        public IActionResult EmployeeDetails(int id)
        {
            
            ViewBag.currentDetails = new Models.Employee(id);
            return View("EmployeeDetails");
        }
        public IActionResult Search(int EmplyID)
        {
            try
            {
                //b = Convert.ToInt32((HttpContext.Request.Form["EmplyID"]));

                EmployeeDetails(EmplyID);
                return View("EmployeeDetails");
            }
            catch (Exception ex) 
            {
                if (ViewBag.currentDetails == false)
                {
                    return View("EditorIndex");
                }
                else
                {
                    throw ex;
                }
            }
        }
        public IActionResult Update() 
        {

            EmployeeDetails(7);
            return View("EmployeeDetails");
        }
    }
}
