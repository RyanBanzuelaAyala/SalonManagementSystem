var dnb = angular.module('dnb', ['ngRoute', 'ui.bootstrap']);

dnb.service('dnbservices', dnbservices);

dnb.factory('ConfirmFactory', ConfirmFactory);
dnb.factory('ValidateFactory', ValidateFactory);
dnb.factory('UserFactory', UserFactory);
dnb.factory('DeptFactory', DeptFactory);
dnb.factory('ServiceFactory', ServiceFactory);

dnb.controller('LandingPageController', LandingPageController);
dnb.factory('AuthHttpResponseInterceptor', AuthHttpResponseInterceptor);

var configFunction = function ($routeProvider, $httpProvider, $locationProvider) {

    $locationProvider.hashPrefix('!').html5Mode(true);
    
    $routeProvider.    
        when('/ibranch/list', {
            templateUrl: 'iBranch/BranchOrderListAll'
        }) 
        .when('/branch/edit/:branchcode', {
            templateUrl: function (params) { return '/Branch/EditBranch?branchcode=' + params.branchcode; }
        })
        .when('/branch/new', {
            templateUrl: 'Branch/NewBranch'
        })        
        .when('/branch/list', {
            templateUrl: 'Branch/BranchList'
        })        
        .when('/department/edit/:deptcode', {
            templateUrl: function (params) { return '/Department/EditDepartment?deptcode=' + params.deptcode; }
        })  
        .when('/department/list', {
            templateUrl: 'Department/NewDepartmentList'
        })
        .when('/department/new', {
            templateUrl: 'Department/NewDepartment'
        })
        .when('/services/set/:servicescode', {
            templateUrl: function (params) { return '/Services/SetServices?servicescode=' + params.servicescode; }
        })  
        .when('/services/edit/:servicescode', {
            templateUrl: function (params) { return '/Services/EditServices?servicescode=' + params.servicescode; }
        })  
        .when('/services/productlist/:servicescode', {
            templateUrl: function (params) { return '/Services/SevicesProductList?servicescode=' + params.servicescode; }
        })         
        .when('/services/list', {
            templateUrl: 'Services/SevicesList'
        })
        .when('/services/new', {
            templateUrl: 'Services/NewServices'
        })
        .when('/pricing/list', {
            templateUrl: 'Pricing/NewProductPricing'
        })
        .when('/pricing/product/:itemcode', {
            templateUrl: function (params) { return '/Pricing/NewPricing?itemcode=' + params.itemcode; }
        })  
        .when('/prod/new', {
            templateUrl: 'Product/NewProduct'
        })
        .when('/prod/edit/:itemcode', {
            templateUrl: function (params) { return '/Product/EditProduct?itemcode=' + params.itemcode; }
        })  
        .when('/prod/list', {
            templateUrl: 'Product/NewProductList'
        })
        .when('/supp/password', {
            templateUrl: 'Account/ChangePassword'
        })            
        .when('/user/new', {
            templateUrl: 'User/NewUser'
        })        
        .when('/user/list', {
            templateUrl: 'User/UserList'
        })        
        .when('/', {
            templateUrl: 'Home/Startup'
        })
        .otherwise({ redirectTo: '/' });

    $httpProvider.interceptors.push('AuthHttpResponseInterceptor');

}
configFunction.$inject = ['$routeProvider', '$httpProvider', '$locationProvider'];

dnb.config(configFunction);