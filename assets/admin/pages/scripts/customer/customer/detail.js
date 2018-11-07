var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {

    $("#menu_customers").addClass("active");
    $("#arrow_customers").addClass("open");
    $("#submenu_customers").addClass("active");

    if (queryString('id')) {
        $("#HiddenIDCustomer").val(queryString("id"));
        Preload(+$("#HiddenIDCustomer").val());
    }
    else {
        window.location.href = "Default.aspx";
    }
});

function Preload(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            $.each(result.d.data.DetailCustomer, function (indexInArray, valueOfElement) {
                if (valueOfElement != null) {
                    if (indexInArray == "Birthday")
                        $("." + indexInArray).text(moment(valueOfElement).format("DD-MM-YYYY HH:mm"));
                    else if (indexInArray == "DateRegistered")
                        $("." + indexInArray).text(moment(valueOfElement).format("DD/MM/YYYY HH:mm"));
                    else if (indexInArray == "Gender") {
                        if (valueOfElement == "L") {
                            $("." + indexInArray).text("Male");
                        }
                        else if (valueOfElement == "P") {
                            $("." + indexInArray).text("Female");
                        }
                        else {
                            $("." + indexInArray).text("Unknown");
                        }
                    }
                    else if (indexInArray == "Status" || indexInArray == "Subscribe") {
                        if (valueOfElement == true) {
                            $("." + indexInArray).html('<i class="fa fa-check" style="color:green;"></i>');
                        }
                        else {
                            $("." + indexInArray).html('<i class="fa fa-times" style="color:red;"></i>');
                        }
                    }
                    else if (indexInArray == "Order") {
                        LoadOrder(valueOfElement);
                    }
                    else if (indexInArray == "Address") {
                        LoadAddress(valueOfElement);
                    }
                    else if (indexInArray == "Product") {
                        LoadProduct(valueOfElement);
                    }
                        //else if (indexInArray == "Address") {
                        //    LoadAddress(valueOfElement);
                        //}
                        //else if (indexInArray == "Voucher") {
                        //    LoadVoucher(valueOfElement);
                        //}
                        //else if (indexInArray == "Order") {
                        //    LoadOrder(valueOfElement);
                        //}
                    else
                        $("." + indexInArray).text(valueOfElement);

                    if (indexInArray == "OrderStatus")
                        $("#HiddenStatus").val(valueOfElement);
                    if (indexInArray == "ConfirmPayment") {
                        $("#imgBankwire").attr("src", "/assets/images/payment_confirmation/" + valueOfElement.Image);
                    }
                }
            });
            $(".money").formatCurrency({ region: 'id-ID' });
        }
    };
    REST.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'IDCustomer': id,
            'RequestData': ['DetailCustomer']
        }
    });
}

function LoadOrder(data) {
    var row = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            row += '<tr>';
            row += '<td>' + data[i].Reference + '</td>';
            row += '<td>' + moment(data[i].Date).format("DD/MM/YYYY HH:mm") + '</td>';
            row += '<td>' + data[i].Shipping + '</td>';
            row += '<td>' + data[i].PaymentMethod + '</td>';
            row += '<td>' + data[i].OrderStatus + '</td>';
            row += '<td>' + data[i].TotalProduct + '</td>';
            row += '<td class="money">' + data[i].TotalPaid + '</td>';
            row += '<td><a href="/adminwitcommerce/orders/orders/detail.aspx?id=' + data[i].IDOrder + '" data-id="' + data[i].IDOrder + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a></td>';
            row += '</tr>';
        }
        $(".order-table tbody").html(row);
        $(".money").formatCurrency({ region: 'id-ID' });
    }
}

function LoadAddress(data) {
    var row = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            row += '<tr>';
            row += '<td>' + data[i].Name + '</td>';
            row += '<td>' + data[i].PeopleName + '</td>';
            row += '<td>' + data[i].Address + '<br/>' + data[i].DistrictName + ', ' + data[i].CityName + '<br/>' + data[i].ProvinceName + ', ' + data[i].CountryName + '<br/>' + data[i].PostalCode + '</td>';
            row += '<td>' + data[i].Phone + '</td>';
            row += '</tr>';
        }
        $(".address-table tbody").html(row);
        $(".money").formatCurrency({ region: 'id-ID' });
    }
}

function LoadProduct(data)
{
    var row = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            row += '<tr>';
            row += '<td>' + data[i].Date + '</td>';
            row += '<td>' + data[i].ProductName + '</td>';
            row += '<td>' + data[i].CombinationName + '</td>';
            row += '<td>' + data[i].Quantity + '</td>';
            row += '</tr>';
        }
        $(".productBought-table tbody").html(row);
        $(".money").formatCurrency({ region: 'id-ID' });
    }
}