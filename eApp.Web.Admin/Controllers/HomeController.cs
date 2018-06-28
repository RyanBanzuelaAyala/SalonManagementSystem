
using eApp.Web.Admin.ADO;
using eApp.Web.Admin.Models;
using eApp.Web.Admin.Resources.LibClass;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eApp.Web.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        #region Setup
        
        dbsmappEntities db = new dbsmappEntities();

        UserManager<User> userManager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));

        private User FindUser()
        {
            return userManager.FindById(User.Identity.GetUserId());
        }

        private string getRegion()
        {
            var lRegg = User.Identity.Name.Length - 3;

            return User.Identity.Name.Substring(lRegg, 3);

        }
    
        private string getSelectedRole()
        {
            var username = User.Identity.GetUserName();

            return db.userappsessions.FirstOrDefault(s => s.userid.Equals(username)).role;
        }

        #endregion

        #region Process

        public ActionResult Index()
        {            
            var user = FindUser();
            
            if (user == null)
                return Redirect("Account/Login");

            if(!UserSession.hasSessionRole(user.UserName))
                return Redirect("Account/LogOff");

            var userRole = getSelectedRole();

            ViewBag.Role = userRole;
            
            ViewBag.UserInfo = db.userapps.FirstOrDefault(s => s.userid.Equals(user.UserName));

            var isExBR = db.userappsessions.FirstOrDefault(s => s.userid.Equals(user.UserName));
            
            ViewBag.UserBranch = db.xbranches.FirstOrDefault(s => s.branchcode.Equals(isExBR.status)).branchname;

            ViewBag.UserBranchCode = isExBR.status;

            return View();

        }

        public ActionResult Startup()
        {
            var user = FindUser();
            
            ViewBag.User = user;
            
            var userRole = user.Roles.FirstOrDefault().RoleId;

            ViewBag.Role = userRole;

            if (userRole == "1")
            {
                return RedirectToAction("RemindUser", "Reminder");
            }
            else
            {
                return RedirectToAction("RemindUser", "Reminder");
            }
            
        }
        
        #endregion

        #region Calendar

        public ActionResult GetEvents()
        {
            var eventList = GetEventsX();

            return Json(eventList.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private List<Events> GetEventsX()
        {
            List<Events> eventList = new List<Events>();

            Events newEvent = new Events
            {
                id = "1",
                title = "Event 1",
                desc = "Event 1 desc",
                start = DateTime.Now.AddDays(1).ToString("s"),
                end = DateTime.Now.AddDays(1).ToString("s"),
                color = "green",
                allDay = false
            };

            eventList.Add(newEvent);

            newEvent = new Events
            {
                id = "1",
                title = "Event 3",
                desc = "Event 3 desc",
                start = DateTime.Now.AddDays(2).ToString("s"),
                end = DateTime.Now.AddDays(3).ToString("s"),
                color = "red",
                allDay = false
            };

            eventList.Add(newEvent);

            return eventList;
        }

        #endregion

    }
}

public partial class Events
{
    public string id { get; set; }
    public string title { get; set; }
    public string desc { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public string color { get; set; }
    public bool allDay { get; set; }
}

public partial class vehicleOAx
{
    //  Vehicle
    public string vehicleid { get; set; }
    public string vehicletype { get; set; }
    public string fleetnumber { get; set; }    
    public string platenumberen { get; set; }
    public string platenumberar { get; set; }
    public string plate { get; set; }
    public string vname { get; set; }
    public string vregion { get; set; }
    public string vgroup { get; set; }
    
    // specification    
    public string vcondition { get; set; }
    public string vstatus { get; set; }

    // Registration 
    public string RegExpiration { get; set; }

    // Inspection
    public string InsExpiration { get; set; }

}