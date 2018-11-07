var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    Forget_validation();
    if (queryString('token')) {
        $("#token").val(queryString('token'));
    }
    else
        window.location = 'login-soft.aspx';
});

function Forget_validation() {
    var e = $("#FormReset");
    var t = $("#bootstrap_alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {
            Password: {
                required: 'Please enter your email address',
                email: 'invalid email format'
            }
        },
        rules: {
            Password: {
                required: true
            },
            Password2: {
                required: true,
                equalTo: "#Password"
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
            Reset();
        }
    });
}

function Reset() {
    REST.onSuccess = function (result) {
        $("input[name=Email]").val('');
        if (result.d.success) {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "success",
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 3,
                icon: "check"
            });

            setTimeout(function () { window.location = 'login-soft.aspx'; }, 3000);
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "error",
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
        'c': 'beauth',
        'm': 'reset',
        'data': {
            'Token': $("#token").val(),
            'Password': $("#Password").val()
        }
    });
}