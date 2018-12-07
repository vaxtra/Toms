var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    Preload();

    $("#btnLogout").click(function () {
        Logout();
    });
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }
            if (result.d.data.Cart) {
                $(".TotalPrice").text(result.d.data.Cart.TotalPrice + ' IDR');
                $(".TotalQuantity").text(result.d.data.Cart.TotalQuantity);
            }
            var Customer = result.d.data.Customer;
            if (Customer != null) {
                $("#HiddenIDCustomer").val(Customer.IDCustomer);
                $(".loginbut").text("Welcome, " + Customer.FirstName);
                $(".loginbut").attr("href", "MyAccount.aspx");
                
                $(".btn-logout").show();
            }
            else {
                $(".loginbut").text("LOGIN");
                $(".btn-logout").hide();
                window.location = "Authentication.aspx";
            }

            var Wishlist = result.d.data.Wishlist;
            if (Wishlist != null) {
                LoadWishlist(Wishlist);
            }

            var Category = result.d.data.Category;
            if (Category != null) {
                LoadCategoryMenu(Category);
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
            'RequestData': ['Customer', 'CartSummary', 'Cart', "Wishlist", 'ExpiredNotification'],
        }
    });
}

function LoadWishlist(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<tr>';
        item += '<td><img src="' + data[i].Photo + '" class="img-responsive" /></td>';
        item += '<td>' + data[i].ProductName + '</td>';
        item += '<td>' + data[i].CombinationName + '</td>';
        item += '<td>' + data[i].Date + '</td>';
        item += '<td><i href="#" data-id="' + data[i].IDWishlist + '" class="fa fa-times btn-delwish" style="cursor:pointer;"></i></td>';
        item += '</tr>';
    }
    $(".wishlist tbody").html(item);

    $(".btn-delwish").click(function () {
        DeleteWishlist($(this).data("id"));
    });

    $(".format-money").formatCurrency({ region: 'id-ID' });
}

function DeleteWishlist(idWishlist)
{
    REST.onSuccess = function (result) {
        if (result.d.success) {
            $(".error").html("");
            toastr.success(result.d.message);
            Preload();
        }
        else {
            bootbox.alert(result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'fewish',
        'm': 'dwish',
        'data': { 'IDWishlist': idWishlist }
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

function LoadCategoryMenu(product) {
    var item = '';
    for (var i = 0; i < product.length; i++) {
        item += '<li><a data-IDCategory="' + product[i].IDCategory + '" href="./ProductList.aspx?idCategory=' + product[i].IDCategory + '"># ' + product[i].Name + '</a></li>';
    }
    $(".ProductCategory").html(item);
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