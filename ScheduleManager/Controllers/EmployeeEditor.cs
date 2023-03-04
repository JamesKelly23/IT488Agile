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
        public IActionResult Edit(int id, int a)
        {
            if(a == 1)
            {
                ViewData["EmployeeEditGeneral"] = id;
                return EmployeeDetails(id);
            }
            else if(a == 2) 
            {
                ViewData["EmployeeEditContact"] = id;
                return EmployeeDetails(id);
            }
            else if (a == 3) 
            {
                ViewData["EmployeeEditAccount"] = id;
                return EmployeeDetails(id);
            }
            else
            {
                return EmployeeDetails(id);
            }
        }

        public IActionResult EmployeeDetails(int id)
        {
            
            ViewBag.currentDetails = new Models.Employee(id);
            return View("EmployeeDetails");
        }
        public IActionResult SearchEmply()
        {
            return View();
        }
        public IActionResult Search(int EmplyID)
        {
            try
            {

                EmployeeDetails(EmplyID);
                return View("EmployeeDetails");
            }
            catch (Exception ex) 
            {
                if (ViewBag.currentDetails == null)
                {
                    EditorIndex();
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
