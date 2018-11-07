var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    if ($("#HiddenIDProduct").val() != "") {
        PreloadMaster(+$("#HiddenIDProduct").val());
    }
    else
        window.location = '/Products';
    
    Wishlist();

    $('.add-to-cart').on('click', function (e) {
        e.preventDefault();
        if (!$(".qty").val()) {
            //$(".qty").after('<span for="Password" class="help-block">This field is required.</span>');
            bootbox.alert("please type quantity product");
        }
        //else if ($(".color-wrap").hasClass("hidden") == false && $(".loractive").data("idcombination") == null) {
        //    bootbox.alert("Please choose your color");
        //}
        else if ($(".selectedz").data("idvalue") == undefined || $(".selectedz").data("idvalue") == null) {
            bootbox.alert("please choose the size");
        }
        //else if ($(".selectedz").data("quantity") <= 0) {
        //    bootbox.alert("Out of stock");
        //}
        //else if (+$(".qty").val() > +$(".selectedz").data("quantity") || +$(".qty").val() <= 0) {
        //    bootbox.alert('Quantity must greater than 0 and less than ' + $(".selectedz").data("quantity"));
        //}
        else {
            //var cart = $('.shopping-cart');
            //var imgtodrag = $('.productdetail-left').find("img").eq(0);
            //if (imgtodrag) {
            //    var imgclone = imgtodrag.clone()
            //        .offset({
            //            top: imgtodrag.offset().top,
            //            left: imgtodrag.offset().left
            //        })
            //        .css({
            //            'opacity': '0.5',
            //            'position': 'absolute',
            //            'height': '150px',
            //            'width': '150px',
            //            'z-index': '100'
            //        })
            //        .appendTo($('body'))
            //        .animate({
            //            'top': cart.offset().top + 10,
            //            'left': cart.offset().left + 10,
            //            'width': 75,
            //            'height': 75
            //        }, 1000, 'easeInOutExpo');


            //    setTimeout(function () {
            //        $('.muncul').removeClass("dissapear");
            //        $('.muncul').effect("bounce", { direction: 'down', times: 3 }, 700);
            //        $('.hilang').hide();
            //    }, 1000);

            //    setTimeout(function () {
            //        cart.effect("bounce", {
            //            times: 2
            //        }, 500);
            //    }, 1500);

            //    imgclone.animate({
            //        'width': 0,
            //        'height': 0
            //    }, function () {
            //        $(this).detach()
            //    });


            //}
            AddToCart(+$("#HiddenIDProduct").val(), +$(".selectedz").data("idcombination"), +$(".qty").val(), $(".selectedz").data("price"), $(".selectedz").data("combinationname"));
        }
    });
});

function PreloadMaster(idproduct) {
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
            if (result.d.data.Cart) {
                $(".TotalPrice").text(result.d.data.Cart.TotalPrice + ' IDR');
                $(".TotalQuantity").text(result.d.data.Cart.TotalQuantity);
            }
            else {
                $(".TotalQuantity").text("0");
                $(".TotalPrice").text('0 IDR');
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
                    $("#txtEmailWish").hide();
                    $("#txtEmailWish").val(result.d.data.Customer.Email);
                    $(".btn-add-to-cart").removeClass("hidden");
                    $(".add-to-cart-info").addClass("hidden");
                }
                else {
                    $(".loginbut").text("GUEST");
                    $(".loginbut").attr("href", "#");
                    $(".btn-logout").hide();
                    $(".btn-newAddress").hide();
                    $("#txtEmailWish").show();
                    $(".btn-add-to-cart").removeClass("hidden");
                    $(".add-to-cart-info").addClass("hidden");
                }
                CheckPackage();
            }
            else {
                $(".loginbut").text("LOGIN");
                $(".btn-logout").hide();
                $("#txtEmailWish").show();
                $(".btn-add-to-cart").addClass("hidden");
                $(".add-to-cart-info").removeClass("hidden");
            }
            if (result.d.data.ProductDetail != null)
                LoadProduct(result.d.data.ProductDetail);


            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);
            }

            var similiarProduct = result.d.data.SimiliarProduct;
            if (similiarProduct != null)
                LoadSimiliarProduct(similiarProduct);

            var Category = result.d.data.Category;
            if (Category != null) {
                LoadCategoryCrumb(Category);
            }
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['ProductDetail', 'Cart', 'Customer', 'SimiliarProduct', 'CartSummary', 'Category','Currency'],
            '_param_IDProduct': idproduct,
            '_param_take': 4
        }
    });
}

function LoadCurrency(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        if (data[i].Selected) {
            item += '<a data-id="' + data[i].IDCurrency + '" style="color:red">' + data[i].Name + '</a>';
            format = data[i].Format;
        }
        else
            item += '<a data-id="' + data[i].IDCurrency + '">' + data[i].Name + '</a>';
    }

    $(".currency").html(item);
    $(".currency a").on("click", function () {
        console.log($(this).data("id"));
        ChangeCurrency($(this).data("id"));
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

function PreloadCart() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);
            }
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['CartSummary'],
        }
    });
}

function LoadSimiliarProduct(data) {
    item = '';
    for (var i = 0; i < data.length; i++) {
        if (data[i].Quantity != 0) {
            item += '<div class="col-md-3 col-sm-6 col-xs-6">';
            item += '<div class="single_feature text-center">';
            item += '<div class="feature-img">';
            item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
            item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
            item += '</a>';
            item += '<span class="img-hover">';
            item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '>';
            item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
            item += '</a>';
            item += '</span>';
            item += '</div>';
            item += '<div class="feature_text">';
            item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '"><h4>' + data[i].Name + '</h4></a>';
            if (data[i].PriceBeforeDiscount > data[i].Price) {
                item += '<span class="format-money">' + data[i].Price + '</span><del class="format-money" style="color:#ff0000;text-decoration:line-through;">' + data[i].PriceBeforeDiscount + '</del>';
            }
            else {
                item += '<span class="format-money">' + data[i].Price + '</span>';
            }
            item += '</div>';
            item += '</div>';
            item += '</div>';
        }
        else {
            item += '<div class="col-md-3 col-sm-6 col-xs-6">';
            item += '<div class="single_feature text-center">';
            item += '<div class="feature-img">';
            item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
            item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
            item += '</a>';
            item += '<span class="img-hover">';
            item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '>';
            item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
            item += '</a>';
            item += '</span>';
            item += '</div>';
            item += '<div class="feature_text">';
            item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '"><h4>' + data[i].Name + '</h4></a>';
            if (data[i].PriceBeforeDiscount > data[i].Price) {
                item += '<span class="format-money">' + data[i].Price + '</span><del class="format-money" style="color:#ff0000;text-decoration:line-through;">' + data[i].PriceBeforeDiscount + '</del>/<span style="color:#ff0000;">SOLD OUT</span>';
            }
            else {
                item += '<span class="format-money">' + data[i].Price + '</span>/<span style="color:#ff0000;">SOLD OUT</span>';
            }
            item += '</div>';
            item += '</div>';
            item += '</div>';
        }
    }
    $(".related-products").html(item);
    $(".format-money").formatCurrency({
        region: format
    })
}

function LoadProduct(data) {
    if (data != undefined) {
        $(".ProductName").text(data.Product.Name);
        $(".ProductNameCrumb").text(data.Product.Name);
        if (data.Product.Price == data.Product.PriceBeforeDiscount) {
            $(".PriceBeforeDiscount").remove();
            $(".ProductPrice").text(data.Product.Price);
        }
        else {
            $(".PriceBeforeDiscount").text(data.Product.PriceBeforeDiscount);
            $(".ProductPrice").text(data.Product.Price);
        }
        if (data.Product.ShortDescription.includes("<iframe")) {
            $(".review_tab").show();
            $(".ShortDescription").html(data.Product.ShortDescription);
        }
        else {
            $(".review_tab").hide();
        }
        
        $(".Description").html(data.Product.Description);
        $(".Note").html(data.Product.Note);
        $(".DefaultCategory").text(data.DefaultCategory);
        $(".DefaultCategory").attr("href", "/Products/" + data.DefaultCategory);
        $(".sizeGuide").html(data.Product.ShortDescription);
        var poto = '';
        for (var i = 0; i < data.Photos.length; i++) {
            if (i == 0) {
                poto += '<div role="tabpanel" class="tab-pane fade in active ex1" id="pro_' + i + '">';
                poto += '<a href="/assets/images/product/' + data.Photos[i].Photo + '" data-lightbox="image-' + i + '" data-title="' + data.Photos[i].Name + '"><img src="/assets/images/product/' + data.Photos[i].Photo + '" alt="' + data.Photos[i].Name + '" /></a>';
                poto += '</div>';
            }
            else {
                poto += '<div role="tabpanel" class="tab-pane fade in ex1" id="pro_' + i + '">';
                poto += '<a href="/assets/images/product/' + data.Photos[i].Photo + '" data-lightbox="image-' + i + '" data-title="' + data.Photos[i].Name + '"><img src="/assets/images/product/' + data.Photos[i].Photo + '" alt="' + data.Photos[i].Name + '" /></a>';
                poto += '</div>';
            }


        }
        var thumbfoto = ''
        for (var i = 0; i < data.Photos.length; i++) {
            if (i == 0) {
                thumbfoto += '<li class="active">';
                thumbfoto += '<a href="#pro_' + i + '" data-toggle="tab" aria-controls="pro_one">';
                thumbfoto += '<img src="/assets/images/product/' + data.Photos[i].Photo + '" alt="' + data.Photos[i].ProductName + '" />';
                thumbfoto += '</a>';
                thumbfoto += '</li>';
            }
            else {
                thumbfoto += '<li>';
                thumbfoto += '<a href="#pro_' + i + '" data-toggle="tab" aria-controls="pro_one">';
                thumbfoto += '<img src="/assets/images/product/' + data.Photos[i].Photo + '" alt="' + data.Photos[i].ProductName + '" />';
                thumbfoto += '</a>';
                thumbfoto += '</li>';
            }

        }
        $(".potoslide").html(poto);
        $(".thumbfoto").html(thumbfoto);

        $(".format-money").formatCurrency({
            region: format
        })

        if ($(window).width > 1024) {
            $.getScript("/assets/frontend/js/raxus-slider.min.js");
            $.getScript("/assets/frontend/js/jquery.zoom.js");
            $('.ex1').zoom();
        }

        //SET SELECTED IDProduct_Combination
        //$("#HiddenIDProduct_Combination").val(data[0].IDProduct_Combination);
        ////SET Quantity default by first combination
        //if (data.Color[0].Quantity != 0) {
        //    $(".qty").show();
        //    $(".Quantity").text(data.Color[0].Quantity);
        //}
        //else {
        //    $(".qty").hide();
        //    $(".qty").parent().append("<p style='text-align:left'>stock is not available</p>");
        //}
        //LOAD COLOR
        var dataColor = data.Color;
        if (dataColor.length > 0) {
            LoadColor(dataColor);
            $(".color-wrap").removeClass("hidden");
        }
        else {
            $(".color-wrap").addClass("hidden");
        }
        //LOAD SIZE
        var dataSize = data.Size;
        LoadSize(dataSize);
    }
}

function InitCartList(data) {

    if (data != undefined && data.Product.length > 0) {
        LoadCartList(data);
        setTimeout(function () {
            $('.muncul').removeClass("dissapear");
            $('.muncul').effect("bounce", { direction: 'down', times: 3 }, 700);
            $('.hilang').hide();
        }, 1000);
    }
    else {
        setTimeout(function () {
            $('.hilang').removeClass("dissapear");
            $('.hilang').effect("bounce", { direction: 'down', times: 3 }, 700);
            $('.muncul').hide();
        }, 1000);
    }
}

function AddToCart(idProduct, idCombination, qty, price, combinationName) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            toastr.success(result.d.message);
            PreloadCart();
            $('.TotalQuantity').text(result.d.data.TotalQuantity);
            

        }
        else
            bootbox.alert(result.d.message);
    };
    REST.sendRequest({
        'c': 'feorder',
        'm': 'addcart',
        'data': {
            'IDProduct': idProduct,
            'IDCombination': idCombination,
            'Quantity': qty,
            'Price': price,
            'CombinationName': combinationName,
            'ProductName': $(".ProductName").text(),
            'OrderType':'new'
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
    });
}

function LoadCartList(data) {
    var total = data.TotalPrice;
    var totalQuantity = data.TotalQuantity;
    data = data.Product;
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<div style="width:100%;float:left;" class="item">';
        item += '<div class="half-content">';
        item += data[i].Quantity + 'x ' + data[i].ProductName + '<br />';
        item += '<span class="combination">' + data[i].CombinationName + '</span>';
        item += '</div>';
        item += '<div class="half-content" style="width: 50%;">';
        item += '<input data-idcombination="' + data[i].IDCombination + '" type="button" class="delete-cart trash-delete" />';
        item += '<label class="Harga">' + data[i].Price + ' IDR</label>';
        item += '</div>';
        item += '</div>';
    }

    $(".cart-list").html(item);
    $(".TotalPrice").text(total + ' IDR');
    $(".Subtotal").text(total + ' IDR');
    $(".TotalQuantity").text(totalQuantity);

    $(".delete-cart").click(function (e) {
        e.preventDefault();
        DeleteCart(+$(this).data("idcombination"), this);
    });
}

function DeleteCart(idcombination, element) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            $(element).parent().parent().remove();
            $(".TotalPrice").text(result.d.data.TotalPrice + " IDR");
            $(".Subtotal").text(result.d.data.TotalPrice + " IDR");
            $(".TotalQuantity").text(result.d.data.TotalQuantity);
            if (!$(".cart-list").html()) {
                $(".TotalPrice").text("0 IDR");
                setTimeout(function () {
                    $('.hilang').removeClass("dissapear");
                    $('.hilang').effect("bounce", { direction: 'down', times: 3 }, 700);
                    $('.muncul').hide();
                }, 1000);
            }
        }
    };
    REST.sendRequest({
        'c': 'feorder',
        'm': 'delcart',
        'data': {
            'IDCombination': idcombination
        }
    });
}

function LoadColor(dataColor) {
    var color = '';
    for (var i = 0; i < dataColor.length; i++) {
        if (i == 0) {
            color += '<div class="col-md-4 col-xs-4 nopad">';
            color += '<div class="selectColor loractive" data-idvalue="' + dataColor[i].IDValue + '" data-idcombination="' + dataColor[i].IDProduct_Combination + '" <span style="background: ' + dataColor[i].RGBColor + ';"><i class="pe-7s-check"></i></span></div>';
            color += '</div>';
        }
        else {
            color += '<div class="col-md-4 col-xs-4 nopad">';
            color += '<div class="selectColor" data-idvalue="' + dataColor[i].IDValue + '" data-idcombination="' + dataColor[i].IDProduct_Combination + '" <span style="background: ' + dataColor[i].RGBColor + ';"><i class="pe-7s-check"></i></span></div>';
            color += '</div>';
        }
        //color += '<li class="kotakcol" data-idvalue="' + dataColor[i].IDValue + '" data-idcombination="' + dataColor[i].IDProduct_Combination + '"><div class="bordering" style="border-color:' + dataColor[i].RGBColor + ';"><span style="background: ' + dataColor[i].RGBColor + ';">&nbsp;</span></div>' + dataColor[i].Name + '</li>';
    }
    $(".color-list").html(color);

    $(".color-list .selectColor").click(function () {
        $(".loractive").removeClass("loractive");
        $(this).addClass("loractive");
        OnColorChange(+$(this).data("idvalue"), +$(this).data("idcombination"));
    });
}

function LoadSize(dataSize) {
    var size = '';
    for (var i = 0; i < dataSize.length; i++) {
        if (i == 0) {
            if (dataSize[i].Quantity == 0) {
                if (dataSize[i].Name == "All Size") {
                    size += '<li style="width:100px;" class="noqty selectedz" data-idproduct="' + dataSize[i].IDProduct + '" data-quantity="' + dataSize[i].Quantity + '" data-price="' + dataSize[i].Price + '" data-combinationname="' + dataSize[i].CombinationName + '" data-idcombination="' + dataSize[i].IDProduct_Combination + '" data-idvalue="' + dataSize[i].IDValue + '"><div class="linenoqty"></div>' + dataSize[i].Name + '</li>';
                }
                else {
                    size += '<li class="noqty selectedz" data-idproduct="' + dataSize[i].IDProduct + '" data-quantity="' + dataSize[i].Quantity + '" data-price="' + dataSize[i].Price + '" data-combinationname="' + dataSize[i].CombinationName + '" data-idcombination="' + dataSize[i].IDProduct_Combination + '" data-idvalue="' + dataSize[i].IDValue + '"><div class="linenoqty"></div>' + dataSize[i].Name + '</li>';
                }
            }
            else {
                if (dataSize[i].Name == "All Size") {
                    size += '<li style="width:100px;" class="hasqty selectedz" data-idproduct="' + dataSize[i].IDProduct + '" data-quantity="' + dataSize[i].Quantity + '" data-price="' + dataSize[i].Price + '" data-combinationname="' + dataSize[i].CombinationName + '" data-idcombination="' + dataSize[i].IDProduct_Combination + '" data-idvalue="' + dataSize[i].IDValue + '">' + dataSize[i].Name + '</li>';
                }
                else {
                    size += '<li class="hasqty selectedz" data-idproduct="' + dataSize[i].IDProduct + '" data-quantity="' + dataSize[i].Quantity + '" data-price="' + dataSize[i].Price + '" data-combinationname="' + dataSize[i].CombinationName + '" data-idcombination="' + dataSize[i].IDProduct_Combination + '" data-idvalue="' + dataSize[i].IDValue + '">' + dataSize[i].Name + '</li>';
                }

            }
        }
        else {
            if (dataSize[i].Quantity == 0) {
                if (dataSize[i].Name == "All Size") {
                    size += '<li style="width:100px;" class="noqty" data-idproduct="' + dataSize[i].IDProduct + '" data-quantity="' + dataSize[i].Quantity + '" data-price="' + dataSize[i].Price + '" data-combinationname="' + dataSize[i].CombinationName + '" data-idcombination="' + dataSize[i].IDProduct_Combination + '" data-idvalue="' + dataSize[i].IDValue + '"><div class="linenoqty"></div>' + dataSize[i].Name + '</li>';
                }
                else {
                    size += '<li class="noqty" data-idproduct="' + dataSize[i].IDProduct + '" data-quantity="' + dataSize[i].Quantity + '" data-price="' + dataSize[i].Price + '" data-combinationname="' + dataSize[i].CombinationName + '" data-idcombination="' + dataSize[i].IDProduct_Combination + '" data-idvalue="' + dataSize[i].IDValue + '"><div class="linenoqty"></div>' + dataSize[i].Name + '</li>';
                }
            }
            else {
                if (dataSize[i].Name == "All Size") {
                    size += '<li style="width:100px;" class="hasqty" data-idproduct="' + dataSize[i].IDProduct + '" data-quantity="' + dataSize[i].Quantity + '" data-price="' + dataSize[i].Price + '" data-combinationname="' + dataSize[i].CombinationName + '" data-idcombination="' + dataSize[i].IDProduct_Combination + '" data-idvalue="' + dataSize[i].IDValue + '">' + dataSize[i].Name + '</li>';
                }
                else {
                    size += '<li class="hasqty" data-idproduct="' + dataSize[i].IDProduct + '" data-quantity="' + dataSize[i].Quantity + '" data-price="' + dataSize[i].Price + '" data-combinationname="' + dataSize[i].CombinationName + '" data-idcombination="' + dataSize[i].IDProduct_Combination + '" data-idvalue="' + dataSize[i].IDValue + '">' + dataSize[i].Name + '</li>';
                }

            }
        }
    }
    $("#SelectSize").html(size);
    $("#SelectSize li").click(function () {
        $("#SelectSize li").removeClass("selectedz");
        $(this).addClass("selectedz");
        if (!$(this).hasClass("noqty")) {
            $(".oos").addClass("hidden");
            $(".add-to-cart").removeClass("hidden");
            $(".wishlist-wrap").addClass("hidden");
        }
        else {
            $(".oos").removeClass("hidden");
            $(".add-to-cart").addClass("hidden");
            $(".wishlist-wrap").removeClass("hidden");
        }
    });
}

function OnColorChange(idcolor, idCombination) {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            $("#HiddenIDProduct_Combination").val(data.Size[0].IDProduct_Combination);
            $(".Quantity").text(data.Size[0].Quantity);
            LoadSize(data.Size);
        }
    };
    REST.sendRequest({
        'c': 'fepro',
        'm': 'oncolorchg',
        'data': {
            'IDProduct': +$("#HiddenIDProduct").val(),
            'IDColor': idcolor,
            'IDCombination': idCombination
        }
    });
}

function OnSizeChange(idcombination) {
    $("#HiddenIDProduct_Combination").val(idcombination);
}

function LoadPhotoCombination(data) {
    var poto = '';

    $(".slider-area").remove();
    $(".mini-images").remove();

    poto += '<div class="slider-area">';
    poto += '<ul class="slider-relative potoslide">';

    for (var i = 0; i < data.length; i++) {
        //poto += '<div class="item"><a class="thumb-link" data-image="assets/images/product/' + data.Photos[i].Photo + '" data-zoom-image="assets/images/product/' + data.Photos[i].Photo + '" href="#" title=""> <img class="img-responsive" src="assets/images/product/' + data.Photos[i].Photo + '" alt="' + data.Product.Name + '" /> </a></div>';
        poto += '<li class="slide zoom ex1">';
        poto += '<img src="assets/images/product/' + data[i].Photo + '" />';
        poto += '</li>';
    }

    poto += '</ul>';
    poto += '</div>';

    $("#mySlider").html(poto);

    //$.getScript("assets/frontend/js/raxus-slider.min.js");
    //$('.ex1').zoom();
}

function LoadCategoryCrumb(product) {
    var item = '';
    for (var i = 0; i < product.length; i++) {
        if (i < 1) {
            item += '<li><a data-IDCategory="' + product[i].IDCategory + '" href="./ProductList.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }
        else {
            item += '<li> // <a data-IDCategory="' + product[i].IDCategory + '" href="./ProductList.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }

    }
    $("#breadCat").html(item);
}

function Wishlist() {
    var e = $("#form-wishlist");
    var t = $("#bootstrap_alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {
            Email: {
                required: "email cannot blank",
                email: "not valid email format"
            },
        },
        rules: {
            Email: {
                required: true,
                email: true
            },
        },
        errorPlacement: function (e, t) {
            $(".alert").append(e);
        },
        invalidHandler: function (e, n) {
            t.show();
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "danger",
                message: "You have some form errors. Please check below. ",
                close: true,
                reset: true,
                focus: true,
                //closeInSeconds: 5,
                icon: "warning"
            });
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error");
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error");
        },
        success: function (e) {
            $(e).closest(".form-group").removeClass("has-error");
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();

            var sendData = {};
            $("input", e).each(function (index, element) {
                // element == this
                if (element.name != undefined && element.name != "") {
                    sendData[element.name] = $(element).val();
                    //console.log($(element).val());
                }
            });

            sendData["IDCombination"] = +$("#SelectSize li.selectedz").data("idcombination");
            sendData["IDCustomer"] = +$("#HiddenIDCustomer").val();

            REST.onSuccess = function (result) {
                var data = result.d.data;
                t.show();
                if (result.d.success) {
                    toastr.success(result.d.message);
                }
                else {
                    bootbox.alert(result.d.message);
                }
            };
            REST.sendRequest({
                'c': 'fewish',
                'm': 'iwish',
                'data': sendData
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

function CheckPackage()
{
    $.ajax({
        url: "/modules/saas/Handler.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "saas",
            m: "checkpackage",
            data: {
                IDProduct: +$("#HiddenIDProduct").val(),
                IDCustomer: +$("#HiddenIDCustomer").val(),
            }
        }),
        beforeSend: function () {
            //Metronic.blockUI({
            //    boxed: true
            //});
        },
        success: function (result) {
            if (result.success) {
                $(".btn-add-to-cart").addClass("hidden");
                $(".add-to-cart-info").addClass("hidden");
                $(".add-to-cart-exist").removeClass("hidden");
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