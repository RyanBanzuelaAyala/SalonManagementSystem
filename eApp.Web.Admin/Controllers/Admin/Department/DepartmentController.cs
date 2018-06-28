using eApp.Web.Admin.ADO;
using eApp.Web.Admin.Resources.LibClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eApp.Web.Admin.Controllers.Admin.Department
{
    [Authorize(Roles = "Administrator")]
    public class DepartmentController : Controller
    {
        #region VIEW

        public ActionResult NewDepartmentList()
        {
            return View();
        }

        public ActionResult NewDepartment()
        {
            return View();
        }

        public ActionResult EditDepartment(string deptcode)
        {
            var db = new dbsmappEntities();

            var isDepartment = db.sdepartments.FirstOrDefault(s => s.deptcode.Equals(deptcode));

            if (isDepartment != null)
            {
                ViewBag.Department = isDepartment;                
            }
            else
            {
                ViewBag.Department = null;                
            }

            return View();
        }
        
        public ActionResult _tempAddService(string deptcode)
        {
            ViewBag.DeptCode = deptcode;

            return View();
        }

        #endregion

        #region JSON

        [HttpGet]
        public JsonResult GetDepartmentCombiAll()
        {
            var db = new dbsmappEntities();

            var QQList = db.sdepartments.ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetDepartmentCombiAllServices(string deptcode)
        {
            var db = new dbsmappEntities();

            var QQList = (from s in db.sdepartmentservices
                          join cs in db.nservices on s.servicecode equals cs.servicescode
                          where s.deptcode.Equals(deptcode)
                          select new sdepartmentserviceX
                          {
                              servicecode = s.servicecode,
                              servicename = cs.servicename,
                              status = s.status

                          }).ToList();
            
            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetDepartmentCombiAllServicesAvailable(string deptcode)
        {
            var db = new dbsmappEntities();
            
            var QQListAvailable = db.sdepartmentservices.Where(s => s.deptcode.Equals(deptcode)).ToList();
            var QQListNotAvailable = db.nservices.ToList();

            var QQList = QQListNotAvailable.Where(i => !QQListAvailable.Any(e => i.servicescode.Equals(e.servicecode))).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        #endregion  

        #region POST

        public bool AddDepartment(sdepartment Department)
        {
            var db = new dbsmappEntities();

            var dept = db.sdepartments.FirstOrDefault(s => s.deptcode.Equals(Department.deptcode));

            if (dept == null)
            {
                Department.status = "activated";

                NullFiller.FillNullFields<sdepartment>(Department);
                Capitalize.UppercaseClassFields<sdepartment>(Department);

                db.sdepartments.Add(Department);

            }
            else
            {
                NullFiller.FillNullFields<sdepartment>(Department);
                Capitalize.UppercaseClassFields<sdepartment>(Department);

                dept.deptcode = Department.deptcode;
                dept.deptname = Department.deptname;
                dept.remarks = Department.remarks;
                dept.status = "ACTIVATED";

            }
            
            db.SaveChanges();

            return true;
        }

        public bool AddDepartmentServices(string ServCode, string DeptCode)
        {
            var db = new dbsmappEntities();

            var sdept = db.sdepartmentservices.FirstOrDefault(s => s.deptcode.Equals(DeptCode) && s.servicecode.Equals(ServCode));

            if (sdept == null)
            {               
                db.sdepartmentservices.Add(new sdepartmentservice
                {
                    deptcode = DeptCode,
                    servicecode = ServCode,
                    remarks = "",
                    status = "ACTIVATED"
                });

                db.SaveChanges();

                return true;

            }
            else
            {
                return false;
            }

        }
        
        public bool RemoveDepartmentServices(string ServCode, string DeptCode)
        {
            var db = new dbsmappEntities();

            var sdept = db.sdepartmentservices.FirstOrDefault(s => s.deptcode.Equals(DeptCode) && s.servicecode.Equals(ServCode));

            if (sdept != null)
            {
                db.sdepartmentservices.Remove(sdept);

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool RemoveDepartment(string DeptCode)
        {
            var db = new dbsmappEntities();

            var sdept = db.sdepartments.FirstOrDefault(s => s.deptcode.Equals(DeptCode));

            if (sdept != null)
            {
                var sdepts = db.sdepartmentservices.Where(s => s.deptcode.Equals(DeptCode)).ToList();

                foreach (var item in sdepts)
                {
                    db.sdepartmentservices.Remove(item);
                }

                db.sdepartments.Remove(sdept);

                db.SaveChanges();

                return true;
            }
            else
            {

                return false;
            }

            
        }
        #endregion
    }
}

public partial class sdepartmentserviceX
{
    public string servicecode { get; set; }
    public string servicename { get; set; }
    public string status { get; set; }
}