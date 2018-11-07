var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_report").addClass("active");
    $("#arrow_report").addClass("open");
    $("#submenu_zone-distribution").addClass("active");
    $("#FilterZone").on("change", function () {
        if ($("#FilterZone option:selected").val() == "Province") {
            InitDT("#dtZoneDis", "/api/services.asmx/admin_dtProvinceDistribution");
        }
        else if ($("#FilterZone option:selected").val() == "City") {
            InitDT("#dtZoneDis", "/api/services.asmx/admin_dtCityDistribution");
        }
        else if ($("#FilterZone option:selected").val() == "District") {
            InitDT("#dtZoneDis", "/api/services.asmx/admin_dtDistrictDistribution");
        }

    });
    InitDT("#dtZoneDis", "/api/services.asmx/admin_dtProvinceDistribution");
    InitDateRange();
});

function InitDT(element, resources) {
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
        sAjaxSource: resources,
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "Number", sWidth: "5%" },
                    { mDataProp: "Zone", sWidth: "20%" },
                    { mDataProp: "TotalOrders", sWidth: "20%" },
                    { mDataProp: "TotalSales", sWidth: "20%" },
        ],
        aoColumnDefs: [{
            bSortable: true,
            aTargets: [3]
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
            o += '<span class="format-money">' + aaData.TotalSales + '</span>';
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

            $(".format-money").formatCurrency({
                region: "id-ID"
            })

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

function InitDateRange() {
    $('#dashboard-report-range').daterangepicker({
        //opens: (App.isRTL() ? 'right' : 'left'),
        startDate: moment(),
        endDate: moment(),
        minDate: '01/01/2012',
        maxDate: moment(),
        showDropdowns: false,
        showWeekNumbers: true,
        timePicker: false,
        timePickerIncrement: 1,
        timePicker12Hour: true,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract('days', 1), moment().subtract('days', 1)],
            'Last 7 Days': [moment().subtract('days', 6), moment()],
            'Last 30 Days': [moment().subtract('days', 29), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')]
        },
        buttonClasses: ['btn'],
        applyClass: 'grey',
        CanceledClass: 'default',
        format: 'DD/MM/YYYY',
        separator: ' to ',
        locale: {
            applyLabel: 'Apply',
            fromLabel: 'From',
            toLabel: 'To',
            customRangeLabel: 'Custom Range',
            daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
            monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
            firstDay: 1
        }
    },
    function (start, end) {
        //FilterByDate($(".input-mini[name=daterangepicker_start]").val(), $(".input-mini[name=daterangepicker_end]").val())
        //FullFilter();
        var resources;
        if ($("#FilterZone option:selected").val() == "Province") {
            resources = "/api/services.asmx/admin_dtProvinceDistribution_FilterDate";
        }
        else if ($("#FilterZone option:selected").val() == "City") {
            resources = "/api/services.asmx/admin_dtCityDistribution_FilterDate";
        }
        else if ($("#FilterZone option:selected").val() == "District") {
            resources = "/api/services.asmx/admin_dtDistrictDistribution_FilterDate";
        }
        InitDT_Filter("#dtZoneDis", $(".input-mini[name=daterangepicker_start]").val(), $(".input-mini[name=daterangepicker_end]").val(), resources);
        $('#dashboard-report-range span').html(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
    }
    );


    $('#dashboard-report-range span').html(moment().format('DD/MM/YYYY') + ' - ' + moment().format('DD/MM/YYYY'));
    $('#dashboard-report-range').show();

}

function InitDT_Filter(element, startDate, endDate, resources) {
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
        sAjaxSource: resources,
        fnServerParams: function (aoData) {
            aoData.push({ "name": "_startDate", "value": startDate }, { "name": "_endDate", "value": endDate });
        },
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "Number", sWidth: "5%" },
                    { mDataProp: "Zone", sWidth: "20%" },
                    { mDataProp: "TotalOrders", sWidth: "20%" },
                    { mDataProp: "TotalSales", sWidth: "20%" },
        ],
        aoColumnDefs: [{
            bSortable: true,
            aTargets: [3]
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
            o += '<span class="format-money">' + aaData.TotalSales + '</span>';
            $("td:eq(3)", nRow).empty().append(o);
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
        }
    });
    jQuery(element + "_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline");
    jQuery(element + "_wrapper .dataTables_length select").addClass("form-control input-xsmall");
    jQuery(element + "_wrapper .dataTables_length select").select2();
}