var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    PreloadMaster();

    $(".btn-next").click(function (e) {
        e.preventDefault();
        if ($("[name=payment]:checked").data("idpayment_method") != null || $("[name=payment]:checked").data("idpayment_method") != undefined) {
            if ($("[name=payment]:checked").data("type") == "Veritrans") {
                SubmitOrdervt(+$("[name=payment]:checked").data("idpayment_method"));
            }
            else
                SubmitPayment(+$("[name=payment]:checked").data("idpayment_method"));
        }

        else
            bootbox.alert("Please choose your payment method");
    });
});

function SubmitOrdervt(idPaymentMethod) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            console.log(result.d);
            if (result.d.data != null)
                window.location = result.d.data;
            else
                bootbox.alert(result.d.message);
        }
        else {
            console.log("ERROR : " + result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'fevt',
        'm': 'vtwebtrx',
        'data': {
            'IDPaymentMethod': idPaymentMethod
        }
    });
}

function PreloadMaster() {
    REST.onComplete = function (result) { };
    REST.onSuccess = function (result) {
        if (result.d.success) {

            var currency = result.d.data.Currency;
            if (currency != null) {
                LoadCurrency(currency);
            }
            else {
                console.log('currency null');
            }

            if (result.d.data.CartSummary.Voucher) {
                $(".VoucherPrice").text(result.d.data.CartSummary.Voucher.Discount);
            }
            else {
                $(".VoucherPrice").text(0);
            }

            if (result.d.data.CartSummary.Shipping) {
                $(".ShippingPrice").text(result.d.data.CartSummary.Shipping.Price);
            }
            else {
                $(".ShippingPrice").text(0);
            }

            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }

            var Customer = result.d.data.Customer;
            if (Customer != null) {
                if (Customer.IDCustomer_Group != 2) {
                    $(".loginbut").text("Welcome, " + Customer.FirstName);
                    $(".loginbut").attr("href", "MyAccount.aspx");
                    $("#HiddenIDCustomer").val(Customer.IDCustomer);
                    $(".btn-logout").show();
                    $(".btn-newAddress").show();

                    $(".btn-logout").show();
                    $(".btn-login").hide();
                    $(".FirstName").html(Customer.FirstName);
                }
                else {
                    $(".loginbut").text("GUEST");
                    $(".loginbut").attr("href", "#");
                    $(".btn-logout").hide();
                    $(".btn-newAddress").hide();
                }
            }
            else {
                $(".loginbut").text("LOGIN");
                $(".btn-logout").hide();
                window.location = "authentication.aspx?back=Address.aspx";
            }

            //PAYMENT METHOD
            var payment = result.d.data.Payment;
            if (payment) {
                var item = '';
                for (var i = 0; i < payment.length; i++) {

                    item += '<tr>';
                    item += '<td>';
                    item += '<input type="radio" name="payment" id="radio' + payment[i].IDPaymentMethod + '" class="css-checkbox" data-idpayment_method="' + payment[i].IDPaymentMethod + '" data-type="' + payment[i].Type + '" /><label for="radio' + payment[i].IDPaymentMethod + '" class="css-rlabel radGroup1"><img src="assets/images/payment_method/' + payment[i].Image + '" /></label>';
                    item += '</td>';
                    item += '<td>';
                    item += '<label>' + payment[i].Owner + '</label>';
                    item += '</td>';
                    item += '<td>';
                    item += '<label>' + payment[i].AccountNumber + '</label>';
                    item += '</td>';
                    item += '</tr>';

                }

                $(".payment-list").html(item);
            }

            if (result.d.data.CartSummary) {

                $.each(result.d.data.CartSummary, function (indexInArray, valueOfElement) {
                    $("." + indexInArray).text(valueOfElement);
                });

                $(".TotalShipping").text(result.d.data.CartSummary.Shipping.TotalPrice);

                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);
            }
            else {
                window.location = "Summary.aspx";
            }
        }
        else {
            bootbox.alert(result.d.message, function (e) {
                window.location = 'authentication.aspx';
            });
        }

        $(".format-money").formatCurrency({
            region: format
        })
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'CartSummary', 'Payment','Currency']
        }
    });
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

function SubmitPayment(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            window.location = "PaymentFinal.aspx";
        }
        else {
            console.log("ERROR : " + result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'fepay',
        'm': 'ipaytocart',
        'data': {
            'IDPayment': id
        }
    });
}

function SubmitOrder() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            window.location = "PaymentFinal.aspx";
        }
        else
            bootbox.alert(result.d.message);
    };
    REST.sendRequest({
        'c': 'feorder',
        'm': 'iorder',
        'data': {}
    });
}

function LoadCart(cartData) {
    if (cartData != undefined && cartData.length > 0) {
        var item = '';
        for (var i = 0; i < cartData.length; i++) {
            item += '<div class="col-md-12 listsum nopad">';
            item += '<div class="col-md-6 nopad">';
            item += '<div class="sumage">';
            item += '<img src="./assets/images/product/' + cartData[i].Photo + '" />';
            item += '</div>';
            item += '</div>';
            item += '<div class="col-md-6 nopad sumdesc">';
            item += '<p>' + cartData[i].ProductName + '</p>';
            item += '<label class="format-money">' + cartData[i].Price + '</label>';
            item += '</div>';
            item += '</div>';
            item += '</div>';

        }
    }

    $(".cart-sums").html(item);

    $(".format-money").formatCurrency({
        region: format
    })
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