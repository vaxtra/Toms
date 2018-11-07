var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_stats").addClass("active");
    $("#submenu_bestcategories").addClass("active");

    InitDT("#dtBestCategory");
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
        sAjaxSource: "/api/services.asmx/admin_dtBestCategories",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "Name"},
                    { mDataProp: "Quantity"}
        ],
        aoColumnDefs: [{
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            //$("td:eq(0)", nRow).html('<img src="/assets/images/product/' + aaData.Photo + '" class="img-responsive" />');
            //$("td:eq(3)", nRow).html("<label class='money-format'>" + aaData.Total + "</label>");
            //$("td:eq(4)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDCustomer + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDCustomer + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            //var o = "";
            ////o += '<a href="#" data-id="' + aaData.IDCustomer + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            ////o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDCustomer + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            //$("td:eq(5)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
            //$(".btn_view").on("click", function (e) {
            //    e.preventDefault();
            //    GetDetail(+$(this).data("id"));
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

            //$("a", this.fnGetNodes()).tooltip({
            //    delay: 0,
            //    track: true,
            //    fade: 250
            //});
            $(".money-format").formatCurrency({ region: 'id-ID' });
        }
    });
    jQuery(element + "_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline");
    jQuery(element + "_wrapper .dataTables_length select").addClass("form-control input-xsmall");
    jQuery(element + "_wrapper .dataTables_length select").select2();
}