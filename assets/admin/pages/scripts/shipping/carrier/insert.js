var REST = new OF({
    'url': '/api/services.asmx/Request',
});

$(document).ready(function () {
    $("#menu_shipping").addClass("active");
    $("#submenu_carries").addClass("active");

    InsertCarrier();
});

function InsertCarrier() {
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
            //Carrier Insert
            var sendData = {};
            sendData["Name"] = $("input[name=Name]").val();
            sendData["Information"] = $("input[name=Information]").val();
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
                    $("input", ".form-group").val("");
                    //$(".alert-success").show();
                    //Metronic.scrollTo($("#alert-succes"));

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
            }
            
            REST.sendRequest({
                'c': 'becar',
                'm': 'icar',
                'data': sendData
            });
            //Carrier Insert END
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