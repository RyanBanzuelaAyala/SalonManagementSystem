
var iBranchOrderController = function () {
    
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
                { "data": "servicecode", "title": "Service Code", "class": "tablemid" },                
                { "data": "ordercode", "title": "Order Code", "class": "tablemid" },                
                { "data": "orderby", "title": "Order By", "class": "tablemid" },
                {
                    "data": "datecreated", "class": "tablemid",
                    render: function (data, type, full, row) {

                        return moment(full.datecreated).format('DD-MMM-YYYY HH:mm:ss');

                    }, "title": "Date <br> Created"
                },
                { "data": "approvedby", "title": "Approved By", "class": "tablemid" },  
                {
                        "data": "dateapproved", "class": "tablemid",
                        render: function (data, type, full, row) {

                            return moment(full.dateapproved).format('DD-MMM-YYYY HH:mm:ss');

                        }, "title": "Date <br> Approved"
                }, 
                { "data": "status", "title": "Status", "class": "tablemid" }               
            ]
        };

        var table = $('#example').DataTable({            
            "bDestroy": true,
            "autoWidth": true,
            "bLengthChange": false,
            "processing": true,
            "bServerSide": false,
            "sAjaxSource": "iBranch/GetCombiAllBranchOrdersOA",
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

                forChildTable(row.data().ordercode, row.data().status);
            }
        });
        
        function format(d) {

            if (d.status == "blank") {

                return '<div class="col-md-12 bg-white">' +
                    '<hr />' +
                    '<table id="' + d.ordercode + '" class="table table-bordered table-hover table-striped" width= "100%"></table><hr />' +
                    '<button onclick="angular.element(document.getElementById(\'bkbcc\')).scope().AddProductToOrder(\'' + d.ordercode + '\');" class="btn btn-default btn-xs margin-r-5">ADD PRODUCTS TO ORDER</button>' +
                    '<button onclick="angular.element(document.getElementById(\'bkbcc\')).scope().FinalOrder(\'' + d.ordercode + '\');" class="btn btn-default btn-xs margin-r-5">SUBMIT ORDER</button>' +
                    '<hr /></div>';
            }
            else {
                return '<div class="col-md-12 bg-white">' +
                    '<hr />' +
                    '<table id="' + d.ordercode + '" class="table table-bordered table-hover table-striped" width= "100%"></table><hr />' +                    
                    '</div>';
            }
        }


        function forChildTable(dataX, dataY) {

            var dataObjectX = {
                columns: [
                    {
                        "className": 'details-control',
                        "width": "2%",
                        "orderable": false,
                        "data": null,
                        "defaultContent": '<i class="fa fa-plus"></i>'
                    },
                    { "data": "itemcode", "title": "Item Code", "class": "tablemid" },      
                    { "data": "itemname", "title": "Item Name", "class": "tablemid" },      
                    { "data": "quantity", "title": "Quantity", "class": "tablemid" },      
                    { "data": "price", "title": "Price", "class": "tablemid" },      
                    {
                        "data": "itemcode", "class": "tablemid", "title": "", "width": "5%",
                        render: function (data, type, full, row) {

                            if (dataY == "blank") {
                                return '<button onclick="angular.element(document.getElementById(\'bkbcc\')).scope().DeleteBranchOrder(\'' + full.itemcode + '\', \'' + dataX + '\');" class="btn btn-default btn-xs margin-r-5">remove</button>';
                            }
                            else {
                                return ' ';
                            }
                            
                        }
                    },
                    
                ]
            };


            var tableX = $('#' + dataX).DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ],
                "bDestroy": true,
                "bFilter": false,
                "autoWidth": true,
                "bLengthChange": false,
                "processing": true,
                "bServerSide": false,
                "sAjaxSource": "iBranch/GetCombiAllBranchOrderList?ordercode=" + dataX,
                "sAjaxDataProp": "",
                "columns": dataObjectX.columns,
                "rowReorder": {
                    selector: 'td:nth-child(0)'
                },
                "autoWidth": true,
                'bLengthChange': false,
                'columnDefs': [
                    {
                        'targets': 0,
                        'checkboxes': {
                            'selectRow': false
                        }
                    }
                ],
                'select': {
                    'style': 'single'
                },
                'order': [[1, 'asc']]
            });

        }
    };
    
    return {
        init: init
    }

}();

