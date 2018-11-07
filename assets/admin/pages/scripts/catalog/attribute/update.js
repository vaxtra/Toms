var REST = new OF({
    'url': '/api/services.asmx/request'
});

function Preload(id) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var result = data.d.data;
            $("#nameAttribute").empty().append(result.Name);
            $(".caption").empty().append('<i class="fa fa-edit"></i>' + result.Name);
            $("input[name='Name']").val(result.Name);
            $("input[name='IsColor']").bootstrapSwitch("state", result.IsColor);
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
        'c': 'beattr',
        'm': 'upreload',
        'data': { 'id': id }
    });
}

function Att_Update_Validation() {
    var e = $("#form_attribute");
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
                minlength: 3,
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
            t.fadeIn();
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
            Att_Update();
        }
    });
}

function Att_Update() {
    var sendData = {};
    $("#form_attribute").find(".input").each(function (index, element) {
        if ($(this).attr("name") == "IsColor") {
            sendData[$(this).attr("name")] = $("input[name='IsColor']").prop('checked')
        }
        else
            sendData[$(this).attr("name")] = $(this).val();
    });
    sendData["id"] = +$("#HiddenIDAttribute").val();

    REST.onSuccess = function (data) {
        if (data.d.success) {
            Preload(+$("#HiddenIDAttribute").val());
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
                icon: "error"
            });
        }
    };

    REST.sendRequest({
        'c': 'beattr',
        'm': 'uattr',
        'data': sendData
    });
}

$(document).ready(function () {
    if (!queryString("id"))
        window.location = "./default.aspx";

    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_attributes").addClass("active");
    $("#HiddenIDAttribute").val(queryString("id"));
    Preload(+$("#HiddenIDAttribute").val());

    Att_Update_Validation();
});