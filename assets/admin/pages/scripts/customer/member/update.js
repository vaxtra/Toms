$(document).ready(function () {
    
    $(".alert").hide();
    if (queryString("id")) {
        $("#HiddenIDCustomer").val(queryString("id"));
    }
    else {
        window.location = "./default.aspx";
    }
    Preload();
    SubmitUpdatePoint()
});

function Preload() {

    $.ajax({
        url: "/modules/MemberPoint/MemberPoint.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "MemberPoint",
            t: "be",
            m: "getpoint",
            data: {
                IDCustomer: +$("#HiddenIDCustomer").val()
            }
        }),
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true
            });
        },
        success: function (result) {
            $("input[name=Point]").val(result.data.Point);
        },
        error: function (result) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "3",
                icon: "warning"
            });
        },
        complete: function () {
            Metronic.unblockUI();
        },
    });
}

function SubmitUpdatePoint() {
    var e = $("#FormMember");
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
            Point: {
                required: true
            },
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

                        sendData[element.name] = $(element).val();
                        //console.log($(element).val());
                }
            });
            sendData["IDCustomer"] = +$("#HiddenIDCustomer").val();
            console.log(sendData);

            $.ajax({
                url: "/modules/MemberPoint/MemberPoint.ashx",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    c: "MemberPoint",
                    t: "be",
                    m: "upmem",
                    data: sendData
                }),
                beforeSend: function () {
                    Metronic.blockUI({
                        boxed: true
                    });
                },
                success: function (result) {
                    if (result.data.success) {
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
                    }
                    else {
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
                    }
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
        }
    });
}