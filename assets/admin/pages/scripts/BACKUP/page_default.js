function Page_GetAll_DataTable() {
    $("#dtPage").each(function () {
        $("#dtPage").dataTable().fnDestroy()
    });
    var e = $("#dtPage").dataTable({
        oLanguage: {
            sProcessing: '<img src="' + Metronic.getGlobalImgPath() + 'loading-spinner-grey.gif"/><span>&nbsp;&nbsp;Loading...</span>',
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
        sAjaxSource: window.location.origin + "/api/Backend_Page.asmx/GetAll_DataTable",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500)
        },
        aoColumns: [{
            mDataProp: "IDPage",
            sWidth: "5%"
        }, {
            mDataProp: "Page_Title",
            sWidth: "30%"
        }, {
            mDataProp: "Page_ShortContent",
            sWidth: "40%"
        }, {
            mDataProp: "Active",
            sWidth: "10%"
        }, {
            mDataProp: "IDPage",
            sWidth: "15%"
        }],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [2, 4]
        }, {
            sClass: "text-center",
            aTargets: [0, 1, 2, 3, 4]
        }],
        fnRowCallback: function (e, t, n, r) {
            //var i = new Date;
            //$("td:eq(1)", e).empty();
            //$("td:eq(1)", e).append('<center><img src="' + window.location.origin + "/assets/images/manufacturer/" + t.Logo + "?v=" + i.getTime() + '  class="img-responsive" style="width:80px;"/></center>');
            $("td:eq(3)", e).empty();
            var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data_id="' + t.IDPage + '" data_nama="' + t.Page_Title + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            if (t.Active != "True") s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data_id="' + t.IDPage + '" data_nama="' + t.Page_Title + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            $("td:eq(3)", e).append(s);
            $("td:eq(4)", e).empty();
            var o = "";
            o += '<a href="#" data_id="' + t.IDPage + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a> ';
            o += '<a href="update.aspx?id=' + t.IDPage + '" data_id="' + t.IDPage + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a> ';
            o += '<a href="#" data_id="' + t.IDPage + '" data_nama="' + t.Page_Title + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a> ';
            $("td:eq(4)", e).append(o);
        },
        fnDrawCallback: function (e) {
            $("a", this.fnGetNodes()).tooltip({
                delay: 0,
                track: true,
                fade: 250
            });
        }
    });
jQuery("#dtPage_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline");
jQuery("#dtPage_wrapper .dataTables_length select").addClass("form-control input-xsmall");
jQuery("#dtPage_wrapper .dataTables_length select").select2()
}

$(document).ready(function () {
    $(document).ajaxStop(function () {
        console.clear()
    });
    Toastr.init();
    $("#menu_content").addClass("active");
    $("#submenu_page").addClass("active");
    $("#menu_content").addClass("open");
    Page_GetAll_DataTable();
})