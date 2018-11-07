var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    Preload();
    
    $("#btnLogout").click(function () {
        Logout();
    });
});

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            var currency = result.d.data.Currency;
            if (currency != null) {
                LoadCurrency(currency);
            }
            else {
                console.log('currency null');
            }

            if (result.d.data.Addresses)
                LoadAddress(result.d.data.Addresses);

            var Customer = result.d.data.Customer;
            if (Customer != null) {
                if (Customer.IDCustomer_Group != 2) {
                    $(".loginbut").text("Welcome, " + Customer.FirstName);
                    $(".loginbut").attr("href", "MyAccount.aspx");
                    $("#HiddenIDCustomer").val(Customer.IDCustomer);
                    $(".btn-logout").show();
                    $(".btn-newAddress").show();

                    if (queryString('back'))
                        window.location = queryString('back');
                    $(".btn-logout").show();
                    $(".btn-login").hide();
                    $(".FirstName").html(Customer.FirstName);
                }
                else {
                    $(".loginbut").text("GUEST");
                    $(".loginbut").attr("href", "#");
                    $(".btn-logout").hide();
                    $(".btn-newAddress").hide();
                    window.location = "Authentication.aspx";
                }
            }
            else {
                $(".loginbut").text("LOGIN");
                $(".btn-logout").hide();
            }

            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);

                $(".TotalPrice").text(result.d.data.CartSummary.TotalPrice);
                $(".Subtotal").text(result.d.data.CartSummary.TotalPrice);
                $(".TotalQuantity").text(result.d.data.CartSummary.TotalQuantity);
            }

            $(".format-money").formatCurrency({
                region: format
            })
                
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'IDCustomer': +$("#HiddenIDCustomer").val(),
            'RequestData': ['Customer', 'Addresses', 'CartSummary', 'Currency']
        }
    });
}

function LoadAddress(data) {
    var item = '';

    $.each(data, function (indexInArray, valueOfElement) {
        item += '<div class="col-md-6 margbot">';
        item += '<div class="box-erigo theddress">';
        item += '<h4>' + valueOfElement.Name + '</h4>';
        item += '<hr class="infoline" />';
        item += '<p>' + valueOfElement.Address + '</p>';
        item += '<p>' + valueOfElement.CountryName + '</p>';
        item += '<p>' + valueOfElement.CityName + '</p>';
        item += '<p>' + valueOfElement.DistrictName + ' ' + valueOfElement.PostalCode + '</p>';
        item += '<p>' + valueOfElement.Phone + '</p>';
        item += '<br/>'
        item += '<a class="btn btn-block btn-erigo btn-black" style="width:50%;float:left;" data-idaddress="' + valueOfElement.IDAddress + '" href="UpdateAddress.aspx?id=' + valueOfElement.IDAddress + '">Update</a><a href="#" data-idaddress="' + valueOfElement.IDAddress + '" class="btn btn-block btn-erigo btn-red btn-delete" style="width:50%;float:left;margin-top:0;">&nbsp;&nbsp;Delete</a>';
        item += '<div class="clearfix"></div>'
        item += '</div>';
        item += '</div>';
    });
    $(".listaddress").html(item);

    $(".btn-delete").click(function (e) {
        e.preventDefault();
        var id = $(this).data('idaddress');
        bootbox.confirm('Are you sure?', function (result) {
            if (result)
                DeleteAddress(id);
        });
    });
}

function DeleteAddress(id) {
    REST.onSuccess = function (result) {
        if (result.d.success)
            location.reload();
        else
            bootbox.alert(result.d.message);
    };
    REST.sendRequest({
        'c': 'feaddr',
        'm': 'daddr',
        'data': {
            'IDAddress': id,
            'IDCustomer': +$("#HiddenIDCustomer").val()
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