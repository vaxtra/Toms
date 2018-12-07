var format = "";

$(document).ready(function () {
    PreloadMaster();

    $(".choice .optionspoint").click(function () {
        $(".activepoint").removeClass("activepoint");
        $(this).addClass("activepoint");

        if ($(".dcr").hasClass("activepoint")) {
            $(".usingpoint-wrap").removeClass("hidden");
        }
        else {
            $(".usingpoint-wrap").addClass("hidden");
        }

    });

    $(".placeorderdet").click(function (e) {
        e.preventDefault();
        //if ($("#HiddenActivePromo").val() != "false") {
            
        //    SubmitOrderPromo();
        //}
        //else if ($("#HiddenMember").val() != "") {
        //    if ($("#HiddenMember").val() == "false") {
        //        SubmitOrderMember("regular", false, 0);
        //    }
        //    else {
        //        if ($(".inc").hasClass("activepoint")) {
        //            SubmitOrderMember("regular", true, 0);
        //        }
        //        else {
        //            if ($(".inputpoint").val() != "" && $(".inputpoint").val() != undefined && $(".inputpoint").val() != null) {
        //                SubmitOrderMember("discount", true, $(".inputpoint").val());
        //            }
        //            else {
        //                bootbox.alert("Please input how many point you want to use");
        //            }
        //        }
                
        //    }
            
        //}
        //else {
            //console.log("Promo Bro")
            SubmitOrder();
        //}
        
    });
})


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

            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }
            if (result.d.data.CartSummary) {
                $(".TotalPrice").text(result.d.data.CartSummary.TotalPrice);
                $(".TotalQuantity").text(result.d.data.CartSummary.TotalQuantity);
                $(".Subtotal").text(result.d.data.CartSummary.Subtotal);
                $(".Shipping").text(result.d.data.CartSummary.Shipping.Price);
                $(".TotalShipping").text(result.d.data.CartSummary.Shipping.TotalPrice);
                LoadBillingAddress(result.d.data.CartSummary.BillingAddress);
                LoadDeliveryAddress(result.d.data.CartSummary.DeliveryAddress);
                $(".ShippingName").text(result.d.data.CartSummary.Shipping.Name);
                $(".ShippingInfo").text(result.d.data.CartSummary.Shipping.Information);
                $(".ShippingPrice").text(result.d.data.CartSummary.Shipping.Price);
                $(".imageShipping").attr("src", "./assets/images/carrier/" + result.d.data.CartSummary.Shipping.Image);
                if (result.d.data.CartSummary.ActivePromo != undefined) {
                    $("#HiddenActivePromo").val(result.d.data.CartSummary.ActivePromo);
                }
                else {
                    $("#HiddenActivePromo").val(false);
                }
            }
            else {
                window.location = "Summary.aspx";
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

            if (result.d.data.CartSummary != undefined) {
                LoadCartList(result.d.data.CartSummary);
                if (result.d.data.CartSummary.Voucher != undefined) {
                    $("#form_voucher").hide();
                    DisplayVoucher(result.d.data.CartSummary.Voucher, "");
                }
                else {
                    $(".Voucher").text(0);
                }

                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);

            }
            else {
                $(".btn-next-summary").remove();
                $("input[name=Voucher]").attr("disabled", true);
                $(".btn-voucher").attr("disabled", true);
            }

            //LOAD PAYMENT
            var payment = result.d.data.CartSummary.PaymentMethod;
            if (payment) {
                $("#ImagePayment").attr("src", "/assets/images/Payment_Method/" + payment.Image);
                $("#DetailPayment").html(payment.AccountNumber + ' - a.n ' + payment.Owner);
            }

            var ExpiredNotification = result.d.data.ExpiredNotification;
            if (ExpiredNotification) {
                LoadNotification(ExpiredNotification);
            }

            $(".format-money").formatCurrency({
                region: format
            })

            $.ajax({
                url: "/modules/MemberPoint/MemberPoint.ashx",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    c: "MemberPoint",
                    t: "fe",
                    m: "checkmember",
                    data: {
                        IDCustomerGroup: Customer.IDCustomer_Group,
                        IDCustomer: Customer.IDCustomer
                    }
                }),
                beforeSend: function () {
                    Metronic.blockUI({
                        boxed: true
                    });
                },
                success: function (result) {
                    if (result.success) {
                        if (result.data.success) {
                            $(".memberinfo").removeClass("hidden");
                            var info = '';
                            info += result.data.message + ', you have ' + result.data.data.Customer.Point + ' Poin now';
                            $(".infoMsg").text(info);
                            $("#HiddenMember").val(true);
                            $(".custPoint").text(result.data.data.Customer.Point);
                        }
                        else {
                            $("#HiddenMember").val(false);
                        }
                    }
                },
                complete: function () {
                    Metronic.unblockUI();
                },
                error: function (result) {
                    $(".error").html(result.message);
                }
            });

            
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['CartSummary', 'Customer', 'Currency', 'ExpiredNotification']
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

function DisplayVoucher(data, action) {

    if (action == 'update') {
        result = totalPrice - potongan;
        REST.onSuccess = function (result) {
            if (result.d.success) {
                $(".Voucher").text(data.Discount);
                $(".Subtotal").text(result.d.data.Subtotal);
            }
        };
        REST.sendRequest({
            'c': 'fesum',
            'm': 'uprc',
            'data': { 'Price': result }
        });
    }
}

function LoadCartList(data) {
    var total = data.TotalPrice;
    var totalQuantity = data.TotalQuantity;
    var subtotal = data.Subtotal;
    var shipping = data.Shipping.Price
    var voucher = 0;
    if (data.Voucher) {
        voucher = data.Voucher.Discount;
    }
    data = data.Product;
    var item = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            item += '<tr>';
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

        }
        $("#cart-list tbody").html(item);
    }

    $(".TotalPrice").text(total + ' IDR');
    $(".Subtotal").text(subtotal + ' IDR');
    $(".TotalQuantity").text(totalQuantity);
    $(".Shipping").text(shipping + ' IDR');
    $(".Voucher").text(voucher + ' IDR');

    $(".format-money").formatCurrency({
        region: format
    })

    $(".delete-cart").click(function (e) {
        e.preventDefault();
        DeleteCart(+$(this).data("idcombination"), this);
    });
}

function LoadDeliveryAddress(data) {
    var item = "";
    item += '<h5><span>' + data.Address + '</span></h5>';
    item += '<p><span>' + data.CountryName + '<span></p>';
    item += '<p>' + data.PostalCode + ' ' + data.CityName + '</p>';
    item += '<p>' + data.DistrictName + '</p>';
    item += '<p>' + data.Phone + '</p>';

    $(".delivery-address").html(item);


    $("#IDDeliveryAddress").val(data.IDAddress);
}

function LoadBillingAddress(data) {
    var item = "";
    item += '<h5><span>' + data.Address + '</span></h5>';
    item += '<p><span>' + data.CountryName + '<span></p>';
    item += '<p>' + data.PostalCode + ' ' + data.CityName + '</p>';
    item += '<p>' + data.DistrictName + '</p>';
    item += '<p>' + data.Phone + '</p>';
    $(".billing-address").html(item);

    $("#IDBillingAddress").val(data.IDAddress);
}

function SubmitOrder() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            ga('ecommerce:addTransaction', {
                'id': result.data.data,                     // Transaction ID. Required.
                'affiliation': 'NIION Indonesia',          // Affiliation or store name.
                'revenue': $(".Subtotal").text().replace(" IDR", ""),               // Grand Total.
                'shipping': $(".TotalShipping").text().replace(" IDR", ""),                  // Shipping.
                'tax': '0'                     // Tax.
            });

            for (var i = 0; i < count; i++) {


                ga('ecommerce:addItem', {
                    'id': $(".product" + count).data("idproduct"),                     // Transaction ID. Required.
                    'name': $(".product" + count).data("name"),                       // Product name. Required.
                    'sku': $(".product" + count).data("reference"),                  // SKU/code.
                    'category': $(".product" + count).data("category"),               // Category or variation.
                    'price': $(".product" + count).data("priceunit"),                 // Unit price.
                    'quantity': $(".product" + count).data("quantity")                // Quantity.
                });
            }

            window.location = "thank-you.aspx";
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

function LoadCartPromo(data) {
    var cartData = data.Product;
    if (cartData != undefined && cartData.length > 0) {
        var item = '';

        for (var i = 0; i < cartData.length; i++) {
            item += '<tr>';
            item += '<td class="cartmageprd">';
            item += '<a href="ProductDetail.aspx?id=' + cartData[i].IDProduct + '">';
            item += '<img src="./assets/images/product/' + cartData[i].Photo + '" width="100" height="75" alt="Product Image" />';
            item += '</a>';
            item += '</td>';
            item += '<td>';
            item += '<a class="ProductName" href="./ProdutDetail.aspx?id=' + cartData[i].IDProduct + '">' + cartData[i].ProductName + '</a><br />';
            item += '<span class="CombinationName">' + cartData[i].CombinationName + '</span>';
            item += '</td>';
            item += '<td class="centerBro">';
            item += '<label class="textboxQty centerBro" data-maxquantity="' + cartData[i].MaxQuantity + '" data-idcombination="' + cartData[i].IDCombination + '">' + cartData[i].Quantity + '<label>';
            item += '</td>';
            item += '</tr>';

        }
    }
    else {
        $(".promoProduct").hide();
    }

    $("#cartPromo-list tbody").html(item);
    $(".promoProduct").show();
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

function SubmitOrderPromo()
{
    $.ajax({
        url: "/modules/BuyGetFree/BuyGet.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "BuyGet",
            t: "fe",
            m: "submitorderpromo",
            data: { }
        }),
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true
            });
        },
        success: function (result) {
            if (result.success) {
                if (result.data.success) {
                    window.location = "thank-you.aspx";
                }
                else {
                    //bootbox.alert(result.d.message);
                    bootbox.alert(result.d.message, function () {
                        location.reload();
                    });
                }
            }
        },
        complete: function () {
            Metronic.unblockUI();
        },
        error: function (result) {
            $(".error").html(result.message);
        }
    });
}

function SubmitOrderMember(typeTrans, member, point) {

    $.ajax({
        url: "/modules/MemberPoint/MemberPoint.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "MemberPoint",
            t: "fe",
            m: "submitordermember",
            data: {
                Member: member,
                TypeTransaction: typeTrans,
                Point: point
            }
        }),
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true
            });
        },
        success: function (result) {
            if (result.success) {
                if (result.data.success) {
                    window.location = "thank-you.aspx";
                }
                else {
                    //bootbox.alert(result.d.message);
                    bootbox.alert(result.d.message, function () {
                        location.reload();
                    });
                }
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