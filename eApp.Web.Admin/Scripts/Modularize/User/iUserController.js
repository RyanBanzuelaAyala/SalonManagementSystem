

var iUserController = function () {

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
                { "data": "userid", "title": "User Id", "class": "tablemid" },
                { "data": "name", "title": "Full Name", "class": "tablemid" },
                { "data": "empid", "title": "Supplier Id" },
                { "data": "role", "title": "USer Role" },
                { "data": "status", "title": "Status" },
                { "data": "password", "title": "Password" }
            ]
        };

        var table = $('#example').DataTable({
            "iDisplayLength": 10,
            "bDestroy": true,
            "autoWidth": true,
            "bLengthChange": false,
            "processing": true,
            "bServerSide": false,
            "sAjaxSource": "User/GetUserCombiAll",
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

                forChildTableX(row.data().userid);
                forChildTableY(row.data().userid);
            }
        });

        function format(d) {

            return '<div class="col-md-12 bg-white">' +
                '<hr />' +
                '<div class="col-md-6 bg-white">' +
                '<h5>USER ROLES</h5>' +
                '<table id="x' + d.userid + '" class="table table-bordered table-hover table-striped" width= "100%"></table><hr />' +
                '</div>' +
                '<div class="col-md-6 bg-white">' +
                '<h5>BRANCH LINK TO USER</h5>' +
                '<table id="y' + d.userid + '" class="table table-bordered table-hover table-striped" width= "100%"></table><hr />' +
                '</div>' +
                '<div class="col-md-12 bg-white"><hr />' +
                '<a href="/sup/' + d.userid + '" class="btn btn-default btn-xs margin-r-5 vwcnn"> VIEW INFO </a>' +
                '<button onclick="angular.element(document.getElementById(\'ucc\')).scope().AddRole(\'' + d.userid + '\');" class="btn btn-default btn-xs margin-r-5"> NEW ROLE </button>' +
                '<button onclick="angular.element(document.getElementById(\'ucc\')).scope().AddBranch(\'' + d.userid + '\');" class="btn btn-default btn-xs margin-r-5"> LINK BRANCH TO USER </button>' +
                '<hr /></div>';


        }

        function forChildTableX(dataX) {

            var dataObjectX = {
                columns: [
                    { "data": "role", "title": "User Role", "class": "tablemid" },
                    { "data": "status", "title": "Status", "class": "tablemid" },
                    {
                        "data": "itemcode", "width": "2%",
                        render: function (data, type, full, row) {
   
                            return '<button onclick="angular.element(document.getElementById(\'ucc\')).scope().RemoveRole(\'' + full.role + '\', \'' + dataX + '\');" class="btn btn-default btn-xs margin-r-5">remove</button>';

                        }, "title": ""
                    }
                ]
            };

            var tableX = $('#x' + dataX).DataTable({
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
                "sAjaxSource": "User/GetUserCombiAllRole?userid=" + dataX,
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

        function forChildTableY(dataX) {

            var dataObjectX = {
                columns: [
                    { "data": "branchcode", "title": "Branch Code", "class": "tablemid" },
                    { "data": "branchname", "title": "Branch Name", "class": "tablemid" },
                    { "data": "status", "title": "Status", "class": "tablemid" },
                    {
                        "data": "itemcode", "width": "2%",
                        render: function (data, type, full, row) {

                            return '<button onclick="angular.element(document.getElementById(\'ucc\')).scope().RemoveBranch(\'' + full.branchcode + '\', \'' + dataX + '\');" class="btn btn-default btn-xs margin-r-5">remove</button>';

                        }, "title": ""
                    }

                ]
            };

            var tableX = $('#y' + dataX).DataTable({
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
                "sAjaxSource": "User/GetUserCombiAllBranch?userid=" + dataX,
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
    
    var initXX = function (userid) {

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
               
            ]
        };

        var table = $('#exampleD').DataTable({
            "iDisplayLength": 10,
            "bDestroy": true,
            "autoWidth": true,
            "bLengthChange": false,
            "processing": true,
            "bServerSide": false,
            "sAjaxSource": "User/GetUserCombiAllBranchAvailable?userid=" + userid,
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
        $('#exampleD tbody').on('click', 'td', function () {


            var tr = $(this).closest('tr');
            var row = table.row(tr);

            document.getElementById("branchcode").value = row.data().branchcode;
            
        });
        

    };


    return {
        init: init,
        initXX: initXX
    }

}();

