var REST = new OF({
    'url': '/api/services.asmx/request'
});

function PreloadMaster()
{
    $.ajax({
        url: "/modules/MemberPoint/MemberPoint.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "MemberPoint",
            t: "be",
            m: "listgr",
            data: {}
        }),
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true
            });
        },
        success: function (result) {
            var data = result.data
            var item = '';
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    item += '<option value=' + data[i].IDCustomer_Group + ' selected="selected">' + data[i].Name + '</option>';
                }
                else {
                    item += '<option value=' + data[i].IDCustomer_Group + '>' + data[i].Name + '</option>';
                }
            }

            $("#selectGroup").html(item);

            InitDT("#dtMember", $("#selectGroup").val());

            $("#selectGroup").on("change", function () {
                InitDT("#dtMember", $("#selectGroup").val());
            });
        },
        error: function (result) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.message,
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

function Delete(id) {
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
            InitDT("#dtNewsletter");
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
        'c': 'benews',
        'm': 'dnews',
        'data': { 'IDNewsletter': id }
    });
}

function StatusToggle(id) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            InitDT("#dtNewsletter");

            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 0,
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
                closeInSeconds: 0,
                icon: "warning"
            });
        }
    };

    REST.sendRequest({
        'c': 'becust',
        'm': 'statusToggle',
        'data': {
            'IDCustomer': id
        }
    });
}

function InitDT(element, idMember) {
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
        sAjaxSource: "/modules/MemberPoint/MemberPoint.ashx/admin_dtCustomerByGroup?dt=true&data=member",
        fnServerParams: function (aoData) {
            aoData.push(
                { "name": "id", "value": idMember })
        },
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDCustomer", sWidth: "5%" },
                    { mDataProp: "Name", sWidth: "20%" },
                    { mDataProp: "Email", sWidth: "20%" },
                    { mDataProp: "Active", sWidth: "5%" },
                    { mDataProp: "PhoneNumber", sWidth: "10%" },
                    { mDataProp: "IDCustomer", sWidth: "10%" }

        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [3]
        }, {
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            //$("td:eq(4)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDNewsletter + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDNewsletter + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            var o = "";
            //o += '<a href="#" data-id="' + aaData.IDCustomer + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            o += '<a href="#" data-id="' + aaData.IDCustomer + '" data-nama="' + aaData.Email + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            o += '<a href="./update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" data-nama="' + aaData.Email + '" class="btn_update btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            $("td:eq(5)", nRow).empty().append(o);
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
    $("#arrow_customers").addClass("open");
    $("#submenu_member").addClass("active");
    PreloadMaster();
});
