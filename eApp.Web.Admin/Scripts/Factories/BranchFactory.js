var BranchFactory = function ($http, ValidateFactory, ConfirmFactory) {

    var BranchInfo = function (data) {

        $.confirm({
            title: ' ',
            draggable: true,
            boxWidth: '75%',
            useBootstrap: false,
            content: 'url:Department/_tempAddService?deptcode=' + data,
            buttons: {
                formSubmit: {
                    text: 'Add to Department',
                    btnClass: 'btn-default',
                    action: function () {

                        var formDataDoc = new FormData();

                        formDataDoc.append("ServCode", document.getElementById('ServCode').value);
                        formDataDoc.append("DeptCode", data);

                        ConfirmFactory.UrlPostRedirect('/Department/AddDepartmentServices', formDataDoc, '/department/list');
    
                        
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

        BranchInfo: BranchInfo
    };

}

BranchFactory.$inject = ['$http', 'ValidateFactory', 'ConfirmFactory'];