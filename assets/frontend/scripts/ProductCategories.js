var REST = new OF({
    'url': '/api/services.asmx/request'
});

var filterCategory;
var promo;

var format = "id-ID";

$(document).ready(function () {
    filterCategory = true;

    //var r = 'NOTLET 11" MAGENTA';
    //var split = r.split(" ").join("-");
    //console.log(split);

    $(".filterBy-size").click(function () {
        var valueFilt = {};
        valueFilt = GetProductValueFilter();
        LoadProductByFilterSize(valueFilt, 16, 1);
    });

    Preload(16, 1);
});

function Preload(take, currentPage) {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            var Category = result.d.data.Category;
            if (Category != null) {
                LoadCategoryProduct(Category);
                LoadCategoryCrumb(Category);
            }

            if (result.d.data.ListProductPaging) {
                var produks = result.d.data.ListProductPaging;

                if (produks) {
                    if (queryString("q")) {
                        $("#HiddenKeywords").val(queryString("q"));
                        LoadSearchProduct();
                    }
                    else {
                        $("#HiddenKeywords").val("empty");
                        LoadProduct(produks, Category);
                    }
                }
            }
            else if (result.d.data.ProductByCategory) {
                var produkCat = result.d.data.ProductByCategory;

                LoadProductByCategory(produkCat);
            }


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
                $(".loginbut").text("Welcome, " + Customer.FirstName);
                $(".loginbut").attr("href", "MyAccount.aspx");
                $(".btn-logout").show();
            }
            else {
                $(".loginbut").text("LOGIN");
                $(".btn-logout").hide();
            }

            var Type = result.d.data.Type;
            if (Type != null) {
                LoadType(Type);
            }

            var Feature = result.d.data.Feature;
            if (Feature != null) {
                LoadFeature(Feature);
            }

            var Size = result.d.data.Size;
            if (Size) {
                LoadFilterSize(Size);
            }

            var Color = result.d.data.Color;
            if (Color) {
                LoadFilterColor(Color);
            }
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['ListProductPaging', 'CartSummary', 'Customer', 'Category'],
            'IDManufacturer': 0,
            '_param_take': 4,
            'IDCategory': +$("#HiddenIDCategory").val(),
            'IDAttribute': [3],
            'IDValue': null,
            'PriceRange': null,
            '_page': currentPage,
            'FilterCategory': filterCategory,
            'IDParent': 0
        }
    });
}
//    REST.sendRequest({
//        'c': 'femaster',
//        'm': 'preload',
//        'data': {
//            'RequestData': ['ListProductPaging', 'CartSummary', 'Customer', 'Category', 'Value'],
//            'IDManufacturer': 0,
//            '_param_take': 0,
//            'IDCategory': +$("#HiddenIDCategory").val(),
//            'IDAttribute': 2,
//            'IDValue': null,
//            'PriceRange': null,
//            '_page': currentPage
//        }
//    });
//}

function LoadProduct(data, dataCat) {
    var item = '';
    if (data.length > 0) {
        var _totalPage = data[0].TotalPage;
        var _currentPage = data[0].CurrentPage;
        var _startPage = data[0].StartPage;
        var _endPage = data[0].EndPage;

        var paging = '';
        if (_startPage > 1)
            paging += '<li><a href="#">« Previous</a></li>';

        for (var i = _startPage; i <= _endPage; i++) {
            if (i == _currentPage)
                paging += '<li><a class="paging-number active" data-page="' + i + '" href="#">' + i + '</a></li>';
            else
                paging += '<li><a class="paging-number" data-page="' + i + '" href="#">' + i + '</a></li>';
        }
        //paging += '<li><a href="#">Next »</a></li>';

        $(".paging-prod").html(paging);

        $(".paging-number").click(function (e) {
            e.preventDefault();
            if (queryString("id_manufacturer")) {
                var idman = parseInt(queryString("id_manufacturer"));
                REST.onSuccess = function (result) {
                    LoadProduct(result.d);
                    $('html, body').animate({
                        scrollTop: $(".logo").offset().top - 100
                    }, 1000);
                };
                REST.sendRequest({
                    'c': 'fepro',
                    'm': 'onpagechg_byidman',
                    'data': {
                        'CurrentPage': +$(this).data("page"),
                        'IDManufacturer': idman
                    }
                });
            }
            else if (queryString("idCategory")) {
                var idcat = parseInt(queryString("idCategory"));
                REST.onSuccess = function (result) {
                    LoadProduct(result.d);
                    $('html, body').animate({
                        scrollTop: $(".logo").offset().top - 100
                    }, 1000);
                };
                REST.sendRequest({
                    'c': 'fepro',
                    'm': 'onpagechg_byidcat',
                    'data': {
                        'CurrentPage': +$(this).data("page"),
                        'IDCategory': idcat
                    }
                });
            }
            else if ($("#HiddenActiveFilter").val() == 1) {
                var idValue = {};
                idValue = GetProductValueFilter();
                var priceRange = $("#slider-range").slider("values");
                REST.onSuccess = function (result) {
                    LoadProduct(result.d);
                    $('html, body').animate({
                        scrollTop: $(".logo").offset().top - 100
                    }, 1000);
                };
                REST.sendRequest({
                    'c': 'fepro',
                    'm': 'onpagechg_byidval_price',
                    'data': {
                        'CurrentPage': +$(this).data("page"),
                        'IDValue': idValue,
                        'PriceRange': priceRange
                    }
                });
            }
            else {
                REST.onSuccess = function (result) {
                    if (result.d.success) {
                        LoadProduct(result.d.data);
                        $('html, body').animate({
                            scrollTop: $(".logo").offset().top - 100
                        }, 1000);
                    }
                };
                REST.sendRequest({
                    'c': 'fepro',
                    'm': 'onpagechg',
                    'data': {
                        'CurrentPage': +$(this).data("page")
                    }
                });
            }
        });
        for (var i = 0; i < data.length; i++) {
            if (data[i].Quantity != 0) {
                item += '<div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">';
                item += '<a href="/ProductDetail.aspx?id=' + data[i].IDProduct + '" data-toggle="modal">';
                item += '<img src="/assets/images/product/' + data[i].Photo + '?w=600" />';
                item += '</a>';
                item += '</div>';
            }
        }
    }
    else {
        item += '<h5 style="text-align:center;">No Product in this Category</h5>';
    }
    $("#ProductList").html(item);

    $(".format-money").formatCurrency({
        region: "id-ID"
    })
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

function LoadSearchProduct() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var item = '';
            var data = result.d.data;

            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {

                    item += '<div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">';
                    item += '<a href="/ProductDetail.aspx?id=' + data[i].IDProduct + '" data-toggle="modal">';
                    item += '<img src="/assets/images/product/' + data[i].Photo + '" />';
                    item += '</a>';
                    item += '</div>';

                }
                $("#ProductList").html(item);

                $(".format-money").formatCurrency({
                    region: "id-ID"
                })
            }

            else {
                $("#ProductList").html("<h3 style='text-align:center;width:100%;'>No Product</p>");
            }

        }
    };
    REST.sendRequest({
        'c': 'fepro',
        'm': 'search',
        'data': {
            'Keywords': $("#HiddenKeywords").val()
        }
    });
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

function LoadFilterSize(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li>';
        item += '<input type="checkbox" id="' + data[i].IDValue + '" data-idvalue="' + data[i].IDValue + '">';
        item += '<label class="filcattext" for="' + data[i].IDValue + '">' + data[i].Name + '</label>';
        item += '</li>';
    }

    $(".size-filter").html(item);


}

function LoadFilterColor(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li>';
        item += '<input type="checkbox" data-idvalue="' + data[i].IDValue + '">';
        item += '<span style="display:block;float:left;top:5px;position:relative;margin-right:5px;background:' + data[i].RGBColor + ';width:10px;height:10px;"></span><a>' + data[i].Name + '</a><span>(' + data[i].TotalProduct + ')</span>';
        item += '</li>';
    }

    $(".color-filter").html(item);


}

function LoadCategoryProduct(Category) {
    var item = '';
    for (var i = 0; i < Category.length; i++) {
        item += '<a href="/ProductList.aspx?idCategory=' + Category[i].IDCategory + '">' + Category[i].Name + '</a>';
    }
    $(".ProductCategories").html(item);
}

function GetProductValueFilter() {
    var arr = [];
    $(".size-filter li input[type=checkbox]").each(function (index, element) {
        // element == this
        if ($(this).prop('checked'))
            arr.push(+$(this).data('idvalue'));
    });
    $(".color-filter li input[type=checkbox]").each(function (index, element) {
        // element == this
        if ($(this).prop('checked'))
            arr.push(+$(this).data('idvalue'));
    });
    return arr;
}

function LoadProductByFilterSize(idValue, take, currentPage) {
    if (idValue) {
        REST.onSuccess = function (result) {
            if (result.d.success) {
                $("#HiddenActiveFilter").val(1);
                var produks = result.d.data.ListProductPaging;

                if (produks) {
                    if (queryString("q")) {
                        $("#HiddenKeywords").val(queryString("q"));
                        LoadSearchProduct();
                    }
                    else {
                        $("#HiddenKeywords").val("empty");
                        LoadProduct(produks);
                    }
                }

                if (result.d.data.ProductCategory) {
                    $(".categorynya").text(result.d.data.ProductCategory.Name);
                }
                if (result.d.data.TotalProduct) {
                    $(".totalproductnya").text(result.d.data.TotalProduct.data);
                }
            }
        };
        REST.sendRequest({
            'c': 'femaster',
            'm': 'preload',
            'data': {
                'RequestData': ['ListProductPaging'],
                'IDManufacturer': 0,
                '_param_take': 0,
                'IDCategory': +$("#HiddenIDCategory").val(),
                'IDAttribute': 3,
                'IDValue': idValue,
                'PriceRange': $("#slider-range").slider("values"),
                '_page': currentPage,
                'FilterCategory': filterCategory,
                'IDParent': 23
            }
        });
    }
}

function LoadType(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li><input type="checkbox" id="' + data[i].IDCategory + '" value="' + data[i].IDCategory + '" /><label class="filcattext" for="' + data[i].IDCategory + '">' + data[i].Name + '</label></li>';

    }
    $(".ProductCategories").html(item);
}

function LoadFeature(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li><input type="checkbox" id="' + data[i].IDCategory + '" value="' + data[i].IDCategory + '" /><label class="filcattext" for="' + data[i].IDCategory + '">' + data[i].Name + '</label></li>';

    }
    $(".ProductFeature").html(item);
}

function LoadProductByCategory(data) {
    var item = '';
    var loop = '';
    var prod = {};
    var gaProd = [];

    for (var i = 0; i < data.length; i++) {
        if (data[i].IDCategory != 9) {
            item += '<div class="col-md-12 col-xs-12 cat_products">';
            item += '<div class="col-md-6 col-sm-6 col-xs-12 catsbung nopad">';
            item += '<h3>' + data[i].Name + '<a href="/Products/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '" class="findout">find out more...</a></h3>';
            item += '<img src="/assets/images/category/' + data[i].Image + '" class="img-responsive" />';
            item += '</div>';
            item += '<div class="col-md-6 col-sm-6 col-xs-12 prodsbung nopad">';
            for (var y = 0; y < 4; y++) {
                item += '<div class="col-md-6 col-sm-6 col-xs-6">';
                item += '<div class="single_feature text-center">';
                item += '<div class="feature-img">';
                item += '<a href="/ProductDetail/' + data[i].Products[y].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
                item += '<img src="/assets/images/product/' + data[i].Products[y].Photo + '" alt="' + data[i].Products[y].Name + '" />';
                item += '</a>';
                item += '<span class="img-hover">';
                item += '<a href="/ProductDetail/' + data[i].Products[y].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
                item += '<img src="/assets/images/product/' + data[i].Products[y].Photo + '" alt="' + data[i].Products[y].Name + '" />';
                item += '</a>';
                item += '</span>';
                item += '</div>';
                item += '<div class="feature_text">';
                item += '<a href="/ProductDetail/' + data[i].Products[y].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '"><h4>' + data[i].Products[y].Name + '</h4></a>';
                if (data[i].Products[y].PriceBeforeDiscount > data[i].Products[y].Price) {
                    item += '<span class="format-money">' + data[i].Products[y].Price + '</span>&nbsp;&nbsp;<del class="format-money" style="color:#ff0000;">' + data[i].Products[y].PriceBeforeDiscount + '</del>';
                }
                else {
                    item += '<span class="format-money">' + data[i].Products[y].Price + '</span>';
                }
                item += '</div>';
                item += '</div>';
                item += '</div>';
            }

            item += '</div>';
            item += '</div>';
        }

    }

    $("#ProductList").html(item);


    var owl = $(".newProd");

    owl.owlCarousel({

        items: 4,
        lazyLoad: true,
        navigation: true

    });

    $(".owl-prev").html("<i class='fa fa-chevron-left'></i>");
    $(".owl-next").html("<i class='fa fa-chevron-right'></i>");
    // Custom Navigation Events

    $(".format-money").formatCurrency({
        region: "id-ID"
    })

    $('.add-to-cart').on('click', function (e) {
        if ($("#HiddenPromo").val() == "true") {
            e.preventDefault();
            if (!$(this).data("qty") == null) {
                //$(".qty").after('<span for="Password" class="help-block">This field is required.</span>');
                bootbox.alert("please type quantity product");
            }
            else if ($(this).data("qty") <= 0) {
                bootbox.alert("Out of stock");
            }
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
                AddToCartIfPromo(+$(this).data("idproduct"), +$(this).data("idcombination"), 1, +$(this).data("price"), $(this).data("combinationname"), $(this).data("category"));
            }
        }
        else {
            e.preventDefault();
            if (!$(this).data("qty") == null) {
                //$(".qty").after('<span for="Password" class="help-block">This field is required.</span>');
                bootbox.alert("please type quantity product");
            }
            else if ($(this).data("qty") <= 0) {
                bootbox.alert("Out of stock");
            }
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
                AddToCart(+$(this).data("idproduct"), +$(this).data("idcombination"), 1, +$(this).data("price"), $(this).data("combinationname"), $(this).data("category"));
            }
        }
    });

}

function AddToCart(idProduct, idCombination, qty, price, combinationName, category) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            fbq('track', 'AddToCart');
            PreloadCart();

            if (category.toLowerCase() == "timepiece") {
                toastr.success("Product added to cart successfully");

                setTimeout(function () {
                    window.location.href = "ProductList.aspx?idCategory=22"; //will redirect to your blog page (an ex: blog.html)
                }, 3000); //will call the function after 2 secs.
            }
            else {
                PreloadCart();
                $('.TotalQuantity').text(result.d.data.TotalQuantity);
                $('.fullcontent').addClass("tranformation");
                $('.fullcontent').addClass("tranformation");
                $('.cover-opacity').fadeIn();
                $('.cartsummary').addClass("tranformation");
                $('.cartsummary').addClass("zindex");
            }
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
            'Price': +price,
            'CombinationName': combinationName,
            'ProductName': $(".ProductName").text(),
        }
    });
}

function PreloadCart() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            if (result.d.data.CartSummary) {
                LoadListCartSummary(result.d.data.CartSummary.Product, result.d.data.CartSummary.TotalPrice);
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

function DeleteProduct(idCombination) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            $(".error").html("");
            if (result.d.message != "OK") {
                bootbox.alert(result.d.message, function (result) {
                    location.reload();
                });
            }
            else {
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

