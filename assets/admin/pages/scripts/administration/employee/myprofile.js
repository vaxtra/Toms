var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_administration").addClass("active");
    $("#arrow_administration").addClass("open");
    $("#submenu_employee").addClass("active");
    Preload();

    Validate_Update();
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var emp = result.d.data;
            $('.role option[value=' + emp.IDRole + ']').attr('selected', 'selected');
            $("input[name=Name]").val(emp.Name);
            $("input[name=Email]").val(emp.Email);
        }
    };
    REST.sendRequest({
        'c': 'beemp',
        'm': 'detprofile',
        'data': {}
    });
}

function LoadRole(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<option value="' + data[i].IDRole + '">' + data[i].Name + '</option>';
    }

    $(".role").html(item);
}

function Validate_Update() {
    var e = $("#form_employee");
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
                maxlength: 50
            },
            Email: {
                required: true,
                email: true,
                maxlength: 50
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

            Update(sendData);
        }
    });
}

function Update(data) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            window.location = 'default.aspx';
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
        'c': 'beemp',
        'm': 'uprofile',
        'data': data
    });
}