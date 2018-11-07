var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_products").addClass("active");
    Preload();
    InitDT("#dtProduct");
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            $.each(result.d.data, function (indexInArray, valueOfElement) {
                $("." + indexInArray).html(valueOfElement);
            });

            $(".EmployeeName").html(result.d.data.Employee.Name);
        }
        else {
            if (result.d.message == 'Invalid token!') {
                Logout();
                //window.location = 'login-soft.aspx?returnUrl=' + window.location.pathname;
            }
        }
    };
    REST.sendRequest({
        'c': 'admindashboard',
        'm': 'preload',
        'data': {
            'RequestData': []
        }
    });
}

function InitDT(element) {
    $(element).each(function () {
        $(element).dataTable().fnDestroy();
    });
    var e = $(element).dataTable({
        oLanguage: {
            sProcessing: '<span>Processing...</span>',
            sLengthMenu: "_MENU_ records",
            oPaginate: {
                sPrevious: "Prev",
                sNext: "Next"
            },
            sAjaxRequestGeneralError: "Could not complete request. Please check your internet connection",
            sEmptyTable: "No records to display",
            sZeroRecords: "No matching records found"
        },
        aLengthMenu: [
        [5, 10, 20, 50, -1],
        [5, 10, 20, 50, "All"]
        ],
        iDisplayLength: 10,
        bSortClasses: false,
        bStateSave: true,
        bPaginate: true,
        bAutoWidth: false,
        bProcessing: true,
        bServerSide: true,
        bDestroy: true,
        bRetrieve: true,
        sPaginationType: "bootstrap",
        sAjaxSource: "/api/services.asmx/dtpro",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "SequenceNumber", sWidth: "5%" },
                    { mDataProp: "Photo", sWidth: "10%" },
                    { mDataProp: "Name", sWidth: "15%" },
                    { mDataProp: "ReferenceCode", sWidth: "15%" },
                    { mDataProp: "PriceBeforeDiscount", sWidth: "10%" },
                    { mDataProp: "Price", sWidth: "10%" },
                    { mDataProp: "Quantity", sWidth: "5%" },
                    { mDataProp: "SequenceNumber", sWidth: "10%" },
                    { mDataProp: "Active", sWidth: "5%" },
                    { mDataProp: "IDProduct", sWidth: "10%" }
        ],
        aoColumnDefs: [
            { bSortable: false, aTargets: [9] },
            { sClass: "text-center", aTargets: ['_all'] }
        ],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            var oSettings = e.fnSettings();
            var iTotalRecords = oSettings.fnRecordsTotal();
            if (aaData.Photo != null)
                $("td:eq(1)", nRow).html('<img class="img-responsive" src="/assets/images/product/' + aaData.Photo + '" />');
            else
                $("td:eq(1)", nRow).html('<img class="img-responsive" src="/assets/images/noimage.jpg" />');
            $("td:eq(8)", nRow).empty();
            var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDProduct + '" data-original-title="Click to Deactivate"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDProduct + '" data-original-title="Click to Activate"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            $("td:eq(8)", nRow).append(s);

            var p = '';
            if (aaData.SequenceNumber != 1)
                p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-up green btn-sm tooltips" data-original-title="Up Position"><i class="glyphicon glyphicon-arrow-up"></i></button> ';
            if (aaData.SequenceNumber != iTotalRecords)
                p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-down red btn-sm tooltips" data-original-title="Down Position"><i class="glyphicon glyphicon-arrow-down"></i></button>';
            $("td:eq(7)", nRow).html(p);

            var o = "";
            //o += '<a href="#" data-id="' + aaData.IDProduct + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            o += '<a href="update.aspx?id=' + aaData.IDProduct + '" data-id="' + aaData.IDProduct + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            o += '<a href="#" data-id="' + aaData.IDProduct + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(9)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
            $(".btn_view").on("click", function (e) {
                e.preventDefault();
                GetDetail(+$(this).data("id"));
            });

            $(".btn_changestatus").click(function (e) {
                e.preventDefault();
                StatusToggle(+$(this).data("id"));
            });

            $(".btn_delete").on("click", function (e) {
                e.preventDefault();
                var button = $(this);
                bootbox.confirm("Are you sure to <b>delete</b>?", function (result) {
                    if (result) {
                        Delete(+button.data("id"));
                    }
                });
            });

            $(".btn-up").click(function (e) {
                e.preventDefault();
                UpProductPosition(+$(this).data("id"));
            });

            $(".btn-down").click(function (e) {
                e.preventDefault();
                DownProductPosition(+$(this).data("id"));
            });

            $("a,button", this.fnGetNodes()).tooltip({
                delay: 0,
                track: true,
                fade: 250
            });
        }
    });
    jQuery(element + "_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline");
    jQuery(element + "_wrapper .dataTables_length select").addClass("form-control input-xsmall");
    jQuery(element + "_wrapper .dataTables_length select").select2();
}

function Delete(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
            InitDT("#dtProduct");
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "warning"
            });
        }
    };
    REST.sendRequest({
        'c': 'bepro',
        'm': 'dpro',
        'data': { 'id': id }
    });
}

function StatusToggle(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            InitDT("#dtProduct");
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "warning"
            });
        }
    };

    REST.sendRequest({
        'c': 'bepro',
        'm': 'statustoggle',
        'data': { 'id': id }
    });
}

function UpProductPosition(id) {
    REST.onSuccess = function (data) { InitDT("#dtProduct"); };
    REST.sendRequest({
        'c': 'bepro',
        'm': 'uppropos',
        'data': { 'id': id }
    });
}

function DownProductPosition(id) {
    REST.onSuccess = function (data) { InitDT("#dtProduct"); };
    REST.sendRequest({
        'c': 'bepro',
        'm': 'downpropos',
        'data': { 'id': id }
    });
}