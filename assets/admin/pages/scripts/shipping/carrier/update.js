var REST = new OF({
    'url': '/api/services.asmx/request',
});

$(document).ready(function () {
    $("#menu_shipping").addClass("active");
    $("#submenu_carries").addClass("active");
    

    if (!queryString("id"))
        window.location = "default.aspx";
    else {
        $("#HiddenIDCarrier").val(queryString("id"));
        Preload(+$("#HiddenIDCarrier").val());
    }

    SubmitUpdate();
});

function Preload(idCarrier) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            $.each(result.data, function (indexInArray, valueOfElement) {
                $("input[name=" + indexInArray + "]").val(valueOfElement);
            });

            $("#imgPreview").attr("src", "/assets/images/carrier/" + result.data.Image);
        }
    };
    REST.sendRequest({
        'c': 'becar',
        'm': 'upreload',
        'data': { 'IDCarrier': idCarrier }
    });
}

function SubmitUpdate() {
    var e = $("#form_carrier");
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
            },
            Information: {
                required: true,
            },
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
            console.log("sukses");
            t.hide();

            var sendData = {};
            sendData['IDCarrier'] = +$("#HiddenIDCarrier").val();
            sendData["Name"] = $("input[name=Name]").val();
            sendData["Information"] = $("input[name=Information]").val();
            sendData["BaseImage"] = "";
            sendData["FnImage"] = "";
            if ($(".fileinput-preview").find("img").length === 1 && $("#fuImage").val() !== "") {
                sendData["BaseImage"] = $(".fileinput-preview").find("img").eq(0).attr("src");
                sendData["FnImage"] = $("#fuImage").val();
            }

            REST.onSuccess = function (data) {
                var result = data.d;
                if (result.success) {
                    Metronic.alert({
                        container: "#bootstrap_alert",
                        place: "append",
                        type: "success",
                        message: result.message,
                        close: true,
                        reset: true,
                        focus: true,
                        closeInSeconds: 5,
                        icon: "checked"
                    });
                }
                else {
                    Metronic.alert({
                        container: "#bootstrap_alert",
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
            };

            REST.sendRequest({
                'c': 'becar',
                'm': 'ucar',
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