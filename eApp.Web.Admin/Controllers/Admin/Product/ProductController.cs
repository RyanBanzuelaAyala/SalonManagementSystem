using eApp.Web.Admin.ADO;
using eApp.Web.Admin.Resources.LibClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eApp.Web.Admin.Controllers.Admin.Product
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        #region VIEW
                
        public ActionResult NewProductList()
        {
            return View();
        }

        public ActionResult NewProduct()
        {
            return View();
        }

        public ActionResult EditProduct(string itemcode)
        {
            var db = new dbsmappEntities();

            var isProduct = db.iproducts.FirstOrDefault(s => s.itemcode.Equals(itemcode));

            if(isProduct != null)
            {
                ViewBag.Product = isProduct;

                var isImg = db.ysysphotoes.FirstOrDefault(s => s.imgcode.Equals(itemcode + "P"));

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

        #region JSON

        [HttpGet]
        public JsonResult GetProductCombiAll()
        {
            var db = new dbsmappEntities();
            
            var QQList = (from s in db.iproducts                                                  
                          select new xProduct
                          {
                              itemcode = s.itemcode,
                              barcode = s.barcode,
                              arname = s.arname,
                              enname = s.enname,
                              remarks = s.remarks
                              
                          }).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetProductCombiAllInfo()
        {
            var db = new dbsmappEntities();

            var QQList = (from s in db.iproducts
                          select new xProductInfo
                          {
                              itemcode = s.itemcode,
                              barcode = s.barcode,
                              arname = s.arname,
                              enname = s.enname,
                              remarks = s.remarks,
                              size = s.size,
                              unit = s.unit,
                              price = db.iproductprices.FirstOrDefault(x => x.itemcode.Equals(s.itemcode))
                          }).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        #endregion  

        #region POST
        public bool AddProduct(iproduct product)
        {
            var db = new dbsmappEntities();
            
            var prod = db.iproducts.FirstOrDefault(s => s.barcode.Equals(product.barcode));

            if(prod == null)
            {
                product.status = "activated";

                NullFiller.FillNullFields<iproduct>(product);

                Capitalize.UppercaseClassFields<iproduct>(product);
                                               
                db.iproducts.Add(product);
                
                var files = Request.Files;

                if(files.Count != 0)
                {
                    new ImageFunc().UploadProductPic(Request.Files, product.itemcode + "P");
                }
                
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;                
            }

        }

        public bool UpdateProduct(iproduct product)
        {
            var db = new dbsmappEntities();

            var prod = db.iproducts.FirstOrDefault(s => s.itemcode.Equals(product.itemcode));

            if (prod == null)
            {
                return false;
            }
            else
            {
                NullFiller.FillNullFields<iproduct>(product);

                Capitalize.UppercaseClassFields<iproduct>(product);

                prod.itemcode = product.itemcode;
                prod.barcode = product.barcode;
                prod.modelno = product.modelno;
                prod.serialno = product.serialno;
                prod.arname = product.arname;
                prod.enname = product.enname;
                prod.arshortname = product.arshortname;
                prod.enshortname = product.enshortname;
                prod.size = product.size;
                prod.unit = product.unit;
                prod.remarks = product.remarks;
                prod.status = product.status;
                
                var files = Request.Files;
                
                if (files.Count != 0)
                {
                    new ImageFunc().UploadProductPic(Request.Files, product.itemcode + "P");
                }

                db.SaveChanges();

                return true;

            }

        }

        public bool DeleteProduct(string itemcode)
        {
            var db = new dbsmappEntities();

            var prod = db.iproducts.FirstOrDefault(s => s.itemcode.Equals(itemcode));

            if (prod == null)
            {
                return false;
            }
            else
            {
                db.iproducts.Remove(prod);

                var item1 = db.nservicesproductprices.FirstOrDefault(s => s.itemcode.Equals(itemcode));

                if (item1 != null)
                {
                    db.nservicesproductprices.Remove(item1);
                }

                var item2 = db.nservicesproducts.FirstOrDefault(s => s.itemcode.Equals(itemcode));

                if (item2 != null)
                {
                    db.nservicesproducts.Remove(item2);
                }

                var picExist = db.ysysphotoes.FirstOrDefault(s => s.imgcode.Equals(itemcode));

                if(picExist != null)
                {
                    db.ysysphotoes.Remove(picExist);

                    new ImageFunc().RemoveProductPic(picExist.imgcode);
                }
                
                db.SaveChanges();

                return true;

            }
        }


        #endregion
    }
}


public partial class xProduct
{
    public string itemcode { get; set; }
    public string barcode { get; set; }    
    public string arname { get; set; }
    public string enname { get; set; }    
    public string remarks { get; set; }
    public string status { get; set; }
}

public partial class xProductInfo
{
    public string itemcode { get; set; }
    public string barcode { get; set; }
    public string arname { get; set; }
    public string enname { get; set; }
    public string remarks { get; set; }
    public string size { get; set; }
    public string unit { get; set; }
    public iproductprice price { get; set; }
}