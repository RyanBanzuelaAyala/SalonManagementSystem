
var iBranchController = function () {
    
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
                { "data": "branchcode", "title": "Branch Code", "class": "tablemid" },                
                { "data": "branchname", "title": "Branch Name", "class": "tablemid" },
                { "data": "remarks", "title": "Remarks", "class": "tablemid" }                
            ]
        };

        var table = $('#example').DataTable({            
            "bDestroy": true,
            "autoWidth": true,
            "bLengthChange": false,
            "processing": true,
            "bServerSide": false,
            "sAjaxSource": "Branch/GetCombiAllBranch",
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

                forChildTable(row.data().deptcode);
            }
        });
        
        function format(d) {

            return '<div class="col-md-12 bg-white">' +
                '<hr />' +                
                '<a href="/branch/edit/' + d.branchcode + '" class="btn btn-default btn-xs margin-r-5"> EDIT BRANCH INFO </a>' +
                '<button onclick="angular.element(document.getElementById(\'brc\')).scope().DeleteBranch(\'' + d.branchcode + '\');" class="btn btn-default btn-xs margin-r-5">DELETE BRANCH</button>' +                
                '<hr /></div>';

        }

        function forChildTable(dataX) {


            var dataObjectX = {
                columns: [
                    { "data": "servicecode", "title": "Service Code", "class": "tablemid" },
                    { "data": "servicename", "title": "Service Name", "class": "tablemid" },
                    { "data": "status", "title": "Status", "class": "tablemid" },
                    {
                        "data": "servicecode", "class": "tablemid", "title": "", "width": "5%",
                        render: function (data, type, full, row) {

                            return '<button onclick="angular.element(document.getElementById(\'ddp\')).scope().DeletetDepartmentService(\'' + full.servicecode + '\', \'' + dataX + '\');" class="btn btn-default btn-xs margin-r-5">REMOVE</button>';
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
                "sAjaxSource": "Department/GetDepartmentCombiAllServices?deptcode=" + dataX,
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

    var initX = function (data) {

        var dataObject = {
            columns: [       
                {
                    "className": 'details-control',
                    "width": "2%",
                    "orderable": false,
                    "data": null,
                    "defaultContent": '<i class="fa fa-plus"></i>'
                },
                { "data": "servicescode", "title": "Services Code", "class": "tablemid" },
                { "data": "description", "title": "Description", "class": "tablemid" },
                { "data": "nprice", "title": "Normal Price", "class": "tablemid" },
                { "data": "sprice", "title": "Selling Price", "class": "tablemid" }
            ]
        };

        var table = $('#exampleD').DataTable({
            "iDisplayLength": 10,
            "bDestroy": true,
            "autoWidth": true,
            "bLengthChange": false,
            "processing": true,
            "bServerSide": false,
            "sAjaxSource": "Department/GetDepartmentCombiAllServicesAvailable?deptcode=" + data,
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

            document.getElementById("ServCode").value = row.data().servicescode;
                        
        });


    };
    
    return {
        init: init,
        initX: initX
    }

}();

