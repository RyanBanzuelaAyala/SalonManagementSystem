var PricingController = function ($scope, ConfirmFactory, ValidateFactory) {

    $scope.Process = function () {

        var result = ValidateFactory.validate();

        if (result == "success") {

            var formDataDoc = new FormData();

            formDataDoc.append("itemcode", document.getElementById('itemcode').value);
            
            formDataDoc.append("sellingprice", document.getElementById('sellingprice').value);
            formDataDoc.append("purchasingprice", document.getElementById('purchasingprice').value);
            formDataDoc.append("vatprice", document.getElementById('vatprice').value);
            formDataDoc.append("remarks", document.getElementById('remarks').value);            

            ConfirmFactory.UrlPostRedirect('/Pricing/AddProductPrice', formDataDoc, '/prod/list');

        }
    }
}

PricingController.$inject = ['$scope', 'ConfirmFactory', 'ValidateFactory'];
