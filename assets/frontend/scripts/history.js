﻿var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    Preload();

    $("#btnLogout").click(function () {
        Logout();
    });
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

            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }
            else {
                window.location = "Authentication.aspx";
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

            var order = result.d.data.OrderHistory;
            if (order != null) {
                LoadOrderHistory(order);
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
            'RequestData': ['Customer', 'OrderHistory', 'CartSummary', 'Currency', 'ExpiredNotification']
        }
    });
}

function LoadOrderHistory(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<tr>';
        item += '<td>' + data[i].Reference + '</td>';
        item += '<td>' + moment(data[i].Date).format("DD MMM YYYY") + '</td>';
        item += '<td><label class="totalprice format-money">' + data[i].TotalPaid + '</label></td>';
        item += '<td>' + data[i].PaymentMethod + '</td>';
        if (data[i].ShippingNumber != null)
            item += '<td>' + data[i].ShippingNumber + '</td>';
        else
            item += '<td>-</td>';
        item += '<td>' + data[i].OrderStatus + '</td>';
        item += '<td><a href="OrderDetail.aspx?id=' + data[i].IDOrder + '" data-id="' + data[i].IDOrder + '" class="btn-detail">Detail</a></td>';
        item += '</tr>';
    }
    $(".order-history tbody").html(item);
    $(".format-money").formatCurrency({ region: format });
}

function LoadDetailOrder(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var data = result.d.data;
            $(".dethistCoy").fadeIn(200);
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
                    $("#ShippingImage").attr("src", valueOfElement.ShippingImage);
                }
                else if (indexInArray == "PaymentImage") {
                    $("#PaymentImage").attr("src", valueOfElement.PaymentImage);
                }
                else if (indexInArray == "Product") {
                    LoadProduct(valueOfElement);
                }
                else if (indexInArray == "TotalPrice") {
                    $(".TotalPriceHist").text(valueOfElement.TotalPrice);
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

function LoadProduct(data) {
    var row = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            row += '<tr>';
            row += '<td>';
            row += '<a class="bh-text-uppercase uk-link-reset" href="./ProdutDetail.aspx?id=' + data[i].IDProduct + '">' + data[i].ProductName + '</a><br />';
            row += '<span class="uk-text-muted uk-text-small">' + data[i].CombinationName + '</span>';
            row += '</td>';
            row += '<td class="uk-text-center format-money">' + data[i].Price + '</td>';
            row += '<td class="uk-text-center">';
            row += '<form class="uk-form" />';
            row += '<label class="uk-form-width-mini textboxQty" type="text" data-maxquantity="' + data[i].MaxQuantity + '" data-idcombination="' + data[i].IDCombination + '">' + data[i].Quantity + '</label></form>';
            row += '</td>';
            row += '<td class="uk-text-right format-money">' + data[i].TotalPrice + '</td>';
            row += '</tr>';
        }
        $("#detailOrder tbody").html(row);
        $(".money").formatCurrency({ region: format });
    }
}
function Logout() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            location.reload();
        }
        else {
            bootbox.alert(result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'logout',
        'data': {}
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