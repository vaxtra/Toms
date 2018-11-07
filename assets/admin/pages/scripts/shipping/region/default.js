var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_shipping").addClass("active");
    $("#arrow_shipping").addClass("open");
    $("#submenu_regions").addClass("active");

    LoadCountry();
});

function LoadCountry() {
    var element = "#dtCountry";
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
        sAjaxSource: "/api/services.asmx/admin_dtCountry",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [
                    { mDataProp: "Name", },
                    { mDataProp: "Name", }
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [1]
        }, {
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            //$("td:eq(4)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDCustomer + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDCustomer + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            var o = "";
            o += '<a href="#" data-id="' + aaData.IDCountry + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDCountry + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(1)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
            $(".btn_view").on("click", function (e) {
                e.preventDefault();
                LoadProvince(+$(this).data("id"));
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

function LoadProvince(idCountry) {
    var element = "#dtProvince";
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
        fnServerData: function (sSource, aoData, fnCallback) {
            aoData.push({
                name: "idCountry",
                value: idCountry
            });
            $.getJSON(sSource, aoData, function (json) {
                fnCallback(json);
            });
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
        sAjaxSource: "/api/services.asmx/admin_dtProvince",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [
                    { mDataProp: "Name", },
                    { mDataProp: "Name", }
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [1]
        }, {
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            //$("td:eq(4)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDCustomer + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDCustomer + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            var o = "";
            o += '<a href="#" data-id="' + aaData.IDProvince + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDCountry + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(1)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
            $(".btn_view").on("click", function (e) {
                e.preventDefault();
                LoadCity(+$(this).data("id"));
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

function LoadCity(idProvince) {
    var element = "#dtCity";
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
        fnServerData: function (sSource, aoData, fnCallback) {
            aoData.push({
                name: "idProvince",
                value: idProvince
            });
            $.getJSON(sSource, aoData, function (json) {
                fnCallback(json);
            });
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
        sAjaxSource: "/api/services.asmx/admin_dtCity",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [
                    { mDataProp: "Name", },
                    { mDataProp: "Name", }
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [1]
        }, {
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            //$("td:eq(4)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDCustomer + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDCustomer + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            var o = "";
            o += '<a href="#" data-id="' + aaData.IDCity + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDCountry + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(1)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
            $(".btn_view").on("click", function (e) {
                e.preventDefault();
                LoadDistrict(+$(this).data("id"));
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

function LoadDistrict(idCity) {
    var element = "#dtDistrict";
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
        fnServerData: function (sSource, aoData, fnCallback) {
            aoData.push({
                name: "idCity",
                value: idCity
            });
            $.getJSON(sSource, aoData, function (json) {
                fnCallback(json);
            });
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
        sAjaxSource: "/api/services.asmx/admin_dtDistrict",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [
                    { mDataProp: "Name", },
                    { mDataProp: "Name", }
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [1]
        }, {
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            //$("td:eq(4)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDCustomer + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDCustomer + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            var o = "";
            o += '<a href="#" data-id="' + aaData.IDDistrict + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDCountry + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(5)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
            //$(".btn_view").on("click", function (e) {
            //    e.preventDefault();
            //    LoadProvince(+$(this).data("id"));
            //});

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
