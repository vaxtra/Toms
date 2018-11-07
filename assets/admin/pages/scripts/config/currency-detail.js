var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_config").addClass("active");
    $("#arrow_config").addClass("open");
    $("#submenu_currency").addClass("active");
    if (queryString("id")) {
        $("#HiddenIDCurrency").val(queryString("id"));
        Preload();
    }
    else {
        window.location.href = "./Currency.aspx";
    }
    
    Validate_Update();
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            //$.each(result.d.data.EmailConfiguration, function (indexInArray, valueOfElement) {
            //    $("input[name=" + indexInArray + "]").val(valueOfElement);
            //});
            var curr = result.d.data.CurrencyDetail;
            $.each(curr, function (indexInArray, valueOfElement) {
                $("input[name=" + indexInArray + "]").val(valueOfElement);
            });
        }
    };
    REST.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'RequestData': ['CurrencyDetail'],
            'IDCurrency': +$("#HiddenIDCurrency").val()
        }
    });
}

function Validate_Update() {
    var e = $("#form_currency");
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
                maxlength: 50,
                email: true
            },
            ISOCode: {
                required: true,
                minlength: 5,
                maxlength: 50
            },
            ISOCodeNumber: {
                required: true
            },
            Sign: {
                required: true
            },
            ConversionRate: {
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
                if ($(this).attr("name") == "ConversionRate") {
                    sendData[$(this).attr("name")] = +$(this).val();
                }
                else {
                    sendData[$(this).attr("name")] = $(this).val();
                }
                //console.log($(this).attr("name"));
            });
            sendData["IDCurrency"] = +$("#HiddenIDCurrency").val();
            console.log(sendData);
            Update(sendData);
        }
    });
}

function Update(data) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "success",
                message: result.d.message,
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
        'c': 'becurr',
        'm': 'ucurr',
        'data': data
    });
}