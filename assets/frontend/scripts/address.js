var REST = new OF({
    'url': '/api/services.asmx/request'
});

var count = 0;

$(document).ready(function () {
    PreloadMaster();

    if ($("#cbSame").prop("checked")) {
        $("#ddlAddress2").css("visibility", "hidden");
    }
    else {
        $("#ddlAddress2").css("visibility", "visible");
    }

    $('.placeorderdet').click(function (e) {
        e.preventDefault();
        if ($("#IDDeliveryAddress").val() && $("#IDBillingAddress").val() && $("[name=payment]:checked").data("idpayment_method") != null || $("[name=payment]:checked").data("idpayment_method") != undefined) {
            if ($("[name=payment]:checked").data("idpayment_method") != null || $("[name=payment]:checked").data("idpayment_method") != undefined) {

                SubmitOrder();
                //testSubmit();
            }
            else
                bootbox.alert("Please choose your payment method");
        }
        else {
            bootbox.alert("Please choose your payment method");
        }
    });

    $("#ddlAddress").on("change", function (e) {
        e.preventDefault();
        GetAddressDetail(+$(this).val(), $("#cbSame").prop("checked"));
    });
    $("#ddlAddress2").on("change", function (e) {
        e.preventDefault();
        GetAddressDetail2(+$(this).val(), $("#cbSame").prop("checked"));
    });

    $("#cbSame").on("change", function (e) {
        e.preventDefault();
        if ($("#cbSame").prop("checked")) {
            $("#ddlAddress2").css("visibility", "hidden");
        }
        else {
            $("#ddlAddress2").css("visibility", "visible");
        }
    })

    $("#Notes").focusout(function () {
        SubmitAddressToCart(+$("#IDDeliveryAddress").val(), +$("#IDBillingAddress").val(), $("#Notes").val());
    });

});

function PreloadMaster() {
    REST.onComplete = function (result) { };
    REST.onSuccess = function (result) {
        if (result.d.success) {

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
                window.location = "/Authentication?back=Address";
            }

            //if (result.d.data.CartSummary.DeliveryAddress)
            //    LoadDeliveryAddress(result.d.data.CartSummary.DeliveryAddress);
            //else

           
            //if (result.d.data.CartSummary.BillingAddress)
            //    LoadBillingAddress(result.d.data.CartSummary.BillingAddress);
            //else
            

            if (result.d.data.Addresses.length > 0)
            {
                LoadAddress(result.d.data.Addresses);
                LoadDeliveryAddress(result.d.data.Addresses[0]);
                LoadBillingAddress(result.d.data.Addresses[0]);
            }
            else {
                bootbox.alert("You don't have any address yet", function (e) {
                    window.location = '/NewAddress';
                });
            }


            if (result.d.data.Addresses.length > 0) {
                SubmitAddressToCart(+$("#IDDeliveryAddress").val(), +$("#IDBillingAddress").val(), $("#Notes").val());
            }
            else {
                bootbox.alert("You don't have any address yet", function (e) {
                    window.location = '/NewAddress';
                });
            }

            var payment = result.d.data.Payment;
            if (payment) {
                var item = '';
                for (var i = 0; i < payment.length; i++) {
                    //if (payment[i].IDPaymentMethod !=5) {
                    item += '<tr>';
                    item += '<td>';
                    item += '<input type="radio" name="payment" id="radio' + payment[i].IDPaymentMethod + '" class="css-checkbox" data-idpayment_method="' + payment[i].IDPaymentMethod + '" data-type="' + payment[i].Type + '" /><label for="radio' + payment[i].IDPaymentMethod + '" class="css-rlabel radGroup1"><img style="width:80px;" src="assets/images/payment_method/' + payment[i].Image + '" /></label>';
                    item += '</td>';
                    item += '<td>';
                    item += '<label>' + payment[i].Owner + '</label>';
                    item += '</td>';
                    item += '<td>';
                    item += '<label>' + payment[i].AccountNumber + '</label>';
                    item += '</td>';
                    item += '</tr>';
                    //}

                }

                $(".payment-list").html(item);

                //SubmitPayment(parseInt($("[name=payment]:checked").data("idpayment_method")));

                $('input[type=radio][name=payment]').change(function (e) {
                    e.preventDefault();
                    if ($("[name=payment]:checked").data("idpayment_method") != null || $("[name=payment]:checked").data("idpayment_method") != undefined) {
                        if ($("[name=payment]:checked").data("type") == "Veritrans") {
                            SubmitOrdervt(+$("[name=payment]:checked").data("idpayment_method"));
                        }
                        else {
                            SubmitPayment(+$("[name=payment]:checked").data("idpayment_method"));
                        }

                    }
                    else
                        bootbox.alert("Please choose your payment method");
                });
            }

            if (result.d.data.CartSummary) {

                if (result.d.data.CartSummary.Voucher) {
                    $(".VoucherPrice").text(result.d.data.CartSummary.Voucher.Discount);
                }
                else {
                    $(".VoucherPrice").text(0);
                }

                if (result.d.data.CartSummary.Notes)
                    $("#Notes").val(result.d.data.CartSummary.Notes);

                if (result.d.data.CartSummary.Shipping) {
                    $(".ShippingPrice").text(result.d.data.CartSummary.Shipping.Price);
                }
                else {
                    $(".ShippingPrice").text(0);
                }

                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);



                $(".TotalPrice").text(result.d.data.CartSummary.TotalPrice);
                $(".Subtotal").text(result.d.data.CartSummary.Subtotal);
                $(".TotalQuantity").text(result.d.data.CartSummary.TotalQuantity);
                $(".Shipping").text(result.d.data.CartSummary.Shipping);
                //if (result.d.data.CartSummary.Voucher != undefined) {
                //    $("#form_voucher").hide();
                //    DisplayVoucher(result.d.data.CartSummary.Voucher, "");
                //}
                //else {
                //    $(".Voucher").text(0);
                //}
                $(".format-money").formatCurrency({
                    region: "id-ID"
                })
            }

            if (result.d.data.CartSummary != null || result.d.data.CartSummary != undefined) {
                //LoadCart(result.d.data.CartSummary.Product);
                LoadCartList(result.d.data.CartSummary);
            }

            $(".format-money").formatCurrency({
                region: "id-ID"
            })
        }
        else {
            bootbox.alert(result.d.message, function (e) {
                window.location = '/Authentication?back=Address';
            });
        }


    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'Addresses', 'CartSummary', 'Payment']
        }
    });
}

function PreloadShipping()
{
    REST.onSuccess = function (result) {
        if (result.d.success) {
            if (result.d.data.Shipping) {
                var data = result.d.data.Shipping;
                var shipping = "";
                for (var i = 0; i < data.length; i++) {
                    if (data[i].IDCarrier != 8) {
                        if (i == 0)
                            shipping += '<tr><td><input class="css-checkbox" checked="checked" id="Ship' + data[i].IDShipping + '" name="Shipping" data-idshipping="' + data[i].IDShipping + '" data-idcarrier="' + data[i].IDCarrier + '" type="radio" /></td><td class="text-center"><label class="text-center" for="Ship' + data[i].IDShipping + '"><img class="img-carrier" src="./assets/images/carrier/' + data[i].ImageShipping + '" /></label></td><td>' + data[i].Name + '</td><td>' + data[i].Information + '</td><td class="format-money">' + data[i].TotalPrice + ' IDR</td></tr>';
                        else
                            shipping += '<tr><td><input class="css-checkbox" id="Ship' + data[i].IDShipping + '" name="Shipping" data-idshipping="' + data[i].IDShipping + '" data-idcarrier="' + data[i].IDCarrier + '" type="radio" /></td><td class="text-center"><label class="text-center" for="Ship' + data[i].IDShipping + '"><img class="img-carrier" src="./assets/images/carrier/' + data[i].ImageShipping + '" /></label></td><td>' + data[i].Name + '</td><td>' + data[i].Information + '</td><td class="format-money">' + data[i].TotalPrice + ' IDR</td></tr>';
                    }
                }
                $("#table_shipping tbody").html(shipping);

                $('input[type=radio][name=Shipping]').change(function (e) {
                    e.preventDefault();
                    if ($("[name=Shipping]:checked").data("idshipping") != null || $("[name=Shipping]:checked").data("idshipping") != undefined) {
                        SubmitShipping(+$("[name=Shipping]:checked").data("idshipping"));
                    }
                });

                $(".format-money").formatCurrency({
                    region: "id-ID"
                })
            }
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Shipping']
        }
    });
}

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
            bootbox.alert(result.d.message, function (e) {
                window.location = '/Summary';
            });
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


function LoadAddress(data) {
    if (data.length >= 1) {
        var option = '';
        var item = '';
        var count = 1;
        for (var i = 0; i < data.length; i++) {
            
            option += '<option value="' + data[i].IDAddress + '">Address ' + count + '</option>';
            count++;
        }
        $("#ddlAddress").html(option);

        $("#ddlAddress2").html(option);
    }
}

function LoadDeliveryAddress(data) {
    var item = "";
    item += '<p>' + data.Address + '</p>';
    item += '<p>' + data.CountryName + '</p>';
    item += '<p>' + data.PostalCode + ' ' + data.CityName + '</p>';
    item += '<p>' + data.DistrictName + '</p>';
    item += '<p>' + data.Phone + '</p>';
    item += '<a href="UpdateAddress.aspx?id=' + data.IDAddress + '&back=Address.aspx">Update Address >></a>';
    $(".delivery-address").html(item);
    $(".update-delivery").attr('data-idaddress', data.IDAddress);
    $(".update-delivery").attr('href', 'update-address.aspx?id=' + data.IDAddress + '&back=address.aspx');

    $("#IDDeliveryAddress").val(data.IDAddress);
}

function LoadBillingAddress(data) {
    var item = "";
    item += '<p>' + data.Address + '</p>';
    item += '<p>' + data.CountryName + '</p>';
    item += '<p>' + data.PostalCode + ' ' + data.CityName + '</p>';
    item += '<p>' + data.DistrictName + '</p>';
    item += '<p>' + data.Phone + '</p>';
    item += '<a href="UpdateAddress.aspx?id=' + data.IDAddress + '&back=Address.aspx">Ganti Alamat >></a>';
    $(".billing-address").html(item);
    $(".update-billing").attr('data-idaddress', data.IDAddress);
    $(".update-billing").attr('href', 'update-address.aspx?id=' + data.IDAddress + '&back=address.aspx');

    $("#IDBillingAddress").val(data.IDAddress);
}

function GetAddressDetail(id, same) {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            if (same) {
                LoadDeliveryAddress(data);
                LoadBillingAddress(data);

                SubmitAddressToCart(+$("#IDDeliveryAddress").val(), +$("#IDBillingAddress").val(), $("#Notes").val());
            }
            else {
                LoadDeliveryAddress(data);
            }
        }
        else
            bootbox.alert(result.d.message);
    };
    REST.sendRequest({
        'c': 'feaddr',
        'm': 'detaddr',
        'data': {
            'IDCustomer': +$("#HiddenIDCustomer").val(),
            'IDAddress': id
        }
    });
}

function GetAddressDetail2(id, same) {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            if (same) {
                LoadDeliveryAddress(data);
                LoadBillingAddress(data);
            }
            else {
                LoadBillingAddress(data);
            }
        }
        else
            bootbox.alert(result.d.message);
    };
    REST.sendRequest({
        'c': 'feaddr',
        'm': 'detaddr',
        'data': {
            'IDCustomer': +$("#HiddenIDCustomer").val(),
            'IDAddress': id
        }
    });
}

function SubmitAddressToCart(idBilling, idDelivery, notes) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            //window.location = 'paymentfinal.aspx';
            PreloadShipping();
            $(".ShippingPrice").text(result.d.data.Shipping.TotalPrice);
            var Subtotal;
            if (result.d.data.Voucher) {
                Subtotal = result.d.data.TotalPrice - result.d.data.Voucher.Discount + result.d.data.Shipping.TotalPrice
            }
            else {
                Subtotal = result.d.data.TotalPrice + result.d.data.Shipping.TotalPrice
            }
            
            $(".Subtotal").text(Subtotal);
            $(".format-money").formatCurrency({
                region: "id-ID"
            })
        }
        else
            bootbox.alert(result.d.message);
    };
    REST.onComplete = function () {
        //if ($("[name=payment]:checked").data("idpayment_method") != null || $("[name=payment]:checked").data("idpayment_method") != undefined) {
        //    SubmitPayment(+$("[name=payment]:checked").data("idpayment_method"));
        //}
    };
    REST.sendRequest({
        'c': 'feaddr',
        'm': 'iaddrtocrt',
        'data': {
            'IDBillingAddress': idBilling,
            'IDDeliveryAddress': idDelivery,
            'Notes': notes
        }
    });
}

function SubmitPayment(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {

        }
        else {
            console.log("ERROR : " + result.d.message);
        }
    };
    REST.onComplete = function (result) {
        
    }
    REST.sendRequest({
        'c': 'fepay',
        'm': 'ipaytocart',
        'data': {
            'IDPayment': id
        }
    });
}

function LoadCartList(data) {
    var total = data.TotalPrice.toString().replace(".", ",");
    var totalQuantity = data.TotalQuantity;
    var subtotal = data.Subtotal.toString().replace(".", ",");
    var shipping = data.Shipping.TotalPrice
    var voucher = 0;
    if (data.Voucher) {
        voucher = data.Voucher.Discount.toString().replace(".", ",");
    }
    data = data.Product;
    var item = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            item += '<tr class="product' + i + '" data-name="' + data[i].ProductName + '" data-idproduct="' + data[i].IDProduct + '" data-reference="' + data[i].ReferenceCode + '" data-category="' + data[i].DefaultCategory + '" data-priceunit="' + data[i].PricePerUnit + '" data-quantity="' + data[i].Quantity + '">';
            item += '<td class="cartmageprd text-center">';
            item += '<a href="ProductDetail.aspx?id=' + data[i].IDProduct + '">';
            item += '<img src="./assets/images/product/' + data[i].Photo + '" width="60" alt="Product Image" />';
            item += '</a>';
            item += '</td>';
            item += '<td>';
            item += '<a class="ProductName" href="./ProdutDetail.aspx?id=' + data[i].IDProduct + '">' + data[i].ProductName + '</a><br />';
            item += '<span class="CombinationName">' + data[i].CombinationName + '</span>';
            item += '</td>';
            item += '<td class="centerBro PriceUnit format-money">' + data[i].PricePerUnit + '</td>';
            item += '<td class="centerBro">';
            item += '<label class="textboxQty centerBro" data-maxquantity="' + data[i].MaxQuantity + '" data-idcombination="' + data[i].IDCombination + '">' + data[i].Quantity + '<label>';
            item += '</td>';
            item += '<td class="rightBro format-money">' + data[i].Price + '</td>';
            item += '</tr>';

            count++;

        }
        $("#cart-list tbody").html(item);
    }

    $(".TotalPrice").text(total + ' IDR');
    $(".Subtotal").text(subtotal + ' IDR');
    $(".TotalQuantity").text(totalQuantity);
    $(".ShippingPrice").text(shipping + ' IDR');
    $(".Voucher").text(voucher + ' IDR');

    $(".format-money").formatCurrency({
        region: "id-ID"
    })

    $(".delete-cart").click(function (e) {
        e.preventDefault();
        DeleteCart(+$(this).data("idcombination"), this);
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

function SubmitOrder() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            

            ga('ecommerce:addTransaction', {
                'id': result.d.data,                     // Transaction ID. Required.
                'affiliation': 'NIION Indonesia',          // Affiliation or store name.
                'revenue': $(".Subtotal").text().replace(" IDR", ""),               // Grand Total.
                'shipping': $(".TotalShipping").text().replace(" IDR", ""),                  // Shipping.
                'tax': '0',                     // Tax.
                'currency': 'IDR'
            });

            for (var i = 0; i < count; i++) {

                ga('ecommerce:addItem', {
                    'id': $(".product" + i).data("idproduct"),                     // Transaction ID. Required.
                    'name': $(".product" + i).data("name"),                       // Product name. Required.
                    'sku': $(".product" + i).data("reference"),                  // SKU/code.
                    'category': $(".product" + i).data("category"),               // Category or variation.
                    'price': $(".product" + i).data("priceunit"),                 // Unit price.
                    'quantity': $(".product" + i).data("quantity")                // Quantity.
                });
            }

            ga('ecommerce:send');

            window.location = "/ThankYou";
        }
        else
            //bootbox.alert(result.d.message);
            bootbox.alert(result.d.message, function () {
                location.reload();
            });
    };
    REST.sendRequest({
        'c': 'feorder',
        'm': 'iorder',
        'data': {}
    });
}

function testSubmit()
{
    ga('ecommerce:addTransaction', {
        'id': 01,                     // Transaction ID. Required.
        'affiliation': 'NIION Indonesia',          // Affiliation or store name.
        'revenue': $(".Subtotal").text().replace(" IDR", ""),               // Grand Total.
        'shipping': $(".TotalShipping").text().replace(" IDR", ""),                  // Shipping.
        'tax': '0',                     // Tax.
        'currency': 'IDR'
    });

    for (var i = 0; i < count; i++) {

        ga('ecommerce:addItem', {
            'id': $(".product" + i).data("idproduct"),                     // Transaction ID. Required.
            'name': $(".product" + i).data("name"),                       // Product name. Required.
            'sku': $(".product" + i).data("reference"),                  // SKU/code.
            'category': $(".product" + i).data("category"),               // Category or variation.
            'price': $(".product" + i).data("priceunit"),                 // Unit price.
            'quantity': $(".product" + i).data("quantity")                // Quantity.
        });
    }

    ga('ecommerce:send');
}

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

function SubmitShipping(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            RefreshCart();
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

function RefreshCart()
{
    REST.onSuccess = function (result) {
        if (result.d.success) {
            if (result.d.data) {
                var CartSummary = result.d.data.CartSummary;
                if (CartSummary.Voucher) {
                    $(".VoucherPrice").text(CartSummary.Voucher.Discount);
                }
                else {
                    $(".VoucherPrice").text(0);
                }

                if (result.d.data.Shipping) {
                    $(".ShippingPrice").text(result.d.data.Shipping.Price);
                }
                else {
                    $(".ShippingPrice").text(0);
                }

                var listCartSummary = CartSummary.Product;
                var TotalPrice = CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);



                $(".TotalPrice").text(CartSummary.TotalPrice);
                $(".Subtotal").text(CartSummary.Subtotal);
                $(".TotalQuantity").text(CartSummary.TotalQuantity);
                $(".ShippingPrice").text(CartSummary.Shipping.TotalPrice);
                //if (result.d.data.CartSummary.Voucher != undefined) {
                //    $("#form_voucher").hide();
                //    DisplayVoucher(result.d.data.CartSummary.Voucher, "");
                //}
                //else {
                //    $(".Voucher").text(0);
                //}
                $(".format-money").formatCurrency({
                    region: "id-ID"
                })
            }
        }
        else
            console.log("ERROR : " + result.d.message);
    };

    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['CartSummary']
        }
    });

}

//function DisplayVoucher(data, action) {

//    if (action == 'update') {
//        result = totalPrice - potongan;
//        REST.onSuccess = function (result) {
//            if (result.d.success) {
//                $(".Voucher").text(data.Discount);
//                $(".Subtotal").text(result.d.data.Subtotal);
//            }
//        };
//        REST.sendRequest({
//            'c': 'fesum',
//            'm': 'uprc',
//            'data': { 'Price': result }
//        });
//    }
//}