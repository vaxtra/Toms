var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_config").addClass("active");
    $("#arrow_config").addClass("open");
    $("#submenu_currency").addClass("active");
    InitDT("#dtCurrency");
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
        sAjaxSource: "/api/services.asmx/admin_dtCurrency",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDCurrency", sWidth: "5%" },
                    { mDataProp: "Name", sWidth: "10%" },
                    { mDataProp: "ConversionRate", sWidth: "10%" },
                    { mDataProp: "ISOCode", sWidth: "10%" },
                    { mDataProp: "Symbol", sWidth: "20%" },
                    { mDataProp: "IDCurrency", sWidth: "5%" },

        ],
        aaSorting: [[0, 'desc']],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [4]
        }, {
            sClass: "text-center",
            aTargets: ["_all"]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {


            var o = "";
            if (aaData.IDCurrency != 1) {
                o += '<a href="currency-detail.aspx?id=' + aaData.IDCurrency + '" data-id="' + aaData.IDCurrency + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            }
            //o += '<a href="#" data-id="' + aaData.IDCustomer + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            
            $("td:eq(5)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {

            $(".btn_view").on("click", function (e) {
                e.preventDefault();
                GetDetail(+$(this).data("id"));
            });

            $("a,button", this.fnGetNodes()).tooltip({
                delay: 0,
                track: true,
                fade: 250
            });

            $(".money").formatCurrency({ region: 'id-ID' });
        }
    });
    jQuery(element + "_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline");
    jQuery(element + "_wrapper .dataTables_length select").addClass("form-control input-xsmall");
    jQuery(element + "_wrapper .dataTables_length select").select2();
}