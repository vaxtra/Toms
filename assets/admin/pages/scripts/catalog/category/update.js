var REST = new OF({
    'url': '/api/services.asmx/Request',
});

function Preload() {
    var _idpromo;

    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            $(".backLink").attr("href", (result.data.IDCategoryParent === null) ? "default.aspx" : "default.aspx?id=" + result.data.IDCategoryParent);
            $("#imgPreview").attr("src", "/assets/images/category/" + result.data.Image);
            $("#tbName").val(result.data.Name);
            $("#tbDescription").code(result.data.Description);
            $('input[name="cbActive"]').bootstrapSwitch('state', result.data.Active);
            $("#HiddenIDCategoryParent").val(result.data.IDCategoryParent);
        }
        else {
            toastr.error(result.message);
        }
    };

    REST.sendRequest({
        'c': 'becat',
        'm': 'upreload',
        'data': {
            'id': +$("#HiddenIDCategory").val()
        }
    });
}

function Update() {
    var e = $("#cat_update");
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            fuImage: {
                extension: "png|jpe?g|gif",
                size: true
            },
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
            //s.hide();

            var sendData = {};
            sendData['id'] = +$("#HiddenIDCategory").val();
            sendData["idparent"] = +$("#hiddenIdCategoryParent").val();
            sendData["name"] = $("#tbName").val();
            sendData["description"] = $("#tbDescription").code();
            sendData["active"] = $('input[name="cbActive"]').bootstrapSwitch("state");
            if ($(".fileinput-preview").find("img").length == 1) {
                sendData["baseImage"] = $(".fileinput-preview").find("img").eq(0).attr("src");
                sendData["fnImage"] = $("#fuImage").val();
            }
            else {
                sendData["baseImage"] = "";
                sendData["fnImage"] = "";
            }

            REST.onSuccess = function (data) {
                var result = data.d;
                if (result.success) {
                    $(".alert-success").show();
                    Metronic.scrollTo($(".alert-succes"));
                    Preload();
                }
                else
                    bootbox.alert(result.message);
            };

            REST.sendRequest({
                'c': 'becat',
                'm': 'ucat',
                'data': sendData
            });
        }
    });

    jQuery.validator.addMethod("size", function (e, t, n) {
        if (jQuery(t).attr("type") === "file") {
            if (t.files[0]) {
                file = t.files[0];
                size = file.size / 1024 / 1024;
                if (size > 2) {
                    return false
                }
            }
        }
        return true
    }, jQuery.validator.format("Maximum file size is 2MB."));

    jQuery.extend(jQuery.validator.messages, {
        extension: jQuery.validator.format("Allowed Extension are : .png .jpeg .jpg .gif.")
    });
}

$(document).ready(function () {
    $("input[name='IsColor']").bootstrapSwitch();
    $("#HiddenIDCategory").val(queryString("id"));
    Summernote.init();
    Preload();

    Update();
});