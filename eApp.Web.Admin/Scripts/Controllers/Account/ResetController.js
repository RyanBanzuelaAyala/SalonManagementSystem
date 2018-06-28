var ResetController = function ($scope, ConfirmFactory, ValidateFactory) {


    $scope.NewPassword = function () {

        var result = ValidateFactory.validate();

        if (result == "success") {

            var formDataDoc = new FormData();

            var npass = document.getElementById('npassword').value;
            var rpass = document.getElementById('rpassword').value;

            if (npass != rpass) {
                return;
            }

            formDataDoc.append("npassword", npass);
            formDataDoc.append("cpassword", document.getElementById('cpassword').value);

            ConfirmFactory.UrlPostRedirect('/Account/ResetPassword', formDataDoc, '/supp/password');

        }

    }
    

}

ResetController.$inject = ['$scope', 'ConfirmFactory', 'ValidateFactory'];

