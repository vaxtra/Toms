var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_log").addClass("active");
    $("#arrow_log").addClass("open");
    $("#submenu_log-error").addClass("active");
    InitDT("#dtLogError");
});

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
        sAjaxSource: "/api/services.asmx/admin_dtLog_Error",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "ID", sWidth: "5%" },
                    { mDataProp: "Date", sWidth: "5%" },
                    { mDataProp: "Message", sWidth: "45%" },
                    { mDataProp: "StackTrace", sWidth: "45%" },
                    { mDataProp: "ID", sWidth: "2%" },
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: []
        }, {
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            //$("td:eq(4)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDCustomer + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDCustomer + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            var a = "";
            ////o += '<a href="#" data-id="' + aaData.IDCustomer + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            ////o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            a += moment(aaData.InsertDate).format("DD-MM-YYYY HH:mm:ss");
            $("td:eq(1)", nRow).empty().append(a);

            var o = "";
            //o += '<a href="#" data-id="' + aaData.IDVoucher + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            o += '<a href="#" data-id="' + aaData.ID + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(4)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
            $(".format-money").formatCurrency({
                region: "id-ID"
            })

            $("a", this.fnGetNodes()).tooltip({
                delay: 0,
                track: true,
                fade: 250
            });

            $(".btn_delete").on("click", function (e) {
                e.preventDefault();
                var button = $(this);
                bootbox.confirm("whether this error has been resolved? ?", function (result) {
                    if (result) {
                        Delete(+button.data("id"));
                    }
                });
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
            bootbox.alert(result.message, function () {
                InitDT("#dtLogError");
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 0,
                icon: "warning"
            });
        }
    };

    REST.sendRequest({
        'c': 'belog',
        'm': 'dlogerror',
        'data': {
            'ID': id
        }
    });
}
