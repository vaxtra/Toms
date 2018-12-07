var format = "";
var SubtotalVoucher = "";
$(document).ready(function () {
    PreloadMaster();

    $("#form_voucher").submit(function (e) {
        e.preventDefault();
        if ($("input[name=Voucher]").val()) {
            SubmitVoucher($("input[name=Voucher]").val());
            $("input[name=Voucher]").val("");
        }
        else
            bootbox.alert("Please type your voucher");
    });

    $(".btn-next-summary").click(function (e) {
        e.preventDefault();
        window.location = 'address.aspx';
    });
    $(".ProductUrl").hide();
    $(".ProductCancel").click(function () {
        $(".overlayPromo").fadeOut(200);
    })
    $(".laycontPromo .fa-times").click(function () {
        $(".overlayPromo").fadeOut(200);
    })
});

function SubmitVoucher(code) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            $(".error").html("");
            //DisplayVoucher(result.d.data, "update");
            location.reload();
        }
        else {
            bootbox.alert(result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'fesum',
        'm': 'vo',
        'data': {
            'Code': code,
            'Token': $("#token").val(),
            'TotalAmount': +SubtotalVoucher
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

            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }
            if (result.d.data.CartSummary) {
                $(".TotalPrice").text(result.d.data.CartSummary.TotalPrice);
                $(".TotalQuantity").text(result.d.data.CartSummary.TotalQuantity);
                $(".Subtotal").text(result.d.data.CartSummary.Subtotal);
                if (result.d.data.CartSummary.Shipping) {
                    $(".Shipping").text(result.d.data.CartSummary.Shipping.Price);
                }
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
            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);
                SubtotalVoucher = TotalPrice;
            }
            if (result.d.data.CartSummary != undefined) {
                LoadCart(result.d.data.CartSummary);
                LoadCartList(result.d.data.CartSummary);
                if (result.d.data.CartSummary.Product != 0) {
                    CheckPromo(result.d.data.CartSummary.Product);
                }

                if (result.d.data.CartSummary.Voucher != undefined) {
                    $("#form_voucher").hide();
                    DisplayVoucher(result.d.data.CartSummary.Voucher, "");
                }
                else {
                    $(".Voucher").text(0);
                }
            }
            else {
                $(".btn-next-summary").remove();
                $("input[name=Voucher]").attr("disabled", true);
                $(".btn-voucher").attr("disabled", true);
            }

            if (result.d.data.CartPromo) {
                bootbox.alert("You have choosen to get a promo, please complete your order first", function (e) {
                    window.location = 'Address.aspx';
                });
            }

            var ExpiredNotification = result.d.data.ExpiredNotification;
            if (ExpiredNotification) {
                LoadNotification(ExpiredNotification);
            }

            $(".format-money").formatCurrency({
                region: format
            })
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

function LoadCart(data) {
    var cartData = data.Product;
    if (cartData != undefined && cartData.length > 0) {
        var item = '';
        for (var i = 0; i < cartData.length; i++) {
            item += '<tr>';
            item += '<td class="cartmageprd text-center">';
            item += '<a href="ProductDetail.aspx?id=' + cartData[i].IDProduct + '">';
            item += '<img src="./assets/images/product/' + cartData[i].Photo + '" width="60" alt="Product Image" />';
            item += '</a>';
            item += '</td>';
            item += '<td>';
            item += '<a class="ProductName" href="./ProductDetail.aspx?id=' + cartData[i].IDProduct + '">' + cartData[i].ProductName + '</a><br />';
            item += '<span class="CombinationName">' + cartData[i].CombinationName + '</span>';
            item += '</td>';
            item += '<td class="centerBro PriceUnit format-money">' + cartData[i].PricePerUnit + '</td>';
            item += '<td class="centerBro">';
            if (cartData[i].IsFixed) {
                item += '<input class="textboxQty centerBro" disabled type="text" data-maxquantity="' + cartData[i].MaxQuantity + '" data-idcombination="' + cartData[i].IDCombination + '" value="' + cartData[i].Quantity + '" /></form>';
            }
            else {
                item += '<div class="summary-qtyDown" data-idcombination="' + cartData[i].IDCombination + '">-</div>';
                item += '<input class="textboxQty centerBro" type="text" data-maxquantity="' + cartData[i].MaxQuantity + '" data-idcombination="' + cartData[i].IDCombination + '" value="' + cartData[i].Quantity + '" /></form>';
                item += '<div class="summary-qtyUp" data-idcombination="' + cartData[i].IDCombination + '">+</div>';
            }
            item += '</td>';
            item += '<td class="rightBro"><span class="format-money">' + cartData[i].Price + '</span>';
            if (!cartData[i].IsFixed) {
                item += '<a href="#" class="summary-delete" data-idcombination="' + cartData[i].IDCombination + '"><i class="fa fa-times"></i></a></td>';
            }
            item += '</tr>';

        }
    }

    $("#cart-list tbody").html(item);

    $(".summary-qtyUp").click(function (e) {
        e.preventDefault();
        var valNow = parseInt($(".textboxQty[data-idcombination=" + $(this).data("idcombination") + "]").val());
        var max = +$(".textboxQty[data-idcombination=" + $(this).data("idcombination") + "]").data("maxquantity");
        console.log(max);
        if (valNow >= max)
            bootbox.alert("Maximum quantity for this product is " + max);
        else if (isNaN(valNow))
            bootbox.alert("Please check your quantity");
        else {
            valNow += 1;
            var pricePerUnit = +$(".PricePerUnit[data-idcombination=" + $(this).data("idcombination") + "]").text();
            var totalPrice = pricePerUnit * valNow;
            $(".textboxQty[data-idcombination=" + $(this).data("idcombination") + "]").val(valNow);
            $(".TotalPricePerProduct[data-idcombination=" + $(this).data("idcombination") + "]").text(totalPrice);

            var subtotal;
            $(".TotalPricePerProduct").each(function (index, element) {
                subtotal += +$(this).text();
            });
            $(".TotalPrice").text(subtotal);

            var qty = 0;
            $(".textboxQty").each(function (index, element) {
                qty += +$(this).val();
            });
            $(".TotalQuantity").text(qty);
            UpdateQuantity(+$(this).data("idcombination"), valNow);
        }
    });

    $(".summary-qtyDown").click(function (e) {
        e.preventDefault();
        var valNow = parseInt($(".textboxQty[data-idcombination=" + $(this).data("idcombination") + "]").val());
        if (valNow <= 1)
            bootbox.alert("Quantity cannot less then 1");
        else if (isNaN(valNow))
            bootbox.alert("Please check your quantity");
        else {
            valNow -= 1;
            var pricePerUnit = +$(".PricePerUnit[data-idcombination=" + $(this).data("idcombination") + "]").text();
            var totalPrice = pricePerUnit * valNow;
            $(".textboxQty[data-idcombination=" + $(this).data("idcombination") + "]").val(valNow);
            $(".TotalPricePerProduct[data-idcombination=" + $(this).data("idcombination") + "]").text(totalPrice);

            var subtotal;
            $(".TotalPricePerProduct").each(function (index, element) {
                subtotal += +$(this).text();
            });
            $(".TotalPrice").text(subtotal);

            var qty = 0;
            $(".textboxQty").each(function (index, element) {
                qty += +$(this).val();
            });
            $(".TotalQuantity").text(qty);
            UpdateQuantity(+$(this).data("idcombination"), valNow);
        }
    });

    $(".summary-delete").click(function (e) {
        e.preventDefault();
        DeleteProduct(+$(this).data("idcombination"));
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

function LoadCartList(data) {
    var total = data.TotalPrice;
    var totalQuantity = data.TotalQuantity;
    var subotal = data.Subtotal;
    if (data.Voucher) {
        var Discount = data.Voucher.Discount;
    }
    //data = data.Product;
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
    $(".Subtotal").text(subotal + ' IDR');
    if (Discount) {
        $(".Voucher").text(Discount + ' IDR');
    }
    $(".TotalQuantity").text(totalQuantity);

    $(".format-money").formatCurrency({
        region: format
    })

    $(".delete-cart").click(function (e) {
        e.preventDefault();
        DeleteCart(+$(this).data("idcombination"), this);
    });
}

function DeleteProduct(idCombination) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            $(".error").html("");
            if (result.d.message != "OK") {
                bootbox.alert(result.d.message, function (result) {
                    location.reload();
                });
            }
            else
            {
                location.reload();
            }
        }
        else {
            bootbox.alert(result.d.message, function (result) {
                location.reload();
            });
            $(".error").html(result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'fesum',
        'm': 'dpro',
        'data': { 'IDCombination': idCombination }
    });
}

function UpdateQuantity(idCombination, qty) {
    REST.onComplete = function (data) {
        var result = data.responseJSON
        if (data.success) {
            PreloadMaster();
            //if (result.d.data.token != undefined) {
            //    $("#token").val(result.d.data.token);
            //}
            //if (result.d.data) {
            //    $(".TotalPrice").text(result.d.data.TotalPrice);
            //    $(".TotalQuantity").text(result.d.data.TotalQuantity);
            //    $(".Subtotal").text(result.d.data.Subtotal);
            //}

            //if (result.d.data != undefined) {
            //    LoadCart(result.d.data.Product);
            //    if (result.d.data.Voucher != undefined) {
            //        DisplayVoucher(result.d.data.Voucher, "");
            //    }
            //}
            //else {
            //    $(".btn-next-summary").remove();
            //    $("input[name=Voucher]").attr("disabled", true);
            //    $(".btn-voucher").attr("disabled", true);
            //}
        }
        else {
            bootbox.alert(result.d.message);
            PreloadMaster();
        }
    };
    REST.onSuccess = function (result) { };
    REST.sendRequest({
        'c': 'fesum',
        'm': 'uqty',
        'data': {
            'IDCombination': idCombination,
            'Quantity': qty
        }
    });
}

function CheckPromo(catData) {
    var countQty = 0;
    var amount = 0;
    var idKat = catData[0].IDCategory;
    var compareKat = 0;
    if (catData != undefined && catData.length > 0) {
        for (var i = 0; i < catData.length; i++) {
            compareKat = catData[i].IDCategory;
            if (idKat == compareKat) {
                countQty += catData[i].Quantity;
                amount += catData[i].Price;
            }
        }
    }

    $.ajax({
        url: "/modules/BuyGetFree/BuyGet.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "BuyGet",
            t: "fe",
            m: "checkpromo",
            data: {
                IDKategori: idKat,
                qty: countQty,
                amount: amount
            }
        }),
        success: function (result) {
            if (result.success) {
                if (result.data.data) {
                    $("#formVoucher").hide();
                    $(".PromoText").show();
                    $(".PromoText").text(result.data.message);
                    $(".ProductUrl").fadeIn(200);
                    $(".ProductUrl").attr("href", "./ProductPromo.aspx?id=" + idKat + "&pr=" + result.data.data);
                    $(".overlayPromo").fadeIn(200);
                }
                else {
                    $("#formVoucher").show();
                    $(".PromoText").hide();
                    $(".ProductUrl").hide();
                    $(".overlayPromo").fadeOut(200);
                }
            }
        },
        error: function (result) {
            $(".error").html(result.message);
        }
    });

    //REST.onSuccess = function (result) {
    //    if (result.d.success) {
    //        if (result.d.data) {
    //            $(".PromoText").show();
    //            $(".PromoText").text(result.d.message);
    //            $(".ProductUrl").fadeIn(200);
    //            $(".ProductUrl").attr("href", "./ProductPromo.aspx?id=" + idKat + "&pr=" + result.d.data);
    //            $(".overlayPromo").fadeIn(200);
    //        }
    //        else {
    //            $(".PromoText").hide();
    //            $(".ProductUrl").hide();
    //            $(".overlayPromo").fadeOut(200);
    //        }
    //    }
    //    else {
    //        $(".error").html(result.d.message);
    //    }
    //};
    //REST.sendRequest({
    //    'c': 'feprom',
    //    'm': 'checkpromo',
    //    'data':
    //        {
    //            'IDKategori': idKat,
    //            'qty': countQty,
    //            'amount': amount
    //        }
    //});
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

