var dnb = angular.module('Login', ['ngRoute', 'ui.bootstrap']);

dnb.controller('LoginController', LoginController);

dnb.service('dnbservices', dnbservices);

dnb.factory('ConfirmFactory', ConfirmFactory);
dnb.factory('ValidateFactory', ValidateFactory);

console.log("Login loaded");