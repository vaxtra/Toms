var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_config").addClass("active");
    $("#arrow_config").addClass("open");
    $("#submenu_config_email").addClass("active");
    Preload();
    Validate_Update();
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            //$.each(result.d.data.EmailConfiguration, function (indexInArray, valueOfElement) {
            //    $("input[name=" + indexInArray + "]").val(valueOfElement);
            //});
            var emailConf = result.d.data.EmailConfiguration;
            for (var i = 0; i < emailConf.length; i++) {
                $("input[name=" + emailConf[i].Name + "]").val(emailConf[i].Value);
            }
        }
    };
    REST.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'RequestData': ['EmailConfiguration']
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
            email_user: {
                required: true,
                maxlength: 50,
                email: true
            },
            email_password: {
                required: true,
                minlength: 5,
                maxlength: 50
            },
            email_smpt_client: {
                required: true
            },
            email_smtp_port: {
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
            console.log(sendData);
            Update(sendData);
        }
    });
}

function Update(data) {
    REST.onSuccess = function (result) { };
    REST.sendRequest({
        'c': 'beconf',
        'm': 'uconf',
        'data': data
    });
}