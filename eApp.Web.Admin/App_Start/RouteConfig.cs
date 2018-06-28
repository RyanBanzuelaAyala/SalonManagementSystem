using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eApp.Web.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            // -------------------------- BRANCH DEPARTMENT


            #region BRANCH DEPARTMENT ADMIN BK

            // ------- VIEW

            routes.MapRoute(
               name: "BranchOrderListAll",
               url: "iBranch/BranchOrderListAll",
               defaults: new { controller = "iBranch", action = "BranchOrderListAll" });
            
            // ------- JSON

            routes.MapRoute(
               name: "GetCombiAllBranchOrdersOA",
               url: "iBranch/GetCombiAllBranchOrdersOA",
               defaults: new { controller = "iBranch", action = "GetCombiAllBranchOrdersOA" });
            
            #endregion


            #region BRANCH DEPARTMENT

            // ------- VIEW

            routes.MapRoute(
               name: "BranchOrderList",
               url: "iBranch/BranchOrderList",
               defaults: new { controller = "iBranch", action = "BranchOrderList" });

            routes.MapRoute(
               name: "NewBranchOrder",
               url: "iBranch/NewBranchOrder",
               defaults: new { controller = "iBranch", action = "NewBranchOrder" });

            routes.MapRoute(
               name: "_tempAddBranchOrder",
               url: "iBranch/_tempAddBranchOrder",
               defaults: new { controller = "iBranch", action = "_tempAddBranchOrder" });

            // ------- JSON

            routes.MapRoute(
               name: "GetCombiAllBranchOrders",
               url: "iBranch/GetCombiAllBranchOrders",
               defaults: new { controller = "iBranch", action = "GetCombiAllBranchOrders" });

            routes.MapRoute(
               name: "GetCombiAllBranchOrderList",
               url: "iBranch/GetCombiAllBranchOrderList",
               defaults: new { controller = "iBranch", action = "GetCombiAllBranchOrderList" });

            routes.MapRoute(
               name: "GetProductOrderCombiAllAvailable",
               url: "iBranch/GetProductOrderCombiAllAvailable",
               defaults: new { controller = "iBranch", action = "GetProductOrderCombiAllAvailable" });


            // ------- POST

            routes.MapRoute(
               name: "AddBranchOrder",
               url: "iBranch/AddBranchOrder",
               defaults: new { controller = "iBranch", action = "AddBranchOrder" });

            routes.MapRoute(
               name: "AddProductToOrder",
               url: "iBranch/AddProductToOrder",
               defaults: new { controller = "iBranch", action = "AddProductToOrder" });

            routes.MapRoute(
               name: "DeleteProductToOrder",
               url: "iBranch/DeleteProductToOrder",
               defaults: new { controller = "iBranch", action = "DeleteProductToOrder" });

            routes.MapRoute(
               name: "UpdateBranchOrder",
               url: "iBranch/UpdateBranchOrder",
               defaults: new { controller = "iBranch", action = "UpdateBranchOrder" });

            #endregion

            // -------------------------- BRANCH

            #region BRANCH

            // ------- VIEW

            routes.MapRoute(
                name: "NewBranch",
                url: "Branch/NewBranch",
                defaults: new { controller = "Branch", action = "NewBranch" });

            routes.MapRoute(
                name: "BranchList",
                url: "Branch/BranchList",
                defaults: new { controller = "Branch", action = "BranchList" });

            routes.MapRoute(
                name: "EditBranch",
                url: "Branch/EditBranch",
                defaults: new { controller = "Branch", action = "EditBranch" });

            // ------- POST

            routes.MapRoute(
                name: "AddBranch",
                url: "Branch/AddBranch",
                defaults: new { controller = "Branch", action = "AddBranch" });

            routes.MapRoute(
                name: "DeleteBranch",
                url: "Branch/DeleteBranch",
                defaults: new { controller = "Branch", action = "DeleteBranch" });


            // ------- JSON

            routes.MapRoute(
                name: "GetCombiAllBranch",
                url: "Branch/GetCombiAllBranch",
                defaults: new { controller = "Branch", action = "GetCombiAllBranch" });


            #endregion

            // -------------------------- DEPARTMENT

            #region DEPARTMENT

            // ------- VIEW

            routes.MapRoute(
                name: "EditDepartment",
                url: "Department/EditDepartment",
                defaults: new { controller = "Department", action = "EditDepartment" });

            routes.MapRoute(
                name: "NewDepartment",
                url: "Department/NewDepartment",
                defaults: new { controller = "Department", action = "NewDepartment" });
            
            routes.MapRoute(
                name: "NewDepartmentList",
                url: "Department/NewDepartmentList",
                defaults: new { controller = "Department", action = "NewDepartmentList" });
            
            routes.MapRoute(
                name: "_tempAddService",
                url: "Department/_tempAddService",
                defaults: new { controller = "Department", action = "_tempAddService" });


            // ------- POST

            routes.MapRoute(
                name: "AddDepartment",
                url: "Department/AddDepartment",
                defaults: new { controller = "Department", action = "AddDepartment" });

            routes.MapRoute(
                name: "AddDepartmentServices",
                url: "Department/AddDepartmentServices",
                defaults: new { controller = "Department", action = "AddDepartmentServices" });

            routes.MapRoute(
                name: "RemoveDepartmentServices",
                url: "Department/RemoveDepartmentServices",
                defaults: new { controller = "Department", action = "RemoveDepartmentServices" });

            routes.MapRoute(
                name: "RemoveDepartment",
                url: "Department/RemoveDepartment",
                defaults: new { controller = "Department", action = "RemoveDepartment" });

            // ------- JSON

            routes.MapRoute(
                name: "GetDepartmentCombiAll",
                url: "Department/GetDepartmentCombiAll",
                defaults: new { controller = "Department", action = "GetDepartmentCombiAll" });

            routes.MapRoute(
                name: "GetDepartmentCombiAllServices",
                url: "Department/GetDepartmentCombiAllServices",
                defaults: new { controller = "Department", action = "GetDepartmentCombiAllServices" });

            routes.MapRoute(
                name: "GetDepartmentCombiAllServicesAvailable",
                url: "Department/GetDepartmentCombiAllServicesAvailable",
                defaults: new { controller = "Department", action = "GetDepartmentCombiAllServicesAvailable" });

            #endregion

            // -------------------------- SERVICES

            #region SERVICES

            // ------- VIEW

            routes.MapRoute(
               name: "SetServices",
               url: "Services/SetServices",
               defaults: new { controller = "Services", action = "SetServices" });

            routes.MapRoute(
               name: "EditServices",
               url: "Services/EditServices",
               defaults: new { controller = "Services", action = "EditServices" });

            routes.MapRoute(
               name: "NewServices",
               url: "Services/NewServices",
               defaults: new { controller = "Services", action = "NewServices" });
            
            routes.MapRoute(
               name: "SevicesList",
               url: "Services/SevicesList",
               defaults: new { controller = "Services", action = "SevicesList" });

            routes.MapRoute(
               name: "SevicesProductList",
               url: "Services/SevicesProductList",
               defaults: new { controller = "Services", action = "SevicesProductList" });
            
            routes.MapRoute(
               name: "_tempAddService2",
               url: "Services/_tempAddService",
               defaults: new { controller = "Services", action = "_tempAddService" });

            // ------- POST

            routes.MapRoute(
               name: "AddServices",
               url: "Services/AddServices",
               defaults: new { controller = "Services", action = "AddServices" });

            routes.MapRoute(
               name: "AddServicesProduct",
               url: "Services/AddServicesProduct",
               defaults: new { controller = "Services", action = "AddServicesProduct" });

            routes.MapRoute(
               name: "AddProductToServices",
               url: "Services/AddProductToServices",
               defaults: new { controller = "Services", action = "AddProductToServices" });

            routes.MapRoute(
               name: "RemoveProductToServices",
               url: "Services/RemoveProductToServices",
               defaults: new { controller = "Services", action = "RemoveProductToServices" });

            routes.MapRoute(
               name: "RemoveServices",
               url: "Services/RemoveServices",
               defaults: new { controller = "Services", action = "RemoveServices" });

            // ------- JSON

            routes.MapRoute(
               name: "GetServicesCombiAll",
               url: "Services/GetServicesCombiAll",
               defaults: new { controller = "Services", action = "GetServicesCombiAll" });

            routes.MapRoute(
               name: "GetServicesCombiAllProduct",
               url: "Services/GetServicesCombiAllProduct",
               defaults: new { controller = "Services", action = "GetServicesCombiAllProduct" });

            routes.MapRoute(
               name: "GetProductCombiAllServicesAvailable",
               url: "Services/GetProductCombiAllServicesAvailable",
               defaults: new { controller = "Services", action = "GetProductCombiAllServicesAvailable" });

            #endregion

            // -------------------------- PRODUCT PRICING

            #region PRICING

            // ------- PRODUCT VIEW

            routes.MapRoute(
               name: "NewProductPricing",
               url: "Pricing/NewProductPricing",
               defaults: new { controller = "Pricing", action = "NewProductPricing" });


            routes.MapRoute(
               name: "NewPricing",
               url: "Pricing/NewPricing",
               defaults: new { controller = "Pricing", action = "NewPricing" });


            // ------- PRODUCT POST

            routes.MapRoute(
               name: "AddProductPrice",
               url: "Pricing/AddProductPrice",
               defaults: new { controller = "Pricing", action = "AddProductPrice" });


            // ------- PRODUCT JSON

            routes.MapRoute(
               name: "GetProductPrice",
               url: "Pricing/GetProductPrice",
               defaults: new { controller = "Pricing", action = "GetProductPrice" });

            routes.MapRoute(
               name: "GetProductPriceList",
               url: "Pricing/GetProductPriceList",
               defaults: new { controller = "Pricing", action = "GetProductPriceList" });

            #endregion

            // -------------------------- PRODUCTS

            #region PRODUCT

            // ------- JSON 

            routes.MapRoute(
               name: "GetProductCombiAllInfo",
               url: "Product/GetProductCombiAllInfo",
               defaults: new { controller = "Product", action = "GetProductCombiAllInfo" });

            routes.MapRoute(
               name: "GetProductCombiAll",
               url: "Product/GetProductCombiAll",
               defaults: new { controller = "Product", action = "GetProductCombiAll" });

            // ------- VIEWS 

            routes.MapRoute(
               name: "NewProductList",
               url: "Product/NewProductList",
               defaults: new { controller = "Product", action = "NewProductList" });

            routes.MapRoute(
               name: "NewProduct",
               url: "Product/NewProduct",
               defaults: new { controller = "Product", action = "NewProduct" });

            routes.MapRoute(
               name: "EditProduct",
               url: "Product/EditProduct",
               defaults: new { controller = "Product", action = "EditProduct" });

            // ------- POST 

            routes.MapRoute(
               name: "DeleteProduct",
               url: "Product/DeleteProduct",
               defaults: new { controller = "Product", action = "DeleteProduct" });

            routes.MapRoute(
               name: "UpdateProduct",
               url: "Product/UpdateProduct",
               defaults: new { controller = "Product", action = "UpdateProduct" });

            routes.MapRoute(
               name: "AddProduct",
               url: "Product/AddProduct",
               defaults: new { controller = "Product", action = "AddProduct" });

            #endregion

            // -------------------------- SYSTEM ADDS-ON

            #region Reminder

            routes.MapRoute(
                name: "RemindUser",
                url: "Reminder/RemindUser",
                defaults: new { controller = "Reminder", action = "RemindUser" });

            #endregion

            // -------------------------- USERS 

            #region User

            // ------- VIEW

            routes.MapRoute(
                name: "UserList",
                url: "User/UserList",
                defaults: new { controller = "User", action = "UserList" });

            routes.MapRoute(
                name: "NewUser",
                url: "User/NewUser",
                defaults: new { controller = "User", action = "NewUser" });

            routes.MapRoute(
                name: "_tempAddRole",
                url: "User/_tempAddRole",
                defaults: new { controller = "User", action = "_tempAddRole" });

            routes.MapRoute(
                name: "_tempAddBranch",
                url: "User/_tempAddBranch",
                defaults: new { controller = "User", action = "_tempAddBranch" });

            // ------- JSON 

            routes.MapRoute(
                name: "GetUserCombiAll",
                url: "User/GetUserCombiAll",
                defaults: new { controller = "User", action = "GetUserCombiAll" });

            routes.MapRoute(
                name: "GetUserCombiAllRole",
                url: "User/GetUserCombiAllRole",
                defaults: new { controller = "User", action = "GetUserCombiAllRole" });

            routes.MapRoute(
                name: "GetUserCombiAllBranch",
                url: "User/GetUserCombiAllBranch",
                defaults: new { controller = "User", action = "GetUserCombiAllBranch" });

            routes.MapRoute(
                name: "GetUserCombiAllBranchAvailable",
                url: "User/GetUserCombiAllBranchAvailable",
                defaults: new { controller = "User", action = "GetUserCombiAllBranchAvailable" });

            // ------- POST

            routes.MapRoute(
                name: "UserSubmit",
                url: "User/UserSubmit",
                defaults: new { controller = "User", action = "UserSubmit" });

            routes.MapRoute(
                name: "UserRoleSubmit",
                url: "User/UserRoleSubmit",
                defaults: new { controller = "User", action = "UserRoleSubmit" });

            routes.MapRoute(
                name: "UserBranchSubmit",
                url: "User/UserBranchSubmit",
                defaults: new { controller = "User", action = "UserBranchSubmit" });

            routes.MapRoute(
                name: "UserRemoveBranch",
                url: "User/UserRemoveBranch",
                defaults: new { controller = "User", action = "UserRemoveBranch" });

            routes.MapRoute(
                name: "UserRemoveRole",
                url: "User/UserRemoveRole",
                defaults: new { controller = "User", action = "UserRemoveRole" });

            #endregion

            #region Authentication

            // ------- POST

            routes.MapRoute(
                name: "Login",
                url: "Account/Login",
                defaults: new { controller = "Account", action = "Login" });

            routes.MapRoute(
                name: "LoginRole",
                url: "Account/LoginRole",
                defaults: new { controller = "Account", action = "LoginRole" });

            routes.MapRoute(
                name: "LogOff",
                url: "Account/LogOff",
                defaults: new { controller = "Account", action = "LogOff" });

            routes.MapRoute(
                name: "SessionExpired",
                url: "Account/SessionExpired",
                defaults: new { controller = "Account", action = "SessionExpired" });

            // ------- VIEW

            routes.MapRoute(
                name: "AddAgent",
                url: "Account/AddAgent",
                defaults: new { controller = "Account", action = "AddAgent" });

            routes.MapRoute(
                name: "ChooseCurrentRole",
                url: "Account/ChooseCurrentRole",
                defaults: new { controller = "Account", action = "ChooseCurrentRole" });

            routes.MapRoute(
                name: "SelectedCurrentRole",
                url: "Account/SelectedCurrentRole",
                defaults: new { controller = "Account", action = "SelectedCurrentRole" });

            routes.MapRoute(
                name: "AccountMaintenance",
                url: "Account/Maintenance",
                defaults: new { controller = "Account", action = "Maintenance" });

            routes.MapRoute(
                name: "ChangePassword",
                url: "Account/ChangePassword",
                defaults: new { controller = "Account", action = "ChangePassword" });

            routes.MapRoute(
                name: "ResetPassword",
                url: "Account/ResetPassword",
                defaults: new { controller = "Account", action = "ResetPassword" });
            
          
            #endregion

            // -------------------------- SYSTEM ADDS-ON

            #region Default

            routes.MapRoute(
                name: "_Template",
                url: "Vehicles/_Template",
                defaults: new { controller = "Vehicles", action = "_Template" });

            routes.MapRoute(
                name: "Startup",
                url: "Home/Startup",
                defaults: new { controller = "Home", action = "Startup" });

            routes.MapRoute(
                name: "GetEvents",
                url: "Home/GetEvents",
                defaults: new { controller = "Home", action = "GetEvents" });

            routes.MapRoute(
                name: "Default",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index" });
            
            #endregion


        }
    }
}
