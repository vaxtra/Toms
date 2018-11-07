var REST = new OF({
    'url': '/api/service.asmx/dtattr'
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
        sAjaxSource: "/api/services.asmx/dtattr",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDAttribute", sWidth: "5%" },
                    { mDataProp: "Name", sWidth: "45%" },
                    { mDataProp: "TotalValue", sWidth: "15%" },
                    { mDataProp: "IsColor", sWidth: "15%" },
                    { mDataProp: "IDAttribute", sWidth: "20%" }
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [4]
        }, {
            sClass: "text-center",
            aTargets: [0, 1, 2, 3, 4]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            if (aaData.IsColor)
                $("td:eq(3)", nRow).empty().append('<span class="btn btn-sm green"><i class="fa fa-check"></i></span>');
            else
                $("td:eq(3)", nRow).empty().append('<span class="btn btn-sm red"><i class="fa fa-times"></i></span>');

            var actions = "";
            actions += '<a href="value/default.aspx?id=' + aaData.IDAttribute + '" data-id="' + aaData.IDAttribute + '" class="btn_view btn btn-sm green tooltips" data-original-title="Values"><i class="glyphicon glyphicon-eye-open"></i></a> ';
            actions += '<a href="update.aspx?id=' + aaData.IDAttribute + '" data-id="' + aaData.IDAttribute + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a> ';
            actions += '<a href="javascript:;" data-id="' + aaData.IDAttribute + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a> ';
            $("td:eq(4)", nRow).html(actions);
        },
        fnDrawCallback: function (e) {
            $(".btn_delete").on("click", function (e) {
                e.preventDefault();
                var button = $(this);
                bootbox.confirm("Are you sure to delete <b>" + button.data("nama") + "</b>", function (result) {
                    if (result) {
                        Att_Delete(+button.data("id"));
                    }
                });
            });

            $("a", this.fnGetNodes()).tooltip({
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

function Att_Delete(id) {
    var REST = new OF({
        'url': '/api/services.asmx/request'
    });

    REST.onSuccess = function (data) {
        if (data.d.success) {
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
            InitDT("#dtAttribute");
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
        'c': 'beattr',
        'm': 'dattr',
        'data': {
            'id': id
        }
    });
}

$(document).ready(function () {
    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_attributes").addClass("active");

    InitDT("#dtAttribute");
});