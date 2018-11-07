var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_voucher").addClass("active");
    Preload();

    $(".money").inputmask("decimal", { allowPlus: false, allowMinus: false, rightAlignNumerics: false, radixPoint: ",", autoGroup: true, groupSeparator: ".", groupSize: 3 });

    DateTimePicker.init();
    $("[name=StartDate]").datetimepicker({
        format: "dd MM yyyy hh:ii",
        autoclose: true,
        todayBtn: true,
        startDate: moment().format("YYYY-MM-DD HH:mm"),
        minuteStep: 10
    });
    $("[name=EndDate]").datetimepicker({
        format: "dd MM yyyy hh:ii",
        autoclose: true,
        todayBtn: true,
        minuteStep: 10
    });
    $("[name=StartDate]").datetimepicker().on('changeDate', function (ev) {
        $('[name=EndDate]').datetimepicker('setStartDate', moment.utc(ev.date).add(1, 'hours').format("YYYY-MM-DD HH:mm"));
    });

    $("[name=EndDate]").datetimepicker().on('changeDate', function (ev) {
        $('[name=StartDate]').datetimepicker('setEndDate', moment.utc(ev.date).subtract(1, 'hours').format("YYYY-MM-DD HH:mm"));
    });

    $('[name=VoucherType]').on('switchChange.bootstrapSwitch', function (event, state) {
        if (state) {
            $('.typePercent').parent().removeClass("hidden");
            $('.typeAmount').parent().addClass("hidden");
            $('.typeAmount').removeAttr('name');
            $('.typePercent').attr('name', 'Value');

        } else {
            $('.typeAmount').parent().removeClass("hidden");
            $('.typePercent').parent().addClass("hidden");
            $('.typePercent').removeAttr('name');
            $('.typeAmount').attr('name', 'Value');
        }
    });
    $("input[type=checkbox]").bootstrapSwitch('state', true);
    VoucherInsertValidation();
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var dataCustomer = result.d.data.Customer;

            var _data = [];
            for (var i = 0; i < dataCustomer.length; i++) {
                _data.push({
                    id: dataCustomer[i].IDCustomer,
                    text: dataCustomer[i].FirstName + ' ' + dataCustomer[i].LastName + ' (' + dataCustomer[i].Email + ')'
                });
            }

            $("#IDCustomer").select2({
                data: _data,
                placeholder: 'choose customer',
                allowClear: true
            }).select2("data", null);
        }
    };
    REST.sendRequest({
        'c': 'bevo',
        'm': 'preload',
        'data': {}
    });
}

function VoucherInsertValidation() {
    var e = $("#form_voucher");
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
            Code: {
                required: true
            },
            StartDate: {
                required: true
            },
            EndDate: {
                required: true
            },
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
    var form = $("#form_voucher");
    //moment($("[name=StartDate]").val(),"DD MMM YYYY HH:mm").format("YYYY MM DD HH:mm")
    $(".form-control", form).each(function (index, element) {
        if ($(element).attr("name") == "IDCustomer") {
            if ($(element).select2("data") != null)
                data[$(element).attr("name")] = +$("#IDCustomer").select2("data").id;
            else
                data[$(element).attr("name")] = null;
        }
        else if ($(element).attr("name") == "VoucherType") {
            if ($(element).bootstrapSwitch("state")) {
                data[$(element).attr("name")] = "percent";
                data["Value"] = +$("input.typePercent").val();
            }
            else {
                data[$(element).attr("name")] = "amount";
                data["Value"] = Number(ReplacePoint($("input.typeAmount").val()));
            }
        }
        else if ($(element).attr("name") == "StartDate" || $(element).attr("name") == "EndDate") {
            data[$(element).attr("name")] = moment($(element).val(), "DD MMM YYYY HH:mm").format("YYYY MM DD HH:mm")
        }
        else if ($(element).attr("name") == "Active")
            data[$(element).attr("name")] = $(element).bootstrapSwitch("state");
        else if ($(element).attr("name") == "TotalAvailable" || $(element).attr("name") == "MinimumAmount")
            data[$(element).attr("name")] = Number(ReplacePoint($(element).val()));
        else if ($(element).attr("name") != "Value" && $(element).attr("name") != undefined)
            data[$(element).attr("name")] = $(element).val();
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
            $("#form_attribute").clearForm();
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
        'c': 'bevo',
        'm': 'ivo',
        'data': data
    });
}