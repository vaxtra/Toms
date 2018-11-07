var REST = new OF({
    'url': '/api/services.asmx/request'
});

function Delete(id) {
    $.ajax({
        url: "/modules/MemberPoint/MemberPoint.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "MemberPoint",
            t: "be",
            m: "dcustgr",
            data: {
                IDCustomerGroup: id
            }
        }),
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true
            });
        },
        success: function (result) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: result.data.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "3",
                icon: "check"
            });
            InitDT("#dtCustomerGroup");
        },
        error: function (result) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.data.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "3",
                icon: "warning"
            });
        },
        complete: function () {
            Metronic.unblockUI();
        },
    });
}

function StatusToggle(id) {
    $.ajax({
        url: "/modules/MemberPoint/MemberPoint.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "MemberPoint",
            t: "be",
            m: "activetoggle",
            data: {
                IDCustomerGroup: id
            }
        }),
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true
            });
        },
        success: function (result) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: result.data.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "3",
                icon: "check"
            });
            InitDT("#dtCustomerGroup");
        },
        error: function (result) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.data.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "3",
                icon: "warning"
            });
        },
        complete: function () {
            Metronic.unblockUI();
        },
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
        sAjaxSource: "/modules/MemberPoint/MemberPoint.ashx/admin_dtMemberPoint?dt=true&data=custgr",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDCustomer_Group", sWidth: "5%" },
                    { mDataProp: "Name", sWidth: "20%" },
                    { mDataProp: "IsPoint", sWidth: "15%" },
                    { mDataProp: "Active", sWidth: "20%" },
                    { mDataProp: "IDCustomer_Group", sWidth: "5%" },
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [4]
        }, {
            sClass: "text-center",
            aTargets: [0, 1, 2, 3, 4]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            //$("td:eq(4)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDManufacturer + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDManufacturer + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            $("td:eq(2)", nRow).empty();
            var s = '<a href="#" class="btn btn-sm green tooltips" data-id="' + aaData.IDCustomer_Group + '" data-nama="' + aaData.Name + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips" data-id="' + aaData.IDCustomer_Group + '" data-nama="' + aaData.Name + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            $("td:eq(2)", nRow).append(s);

            $("td:eq(3)", nRow).empty();
            var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDCustomer_Group + '" data-nama="' + aaData.Name + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDCustomer_Group + '" data-nama="' + aaData.Name + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            $("td:eq(3)", nRow).append(s);

            var o = "";
            o += '<a href="update.aspx?id=' + aaData.IDCustomer_Group + '" data-id="' + aaData.IDCustomer_Group + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            o += '<a href="#" data-id="' + aaData.IDCustomer_Group + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(4)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {

            $(".btn_changestatus").click(function (e) {
                e.preventDefault();
                StatusToggle(+$(this).data("id"));
            });

            $(".btn_delete").on("click", function (e) {
                e.preventDefault();
                var button = $(this);
                bootbox.confirm("Are you sure ?", function (result) {
                    if (result) {
                        Delete(+button.data("id"));
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

$(document).ready(function () {
    $("#menu_customers").addClass("active");
    $("#submenu_customer-group").addClass("active");
    InitDT("#dtCustomerGroup");
});