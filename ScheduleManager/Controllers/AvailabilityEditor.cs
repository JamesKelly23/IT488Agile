using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Models;
using System.Diagnostics;
using System.Threading;

namespace ScheduleManager.Controllers
{
    public class AvailabilityEditor : Controller
    {
        public IActionResult Index()
        {
            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            ViewBag.CurrentUser = new Employee(loggedInEmployee);
            ViewBag.CurrentAvailability =  new Availability(loggedInEmployee);

            /*
            if (Convert.ToDateTime(HttpContext.Request.Form["fake"]) == ViewBag.CurrentAvailability.SundayEnd)
            {
                ViewData["Message"] = "The Same";
            }
            else
            {
                ViewData["Message"] = "Not the Same!";
            }
            //@ViewBag.CurrentAvailability.mondayStart
            ///ViewData["Name"]=ViewBag.CurrentUser.Name;
            */
            return View();
        }


        public IActionResult Reset()
        {
            //int theID = 0;
            //int theID = ViewBag.CurrentUser.ID;

            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            ViewData["Message"] = "Reloaded Screen";

            ViewBag.CurrentUser = new Employee(loggedInEmployee);
            ViewBag.CurrentAvailability = new Availability(loggedInEmployee);

           // HttpContext.Session.SetInt32("_LoggedInEmployeeID", loggedInEmployee);
          // HttpContext.Session.SetInt32("_LoggedInRank", new Employee(loggedInEmployee).RankID);
            return View("Index");
        }

        public IActionResult Update()
        {//this update the users availibility in the database

            int loggedInEmployee = HttpContext.Session.GetInt32("_LoggedInEmployeeID") ?? 0;
            //HttpContext.Session.SetInt32("_LoggedInEmployeeID", theID);
            // HttpContext.Session.SetInt32("_LoggedInRank", new Employee(theID).RankID);



           


            ViewBag.CurrentUser = new Employee(loggedInEmployee);
            ViewBag.CurrentAvailability = new Availability(loggedInEmployee);

            // ViewBag.CurrentAvailability.MondayEnd = "1/1/2001 14:00:00";


             DateTime theTime = Convert.ToDateTime("1/1/2001 19:00:00");
            DateTime timeStart = Convert.ToDateTime("1/1/2001 19:00:00");
            DateTime timeEnd = Convert.ToDateTime("1/1/2001 19:00:00");
            //these two variables exist to simply save space while typing below
          //  timeStart = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
            //timeEnd = Convert.ToDateTime(HttpContext.Request.Form["m_end"]);

            int determineScenario(DateTime start, DateTime end)
            {
                if ((start == end && end == Convert.ToDateTime(HttpContext.Request.Form["Fake"]))||(end== Convert.ToDateTime("1/1/2001 00:00:00")))
                {//not Available
                    return 2;
                }
                //|| (start == end && end == Convert.ToDateTime("1/1/2001 00:00:00"))
                else if ((start == end && end != Convert.ToDateTime(HttpContext.Request.Form["Fake"])))
                {//this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                   
                    return 1;
                }
                
                else if(end == Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
                {
                    return 3;
                }
                else if (start == Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
                {
                    return 4;
                }
                else if (start > end)
                {
                    return 5;
                }
                else
                {
                    return 6;
                }
            }//end of determine Scenario
            //first, monday
            int outcome= determineScenario(Convert.ToDateTime(HttpContext.Request.Form["m_start"]), Convert.ToDateTime(HttpContext.Request.Form["m_end"]));
            switch (outcome)
            {
                case 1://this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                    ViewData["Message"] = "Unless its 12AM for not available, start time cannot be end time. Error on monday.";
                    return View("Index");
                   // break;
                case 2:
                    //this means they are not available that day. 
                    ViewData["Message"] = "Not Available.";
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.MondayStart = theTime;
                    ViewBag.CurrentAvailability.MondayEnd = theTime;
                    break;
                case 3:
                    //they can close, but aren;t opening
                    ViewData["Message"] = "Close, not open.";
                    ViewBag.CurrentAvailability.MondayStart = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
                    theTime = Convert.ToDateTime("1/1/2001 23:00:00");
                    ViewBag.CurrentAvailability.MondayEnd = theTime;
                    break;
               case 4:
                    //this means they can open, but do not close
                    ViewData["Message"] = "Open, not close.";
                    ViewBag.CurrentAvailability.MondayEnd = Convert.ToDateTime(HttpContext.Request.Form["m_end"]);
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.MondayStart = theTime;
                    break;
                case 5:
                    //this means the start time is before the end time. This is bad data
                    ViewData["Message"] = "Start time cannot be before end time. Error on monday.";
                    return View("Index");
               break;
                case 6:
                    //this means they start and end at neither open or close
                    ViewData["Message"] = "Neither open or close on monday.";
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
                    ViewBag.CurrentAvailability.MondayStart = theTime;
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["m_end"]);
                    ViewBag.CurrentAvailability.MondayEnd = theTime;
                    break;

            }//end of switch
             //now tuesday
             // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_end"]);
            
           outcome = determineScenario(Convert.ToDateTime(HttpContext.Request.Form["tu_start"]), Convert.ToDateTime(HttpContext.Request.Form["tu_end"]));
           switch (outcome)
           {
               case 1://this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                   ViewData["Message"] = "Unless its 12AM for not available, start time cannot be end time. Error on tuesday.";
                   return View("Index");
               // break;
               case 2:
                   //this means they are not available that day. 
                   // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_start"]);
                   ViewData["Message"] = "Not available on tuesday)";
                   theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                   ViewBag.CurrentAvailability.TuesdayStart = theTime;
                   ViewBag.CurrentAvailability.TuesdayEnd = theTime;
                   break;
               case 3:
                   //they can close, but aren;t opening
                   ViewData["Message"] = "Close, not open.";
                   ViewBag.CurrentAvailability.TuesdayStart = Convert.ToDateTime(HttpContext.Request.Form["tu_start"]);
                   theTime = Convert.ToDateTime("1/1/2001 23:00:00");
                   ViewBag.CurrentAvailability.TuesdayEnd = theTime;
                   break;
               case 4:
                   //this means they can open, but do not close
                   ViewData["Message"] = "Open, not close.";
                   ViewBag.CurrentAvailability.TuesdayEnd = Convert.ToDateTime(HttpContext.Request.Form["tu_end"]);
                   theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                   ViewBag.CurrentAvailability.TuesdayStart = theTime;
                   break;
               case 5:
                   //this means the start time is before the end time. This is bad data
                   ViewData["Message"] = "Start time cannot be before end time. Error on Tuesday.";
                   return View("Index");
                   break;
               case 6:
                   //this means they start and end at neither open or close
                   ViewData["Message"] = "Neither open or close on tuesday";
                   theTime = Convert.ToDateTime(HttpContext.Request.Form["tu_start"]);
                   ViewBag.CurrentAvailability.TuesdayStart = theTime;
                   theTime = Convert.ToDateTime(HttpContext.Request.Form["tu_end"]);
                   ViewBag.CurrentAvailability.TuesdayEnd = theTime;
                   break;

           }//end of switch
            //now wednesday
            // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_end"]);

            outcome = determineScenario(Convert.ToDateTime(HttpContext.Request.Form["w_start"]), Convert.ToDateTime(HttpContext.Request.Form["w_end"]));
            switch (outcome)
            {
                case 1://this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                    ViewData["Message"] = "Unless its 12AM for not available, start time cannot be end time. Error on wed.";
                    return View("Index");
                // break;
                case 2:
                    //this means they are not available that day. 
                    // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_start"]);
                    ViewData["Message"] = "Not available on wednesday)";
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.WednesdayStart = theTime;
                    ViewBag.CurrentAvailability.WednesdayEnd = theTime;
                    break;
                case 3:
                    //they can close, but aren;t opening
                    ViewData["Message"] = "Close, not open.";
                    ViewBag.CurrentAvailability.WednesdayStart = Convert.ToDateTime(HttpContext.Request.Form["w_start"]);
                    theTime = Convert.ToDateTime("1/1/2001 23:00:00");
                    ViewBag.CurrentAvailability.WednesdayEnd = theTime;
                    break;
                case 4:
                    //this means they can open, but do not close
                    ViewData["Message"] = "Open, not close.";
                    ViewBag.CurrentAvailability.WednesdayEnd = Convert.ToDateTime(HttpContext.Request.Form["w_end"]);
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.WednesdayStart = theTime;
                    break;
                case 5:
                    //this means the start time is before the end time. This is bad data
                    ViewData["Message"] = "Start time cannot be before end time. Error on wednes.";
                    return View("Index");
                    break;
                case 6:
                    //this means they start and end at neither open or close
                    ViewData["Message"] = "Neither open or close on wednesday";
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["w_start"]);
                    ViewBag.CurrentAvailability.WednesdayStart = theTime;
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["w_end"]);
                    ViewBag.CurrentAvailability.WednesdayEnd = theTime;
                    break;

            }//end of switch

            //now Thursday
            // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_end"]);

            outcome = determineScenario(Convert.ToDateTime(HttpContext.Request.Form["th_start"]), Convert.ToDateTime(HttpContext.Request.Form["th_end"]));
            switch (outcome)
            {
                case 1://this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                    ViewData["Message"] = "Unless its 12AM for not available, start time cannot be end time. Error on thursday.";
                    return View("Index");
                // break;
                case 2:
                    //this means they are not available that day. 
                    // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_start"]);
                    ViewData["Message"] = "Not available on thursday)";
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.ThursdayStart = theTime;
                    ViewBag.CurrentAvailability.ThursdayEnd = theTime;
                    break;
                case 3:
                    //they can close, but aren;t opening
                    ViewData["Message"] = "Close, not open.";
                    ViewBag.CurrentAvailability.ThursdayStart = Convert.ToDateTime(HttpContext.Request.Form["th_start"]);
                    theTime = Convert.ToDateTime("1/1/2001 23:00:00");
                    ViewBag.CurrentAvailability.ThursdayEnd = theTime;
                    break;
                case 4:
                    //this means they can open, but do not close
                    ViewData["Message"] = "Open, not close.";
                    ViewBag.CurrentAvailability.ThursdayEnd = Convert.ToDateTime(HttpContext.Request.Form["th_end"]);
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.ThursdayStart = theTime;
                    break;
                case 5:
                    //this means the start time is before the end time. This is bad data
                    ViewData["Message"] = "Start time cannot be before end time. Error on thursday.";
                    return View("Index");
                    break;
                case 6:
                    //this means they start and end at neither open or close
                    ViewData["Message"] = "Neither open or close on thurs";
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["th_start"]);
                    ViewBag.CurrentAvailability.ThursdayStart = theTime;
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["th_end"]);
                    ViewBag.CurrentAvailability.ThursdayEnd = theTime;
                    break;

            }//end of switch
            //now friday
            // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_end"]);

            outcome = determineScenario(Convert.ToDateTime(HttpContext.Request.Form["f_start"]), Convert.ToDateTime(HttpContext.Request.Form["f_end"]));
            switch (outcome)
            {
                case 1://this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                    ViewData["Message"] = "Unless its 12AM for not available, start time cannot be end time. Error on fri.";
                    return View("Index");
                // break;
                case 2:
                    //this means they are not available that day. 
                    // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_start"]);
                    ViewData["Message"] = "Not available on fri)";
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.FridayStart = theTime;
                    ViewBag.CurrentAvailability.FridayEnd = theTime;
                    break;
                case 3:
                    //they can close, but aren;t opening
                    ViewData["Message"] = "Close, not open.";
                    ViewBag.CurrentAvailability.FridayStart = Convert.ToDateTime(HttpContext.Request.Form["f_start"]);
                    theTime = Convert.ToDateTime("1/1/2001 23:00:00");
                    ViewBag.CurrentAvailability.FridayEnd = theTime;
                    break;
                case 4:
                    //this means they can open, but do not close
                    ViewData["Message"] = "Open, not close.";
                    ViewBag.CurrentAvailability.FridayEnd = Convert.ToDateTime(HttpContext.Request.Form["f_end"]);
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.FridayStart = theTime;
                    break;
                case 5:
                    //this means the start time is before the end time. This is bad data
                    ViewData["Message"] = "Start time cannot be before end time. Error on friday.";
                    return View("Index");
                    break;
                case 6:
                    //this means they start and end at neither open or close
                    ViewData["Message"] = "Neither open or close on fri";
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["f_start"]);
                    ViewBag.CurrentAvailability.FridayStart = theTime;
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["f_end"]);
                    ViewBag.CurrentAvailability.FridayEnd = theTime;
                    break;

            }//end of switch
            //now saturday
            // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_end"]);

            outcome = determineScenario(Convert.ToDateTime(HttpContext.Request.Form["sa_start"]), Convert.ToDateTime(HttpContext.Request.Form["sa_end"]));
            switch (outcome)
            {
                case 1://this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                    ViewData["Message"] = "Unless its 12AM for not available, start time cannot be end time. Error on sat.";
                    return View("Index");
                // break;
                case 2:
                    //this means they are not available that day. 
                    // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_start"]);
                    ViewData["Message"] = "Not available on sat)";
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.SaturdayStart = theTime;
                    ViewBag.CurrentAvailability.SaturdayEnd = theTime;
                    break;
                case 3:
                    //they can close, but aren;t opening
                    ViewData["Message"] = "Close, not open.";
                    ViewBag.CurrentAvailability.SaturdayStart = Convert.ToDateTime(HttpContext.Request.Form["sa_start"]);
                    theTime = Convert.ToDateTime("1/1/2001 23:00:00");
                    ViewBag.CurrentAvailability.SaturdayEnd = theTime;
                    break;
                case 4:
                    //this means they can open, but do not close
                    ViewData["Message"] = "Open, not close.";
                    ViewBag.CurrentAvailability.SaturdayEnd = Convert.ToDateTime(HttpContext.Request.Form["sa_end"]);
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.SaturdayStart = theTime;
                    break;
                case 5:
                    //this means the start time is before the end time. This is bad data
                    ViewData["Message"] = "Start time cannot be before end time. Error on saturday.";
                    return View("Index");
                    break;
                case 6:
                    //this means they start and end at neither open or close
                    ViewData["Message"] = "Neither open or close on sat";
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["sa_start"]);
                    ViewBag.CurrentAvailability.SaturdayStart = theTime;
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["sa_end"]);
                    ViewBag.CurrentAvailability.SaturdayEnd = theTime;
                    break;

            }//end of switch
             //now sunday
             // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_end"]);

            outcome = determineScenario(Convert.ToDateTime(HttpContext.Request.Form["su_start"]), Convert.ToDateTime(HttpContext.Request.Form["su_end"]));
            switch (outcome)
            {
                case 1://this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                    ViewData["Message"] = "Unless its 12AM for not available, start time cannot be end time. Error on sun.";
                    return View("Index");
                // break;
                case 2:
                    //this means they are not available that day. 
                    // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["t_start"]);
                    ViewData["Message"] = "Not available on sun)";
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.SundayStart = theTime;
                    ViewBag.CurrentAvailability.SundayEnd = theTime;
                    break;
                case 3:
                    //they can close, but aren;t opening
                    ViewData["Message"] = "Close, not open.";
                    ViewBag.CurrentAvailability.SundayStart = Convert.ToDateTime(HttpContext.Request.Form["su_start"]);
                    theTime = Convert.ToDateTime("1/1/2001 23:00:00");
                    ViewBag.CurrentAvailability.SundayEnd = theTime;
                    break;
                case 4:
                    //this means they can open, but do not close
                    ViewData["Message"] = "Open, not close.";
                    ViewBag.CurrentAvailability.SundayEnd = Convert.ToDateTime(HttpContext.Request.Form["su_end"]);
                    theTime = Convert.ToDateTime("1/1/2001 00:00:00");
                    ViewBag.CurrentAvailability.SundayStart = theTime;
                    break;
                case 5:
                    //this means the start time is before the end time. This is bad data
                    ViewData["Message"] = "Start time cannot be before end time. Error on sunday.";
                    return View("Index");
                    break;
                case 6:
                    //this means they start and end at neither open or close
                    ViewData["Message"] = "Neither open or close on sun";
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["su_start"]);
                    ViewBag.CurrentAvailability.SundayStart = theTime;
                    theTime = Convert.ToDateTime(HttpContext.Request.Form["su_end"]);
                    ViewBag.CurrentAvailability.SundayEnd = theTime;
                    break;

            }//end of switch






            /*
            if (timeStart == timeEnd && timeEnd!= Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {//this means they entered the same time for entering and leaving (and didn't click "not available") (this is bad data)
                ViewData["Message"] = "Unless its 12AM for not available, start time cannot be end time. Error on monday.";
                return View("Index");
            }  
           else if (Convert.ToDateTime(HttpContext.Request.Form["m_start"]) == Convert.ToDateTime(HttpContext.Request.Form["m_end"]) && Convert.ToDateTime(HttpContext.Request.Form["m_end"]) == Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {//this means they are not available that day. 
                ViewData["Message"] = "Not Available.";
                theTime = Convert.ToDateTime("1/13/2001 00:00:00");
                ViewBag.CurrentAvailability.MondayStart = theTime;
                ViewBag.CurrentAvailability.MondayEnd = theTime;
            }
            else if(Convert.ToDateTime(HttpContext.Request.Form["m_end"]) == Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {//this means they can close, but do not open
                ViewData["Message"] = "Close, not open.";
                ViewBag.CurrentAvailability.MondayStart = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
                theTime = Convert.ToDateTime("1/13/2001 23:00:00");
                ViewBag.CurrentAvailability.MondayEnd = theTime;
            }
            else if (Convert.ToDateTime(HttpContext.Request.Form["m_start"]) == Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {//this means they can open, but do not close
                ViewData["Message"] = "Open, not close.";
                ViewBag.CurrentAvailability.MondayEnd = Convert.ToDateTime(HttpContext.Request.Form["m_end"]);
                theTime = Convert.ToDateTime("1/13/2001 00:00:00");
                ViewBag.CurrentAvailability.MondayStart = theTime;
            }
            else if (timeStart > timeEnd)
            {//this means the start time is before the end time. This is bad data
                ViewData["Message"] = "Start time cannot be before end time. Error on monday.";
                return View("Index");
            }
            else
            {//this means they start and end at neither open or close
                ViewData["Message"] = "Neither open or close.";
                theTime = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
                ViewBag.CurrentAvailability.MondayStart = theTime;
               theTime= Convert.ToDateTime(HttpContext.Request.Form["m_end"]);
                ViewBag.CurrentAvailability.MondayEnd = theTime;
            }
            */



            // theTime = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);


            // ViewBag.CurrentAvailability.MondayEnd = theTime;




            //  Availability theAvail = new(loggedInEmployee);
            //ViewBag.CurrentAvailability.MondayEnd = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);
            //  theAvail.MondayEnd = Convert.ToDateTime(HttpContext.Request.Form["m_start"]);

            //theAvail.Save();


            /*
             *     theTime = Convert.ToDateTime("1/13/2001 06:00:00");
            ViewBag.CurrentAvailability.TuesdayStart = theTime;
            theTime = Convert.ToDateTime("1/13/2001 18:00:00");
            ViewBag.CurrentAvailability.TuesdayEnd = theTime;
            ViewBag.CurrentAvailability.Save();

             //This works
            theTime = Convert.ToDateTime(HttpContext.Request.Form["su_end"]);
            if(theTime== Convert.ToDateTime(HttpContext.Request.Form["Fake"]))
            {
                theTime = Convert.ToDateTime("1/13/2001 00:00:00");
            }

            ViewBag.CurrentAvailability.SundayEnd = theTime;


             * ViewData["Message"] = ViewBag.CurrentAvailability.SundayEnd;
             *  ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["su_end"]);
            if(Convert.ToDateTime(HttpContext.Request.Form["fake"])==Convert.ToDateTime(HttpContext.Request.Form["su_end"])){
                 ViewData["Message"] = "True";
            }
            else
            {
                ViewData["Message"] = "False!";
            }
            //  ViewBag.CurrentAvailability =theAvail;

            // ViewData["Message"] = "Update Complete!";

            
            if (HttpContext.Request.Form["m_op"]==true)
            {
                ViewData["Message"] = "mondya open is checked !";
            }
            else
            {
                ViewData["Message"] = "monday open isn't checked";
            }
            
            if(Convert.ToDateTime(HttpContext.Request.Form["m_start"])> Convert.ToDateTime(HttpContext.Request.Form["m_end"]))
            {
                ViewData["Message"] = "Start is after close!";
            }
            else if(Convert.ToDateTime(HttpContext.Request.Form["m_start"]) < Convert.ToDateTime(HttpContext.Request.Form["m_end"]))
            {
                ViewData["Message"] = "Start is before close";
            }
            else {
                ViewData["Message"] = "Equal";
            }
            

            //The problem is that I can't read from the view
            ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["m_end"]); 
            if (Convert.ToDateTime(HttpContext.Request.Form["m_end"])== Convert.ToDateTime(HttpContext.Request.Form["m_start"]))
            {
                ViewData["Message"] = "Same";
            }
           // ViewData["Message"] = Convert.ToDateTime(HttpContext.Request.Form["m_end"]); 
            // ViewData["Message"] = theAvail.MondayEnd.ToString();
            */
            //if we reach here we are good
            ViewBag.CurrentAvailability.Save();
            return View("Index");
        }
      




    }
}
