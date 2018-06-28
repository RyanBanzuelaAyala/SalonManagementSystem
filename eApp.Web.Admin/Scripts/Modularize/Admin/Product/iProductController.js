

var iProductController = function () {

    var table;

    var init = function () {

        var dataObject = {
            columns: [
                {
                    "className": 'details-control text-center',
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
                { "data": "itemcode", "title": "Item Code", "class": "tablemid" },
                { "data": "barcode", "title": "Barcode", "class": "tablemid" },
                {
                    "data": "arname", "class": "tablemid",
                    render: function (data, type, full, row) {

                        return full.arname + " - " + full.enname

                    }, "title": "Product Name"
                },
                { "data": "remarks", "title": "Remarks" },
                { "data": "status", "title": "Status" }
            ]
        };

        var table = $('#example').DataTable({
            "iDisplayLength": 10,
            "bDestroy": true,
            "autoWidth": true,
            "bLengthChange": false,
            "processing": true,
            "bServerSide": false,
            "sAjaxSource": "Product/GetProductCombiAll",
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

        // Add event listener for opening and closing details
        $('#example tbody').on('click', 'td.details-control', function () {
            var tr = $(this).closest('tr');
            var row = table.row(tr);

            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                tr.removeClass('shown');
            }
            else {
                // Open this row
                row.child(format(row.data())).show();
                tr.addClass('shown');
            }
        });

    };

    function format(d) {

        return '<div class="col-md-12 bg-white">' +
                '<hr />' +                
                '<a href= "/pricing/product/' + d.itemcode + '" class="btn btn-default btn-xs margin-r-5">SET PRODUCT PRICE </a>' +
                '<a href= "/prod/edit/' + d.itemcode + '" class="btn btn-default btn-xs margin-r-5"> EDIT PRODUCT INFO </a>' +                
                '<button onclick="angular.element(document.getElementById(\'ppd\')).scope().Delete(\'' + d.itemcode + '\');" class="btn btn-default btn-xs margin-r-5">DELETE PRODUCT</button>'+
                '<hr /></div>';
        
    }
    

    return {
        init: init
    }

}();

