var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_log").addClass("active");
    $("#arrow_log").addClass("open");
    $("#submenu_log-stock").addClass("active");
    InitDT("#dtReport");
    InitDateRange();
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
        sAjaxSource: "/api/services.asmx/admin_dtLog_Stock",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "Number", sWidth: "5%" },
                    { mDataProp: "EmployeeName", sWidth: "10%" },
                    { mDataProp: "ProductName", sWidth: "10%" },
                    { mDataProp: "Description", sWidth: "20%" },
                    { mDataProp: "QuantityBefore", sWidth: "5%" },
                    { mDataProp: "Quantity", sWidth: "5%" },
                    { mDataProp: "Type", sWidth: "5%" },
                    { mDataProp: "QuantityAfter", sWidth: "5%" },
                    { mDataProp: "InsertDate", sWidth: "5%" },
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: []
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
            ////o += '<a href="#" data-id="' + aaData.IDCustomer + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            ////o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            o += moment(aaData.InsertDate).format("DD-MM-YYYY HH:mm:ss");
            $("td:eq(8)", nRow).empty().append(o);

            var icon = "";
            if(aaData.Type == "increase")
                icon += "<a class='btn green'><i class='fa fa-arrow-up'></i></a>";
            else if (aaData.Type == "decrease")
                icon += "<a class='btn red'><i class='fa fa-arrow-down'></i></a>";
            $("td:eq(6)", nRow).html(icon);
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

function PreloadReport_FilterDate(startDate, endDate) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            $.each(result.d.data, function (indexInArray, valueOfElement) {
                if (valueOfElement == null | valueOfElement == [])
                    $("." + indexInArray).html(0);
                else
                    $("." + indexInArray).html(valueOfElement);
            });

            //$(".EmployeeName").html(result.d.data.Employee.Name);

            $(".money-format").formatCurrency({ region: 'id-ID' });

        }
        else {
            if (result.d.message == 'Invalid token!') {
                Logout();
                //window.location = 'login-soft.aspx?returnUrl=' + window.location.pathname;
            }
        }
    };
    REST.sendRequest({
        'c': 'berep',
        'm': 'preload',
        'data': {
            'RequestData': ["TotalCustomer", "TotalOrder", "TotalSales", "TotalItemsSold"],
            'startDate': startDate,
            'endDate': endDate
        }
    });
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
        InitDT_Filter("#dtReport", $(".input-mini[name=daterangepicker_start]").val(), $(".input-mini[name=daterangepicker_end]").val());
        PreloadReport_FilterDate($(".input-mini[name=daterangepicker_start]").val(), $(".input-mini[name=daterangepicker_end]").val());
        $('#dashboard-report-range span').html(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
    }
    );


    $('#dashboard-report-range span').html(moment().format('DD/MM/YYYY') + ' - ' + moment().format('DD/MM/YYYY'));
    $('#dashboard-report-range').show();

}

function InitDT_Filter(element, startDate, endDate) {
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
        sAjaxSource: "/api/services.asmx/admin_dtLog_Stock_filter",
        fnServerParams: function (aoData) {
            aoData.push({ "name": "_startDate", "value": startDate }, { "name": "_endDate", "value": endDate });
        },
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "Number", sWidth: "5%" },
                    { mDataProp: "EmployeeName", sWidth: "10%" },
                    { mDataProp: "ProductName", sWidth: "10%" },
                    { mDataProp: "Description", sWidth: "20%" },
                    { mDataProp: "QuantityBefore", sWidth: "5%" },
                    { mDataProp: "Quantity", sWidth: "5%" },
                    { mDataProp: "Type", sWidth: "5%" },
                    { mDataProp: "QuantityAfter", sWidth: "5%" },
                    { mDataProp: "InsertDate", sWidth: "5%" },
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
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDCustomer + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDCustomer + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(4)", nRow).append(s);

            var o = "";
            ////o += '<a href="#" data-id="' + aaData.IDCustomer + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            ////o += '<a href="update.aspx?id=' + aaData.IDCustomer + '" data-id="' + aaData.IDCustomer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            o += moment(aaData.InsertDate).format("DD-MM-YYYY HH:mm:ss");
            $("td:eq(8)", nRow).empty().append(o);

            var icon = "";
            if (aaData.Type == "increase")
                icon += "<a class='btn green'><i class='fa fa-arrow-up'></i></a>";
            else if (aaData.Type == "decrease")
                icon += "<a class='btn red'><i class='fa fa-arrow-down'></i></a>";
            $("td:eq(6)", nRow).html(icon);
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