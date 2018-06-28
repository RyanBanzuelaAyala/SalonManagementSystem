var ConfirmFactory = function ($http, dnbservices) {

    var UrlConfirmPostRedirect = function (content, url, formData, urlRedirect) {

        $.confirm({
            title: false,
            boxWidth: '30%',
            closeAnimation: 'bottom',
            useBootstrap: true,
            animateFromElement: true,
            bgOpacity: 0.60,
            content: content,
            autoClose: 'No|8000',
            buttons: {
                Yes: {
                    show: false,
                    action: function () {
                    

                        var self = this;
                        //$('.jconfirm-content-pane').hide();
                        self.$content.find('.jconfirm-content-pane').hide();

                        this.buttons.No.hide();
                        this.buttons.Yes.disable();
                        this.buttons.Yes.setText('Processing..');
                        
                        $http.post(url, formData, {
                            withCredentials: true,
                            headers: { 'Content-Type': undefined },
                            transformRequest: angular.identity
                        }).success(function (response) {

                            self.buttons.Yes.removeClass('btn-default');
                            
                            self.buttons.Yes.addClass('btn-success');
                            self.buttons.Yes.setText('Redirecting..');

                            window.location = urlRedirect;
                                    
                        }).error(function () {
                            self.buttons.ok.addClass('btn-danger');
                            self.buttons.ok.setText('Error encountered..');
                            self.buttons.ok.enable();
                        });

                        return false;
                    }
                },
                No: function () {
                    close
                },
            }
        });


    };
    
    var UrlPostRedirect = function (content, formData, urlRedirect) {

        $.confirm({
            title: false,
            boxWidth: '30%',
            useBootstrap: true,
            offsetTop: 0,
            bgOpacity: 0.60,
            content: function () {

                var self = this;
                //$('.jconfirm-content-pane').hide();
                self.$content.find('.jconfirm-content-pane').hide();

                self.buttons.ok.disable();
                self.buttons.ok.addClass('btn-default');
                self.buttons.ok.setText('Processing..');

                $http.post(content, formData, {
                    withCredentials: true,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                }).success(function (response) {
                    self.buttons.ok.removeClass('btn-default');

                    if (response.trim() == "true" || response.trim() == "True") {
                        self.buttons.ok.addClass('btn-success');
                        self.buttons.ok.setText('Redirecting..');
                        window.location = urlRedirect;
                    }
                    else {
                        self.buttons.ok.addClass('btn-danger');
                        self.buttons.ok.setText('Error encountered..');
                        self.buttons.ok.enable();
                    }
                }).error(function () {
                    self.buttons.ok.addClass('btn-danger');
                    self.buttons.ok.setText('Error encountered..');
                    self.buttons.ok.enable();
                });
            },
            buttons: {
                ok: {
                    isHidden: false,
                    btnClass: 'btn-block'
                }
            }
        });

    };
    
    var UrlPostRedirectX = function (content, formData, urlRedirect) {

        $.confirm({
            title: false,
            boxWidth: '30%',
            useBootstrap: true,
            offsetTop: 0,
            bgOpacity: 0.60,
            content: function () {

                var self = this;
                $('.jconfirm-content-pane').hide();
                //self.$content.find('.jconfirm-content-pane').hide();

                self.buttons.ok.disable();
                self.buttons.ok.addClass('btn-default');
                self.buttons.ok.setText('Processing..');

                $http.post(content, formData, {
                    withCredentials: true,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                }).success(function (response) {
                    self.buttons.ok.removeClass('btn-default');

                    if (response.trim() == "true" || response.trim() == "True") {
                        self.buttons.ok.addClass('btn-success');
                        self.buttons.ok.setText('Redirecting..');
                        window.location = urlRedirect;
                    }
                    else {

                        console.log(response.trim());

                        if (response.trim() == "url") {
                            self.buttons.ok.addClass('btn-success');
                            self.buttons.ok.setText('Redirecting..');
                            window.location = "/";
                        }
                        else {
                            self.buttons.ok.addClass('btn-danger');
                            self.buttons.ok.setText('Error encountered..');
                            self.buttons.ok.enable();
                        }
                    }
                }).error(function () {
                    self.buttons.ok.addClass('btn-danger');
                    self.buttons.ok.setText('Error encountered..');
                    self.buttons.ok.enable();
                });
            },
            buttons: {
                ok: {
                    isHidden: false,
                    btnClass: 'btn-block'
                }
            }
        });

    };


    return {
        
        UrlConfirmPostRedirect: UrlConfirmPostRedirect,
        UrlPostRedirectX: UrlPostRedirectX,
        UrlPostRedirect: UrlPostRedirect
    };

}

ConfirmFactory.$inject = ['$http', 'dnbservices'];