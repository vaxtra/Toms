var REST = new OF({
    'url': '/api/services.asmx/request'
});

function Preload(idattr) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var result = data.d.data;
            $("#HiddenIDAttribute").val(result.IDAttribute);
            $(".linkInsert").attr("href", "insert.aspx?id=" + result.IDAttribute);
            $("#breadcrumbName").empty().append(result.Name);
            $(".caption").empty().append('<i class="fa fa-book"></i>' + result.Name);
            InitDT("#dtValue", result.IDAttribute, result.IsColor);
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
        'c': 'beval',
        'm': 'preload',
        data: {
            'idattr': idattr
        }
    });
}

function InitDT(element, idattr, isColor) {
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
        [5,10,20,50,-1],
        ["5","10","20","50","All"]
        ],
        iDisplayLength: -1,
        bSortClasses: false,
        bStateSave: false,
        bPaginate: true,
        bAutoWidth: false,
        bProcessing: true,
        bServerSide: true,
        bDestroy: true,
        bRetrieve: true,
        sPaginationType: "bootstrap",
        sAjaxSource: "/api/services.asmx/dtval",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        fnServerData: function (sSource, aoData, fnCallback) {
            aoData.push({
                name: "idattr",
                value: idattr
            });
            $.getJSON(sSource, aoData, function (json) {
                fnCallback(json);
            });
        },
        aoColumns: [{ mDataProp: "IDValue", sWidth: "5%" },
                    { mDataProp: "Name", sWidth: "60%" },
                    { mDataProp: "RGBColor", sWidth: "15%" },
                    { mDataProp: "IDAttribute", sWidth: "20%" }
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [3]
        }, {
            sClass: "text-center",
            aTargets: [0, 1, 2, 3]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            if (!isColor) {
                var o = "";
                o += '<a href="update.aspx?id=' + aaData.IDValue + '" data-id="' + aaData.IDValue + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a> ';
                o += '<a href="#" data-id="' + aaData.IDValue + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a> ';
                $("td:eq(2)", nRow).html(o);
            }
            else {

                $("td:eq(2)", nRow).empty();
                $("td:eq(2)", nRow).append('<span class="btn btn-sm red" style="background:' + aaData.RGBColor + ';border:' + '1px solid #ddd' + ';"><i class="glyphicon glyphicon-tint"></i></span>');

                var o = "";
                o += '<a href="update.aspx?id=' + aaData.IDValue + '" data-id="' + aaData.IDValue + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a> ';
                o += '<a href="#" data-id="' + aaData.IDValue + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a> ';
                $("td:eq(3)", nRow).html(o);
            }
        },
        fnDrawCallback: function (e) {
            $(".btn_delete").on("click", function (e) {
                e.preventDefault();
                var button = $(this);
                bootbox.confirm("Are you sure want to delete <b>" + button.data("nama") + "</b> ?", function (result) {
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

    if (!isColor) {
        e.fnSetColumnVis(2, false);
    }
}

function Delete(id) {
    var REST = new OF({
        'url': '/api/services.asmx/request'
    });
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
            Preload(+$("#HiddenIDAttribute").val());
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
        'c': 'beval',
        'm': 'dval',
        'data': {
            'id': id
        }
    });
}

$(document).ready(function () {
    if (!queryString("id"))
        window.location = "../default.aspx";

    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_attributes").addClass("active");

    Preload(+queryString("id"));
});