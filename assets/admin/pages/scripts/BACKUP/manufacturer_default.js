﻿function Manufacturer_GetAll_DataTable() { $("#dtManufacturer").each(function () { $("#dtManufacturer").dataTable().fnDestroy() }); var e = $("#dtManufacturer").dataTable({ oLanguage: { sProcessing: '<img src="' + Metronic.getGlobalImgPath() + 'loading-spinner-grey.gif"/><span>&nbsp;&nbsp;Loading...</span>', sLengthMenu: "_MENU_ records", oPaginate: { sPrevious: "Prev", sNext: "Next" }, sAjaxRequestGeneralError: "Could not complete request. Please check your internet connection", sEmptyTable: "No records to display", sZeroRecords: "No matching records found" }, aLengthMenu: [[5, 10, 20, 50, -1], [5, 10, 20, 50, "All"]], iDisplayLength: 5, bSortClasses: false, bStateSave: true, bPaginate: true, bAutoWidth: false, bProcessing: true, bServerSide: true, bDestroy: true, bRetrieve: true, sPaginationType: "bootstrap", sAjaxSource: window.location.origin + "/api/Backend_Manufacturer.asmx/GetAll_DataTable", fnInitComplete: function () { this.fnSetFilteringDelay(500) }, aoColumns: [{ mDataProp: "IDManufacturer", sWidth: "5%" }, { mDataProp: "Logo", sWidth: "10%" }, { mDataProp: "Name", sWidth: "15%" }, { mDataProp: "Email", sWidth: "15%" }, { mDataProp: "Phone", sWidth: "15%" }, { mDataProp: "Active", sWidth: "10%" }, { mDataProp: "IDManufacturer", sWidth: "15%" }], aoColumnDefs: [{ bSortable: false, aTargets: [1, 6] }, { sClass: "text-center", aTargets: [0, 1, 2, 3, 4, 5, 6] }], fnRowCallback: function (e, t, n, r) { var i = new Date; $("td:eq(1)", e).empty(); $("td:eq(1)", e).append('<center><img src="' + window.location.origin + "/assets/images/manufacturer/" + t.Logo + "?v=" + i.getTime() + '  class="img-responsive" style="width:80px;"/></center>'); $("td:eq(5)", e).empty(); var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data_id="' + t.IDManufacturer + '" data_nama="' + t.Name + '" data-original-title="Active"><i class="glyphicon glyphicon-ok" title="Active"></i></a>'; if (t.Active != "True") s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data_id="' + t.IDManufacturer + '" data_nama="' + t.Name + '" data-original-title="Deactive"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>'; $("td:eq(5)", e).append(s); $("td:eq(6)", e).empty(); var o = ""; o += '<a href="update.aspx?id=' + t.IDManufacturer + '" data_id="' + t.IDManufacturer + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a> '; o += '<a href="#" data_id="' + t.IDManufacturer + '" data_nama="' + t.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a> '; $("td:eq(6)", e).append(o) }, fnDrawCallback: function (e) { $("a", this.fnGetNodes()).tooltip({ delay: 0, track: true, fade: 250 }); $(".btn_changestatus").click(function (e) { e.preventDefault(); var t = $(this).attr("data_id"); var n = $(this).attr("data_nama"); var r = $(this).attr("data-original-title"); if (r == "Active") { bootbox.confirm("Are you sure want to deactive <b>" + n + "</b> ?", function (e) { if (e) { bootbox.confirm("All Products of <b>" + n + "</b> will be deactived, proceed?", function (e) { if (e) { Manufacturer_ChangeStatus(t) } }) } }) } else { bootbox.confirm("Are you sure want to active <b>" + n + "</b> ?", function (e) { if (e) { bootbox.confirm("All Products of <b>" + n + "</b> will be actived, proceed?", function (e) { if (e) { Manufacturer_ChangeStatus(t) } }) } }) } }); $(".btn_delete").click(function (e) { e.preventDefault(); var t = $(this).attr("data_id"); var n = $(this).attr("data_nama"); bootbox.confirm("Are you sure want to delete <b>" + n + "</b> ?", function (e) { if (e) { bootbox.confirm("All Products of <b>" + n + "</b> will be deleted, proceed?", function (e) { if (e) { Manufacturer_Delete(t) } }) } }) }) } }); jQuery("#dtManufacturer_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline"); jQuery("#dtManufacturer_wrapper .dataTables_length select").addClass("form-control input-xsmall"); jQuery("#dtManufacturer_wrapper .dataTables_length select").select2() } function Manufacturer_Delete(e) { $.ajax({ type: "POST", url: window.location.origin + "/api/Backend_Manufacturer.asmx/Delete", data: JSON.stringify({ idManufacturer: e }), async: true, dataType: "json", contentType: "application/json; charset=utf-8", success: function (e, t, n) { if (e.d.Status == "Success") { toastr.success(e.d.Deskripsi); Manufacturer_GetAll_DataTable() } else if (e.d.Status == "Warning") { toastr.warning(e.d.Deskripsi) } else { toastr.error(e.d.Deskripsi) } } }) } function Manufacturer_ChangeStatus(e) { $.ajax({ type: "POST", url: window.location.origin + "/api/Backend_Manufacturer.asmx/ChangeStatus", data: '{"idManufacturer":"' + e + '"}', dataType: "json", contentType: "application/json; charset=utf-8", success: function (e, t, n) { if (e.d.Status == "Success") { toastr.success(e.d.Deskripsi); Manufacturer_GetAll_DataTable() } else if (e.d.Status == "Warning") { toastr.warning(e.d.Deskripsi) } else { toastr.error(e.d.Deskripsi) } } }) } $(document).ready(function () { $(document).ajaxStop(function () { console.clear() }); Toastr.init(); $("#menu_catalog").addClass("active"); $("#submenu_manufacturer").addClass("active"); $("#arrow_catalog").addClass("open"); Manufacturer_GetAll_DataTable() })