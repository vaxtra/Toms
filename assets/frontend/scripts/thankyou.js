var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    Preload();
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            var currency = result.d.data.Currency;
            if (currency != null) {
                LoadCurrency(currency);
            }
            else {
                console.log('currency null');
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
            }

            var orderSummary = result.d.data.OrderSummary;
            $(".TotalPaid").text(orderSummary.TotalPaid);
            $(".Reference").text(orderSummary.Reference);
            LoadDeliveryAddress(orderSummary.DeliveryAddress);
            LoadBillingAddress(orderSummary.BillingAddress);

            $(".ShippingName").text(orderSummary.Shipping.Name);
            $(".PaymentMethod").text(orderSummary.PaymentMethod.Name);
            $("#ImageShipping").attr("src", "/assets/images/carrier/" + orderSummary.Shipping.Image);
            $("#ImagePayment").attr("src", "/assets/images/payment_method/" + orderSummary.PaymentMethod.Image);
            $(".PaymentOwner").text(orderSummary.PaymentMethod.Owner);
            $(".AccountNumber").text(orderSummary.PaymentMethod.AccountNumber);

            $(".format-money").formatCurrency({
                region: format
            })
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ["OrderSummary", "Customer", "Currency"]
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

function LoadDeliveryAddress(data) {
    var item = "";
    item += '<li><span class="name">' + data.PeopleName + '</span></li>';
    item += '<li>' + data.Address + '</li>';
    item += '<li>' + data.PostalCode + ' ' + data.CityName + '</li>';
    item += '<li>Indonesia</li>';
    $(".delivery-address").html(item);
    $(".update-delivery").attr('data-idaddress', data.IDAddress);
    $(".update-delivery").attr('href', 'update-address.aspx?id=' + data.IDAddress + '&back=address.aspx');

    $("#IDDeliveryAddress").val(data.IDAddress);
}

function LoadBillingAddress(data) {
    var item = "";
    item += '<li><span class="name">' + data.PeopleName + '</span></li>';
    item += '<li>' + data.Address + '</li>';
    item += '<li>' + data.PostalCode + ' ' + data.CityName + '</li>';
    item += '<li>Indonesia</li>';
    $(".billing-address").html(item);
    $(".update-billing").attr('data-idaddress', data.IDAddress);
    $(".update-billing").attr('href', 'update-address.aspx?id=' + data.IDAddress + '&back=address.aspx');

    $("#IDBillingAddress").val(data.IDAddress);
}