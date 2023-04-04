using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ScheduleManager.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ScheduleManager.Controllers
{
    public class EmployeeEditor : Controller
    {
        [AuthenticateUser]
        /*Displays table for all employees*/
        public IActionResult EditorIndex()
        {

            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            Employee CurrentUser = new(loggedInEmployee);
            ViewBag.CurrentUser = CurrentUser;
            List<Employee> theEmployeeList = Models.Employee.GetList();

            ViewBag.EmployeeList = theEmployeeList;

            return View();

        }
        /*Shift Edit function. Divides Employee detials into sections. The a variable decides which section to show, which it gets 
         from the user's input (see view page)*/
        [AuthenticateUser]
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
        /*Displays Employee Information. Users will be taken to their own details. Managers and GMs are able to 
         view the details of other users.*/
        [AuthenticateUser]
        public IActionResult EmployeeDetails(int id)
        {

            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            if (loggedInEmployee == 0)
            {

            }
            else
            {
                ViewData["LoggedIn"] = 1;
                ViewBag.CurrentUser = new Employee(loggedInEmployee);
            }
            ViewBag.currentDetails = new Models.Employee(id);
            return View("EmployeeDetails");
        }
        /*Logic for searching employee. Not implemented as the need for it was not great.*/
        public IActionResult SearchEmply()
        {
            return View();
        }
        [AuthenticateManager]
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
        /*This is where data from edits are saved to the database. Data from the sections edited are saved.*/
        [AuthenticateUser]
        public IActionResult Update(int id, int a) 
        {
            Employee thisEmployee = new(id);
            bool passwordVerify = true;
            if (a == 1)
            {
                thisEmployee.FirstName = HttpContext.Request.Form["newFirstName"];
                thisEmployee.LastName = HttpContext.Request.Form["newLastName"];
                thisEmployee.RankID = Convert.ToInt32(HttpContext.Request.Form["newRank"]);
                thisEmployee.DOB = Convert.ToDateTime(HttpContext.Request.Form["newDOB"]);
            }
            else if(a == 2)
            {
                thisEmployee.Email = HttpContext.Request.Form["newEmail"];
                thisEmployee.Phone = HttpContext.Request.Form["newPhone"];
            }
            else if (a == 3)
            {
                thisEmployee.Username = HttpContext.Request.Form["newUserName"];
                thisEmployee.Password = HttpContext.Request.Form["newPassword"];
                string matchPassword = HttpContext.Request.Form["verifyPassword"];

                if(matchPassword != thisEmployee.Password)
                {
                    passwordVerify = false;
                }
            }
            else
            {
                return View("EmployeeDetails");
            }

            if (passwordVerify == false)
            {

                Edit(id, 3);
                ViewData["EmployeeEditResult"] = "Passwords do not match";
                return View("EmployeeDetails");
            }
            else if (HttpContext.Request.Form["newUserName"] == "")
            {
                Edit(id, 3);
                ViewData["EmployeeEditResult"] = "User Name cannot be empty.";
                return View("EmployeeDetails");
            }
            else if (HttpContext.Request.Form["newPassword"] == "")
            {
                Edit(id, 3);
                ViewData["EmployeeEditResult"] = "Password must be set.";
                return View("EmployeeDetails");
            }
            else
            {
                thisEmployee.Save();
                EmployeeDetails(id);
                ViewData["EmployeeEditResult"] = "Changes Saved";
                return View("EmployeeDetails");
            }

        }
        /*Displays the page for creating a new employee*/
        [AuthenticateManager]
        public ActionResult NewEmployee() 
        {
            return View();
        }
        /*Saves the details of the new Employee created. Should only be available to managers. Once the employee
         is created, it should take you to the detail page for the new employee.*/
        [AuthenticateManager]
        public ActionResult AddEmployee()
        {
            Employee newEmployee = new(0);
            newEmployee.FirstName = HttpContext.Request.Form["addFirstName"];
            newEmployee.LastName = HttpContext.Request.Form["addLastName"];
            newEmployee.RankID = Convert.ToInt32(HttpContext.Request.Form["addRank"]);
            newEmployee.DOB = Convert.ToDateTime(HttpContext.Request.Form["addDOB"]);
            newEmployee.Email = HttpContext.Request.Form["addEmail"];
            newEmployee.Phone = HttpContext.Request.Form["addPhone"];
            newEmployee.Username = HttpContext.Request.Form["addUserName"];
            newEmployee.Password = HttpContext.Request.Form["addPassword"];
            if (HttpContext.Request.Form["addUserName"].ToString() == "")
            {
                ViewData["NewEmployeeMessage"] = "Username cannot be null.";
                return View("NewEmployee");
            }
            else if(HttpContext.Request.Form["addPassword"].ToString() == "")
            {
                ViewData["NewEmployeeMessage"] = "Password must be filled out";
                return View("NewEmployee");
            }
            else
            {
                newEmployee.Save();
                EmployeeDetails(newEmployee.ID);
                ViewData["EmployeeEditResult"] = "Employee Created Successfully!";
                return View("EmployeeDetails");
            }

        }
        /*Method to delete an employee. Should only be available to Managers and GMs.*/
        [AuthenticateManager]
        public IActionResult Delete(int id)
        {
            Models.Employee.Delete(id);
            EditorIndex();
            ViewData["EmployeeIndexResult"] = "Employee Deleted Successfully!";
            return View("EditorIndex");
        }
    }
}
