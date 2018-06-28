using eApp.Web.Admin.ADO;
using eApp.Web.Admin.Resources.LibClass;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eApp.Web.Admin.Controllers.Backend.xBranch
{
    [Authorize]
    public class iBranchController : Controller
    {
        #region VIEWS

        public ActionResult BranchOrderList()
        {
            return View();
        }

        public ActionResult NewBranchOrder()    
        {
            var db = new dbsmappEntities();

            ViewBag.RequestVeh = db.nservices.ToList();

            return View();
        }

        public ActionResult _tempAddBranchOrder(string ordercode)
        {
            ViewBag.ordercode = ordercode;

            return View();
        }

        public ActionResult _tempAddProductOrder(string branchcode)
        {
            ViewBag.branchcode = branchcode;

            return View();
        }


        #endregion
        
        #region POST

        public bool AddBranchOrder(xbc branch)
        {
            var db = new dbsmappEntities();

            var newGeneratedId = new NewId()._GenerateId("xbranchorders");

            var NewIDD = "B00" + newGeneratedId + "H" + DateTime.Today.ToString("yyyy");

            var username = User.Identity.GetUserName();
            
            db.xbranchorders.Add(new xbranchorder
            {
                branchcode = branch.brn,
                servicecode = branch.servicecode,
                ordercode = NewIDD,
                orderby = username,
                datecreated = DateTime.Now,
                approvedby = "",
                dateapproved = DateTime.Now,
                remarks = branch.remarks,
                status = "blank",
            });           

            db.SaveChanges();

            return true;
        }

        public bool UpdateBranchOrder(string ordercode)
        {
            var db = new dbsmappEntities();

            var serv = db.xbranchorders.FirstOrDefault(s => s.ordercode.Equals(ordercode));

            if (serv != null)
            {
                serv.status = "pending";
               
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteProductToOrder(string itemcode, string ordercode)
        {
            var db = new dbsmappEntities();

            var prd = db.xbranchorderslists.FirstOrDefault(s => s.itemcode.Equals(itemcode) && s.ordercode.Equals(ordercode));

            if (prd != null)
            {
                db.xbranchorderslists.Remove(prd);

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;

            }

        }

        public bool AddProductToOrder(string itemcode, string ordercode, string quantity)
        {
            var db = new dbsmappEntities();

            var serv = db.xbranchorderslists.FirstOrDefault(s => s.itemcode.Equals(itemcode) && s.ordercode.Equals(ordercode));

            if (serv == null)
            {
                db.xbranchorderslists.Add(new xbranchorderslist
                {
                    ordercode = ordercode,
                    itemcode = itemcode,
                    quantity = quantity,
                    status = "ordered"
                });

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
        public JsonResult GetCombiAllBranchOrders(string brn)
        {
            var db = new dbsmappEntities();
            
            var listQQ = db.xbranchorders.Where(s => s.branchcode.Equals(brn)).ToList();

            return Json(listQQ, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetCombiAllBranchOrderList(string ordercode)
        {
            var db = new dbsmappEntities();
            

            var QQList = (from s in db.xbranchorderslists
                          join cs in db.iproducts on s.itemcode equals cs.itemcode
                          where s.ordercode.Equals(ordercode)
                          select new xbcItem
                          {
                              itemcode = s.itemcode,
                              barcode = cs.barcode,
                              itemname = cs.arname + " - " + cs.enname,
                              quantity = cs.enname,
                              price = db.iproductprices.FirstOrDefault(x => x.itemcode.Equals(s.itemcode)).sellingprice

                          }).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetProductOrderCombiAllAvailable(string ordercode)
        {
            var db = new dbsmappEntities();

            var QQListAvailable = db.xbranchorderslists.Where(s => s.ordercode.Equals(ordercode)).ToList();
            var QQListNotAvailable = db.iproducts.ToList();

            var QQList = QQListNotAvailable.Where(i => !QQListAvailable.Any(e => i.itemcode.Equals(e.itemcode))).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        #endregion

        // ------- FOR ADMIN

        #region VIEW

        
        [Authorize(Roles="Administrator")]
        public ActionResult BranchOrderListAll()
        {
            return View();
        }

        #endregion


        #region JSON

        [HttpGet]
        public JsonResult GetCombiAllBranchOrdersOA()
        {
            var db = new dbsmappEntities();

            var listQQ = db.xbranchorders.Where(s => s.status.Equals("pending") || s.status.Equals("approved")).ToList();

            return Json(listQQ, JsonRequestBehavior.AllowGet);

        }
        

        #endregion

    }
}

public partial class xbc
{
    public string brn { get; set; }
    public string servicecode { get; set; }
    public string remarks { get; set; }

}


public partial class xbcItem
{
    public string itemcode { get; set; }
    public string barcode { get; set; }
    public string itemname { get; set; }
    public string quantity { get; set; }
    public string price { get; set; }

}