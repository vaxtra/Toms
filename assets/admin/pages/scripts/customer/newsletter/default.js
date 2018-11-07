var REST = new OF({
    'url': '/api/services.asmx/request'
});

function GetDetail(id) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var result = data.d.data;
            $("#viewModal").modal("show");
            var item = '';
            $.each(result, function (indexInArray, valueOfElement) {
                if (indexInArray == "Active") {
                    item += '<div class="form-group">';
                    item += '<label class="control-label col-md-4">' + indexInArray + ' : </label>';
                    item += '<div class="col-md-8">';
                    item += '<p class="form-control-static"><b>' + valueOfElement + '</b></p>';
                    item += '</div>';
                    item += '</div>';
                }
                else if (indexInArray == "Logo") {
                    item += '<div class="form-group">';
                    item += '<div class="col-md-12"><center>';
                    item += '<img class="img-responsive" style="width:150px;height:auto;" src="/assets/images/manufacturer/' + valueOfElement + '" />';
                    item += '</center></div>';
                    item += '</div>';
                }
                else if (indexInArray !== "IDManufacturer") {
                    item += '<div class="form-group">';
                    item += '<label class="control-label col-md-4">' + indexInArray + ' : </label>';
                    item += '<div class="col-md-8">';
                    item += '<p class="form-control-static"><b>' + valueOfElement + '</b></p>';
                    item += '</div>';
                    item += '</div>';
                }
            });
            $("#view_manufacturer").find(".form-body").empty();
            $("#view_manufacturer").find(".form-body").append(item);
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
        'c': 'beman',
        'm': 'rman',
        'data': { 'id': id }
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
        sAjaxSource: "/api/services.asmx/dtNewsletter",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDNewsletter", sWidth: "5%" },
                    { mDataProp: "Email", sWidth: "20%" },
                    { mDataProp: "DateInsert", sWidth: "20%" },
                    { mDataProp: "IDNewsletter", sWidth: "5%" }
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
            o += '<a href="#" data-id="' + aaData.IDNewsletter + '" data-nama="' + aaData.Email + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(3)", nRow).empty().append(o);
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

function InitDTMember(element) {
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
        iDisplayLength: 5,
        bSortClasses: false,
        bStateSave: true,
        bPaginate: true,
        bAutoWidth: false,
        bProcessing: true,
        bServerSide: true,
        bDestroy: true,
        bRetrieve: true,
        sPaginationType: "bootstrap",
        sAjaxSource: "/api/services.asmx/dtCustomerIsSubscribe",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDCustomer", sWidth: "5%" },
                    { mDataProp: "Name", sWidth: "5%" },
                    { mDataProp: "Email", sWidth: "20%" },
                    { mDataProp: "PhoneNumber", sWidth: "20%" },
                    { mDataProp: "DateInsert", sWidth: "20%" },
                    { mDataProp: "IDCustomer", sWidth: "5%" }
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [5]
        }, {
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            //$("td:eq(4)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDNewsletter + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDNewsletter + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            //var o = "";
            //o += '<a href="#" data-id="' + aaData.IDCustomer + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDCustomer + '" data-nama="' + aaData.Email + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            //$("td:eq(5)", nRow).empty().append(o);
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
    $("#menu_customer").addClass("active");
    $("#arrow_customer").addClass("open");
    $("#submenu_customer").addClass("active");

    InitDT("#dtNewsletter");
    InitDTMember("#dtCustomerSub");
});
