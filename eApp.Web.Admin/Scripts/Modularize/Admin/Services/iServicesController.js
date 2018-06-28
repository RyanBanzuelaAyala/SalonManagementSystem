

var iServicesController = function () {
    
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
                    "data": "servicescode", "class": "tablemid", "width": "10%",
                    render: function (data, type, full, row) {

                        return '<img class="img-responsive img-circle" style="height: 35px; width: 35px" src="/Uploadpic/thmb' + full.servicescode + 'S.jpg">';


                    }, "title": "Service <br> Image"
                },
                { "data": "servicescode", "title": "Services Code", "class": "tablemid" },                
                { "data": "description", "title": "Description", "class": "tablemid" },
                { "data": "nprice", "title": "Normal Price", "class": "tablemid" },
                { "data": "sprice", "title": "Selling Price", "class": "tablemid" },
                {
                    "data": "hours", "class": "tablemid",
                    render: function (data, type, full, row) {

                       return full.hours + " : " + full.minutes

                    }, "title": "Services Time"
                },
                { "data": "fixedcommission", "title": "Commissions", "class": "tablemid" },
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
            "sAjaxSource": "Services/GetServicesCombiAll",
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

                forChildTable(row.data().servicescode);
            }
        });


        function format(d) {

            return '<div class="col-md-12 bg-white">' +
                '<hr />' +
                '<table id="' + d.servicescode + '" class="table table-bordered table-hover table-striped" width= "100%"></table><hr />' +
                '<button onclick="angular.element(document.getElementById(\'ppd\')).scope().AddProductService(\'' + d.servicescode + '\');" class="btn btn-default btn-xs margin-r-5">ADD PRODUCTS</button>' +
                '<button onclick="angular.element(document.getElementById(\'ppd\')).scope().DeleteService(\'' + d.servicescode + '\');" class="btn btn-default btn-xs margin-r-5 pull-right">DELETE SERVICE</button>' +
                '<a href="/services/edit/' + d.servicescode + '" class="btn btn-default btn-xs">EDIT SERVICES INFO</a><hr /></div>';


        }

        function forChildTable(dataX) {


            var dataObjectX = {
                columns: [
                    { "data": "itemcode", "title": "Item Code", "class": "tablemid", "width": "15%" },
                    {
                        "data": "arname", "class": "tablemid",
                        render: function (data, type, full, row) {

                            return full.arname + " - " + full.enname

                        }, "title": "Product Name"
                    },
                    {
                        "data": "size", "class": "tablemid", "title": "Size", "width": "25%",
                        render: function (data, type, full, row) {

                            return full.size + " - " + full.unit;
                        }
                    },
                    {
                        "data": "itemcode", "class": "tablemid", "title": "", "width": "5%",
                        render: function (data, type, full, row) {

                            return '<button onclick="angular.element(document.getElementById(\'ppd\')).scope().DeletetProductService(\'' + full.itemcode + '\', \'' + dataX + '\');" class="btn btn-default btn-xs margin-r-5">REMOVE</button>';
                        }
                    },
                    
                ]
            };


            var tableX = $('#' + dataX).DataTable({
                //"iDisplayLength": 6,
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
                "sAjaxSource": "Services/GetServicesCombiAllProduct?servicecode=" + dataX,
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
    
    var initX = function () {

        var output = new Array();
        var listDB = new Array();
        var totalprice = new Array();
        var formDataDoc = new FormData;
        var tableX = document.getElementById("exampleChild00");
        var servicecode = document.getElementById("servicescode").value;
        var combinecodevin;

        console.log(servicecode);

        var table;
        var olddv;

        $("#exampleChild00 tr").remove(); 

        $('#example').empty().unbind('select').unbind('deselect');
        
        var dataObject = {
            columns: [                
                { "data": "itemcode", "title": "Item <br>Code", "class": "tablemid", "width": "15%" },                
                {
                    "data": "arname", "class": "tablemid",
                    render: function (data, type, full, row) {

                       return full.arname + " - " + full.enname

                    }, "title": "Product <br> Name"
                },
                {
                    "data": "size", "class": "tablemid", "title": "Size", "width": "25%",
                    render: function (data, type, full, row) {

                        return full.size + " - " + full.unit;
                    }
                },
                {
                    "data": "price", "class": "tablemid", "title": "Price", "width": "15%",
                    render: function (data, type, full, row) {

                        if(full.price != null) {

                            return full.price.sellingprice;

                        } else {

                            return "0.00";

                        }
                    }
                }
            ]
        };

        var table = $('#example').DataTable({            
            "bDestroy": true,
            "autoWidth": true,
            "bLengthChange": false,
            "processing": true,
            "bServerSide": false,
            "sAjaxSource": "Product/GetProductCombiAllInfo",
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
                'style': 'multiple'
            },
            initComplete: function () {
            
            }
        });

        table            
            .on('select', function (e, dt, type, indexes) {

                var data = table.row(indexes).data();                         
                var vin = data.itemcode;

                if (data.price != null) {
                    output.push(vin, data.price.sellingprice);
                    totalprice.push(data.price.sellingprice);
                    addToList(vin, data.price.sellingprice);
                    combinecodevin = servicecode + "-" + vin + "-" + data.price.sellingprice;
                }
                else {
                    output.push(vin, "0.00");
                    addToList(vin, "0.00");
                    combinecodevin = servicecode + "-" + vin + "-0.00";
                }

                listDB.push(combinecodevin);

            }).on('deselect', function (e, dt, type, indexes) {

                var data = table.row(indexes).data();
                var vin = data.itemcode;

                if (data.price != null) {
                    output.pop(vin, data.price.sellingprice);
                    totalprice.pop(data.price.sellingprice);
                    delToList(vin, data.price.sellingprice);
                    combinecodevin = servicecode + "-" + vin + "-" + data.price.sellingprice;
                }
                else {
                    output.pop(vin, "0.00");
                    delToList(vin, "0.00");
                    combinecodevin = servicecode + "-" + vin + "-0.00";
                }
                                
                listDB.pop(combinecodevin);

            });
            
            function addToList(val1, val2) {

                var row = tableX.insertRow(0);
                var cell1 = row.insertCell(0);
                var cell2 = row.insertCell(1);

                row.className = "ppr";

                cell1.className = "xDD";
                cell1.innerHTML = val1;

                cell2.className = "xDD";
                cell2.innerHTML = val2;

                var getprice = $('#tprice').html();

                console.log(val2);

                $('#tprice').html(sum(totalprice));
                
            }

            function sum(array) {
                var total = 0;
                for (var i = 0; i < array.length; i++) total += Number(array[i]);
                return total;
            }
            
            function delToList(val1, val2) {

                $("#exampleChild00 tr.ppr td.xDD").each(function () {
                    var thisRow = $(this);

                    //console.log(thisRow.text());
                    // note the `==` operator
                    if (thisRow.text() == val1) {
                        thisRow.remove();
                        // OR thisRow.hide();
                    }

                    if (thisRow.text() == val2) {
                        thisRow.remove();
                        // OR thisRow.hide();
                    }

                    $('#tprice').html(Math.abs(subtract(totalprice)));

                });

            }

            function subtract(array) {
                var total = 0;
                for (var i = 0; i < array.length; i++) total -= Number(array[i]);

                return total;
            }
            
            $('#btnser').on('click', function () {
            
                angular.element(document.getElementById('ssc')).scope().ProcessServices(listDB);

            });

    };    
    
    var initXX = function (data) {

        var dataObject = {
            columns: [
                {
                    "className": 'details-control',
                    "width": "2%",
                    "orderable": false,
                    "data": null,
                    "defaultContent": '<i class="fa fa-plus"></i>'
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

        var table = $('#exampleD').DataTable({
            "iDisplayLength": 10,
            "bDestroy": true,
            "autoWidth": true,
            "bLengthChange": false,
            "processing": true,
            "bServerSide": false,
            "sAjaxSource": "Services/GetProductCombiAllServicesAvailable?servicecode=" + data,
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
        $('#exampleD tbody').on('click', 'td', function () {

            var tr = $(this).closest('tr');
            var row = table.row(tr);
            
            document.getElementById("itemcode").value = row.data().itemcode;

        });


    };


    return {
        init: init,
        initX: initX,
        initXX: initXX
    }

}();

