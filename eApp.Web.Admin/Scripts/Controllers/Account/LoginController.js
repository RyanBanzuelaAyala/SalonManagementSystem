var LoginController = function ($scope, ConfirmFactory, ValidateFactory) {

    $scope.Process = function (event) {

        if (event.keyCode === 13) {
            _processAuth();
        }

    }

    $scope.Login = function () {
        
        _processAuth();

    }

    function _processAuth() {

        var result = ValidateFactory.validate();
        
        if (result == "success") {

            var formDataDoc = new FormData();

            var userid = document.getElementById('username').value;

            formDataDoc.append("Username", userid);
            formDataDoc.append("Password", document.getElementById('password').value);
            formDataDoc.append("RememberMe", document.getElementById('password').value);
            formDataDoc.append("Region", document.getElementById('regg').value);

            ConfirmFactory.UrlPostRedirectX('/Account/Login', formDataDoc, '/Account/SelectedCurrentRole');

        }

    }

    $scope.LoginRole = function () {
    
        var result = ValidateFactory.validate();

        if (result == "success") {
        
            var formDataDoc = new FormData();
            
            formDataDoc.append("role", document.getElementById('role').value);
            formDataDoc.append("branch", document.getElementById('branch').value);

            ConfirmFactory.UrlPostRedirectX('/Account/LoginRole', formDataDoc, '/');

        }

    }
    
}

LoginController.$inject = ['$scope', 'ConfirmFactory', 'ValidateFactory'];