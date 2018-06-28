var bkBranchController = function ($scope, ConfirmFactory, ValidateFactory, OrderFactory) {
   
    $scope.AddBranchOrder = function () {

        var result = ValidateFactory.validate();

        if (result == "success") {

            var formDataDoc = new FormData();

            var elem = document.getElementById('servicescode');
            var srv = elem.options[elem.selectedIndex].value;
    
            formDataDoc.append("servicecode", srv);
            formDataDoc.append("remarks", document.getElementById('remarks').value);
            formDataDoc.append("brn", document.getElementById('brn').value);

            ConfirmFactory.UrlPostRedirect('/iBranch/AddBranchOrder', formDataDoc, '/ibranch/list');

        }

    }

    $scope.DeleteBranchOrder = function (itemcode, ordercode) {
                
        var formDataDoc = new FormData();

        formDataDoc.append("ordercode", ordercode);
        formDataDoc.append("itemcode", itemcode);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to remove this product in order list?", "iBranch/DeleteProductToOrder", formDataDoc, "/ibranch/list");

    }

    $scope.AddProductToOrder = function (data) {

        console.log(data);

        OrderFactory.OrderInfo(data);

    }

    $scope.FinalOrder = function (data) {

        var formDataDoc = new FormData();

        formDataDoc.append("ordercode", data);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to submit this order list?", "iBranch/UpdateBranchOrder", formDataDoc, "/ibranch/list");


    }

}

bkBranchController.$inject = ['$scope', 'ConfirmFactory', 'ValidateFactory', 'OrderFactory'];
