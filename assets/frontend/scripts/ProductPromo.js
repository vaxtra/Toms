var REST = new OF({
    'url': '/api/services.asmx/request'
});
$(document).ready(function () {
    if (queryString("id")) {
        $("#HiddenIDCategoryPromo").val(queryString("id"));
    }
    else {
        window.location = "Summary.aspx";
    }

    if (queryString("pr")) {
        $("#HiddenIDPromo").val(queryString("pr"));
    }
    else {
        window.location = "Summary.aspx";
    }

    var arr = [];
    var arrPromoItems = [];

    $("#SubmitPromo").click(function () {
        $(".addedcart").each(function () {
            console.log('aa');
            var idProduct = $(this).data("idproduct");
            var qty = 0;
            var idcombination = 0;
            var price = 0;
            var combinationName = 0;
            var productName = 0;

            var item = {};

            $(".qty").each(function () {
                if ($(this).data("idproduct") == idProduct) {
                    qty = $(this).val();
                }
            })
            $(".sizeadded").each(function () {
                if ($(this).data("idproduct") == idProduct) {
                    idcombination = $(this).data("idcombination");
                    price = $(this).data("price");
                    combinationName = $(this).data("combinationname");
                }
            })
            $(".productName").each(function () {
                if ($(this).data("idproduct") == idProduct) {
                    productName = $(this).text();
                }
            })
            arr.push([idProduct, idcombination, qty, price, combinationName, productName, $(this).data("promoname")]);
            item = {
                'idProduct': idProduct, 'idcombination': idcombination, 'qty': qty, 'price': price, 'combinationName': combinationName, 'productName': productName, 'promoName': $(this).data("promoname")
            };

            arrPromoItems.push(item);
            //addFreeProduct(idProduct, idcombination, qty, price, combinationName, productName);
        });

        for (var i = 0; i < arr.length; i++) {
            //addFreeProduct(arr[i][0], arr[i][1], arr[i][2], arr[i][3], arr[i][4], arr[i][5]);
        }
        addFreeProduct(arrPromoItems);
    });

    Preload();
});

function Preload() {
    var produks;
    var promo;
    REST.onSuccess = function (result) {
        if (result.d.success) {

            if (result.d.data.CartSummary != null) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);
                var jumlahProductCart = result.d.data.CartSummary.TotalQuantity;
                $('.TotalQuantity').text(jumlahProductCart);

            }
            if (result.d.data.CartSummary)
                LoadCartList(result.d.data.CartSummary);

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
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['CartSummary', 'Customer'],
            'IDCategory': +$("#HiddenIDCategoryPromo").val()
        }
    });

    $.ajax({
        url: "/modules/BuyGetFree/BuyGet.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "BuyGet",
            t: "fe",
            m: "listprodprom",
            data: {
                IDCategory: +$("#HiddenIDCategoryPromo").val()
            }
        }),
        success: function (result1) {
            if (result1.success) {
                produks = result1.data.ListProduct;
                promo = result1.data.Promo;
                LoadProduct(produks, promo);
            }
        },
        error: function (result) {
            $(".error").html(result.message);
        }
    });
}

function LoadProduct(data, dataprom) {
    var item = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {

            item += '<div class="col-md-3 singlepromo">';
            item += '<img src="assets/images/product/' + data[i].Photo + '" class="img-full" />';
            item += '<div class="desc-prodlist">';
            item += '<div class="col-md-12">';
            item += '<label class="productName" data-idproduct="' + data[i].IDProduct + '">' + data[i].Name + '</label>';
            item += '<span class="format-money">' + data[i].Price + '</span>';
            item += '</div>';
            item += '<div class="qtybung nopadding">';
            item += '<input type="text" class="form-control qty" data-idproduct="' + data[i].IDProduct + '" value="1" />';
            item += '</div>';
            item += '</div>';
            item += '<div class="col-md-12 nopadding">';
            item += '<div id="SelectSize" style="margin: auto; display: table;">';
            for (var y = 0; y < data[i].Size.length; y++) {
                item += '<div data-idproduct="' + data[i].Size[y].IDProduct + '" data-quantity="' + data[i].Size[y].Quantity + '" data-price="' + data[i].Size[y].Price + '" data-combinationname="' + data[i].Size[y].CombinationName + '" data-idcombination="' + data[i].Size[y].IDProduct_Combination + '" data-idvalue="' + data[i].Size[y].IDValue + '" class="sizing">' + data[i].Size[y].Name + '</div>'
            }
            //item += '<div class="sizing">S</div>';
            //item += '<div class="sizing">M</div>';
            //item += '<div class="sizing">L</div>';
            //item += '<div class="sizing">XL</div>';
            item += '</div>';
            item += '</div>';
            item += '<div class="col-md-12 text-center margtotop">';
            item += '<a href="#" class="btn btn-erigo site-button-dark add-to-cart" data-idproduct="' + data[i].IDProduct + '" data-promoname="' + dataprom.Name + '">ADD TO BONUS</a>';
            item += '</div>';
            item += '</div>';
            item += '</div>';
        }
    }
    else {
        item += '<h5 style="text-align:center;">No Product in this Category</h5>';
    }
    $("#ProductList").html(item);

    var jumlah = 0;

    $(".format-money").formatCurrency({
        region: "id-ID"
    })

    $(".sizing").click(function () {
        if (!$(this).siblings().hasClass("sizeadded")) {
            $(".sizeactive").removeClass("sizeactive");
            $(this).addClass("sizeactive");
        }
    })

    $(".add-to-cart").mouseenter(function () {
        if ($(this).hasClass("addedcart")) {
            $(this).text("CANCEL");
        }
    });

    $(".add-to-cart").mouseleave(function () {
        if ($(this).hasClass("addedcart")) {
            $(this).text("ADDED");
        }
    });

    $('.add-to-cart').on('click', function (e) {
        e.preventDefault();
        var idProds = $(this).data("idproduct");
        $(".qty").each(function () {
            if ($(this).data("idproduct") == idProds) {
                $(".qtyactive").removeClass("qtyactive");
                $(this).addClass("qtyactive");
                $(".qtyactive").attr("disabled", "disabled");
            }
        })


        if ($(this).hasClass("addedcart")) {
            $(".qtyactive").removeAttr("disabled");
            $(".qtyactive").removeClass("qtyactive");
            $(".sizing").each(function () {
                if ($(this).hasClass("sizeadded") && $(this).data("idproduct") == idProds) {
                    $(this).removeClass("sizeadded");
                    $(this).removeClass("sizeactive");
                }
            })
            $(".qty").each(function () {
                if ($(this).data("idproduct") == idProds) {
                    jumlah -= +$(this).val();
                    console.log(jumlah);
                }
            })
            $(this).text("ADD TO BONUS");
            $(this).removeClass("addedcart");
            if ($(".addedcart").length == dataprom.PromoQty) {
                $("#SubmitPromo").show();
                $("#SubmitPromo").addClass("MunculNengah")
            }
            else {
                $("#SubmitPromo").hide();
                $("#SubmitPromo").removeClass("MunculNengah")
            }
        }
        else {
            if (!$(".qty.qtyactive").val()) {
                //$(".qty").after('<span for="Password" class="help-block">This field is required.</span>');
                bootbox.alert("please type quantity product");
                $(".qtyactive").removeAttr("disabled");
            }
            else if ($(".sizing.sizeactive").data("idvalue") == undefined || $(".sizing.sizeactive").data("idvalue") == null || $(this).data("idproduct") != $(".sizing.sizeactive").data("idproduct")) {
                bootbox.alert("please choose the size");
                $(".qtyactive").removeAttr("disabled");
            }
            else if ($(".sizing.sizeactive").data("quantity") <= 0) {
                bootbox.alert("Out of stock");
                $(".qtyactive").removeAttr("disabled");
            }
            else if (+$(".qty.qtyactive").val() > +$(".sizing.sizeactive").data("quantity") || +$(".qty.qtyactive").val() <= 0) {
                bootbox.alert('Quantity must greater than 0 and less than ' + $(".sizing.sizeactive").data("quantity"));
                $(".qtyactive").removeAttr("disabled");
            }
            else {
                if (jumlah + +$(".qtyactive").val() <= dataprom.PromoQty) {
                    $(".qty").each(function () {
                        if ($(this).data("idproduct") == idProds) {
                            jumlah += +$(this).val();
                            console.log(jumlah);
                        }
                    })
                    $(this).addClass("addedcart");
                    $(this).text("ADDED");
                    $(".sizing.sizeactive").addClass("sizeadded");
                    if (jumlah == dataprom.PromoQty) {
                        $("#SubmitPromo").show();
                        $("#SubmitPromo").addClass("MunculNengah")
                    }
                    else {
                        $("#SubmitPromo").hide();
                        $("#SubmitPromo").removeClass("MunculNengah")
                    }
                }
                else {
                    bootbox.alert('You only allowed to add ' + dataprom.PromoQty + ' free products for this promo');
                }
                //AddToCart(+$("#HiddenIDProduct").val(), +$("#SelectSize option:selected").data("idcombination"), +$(".qty").val(), $("#SelectSize option:selected").data("price"), $("#SelectSize option:selected").data("combinationname"));
            }
        }

    });

    //var dataSize = data.Size;
    //LoadSize(dataSize);
}

function LoadListCartSummary(data, TotalPrice) {
    item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li>';
        item += '<a href="/ProductDetail/' + data[i].ProductName.toLowerCase().split('"').join("").split(" ").join("-") + '">';
        item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].ProductName + '" /></a>';
        item += '<div class="bag_list_details floatright">';
        item += '<a href="/ProductDetail/' + data[i].ProductName.toLowerCase().split('"').join("").split(" ").join("-") + '">';
        item += '<h4>' + data[i].ProductName + '</h4>';
        item += '</a>';
        item += '<span class="format-money">' + data[i].Price + '</span>';
        item += '</div>';
        item += '</li>';
    }
    $('.list-summary').html(item);

    $('.TotalPrice').text(TotalPrice);
    $(".format-money").formatCurrency({
        region: format
    })
}

function testPromo(items) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            toastr.success("Promo Product Successfully Added to Cart");
            //window.location = "SummaryPromo.aspx";
        }
        else
            bootbox.alert(result.d.message);
    };
    REST.sendRequest({
        'c': 'feprom',
        'm': 'testpromo',
        'data': { 'items': items }
    });
}

function addFreeProduct(items) {

    $.ajax({
        url: "/modules/BuyGetFree/BuyGet.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "BuyGet",
            t: "fe",
            m: "savepromotocart",
            data: {
                items: items
            }
        }),
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true
            });
        },
        success: function (result) {
            if (result.success) {
                toastr.success("Promo Product Successfully Added to Cart");
                setTimeout(function () { window.location = "Address.aspx"; }, 400);
            }
        },
        complete: function() {
            Metronic.unblockUI();
        },
        error: function (result) {
            $(".error").html(result.message);
        }
    });

    //REST.onSuccess = function (result) {
    //    if (result.d.success) {
    //        toastr.success("Promo Product Successfully Added to Cart");
    //        setTimeout(function () { window.location = "Address.aspx"; }, 400);
    //    }
    //    else
    //        bootbox.alert(result.d.message);
    //};
    //REST.sendRequest({
    //    'c': 'feprom',
    //    'm': 'submitpromo',
    //    'data': { 'items': items }
    //});
}