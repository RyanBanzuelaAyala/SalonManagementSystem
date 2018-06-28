var UserFactory = function ($http, ValidateFactory, ConfirmFactory) {

    var UserInfo = function (data) {

        $.confirm({
            title: ' ',
            draggable: true,
            boxWidth: '50%',
            useBootstrap: false,
            content: 'url:User/_tempAddRole?userid=' + data,
            buttons: {
                formSubmit: {
                    text: 'Add to Role',
                    btnClass: 'btn-default',
                    action: function () {

                        var formDataDoc = new FormData();

                        formDataDoc.append("role", document.getElementById('role').value);
                        formDataDoc.append("userid", document.getElementById('userid').value);

                        ConfirmFactory.UrlPostRedirect('/User/UserRoleSubmit', formDataDoc, '/user/list');


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

    var UserInfoBranch = function (data) {

        $.confirm({
            title: ' ',
            draggable: true,
            boxWidth: '50%',
            useBootstrap: false,
            content: 'url:User/_tempAddBranch?userid=' + data,
            buttons: {
                formSubmit: {
                    text: 'Link to Branch',
                    btnClass: 'btn-default',
                    action: function () {

                        var formDataDoc = new FormData();

                        formDataDoc.append("branchcode", document.getElementById('branchcode').value);
                        formDataDoc.append("userid", document.getElementById('userid').value);

                        ConfirmFactory.UrlPostRedirect('/User/UserBranchSubmit', formDataDoc, '/user/list');


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

        UserInfo: UserInfo,
        UserInfoBranch: UserInfoBranch
    };

}

UserFactory.$inject = ['$http', 'ValidateFactory', 'ConfirmFactory'];