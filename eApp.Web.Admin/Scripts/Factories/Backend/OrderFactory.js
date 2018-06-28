var OrderFactory = function ($http, ValidateFactory, ConfirmFactory) {

    var OrderInfo = function (data) {

        $.confirm({
            title: ' ',
            draggable: true,
            boxWidth: '75%',
            useBootstrap: false,
            content: 'url:iBranch/_tempAddBranchOrder?ordercode=' + data,
            buttons: {
                formSubmit: {
                    text: 'Add to Order',
                    btnClass: 'btn-default',
                    action: function () {

                     var result = ValidateFactory.validate();

                        if (result == "success") {
                            var formDataDoc = new FormData();

                            formDataDoc.append("itemcode", document.getElementById('itemcode').value);
                            formDataDoc.append("quantity", document.getElementById('quantity').value);                            
                            formDataDoc.append("ordercode", data);

                            ConfirmFactory.UrlPostRedirect('/iBranch/AddProductToOrder', formDataDoc, '/ibranch/orderlist');
    
                        }

                         return false;
                         e.preventDefault();
                    }
                },
                cancel: function () {

                },
            },
            onContentReady: function () {
                // bind to events
                var jc = this;
                this.$content.find('form').on('submit', function (e) {
                    // if the user submits the form by pressing enter in the field.
                    e.preventDefault();
                    jc.$$formSubmit.trigger('click'); // reference the button and click it
                });
            }
        });

    }
        
    return {

        OrderInfo: OrderInfo
    };

}

OrderFactory.$inject = ['$http', 'ValidateFactory', 'ConfirmFactory'];