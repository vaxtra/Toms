﻿	function Attribute_GetAll_DataTable() { $("#dtAttribute").each(function () { $("#dtAttribute").dataTable().fnDestroy() }); var e = $("#dtAttribute").dataTable({ oLanguage: { sProcessing: '<img src="' + Metronic.getGlobalImgPath() + 'loading-spinner-grey.gif"/><span>&nbsp;&nbsp;Loading...</span>', sLengthMenu: "_MENU_ records", oPaginate: { sPrevious: "Prev", sNext: "Next" }, sAjaxRequestGeneralError: "Could not complete request. Please check your internet connection", sEmptyTable: "No records to display", sZeroRecords: "No matching records found" }, aLengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "All"]], iDisplayLength: 5, bSortClasses: false, bStateSave: true, bPaginate: true, bAutoWidth: false, bProcessing: true, bServerSide: true, bDestroy: true, bRetrieve: true, sPaginationType: "bootstrap", sAjaxSource: window.location.origin + "/api/Backend_Attribute.asmx/GetAll_DataTable", fnInitComplete: function () { this.fnSetFilteringDelay(500) }, aoColumns: [{ mDataProp: "IDAttribute", sWidth: "5%" }, { mDataProp: "Name", sWidth: "70%" }, { mDataProp: "CountValues", sWidth: "10%" }, { mDataProp: "IDAttribute", sWidth: "15%" }], aoColumnDefs: [{ bSortable: false, aTargets: [3] }, { sClass: "text-center", aTargets: [0, 1, 2, 3] }], fnRowCallback: function (e, t, n, r) { $("td:eq(3)", e).empty(); var i = ""; i += '<a href="./value/Default.aspx?id=' + t.IDAttribute + '" class="btn_view btn btn-sm green tooltips" data-original-title="View Values"><i class="glyphicon glyphicon-eye-open"></i></a> '; i += '<a href="#modalAttribute_Update" data_id="' + t.IDAttribute + '" class="btn_update btn btn-sm yellow tooltips" data-toggle="modal" data-original-title="Update"><i class="fa fa-edit"></i></a>'; i += '<a href="#" data_id="' + t.IDAttribute + '" data_nama="' + t.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a> '; $("td:eq(3)", e).append(i) }, fnDrawCallback: function (e) { $("a", this.fnGetNodes()).tooltip({ delay: 0, track: true, fade: 250 }); $(".btn_delete").click(function (e) { e.preventDefault(); var t = $(this).attr("data_id"); var n = $(this).attr("data_nama"); bootbox.confirm("Are you sure want to delete <b>" + n + "</b> ?", function (e) { if (e) { bootbox.confirm("All Products of <b>" + n + "</b> will be deleted, proceed?", function (e) { if (e) { Attribute_Delete(t) } }) } }) }); $(".btn_update").click(function (e) { e.preventDefault(); var t = $(this).attr("data_id"); Attribute_GetDetail(t) }) } }); jQuery("#dtAttribute_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline"); jQuery("#dtAttribute_wrapper .dataTables_length select").addClass("form-control input-xsmall"); jQuery("#dtAttribute_wrapper .dataTables_length select").select2(); Attribute_ValidationInsert($("#formAttribute_Insert")) } function Attribute_ValidationInsert(e) { var t = $(".alert-danger", e); e.validate({ errorElement: "span", errorClass: "help-block", focusInvalid: true, ignore: "", messages: {}, rules: { tbName: { required: true, minlength: 3, maxlength: 25, exists: true } }, errorPlacement: function (e, t) { if (t.parent(".input-group").size() > 0) { e.insertAfter(t.parent(".input-group")) } else if (t.attr("data-error-container")) { e.appendTo(t.attr("data-error-container")) } else if (t.parents(".radio-list").size() > 0) { e.appendTo(t.parents(".radio-list").attr("data-error-container")) } else if (t.parents(".radio-inline").size() > 0) { e.appendTo(t.parents(".radio-inline").attr("data-error-container")) } else if (t.parents(".checkbox-list").size() > 0) { e.appendTo(t.parents(".checkbox-list").attr("data-error-container")) } else if (t.parents(".checkbox-inline").size() > 0) { e.appendTo(t.parents(".checkbox-inline").attr("data-error-container")) } else if (t.parents(".input-group").size() > 0) { e.insertAfter(t.parents(".input-group")) } else { e.insertAfter(t) } }, invalidHandler: function (e, n) { t.show(); Metronic.scrollTo(t, -200) }, highlight: function (e) { $(e).closest(".form-group").addClass("has-error") }, unhighlight: function (e) { $(e).closest(".form-group").removeClass("has-error") }, success: function (e) { e.closest(".form-group").removeClass("has-error") }, onkeyup: false, submitHandler: function (e) { t.hide(); var n = $("#tbName").val(); var r = true; if (!$("#cbIsColor").is(":checked")) { r = false } Attribute_Insert(n, r) } }); jQuery.validator.addMethod("exists", function (e, t, n) { return Attribute_ValidationName_Add(e) }, "Attribute is already exists.") } function Attribute_ValidationName_Add(e) { var t = false; $("#tbName").attr("readonly", true).attr("disabled", true).addClass("spinner"); $.ajax({ type: "POST", url: window.location.origin + "/api/Backend_Attribute.asmx/ValidationName_Add", data: '{"name":"' + e + '"}', async: false, dataType: "json", contentType: "application/json; charset=utf-8", success: function (e, n, r) { if (e.d) t = true }, error: function (e, n, r) { if (e.status === 0) { toastr.error("<b>Not connect. Please, Verify Network.</b>") } else if (e.status == 404) { toastr.error("<b>Requested page not found. [404]</b>") } else if (e.status == 500) { toastr.error("<b>Internal Server Error [500]</b>") } else if (r === "parsererror") { toastr.error("<b>Requested JSON parse failed</b>") } else if (r === "timeout") { toastr.error("<b>Time out error</b>") } else if (r === "abort") { toastr.error("<b>Ajax request aborted</b>") } else { toastr.error("<b>Uncaught Error.</b>") } t = true } }); $("#tbName").attr("readonly", false).attr("disabled", false).removeClass("spinner"); return t } function Attribute_Insert(e, t) { $.ajax({ type: "POST", url: window.location.origin + "/api/Backend_Attribute.asmx/Add", data: JSON.stringify({ name: e, isColor: t }), dataType: "json", contentType: "application/json; charset=utf-8", success: function (e, t, n) { if (e.d.Status == "Success") { toastr.success(e.d.Deskripsi); $("#formAttribute_Insert").clearForm(); Attribute_GetAll_DataTable(); $(".default").trigger("click") } else if (e.d.Status == "Warning") { toastr.warning(e.d.Deskripsi) } else { toastr.error(e.d.Deskripsi) } } }) } function Attribute_Delete(e) { $.ajax({ type: "POST", url: window.location.origin + "/api/Backend_Attribute.asmx/Delete", data: JSON.stringify({ idAttribute: e }), dataType: "json", contentType: "application/json; charset=utf-8", success: function (e, t, n) { if (e.d.Status == "Success") { toastr.success(e.d.Deskripsi); Attribute_GetAll_DataTable() } else if (e.d.Status == "Warning") { toastr.warning(e.d.Deskripsi) } else { toastr.error(e.d.Deskripsi) } } }) } function Attribute_GetDetail(e) { $.ajax({ type: "POST", url: window.location.origin + "/api/Backend_Attribute.asmx/GetDetail", data: JSON.stringify({ idAttribute: e }), dataType: "json", contentType: "application/json; charset=utf-8", success: function (e, t, n) { if (e.d != null) { Attribute_SetForm(e.d) } else { toastr.error("<b>Attribute is not exists.</b>"); window.setTimeout(function () { location.href = "./Default.aspx" }, 1e3) } }, error: function (e, t, n) { if (e.status === 0) { toastr.error("<b>Not connect. Please, Verify Network.</b>") } else if (e.status == 404) { toastr.error("<b>Requested page not found. [404]</b>") } else if (e.status == 500) { toastr.error("<b>Internal Server Error [500]</b>") } else if (n === "parsererror") { toastr.error("<b>Requested JSON parse failed</b>") } else if (n === "timeout") { toastr.error("<b>Time out error</b>") } else if (n === "abort") { toastr.error("<b>Ajax request aborted</b>") } else { toastr.error("<b>Uncaught Error.</b>") } window.setTimeout(function () { location.href = "./Default.aspx" }, 1e3) }, beforeSend: function () { Metronic.blockUI({ boxed: true, message: "Processing..." }) }, complete: function () { Metronic.unblockUI() } }) } function Attribute_SetForm(e) { var t = new Date; $(".title-value-modal_update").html("Update Attribute : <b>" + e.Name + "</b>"); $("#hiddenIdAttribute").val(e.IDAttribute); $("#tbName_Update").val(e.Name); if (e.IsColor) { $(".bootstrap-switch-id-cbIsColor_Update").removeClass("bootstrap-switch-off"); $(".bootstrap-switch-id-cbIsColor_Update").addClass("bootstrap-switch-on"); $("#cbIsColor_Update").attr("checked", "true") } else { $(".bootstrap-switch-id-cbIsColor_Update").removeClass("bootstrap-switch-on"); $(".bootstrap-switch-id-cbIsColor_Update").addClass("bootstrap-switch-off"); $("#cbIsColor_Update").attr("checked", "false") } Attribute_ValidationUpdate($("#formAttribute_Update")) } function Attribute_ValidationUpdate(e) { var t = $(".alert-danger", e); e.validate({ errorElement: "span", errorClass: "help-block", focusInvalid: true, ignore: "", messages: {}, rules: { tbName_Update: { required: true, minlength: 3, maxlength: 25, existsUpdate: true } }, errorPlacement: function (e, t) { if (t.parent(".input-group").size() > 0) { e.insertAfter(t.parent(".input-group")) } else if (t.attr("data-error-container")) { e.appendTo(t.attr("data-error-container")) } else if (t.parents(".radio-list").size() > 0) { e.appendTo(t.parents(".radio-list").attr("data-error-container")) } else if (t.parents(".radio-inline").size() > 0) { e.appendTo(t.parents(".radio-inline").attr("data-error-container")) } else if (t.parents(".checkbox-list").size() > 0) { e.appendTo(t.parents(".checkbox-list").attr("data-error-container")) } else if (t.parents(".checkbox-inline").size() > 0) { e.appendTo(t.parents(".checkbox-inline").attr("data-error-container")) } else if (t.parents(".input-group").size() > 0) { e.insertAfter(t.parents(".input-group")) } else { e.insertAfter(t) } }, invalidHandler: function (e, n) { t.show(); Metronic.scrollTo(t, -200) }, highlight: function (e) { $(e).closest(".form-group").addClass("has-error") }, unhighlight: function (e) { $(e).closest(".form-group").removeClass("has-error") }, success: function (e) { e.closest(".form-group").removeClass("has-error") }, onkeyup: false, submitHandler: function (e) { t.hide(); var n = $("#hiddenIdAttribute").val(); var r = $("#tbName_Update").val(); var i = true; if (!$("#cbIsColor_Update").is(":checked")) { i = false } Attribute_Update(n, r, i) } }); jQuery.validator.addMethod("existsUpdate", function (e, t, n) { var r = $("#hiddenIdAttribute").val(); return Attribute_ValidationName_Update(r, e) }, "Attribute is already exists.") } function Attribute_ValidationName_Update(e, t) { var n = false; $("#tbName_Update").attr("readonly", true).attr("disabled", true).addClass("spinner"); $.ajax({ type: "POST", url: window.location.origin + "/api/Backend_Attribute.asmx/ValidationName_Update", data: '{"idAttribute":"' + e + '", "name":"' + t + '"}', async: false, dataType: "json", contentType: "application/json; charset=utf-8", success: function (e, t, r) { if (e.d) n = true }, error: function (e, t, r) { if (e.status === 0) { toastr.error("<b>Not connect. Please, Verify Network.</b>") } else if (e.status == 404) { toastr.error("<b>Requested page not found. [404]</b>") } else if (e.status == 500) { toastr.error("<b>Internal Server Error [500]</b>") } else if (r === "parsererror") { toastr.error("<b>Requested JSON parse failed</b>") } else if (r === "timeout") { toastr.error("<b>Time out error</b>") } else if (r === "abort") { toastr.error("<b>Ajax request aborted</b>") } else { toastr.error("<b>Uncaught Error.</b>") } n = true } }); $("#tbName_Update").attr("readonly", false).attr("disabled", false).removeClass("spinner"); return n } function Attribute_Update(e, t, n) { $.ajax({ type: "POST", url: window.location.origin + "/api/Backend_Attribute.asmx/Update", data: JSON.stringify({ idAttribute: e, name: t, isColor: n }), dataType: "json", contentType: "application/json; charset=utf-8", success: function (e, t, n) { if (e.d.Status == "Success") { toastr.success(e.d.Deskripsi); $("#formAttribute_Update").clearForm(); Attribute_GetAll_DataTable(); $(".default").trigger("click") } else if (e.d.Status == "Warning") { toastr.warning(e.d.Deskripsi) } else { toastr.error(e.d.Deskripsi) } } }) } $(document).ready(function () { Toastr.init(); $("#menu_catalog").addClass("active"); $("#submenu_attributes").addClass("active"); $("#arrow_catalog").addClass("open"); $(document).ajaxStop(function () { console.clear() }); Attribute_GetAll_DataTable() })