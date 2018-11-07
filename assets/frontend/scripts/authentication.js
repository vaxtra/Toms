var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    Login();
    Register();
    Forget_validation();
    PreloadMaster();
    $(".btn-forgot").click(function () {
        $("#FormLogin").fadeOut();
        $("#FormForgot").fadeIn();
    })
});

function CheckEmail() {

    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            window.location = 'Register.aspx?email=' + $("input[name=Email]").val() + '&back=' + queryString("back");
        }
        else {
            bootbox.alert(result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'feauth',
        'm': 'EmailCheck',
        'data': {
            'e': $("#tbEmailSignUp").val(),
        }
    });
}

function PreloadMaster() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            var currency = result.d.data.Currency;
            if (currency != null) {
                LoadCurrency(currency);
            }

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
                    window.location = "MyAccount.aspx";
                }
                else {
                    $(".loginbut").text("GUEST");
                    $(".loginbut").attr("href", "#");
                    $(".btn-logout").hide();
                    $(".btn-newAddress").hide();
                    window.location = "Shipping.aspx";
                }
            }
            else {
                $(".loginbut").text("LOGIN / REGISTER");
                $(".btn-logout").hide();
            }


            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                var TotalQuantity = result.d.data.CartSummary.TotalQuantity;
                $(".TotalQuantity").text(TotalQuantity);
                LoadListCartSummary(listCartSummary, TotalPrice);
            }
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'CartSummary', 'Currency']
        }
    });
}

function Register() {
    var e = $("#FormRegister");
    var t = $("#bootstrap_alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Email: {
                required: true,
                email: true
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
            //t.hide();
            CheckEmail()
        }
    });
}

function Login() {
    var e = $("#FormLogin");
    var t = $("#bootstrap_alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
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

            REST.onSuccess = function (result) {
                var data = result.d.data;
                if (result.d.success) {
                    if (queryString("back")) {
                        window.location = queryString("back");
                    } else {
                        window.location = "MyAccount.aspx";
                    }
                }
                else {
                    bootbox.alert(result.d.message);
                }
            };
            REST.sendRequest({
                'c': 'feauth',
                'm': 'login',
                'data': {
                    'e': $("input[name=Email]", e).val(),
                    'p': $("input[name=Password]", e).val()
                }
            });
        }
    });
}
function LoadCartList(data) {
    var total = data.TotalPrice;
    var totalQuantity = data.TotalQuantity;
    data = data.Product;
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += ' <tr>';
        item += '<td>';
        item += '<a href="ProductDetail.aspx?id=' + data[i].IDProduct + '">';
        item += '<img src="./assets/images/product/' + data[i].Photo + '" width="100" height="75" alt="' + data[i].ProductName + '" />';
        item += '</a>';
        item += '</td>';
        item += '<td>';
        item += '<a class="bh-text-uppercase" href="ProductDetail.aspx?id=' + data[i].IDProduct + '">' + data[i].ProductName + '</a><br />';
        item += '<span class="sizing">' + data[i].CombinationName + '</span><br />';
        item += '<span class="format-money">' + data[i].PricePerUnit + '</span>';
        item += '</td>'
        item += '<td class="uk-text-center"><a href="#"><i class="uk-icon-times"></i></a></td>';
        item += '</tr>';

    }
    $(".cart-list tbody").html(item);
    $(".TotalPrice").text(total + ' IDR');
    $(".Subtotal").text(total + ' IDR');
    $(".TotalQuantity").text(totalQuantity);

    $(".format-money").formatCurrency({
        region: format
    })

    $(".delete-cart").click(function (e) {
        e.preventDefault();
        DeleteCart(+$(this).data("idcombination"), this);
    });
}

function Forget_validation() {
    var e = $("#FormForgot");
    var t = $("#bootstrap_alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {
            Email: {
                required: 'Please enter your email address',
                email: 'invalid email format'
            }
        },
        rules: {
            Email: {
                required: true,
                email: true
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
            Forget();
        }
    });
}

function Forget() {
    REST.onSuccess = function (result) {
        $("input[name=Email]").val('');
        if (result.d.success) {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "default",
                message: "Email sent, please check your inbox or spam folder.",
                close: true,
                reset: true,
                focus: true,
                //closeInSeconds: 5,
                icon: "envelope"
            });
            $("#FormForgot").fadeOut();
            $("#FormLogin").fadeIn();
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "error",
                message: "Email sent, please check your inbox or spam folder.",
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
        'm': 'forgot',
        'data': {
            'Email': $(".EmailForgot").val()
        }
    });
}

function LoadListCartSummary(data, TotalPrice) {
    item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li>';
        item += '<a href="/ProductDetail/' + data[i].ProductName.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
        item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].ProductName + '" /></a>';
        item += '<div class="bag_list_details floatright">';
        item += '<a href="/ProductDetail/' + data[i].ProductName.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
        item += '<h4>' + data[i].ProductName + '</h4>';
        item += '</a>';
        item += '<span class="format-money">' + data[i].Price + '</span>';
        item += '</div>';
        item += '</li>';
    }
    $('.list-summary').html(item);

    $('.TotalPrice').text(TotalPrice);
    $(".format-money").formatCurrency({
        region: "id-ID"
    })
}

function LoadCurrency(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        if (data[i].Selected) {
            item += '<option data-id="' + data[i].IDCurrency + '" style="color:red" class="format-money" selected="selected">' + data[i].Name + '</option>';
            format = data[i].Format;
        }
        else
            item += '<option data-id="' + data[i].IDCurrency + '" class="format-money">' + data[i].Name + '</option>';
    }

    $("#SelectCurrency").html(item);
    $("#SelectCurrency").on("change", function () {
        console.log($("#SelectCurrency option:selected").data("id"));
        ChangeCurrency($("#SelectCurrency option:selected").data("id"));
    });

    $(".format-money").formatCurrency({
        region: format
    })
}

function ChangeCurrency(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            location.reload();
        }
    };
    REST.sendRequest({
        'c': 'fecur',
        'm': 'chgcur',
        'data': {
            'ID': id
        }
    });
}
