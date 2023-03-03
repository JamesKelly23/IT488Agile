using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class EmployeeEditor : Controller
    {
        public IActionResult EditorIndex()
        {
            Employee logon = new Employee(0);
            EmployeeTester Test = new EmployeeTester();
            Test.FirstName = "Test";
            Test.LastName = "Test";
            Test.DOB = "01 / 11 / 2000";
            Test.Email = "johndoe@gmail.com";
            Test.Phone = "1234567890";
            Test.Username = "Test";

            return View(logon);
        }
        public IActionResult Edit()
        {
            
            return View();
        }
    }
}
