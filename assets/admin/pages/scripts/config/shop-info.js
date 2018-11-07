var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_config").addClass("active");
    $("#arrow_config").addClass("open");
    $("#submenu_config_shopinfo").addClass("active");
    Preload();
    Validate_Update();
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            //$.each(result.d.data.EmailConfiguration, function (indexInArray, valueOfElement) {
            //    $("input[name=" + indexInArray + "]").val(valueOfElement);
            //});
            var ShopInfo = result.d.data.ShopInfo;
            for (var i = 0; i < ShopInfo.length; i++) {
                $("input[name=" + ShopInfo[i].Name + "]").val(ShopInfo[i].Value);
            }

            $("#imgLogoEmail").attr("src", "/assets/images/email_logo/" + ShopInfo[4].Value);
        }
    };
    REST.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'RequestData': ['ShopInfo']
        }
    });
}

function Validate_Update() {
    var e = $("#form_config");
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            shop_email: {
                required: true,
                maxlength: 50,
                email: true
            },
            shop_city: {
                required: true
            },
            shop_address: {
                required: true
            },
            shop_phone: {
                required: true
            },
            shop_email_logo: {
                required: true
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
            $(".input", e).each(function (index, element) {
                sendData[$(this).attr("name")] = $(this).val();
                //console.log($(this).attr("name"));
            });
            sendData["BaseImage"] = "";
            sendData["shop_email_logo"] = "";
            if ($(".fileinput-preview").find("img").length === 1 && $("#fuImage").val() !== "") {
                sendData["shop_email_logo"] = $("#fuImage").val();
                sendData["BaseImage"] = $(".fileinput-preview").find("img").eq(0).attr("src");
            }
            console.log(sendData);
            //Update(sendData);

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
                'c': 'beconf',
                'm': 'ushopinfo',
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

function Update(data) {
    REST.onSuccess = function (result) { };
    REST.sendRequest({
        'c': 'beconf',
        'm': 'ushopinfo',
        'data': data
    });
}