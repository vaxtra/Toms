var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    //if (queryString("idCategory")) {
    //    $("#HiddenIDCategory").val(queryString("idCategory"));
    //}
    //else {
    //    $("#HiddenIDCategory").val(0);
    //}

    $(".pricefilt").click(function () {
        var valueFilt = {};
        valueFilt = GetProductValueFilter();
        LoadProductByFilterSize(valueFilt, 12, 1, +$(this).data("min"), +$(this).data("max"));
    });

    Preload(12, 1);
});

function Preload(take, currentPage) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var currency = result.d.data.Currency;
            if (currency != null) {
                LoadCurrency(currency);
            }
            else {
                console.log('currency null');
            }

            var produks = result.d.data.ListProductPaging;
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

            var Size = result.d.data.Size;
            if (Size) {
                LoadFilterSize(Size);
            }

            var Color = result.d.data.Color;
            if (Color) {
                LoadFilterColor(Color);
            }

            if (result.d.data.ProductCategory) {
                $(".categorynya").text(result.d.data.ProductCategory.Name);
            }
            if (result.d.data.TotalProduct) {
                $(".totalproductnya").text(result.d.data.TotalProduct.data);
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
            'RequestData': ['ListProductPaging', 'CartSummary', 'Customer', 'Category', 'Value', 'Currency', 'ExpiredNotification'],
            'IDManufacturer': 0,
            '_param_take': 0,
            'IDCategory': +$("#HiddenIDCategory").val(),
            'IDAttribute': [1],
            'IDValue': null,
            'PriceRange': null,
            '_page': currentPage,
            'FilterCategory': false,
            'IDParent': 0
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

function LoadCategoryProduct(Category) {
    var item = '';
    for (var i = 0; i < Category.length; i++) {
        item += '<li>';
        item += '<a href="/Products/' + Category[i].Name.split(" ").join("-").toLowerCase() + '">';
        item += Category[i].Name;
        item += '</a>';
        item += '<ul>';
        for (var y = 0; y < Category[i].Child.length; y++) {
            item += '<li>';
            item += '<a href="/Products/' + Category[i].Name.split(" ").join("-").toLowerCase() + '">';
            item += Category[i].Child[y].Name;
            item += '</a>';
        }
        item += '</ul>';
        item += '</li>';

    }
    $("#ProductCategories").html(item);
}
function LoadProduct(data) {
    var item = '';
    var item2 = '';
    if (data.length > 0) {
        var _totalPage = data[0].TotalPage;
        var _currentPage = data[0].CurrentPage;
        var _startPage = data[0].StartPage;
        var _endPage = data[0].EndPage;

        var paging = '';
        if (_currentPage == _startPage)
            paging += '<a href="#" class="right paging-number" data-page="' + parseInt(_currentPage + 1) + '"> Next <i class="fa fa-arrow-right"></i></a>';
        else if (_currentPage == _endPage)
            paging += '<a href="#" class="floatleft paging-number" data-page="' + parseInt(_currentPage - 1) + '"><i class="fa fa-arrow-left">  </i>Previous</a>';
        else {
            paging += '<a href="#" class="floatleft paging-number" data-page="' + parseInt(_currentPage - 1) + '"><i class="fa fa-arrow-left">  </i>Previous</a>';
            paging += '<a href="#" class="right paging-number" data-page="' + parseInt(_currentPage + 1) + '"> Next <i class="fa fa-arrow-right"></i></a>';
        }



        //for (var i = _startPage; i <= _endPage; i++) {
        //    if (i == _currentPage)
        //        paging += '<li><a class="paging-number active" data-page="' + i + '" href="#">' + i + '</a></li>';
        //    else
        //        paging += '<li><a class="paging-number" data-page="' + i + '" href="#">' + i + '</a></li>';
        //}


        $(".paging-prod").html(paging);

        $(".paging-number").click(function (e) {
            e.preventDefault();
            if (queryString("id_manufacturer")) {
                var idman = parseInt(queryString("id_manufacturer"));
                REST.onSuccess = function (result) {
                    LoadProduct(result.d);
                    $('html, body').animate({
                        scrollTop: $(".breadcumb").offset().top - 100
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
                        scrollTop: $(".breadcumb").offset().top - 100
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
                        scrollTop: $(".breadcumb").offset().top - 100
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
                            scrollTop: $(".breadcumb").offset().top - 100
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
            if (data[i].Category.toLowerCase() == "software") {
                item += '<div class="pricing--item">';
                item += '<h3 class="pricing--title">' + data[i].Name + '</h3>';
                item += '<div class="pricing--price format-money">' + data[i].Price + '</div>';
                item += '<div class="description">' + data[i].Description + '</div>';
                item += '<select class="ddlCombination' + data[i].IDProduct + '">'
                for (var y = 0; y < data[i].Combination.length; y++) {
                    item += '<option data-combinationname="' + data[i].Combination[y].Name + '" data-price="' + data[i].Combination[y].Price + '" value="' + data[i].Combination[y].IDProduct_Combination + '">' + data[i].Combination[y].Name.replace("Package : ", "") + '</option>'
                }
                item += '</select>'
                item += '<button class="pricing--action" data-idproduct="' + data[i].IDProduct + '" data-qty="1">Choose plan</button>';
                item += '</div>';
            }
            else if (data[i].Category.toLowerCase() == "add-ons") {
                item2 += '<div class="pricing--item">';
                item2 += '<div class="fancy-title title-dotted-border title-center">';
                item2 += '<h3>' + data[i].Name + '</h3>';
                item2 += '</div>';
                item2 += '<div class="pricing--price format-money">' + data[i].Price + '</div>';
                item2 += '<div class="description">' + data[i].Description + '</div>';
                item2 += 'QTY : <input type="text" name="qty-add-on" class="qty-add-on" value="1" />';
                item2 += '<button class="pricing--action--addon" data-idproduct="' + data[i].IDProduct + '" data-idcombination="' + data[i].IDCombination + '" data-combinationname="' + data[i].CombinationName + '" data-price="' + data[i].CombinationPrice + '">Buy</button>';
                item2 += '</div>';
            }
        }
    }
    else {
        item += '<h5 style="text-align:center;">No Product in this Category</h5>';
    }

    $("#ProductList").html(item);

    $(".addon-product").html(item2);

    $(".pricing--action").click(function () {
        AddToCart(+$(this).data("idproduct"), +$(".ddlCombination" + $(this).data("idproduct") + " option:selected").val(), +$(this).data("qty"), $(".ddlCombination" + $(this).data("idproduct") + " option:selected").data("price"), $(".ddlCombination" + $(this).data("idproduct") + " option:selected").data("combinationname"));
    });

    $(".pricing--action--addon").click(function () {
        AddToCart(+$(this).data("idproduct"), +$(this).data("idcombination"), +$(".qty-add-on").val(), $(this).data("price"), $(this).data("combinationname"));
    });

    console.log(format);
    $(".format-money").formatCurrency({
        region: format
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

function LoadSearchProduct() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var item = '';
            var data = result.d.data;

            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Quantity != 0) {
                        item += '<div class="col-md-3 col-sm-6 col-xs-6">';
                        item += '<div class="single_feature text-center">';
                        item += '<div class="feature-img">';
                        item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
                        item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
                        item += '</a>';
                        item += '<span class="img-hover">';
                        item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
                        item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
                        item += '</a>';
                        item += '</span>';
                        item += '</div>';
                        item += '<div class="feature_text">';
                        item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '"><h4>' + data[i].Name + '</h4></a>';
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
                    else {
                        item += '<div class="col-md-3 col-sm-6 col-xs-6">';
                        item += '<div class="single_feature text-center">';
                        item += '<div class="feature-img">';
                        item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
                        item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
                        item += '</a>';
                        item += '<span class="img-hover">';
                        item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
                        item += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
                        item += '</a>';
                        item += '</span>';
                        item += '</div>';
                        item += '<div class="feature_text">';
                        item += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '"><h4>' + data[i].Name + '</h4></a>';
                        if (data[i].PriceBeforeDiscount > data[i].Price) {
                            item += '<span class="format-money">' + data[i].Price + '</span>&nbsp;&nbsp;<del class="format-money" style="color:#ff0000;">' + data[i].PriceBeforeDiscount + '</del>/<span style="color:#ff0000;">SOLD OUT</span>';
                        }
                        else {
                            item += '<span class="format-money">' + data[i].Price + '</span>/<span style="color:#ff0000;">SOLD OUT</span>';
                        }
                        item += '</div>';
                        item += '</div>';
                        item += '</div>';
                    }

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
        item += '<li class="size-filt">';
        item += '<input type="checkbox" data-idvalue="' + data[i].IDValue + '">';
        item += '<a>' + data[i].Name + '</a><span>' + data[i].TotalProduct + '</span>';
        item += '</li>';
    }

    $(".size-filter").html(item);

    $(".size-filt").click(function () {
        $(this).addClass("sizeactive")
    });
}

function LoadFilterColor(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li class="color-filt">';
        item += '<span style="background:' + data[i].RGBColor + ';"data-idvalue="' + data[i].IDValue + '"></span>';
        item += '</li>';
    }
    item += '<div class="clearfinx"></div>';
    $(".color-filter").html(item);

    $(".color-filt").click(function () {
        $(this).addClass("colactive")
    });
}

function GetProductValueFilter() {
    var arr = [];
    $(".size-filter li.sizeactive").each(function (index, element) {
        // element == this
        if ($(this).prop('checked'))
            arr.push(+$(this).data('idvalue'));
    });
    $(".color-filter li.colactive").each(function (index, element) {
        // element == this
        if ($(this).prop('checked'))
            arr.push(+$(this).data('idvalue'));
    });
    return arr;
}

function LoadProductByFilterSize(idValue, take, currentPage, minprice, maxprice) {
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
                'IDAttribute': 2,
                'IDValue': idValue,
                'PriceRange': [minprice, maxprice],
                '_page': currentPage,
                'FilterCategory': false,
            }
        });
    }
}
function AddToCart(idProduct, idCombination, qty, price, combinationName) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            toastr.success(result.d.message);
            window.location = "/Address";
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
            'OrderType': 'new'
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
