var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_orders").addClass("active");
    $("#arrow_orders").addClass("open");
    $("#submenu_orders").addClass("active");

    $(".btn-reset").click(function (e) {
        e.preventDefault();
        InitDT("#dtProduct");
        $(".input-mini[name=daterangepicker_start]").val(moment().format('DD/MM/YYYY'));
        $(".input-mini[name=daterangepicker_end]").val(moment().format('DD/MM/YYYY'));
        $('#dashboard-report-range span').html(moment().format('DD/MM/YYYY') + ' - ' + moment().format('DD/MM/YYYY'));
    });

    $(".btn-submit-filter").click(function (e) {
        e.preventDefault();
        InitDT_Filter2("#dtProduct", $(".input-mini[name=daterangepicker_start]").val(), $(".input-mini[name=daterangepicker_end]").val(), GetOrderStatusFilter());
    });

    $("#btnPrintShippingLabel").click(function () {
        var listID = [];
        var item = '';
        $("#dtProduct .checkboxes:checked").each(function () {
            listID.push($(this).val());
        });
        for (var i = 0; i < listID.length; i++) {
            if (i == 0) {
                item += listID[i];
            }
            else {
                item += ',' + listID[i];
            }
        }
        window.open(
                'shipping-label.aspx?id=' + item,
                '_blank'
            );
    });

    $("#btnPrintInvoice").click(function () {
        var listID = [];
        var item = '';
        $("#dtProduct .checkboxes:checked").each(function () {
            listID.push($(this).val());
        });
        for (var i = 0; i < listID.length; i++) {
            if (i == 0) {
                item += listID[i];
            }
            else {
                item += ',' + listID[i];
            }
        }

        window.open(
            'invoice.aspx?id=' + item,
            '_blank'
        );
    });

    $('#dtProduct').dataTable({
        "aoColumns": [
          { "bSortable": false },
          null,
          { "bSortable": false },
          null,
          { "bSortable": false },
          { "bSortable": false }
        ],
        "aLengthMenu": [
            [5, 15, 20, -1],
            [5, 15, 20, "All"] // change per page values here
        ],
        // set the initial value
        "iDisplayLength": 10,
        "sPaginationType": "bootstrap",
        "oLanguage": {
            "sLengthMenu": "_MENU_ records",
            "oPaginate": {
                "sPrevious": "Prev",
                "sNext": "Next"
            }
        },
        "aoColumnDefs": [
            { 'bSortable': false, 'aTargets': [0] },
            { "bSearchable": false, "aTargets": [0] }
        ]
    });

    jQuery('#dtProduct .group-checkable').change(function () {
        var set = jQuery(this).attr("data-set");
        var checked = jQuery(this).is(":checked");
        jQuery(set).each(function () {
            if (checked) {
                $(this).attr("checked", true);
                $(this).parents('tr').addClass("active");
            } else {
                $(this).attr("checked", false);
                $(this).parents('tr').removeClass("active");
            }
        });
        jQuery.uniform.update(set);
    });

    jQuery('#dtProduct').on('change', 'tbody tr .checkboxes', function () {
        $(this).parents('tr').toggleClass("active");
    });

    jQuery('#sample_1_wrapper .dataTables_filter input').addClass("form-control input-medium input-inline"); // modify table search input
    jQuery('#sample_1_wrapper .dataTables_length select').addClass("form-control input-xsmall input-inline"); // modify table per page dropdown
    //jQuery('#sample_1_wrapper .dataTables_length select').select2(); // initialize select2 dropdown

    LoadOrderStatus();
    InitDateRange2();
});

function LoadOrderStatus() {
    REST.onSuccess = function (data) {
        var result = data.d.data;
        var item = '';
        for (var i = 0; i < result.length; i++) {
            if (i == 0)
                item += '<label><input type="checkbox" checked="checked" data-id="' + result[i].IDOrderStatus + '" data-name="' + result[i].Name + '"> ' + result[i].Name + '</label>';
            else
                item += '<label><input type="checkbox" data-id="' + result[i].IDOrderStatus + '" data-name="' + result[i].Name + '"> ' + result[i].Name + '</label>';
        }

        $(".orderstatus-list").html(item);
        InitDT_Filter2("#dtProduct", $(".input-mini[name=daterangepicker_start]").val(), $(".input-mini[name=daterangepicker_end]").val(), GetOrderStatusFilter());
    };

    REST.sendRequest({
        'c': 'beord',
        'm': 'rstord',
        'data': {}
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
        InitDT_Filter("#dtProduct", $(".input-mini[name=daterangepicker_start]").val(), $(".input-mini[name=daterangepicker_end]").val());
        $('#dashboard-report-range span').html(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
    }
    );


    $('#dashboard-report-range span').html(moment().format('DD/MM/YYYY') + ' - ' + moment().format('DD/MM/YYYY'));
    $('#dashboard-report-range').show();
}

function InitDateRange2() {
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
        InitDT_Filter2("#dtProduct", $(".input-mini[name=daterangepicker_start]").val(), $(".input-mini[name=daterangepicker_end]").val(), GetOrderStatusFilter());
        $('#dashboard-report-range span').html(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
    }
    );


    $('#dashboard-report-range span').html(moment().format('DD/MM/YYYY') + ' - ' + moment().format('DD/MM/YYYY'));
    $('#dashboard-report-range').show();
}

function GetOrderStatusFilter() {
    var arr = [];
    $(".orderstatus-list input[type=checkbox]").each(function (index, element) {
        // element == this
        if ($(this).prop('checked'))
            arr.push(+$(this).data('id'));
    });
    return arr;
}

function InitDT_Filter2(element, startDate, endDate, orderStatus) {
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
        sAjaxSource: "/api/services.asmx/admin_dtOrd_filter",
        fnServerParams: function (aoData) {
            aoData.push(
                { "name": "_startDate", "value": startDate },
                { "name": "_endDate", "value": endDate },
                { "name": "_orderStatus", "value": orderStatus });
        },
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDOrder", sWidth: "5%" },
                    //{ mDataProp: "IDOrder", sWidth: "5%" },
                    { mDataProp: "Reference", sWidth: "10%" },
                    { mDataProp: "Customer", sWidth: "20%" },
                    { mDataProp: "TotalPaid", sWidth: "15%", sClass: "money text-center" },
                    { mDataProp: "PaymentMethod", sWidth: "10%" },
                    { mDataProp: "Status", sWidth: "20%" },
                    { mDataProp: "Date", sWidth: "10%" },
                    { mDataProp: "IDOrder", sWidth: "10%" }
        ],
        aaSorting: [[0, 'desc']],
        aoColumnDefs: [
            { bSortable: false, aTargets: [7] },
            { sClass: "text-center", aTargets: ['_all'] }
        ],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            var oSettings = e.fnSettings();
            var iTotalRecords = oSettings.fnRecordsTotal();
            //$("td:eq(1)", nRow).html('<img class="img-responsive" src="/assets/images/product/' + aaData.Photo + '" />');
            //$("td:eq(7)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDProduct + '" data-original-title="Click to Deactivate"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDProduct + '" data-original-title="Click to Activate"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(7)", nRow).append(s);

            //var p = '';
            //if (aaData.SequenceNumber != 1)
            //    p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-up green btn-sm tooltips" data-original-title="Up Position"><i class="glyphicon glyphicon-arrow-up"></i></button> ';
            //if (aaData.SequenceNumber != iTotalRecords)
            //    p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-down red btn-sm tooltips" data-original-title="Down Position"><i class="glyphicon glyphicon-arrow-down"></i></button>';
            //$("td:eq(6)", nRow).html(p);

            var cb = "";
            cb += '<input type="checkbox" class="checkboxes" value="' + aaData.IDOrder + '" />';
            $("td:eq(0)", nRow).empty().append(cb);

            var o = "";
            o += '<a target="_blank" href="detail.aspx?id=' + aaData.IDOrder + '" data-id="' + aaData.IDOrder + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            if (aaData.Invoice != null)
                o += '<a target="_blank" href="invoice.aspx?id=' + aaData.IDOrder + '" data-id="' + aaData.IDOrder + '" class="btn_view btn btn-sm blue tooltips" data-original-title="Print Invoice"><i class="glyphicon glyphicon-print"></i></a>';
            if (aaData.Status == 'Payment Accepted')
                o += '<a target="_blank" href="shipping-label.aspx?id=' + aaData.IDOrder + '" data-id="' + aaData.IDOrder + '" class="btn_view btn btn-sm yellow tooltips" data-original-title="Print shipping label"><i class="glyphicon glyphicon-print"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDProduct + '" data-id="' + aaData.IDProduct + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDProduct + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(7)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
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
        sAjaxSource: "/api/services.asmx/admin_dtOrd_filterdate",
        fnServerParams: function (aoData) {
            aoData.push({ "name": "_startDate", "value": startDate }, { "name": "_endDate", "value": endDate });
        },
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDOrder", sWidth: "5%" },
                    //{ mDataProp: "IDOrder", sWidth: "5%" },
                    { mDataProp: "Reference", sWidth: "10%" },
                    { mDataProp: "Customer", sWidth: "20%" },
                    { mDataProp: "TotalPaid", sWidth: "15%", sClass: "money text-center" },
                    { mDataProp: "PaymentMethod", sWidth: "10%" },
                    { mDataProp: "Status", sWidth: "20%" },
                    { mDataProp: "Date", sWidth: "10%" },
                    { mDataProp: "IDOrder", sWidth: "10%" }
        ],
        aaSorting: [[0, 'desc']],
        aoColumnDefs: [
            { bSortable: false, aTargets: [7] },
            { sClass: "text-center", aTargets: ['_all'] }
        ],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            var oSettings = e.fnSettings();
            var iTotalRecords = oSettings.fnRecordsTotal();
            //$("td:eq(1)", nRow).html('<img class="img-responsive" src="/assets/images/product/' + aaData.Photo + '" />');
            //$("td:eq(7)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDProduct + '" data-original-title="Click to Deactivate"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDProduct + '" data-original-title="Click to Activate"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(7)", nRow).append(s);

            //var p = '';
            //if (aaData.SequenceNumber != 1)
            //    p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-up green btn-sm tooltips" data-original-title="Up Position"><i class="glyphicon glyphicon-arrow-up"></i></button> ';
            //if (aaData.SequenceNumber != iTotalRecords)
            //    p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-down red btn-sm tooltips" data-original-title="Down Position"><i class="glyphicon glyphicon-arrow-down"></i></button>';
            //$("td:eq(6)", nRow).html(p);

            var cb = "";
            cb += '<input type="checkbox" class="checkboxes" value="' + aaData.IDOrder + '" />';
            $("td:eq(0)", nRow).empty().append(cb);

            var o = "";
            o += '<a target="_blank" href="detail.aspx?id=' + aaData.IDOrder + '" data-id="' + aaData.IDOrder + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            if (aaData.Invoice != null)
                o += '<a target="_blank" href="invoice.aspx?id=' + aaData.IDOrder + '" data-id="' + aaData.IDOrder + '" class="btn_view btn btn-sm blue tooltips" data-original-title="Print Invoice"><i class="glyphicon glyphicon-print"></i></a>';
            if (aaData.Status == 'Payment Accepted')
                o += '<a target="_blank" href="shipping-label.aspx?id=' + aaData.IDOrder + '" data-id="' + aaData.IDOrder + '" class="btn_view btn btn-sm yellow tooltips" data-original-title="Print shipping label"><i class="glyphicon glyphicon-print"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDProduct + '" data-id="' + aaData.IDProduct + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDProduct + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(7)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
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
        sAjaxSource: "/api/services.asmx/admin_dtOrd",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDOrder", sWidth: "5%" },
                    //{ mDataProp: "IDOrder", sWidth: "5%" },
                    { mDataProp: "Reference", sWidth: "10%" },
                    { mDataProp: "Customer", sWidth: "20%" },
                    { mDataProp: "TotalPaid", sWidth: "15%", sClass: "money text-center" },
                    { mDataProp: "PaymentMethod", sWidth: "10%" },
                    { mDataProp: "Status", sWidth: "20%" },
                    { mDataProp: "Date", sWidth: "10%" },
                    { mDataProp: "IDOrder", sWidth: "10%" }
        ],
        aaSorting: [[0, 'desc']],
        aoColumnDefs: [
            { bSortable: false, aTargets: [7] },
            { sClass: "text-center", aTargets: ['_all'] }
        ],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            var oSettings = e.fnSettings();
            var iTotalRecords = oSettings.fnRecordsTotal();
            //$("td:eq(1)", nRow).html('<img class="img-responsive" src="/assets/images/product/' + aaData.Photo + '" />');
            //$("td:eq(7)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDProduct + '" data-original-title="Click to Deactivate"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDProduct + '" data-original-title="Click to Activate"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(7)", nRow).append(s);

            //var p = '';
            //if (aaData.SequenceNumber != 1)
            //    p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-up green btn-sm tooltips" data-original-title="Up Position"><i class="glyphicon glyphicon-arrow-up"></i></button> ';
            //if (aaData.SequenceNumber != iTotalRecords)
            //    p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-down red btn-sm tooltips" data-original-title="Down Position"><i class="glyphicon glyphicon-arrow-down"></i></button>';
            //$("td:eq(6)", nRow).html(p);

            var cb = "";
            cb += '<input type="checkbox" class="checkboxes" value="' + aaData.IDOrder + '" />';
            $("td:eq(0)", nRow).empty().append(cb);

            var o = "";
            o += '<a target="_blank" href="detail.aspx?id=' + aaData.IDOrder + '" data-id="' + aaData.IDOrder + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            if (aaData.Invoice != null)
                o += '<a target="_blank" href="invoice.aspx?id=' + aaData.IDOrder + '" data-id="' + aaData.IDOrder + '" class="btn_view btn btn-sm blue tooltips" data-original-title="Print Invoice"><i class="glyphicon glyphicon-print"></i></a>';
            if (aaData.Status == 'Payment Accepted')
                o += '<a target="_blank" href="shipping-label.aspx?id=' + aaData.IDOrder + '" data-id="' + aaData.IDOrder + '" class="btn_view btn btn-sm yellow tooltips" data-original-title="Print shipping label"><i class="glyphicon glyphicon-print"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDProduct + '" data-id="' + aaData.IDProduct + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            //o += '<a href="#" data-id="' + aaData.IDProduct + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(7)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
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

function Delete(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
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
            InitDT("#dtProduct");
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
        'c': 'bepro',
        'm': 'dpro',
        'data': { 'id': id }
    });
}

function StatusToggle(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            InitDT("#dtProduct");
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
        'c': 'bepro',
        'm': 'statustoggle',
        'data': { 'id': id }
    });
}