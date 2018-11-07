var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_monitoring").addClass("active");

    //Preload();
    InitDT("#dtProduct");
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

        }
    };
    REST.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'RequestData': ['MonitoringStock']
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
        sAjaxSource: "/api/services.asmx/admin_dtMonitoring",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "ProductName", sWidth: "20%" },
                    { mDataProp: "CombinationName", sWidth: "20%" },
                    { mDataProp: "ReferenceCode", sWidth: "20%" },
                    { mDataProp: "Quantity", sWidth: "20%" },
                    { mDataProp: "IDProductCombination", sWidth: "20%" }
        ],
        aoColumnDefs: [{
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            $("td:eq(3)", nRow).empty();
            var s = '<input type="text" class="qtybox" ID="qtyfor' + aaData.IDProductCombination + '" value="' + aaData.Quantity + '" style="text-align:center;border:none;" />';
            $("td:eq(3)", nRow).append(s);

            $("td:eq(4)", nRow).empty();
            var s = '<a href="#" class="btn btn-sm blue tooltips btn-update-qty" data-id="' + aaData.IDProductCombination + '" data-original-title="Update">Update</a>';
            $("td:eq(4)", nRow).append(s);

            //var o = "";
            ////o += '<a href="#" data-id="' + aaData.IDCustomer + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            ////o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDCustomer + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            //$("td:eq(5)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
            $(".btn-update-qty").on("click", function (e) {
                e.preventDefault();
                UpdateQuantity(+$(this).data("id"), +$("#qtyfor" + $(this).data("id")).val());
            });

            //$(".btn_changestatus").click(function (e) {
            //    e.preventDefault();
            //    StatusToggle(+$(this).data("id"));
            //});

            //$(".btn_delete").on("click", function (e) {
            //    e.preventDefault();
            //    var button = $(this);
            //    bootbox.confirm("Are you sure ?", function (result) {
            //        if (result) {
            //            Delete(+button.data("id"));
            //        }
            //    });
            //});

            //$("a", this.fnGetNodes()).tooltip({
            //    delay: 0,
            //    track: true,
            //    fade: 250
            //});
        }
    });
    jQuery(element + "_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline");
    jQuery(element + "_wrapper .dataTables_length select").addClass("form-control input-xsmall");
    jQuery(element + "_wrapper .dataTables_length select").select2();
}

function UpdateQuantity(idCombination, qty) {
    REST.onSuccess = function (data) {
        var result = data;
        if (result.d.success) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: result.d.message,
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
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "warning"
            });
        }
    }

    var sendData = {};
    sendData["id"] = +idCombination;;
    sendData["qty"] = +qty;

    REST.sendRequest({
        'c': 'bepro',
        'm': 'uqty',
        'data': sendData
    });
}