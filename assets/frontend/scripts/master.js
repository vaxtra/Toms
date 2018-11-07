var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $(".btn-logout").click(function (e) {
        e.preventDefault();
        Logout();

    });

    //$("#btnSearchProduct").click(function (e) {
    //    window.location = "Shop.aspx?q=" + $("#txtSearch").val();
    //});

    SearchProduct();

    //PreloadMaster();
});

//function LoadCurrency(data) {
//    var item = '';
//    for (var i = 0; i < data.length; i++) {
//        if (data[i].Selected) {
//            item += '<a data-id="' + data[i].IDCurrency + '" style="color:white">' + data[i].Name + '</a>';
//            format = data[i].Format;
//        }
//        else
//            item += '<a data-id="' + data[i].IDCurrency + '">' + data[i].Name + '</a>';
//    }

//    $(".currency").html(item);
//    $(".currency a").on("click", function () {
//        console.log($(this).data("id"));
//        ChangeCurrency($(this).data("id"));
//    });
//    $(".format-money").formatCurrency({
//        region: format
//    })
//}

//function ChangeCurrency(id) {
//    REST.onSuccess = function (result) {
//        if (result.d.success) {
//            location.reload();
//        }
//    };
//    REST.sendRequest({
//        'c': 'fecur',
//        'm': 'chgcur',
//        'data': {
//            'ID': id
//        }
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
                $(".loginbut").text("Login / Register");
                $(".btn-logout").hide();
            }

            if (result.d.data.CartSummary)
                LoadCartList(result.d.data.CartSummary);

            var Category = result.d.data.Category;
            if (Category != null) {
                LoadCategoryProduct(Category);
                LoadCategoyList(Category);
            }

            $(".format-money").formatCurrency({
                region: "id-ID"
            })

        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'Cart', 'CartSummary', 'Currency']
        }
    });
}

function Logout() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            location.reload();
        }
        else {
            bootbox.alert(result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'logout',
        'data': {}
    });
}

function LoadCartList(data) {
    var total = data.TotalPrice;
    var totalQuantity = data.TotalQuantity;
    data = data.Product;
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
    $(".Subtotal").text(total + ' IDR');
    $(".TotalQuantity").text(totalQuantity);

    $(".format-money").formatCurrency({
        region: "id-ID"
    })

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

function LoadCategoryProduct(product) {
    var item = '';
    for (var i = 0; i < product.length; i++) {
        if (i = 0) {
            item += '<li><a data-IDCategory="' + product[i].IDCategory + '" href="./ProductList.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }
        else {
            item += '<li> // <a data-IDCategory="' + product[i].IDCategory + '" href="./ProductList.aspx?idCategory=' + product[i].IDCategory + '">' + product[i].Name + '</a></li>';
        }

    }
    $("#breadCat").html(item);
}



function SearchProduct() {
    var e = $("#FormSearch");
    var t = $("#bootstrap_alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Search: {
                required: true
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

            window.location = "/Products?q=" + $("#txtSearch").val();
        }
    });
}


