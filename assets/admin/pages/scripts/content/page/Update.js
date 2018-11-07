var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    //$("#DefaultCategory").select2();
    if (!queryString("id") || isNaN(queryString("id")))
        window.location = "./default.aspx";
    else
        $("#HiddenIDPage").val(queryString("id"));

    InitDTPageCategory(+$("#HiddenIDPage").val());

    $("#btnSubmitInformation").click(function (e) {
        e.preventDefault();
        $("#formProductInformation_Update").submit();
    });

    Cat_Insert_Validation($("#cat_insert"));

    $("#submitCategory").click(function (e) {
        e.preventDefault();
        var form = $("#cat_insert");
        if (form.valid())
            Cat_Insert();
    });

    $(".insertCat").click(function () {
        $(".uCat").fadeOut(200);
        $(".iuCat").fadeIn(200);
        Metronic.scrollTo($("#cat_insert"), 200);
    })

    InfoMedia_Validation($("#info_insert"));

    $("#submitMediaInfo").click(function () {
        var form = $("#info_insert");
        if (form.valid())
            InfoMedia_Update($("#HiddenIDPageMedia").val());
    })

    

    $("#menu_content").addClass("active");
    $("#arrow_content").addClass("open");
    $("#submenu_page").addClass("active");
    $("#HiddenIDAttribute").val(queryString("id"));
    Preload(+$("#HiddenIDPage").val());

    UpdateInformationValidation();

    Validate_Update();

    //END COMBINATION
});

function UpdateInformationValidation() {
    var e = $("#formPageData_Update");
    var t = $(".alert", e);

    //$.validator.addMethod("Select2Required", function (value, element, params) {
    //    console.log(params[0]);
    //    return ($(params[0]).select2("val") === "");
    //}, "Choose option");

    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        rules: {
            Page_Title: {
                required: true,
                minlength: 3,
                maxlength: 25
            },
        },
        messages: {},
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
            t.fadeIn();
            Metronic.scrollTo(t, -200);
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        submitHandler: function (e) {
            t.hide();
            UpdateInformation();
        }
    });
}

function UpdateInformation() {
    var sendData = {};
    sendData["IDPage"] = +$("#HiddenIDPage").val();
    $("#formPageData_Update").find(".input").each(function (index, element) {
        if ($(this).attr("name") == "pageTitle") {
            sendData[$(this).attr("name")] = $(this).val();
        }
        else if ($(this).attr("name") == "pageShortContent") {
            sendData[$(this).attr("name")] = $(this).code();
        }
        else if ($(this).attr("name") == "pageContent") {
            sendData[$(this).attr("name")] = $(this).code();
        }
        else {
            sendData[$(this).attr("name")] = $(this).val();
        }
    });

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
        'c': 'bepag',
        'm': 'upaginfo',
        'data': sendData
    });
}

function Preload(id) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var page = data.d.data.Page;

            //LOAD TAB INFORMATION
            $.each(page, function (indexInArray, valueOfElement) {
                if (indexInArray == "Page_ShortContent") {
                    $("#" + indexInArray).code(valueOfElement);
                }
                else if (indexInArray == "Page_Content") {
                    $("#" + indexInArray).code(valueOfElement);
                }
                else {
                    $("#" + indexInArray).val(valueOfElement);
                    $("." + indexInArray).val(valueOfElement);
                }
            });

            //END LOAD TAB INFORMATION

            //LOAD TAB IMAGES
            if (data.d.data.PageMedia)
                ReloadImage(data.d.data.PageMedia);
            //END LOAD TAB IMAGES

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
        'c': 'bepag',
        'm': 'detpag',
        'data': { 'IDPage': id }
    });
}

function ReloadImage(data) {
    var item = "";
    var itemCmb = ""; //untuk tab combination
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            item += "<tr>" +
            "<td><center><img class='img-responsive' style='max-height:150px;width:auto;' src='/assets/images/page/" + data[i].Preview + "?v=" + new Date().getTime() + "' /></center></td>" +
            "<td>" + data[i].Preview + "</td>";
            if (data[i].Cover)
                item += '<td><a data-id="' + data[i].IDPageMedia + '" class="btn btn-sm btn_changestatus green tooltips" href="javascript:;"><i title="Active" class="glyphicon glyphicon-ok"></i></a></td>';
            else
                item += '<td><a data-id="' + data[i].IDPageMedia + '" class="btn btn-sm btn_changestatus red tooltips" href="javascript:;"><i title="Active" class="glyphicon glyphicon-remove"></i></a></td>';
            item += '<td><button data-name="' + data[i].Preview + '" data-id="' + data[i].IDPageMedia + '" title="Insert title / description" class="btn btn-info btn-sm green"><i class="fa fa-thumb-tack"></i> Info</button><button data-name="' + data[i].Preview + '" data-id="' + data[i].IDPageMedia + '" title="set as cover" class="btn btn-cover btn-sm yellow"><i class="fa fa-thumb-tack"></i> Set as Cover</button> <button data-name="' + data[i].Preview + '" data-id="' + data[i].IDPageMedia + '" class="btn btn-delete btn-sm red" title="delete"><i class="fa fa-trash-o"></i> Delete</button></td>' +
            "</tr>";

            itemCmb += '<label class="checkbox-inline statusCover">';
            itemCmb += '<input type="checkbox" name="ImageCombination" value="' + data[i].IDPageMedia + '" />';
            itemCmb += '<img style="max-width: 75px; margin: 3px;" src="/assets/images/product/' + data[i].Preview + '" alt="image" class="img-responsive" />';
            itemCmb += '</label>';
        }
        $("#dtProductImage tbody").html(item);
        $("#listImages").html(itemCmb);
        Metronic.init();

        $(".btn-delete").click(function (e) {
            e.preventDefault();
            var name = $(this).data("Preview");
            var id = $(this).data("id");
            bootbox.confirm("Are you sure to delete <b>" + name + "</b> ?", function (result) {
                if (result) {
                    DeletePhoto(id);
                }
            });
        });

        $(".btn-cover").click(function (e) {
            e.preventDefault();
            var name = $(this).data("name");
            var id = $(this).data("id");
            bootbox.confirm("Are you sure to set <b>" + name + "</b> as cover ?", function (result) {
                if (result) {
                    SetCoverPhoto(id);
                }
            });
        });

        $(".btn-info").click(function (e) {
            e.preventDefault();
            var idPageMedia = $(this).data("id");
            $("#HiddenIDPageMedia").val(idPageMedia);
            $(".iuCat").fadeOut(200);
            $(".iuCat").fadeIn(200);
            LoadInfoMedia(idPageMedia);
            Metronic.scrollTo($("#info_insert"), 200);
        });
    }
    else {
        $("#dtProductImage tbody").html("<tr><td class='text-center' colspan='4'>No records to display</tr>");
    }

}

function DeletePhoto(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            ReloadImage(result.data.Photos);
            Metronic.alert({
                container: "#bootstrap_alerts_photo",
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
                container: "#bootstrap_alerts_photo",
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
        'c': 'bepag',
        'm': 'dpagphoto',
        'data': { 'IDPageMedia': id }
    });
}

function SetCoverPhoto(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            ReloadImage(result.data.Photos);
            Metronic.alert({
                container: "#bootstrap_alerts_photo",
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
                container: "#bootstrap_alerts_photo",
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
        'c': 'bepag',
        'm': 'setcover',
        'data': { 'IDPageMedia': id }
    });
}

function Cat_Insert() {
    var sendData = {};
    sendData["idPage"] = +$("#HiddenIDPage").val();
    sendData["name"] = $("#tbName").val();
    sendData["description"] = $("#tbDescription").code();

    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            $("input", ".form-group").val("");
            //$(".alert-success").show();
            //Metronic.scrollTo($("#alert-succes"));

            Metronic.alert({
                container: "#bootstrap_alerts_catpage",
                place: "append",
                type: "success",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "checked"
            });
            $("#iuCat").fadeOut(200);
            InitDTPageCategory(+$("#HiddenIDPage").val());
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_catpage",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "warning"
            });
        }
    }
    REST.sendRequest({
        'c': 'bepagcat',
        'm': 'ipagcat',
        'data': sendData
    });
}

function Cat_Insert_Validation() {
    var e = $("#cat_insert");
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Name: {
                required: true,
                minlength: 3,
                maxlength: 25,
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
            Metronic.scrollTo(t, -200)
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();
            Cat_Insert();
        }
    });
}

function LoadInfoMedia(idPageMedia)
{
    REST.onSuccess = function (data) {
        if (data.d.success) {
            $("#tbTitle").val(data.d.data.PageMedia[0].Title);
            $("#tbMediaDescription").code(data.d.data.PageMedia[0].Description);
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
        'c': 'bepag',
        'm': 'detpagphoto',
        'data': { 'IDPageMedia': idPageMedia }
    });
}

function InfoMedia_Update(idPageMedia) {
    var sendData = {};
    sendData["IDPageMedia"] = +idPageMedia;
    sendData["Title"] = $("#tbTitle").val();
    sendData["Description"] = $("#tbMediaDescription").code();

    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            $("input", ".form-group").val("");
            //$(".alert-success").show();
            //Metronic.scrollTo($("#alert-succes"));

            Metronic.alert({
                container: "#bootstrap_alerts_photo",
                place: "append",
                type: "success",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "checked"
            });
            $(".iuCat").fadeOut(200);
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_photo",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "warning"
            });
        }
    }
    REST.sendRequest({
        'c': 'bepag',
        'm': 'upagphoinfo',
        'data': sendData
    });
}

function InfoMedia_Validation() {
    var e = $("#info_insert");
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Name: {
                required: true,
                minlength: 3,
                maxlength: 25,
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
            Metronic.scrollTo(t, -200)
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();
            InfoMedia_Update($("#HiddenIDPageMedia").val());
        }
    });
}

function InitDTPageCategory(idPage) {
    var element = "#dtPageCategory";
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
        fnServerData: function (sSource, aoData, fnCallback) {
            aoData.push({
                name: "idPage",
                value: idPage
            });
            $.getJSON(sSource, aoData, function (json) {
                fnCallback(json);
            });
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
        sAjaxSource: "/api/services.asmx/admin_dtPageCategory",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [{ mDataProp: "IDPageCategory", sWidth: "5%" },
                    { mDataProp: "Name", sWidth: "15%" },
                    { mDataProp: "DateInsert", sWidth: "20%" },
                    { mDataProp: "DateLastUpdate", sWidth: "20%" },
                    { mDataProp: "IDPageCategory", sWidth: "5%" },

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
            //o += '<a href="#" data-id="' + aaData.IDCustomer + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            o += '<a href="update.aspx?id=' + aaData.IDPageCategory + '" data-id="' + aaData.IDPageCategory + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            o += '<a href="#" data-id="' + aaData.IDPageCategory + '" data-name="' + aaData.Name + '" class="btn_delete btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a>';
            $("td:eq(4)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {

            $(".btn_edit").on("click", function (e) {
                e.preventDefault();
                $(".iuCat").fadeOut(200);
                $(".uCat").fadeIn(200);
                Metronic.scrollTo($("#cat_update"), 200);
                PreloadPageDetail(+$(this).data("id"));
            });

            $(".btn_delete").on("click", function (e) {
                e.preventDefault();
                var button = $(this);
                bootbox.confirm("Are you sure to delete <b>" + $(this).data('name') + "</b> Page ?", function (result) {
                    if (result) {
                        Delete(+button.data("id"));
                    }
                });
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

function PreloadPageDetail(idPageCategory) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            $("#HiddenIDPageCategory").val(idPageCategory);
            $("#tbUpdateName").val(result.data.PageCategory.Name);
            $("#tbUpdateDescription").code(result.data.PageCategory.Description);
        }
        else {
            toastr.error(result.message);
        }
    };

    REST.sendRequest({
        'c': 'bepagcat',
        'm': 'upreload',
        'data': {
            'idPageCategory': idPageCategory
        }
    });
}

function Validate_Update() {
    var e = $("#cat_update");
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Name: {
                required: true,
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
            t.fadeIn();
            Metronic.scrollTo(t, -200);
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        submitHandler: function (e) {
            t.hide();
            var sendData = {};
            sendData["idPageCategory"] = +$("#HiddenIDPageCategory").val();
            sendData["name"] = $("#tbUpdateName").val();
            sendData["description"] = $("#tbUpdateDescription").code();
            Update(sendData);
        }
    });
}

function Update(data) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            Metronic.alert({
                container: "#bootstrap_alerts_catpage",
                place: "append",
                type: "success",
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "checked"
            });
            $("#cat_update").fadeOut(200);
            InitDTPageCategory(+$("#HiddenIDPage").val());
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_catpage",
                place: "append",
                type: "danger",
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "warning"
            });
        }
    };
    REST.sendRequest({
        'c': 'bepagcat',
        'm': 'upagcat',
        'data': data
    });
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
            InitDTPageCategory(+$("#HiddenIDPage").val());
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
        'c': 'bepagcat',
        'm': 'dpagcat',
        'data': { 'idPageCategory': id }
    });
}


