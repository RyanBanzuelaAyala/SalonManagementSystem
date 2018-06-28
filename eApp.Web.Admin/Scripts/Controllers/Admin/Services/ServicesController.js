var ServicesController = function ($scope, ConfirmFactory, ValidateFactory, ServiceFactory) {

    $scope.Process = function () {

        var result = ValidateFactory.validate();

        if (result == "success") {

            var formDataDoc = new FormData();

            formDataDoc.append("servicescode", document.getElementById('servicescode').value);
            formDataDoc.append("servicename", document.getElementById('servicename').value);
            formDataDoc.append("description", document.getElementById('description').value);

            formDataDoc.append("nprice", document.getElementById('nprice').value);
            formDataDoc.append("sprice", document.getElementById('sprice').value);

            formDataDoc.append("minutes", document.getElementById('minutes').value);
            formDataDoc.append("hours", document.getElementById('hours').value);

            formDataDoc.append("fixedcommission", document.getElementById('fixedcommission').value);
            formDataDoc.append("remarks", document.getElementById('remarks').value);
            
            formDataDoc.append("file", document.getElementById("imgInpX").files[0]);

            ConfirmFactory.UrlPostRedirect('/Services/AddServices', formDataDoc, '/services/list');

        }
    }

    $scope.ProcessServices = function (output) {
    
        var formDataDoc = new FormData();

        formDataDoc.append("datalist", output);
       
        ConfirmFactory.UrlPostRedirect('/Services/AddServicesProduct', formDataDoc, '/services/list');
        
    }

    $scope.AddProductService = function (data) {

        console.log(data);

        ServiceFactory.ServiceInfo(data);

    }

    $scope.DeletetProductService = function (itemcode, servicecode) {

        var formDataDoc = new FormData();

        formDataDoc.append("itemcode", itemcode);
        formDataDoc.append("ServCode", servicecode);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to remove this Product?", "Services/RemoveProductToServices", formDataDoc, "/services/list");

    }

    $scope.DeleteService = function (servicecode) {

        var formDataDoc = new FormData();

        formDataDoc.append("ServCode", servicecode);

        ConfirmFactory.UrlConfirmPostRedirect("Are you sure want to remove this Service? <br /> Note: All products in this Services will be deleted also.", "Services/RemoveServices", formDataDoc, "/services/list");

    }
}

ServicesController.$inject = ['$scope', 'ConfirmFactory', 'ValidateFactory', 'ServiceFactory'];
