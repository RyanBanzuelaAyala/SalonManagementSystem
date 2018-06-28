var ServiceFactory = function ($http, ValidateFactory, ConfirmFactory) {

    var ServiceInfo = function (data) {

        $.confirm({
            title: ' ',
            draggable: true,
            boxWidth: '75%',
            useBootstrap: false,
            content: 'url:Services/_tempAddService?servicecode=' + data,
            buttons: {
                formSubmit: {
                    text: 'Add to Services',
                    btnClass: 'btn-default',
                    action: function () {

                        var formDataDoc = new FormData();

                        formDataDoc.append("itemcode", document.getElementById('itemcode').value);
                        formDataDoc.append("ServCode", data);

                        ConfirmFactory.UrlPostRedirect('/Services/AddProductToServices', formDataDoc, '/services/list');
    
                        
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

        ServiceInfo: ServiceInfo
    };

}

ServiceFactory.$inject = ['$http', 'ValidateFactory', 'ConfirmFactory'];