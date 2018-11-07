var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    PreloadMaster();

    $(".btn-next").click(function (e) {
        e.preventDefault();
        if ($("#cbAgree").prop("checked"))
            SubmitShipping(+$("[name=Shipping]:checked").data("idshipping"));
        else
            bootbox.alert("You must agree to our terms of service");
    });

});

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

            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);
            }
            else {
                window.location = "Product.aspx";
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
            if (result.d.data.CartSummary) {
                $(".TotalPrice").text(result.d.data.CartSummary.TotalPrice);
                $(".Subtotal").text(result.d.data.CartSummary.Subtotal);
                $(".TotalQuantity").text(result.d.data.CartSummary.TotalQuantity);
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

            //SHIPPING
            var shipping = result.d.data.Shipping;
            if (shipping)
                LoadShipping(shipping);
        }
        else {
            bootbox.alert(result.d.message, function (e) {
                window.location = 'authentication.aspx';
            });
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'CartSummary', "Shipping","Currency"]
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

function LoadShipping(data) {
    var item = "";
    for (var i = 0; i < data.length; i++) {
        if (data[i].IDCarrier != 8) {
            if (i == 0)
                item += '<tr><td><input class="css-checkbox" checked="checked" id="Ship' + data[i].IDShipping + '" name="Shipping" data-idshipping="' + data[i].IDShipping + '" data-idcarrier="' + data[i].IDCarrier + '" type="radio" /></td><td class="text-center"><label class="text-center" for="Ship' + data[i].IDShipping + '"><img class="img-carrier" src="./assets/images/carrier/' + data[i].ImageShipping + '" /></label></td><td>' + data[i].Name + '</td><td>' + data[i].Information + '</td><td class="format-money">' + data[i].TotalPrice + ' IDR</td></tr>';
            else
                item += '<tr><td><input class="css-checkbox" id="Ship' + data[i].IDShipping + '" name="Shipping" data-idshipping="' + data[i].IDShipping + '" data-idcarrier="' + data[i].IDCarrier + '" type="radio" /></td><td class="text-center"><label class="text-center" for="Ship' + data[i].IDShipping + '"><img class="img-carrier" src="./assets/images/carrier/' + data[i].ImageShipping + '" /></label></td><td>' + data[i].Name + '</td><td>' + data[i].Information + '</td><td class="format-money">' + data[i].TotalPrice + ' IDR</td></tr>';
        }
    }
    $("#table_shipping tbody").html(item);

    $(".format-money").formatCurrency({
        region: format
    })
}

function SubmitShipping(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            window.location = 'Payment.aspx';
        }
        else
            console.log("ERROR : " + result.d.message);
    };

    REST.sendRequest({
        'c': 'feshp',
        'm': 'ishptocrt',
        'data': {
            'IDShipping': id
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