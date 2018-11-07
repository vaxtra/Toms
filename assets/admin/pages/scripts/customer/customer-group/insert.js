var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_customers").addClass("active");
    $("#submenu_customer-group").addClass("active");

    $(".money").inputmask("decimal", { allowPlus: false, allowMinus: false, rightAlignNumerics: false, radixPoint: ",", autoGroup: true, groupSeparator: ".", groupSize: 3 });

    $("input[type=checkbox]").bootstrapSwitch('state', true);
    $("input[name=IsPoint]").bootstrapSwitch('state', false);

    $('[name=IsPoint]').on('switchChange.bootstrapSwitch', function (event, state) {
        if (state) {
            $('.member_point').removeClass("hidden");

        } else {
            $('.member_point').addClass("hidden");
        }
    });

    GroupInsertValidation();
});

//function Preload() {
//    REST.onSuccess = function (result) {
//        if (result.d.success) {
//            var dataCategory = result.d.data.Category;

//            var _data = [];
//            for (var i = 0; i < dataCategory.length; i++) {
//                _data.push({
//                    id: dataCategory[i].IDCategory,
//                    text: dataCategory[i].Name
//                });
//            }

//            $("#ddlCategory").select2({
//                data: _data,
//                placeholder: 'choose category',
//                allowClear: true
//            }).select2("data", null);
//        }
//    };
//    REST.sendRequest({
//        'c': 'beprom',
//        'm': 'preload',
//        'data': {}
//    });
//}

function GroupInsertValidation() {
    var e = $("#form_customerGroup");
    var t = $(".alert", e);

    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        rules: {
            Name: {
                required: true,
                minlength: 3,
                maxlength: 25
            },
            MinimumTransaction: {
                required: true
            },
            AquiredPoint: {
                required: true
            },
            DiscountPoint: {
                required: true
            }
        },
        messages: {},
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
            SubmitData();
        }
    });
}

function SubmitData() {
    var data = {};
    var form = $("#form_customerGroup");
    //moment($("[name=StartDate]").val(),"DD MMM YYYY HH:mm").format("YYYY MM DD HH:mm")
    $(".form-control", form).each(function (index, element) {
        if ($(element).attr("name") == "MinimumTransaction" || $(element).attr("name") == "AquiredPoint" || $(element).attr("name") == "DiscountPoint" || $(element).attr("name") == "MaximumTransaction") {
            data[$(element).attr("name")] = Number(ReplacePoint($(element).val()));
        }
        else if ($(element).attr("name") == "Active" || $(element).attr("name") == "IsPoint")
            data[$(element).attr("name")] = $(element).bootstrapSwitch("state");
        else {
            data[$(element).attr("name")] = $(element).val();
        }
    });

    $.ajax({
        url: "/modules/MemberPoint/MemberPoint.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "MemberPoint",
            t: "be",
            m: "icustgr",
            data: data
        }),
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true
            });
        },
        success: function (result) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: result.data.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
        },
        error: function (result) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.data.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "warning"
            });
        },
        complete: function () {
            Metronic.unblockUI();
        },
    });

    //REST.onSuccess = function (data) {
    //    var result = data.d;
    //    if (result.success) {
    //        Metronic.alert({
    //            container: "#bootstrap_alerts",
    //            place: "append",
    //            type: "success",
    //            message: result.message,
    //            close: true,
    //            reset: true,
    //            focus: true,
    //            closeInSeconds: "0",
    //            icon: "check"
    //        });
    //        $("#form_promo").clearForm();
    //    }
    //    else {
    //        Metronic.alert({
    //            container: "#bootstrap_alerts",
    //            place: "append",
    //            type: "danger",
    //            message: result.message,
    //            close: true,
    //            reset: true,
    //            focus: true,
    //            closeInSeconds: "0",
    //            icon: "warning"
    //        });
    //    }
    //};
    //REST.sendRequest({
    //    'c': 'beprom',
    //    'm': 'iprom',
    //    'data': data
    //});
}