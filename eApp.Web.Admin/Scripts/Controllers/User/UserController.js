var UserController = function ($scope, ConfirmFactory, ValidateFactory, UserFactory) {

    $scope.Process = function () {

        var result = ValidateFactory.validate();

        if (result == "success") {

            var formDataDoc = new FormData();

            formDataDoc.append("userid", document.getElementById('userid').value);
            formDataDoc.append("name", document.getElementById('name').value);
            formDataDoc.append("role", document.getElementById('role').value);
            formDataDoc.append("file", document.getElementById("imgInpX").files[0]);
          
            ConfirmFactory.UrlPostRedirect('/User/UserSubmit', formDataDoc, '/user/list');

        }
    }

    $scope.AddRole = function (userid) {

        UserFactory.UserInfo(userid);
    }

    $scope.AddBranch = function (userid) {

        UserFactory.UserInfoBranch(userid);
    }

    $scope.RemoveRole = function (role, userid) {

        var formDataDoc = new FormData();

        formDataDoc.append("role", role);
        formDataDoc.append("userid", userid);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to remove this Role?", "User/UserRemoveRole", formDataDoc, "/user/list");

    }

    $scope.RemoveBranch = function (branchcode, userid) {

        var formDataDoc = new FormData();

        formDataDoc.append("branchcode", branchcode);
        formDataDoc.append("userid", userid);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to remove this Branch?", "User/UserRemoveBranch", formDataDoc, "/user/list");

    }
}

UserController.$inject = ['$scope', 'ConfirmFactory', 'ValidateFactory', 'UserFactory'];
