var REST = new OF({
    'url': '/api/services.asmx/request'
});

function Preload(idattr) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var result = data.d.data;
            if (result != null) {
                $("#breadcrumbName").text(result.Name);
                $(".linkBack").attr('href', 'default.aspx?id=' + result.IDAttribute);
                $(".cancel").attr('href', 'default.aspx?id=' + result.IDAttribute);
                $(".linkBack").html('<i class="fa fa-mail-reply"></i>&nbsp;' + result.Name);
                if (!result.IsColor) {
                    $(".color").addClass("hidden");
                }
                else {
                    $('.colorpicker-default').colorpicker();
                }
            }
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
        'c': 'beval',
        'm': 'preload',
        data: {
            'idattr': idattr
        }
    });
}

function Val_Insert_Validation() {
    var e = $("#form_value");
    var t = $(".alert-danger", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Name: {
                required: true,
                maxlength: 25
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
        submitHandler: function (e) {
            t.hide();
            Val_Insert();
        }
    });
}

function Val_Insert() {
    var sendData = {};
    sendData["idattr"] = parseInt(queryString("id"));
    $("#form_value").find(".input").each(function (index, element) {
        sendData[$(this).attr("name")] = $(this).val();
    });

    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
            $("input[name=Name]", "#form_value").val("");
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "warning"
            });
        }
    };

    REST.sendRequest({
        'c': 'beval',
        'm': 'ival',
        'data': sendData
    });
}

$(document).ready(function () {
    if (!queryString("id"))
        window.location = "./default.aspx";

    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_attributes").addClass("active");

    Preload(parseInt(queryString("id")));
    Val_Insert_Validation();
});