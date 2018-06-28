var ProductController = function ($scope, ConfirmFactory, ValidateFactory) {

    $scope.Process = function () {

        var result = ValidateFactory.validate();

        if (result == "success") {

            var formDataDoc = new FormData();

            formDataDoc.append("itemcode", document.getElementById('itemcode').value);
            formDataDoc.append("barcode", document.getElementById('barcode').value);
            formDataDoc.append("modelno", document.getElementById('modelno').value);
            formDataDoc.append("serialno", document.getElementById('serialno').value);

            formDataDoc.append("remarks", document.getElementById('remarks').value);
            formDataDoc.append("arname", document.getElementById('arname').value);
            formDataDoc.append("enname", document.getElementById('enname').value);
            formDataDoc.append("arshortname", document.getElementById('arshortname').value);
            formDataDoc.append("enshortname", document.getElementById('enshortname').value);

            formDataDoc.append("size", document.getElementById('size').value);
            formDataDoc.append("unit", document.getElementById('unit').value);
            
            formDataDoc.append("file", document.getElementById("imgInpX").files[0]);

            ConfirmFactory.UrlPostRedirect('/Product/AddProduct', formDataDoc, '/prod/list');

        }
    }

    $scope.Update = function () {

        var result = ValidateFactory.validate();

        if (result == "success") {

            var formDataDoc = new FormData();

            formDataDoc.append("itemcode", document.getElementById('itemcode').value);
            formDataDoc.append("barcode", document.getElementById('barcode').value);
            formDataDoc.append("modelno", document.getElementById('modelno').value);
            formDataDoc.append("serialno", document.getElementById('serialno').value);

            formDataDoc.append("remarks", document.getElementById('remarks').value);
            formDataDoc.append("arname", document.getElementById('arname').value);
            formDataDoc.append("enname", document.getElementById('enname').value);
            formDataDoc.append("arshortname", document.getElementById('arshortname').value);
            formDataDoc.append("enshortname", document.getElementById('enshortname').value);

            formDataDoc.append("size", document.getElementById('size').value);
            formDataDoc.append("unit", document.getElementById('unit').value);

            formDataDoc.append("file", document.getElementById("imgInpX").files[0]);

            ConfirmFactory.UrlPostRedirect('/Product/UpdateProduct', formDataDoc, '/prod/list');

        }
    }

    $scope.Delete = function (itemcode) {
                
        var formDataDoc = new FormData();

        formDataDoc.append("itemcode", itemcode);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to delete this Product?", "Product/DeleteProduct", formDataDoc, "/prod/list");

    }
}

ProductController.$inject = ['$scope', 'ConfirmFactory', 'ValidateFactory'];
