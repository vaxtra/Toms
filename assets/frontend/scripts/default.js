var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "id-ID";

$(document).ready(function () {

    PreloadMaster();
    Subscribe();
    $(".CatMenu li").each(function () {
        if ($(this).hasClass("active")) {
            $("#HiddenIDCategory").val($(this).data("idcategory"))
        }
    });

    PreloadProductByCategory(+$("#HiddenIDCategory").val());

    $("#NewArrival").click(function () {
        $("#HiddenIDCategory").val($(this).data("idcategory"))
        PreloadProductByCategory($(this).data("idcategory"))
    });

    $("#OnSale").click(function () {
        $("#HiddenIDCategory").val($(this).data("idcategory"))
        PreloadProductByCategory($(this).data("idcategory"))
    });

    $("#BestSeller").click(function () {
        $("#HiddenIDCategory").val($(this).data("idcategory"))
        PreloadProductByCategory($(this).data("idcategory"))
    });
});

//function LoadBestSeller() {
//    REST.onSuccess = function (result) {
//        if (result) {
//            LoadProduct(result.d.data);
//        }
//    };
//    REST.sendRequest({
//        'c': 'fepro',
//        'm': 'rbest',
//        'data': {}
//    });
//}

//function LoadSale() {
//    REST.onSuccess = function (result) {
//        if (result) {
//            LoadProduct(result.d.data);
//        }
//    };
//    REST.sendRequest({
//        'c': 'fepro',
//        'm': 'rsale',
//        'data': {}
//    });
//}

function PreloadMaster() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

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
                }
                else {
                    $(".loginbut").text("GUEST");
                    $(".loginbut").attr("href", "#");
                    $(".btn-logout").hide();
                    $(".btn-newAddress").hide();
                }
            }
            else {
                $(".loginbut").text("Login / Register");
                $(".btn-logout").hide();
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

            var Category = result.d.data.Category;
            if (Category != null) {
                LoadCategoryProduct(Category);
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
            'RequestData': ['Customer', 'Cart', 'CartSummary', 'AutoCancel', 'ExpiredNotification'],
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

function PreloadSlideShow() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            LoadSlideshow(result.d.data.Post.Media);
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Post'],
            'IDPageCategory': 3,
        }
    });
}

function LoadSlideshow(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li><a href="#"><img src="assets/images/post/' + data[i] + '" /></a></li>';
    }
    $(".listimage").html(item);

}

function PreloadProductByCategory(idCategory) {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }

            var Product = result.d.data.ListProduct;
            if (result.d.data.ListProduct) {
                LoadProductByCategory(Product);
            }

            console.log(format);
            $(".format-money").formatCurrency({
                region: format
            })
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['ListProduct'],
            'IDManufacturer': 0,
            '_param_take': 6,
            'IDCategory': 0,
            'IDValue': null,
            'PriceRange':null
        }
    });
}

function LoadProductByCategory(data) {
    var item = '';
    var item2 = '';
    for (var i = 0; i < data.length; i++) {

        item += '<div class="col-md-3 col-sm-6 col-xs-6">';
        item += '<div class="single_feature text-center prodbung">';
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
            item += '<span class="format-money">' + data[i].Price + '</span><del class="format-money" style="color:#ff0000; text-decoration:line-throught;">' + data[i].PriceBeforeDiscount + '</del>';
        }
        else {
            item += '<span class="format-money">' + data[i].Price + '</span>';
        }
        item += '</div>';
        item += '</div>';
        item += '</div>';
    }

    for (var i = 0; i < 4; i++) {

        item2 += '<div class="col-md-3 col-sm-6 col-xs-6">';
        item2 += '<div class="single_feature text-center prodbung">';
        item2 += '<div class="feature-img">';
        item2 += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
        item2 += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
        item2 += '</a>';
        item2 += '<span class="img-hover">';
        item2 += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '">';
        item2 += '<img src="/assets/images/product/' + data[i].Photo + '" alt="' + data[i].Name + '" />';
        item2 += '</a>';
        item2 += '</span>';
        item2 += '</div>';
        item2 += '<div class="feature_text">';
        item2 += '<a href="/ProductDetail/' + data[i].Name.toLowerCase().split('"').join("").split(" ").join("-").split(".").join("") + '"><h4>' + data[i].Name + '</h4></a>';
        if (data[i].PriceBeforeDiscount > data[i].Price) {
            item2 += '<span class="format-money">' + data[i].Price + '</span><del class="format-money" style="color:#ff0000; text-decoration:line-throught;">' + data[i].PriceBeforeDiscount + '</del>';
        }
        else {
            item2 += '<span class="format-money">' + data[i].Price + '</span>';
        }
        item2 += '</div>';
        item2 += '</div>';
        item2 += '</div>';
    }

    $(".LatestTrending").html(item);
    $(".shophome").html(item2);

    $(".format-money").formatCurrency({
        region: format
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

function Subscribe() {
    var e = $("#FormSubscr");
    var t = $("#bootstrap_alert_sub", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Email: {
                required: true,
                email: true
            }
        },
        errorPlacement: function (e, t) {
            if (t.parent(".input-group").size() > 0) {
                e.insertAfter(t.parent(".input-group"))
            } else if (t.attr("data-error-container")) {
                e.appendTo(t.attr("data-error-container"))
            } else if (t.parents(".radio-list").size() > 0) {
                e.appendTo(t.parents(".radio-list").attr("data-error-container"))
            } else if (t.parents(".radio-inline").size() > 0) {
                e.appendTo(t.parents(".radio-inline").attr("data-error-container"))
            } else if (t.parents(".checkbox-list").size() > 0) {
                e.appendTo(t.parents(".checkbox-list").attr("data-error-container"))
            } else if (t.parents(".checkbox-inline").size() > 0) {
                e.appendTo(t.parents(".checkbox-inline").attr("data-error-container"))
            } else if (t.parents(".input-group").size() > 0) {
                e.insertAfter(t.parents(".input-group"))
            } else {
                e.insertAfter(t)
            }
        },
        invalidHandler: function (e, n) {
            t.show();
            Metronic.alert({
                container: "#bootstrap_alert_sub",
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

            REST.onSuccess = function (result) {
                var data = result.d.data;
                if (result.d.success) {
                    toastr.success(result.d.message)
                }
                else {
                    bootbox.alert(result.d.message);
                }
            };
            REST.sendRequest({
                'c': 'femaster',
                'm': 'inews',
                'data': {
                    'email': $("input[name=Email]", e).val()
                }
            });
        }
    });
}

function LoadNotification(data)
{
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
