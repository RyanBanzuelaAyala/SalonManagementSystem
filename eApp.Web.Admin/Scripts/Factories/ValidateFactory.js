var ValidateFactory = function () {
    
    var validate = function () {

        var isValid;

        $(".validate").each(function () {

            var element = $(this);

            var x = element.val();

            if (element.val() == "") {

                notify("Please check required fields..", "danger");

                element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');

                isValid = "error";

                return false;

            } else {

                if (element.hasClass("email")) {

                    var atpos = x.indexOf("@");
                    var dotpos = x.lastIndexOf(".");
                    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {

                        notify("Not a valid e-mail address..", "danger");

                        element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');

                        isValid = "error";

                        return false;

                    }
                }
                else {

                    element.css("border-color", "#ccc").css('box-shadow', 'none');

                    isValid = "success";

                }

                if (element.hasClass("number")) {

                    if (isNaN(x)) {

                        notify("Not a valid numbers..", "danger");

                        element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');

                        isValid = "error";

                        return false;

                    }
                    else {

                        if (element.hasClass("mobile")) {

                            if (x.length < 10 || x.length > 10) {

                                notify("Please enter valid mobile number..", "danger");

                                element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');

                                isValid = "error";

                                return false;

                            }
                            else {

                                element.css("border-color", "#ccc").css('box-shadow', 'none');

                                isValid = "success";
                            }
                        }
                        else {

                            element.css("border-color", "#ccc").css('box-shadow', 'none');

                            isValid = "success";
                        }

                    }
                }
                else {

                    element.css("border-color", "#ccc").css('box-shadow', 'none');

                    isValid = "success";

                }

            }

        });

        return isValid;
    }

    var validateX = function () {

        var isValid = "success";

        $(".validate").each(function () {

            var element = $(this);

            var x = element.val();
            //box-shadow: 0px 0px 50px #ddd
            //0px 0px 50px #ddd
            if (element.val() == "") {
                element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');
                isValid = "error";

                return false;

            } else {

                if (element.hasClass("email")) {
                    var atpos = x.indexOf("@");
                    var dotpos = x.lastIndexOf(".");
                    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {

                        element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');
                        isValid = "error";

                        return false;
                    }
                }
                else {
                    element.css("border-color", "#ccc").css('box-shadow', 'none');
                    isValid = "success";
                }

                if (element.hasClass("number")) {

                    if (isNaN(x)) {
                        element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');
                        isValid = "error";

                        return false;
                    }
                    else {

                        if (element.hasClass("mobile")) {

                            if (x.length < 10 || x.length > 10) {
                                element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');
                                isValid = "error";

                                return false;
                            }
                            else {
                                element.css("border-color", "#ccc").css('box-shadow', 'none');
                                isValid = "success";
                            }
                        }
                        else {
                            element.css("border-color", "#ccc").css('box-shadow', 'none');
                            isValid = "success";
                        }
                    }
                }
                else {
                    element.css("border-color", "#ccc").css('box-shadow', 'none');
                    isValid = "success";
                }
            }
        });

        return isValid;
    }
    
    var validateCustom = function (obj) {

        var isValid;

        $("." + obj).each(function () {

            var element = $(this);

            var x = element.val();

            if (element.val() == "") {

                notify("Please check required fields..", "danger");

                element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');

                isValid = "error";

                return false;

            } else {

                if (element.hasClass("email")) {

                    var atpos = x.indexOf("@");
                    var dotpos = x.lastIndexOf(".");
                    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {

                        notify("Not a valid e-mail address..", "danger");

                        element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');

                        isValid = "error";

                        return false;

                    }
                }
                else {

                    element.css("border-color", "#ccc").css('box-shadow', 'none');

                    isValid = "success";

                }

                if (element.hasClass("number")) {

                    if (isNaN(x)) {

                        notify("Not a valid numbers..", "danger");

                        element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');

                        isValid = "error";

                        return false;

                    }
                    else {

                        if (element.hasClass("mobile")) {

                            if (x.length < 10 || x.length > 10) {

                                notify("Please enter valid mobile number..", "danger");

                                element.css("border-color", "#605ca8").css('box-shadow', '0px 0px 50px #ddd');

                                isValid = "error";

                                return false;

                            }
                            else {

                                element.css("border-color", "#ccc").css('box-shadow', 'none');

                                isValid = "success";
                            }
                        }
                        else {

                            element.css("border-color", "#ccc").css('box-shadow', 'none');

                            isValid = "success";
                        }

                    }
                }
                else {

                    element.css("border-color", "#ccc").css('box-shadow', 'none');

                    isValid = "success";

                }

            }

        });

        return isValid;
    }
    
    var hasValue = function () {

        var rVal;

        $(".include").each(function () {
            
            var element = $(this).val();

            if (element.trim() != "") {
                
                rVal = "success";

            }            
            
        });

        return rVal;
    }
    
    var notify = function (msg, type) {

        $.notify({
            message: msg
        }, {
            type: type,
            z_index: 9999999999999,
            animate: {
		        enter: 'animated bounceInDown',
		        exit: 'animated bounceOutUp'
	        }
        });

    };

    return {

        validate: validate,
        validateX: validateX,
        validateCustom: validateCustom,
        hasValue: hasValue,
        notify: notify

    };

}

ValidateFactory.$inject = [];