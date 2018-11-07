var REST = new OF({
    'url': '/api/services.asmx/request'
});

function Preload(id) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var result = data.d.data;
            $("#breadcrumbName").empty().append(result.Name);
            $(".caption").empty().append('<i class="fa fa-edit"></i>' + result.Name);
            $(".backLinkParent").attr("href", "../default.aspx?id=" + result.IDAttribute).text(result.AttributeName);
            $(".backLink").attr("href", "default.aspx?id=" + result.IDAttribute);
            $(".backLink").empty().append('<i class="fa fa-mail-reply"></i>&nbsp;' + result.AttributeName);
            $(".cancel").attr("href", "default.aspx?id=" + result.IDAttribute);
            if (!result.IsColor) {
                $(".color").addClass("hidden");
                $(".color").val('');
            }
            else {
                $('.colorpicker-default').colorpicker();
                $('.colorpicker-default').colorpicker('setValue', result.RGBColor);
            }

            $.each(result, function (indexInArray, valueOfElement) {
                $("#form_value input[name='" + indexInArray + "']").val(valueOfElement);
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
        'c': 'beval',
        'm': 'upreload',
        'data': { 'id': id }
    });
}

function Val_Update_Validation() {
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
                e.insertAfter(t.parent(".input-group"));
            } else if (t.attr("data-error-container")) {
                e.appendTo(t.attr("data-error-container"));
            } else if (t.parents(".radio-list").size() > 0) {
                e.appendTo(t.parents(".radio-list").attr("data-error-container"));
            } else if (t.parents(".radio-inline").size() > 0) {
                e.appendTo(t.parents(".radio-inline").attr("data-error-container"));
            } else if (t.parents(".checkbox-list").size() > 0) {
                e.appendTo(t.parents(".checkbox-list").attr("data-error-container"));
            } else if (t.parents(".checkbox-inline").size() > 0) {
                e.appendTo(t.parents(".checkbox-inline").attr("data-error-container"));
            } else if (t.parents(".input-group").size() > 0) {
                e.insertAfter(t.parents(".input-group"));
            } else {
                e.insertAfter(t);
            }
        },
        invalidHandler: function (e, n) {
            t.show();
            Metronic.scrollTo(t, -200);
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
        submitHandler: function (e) {
            t.hide();
            Val_Update();
        }
    });
}

function Val_Update() {
    var sendData = {};
    sendData["id"] = +$("#HiddenIDValue").val();
    $("#form_value").find(".input").each(function (index, element) {
        sendData[$(this).attr("name")] = $(this).val();
    });

    REST.onSuccess = function (data) {
        if (data.d.success) {
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
        'c': 'beval',
        'm': 'uval',
        'data': sendData
    });
}

$(document).ready(function () {
    if (!queryString("id"))
        window.location = "./default.aspx";

    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_attributes").addClass("active");

    $("#HiddenIDValue").val(queryString("id"));
    Preload(+$("#HiddenIDValue").val());
    Val_Update_Validation();
});