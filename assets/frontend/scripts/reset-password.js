﻿var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    Forget_validation();
    if (queryString('token')) {
        PreloadMaster();
        $("#token").val(queryString('token'));
    }
    else
        window.location = 'authentication.aspx';
});

function PreloadMaster() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }
            if (result.d.data.Cart) {
                $(".TotalPrice").text(result.d.data.Cart.TotalPrice + ' IDR');
                $(".TotalQuantity").text(result.d.data.Cart.TotalQuantity);
            }
            var Customer = result.d.data.Customer;
            if (Customer != null) {
                if (Customer.IDCustomer_Group != 2) {
                    $(".loginbut").text("Welcome, " + Customer.FirstName);
                    $(".loginbut").attr("href", "MyAccount.aspx");
                    $("#HiddenIDCustomer").val(Customer.IDCustomer);
                    $(".btn-logout").show();
                    $(".btn-newAddress").show();

                    if (queryString('back'))
                        window.location = queryString('back');
                    $(".btn-logout").show();
                    $(".btn-login").hide();
                    $(".FirstName").html(Customer.FirstName);
                }
                else {
                    $(".loginbut").text("GUEST");
                    $(".loginbut").attr("href", "#");
                    $(".btn-logout").hide();
                    $(".btn-newAddress").hide();
                    window.location = "Authentication.aspx";
                }
            }
            else {
                $(".btn-logout").hide();
                $(".btn-login").show();
                $(".FirstName").html("-");
            }

            var ExpiredNotification = result.d.data.ExpiredNotification;
            if (ExpiredNotification) {
                LoadNotification(ExpiredNotification);
            }

        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'AutoCancel', 'ExpiredNotification']
        }
    });
}

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

            setTimeout(function () { window.location = 'authentication.aspx'; }, 3000);
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
        'c': 'feauth',
        'm': 'reset',
        'data': {
            'Token': $("#token").val(),
            'Password': $("#Password").val()
        }
    });
}
function LoadNotification(data) {
    var item = '';
    var endDate;
    var currentDate = new Date();
    console.log(datediff(currentDate, endDate));
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            endDate = new Date(data[i].EndDateYear, data[i].EndDateMonth, data[i].EndDateDay, data[i].EndDateHour, data[i].EndDateMinute, data[i].EndDateSecond, data[i].EndDateMiliSecond);
            item += '<div class="top-cart-items">';
            item += '<div class="top-cart-item clearfix">';
            item += '<div class="top-cart-item-desc">';
            if (datediff(currentDate, endDate) <= 0) {
                item += '<p>Your ' + data[i].ProductName + ' is expired</p>';
            }
            if (datediff(currentDate, endDate) <= 60) {
                item += '<p>Your ' + data[i].ProductName + ' will expire in ' + datediff(currentDate, endDate) + ' day(s)</p>';
            }
            item += '</div>';
            item += '</div>';
            item += '</div>';
        }
    }
    else {
        item += '<div class="top-cart-items">';
        item += '<p>You have no notification about your package</p>';
        item += '</div>';
    }

    $(".notif-list").html(item);

    $("#top-cart-trigger span").text(data.length);
}

function datediff(first, second) {
    // Take the difference between the dates and divide by milliseconds per day.
    // Round to nearest whole number to deal with DST.
    return Math.round((second - first) / (1000 * 60 * 60 * 24));
}