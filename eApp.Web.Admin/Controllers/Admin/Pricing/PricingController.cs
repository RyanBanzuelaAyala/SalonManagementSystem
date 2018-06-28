using eApp.Web.Admin.ADO;
using eApp.Web.Admin.Resources.LibClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eApp.Web.Admin.Controllers.Admin.Pricing
{
    [Authorize(Roles = "Administrator")]
    public class PricingController : Controller
    {
        #region VIEW

        public ActionResult NewProductPricing()
        {
            return View();
        }

        public ActionResult NewPricing(string itemcode)
        {
            var db = new dbsmappEntities();

            var iproduct = db.iproducts.FirstOrDefault(s => s.itemcode.Equals(itemcode));

            if (iproduct != null)
            {
                ViewBag.Product = iproduct;

                var isImg = db.ysysphotoes.FirstOrDefault(s => s.imgcode.Equals(itemcode));

                if (isImg != null)
                {
                    ViewBag.ProductImg = isImg.imgcode;
                }
                else
                {
                    ViewBag.ProductImg = null;
                }

            }
            else
            {
                ViewBag.Product = null;

                ViewBag.ProductImg = null;
            }

            return View();
        }

        #endregion

        #region POST

        public bool AddProductPrice(iproductprice price)
        {
            var db = new dbsmappEntities();

            var pprice = db.iproductprices.FirstOrDefault(s => s.itemcode.Equals(price.itemcode));

            if (pprice == null)
            {
                price.status = "new";

                NullFiller.FillNullFields<iproductprice>(price);
                Capitalize.UppercaseClassFields<iproductprice>(price);
                
                db.iproductprices.Add(price);                
            }
            else
            {
                pprice.sellingprice = price.sellingprice;
                pprice.purchasingprice = price.purchasingprice;
                pprice.vatprice = price.vatprice;
                pprice.remarks = price.remarks;                
            }

            db.SaveChanges();

            return true;
        }

        #endregion

        #region JSON

        [HttpGet]
        public JsonResult GetProductPrice(string itemcode)
        {
            var db = new dbsmappEntities();

            var QQList = (from s in db.iproductprices
                          where s.itemcode.Equals(itemcode)
                          select new xPrice
                          {
                              itemcode = s.itemcode,
                              sellingprice = s.sellingprice,
                              purchasingprice = s.purchasingprice,
                              vatprice = s.vatprice,
                              remarks = s.remarks,
                              status = s.status
                          }).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetProductPriceList()
        {
            var db = new dbsmappEntities();

            var QQList = (from s in db.iproducts
                          select new xProductPrice
                          {
                              itemcode = s.itemcode,
                              priceinfo = db.iproductprices.FirstOrDefault(e => e.itemcode.Equals(s.itemcode)),
                              arname = s.arname,
                              enname = s.enname,
                              barcode = s.barcode

                          }).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        #endregion  
    }
}

public partial class xPrice
{
    public string itemcode { get; set; }
    public string sellingprice { get; set; }
    public string purchasingprice { get; set; }
    public string vatprice { get; set; }
    public string remarks { get; set; }
    public string status { get; set; }

}

public partial class xProductPrice
{
    public string itemcode { get; set; }
    public iproductprice priceinfo { get; set; }
    public string arname { get; set; }
    public string enname { get; set; }
    public string barcode { get; set; }
    
}