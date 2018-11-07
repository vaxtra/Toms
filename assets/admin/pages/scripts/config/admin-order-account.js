var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_config").addClass("active");
    $("#arrow_config").addClass("open");
    $("#submenu_config_email").addClass("active");
    Preload();
    SubmitRegistration();
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            //$.each(result.d.data.EmailConfiguration, function (indexInArray, valueOfElement) {
            //    $("input[name=" + indexInArray + "]").val(valueOfElement);
            //});
            var AdminOrder = result.d.data.AdminOrderAccount;
            if (AdminOrder) {
                $.each(AdminOrder, function (indexInArray, valueOfElement) {
                    if (indexInArray == "Gender")
                        $("input[name=Gender][value=" + valueOfElement + "]").prop("checked", "checked");
                    else
                        $(".input[name=" + indexInArray + "]").val(valueOfElement);
                });
            }
        }
    };
    REST.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'RequestData': ['AdminOrderAccount']
        }
    });
}

function SubmitRegistration() {
    var e = $("#OrderForm");
    var t = $("#bootstrap_alert", e);
    jQuery.validator.addMethod("birthdate", function (e, t, n) {
        var date = moment($("#txtBirthdate").val(), "DD-MM-YYYY").format("YYYY-MM-DD");
        if (date == "Invalid date" || $("#txtBirthdate").val() == null || $("#txtBirthdate").val() == undefined)
            return false;
        else
            return true;
    }, jQuery.validator.format("Invalid birthdate"));
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            Phone: {
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
            t.show();
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "danger",
                message: "You have some form errors. Please check below. ",
                close: true,
                reset: true,
                focus: true,
                //closeInSeconds: 5,
                icon: "warning"
            });
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error");
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error");
        },
        success: function (e) {
            $(e).closest(".form-group").removeClass("has-error");
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();

            var sendData = {};
            $("input", e).each(function (index, element) {
                // element == this
                if (element.name != undefined && element.name != "") {
                    if (element.name == "Gender") {
                        sendData[element.name] = $('input:radio[name=' + element.name + ']:checked').val();
                        //console.log($('input:radio[name=' + element.name + ']:checked').val());
                    }
                    else if (element.name == "Newsletter") {
                        if ($("#cbNewsletter").prop("checked")) {
                            sendData[element.name] = true;
                        }
                        else {
                            sendData[element.name] = false;
                        }
                    }
                    else {
                        sendData[element.name] = $(element).val();
                        //console.log($(element).val());
                    }
                }
            });
            console.log(sendData);

            REST.onSuccess = function (result) {
                var data = result.d.data;
                t.show();
                if (result.d.success) {
                    Metronic.alert({
                        container: "#bootstrap_alert",
                        place: "append",
                        type: "success",
                        message: result.d.message,
                        close: true,
                        reset: true,
                        focus: true,
                        //closeInSeconds: 5,
                        icon: "check"
                    });
                }
                else {
                    Metronic.alert({
                        container: "#bootstrap_alert",
                        place: "append",
                        type: "danger",
                        message: result.d.message,
                        close: true,
                        reset: true,
                        focus: true,
                        //closeInSeconds: 5,
                        icon: "warning"
                    });
                }
            };
            REST.sendRequest({
                'c': 'beconf',
                'm': 'regadmord',
                'data': sendData
            });
        }
    });
}