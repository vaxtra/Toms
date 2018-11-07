var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format;

$(document).ready(function () {
    $(".btn-logout").click(function (e) {
        e.preventDefault();
        Logout();

    });
    PreloadMaster();
    Contactus();
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
            else {
                $(".TotalQuantity").text("0");
                $(".TotalPrice").text('0 IDR');
            }
            var Customer = result.d.data.Customer;
            if (Customer != null) {
                if (Customer.IDCustomer_Group != 2) {
                    $(".loginbut").text("Welcome, " + Customer.FirstName);
                    $(".loginbut").attr("href", "MyAccount.aspx");
                    $("#HiddenIDCustomer").val(Customer.IDCustomer);
                    $(".btn-logout").show();
                    $(".btn-newAddress").show();
                }
                else {
                    $(".loginbut").text("GUEST");
                    $(".loginbut").attr("href", "#");
                    $(".btn-logout").hide();
                    $(".btn-newAddress").hide();
                }
            }
            else {
                $(".loginbut").text("Login / Register");
                $(".btn-logout").hide();
            }

            if (result.d.data.CartSummary != null) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);
                var jumlahProductCart = result.d.data.CartSummary.TotalQuantity;
                $('.TotalQuantity').text(jumlahProductCart);

            }
            if (result.d.data.CartSummary)
                LoadCartList(result.d.data.CartSummary);

            $(".format-money").formatCurrency({
                region: "id-ID"
            })

        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'Cart', 'CartSummary', 'Category']
        }
    });
}

function Contactus() {
    var e = $("#formContact");
    var t = $("#bootstrap_alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Name: {
                required: true
            },
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
            var sendData = {};

            $(".input-data", e).each(function (index, element) {
                // element == this
                if (element.name != undefined && element.name != "") {
                    sendData[element.name] = $(element).val();
                    //console.log($(element).val());
                }
            });

            REST.onSuccess = function (result) {
                var data = result.d.data;
                if (result.d.success) {
                    toastr.success(result.d.message)
                }
                else {
                    bootbox.alert(result.d.message);
                }
            };
            REST.sendRequest({
                'c': 'femaster',
                'm': 'contactus',
                'data': sendData
            });
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

