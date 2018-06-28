using System.Web;
using System.Web.Optimization;

namespace eApp.Web.Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/dnb")
              .Include("~/Scripts/Services/dnbservices.js")
              .Include("~/Scripts/Factories/ConfirmFactory.js")
              .Include("~/Scripts/Factories/ValidateFactory.js")       
              .Include("~/Scripts/Factories/UserFactory.js")
              .Include("~/Scripts/Factories/Admin/DeptFactory.js")
              .Include("~/Scripts/Factories/Admin/ServiceFactory.js")
              .Include("~/Scripts/Controllers/Account/ResetController.js")
              .Include("~/Scripts/Controllers/Profile/LandingPageController.js")
              .Include("~/Scripts/Factories/Profile/AuthHttpResponseInterceptor.js")              
              .Include("~/Scripts/Controllers/User/UserController.js")
              .Include("~/Scripts/Controllers/Admin/Product/ProductController.js")
              .Include("~/Scripts/Controllers/Admin/Pricing/PricingController.js")
              .Include("~/Scripts/Controllers/Admin/Services/ServicesController.js")
              .Include("~/Scripts/Controllers/Admin/Department/DepartmentController.js")
              .Include("~/Scripts/Controllers/Admin/Branch/BranchController.js")
              .Include("~/Scripts/Module/dnb.js"));

            bundles.Add(new ScriptBundle("~/bundles/idnb")
             .Include("~/Scripts/Services/dnbservices.js")
             .Include("~/Scripts/Factories/ConfirmFactory.js")
             .Include("~/Scripts/Factories/ValidateFactory.js")
             .Include("~/Scripts/Factories/Backend/OrderFactory.js")
             .Include("~/Scripts/Controllers/Account/ResetController.js")
             .Include("~/Scripts/Controllers/Profile/LandingPageController.js")
             .Include("~/Scripts/Factories/Profile/AuthHttpResponseInterceptor.js")
             .Include("~/Scripts/Controllers/Backend/bkBranch/bkBranchController.js")
             .Include("~/Scripts/Module/idnb.js"));

            bundles.Add(new ScriptBundle("~/bundles/Login")
                .Include("~/Scripts/Services/dnbservices.js")
                .Include("~/Scripts/Controllers/Account/LoginController.js")
                .Include("~/Scripts/Module/Login.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
