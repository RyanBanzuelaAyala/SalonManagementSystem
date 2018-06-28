

var iPricingController = function () {

    var table;

    var init = function () {

        var dataObject = {
            columns: [
                {
                    "className": 'details-control',
                    "width": "2%",
                    "orderable": false,
                    "data": null,
                    "defaultContent": '<i class="fa fa-plus"></i>'
                },
                {
                    "data": "itemcode", "class": "tablemid", "width": "10%",
                    render: function (data, type, full, row) {

                        return '<img class="img-responsive img-circle" style="height: 35px; width: 35px" src="/Uploadpic/thmb' + full.itemcode + 'P.jpg">';


                    }, "title": "Product <br> Image"
                },
                { "data": "itemcode", "title": "Item <br> Code", "class": "tablemid" },
                { "data": "barcode", "title": "Barcode", "class": "tablemid" },                
                {
                    "data": "iteminfo", "class": "tablemid",
                    render: function (data, type, full, row) {

                        return full.arname + " - " + full.enname;                      

                    }, "title": "Product Name"
                },
                {
                    "data": "priceinfo", "class": "tablemid",
                    render: function (data, type, full, row) {

                        if(full.priceinfo != null) {
                            return full.priceinfo.sellingprice;                      
                        } else {
                            return "";
                        }

                    }, "title": "Selling <br> Price"
                },
                {
                    "data": "priceinfo", "class": "tablemid",
                    render: function (data, type, full, row) {

                        if(full.priceinfo != null) {
                            return full.priceinfo.purchasingprice;                      
                        } else {
                            return "";
                        }                                  

                    }, "title": "Purchasing <br> Price"
                },
                {
                    "data": "priceinfo", "class": "tablemid",
                    render: function (data, type, full, row) {

                        if(full.priceinfo != null) {
                            return full.priceinfo.vatprice;                      
                        } else {
                            return "";
                        }         

                    }, "title": "Vat <br> Price"

                },
                {
                    "data": "itemcode", "width": "2%",
                    render: function (data, type, full, row) {

                        if(full.priceinfo != null) {
                            return '<a href="/pricing/product/' + full.itemcode + '" class="btn btn-default btn-xs">EDIT PRICE</a>';                 
                        } else {
                            return '<a href="/pricing/product/' + full.itemcode + '" class="btn btn-default btn-xs">ADD PRICE</a>';                 
                        }  
                        

                    }, "title": ""
                }
            ]
        };

        var table = $('#example').DataTable({
            "iDisplayLength": 10,
            "bDestroy": true,
            "autoWidth": true,
            "bLengthChange": false,
            "processing": true,
            "bServerSide": false,
            "sAjaxSource": "Pricing/GetProductPriceList",
            "sAjaxDataProp": "",
            "columns": dataObject.columns,
            "rowReorder": {
                selector: 'td:nth-child(0)'
            },
            "autoWidth": true,
            'bLengthChange': false,
            'columnDefs': [
                {
                    'targets': 0,
                    'checkboxes': {
                        'selectRow': true
                    }

                }
            ],
            'select': {
                'style': 'single'
            },
            initComplete: function () {

                $('img').error(function () {
                    $(this).attr('src', '/Content/img/product.png');
                });

            }
        });



    };
    

    return {
        init: init
    }

}();

