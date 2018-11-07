var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    if (queryString("idCategory")) {
        $("#HiddenIDCategory").val(queryString("idCategory"));
    }
    else {
        $("#HiddenIDCategory").val(0);
    }

    $(".filterBy-size").click(function () {
        var valueFilt = {};
        valueFilt = GetProductValueFilter();
        LoadProductByFilterSize(valueFilt);
    });

    Preload();
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var produks = result.d.data.ListProduct;
            $("#HiddenActiveFilter").val(0);

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
                $(".loginbut").text("LOGIN / REGISTER");
                $(".btn-logout").hide();
            }

            var Category = result.d.data.Category;
            if (Category) {
                LoadCategoryProduct(Category);
                //LoadCategoryCrumb(Category);
            }

            var Value = result.d.data.Value;
            if (Value) {
                LoadFilterValue(Value);
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
            'RequestData': ['ListProduct', 'CartSummary', 'Customer', 'Category', 'Value'],
            'IDManufacturer': 0,
            '_param_take': 0,
            'IDCategory': +$("#HiddenIDCategory").val(),
            'IDAttribute': 2,
            'IDValue': null,
            'PriceRange': null
        }
    });
}

function LoadCategoryCrumb(product) {
    var item = '';
    for (var i = 0; i < product.length; i++) {
        if (i < 1) {
            item += '<li><a data-IDCategory="' + product[i].IDCategory + '" href="./ProductListNoPaging.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }
        else {
            item += '<li> // <a data-IDCategory="' + product[i].IDCategory + '" href="./ProductListNoPaging.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }

    }
    $("#breadCat").html(item);
}

function LoadCategoryProduct(Category) {
    var item = '';
    for (var i = 0; i < Category.length; i++) {
        item += '<li>';
        item += '<a href="ProductListNoPaging.aspx?idCategory=' + Category[i].IDCategory + '">';
        item += Category[i].Name;
        item += '</a>';
        item += '<span>(' + Category[i].TotalProduct.data + ')</span>';
        item += '</li>';

    }
    $("#ProductCategories").html(item);
}
function LoadProduct(data) {
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
                        scrollTop: $(".my-account").offset().top - 100
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
                        scrollTop: $(".my-account").offset().top - 100
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
                        scrollTop: $(".my-account").offset().top - 100
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
                            scrollTop: $(".my-account").offset().top - 100
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

            item += '<div class="col-md-4 col-sm-6 spacing">';
            item += '<div class="product-box">';
            item += '<span class="left-corner"></span><span class="right-corner"></span>';
            item += '<div class="product-image catalognya">';
            item += '<div style="background-image:url(./assets/images/product/' + data[i].Photo + ');" class="img-full bg-catalog"></div>';
            item += '<img src="assets/images/product/' + data[i].Photo + '" class="img-full catalog-tab" />';
            item += '<div class="product-buttons"></div>';
            item += '</div>';
            item += '<h4 class="product-name"> ' + data[i].Name + ' </h4>';
            item += '<span class="product-category">' + data[i].Category + '</span>';
            item += '<span class="product-price format-money">' + data[i].Price + '</span>';
            item += '<a href="productDetail.aspx?id=' + data[i].IDProduct + '" class="site-button-dark"><span>VIEW</span></a>';
            item += '</div>';
            item += '</div>';
            item += '</a>';

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
        item += '<div class="cartnya"><div class="img-cart"><img style="width:100%" src="/assets/images/product/' + data[i].Photo + '" /></div><div class="desc-cart"><label class="prodNameCart">' + data[i].ProductName + '</label><span class="CombCart">' + data[i].CombinationName + '</span><span class="Qtycart"><label>Qty </label>:&nbsp;' + data[i].Quantity + '</span><div class="priceCart">' + data[i].Price + '<span>&nbsp;IDR</span></div></div></div>';
    }
    $('.list-summary').html(item);

    $('.TotalPrice').text(TotalPrice);
}

function LoadSearchProduct() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var item = '';
            var data = result.d.data;

            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {

                    item += '<a href="productDetail.aspx?id=' + data.IDProduct + '" class="col-md-2">';
                    item += '<img src="assets/images/product/' + data.Image + '" class="img-full" />';
                    item += '<div class="desc-prodlist">';
                    item += '<label>White Ts With Polka</label>';
                    item += '<span class="format-money">IDR 200.00</span>';
                    item += '</div>';
                    item += '</a>';

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
            item += '<li><a data-IDCategory="' + product[i].IDCategory + '" href="./ProductListNoPaging.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }
        else {
            item += '<li> // <a data-IDCategory="' + product[i].IDCategory + '" href="./ProductListNoPaging.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }

    }
    $("#breadCat").html(item);
}

function LoadFilterValue(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li>';
        item += '<input type="checkbox" data-idvalue="' + data[i].IDValue + '">';
        item += '<a>' + data[i].Name + '</a><span>' + data[i].TotalProduct + '</span>';
        item += '</li>';
    }

    $(".size-filter").html(item);


}

function GetProductValueFilter() {
    var arr = [];
    $(".size-filter li input[type=checkbox]").each(function (index, element) {
        // element == this
        if ($(this).prop('checked'))
            arr.push(+$(this).data('idvalue'));
    });
    return arr;
}

function LoadProductByFilterSize(idValue) {
    if (idValue) {
        REST.onSuccess = function (result) {
            if (result.d.success) {
                $("#HiddenActiveFilter").val(1);
                var produks = result.d.data.ListProduct;

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
                'RequestData': ['ListProduct'],
                'IDManufacturer': 0,
                '_param_take': 0,
                'IDCategory': +$("#HiddenIDCategory").val(),
                'IDAttribute': 2,
                'IDValue': idValue,
                'PriceRange': $("#slider-range").slider("values")
            }
        });
    }
}
