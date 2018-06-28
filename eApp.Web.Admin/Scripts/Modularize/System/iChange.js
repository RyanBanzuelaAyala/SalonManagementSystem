var iChange = function () {


    var init = function () {

        $(document).on('keypress', '.numeric', function (evt) {
                    
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode;

            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;


        });

        
    };

    return {

        init: init
    }

}();