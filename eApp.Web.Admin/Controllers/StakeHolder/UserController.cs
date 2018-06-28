using eApp.Web.Admin.ADO;
using eApp.Web.Admin.Models;
using eApp.Web.Admin.Resources.LibClass;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eApp.Web.Admin.Controllers.StakeHolder
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {

        #region VIEW

        public ActionResult UserList()
        {
            return View();
        }

        public ActionResult NewUser()
        {
            return View();
        }
        
        public ActionResult _tempAddRole(string userid)
        {
            ViewBag.userid = userid;

            return View();
        }

        public ActionResult _tempAddBranch(string userid)
        {
            ViewBag.userid = userid;

            return View();
        }



        #endregion

        #region JSON

        [HttpGet]
        public JsonResult GetUserCombiAll()
        {
            var db = new dbsmappEntities();

            var QQList = db.userapps
               .Select(m => new rUser
               {
                   userid = m.userid,
                   name = m.name,
                   empid = m.empid,
                   role = m.role,
                   status = m.status,
                   password = m.password

               }).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetUserCombiAllRole(string userid)
        {
            var db = new dbsmappEntities();

            var QQList = (from s in db.userapps
                          join cs in db.userapproles on s.userid equals cs.userid
                          where cs.userid.Equals(userid)
                          select new xUser
                          {
                              userid = s.userid,
                              role = cs.role,
                              remarks = cs.remarks,
                              status = cs.status,

                          }).ToList();
            
            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetUserCombiAllBranch(string userid)
        {
            var db = new dbsmappEntities();

            var QQList = (from s in db.userapps
                          join cs in db.userappbranches on s.userid equals cs.userid
                          join css in db.xbranches on cs.branchcode equals css.branchcode
                          where cs.userid.Equals(userid)
                          select new bUser
                          {
                              userid = s.userid,
                              branchcode = cs.branchcode,
                              branchname = css.branchname,
                              remarks = cs.remarks,
                              status = cs.status,

                          }).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetUserCombiAllBranchAvailable(string userid)
        {
            var db = new dbsmappEntities();

            var QQListAvailable = db.userappbranches.Where(s => s.userid.Equals(userid)).ToList();
            var QQListNotAvailable = db.xbranches.ToList();

            var QQList = QQListNotAvailable.Where(i => !QQListAvailable.Any(e => i.branchcode.Equals(e.branchcode))).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region POST

        #region Account Setup

        private ApplicationUserManager _userManager;

        UserManager<User> userManager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));

        private User FindUser()
        {
            return userManager.FindById(User.Identity.GetUserId());
        }

        public UserController() { }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        #endregion

        [HttpPost]
        public async Task<bool> UserSubmit(rUser model)
        {
            NullFiller.FillNullFields<rUser>(model);

            Capitalize.UppercaseClassFields<rUser>(model);

            var db = new dbsmappEntities();

            var isExist = db.userapps.FirstOrDefault(s => s.userid.Equals(model.userid));

            if (isExist != null)
            {
                return false;
            }

            var emailAdd = model.userid + "@danubeco.com";
            var newuserid = model.userid + "JED";

            var user = new User { UserName = newuserid, Email = emailAdd, Region = "JED" };

            //var rnpw = System.Web.Security.Membership.GeneratePassword(6, 0);

            var result = await UserManager.CreateAsync(user, "12345678");

            if (!result.Succeeded)
            {
                return false;
            }
           
            var currentUser = await UserManager.FindByEmailAsync(emailAdd);

            await UserManager.AddToRolesAsync(currentUser.Id, model.role);

            db.userapps.Add(new userapp()
            {
                userid = newuserid,
                empid = model.userid,
                name = model.name,
                role = model.role,
                status = "activated",
                login = DateTime.Today,
                password = "12345678"

            });


            var files = Request.Files;

            if (files.Count != 0)
            {
                new ImageFunc().UploadProductPic(Request.Files, newuserid);
            }

            db.SaveChanges();

            return true;
        }

        [HttpPost]
        public bool UserRoleSubmit(rStaffRole model)
        {
            var db = new dbsmappEntities();

            var account = db.userapps.FirstOrDefault(s => s.userid.Equals(model.userid));

            var currentUser = db.C_User.FirstOrDefault(s => s.UserName.Equals(account.userid));

            var isExist = db.userapproles.FirstOrDefault(s => s.userid.Equals(model.userid) && s.role.Equals(model.role));

            if (isExist != null)
            {
                return false;
            }
            
            db.userapproles.Add(new userapprole()
            {
                userid = model.userid,
                role = model.role,
                remarks = "",
                status = "activated"
            });

            db.SaveChanges();

            return true;
        }
        
        [HttpPost]
        public bool UserBranchSubmit(rStaffBranch model)
        {
            var db = new dbsmappEntities();
            
            var isExist = db.userappbranches.FirstOrDefault(s => s.userid.Equals(model.userid) && s.branchcode.Equals(model.branchcode));

            if (isExist != null)
            {
                return false;
            }
            else
            {
                db.userappbranches.Add(new userappbranch
                {
                    branchcode = model.branchcode,
                    userid = model.userid,
                    remarks = "",
                    status = "ACTIVATED"
                });

                db.SaveChanges();

                return true;

            }

        }
        
        [HttpPost]
        public bool UserRemoveBranch(rStaffBranch model)
        {
            var db = new dbsmappEntities();

            var isExist = db.userappbranches.FirstOrDefault(s => s.userid.Equals(model.userid) && s.branchcode.Equals(model.branchcode));

            if (isExist == null)
            {
                return false;
            }
            else
            {
                db.userappbranches.Remove(isExist);

                db.SaveChanges();

                return true;

            }

        }
        
        [HttpPost]
        public async Task<bool> UserRemoveRole(rStaffRole model)
        {
            var db = new dbsmappEntities();

            var isExist = db.userapproles.FirstOrDefault(s => s.userid.Equals(model.userid) && s.role.Equals(model.role));

            if (isExist == null)
            {
                return false;
            }
            else
            {

                var currentUser = db.C_User.FirstOrDefault(s => s.UserName.Equals(model.userid));
                
                await UserManager.RemoveFromRoleAsync(currentUser.UserId, model.role);
                
                db.userapproles.Remove(isExist);

                db.SaveChanges();

                return true;

            }

        }

        #endregion
    }
}


public partial class rStaffRole
{
    public string userid { get; set; }
    public string role { get; set; }
}

public partial class rStaffBranch
{
    public string userid { get; set; }
    public string branchcode { get; set; }
}

public partial class rUser
{
    public string userid { get; set; }
    public string name { get; set; }    
    public string role { get; set; }
    public string empid { get; set; }
    public string status { get; set; }
    public string password { get; set; }
    
}

public partial class xUser
{
    public string userid { get; set; }
    public string role { get; set; }
    public string remarks { get; set; }
    public string status { get; set; }
}

public partial class bUser
{
    public string userid { get; set; }
    public string branchcode { get; set; }
    public string branchname { get; set; }
    public string remarks { get; set; }
    public string status { get; set; }
}