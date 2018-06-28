var DepartmentController = function ($scope, ConfirmFactory, ValidateFactory, DeptFactory) {

    $scope.Process = function () {

        var result = ValidateFactory.validate();

        if (result == "success") {

            var formDataDoc = new FormData();

            formDataDoc.append("deptcode", document.getElementById('deptcode').value);
            formDataDoc.append("deptname", document.getElementById('deptname').value);
            formDataDoc.append("remarks", document.getElementById('remarks').value);
            
            ConfirmFactory.UrlPostRedirect('/Department/AddDepartment', formDataDoc, '/department/list');

        }
    }

    $scope.AddDeptService = function (data) {

        DeptFactory.DeptInfo(data);

    }

    $scope.DeletetDepartmentService = function (servicecode, deptcode) {

        var formDataDoc = new FormData();

        formDataDoc.append("DeptCode", deptcode);
        formDataDoc.append("ServCode", servicecode);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to remove this Services?", "Department/RemoveDepartmentServices", formDataDoc, "/department/list");

    }

    $scope.DeleteDept = function (deptcode) {

        var formDataDoc = new FormData();

        formDataDoc.append("DeptCode", deptcode);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to remove this Department? <br> Note: All services in this department will be deleted also", "Department/RemoveDepartment", formDataDoc, "/department/list");

    }
    
}

DepartmentController.$inject = ['$scope', 'ConfirmFactory', 'ValidateFactory', 'DeptFactory'];
