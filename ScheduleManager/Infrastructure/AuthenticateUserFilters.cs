using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScheduleManager.Models;

public class AuthenticateUser : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if ((context.HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0) == 0)
        {
            context.Result = new RedirectResult("~/Home");
        }
    }
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // Do something after the action executes.
    }
}
public class AuthenticateManager : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if ((context.HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0) == 0)
        {
            context.Result = new RedirectResult("~/Home");
        } else if((new Employee(context.HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0)).RankID < 2)
        {
            context.Result = new RedirectResult("~/Home/Unauth");
        }  
    }
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // Do something after the action executes.
    }
}
public class AuthenticateGM : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if ((context.HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0) == 0)
        {
            context.Result = new RedirectResult("~/Home");
        }
        else if((new Employee(context.HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0)).RankID < 3)
        {
            context.Result = new RedirectResult("~/Home/Unauth");
        }   
    }
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // Do something after the action executes.
    }
}