var idnb = angular.module('idnb', ['ngRoute', 'ui.bootstrap']);

idnb.service('dnbservices', dnbservices);

idnb.factory('ConfirmFactory', ConfirmFactory);
idnb.factory('ValidateFactory', ValidateFactory);
idnb.factory('OrderFactory', OrderFactory);

idnb.controller('LandingPageController', LandingPageController);
idnb.factory('AuthHttpResponseInterceptor', AuthHttpResponseInterceptor);

var configFunction = function ($routeProvider, $httpProvider, $locationProvider) {

    $locationProvider.hashPrefix('!').html5Mode(true);
    
    $routeProvider.     
        when('/ibranch/neworder', {
            templateUrl: 'iBranch/NewBranchOrder'
        })
        .when('/ibranch/orderlist', {
            templateUrl: 'iBranch/BranchOrderList'
        })        
        .when('/', {
            templateUrl: 'iBranch/BranchOrderList'
        })
        .otherwise({ redirectTo: '/' });

    $httpProvider.interceptors.push('AuthHttpResponseInterceptor');

}
configFunction.$inject = ['$routeProvider', '$httpProvider', '$locationProvider'];

idnb.config(configFunction);