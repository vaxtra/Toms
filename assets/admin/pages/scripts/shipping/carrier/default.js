var REST = new OF({
    'url': '/api/services.asmx/Request',
});

$(document).ready(function () {
    $("#menu_shipping").addClass("active");
    $("#submenu_carries").addClass("active");
    InitDT("#dtCarrier");
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
        sAjaxSource: "/api/services.asmx/dtCar",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDCarrier", sWidth: "10%" },
                    { mDataProp: "Image", sWidth: "20%" },
                    { mDataProp: "Name", sWidth: "30%" },
                    { mDataProp: "Active", sWidth: "10%" },
                    { mDataProp: "IDCarrier", sWidth: "20%" }
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [4]
        }, {
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            var img = '<img class="img-responsive" src="/assets/images/carrier/' + aaData.Image + '" />';
            $("td:eq(1)", nRow).html(img);
            if (aaData.Active)
                $("td:eq(3)", nRow).empty().append('<span class="btn btn-deactivate btn-sm green tooltips" data-id="' + aaData.IDCarrier + '" data-original-title="Click to deactivate"><i class="fa fa-check"></i></span>');
            else
                $("td:eq(3)", nRow).empty().append('<span class="btn btn-activate btn-sm red tooltips" data-id="' + aaData.IDCarrier + '" data-original-title="Click to activate"><i class="fa fa-times"></i></span>');

            var actions = "";
            actions += '<a href="shipping.aspx?idcarrier=' + aaData.IDCarrier + '" data-id="' + aaData.IDCarrier + '" class="btn_view btn btn-sm green tooltips" data-original-title="View detail"><i class="glyphicon glyphicon-eye-open"></i></a> ';
            actions += '<a href="update.aspx?id=' + aaData.IDCarrier + '" data-id="' + aaData.IDCarrier + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a> ';
            actions += '<a href="javascript:;" data-id="' + aaData.IDCarrier + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a> ';
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

            $(".tooltips", this.fnGetNodes()).tooltip({
                delay: 0,
                track: true,
                fade: 250
            });

            $(".btn-deactivate").click(function (e) {
                e.preventDefault();
                ChangeActive(+$(this).data("id"), false);
            });

            $(".btn-activate").click(function (e) {
                e.preventDefault();
                ChangeActive(+$(this).data("id"), true);
            });
        }
    });
    jQuery(element + "_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline");
    jQuery(element + "_wrapper .dataTables_length select").addClass("form-control input-xsmall");
    jQuery(element + "_wrapper .dataTables_length select").select2();
}

function ChangeActive(id, status) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (data.d.success) {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "success",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "checked"
            });
            InitDT("#dtCarrier");
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "warning"
            });
        }
    };
    REST.sendRequest({
        'c': 'becar',
        'm': 'statustoggle',
        'data': { 'IDCarrier': id, 'Active': status }
    });
}

function Att_Delete(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            bootbox.alert(result.message, function () {
                InitDT("#dtCarrier");
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
        'c': 'becar',
        'm': 'dcar',
        'data': {
            'IDCarrier': id
        }
    });
}