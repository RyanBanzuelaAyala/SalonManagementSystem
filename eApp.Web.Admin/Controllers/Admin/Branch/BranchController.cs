using eApp.Web.Admin.ADO;
using eApp.Web.Admin.Resources.LibClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eApp.Web.Admin.Controllers.Admin.Branch
{
    [Authorize(Roles = "Administrator")]
    public class BranchController : Controller
    {
        #region VIEW

        public ActionResult BranchList()
        {
            return View();
        }

        public ActionResult NewBranch()
        {
            return View();
        }

        public ActionResult EditBranch(string branchcode)
        {
            var db = new dbsmappEntities();

            var isBranch = db.xbranches.FirstOrDefault(s => s.branchcode.Equals(branchcode));

            if (isBranch != null)
            {
                ViewBag.Branch = isBranch;
            }
            else
            {
                ViewBag.Branch = null;
            }

            return View();
        }
        
        #endregion

        #region POST

        public bool AddBranch(xbranch branch)
        {
            var db = new dbsmappEntities();

            var brn = db.xbranches.FirstOrDefault(s => s.branchcode.Equals(branch.branchcode));

            if (brn == null)
            {

                NullFiller.FillNullFields<xbranch>(branch);
                Capitalize.UppercaseClassFields<xbranch>(branch);

                db.xbranches.Add(branch);

            }
            else
            {
                NullFiller.FillNullFields<xbranch>(branch);
                Capitalize.UppercaseClassFields<xbranch>(branch);

                brn.branchname = branch.branchname;
                brn.remarks = branch.remarks;

            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteBranch(string branchcode)
        {
            var db = new dbsmappEntities();

            var brn = db.xbranches.FirstOrDefault(s => s.branchcode.Equals(branchcode));

            if (brn != null)
            {               
                db.xbranches.Remove(brn);

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;

            }

        }


        #endregion

        #region JSON

        [HttpGet]
        public JsonResult GetCombiAllBranch()
        {
            var db = new dbsmappEntities();
            
            return Json(db.xbranches.ToList(), JsonRequestBehavior.AllowGet);

        }
        
        #endregion
    }
}