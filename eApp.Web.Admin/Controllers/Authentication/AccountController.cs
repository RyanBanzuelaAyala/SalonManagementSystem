using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using eApp.Web.Admin.Models;

using System.Collections.Generic;
using eApp.Web.Admin.ADO;
using eApp.Web.Admin.Resources.LibClass;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eApp.Web.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {        
        
        #region Basic Setup 

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

        #region Account Views

        [AllowAnonymous]
        public ActionResult Login()
        {                        
            return View();            
        }
        
        public ActionResult SelectedCurrentRole()
        {
            var db = new dbsmappEntities();

            var userid = User.Identity.GetUserName();

            ViewBag.Role = db.userapproles.Where(s => s.userid.Equals(userid)).ToList();

            var brn = db.userappbranches.Where(s => s.userid.Equals(userid)).ToList();

            if(brn.Count.Equals(0))
            {
                ViewBag.Branch = null;
            }
            else
            {
                ViewBag.Branch = brn;
            }            

            return View();
        }
        
        public async Task<ActionResult> LogOff()
        {
            var db = new dbsmappEntities();

            var username = User.Identity.GetUserName();

            var userid = User.Identity.GetUserId();

            var isExist = db.userappsessions.FirstOrDefault(s => s.userid.Equals(username));

            if (isExist != null)
            {
                await UserManager.RemoveFromRoleAsync(userid, isExist.role);

                db.userappsessions.Remove(isExist);

                db.SaveChanges();
            }                

            SignInManager.AuthenticationManager.SignOut();

            return View();
        }

        public ActionResult SessionExpired()
        {
            SignInManager.AuthenticationManager.SignOut();

            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        #endregion

        #region POST
                
        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Login(nLoginViewModel model)
        {
            try
            {

                var newusername = model.Username + "JED";

                var result = await SignInManager.PasswordSignInAsync(newusername, model.Password, false, false);

                switch(result)
                {
                    case SignInStatus.Success:
                        
                        var db = new dbsmappEntities();

                        var getSTatus = db.userapps.FirstOrDefault(s => s.userid.Equals(newusername)).status;

                        if(getSTatus.Equals("deactivated"))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }                            
                      

                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return false;
                }


            }
            catch(Exception)
            {
                return false;
            }

        }
        
        [HttpPost]
        public async Task<bool> LoginRole(xxRB rb)
        {
            var db = new dbsmappEntities();

            var username = User.Identity.GetUserName();
            var userid = User.Identity.GetUserId();

            var isExist = db.userappsessions.FirstOrDefault(s => s.userid.Equals(username));

            if (isExist != null)
            {
                db.userappsessions.Remove(isExist);
                
                await UserManager.RemoveFromRoleAsync(userid, isExist.role);
            }
            
            // STEP 1
            await UserManager.AddToRolesAsync(userid, rb.role);

            db.userappsessions.Add(new userappsession
            {
                userid = username,
                role = rb.role,
                status = (rb.branch == null) ? "000" : rb.branch
            });
                
            db.SaveChanges();
                
            // STEP 2

            SignInManager.AuthenticationManager.SignOut();

            var cpass = db.userapps.FirstOrDefault(s => s.userid.Equals(username)).password;  
            var result = await SignInManager.PasswordSignInAsync(username, cpass, false, false);

            switch (result)
            {
                case SignInStatus.Success:
                    return true;

                default:
                    return false;
            }    
            
        }

        [HttpPost]
        public async Task<bool> ResetPassword(nResetPasswordViewModel model)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if(user == null)
                {
                    return false;
                }

                user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.nPassword);

                var result = await UserManager.UpdateAsync(user);

                if(!result.Succeeded)
                {
                    return false;
                }
                else
                {
                    var db = new dbsmappEntities();

                    var isPerson = db.userapps.FirstOrDefault(s => s.userid.Equals(user.UserName));

                    isPerson.password = model.nPassword;

                    db.SaveChanges();

                    return true;
                }

            }
            catch(Exception)
            {
                return false;
            }

        }
        
        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if(_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }


        //[AllowAnonymous]
        //public async Task<string> AddAgent()
        //{

        //    var user = new User { UserName = "devqqJED", Email = "ryan" + "@devmc.com", Region = "JED" };

        //    var result = await UserManager.CreateAsync(user, "qwe123QQ@@");

        //    if (!result.Succeeded)
        //    {
        //        return "mali";
        //    }

        //    var currentUser = await UserManager.FindByEmailAsync("ryan@devmc.com");

        //    await UserManager.AddToRolesAsync(currentUser.Id, "Administrator");

        //    try
        //    {
        //        var db = new dbsmappEntities();

        //        db.userapps.Add(new userapp()
        //        {
        //            userid = "devqqJED",
        //            name = "Ryan Ayala",
        //            empid = "000000",
        //            role = "Administrator",
        //            status = "activated",
        //            login = DateTime.Today,
        //            password = "qwe123QQ@@"

        //        });

        //        db.SaveChanges();

        //        return "ok";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }

        //}

        #endregion

    }
}


public partial class xxRB
{
    public string role { get; set; }

    public string branch { get; set; }
}