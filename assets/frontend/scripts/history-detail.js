var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";
var conversionRate = "";

$(document).ready(function () {

    PreloadCart();

    if (queryString("id")) {
        LoadDetailOrder(+queryString("id"));
    }
    else
        window.location = 'OrderHistory.aspx';

    $("#btnLogout").click(function () {
        Logout();
    });
    

});

function LoadDetailOrder(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var data = result.d.data;
            $.each(data, function (indexInArray, valueOfElement) {


                if (indexInArray == "BillingAddress") {
                    $(".billing-address").html(valueOfElement.PeopleName + '<br/>' + valueOfElement.Address + '<br/>' + valueOfElement.DistrictName + '<br/>' + valueOfElement.CityName + '<br/>' + valueOfElement.ProvinceName + '<br/>' + valueOfElement.CountryName + '<br/>' + valueOfElement.PostalCode + '<br/>' + valueOfElement.Phone + '<br/>');
                }
                else if (indexInArray == "DeliveryAddress") {
                    $(".delivery-address").html(valueOfElement.PeopleName + '<br/>' + valueOfElement.Address + '<br/>' + valueOfElement.DistrictName + '<br/>' + valueOfElement.CityName + '<br/>' + valueOfElement.ProvinceName + '<br/>' + valueOfElement.CountryName + '<br/>' + valueOfElement.PostalCode + '<br/>' + valueOfElement.Phone + '<br/>');
                }
                else if (indexInArray == "Date") {
                    $(".DateOrder").html(moment(valueOfElement).format("DD-MM-YYYY hh:mm"));
                }
                else if (indexInArray == "ShippingImage") {
                    $(".ShippingImage").html("<img src='./assets/images/carrier/" + valueOfElement + "' class='img-full' />");
                }
                else if (indexInArray == "PaymentImage") {
                    $(".PaymentImage").html("<img src='./assets/images/Payment_Method/" + valueOfElement + "' class='img-full' />");
                }
                else if (indexInArray == "Product") {
                    LoadProduct(valueOfElement);
                }
                else if (indexInArray == "TotalDiscount") {
                    $("." + indexInArray).html(valueOfElement / conversionRate);
                }
                else if (indexInArray == "TotalPaid") {
                    $("." + indexInArray).html(valueOfElement / conversionRate);
                }
                else if (indexInArray == "TotalPriceProduct") {
                    $("." + indexInArray).html(valueOfElement / conversionRate);
                }
                else if (indexInArray == "TotalShipping") {
                    $("." + indexInArray).html(valueOfElement / conversionRate);
                }
                else {
                    $("." + indexInArray).html(valueOfElement);
                }
            });

            $("#viewModal").modal("show");
            $(".format-money").formatCurrency({ region: format });
        }
        else {
            console.log(result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'fecust',
        'm': 'detorder',
        'data': {
            'IDOrder': id
        }
    });
}

function PreloadCart() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }

            var currency = result.d.data.Currency;
            if (currency != null) {
                LoadCurrency(currency);
            }

            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);

                $(".TotalPrice").text(result.d.data.CartSummary.TotalPrice);
                $(".Subtotal").text(result.d.data.CartSummary.TotalPrice);
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
                $(".loginbut").text("LOGIN");
                $(".btn-logout").hide();
                window.location = "Authentication.aspx";
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

function LoadProduct(data) {
    var row = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            row += '<tr>';
            row += '<td class="cartmageprd centerBro">';
            row += '<a href="ProductDetail.aspx?id=' + data[i].IDProduct + '">';
            row += '<img  src="./assets/images/product/' + data[i].Photo + '" width="100" alt="Product Image" />';
            row += '</a>';
            row += '</td>';
            row += '<td>';
            row += '<a class="ProductName" href="./ProdutDetail.aspx?id=' + data[i].IDProduct + '">' + data[i].ProductName + '</a><br />';
            row += '<span class="CombinationName">' + data[i].CombinationName + '</span>';
            row += '</td>';
            row += '<td class="centerBro PriceUnit format-money">' + data[i].Price / conversionRate + '</td>';
            row += '<td class="centerBro">';
            row += '<label class="textboxQty centerBro" data-maxquantity="' + data[i].MaxQuantity + '" data-idcombination="' + data[i].IDCombination + '">' + data[i].Quantity + '<label>';
            row += '</td>';
            row += '<td class="rightBro format-money">' + data[i].Subtotal / conversionRate + '</td>';
            row += '</tr>';
        }
        $("#detailOrder tbody").html(row);

        $(".money").formatCurrency({ region: format });
    }
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
            conversionRate = data[i].ConversionRate;
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

