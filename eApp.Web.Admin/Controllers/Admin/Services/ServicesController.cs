using eApp.Web.Admin.ADO;
using eApp.Web.Admin.Resources.LibClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eApp.Web.Admin.Controllers.Admin.Services
{
    [Authorize(Roles = "Administrator")]
    public class ServicesController : Controller
    {
        #region VIEW

        public ActionResult NewServices()
        {
            return View();
        }   

        public ActionResult EditServices(string servicescode)
        {
            var db = new dbsmappEntities();

            var elem = db.nservices.FirstOrDefault(s => s.servicescode.Equals(servicescode));

            if (elem != null)
            {
                ViewBag.Services = elem;

                var isImg = db.ysysphotoes.FirstOrDefault(s => s.imgcode.Equals(servicescode + "S"));

                if (isImg != null)
                {
                    ViewBag.ServicesImg = isImg.imgcode;
                }
                else
                {
                    ViewBag.ServicesImg = null;
                }
            }
            else
            {
                ViewBag.Services = null;

                ViewBag.ServicesImg = null;
            }

            return View();
        }
        
        public ActionResult SetServices(string servicescode)
        {            
            var db = new dbsmappEntities();

            var elem = db.nservices.FirstOrDefault(s => s.servicescode.Equals(servicescode));

            if (elem != null)
            {
                ViewBag.Services = elem;                
            }
            else
            {
                ViewBag.Services = null;                
            }

            return View();
        }
        
        public ActionResult SevicesList()
        {
            return View();
        }

        public ActionResult SevicesProductList(string servicescode)
        {
            ViewBag.Servicecode = servicescode;

            return View();
        }
        
        public ActionResult _tempAddService(string servicecode)
        {
            ViewBag.ServiceCode = servicecode;

            return View();
        }


        #endregion

        #region JSON

        [HttpGet]
        public JsonResult GetServicesCombiAll()
        {
            var db = new dbsmappEntities();

            var QQList = db.nservices.ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetServicesCombiAllProduct(string servicecode)
        {
            var db = new dbsmappEntities();

            var QQList = (from s in db.nservicesproducts
                          join cs in db.iproducts on s.itemcode equals cs.itemcode
                          where s.servicescode.Equals(servicecode)
                          select new xProductInfoServices
                          {
                            itemcode= s.itemcode,
                            barcode= cs.barcode,
                            arname= cs.arname,
                            enname= cs.enname,
                            remarks= s.remarks,
                            size= cs.size,
                            unit= cs.unit,

                          }).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult GetProductCombiAllServicesAvailable(string servicecode)
        {
            var db = new dbsmappEntities();

            var QQListAvailable = db.nservicesproducts.Where(s => s.servicescode.Equals(servicecode)).ToList();
            var QQListNotAvailable = db.iproducts.ToList();

            var QQList = QQListNotAvailable.Where(i => !QQListAvailable.Any(e => i.itemcode.Equals(e.itemcode))).ToList();

            return Json(QQList, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region POST

        public bool AddServices(nservice services)
        {
            var db = new dbsmappEntities();

            var serv = db.nservices.FirstOrDefault(s => s.servicescode.Equals(services.servicescode));

            if (serv == null)
            {
                NullFiller.FillNullFields<nservice>(services);
                Capitalize.UppercaseClassFields<nservice>(services);
                
                db.nservices.Add(services);
            }
            else
            {
                NullFiller.FillNullFields<nservice>(services);
                Capitalize.UppercaseClassFields<nservice>(services);

                serv.servicename = services.servicename;
                serv.description = services.description;
                serv.departmentid = services.departmentid;
                serv.nprice = services.nprice;
                serv.sprice = services.sprice;
                serv.minutes = services.minutes;
                serv.hours = services.hours;
                serv.fixedcommission = services.fixedcommission;
                serv.remarks = services.remarks;
                serv.status = services.status;
                
            }


            var files = Request.Files;

            if(files.Count != 0)
            {
                new ImageFunc().UploadProductPic(Request.Files, services.servicescode + "S");
            }            

            db.SaveChanges();

            return true;
        }
        
        public bool AddServicesProduct(string datalist)
        {
            var db = new dbsmappEntities();

            foreach (var item in datalist.Split(','))
            {
                var xo = item.Split('-');
                var sscode = xo[0].ToString();
                var ssitemcode = xo[1].ToString();
                var ssprice = xo[2].ToString();

                var serv = db.nservicesproductprices.FirstOrDefault(s => s.itemcode.Equals(sscode) && s.servicescode.Equals(ssitemcode));

                db.nservicesproductprices.Add(new nservicesproductprice
                {
                    servicescode = sscode,
                    itemcode = ssitemcode,
                    price = ssprice,
                    remarks = "",
                    status = "ACTIVATED"
                });

            }            

            db.SaveChanges();

            return true;
        }
        
        public bool AddProductToServices(string ServCode, string itemcode)
        {
            var db = new dbsmappEntities();

            var serv = db.nservicesproducts.FirstOrDefault(s => s.itemcode.Equals(itemcode) && s.servicescode.Equals(ServCode));

            if(serv == null)
            {
                db.nservicesproducts.Add(new nservicesproduct
                {
                    servicescode = ServCode,
                    itemcode = itemcode,
                    usage = "",
                    remarks = "",
                    status = "Acivated"
                });

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
                
        }

        public bool RemoveProductToServices(string ServCode, string itemcode)
        {
            var db = new dbsmappEntities();

            var serv = db.nservicesproducts.FirstOrDefault(s => s.itemcode.Equals(itemcode) && s.servicescode.Equals(ServCode));

            if (serv != null)
            {
                db.nservicesproducts.Remove(serv);

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool RemoveServices(string ServCode)
        {
            var db = new dbsmappEntities();
            
            var serv = db.nservices.FirstOrDefault(s => s.servicescode.Equals(ServCode));

            if (serv != null)
            {

                //------------------------ Service Products Price

                var getallnspp = db.nservicesproductprices.Where(s => s.servicescode.Equals(ServCode)).ToList();

                foreach (var item in getallnspp)
                {
                    db.nservicesproductprices.Remove(item);
                }

                //------------------------ Service Products

                var getallnsp = db.nservicesproducts.Where(s => s.servicescode.Equals(ServCode)).ToList();

                foreach (var item in getallnsp)
                {
                    db.nservicesproducts.Remove(item);
                }

                //------------------------ Service

                db.nservices.Remove(serv);

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


public partial class xProductInfoServices
{
    public string itemcode { get; set; }
    public string barcode { get; set; }
    public string arname { get; set; }
    public string enname { get; set; }
    public string remarks { get; set; }
    public string size { get; set; }
    public string unit { get; set; }
}