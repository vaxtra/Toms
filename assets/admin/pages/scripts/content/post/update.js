var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    //$("#DefaultCategory").select2();
    if (!queryString("id") || isNaN(queryString("id")))
        window.location = "./default.aspx";
    else
        $("#HiddenIDPost").val(queryString("id"));

    $("#btnSubmitInformation").click(function (e) {
        e.preventDefault();
        $("#formPostData_Update").submit();
    });

    $("#submitCategory").click(function (e) {
        e.preventDefault();
        SubmitPageCategories();
    });

    InfoMedia_Validation($("#info_insert"));

    $("#submitMediaInfo").click(function () {
        var form = $("#info_insert");
        if (form.valid())
            InfoMedia_Update($("#HiddenIDPostMedia").val());
    })

    $("#menu_content").addClass("active");
    $("#arrow_content").addClass("open");
    $("#submenu_post").addClass("active");
    $("#HiddenIDAttribute").val(queryString("id"));
    Preload(+$("#HiddenIDPost").val());

    UpdateInformationValidation();

    //END COMBINATION
});

function Preload(id) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var page = data.d.data.Post;

            $("#HiddenIDPage").val(page.IDPage);

            //LOAD TAB INFORMATION
            $.each(page, function (indexInArray, valueOfElement) {
                if (indexInArray == "Post_ShortContent") {
                    $("#" + indexInArray).code(valueOfElement);
                }
                else if (indexInArray == "Post_Content") {
                    $("#" + indexInArray).code(valueOfElement);
                }
                else {
                    $("#" + indexInArray).val(valueOfElement);
                    $("." + indexInArray).val(valueOfElement);
                }
            });

            //END LOAD TAB INFORMATION

            //LOAD PAGE CATEGORIES
            if ($("#HiddenIDPage").val())
                PreloadPageCategories(+$("#HiddenIDPage").val(), id);
            //END LOAD PAGE CATEGORIES

            //LOAD TAB IMAGES
            if (data.d.data.PostMedia)
                ReloadImage(data.d.data.PostMedia);
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
        'c': 'bepost',
        'm': 'detpost',
        'data': {
            'IDPost': id
        }
    });
}

function UpdateInformationValidation() {
    var e = $("#formPostData_Update");
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
            postTitle: {
                required: true,
                minlength: 3
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
    sendData["IDPost"] = +$("#HiddenIDPost").val();
    $("#formPostData_Update").find(".input").each(function (index, element) {
        if ($(this).attr("name") == "postTitle") {
            sendData[$(this).attr("name")] = $(this).val();
        }
        else if ($(this).attr("name") == "postShortContent") {
            sendData[$(this).attr("name")] = $(this).code();
        }
        else if ($(this).attr("name") == "postContent") {
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
        'c': 'bepost',
        'm': 'upost',
        'data': sendData
    });
}

function PreloadPageCategories(idPage, idPost)
{
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var pageCategory = data.d.data.PageCategory;
            $("#treePageCategory").jstree({
                'plugins': ['themes', 'html_data', 'checkbox', 'ui'],
                "checkbox": {
                    "three_state": false,
                },
                'core': {
                    "themes": {
                        "responsive": true
                    },
                    'data': pageCategory
                },
                "types": {
                    "default": {
                        "icon": "fa fa-folder icon-state-warning icon-lg"
                    },
                    "file": {
                        "icon": "fa fa-file icon-state-warning icon-lg"
                    }
                }
            });

            //LOAD TAB CATEGORIES
            var categories = data.d.data.SelectedPageCategory;
            ReloadCategories(categories);
            //END LOAD TAB CATEGORIES
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
        'c': 'bepost',
        'm': 'detpagcatpost',
        'data': {
            'IDPage': idPage,
            'IDPost': idPost
        }
    });
}

function SubmitPageCategories() {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            ReloadCategories(data.d.data.SelectedCategories);
            Metronic.alert({
                container: "#bootstrap_alerts_cat",
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
                container: "#bootstrap_alerts_cat",
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

    //GET DATA
    var sendData = {};
    sendData["IDPageCategory"] = $("#treePageCategory").jstree(true).get_selected();;
    sendData["IDPost"] = +$("#HiddenIDPost").val();
    sendData["IDPage"] = +$("#HiddenIDPage").val();
    //END GET DATA

    REST.sendRequest({
        'c': 'bepost',
        'm': 'upostpagcat',
        'data': sendData
    });
}

function ReloadCategories(SelectedCategories) {
    //LOAD TAB CATEGORIES 
    var selectedCat = {};
    var cat = [];
    for (var i = 0; i < SelectedCategories.length; i++) {
        if (SelectedCategories[i].IDProduct_Category) {
            selectedCat = {
                id: SelectedCategories[i].IDProduct_Category,
                text: SelectedCategories[i].Name
            };
        }

        cat.push({
            id: SelectedCategories[i].IDProduct_Category,
            text: SelectedCategories[i].Name
        });
    }

    if (cat.length === 0) {
        Metronic.alert({
            container: "#bootstrap_alerts_cat",
            place: "append",
            type: "danger",
            message: "Please, choose Category before you post this information.",
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: "0",
            icon: "warning"
        });
    }
    //END LOAD TAB CATEGORIES
}

function ReloadImage(data) {
    var item = "";
    var itemCmb = ""; //untuk tab combination
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            item += "<tr>" +
            "<td><center><img class='img-responsive' style='max-height:150px;width:auto;' src='/assets/images/post/" + data[i].Preview + "?v=" + new Date().getTime() + "' /></center></td>" +
            "<td>" + data[i].Preview + "</td>";
            item += '<td><button data-name="' + data[i].Preview + '" data-id="' + data[i].IDPostMedia + '" title="Insert title / description" class="btn btn-info btn-sm green"><i class="fa fa-thumb-tack"></i> Info</button><button data-name="' + data[i].Preview + '" data-id="' + data[i].IDPostMedia + '" class="btn btn-delete btn-sm red" title="delete"><i class="fa fa-trash-o"></i> Delete</button></td>' +
            "</tr>";

            itemCmb += '<label class="checkbox-inline statusCover">';
            itemCmb += '<input type="checkbox" name="ImageCombination" value="' + data[i].IDPostMedia + '" />';
            itemCmb += '<img style="max-width: 75px; margin: 3px;" src="/assets/images/product/' + data[i].Preview + '" alt="image" class="img-responsive" />';
            itemCmb += '</label>';
        }
        $("#dtProductImage tbody").html(item);
        $("#listImages").html(itemCmb);
        Metronic.init();

        $(".btn-delete").click(function (e) {
            e.preventDefault();
            var name = $(this).data("name");
            var id = $(this).data("id");
            bootbox.confirm("Are you sure to delete <b>" + name + "</b> ?", function (result) {
                if (result) {
                    DeletePhoto(id);
                }
            });
        });

        $(".btn-info").click(function (e) {
            e.preventDefault();
            var idPostMedia = $(this).data("id");
            $("#HiddenIDPostMedia").val(idPostMedia);
            $(".iuCat").fadeOut(200);
            $(".iuCat").fadeIn(200);
            LoadInfoMedia(idPostMedia);
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
        'c': 'bepost',
        'm': 'dpostphoto',
        'data': { 'IDPostMedia': id }
    });
}

function InfoMedia_Update(idPostMedia) {
    var sendData = {};
    sendData["IDPostMedia"] = +idPostMedia;
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
        'c': 'bepost',
        'm': 'upostphoinfo',
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
            InfoMedia_Update($("#HiddenIDPostMedia").val());
        }
    });
}

function LoadInfoMedia(idPostMedia) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            $("#tbTitle").val(data.d.data.PostMedia[0].Title);
            $("#tbMediaDescription").code(data.d.data.PostMedia[0].Description);
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
        'c': 'bepost',
        'm': 'detpostphoto',
        'data': { 'IDPostMedia': idPostMedia }
    });
}


