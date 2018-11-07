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
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'CartSummary', 'Cart', "Wishlist"],
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