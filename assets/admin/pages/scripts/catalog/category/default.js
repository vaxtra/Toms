var REST = new OF({
    'url': '/api/services.asmx/Request',
});

$(document).ready(function () {
    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_categories").addClass("active");

    if (queryString("id")) {
        Precheck(parseInt(queryString("id")));
        InitDT("#dtCategory", parseInt(queryString("id")));
    }
    else
        InitDT("#dtCategory", 0);
});

function Precheck(idcat) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            $(".backLink").attr("href", "insert.aspx?id=" + idcat)

            var item = '<li><i class="fa fa-angle-right"></i></li>';
            item += '<li><a href="#" data-id="' + result.data.IDCategory + '">' + result.data.Name + '</a></li>';

            $(".breadcrumb").append(item);
            $(".breadcrumb > li").on("click", function (e) {
                var listitem = $(this);
                if ($(".breadcrumb > li").index(this) % 2 === 0 && $(this).find("a").data("id") !== undefined) {
                    $(".breadcrumb > li").slice(+$(".breadcrumb > li").index(this) + 1).remove();
                    InitDT("#dtCategory", $(this).find("a").data("id"));
                }
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 0,
                icon: "warning"
            });
        }
    };

    REST.sendRequest({
        'c': 'becat',
        'm': 'preload',
        'data': {
            'id': idcat
        }
    });
}

function StatusToggle(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 0,
                icon: "check"
            });

            if (result.data.IDCategoryParent === null) {
                InitDT("#dtCategory", 0);
            } else {
                InitDT("#dtCategory", result.data.IDCategoryParent);
            }
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 0,
                icon: "warning"
            });
        }
    };

    REST.sendRequest({
        'c': 'becat',
        'm': 'statustoggle',
        'data': {
            'id': id
        }
    });
}

function Delete(id, idparent) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            bootbox.alert(result.message, function () {
                InitDT("#dtCategory", idparent);
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 0,
                icon: "warning"
            });
        }
    };

    REST.sendRequest({
        'c': 'becat',
        'm': 'dcat',
        'data': {
            'id': id
        }
    });
}

function InitDT(element, idParent) {
    $("#hiddenIdCategoryParent").val(idParent);
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
        fnServerData: function (sSource, aoData, fnCallback) {
            aoData.push({
                name: "idCategoryParent",
                value: idParent
            });
            $.getJSON(sSource, aoData, function (json) {
                fnCallback(json);
            });
        },
        sAjaxSource: "/api/services.asmx/dtcat",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDCategory", sWidth: "5%" },
                    { mDataProp: "Name", sWidth: "60%" },
                    { mDataProp: "Active", sWidth: "15%" },
                    { mDataProp: "IDCategoryParent", sWidth: "20%" }
        ],
        aoColumnDefs: [{
            bSortable: false,
            aTargets: [3]
        }, {
            sClass: "text-center",
            aTargets: [0, 1, 2, 3]
        }],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            $("td:eq(2)", nRow).empty();
            var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDCategory + '" data-nama="' + aaData.Name + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDCategory + '" data-nama="' + aaData.Name + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            $("td:eq(2)", nRow).append(s);

            $("td:eq(3)", nRow).empty();
            var o = "";
            o += '<a href="#" data-id="' + aaData.IDCategory + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a> ';
            o += '<a href="update.aspx?id=' + aaData.IDCategory + '" data-id="' + aaData.IDCategory + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a> ';
            o += '<a href="#" data-parent="' + aaData.IDCategoryParent + '" data-id="' + aaData.IDCategory + '" data-nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a> ';
            $("td:eq(3)", nRow).append(o);
        },
        fnDrawCallback: function (e) {
            $(".btn_view").on("click", function (e) {
                e.preventDefault();
                Precheck(+$(this).data("id"));
                InitDT("#dtCategory", +$(this).data("id"));
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
                        Delete(+button.data("id"), +button.data("parent"));
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
    jQuery("#dtCategory_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline");
    jQuery("#dtCategory_wrapper .dataTables_length select").addClass("form-control input-xsmall");
    jQuery("#dtCategory_wrapper .dataTables_length select").select2();
}