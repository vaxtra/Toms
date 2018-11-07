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

    Preload();
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
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

            var Category = result.d.data.Category;
            if (Category != null) {
                LoadCategoryProduct(Category);
                LoadCategoryCrumb(Category);
            }
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['ListProduct', 'CartSummary', 'Customer', 'Category'],
            'IDManufacturer': 0,
            '_param_take': 0,
            'IDCategory': +$("#HiddenIDCategory").val()
        }
    });
}
function LoadCategoryProduct(Category) {
    var item = '';
    for (var i = 0; i < Category.length; i++) {
        item += '<li>';
        item += '<a href="ProductList.aspx?idCategory=' + Category[i].IDCategory + '">';
        item += Category[i].Name;
        item += '</a>';
        item += '<span>(0)</span>';
        item += '</li>';

    }
    $("#ProductCategories").html(item);
}
function LoadProduct(data) {
    var item = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {

            item += '<div class="col-md-4 col-sm-6 spacing">';
            item += '<div class="product-box">';
            item += '<span class="left-corner"></span><span class="right-corner"></span>';
            item += '<div class="product-image">';
            item += '<img src="assets/images/product/' + data[i].Photo + '" class="img-full" />';
            item += '<div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>';
            item += '</div>';
            item += '<h4 class="product-name"> '+ data[i].Name +' </h4>';
            item += '<span class="product-category">BAGS</span>';
            item += '<span class="product-price">$20.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>';
            item += '<a href="productDetail.aspx?id=' + data[i].IDProduct + '" class="col-md-2 col-md-offset-1">';
            item += '<div class="desc-prodlist">';
            item += '<span class="format-money">' + data[i].Price + '</span>';
            item += '</div>';
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
            item += '<li><a data-IDCategory="' + product[i].IDCategory + '" href="./ProductList.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }
        else {
            item += '<li> // <a data-IDCategory="' + product[i].IDCategory + '" href="./ProductList.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }

    }
    $("#breadCat").html(item);
}

