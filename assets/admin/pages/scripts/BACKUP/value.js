$(document).ready(function () {
    var queryString_id = queryString('id');
    if (queryString_id === '') {
        toastr.error('<b>Attribute is not exists.</b>');
        window.setTimeout(function () {
            location.href = "../Default.aspx";
        }, 1000);
    }
    Toastr.init();
    $('.colorpicker-default').colorpicker();

    $("#menu_catalog").addClass("active");
    $("#submenu_attributes").addClass("active");
    $("#arrow_catalog").addClass("open");
    ValueColor_ValidationInsert($("#formValueColor_Insert"));
    Value_ValidationInsert($("#formValue_Insert"));

    $(document).ajaxStop(function () {
        //console.clear();
    });
    
    Attribute_GetDetail(queryString_id);
    $("#modalValueColor_Insert").on('show.bs.modal', function () {
        $("#tbRGBColor").val("#000000");
        $('.colorpicker-default').colorpicker('setValue', "#000000");
    });

    $("#modalValue_Insert").on('show.bs.modal', function () {
    });
});

function Attribute_GetDetail(idAttribute) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Attribute.asmx/GetDetail",
        data: JSON.stringify({ 'idAttribute': idAttribute }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, xhr) {
            if (data.d != null) {
                Attribute_SetForm(data.d);
            }
            else {
                toastr.error('<b>Attribute is not exists.</b>');
                window.setTimeout(function () {
                    location.href = "../Default.aspx";
                }, 1000);
            }
        },
        error: function (jqXHR, errorType, exception) {
            if (jqXHR.status === 0) {
                toastr.error('<b>Not connect. Please, Verify Network.</b>');
            } else if (jqXHR.status == 404) {
                toastr.error('<b>Requested page not found. [404]</b>');
            } else if (jqXHR.status == 500) {
                toastr.error('<b>Internal Server Error [500]</b>');
            } else if (exception === 'parsererror') {
                toastr.error('<b>Requested JSON parse failed</b>');
            } else if (exception === 'timeout') {
                toastr.error('<b>Time out error</b>');
            } else if (exception === 'abort') {
                toastr.error('<b>Ajax request aborted</b>');
            } else {
                toastr.error('<b>Uncaught Error.</b>');
            }

            window.setTimeout(function () {
                location.href = "../Default.aspx";
            }, 1000);
        }
    });
}

function Attribute_SetForm(data) {
    var date = new Date();
    document.title = data.Name + ' - WIT. Commerce';
    $("#page-title").html(data.Name + ' <small>Attribute</small>');
    $("#breadcrumbName").html(data.Name);
    $("#hiddenIdAttribute").val(data.IDAttribute);

    if (data.IsColor) {
        Value_IsColor_GetAll_DataTable(data.IDAttribute);
        hrefModalValue.href = '#modalValueColor_Insert';
    }
    else {
        Value_GetAll_DataTable(data.IDAttribute);
        hrefModalValue.href = '#modalValue_Insert';
    }
}

function Value_IsColor_GetAll_DataTable(idAttribute) {
    $("#dtValue").each(function () {
        $("#dtValue").dataTable().fnDestroy();
    });

    var oTableAttributeIsColor = $('#dtValue').dataTable({
        "oLanguage": {
            "sProcessing": '<img src="' + Metronic.getGlobalImgPath() + 'loading-spinner-grey.gif"/><span>&nbsp;&nbsp;Loading...</span>',
            "sLengthMenu": "_MENU_ records",
            "oPaginate": {
                "sPrevious": "Prev",
                "sNext": "Next"
            },
            "sAjaxRequestGeneralError": "Could not complete request. Please check your internet connection",
            "sEmptyTable": "No records to display",
            "sZeroRecords": "No matching records found"
        },
        "aLengthMenu": [
        [5, 10, 15, 20, -1],
        [5, 10, 15, 20, "All"]
        ],
        "iDisplayLength": 5,
        "bSortClasses": false,
        "bStateSave": false,
        "bPaginate": true,
        "bAutoWidth": false,
        "bProcessing": true,
        "bServerSide": true,
        "bDestroy": true,
        "bRetrieve": true,
        "sPaginationType": "bootstrap",
        "fnServerData": function (sSource, aoData, fnCallback) {
            aoData.push({ "name": "idAttribute", "value": idAttribute });
            $.getJSON(sSource, aoData, function (json) {
                fnCallback(json);
            });
        },
        "sAjaxSource": window.location.origin + "/api/Backend_Value.asmx/GetAll_DataTable",
        "fnInitComplete": function () { this.fnSetFilteringDelay(500); },
        "aoColumns": [
        { "mDataProp": "IDValue", "sWidth": "5%" },
        { "mDataProp": "Name", "sWidth": "65%" },
        { "mDataProp": "RGBColor", "sWidth": "15%" },
        { "mDataProp": "IDValue", "sWidth": "15%" }
        ],
        "aoColumnDefs": [{
            'bSortable': false, 'aTargets': [2, 3]
        }, {
            'sClass': 'text-center', 'aTargets': [0, 1, 2, 3]
        }],
        "fnRowCallback": function (nRow, aaData, dIndex, rIndex) {
            $('td:eq(2)', nRow).empty();
            var color = '<span class="btn btn-sm" style="background:' + aaData.RGBColor + ';color:' + aaData.RGBColor + ';border:solid 0.5px #ddd;width:40px;"><i class="fa"></i></span>';
            $('td:eq(2)', nRow).append(color);

            $('td:eq(3)', nRow).empty();
            var actions = '';
            actions += '<a href="#modalValueColor_Update" data_id="' + aaData.IDValue + '" class="btn_update_color btn btn-sm yellow tooltips" data-toggle="modal" data-original-title="Update"><i class="fa fa-edit"></i></a>';
            actions += '<a href="#" data_id="' + aaData.IDValue + '" data_nama="' + aaData.Name + '" class="btn_delete_color btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $('td:eq(3)', nRow).append(actions);
        },
        "fnDrawCallback": function (oSettings) {
            $('a', this.fnGetNodes()).tooltip({
                "delay": 0,
                "track": true,
                "fade": 250
            });

            $(".btn_update_color").click(function (e) {
                e.preventDefault();
                var idAttribute = $("#hiddenIdAttribute").val();
                var idValue = $(this).attr("data_id");
                Value_GetDetail(idAttribute, idValue, true);
            });

            $(".btn_delete_color").click(function (e) {
                e.preventDefault();
                var idAttribute = $("#hiddenIdAttribute").val();
                var t = $(this).attr("data_id");
                var n = $(this).attr("data_nama");
                bootbox.confirm("Are you sure want to delete <b>" + n + "</b> ?", function (e) {
                    if (e) {
                        Value_Delete(idAttribute, t, true);
                    }
                })
            });
        }
    });

jQuery('#dtValue_wrapper .dataTables_filter input').addClass("form-control input-medium input-inline");
jQuery('#dtValue_wrapper .dataTables_length select').addClass("form-control input-xsmall");
jQuery('#dtValue_wrapper .dataTables_length select').select2();
}

function ValueColor_ValidationInsert(e) {
    var t = $(".alert-danger", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        ignore: "",
        messages: {},
        rules: {
            tbNameColor: {
                required: true,
                minlength: 1,
                maxlength: 25,
                existsColorInsert: true
            }
        },
        errorPlacement: function (e, t) {
            if (t.parent(".input-group").size() > 0) {
                e.insertAfter(t.parent(".input-group"))
            } else if (t.attr("data-error-container")) {
                e.appendTo(t.attr("data-error-container"))
            } else if (t.parents(".radio-list").size() > 0) {
                e.appendTo(t.parents(".radio-list").attr("data-error-container"))
            } else if (t.parents(".radio-inline").size() > 0) {
                e.appendTo(t.parents(".radio-inline").attr("data-error-container"))
            } else if (t.parents(".checkbox-list").size() > 0) {
                e.appendTo(t.parents(".checkbox-list").attr("data-error-container"))
            } else if (t.parents(".checkbox-inline").size() > 0) {
                e.appendTo(t.parents(".checkbox-inline").attr("data-error-container"))
            } else if (t.parents(".input-group").size() > 0) {
                e.insertAfter(t.parents(".input-group"))
            } else {
                e.insertAfter(t)
            }
        },
        invalidHandler: function (e, n) {
            t.show();
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            e.closest(".form-group").removeClass("has-error")
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();
            var idAttribute = $("#hiddenIdAttribute").val();
            var name = $("#tbNameColor").val();
            var rgbColor = $("#tbRGBColor").val();
            Value_Insert(idAttribute, name, rgbColor, true);
        }
    });
jQuery.validator.addMethod("existsColorInsert", function (e, t, n) {
    var idAttribute = $("#hiddenIdAttribute").val();
    return Value_ValidationName_Insert(idAttribute, e);
}, "Value is already exists.")
}

function ValueColor_ValidationName_Insert(idAttribute, e) {
    var n = false;
    $("#tbNameColor").attr("readonly", true).attr("disabled", true).addClass("spinner");
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Value.asmx/ValidationName_Add",
        data: '{"idAttribute":"' + idAttribute + '", "name":"' + e + '"}',
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, r) {
            if (e.d) n = true;
        },
        error: function (e, t, r) {
            if (e.status === 0) {
                toastr.error("<b>Not connect. Please, Verify Network.</b>")
            } else if (e.status == 404) {
                toastr.error("<b>Requested page not found. [404]</b>")
            } else if (e.status == 500) {
                toastr.error("<b>Internal Server Error [500]</b>")
            } else if (r === "parsererror") {
                toastr.error("<b>Requested JSON parse failed</b>")
            } else if (r === "timeout") {
                toastr.error("<b>Time out error</b>")
            } else if (r === "abort") {
                toastr.error("<b>Ajax request aborted</b>")
            } else {
                toastr.error("<b>Uncaught Error.</b>")
            }
            n = true
        }
    });
$("#tbNameColor").attr("readonly", false).attr("disabled", false).removeClass("spinner");
return n;
}

function ValueColor_ValidationUpdate(e) {
    var t = $(".alert-danger", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        ignore: "",
        messages: {},
        rules: {
            tbNameColor_Update: {
                required: true,
                minlength: 1,
                maxlength: 25,
                existsUpdate: true
            }
        },
        errorPlacement: function (e, t) {
            if (t.parent(".input-group").size() > 0) {
                e.insertAfter(t.parent(".input-group"))
            } else if (t.attr("data-error-container")) {
                e.appendTo(t.attr("data-error-container"))
            } else if (t.parents(".radio-list").size() > 0) {
                e.appendTo(t.parents(".radio-list").attr("data-error-container"))
            } else if (t.parents(".radio-inline").size() > 0) {
                e.appendTo(t.parents(".radio-inline").attr("data-error-container"))
            } else if (t.parents(".checkbox-list").size() > 0) {
                e.appendTo(t.parents(".checkbox-list").attr("data-error-container"))
            } else if (t.parents(".checkbox-inline").size() > 0) {
                e.appendTo(t.parents(".checkbox-inline").attr("data-error-container"))
            } else if (t.parents(".input-group").size() > 0) {
                e.insertAfter(t.parents(".input-group"))
            } else {
                e.insertAfter(t)
            }
        },
        invalidHandler: function (e, n) {
            t.show();
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            e.closest(".form-group").removeClass("has-error")
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();
            var idAttribute = $("#hiddenIdAttribute").val();
            var idValue = $("#hiddenIdValue").val();
            var name = $("#tbNameColor_Update").val();
            var rgbColor = $("#tbRGBColor_Update").val();
            Value_Update(idAttribute, idValue, name, rgbColor, true);
        }
    });
jQuery.validator.addMethod("existsUpdate", function (e, t, n) {
    var idAttribute = $("#hiddenIdAttribute").val();
    var idValue = $("#hiddenIdValue").val();
    return ValueColor_ValidationName_Update(idAttribute, idValue, e);
}, "Value is already exists.");
}

function ValueColor_ValidationName_Update(idAttribute, idValue, e) {
    var n = false;
    $("#tbNameColor_Update").attr("readonly", true).attr("disabled", true).addClass("spinner");
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Value.asmx/ValidationName_Update",
        data: '{"idAttribute":"' + idAttribute + '", "idValue":"' + idValue + '", "name":"' + e + '"}',
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, r) {
            if (e.d) n = true;
        },
        error: function (e, t, r) {
            if (e.status === 0) {
                toastr.error("<b>Not connect. Please, Verify Network.</b>")
            } else if (e.status == 404) {
                toastr.error("<b>Requested page not found. [404]</b>")
            } else if (e.status == 500) {
                toastr.error("<b>Internal Server Error [500]</b>")
            } else if (r === "parsererror") {
                toastr.error("<b>Requested JSON parse failed</b>")
            } else if (r === "timeout") {
                toastr.error("<b>Time out error</b>")
            } else if (r === "abort") {
                toastr.error("<b>Ajax request aborted</b>")
            } else {
                toastr.error("<b>Uncaught Error.</b>")
            }
            n = true
        }
    });
$("#tbNameColor_Update").attr("readonly", false).attr("disabled", false).removeClass("spinner");
return n;
}

function Value_GetAll_DataTable(idAttribute) {
    $("#dtValue").each(function () {
        $("#dtValue").dataTable().fnDestroy();
    });

    var oTableAttribute = $('#dtValue').dataTable({
        "oLanguage": {
            "sProcessing": '<img src="' + Metronic.getGlobalImgPath() + 'loading-spinner-grey.gif"/><span>&nbsp;&nbsp;Loading...</span>',
            "sLengthMenu": "_MENU_ records",
            "oPaginate": {
                "sPrevious": "Prev",
                "sNext": "Next"
            },
            "sAjaxRequestGeneralError": "Could not complete request. Please check your internet connection",
            "sEmptyTable": "No records to display",
            "sZeroRecords": "No matching records found"
        },
        "aLengthMenu": [
        [5, 10, 15, 20, -1],
        [5, 10, 15, 20, "All"]
        ],
        "iDisplayLength": 5,
        "bSortClasses": false,
        "bStateSave": false,
        "bPaginate": true,
        "bAutoWidth": false,
        "bProcessing": true,
        "bServerSide": true,
        "bDestroy": true,
        "bRetrieve": true,
        "sPaginationType": "bootstrap",
        "fnServerData": function (sSource, aoData, fnCallback) {
            aoData.push({ "name": "idAttribute", "value": idAttribute });
            $.getJSON(sSource, aoData, function (json) {
                fnCallback(json);
            });
        },
        "sAjaxSource": window.location.origin + "/api/Backend_Value.asmx/GetAll_DataTable",
        "fnInitComplete": function () { this.fnSetFilteringDelay(500); },
        "aoColumns": [
        { "mDataProp": "IDValue", "sWidth": "5%" },
        { "mDataProp": "Name", "sWidth": "65%" },
        { "mDataProp": "RGBColor", "sWidth": "15%" },
        { "mDataProp": "IDValue", "sWidth": "15%" }
        ],
        "aoColumnDefs": [{
            'bSortable': false, 'aTargets': [2, 3]
        }, {
            'sClass': 'text-center', 'aTargets': [0, 1, 2, 3]
        }],
        "fnRowCallback": function (nRow, aaData, dIndex, rIndex) {
            $('td:eq(2)', nRow).empty();
            var actions = '';
            actions += '<a href="#modalValue_Update" data_id="' + aaData.IDValue + '" class="btn_update btn btn-sm yellow tooltips" data-toggle="modal" data-original-title="Update"><i class="fa fa-edit"></i></a>';
            actions += '<a href="#" data_id="' + aaData.IDValue + '" data_nama="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a> ';
            $('td:eq(2)', nRow).append(actions);
        },
        "fnDrawCallback": function (oSettings) {
            $('a', this.fnGetNodes()).tooltip({
                "delay": 0,
                "track": true,
                "fade": 250
            });

            $(".btn_update").click(function (e) {
                e.preventDefault();
                var idAttribute = $("#hiddenIdAttribute").val();
                var idValue = $(this).attr("data_id");
                Value_GetDetail(idAttribute, idValue, false);
            });

            $(".btn_delete").click(function (e) {
                e.preventDefault();
                var idAttribute = $("#hiddenIdAttribute").val();
                var t = $(this).attr("data_id");
                var n = $(this).attr("data_nama");
                bootbox.confirm("Are you sure want to delete <b>" + n + "</b> ?", function (e) {
                    if (e) {
                        Value_Delete(idAttribute, t, false);
                    }
                })
            });
        }
    });

jQuery('#dtValue_wrapper .dataTables_filter input').addClass("form-control input-medium input-inline");
jQuery('#dtValue_wrapper .dataTables_length select').addClass("form-control input-xsmall");
jQuery('#dtValue_wrapper .dataTables_length select').select2();
oTableAttribute.fnSetColumnVis(2, false);
}

function Value_SetForm(e, isColor) {
    if (isColor) {
        $(".title-value-color-update-modal").html("Update Value : <b>" + e.Name + "</b>");
        $("#hiddenIdValue").val(e.IDValue);
        $("#tbNameColor_Update").val(e.Name);
        $("#tbRGBColor_Update").val(e.RGBColor);
        $('.colorpicker-default').colorpicker('setValue', e.RGBColor);
        ValueColor_ValidationUpdate($("#formValueColor_Update"));
    }
    else {
        $(".title-value-update-modal").html("Update Value : <b>" + e.Name + "</b>");
        $("#hiddenIdValue").val(e.IDValue);
        $("#tbName_Update").val(e.Name);
        Value_ValidationUpdate($("#formValue_Update"));
    }
}

function Value_GetDetail(idAttribute, idValue, isColor) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Value.asmx/GetDetail",
        data: JSON.stringify({
            idAttribute: idAttribute,
            idValue: idValue
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, n) {
            if (e.d != null) {
                Value_SetForm(e.d, isColor);
            } else {
                toastr.error("<b>Value is not exists.</b>");
            }
        },
        error: function (e, t, n) {
            if (e.status === 0) {
                toastr.error("<b>Not connect. Please, Verify Network.</b>")
            } else if (e.status == 404) {
                toastr.error("<b>Requested page not found. [404]</b>")
            } else if (e.status == 500) {
                toastr.error("<b>Internal Server Error [500]</b>")
            } else if (n === "parsererror") {
                toastr.error("<b>Requested JSON parse failed</b>")
            } else if (n === "timeout") {
                toastr.error("<b>Time out error</b>")
            } else if (n === "abort") {
                toastr.error("<b>Ajax request aborted</b>")
            } else {
                toastr.error("<b>Uncaught Error.</b>")
            }
        },
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true,
                message: "Processing..."
            })
        },
        complete: function () {
            Metronic.unblockUI();
        }
    })
}

function Value_ValidationInsert(e) {
    var t = $(".alert-danger", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        ignore: "",
        messages: {},
        rules: {
            tbName: {
                required: true,
                minlength: 1,
                maxlength: 25,
                exists: true
            }
        },
        errorPlacement: function (e, t) {
            if (t.parent(".input-group").size() > 0) {
                e.insertAfter(t.parent(".input-group"))
            } else if (t.attr("data-error-container")) {
                e.appendTo(t.attr("data-error-container"))
            } else if (t.parents(".radio-list").size() > 0) {
                e.appendTo(t.parents(".radio-list").attr("data-error-container"))
            } else if (t.parents(".radio-inline").size() > 0) {
                e.appendTo(t.parents(".radio-inline").attr("data-error-container"))
            } else if (t.parents(".checkbox-list").size() > 0) {
                e.appendTo(t.parents(".checkbox-list").attr("data-error-container"))
            } else if (t.parents(".checkbox-inline").size() > 0) {
                e.appendTo(t.parents(".checkbox-inline").attr("data-error-container"))
            } else if (t.parents(".input-group").size() > 0) {
                e.insertAfter(t.parents(".input-group"))
            } else {
                e.insertAfter(t)
            }
        },
        invalidHandler: function (e, n) {
            t.show();
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            e.closest(".form-group").removeClass("has-error")
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();
            var idAttribute = $("#hiddenIdAttribute").val();
            var name = $("#tbName").val();
            Value_Insert(idAttribute, name, null, false);
        }
    });
jQuery.validator.addMethod("exists", function (e, t, n) {
    var idAttribute = $("#hiddenIdAttribute").val();
    return Value_ValidationName_Insert(idAttribute, e);
}, "Value is already exists.")
}

function Value_ValidationName_Insert(idAttribute, e) {
    var n = false;
    $("#tbNameColor").attr("readonly", true).attr("disabled", true).addClass("spinner");
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Value.asmx/ValidationName_Add",
        data: '{"idAttribute":"' + idAttribute + '", "name":"' + e + '"}',
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, r) {
            if (e.d) n = true;
        },
        error: function (e, t, r) {
            if (e.status === 0) {
                toastr.error("<b>Not connect. Please, Verify Network.</b>")
            } else if (e.status == 404) {
                toastr.error("<b>Requested page not found. [404]</b>")
            } else if (e.status == 500) {
                toastr.error("<b>Internal Server Error [500]</b>")
            } else if (r === "parsererror") {
                toastr.error("<b>Requested JSON parse failed</b>")
            } else if (r === "timeout") {
                toastr.error("<b>Time out error</b>")
            } else if (r === "abort") {
                toastr.error("<b>Ajax request aborted</b>")
            } else {
                toastr.error("<b>Uncaught Error.</b>")
            }
            n = true
        }
    });
$("#tbNameColor").attr("readonly", false).attr("disabled", false).removeClass("spinner");
return n;
}

function Value_ValidationUpdate(e) {
    var t = $(".alert-danger", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        ignore: "",
        messages: {},
        rules: {
            tbName_Update: {
                required: true,
                minlength: 1,
                maxlength: 25,
                exists: true
            }
        },
        errorPlacement: function (e, t) {
            if (t.parent(".input-group").size() > 0) {
                e.insertAfter(t.parent(".input-group"))
            } else if (t.attr("data-error-container")) {
                e.appendTo(t.attr("data-error-container"))
            } else if (t.parents(".radio-list").size() > 0) {
                e.appendTo(t.parents(".radio-list").attr("data-error-container"))
            } else if (t.parents(".radio-inline").size() > 0) {
                e.appendTo(t.parents(".radio-inline").attr("data-error-container"))
            } else if (t.parents(".checkbox-list").size() > 0) {
                e.appendTo(t.parents(".checkbox-list").attr("data-error-container"))
            } else if (t.parents(".checkbox-inline").size() > 0) {
                e.appendTo(t.parents(".checkbox-inline").attr("data-error-container"))
            } else if (t.parents(".input-group").size() > 0) {
                e.insertAfter(t.parents(".input-group"))
            } else {
                e.insertAfter(t)
            }
        },
        invalidHandler: function (e, n) {
            t.show();
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            e.closest(".form-group").removeClass("has-error")
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();
            var idAttribute = $("#hiddenIdAttribute").val();
            var idValue = $("#hiddenIdValue").val();
            var name = $("#tbName_Update").val();
            Value_Update(idAttribute, idValue, name, null, false);
        }
    });
jQuery.validator.addMethod("exists", function (e, t, n) {
    var idAttribute = $("#hiddenIdAttribute").val();
    var idValue = $("#hiddenIdValue").val();
    return Value_ValidationName_Update(idAttribute, idValue, e);
}, "Value is already exists.")
}

function Value_ValidationName_Update(idAttribute, idValue, e) {
    var n = false;
    $("#tbName_Update").attr("readonly", true).attr("disabled", true).addClass("spinner");
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Value.asmx/ValidationName_Update",
        data: '{"idAttribute":"' + idAttribute + '", "idValue":"' + idValue + '", "name":"' + e + '"}',
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, r) {
            if (e.d) n = true;
        },
        error: function (e, t, r) {
            if (e.status === 0) {
                toastr.error("<b>Not connect. Please, Verify Network.</b>")
            } else if (e.status == 404) {
                toastr.error("<b>Requested page not found. [404]</b>")
            } else if (e.status == 500) {
                toastr.error("<b>Internal Server Error [500]</b>")
            } else if (r === "parsererror") {
                toastr.error("<b>Requested JSON parse failed</b>")
            } else if (r === "timeout") {
                toastr.error("<b>Time out error</b>")
            } else if (r === "abort") {
                toastr.error("<b>Ajax request aborted</b>")
            } else {
                toastr.error("<b>Uncaught Error.</b>")
            }
            n = true
        }
    });
$("#tbName_Update").attr("readonly", false).attr("disabled", false).removeClass("spinner");
return n;
}

function Value_Insert(idAttribute, name, rgbColor, isColor) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Value.asmx/Add",
        data: JSON.stringify({
            idAttribute: idAttribute,
            name: name,
            rgbColor: rgbColor
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, n, r) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
                $("#formValueColor_Insert").clearForm();
                if (isColor) {
                    Value_IsColor_GetAll_DataTable(idAttribute);
                } else {
                    Value_GetAll_DataTable(idAttribute);
                }
                $(".default").trigger("click");
            } else if (e.d.Status == "Warning") {
                toastr.warning(e.d.Deskripsi)
            } else {
                toastr.error(e.d.Deskripsi)
            }
        }
    })
}

function Value_Update(idAttribute, idValue, name, rgbColor, isColor) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Value.asmx/Update",
        data: JSON.stringify({
            idAttribute: idAttribute,
            idValue: idValue,
            name: name,
            rgbColor: rgbColor
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, n, r) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
                if (isColor) {
                    $("#formValueColor_Update").clearForm();
                    Value_IsColor_GetAll_DataTable(idAttribute);
                } else {
                    $("#formValue_Update").clearForm();
                    Value_GetAll_DataTable(idAttribute);
                }
                $(".default").trigger("click")
            } else if (e.d.Status == "Warning") {
                toastr.warning(e.d.Deskripsi)
            } else {
                toastr.error(e.d.Deskripsi)
            }
        }
    })
}

function Value_Delete(idAttribute, idValue, isColor) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Value.asmx/Delete",
        data: JSON.stringify({
            idValue: idValue
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, n, r) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
                if (isColor) {
                    $("#formValueColor_Update").clearForm();
                    Value_IsColor_GetAll_DataTable(idAttribute);
                } else {
                    $("#formValue_Update").clearForm();
                    Value_GetAll_DataTable(idAttribute);
                }
                $(".default").trigger("click")
            } else if (e.d.Status == "Warning") {
                toastr.warning(e.d.Deskripsi)
            } else {
                toastr.error(e.d.Deskripsi)
            }
        }
    })
}