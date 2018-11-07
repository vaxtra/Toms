var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    Preload();

    $("#btnLogout").click(function () {
        Logout();
    });

    $(".closepopupgrade").click(function () {
        $(".overlayupgrade").fadeOut();
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

            var CustomerProduct = result.d.data.CustomerProduct;
            if (CustomerProduct != null) {
                LoadCustomerProduct(CustomerProduct);
            }
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'CustomerProduct', 'CartSummary', 'Currency'],
        }
    });
}

function LoadCustomerProduct(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<tr>';
        item += '<td>' + data[i].ProductName + '</td>';
        item += '<td>' + moment(data[i].StartDate).format("DD MMMM YYYY") + '</td>';
        item += '<td>' + moment(data[i].EndDate).format("DD MMMM YYYY") + '</td>';
        item += '<td><a class="btn btn-block btn-black btn-upgrade" href="#" data-id="' + data[i].IDCustomer_Product + '" data-idproduct="' + data[i].IDProduct + '" data-idcustomerproduct="' + data[i].IDCustomer_Product + '" class="btn-detail">Upgrade</a>';
        item += '<a class="btn btn-block btn-black btn-renew" href="#" data-id="' + data[i].IDCustomer_Product + '" data-idproduct="' + data[i].IDProduct + '" data-idcustomerproduct="' + data[i].IDCustomer_Product + '" class="btn-detail">Renew</a></td>';
        item += '</tr>';
    }
    $(".order-history tbody").html(item);

    $(".btn-renew").click(function () {
        ClearCart();
        RenewPackage(+$(this).data("idproduct"), $(this).data("idcustomerproduct"));
    });

    $(".btn-upgrade").click(function () {
        $(".overlayupgrade").fadeIn();
        LoadSimillarProduct(+$(this).data("idproduct"), $(this).data("idcustomerproduct"));
    });

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

function RenewPackage(idProduct, idCustomerProduct) {
    $.ajax({
        url: "/modules/saas/Handler.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "saas",
            m: "renew",
            data: {
                IDProduct: idProduct,
                IDCustomer: +$("#HiddenIDCustomer").val(),
                IDCustomerProduct: +idCustomerProduct
            }
        }),
        beforeSend: function () {
            //Metronic.blockUI({
            //    boxed: true
            //});
        },
        success: function (result) {
            if (result.success) {
                window.location = "/Address"
            }
            else {
                //bootbox.alert(result.d.message);
                bootbox.alert(result.data.message, function () {
                    location.reload();
                });
            }
        },
        complete: function () {
            Metronic.unblockUI();
        },
        error: function (result) {
            bootbox.alert(result.d.message, function () {
                location.reload();
            });
        }
    });
}

function ClearCart() {
    REST.onSuccess = function (result) {

    };
    REST.sendRequest({
        'c': 'fesum',
        'm': 'clearcart',
        'data': {

        }
    });
}

function LoadSimillarProduct(idProduct, idCustomerProduct)
{
    REST.onSuccess = function (result) {
        var item = '';
        var data = result.d.data.SimiliarProduct
        for (var i = 0; i < data.length; i++) {
            item += '<div class="col-md-4 col-sm-6 col-xs-6">';
            item += '<div class="single_feature text-center">';
            item += '<div class="feature-img">';
            item += '<a href="#" class="btn-toupgrade" data-idproduct="' + data[i].IDProduct + '" data-idcustomerproduct="' + idCustomerProduct + '">';
            item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
            item += '</a>';
            item += '<span class="img-hover">';
            item += '<a href="#" class="btn-toupgrade" data-idproduct="' + data[i].IDProduct + '" data-idcustomerproduct="' + idCustomerProduct + '">';
            item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
            item += '</a>';
            item += '</span>';
            item += '</div>';
            item += '<div class="feature_text">';
            item += '<a href="#" class="btn-toupgrade" data-idproduct="' + data[i].IDProduct + '" data-idcustomerproduct="' + idCustomerProduct + '"><h4>' + data[i].Name + '</h4></a>';
            if (data[i].PriceBeforeDiscount > data[i].Price) {
                item += '<span class="format-money">' + data[i].Price + '</span>&nbsp;&nbsp;<del class="format-money" style="color:#ff0000;">' + data[i].PriceBeforeDiscount + '</del>';
            }
            else {
                item += '<span class="format-money">' + data[i].Price + '</span>';
            }
            item += '</div>';
            item += '</div>';
            item += '</div>';
        }

        $('.productUpgrade').html(item);

        $(".btn-toupgrade").click(function () {
            ClearCart();
            UpgradePackage($(this).data("idproduct"), $(this).data("idcustomerproduct"));
        });

        $(".format-money").formatCurrency({
            region: "id-ID"
        });

    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['SimiliarProduct'],
            '_param_IDProduct': idProduct,
            '_param_take': 4
        }
    });
}

function UpgradePackage(idProduct, idCustomerProduct) {
    $.ajax({
        url: "/modules/saas/Handler.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "saas",
            m: "upgrade",
            data: {
                IDProduct: idProduct,
                IDCustomer: +$("#HiddenIDCustomer").val(),
                IDCustomerProduct: +idCustomerProduct
            }
        }),
        beforeSend: function () {
            //Metronic.blockUI({
            //    boxed: true
            //});
        },
        success: function (result) {
            if (result.success) {
                window.location = "/Address"
            }
            else {
                //bootbox.alert(result.d.message);
                bootbox.alert(result.data.message, function () {
                    location.reload();
                });
            }
        },
        complete: function () {
            Metronic.unblockUI();
        },
        error: function (result) {
            bootbox.alert(result.d.message, function () {
                location.reload();
            });
        }
    });
}