var BranchController = function ($scope, ConfirmFactory, ValidateFactory, DeptFactory) {
   
    $scope.AddBranch = function () {

        var result = ValidateFactory.validate();

        if (result == "success") {

            var formDataDoc = new FormData();

            formDataDoc.append("branchcode", document.getElementById('branchcode').value);
            formDataDoc.append("branchname", document.getElementById('branchname').value);
            formDataDoc.append("remarks", document.getElementById('remarks').value);

            ConfirmFactory.UrlPostRedirect('/Branch/AddBranch', formDataDoc, '/branch/list');

        }

    }

    $scope.DeleteBranch = function (branchcode) {
                
        var formDataDoc = new FormData();

        formDataDoc.append("branchcode", branchcode);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to delete this Branch?", "Branch/DeleteBranch", formDataDoc, "/branch/list");

    }
    
}

BranchController.$inject = ['$scope', 'ConfirmFactory', 'ValidateFactory', 'DeptFactory'];
