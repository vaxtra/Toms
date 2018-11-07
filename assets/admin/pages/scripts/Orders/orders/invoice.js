var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    if (queryString('id'))
    {
        if (parseInt(queryString("id")) != NaN)
        {
            var id = {};
            
            id = GetIDOrder(queryString("id"));
            
            Preload(id);
            //PreloadShopInfo();
        }
    }

    else
        window.localtion = 'default.aspx';
});

function Preload(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            //$.each(result.d.data.DetailOrder, function (indexInArray, valueOfElement) {
            //    if (valueOfElement != null) {
            //        var date = '';
            //        if (indexInArray == "Date") {
            //            $("." + indexInArray).text(moment(valueOfElement).format("DD MMMM YYYY"));
            //            date = moment(valueOfElement).format("DD/MM/YYYY HH:mm");
            //        }
            //        else if (indexInArray == "InvoiceNumber") {
            //            $("." + indexInArray).html(valueOfElement + "<br/>" + date);
            //        }
            //        else if (indexInArray == "BillingAddress") {
            //            $(".billing-address").html(valueOfElement.PeopleName + '<br/>' + valueOfElement.Address + '<br/>' + valueOfElement.DistrictName + '<br/>' + valueOfElement.CityName + '<br/>' + valueOfElement.ProvinceName + '<br/>' + valueOfElement.CountryName + '<br/>' + valueOfElement.PostalCode + '<br/>' + valueOfElement.Phone + '<br/>');
            //        }
            //        else if (indexInArray == "DeliveryAddress") {
            //            $(".delivery-address").html(valueOfElement.PeopleName + '<br/>' + valueOfElement.Address + '<br/>' + valueOfElement.DistrictName + '<br/>' + valueOfElement.CityName + '<br/>' + valueOfElement.ProvinceName + '<br/>' + valueOfElement.CountryName + '<br/>' + valueOfElement.PostalCode + '<br/>' + valueOfElement.Phone + '<br/>');
            //        }
            //        else if (indexInArray == "Product") {
            //            LoadProduct(valueOfElement);
            //        }
            //        else
            //            $("." + indexInArray).text(valueOfElement);

            //        if (indexInArray == "OrderStatus")
            //            $("#HiddenStatus").val(valueOfElement);
            //    }
            //});
            var Invoice = result.d.data.Invoice;
            if (Invoice) {
                LoadListInvoice(Invoice);
            }
            var ShopInfo = result.d.data.ShopInfo;
            $(".shopCity").text(ShopInfo[1].Value);
            $(".shopEmail").text(ShopInfo[0].Value);
            $(".shopAddress").text(ShopInfo[2].Value);
            $(".shopPhone").text(ShopInfo[3].Value);
        }
    };
    REST.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'IDOrder': id,
            'RequestData': ['Invoice', 'ShopInfo']
        }
    });
}

function LoadDeliveryAddress(data) {
    var item = "";
    item += '<li><span class="name">' + data.PeopleName + '</span></li>';
    item += '<li>' + data.Address + '</li>';
    item += '<li>' + data.PostalCode + ' ' + data.CityName + '</li>';
    item += '<li>Indonesia</li>';
    $(".delivery-address").html(item);
}

function LoadBillingAddress(data) {
    var item = "";
    item += '<li><span class="name">' + data.PeopleName + '</span></li>';
    item += '<li>' + data.Address + '</li>';
    item += '<li>' + data.PostalCode + ' ' + data.CityName + '</li>';
    item += '<li>Indonesia</li>';
    $(".billing-address").html(item);
}

function LoadProduct(data) {
    var row = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            row += '<tr>';
            row += '<td>' + data[i].ProductName + '</td>';
            row += '<td>' + data[i].CombinationName + '</td>';
            row += '<td>' + data[i].ReferenceCode + '</td>';
            row += '<td>' + data[i].Price + '</td>';
            row += '<td>' + data[i].Quantity + '</td>';
            row += '<td>' + data[i].TotalPrice + '</td>';
            row += '<td>' + data[i].Discount + '</td>';
            row += '<td>' + data[i].Subtotal + '</td>';
            row += '</tr>';
        }
        $(".product-table tbody").html(row);
    }
}

function GetIDOrder(id) {
    var partsid = [];
    var parts = id.split(",");
    $.each(parts, function () {
        partsid.push(parseInt(this));
    })
    return partsid;
}

function LoadListInvoice(Invoice) 
{
    var item = '';
    for (var i = 0; i < Invoice.length; i++) {
        item += '<div class="row">';
        item += '<div class="invoice">';
        item += '<div class="row invoice-logo">';
        item += '<div class="col-xs-6 invoice-logo-space">';
        item += '<h1><b>';
        item += '' + Invoice[i].ShopTitle + '</b></h1>';
        item += '</div>';
        item += '<div class="col-xs-6">';
        item += '<p class="InvoiceNumber">' + Invoice[i].InvoiceNumber + '';
        item += '</p>';
        item += '<p class="Date"></p>' + moment(Invoice[i].Date).format("DD MMMM YYYY") + '';
        item += '</div>';
        item += '</div>';
        item += '<hr />';
        item += '<div class="row">';
        item += '<div class="col-xs-4">';
        item += '<h4>Client:</h4>';
        item += '<ul class="list-unstyled">';
        item += '<li class="CustomerName">' + Invoice[i].CustomerName + '';
        item += '</li>';
        item += '<li class="CustomerEmail">' + Invoice[i].CustomerEmail + '';
        item += '</li>';
        item += '</ul>';
        item += '</div>';
        item += '<div class="col-xs-4">';
        item += '<h4>Billing Address:</h4>';
        item += '<div class="billing-address">';
        item += '<li><span class="name">' + Invoice[i].BillingAddress.PeopleName + '</span></li>';
        item += '<li>' + Invoice[i].BillingAddress.Address + '</li>';
        item += '<li>' + Invoice[i].BillingAddress.PostalCode + ' ' + Invoice[i].BillingAddress.CityName + '</li>';
        item += '<li>Indonesia</li>';
        item += '</div>';
        item += '</div>';
        item += '<div class="col-xs-4 invoice-payment">';
        item += '<h4>Shipping Address:</h4>';
        item += '<div class="delivery-address">';
        item += '<li><span class="name">' + Invoice[i].DeliveryAddress.PeopleName + '</span></li>';
        item += '<li>' + Invoice[i].DeliveryAddress.Address + '</li>';
        item += '<li>' + Invoice[i].DeliveryAddress.PostalCode + ' ' + Invoice[i].DeliveryAddress.CityName + '</li>';
        item += '<li>Indonesia</li>';
        item += '</div>';
        item += '</div>';
        item += '</div>';
        item += '<div class="row">';
        item += '<div class="col-xs-12">';
        item += '<table class="table table-striped table-hover product-table">';
        item += '<thead>';
        item += '<tr>';
        item += '<th>Product';
        item += '</th>';
        item += '<th>Combination';
        item += '</th>';
        item += '<th>Reference';
        item += '</th>';
        item += '<th>Price';
        item += '</th>';
        item += '<th>Quantity';
        item += '</th>';
        item += '<th>TotalPrice';
        item += '</th>';
        item += '<th>Discount';
        item += '</th>';
        item += '<th>Subtotal';
        item += '</th>';
        item += '</tr>';
        item += '</thead>';
        item += '<tbody>';
        
        for (var y = 0; y < Invoice[i].Product.length; y++) {
            item += '<tr>';
            item += '<td>' + Invoice[i].Product[y].ProductName + '</td>';
            item += '<td>' + Invoice[i].Product[y].CombinationName + '</td>';
            item += '<td>' + Invoice[i].Product[y].ReferenceCode + '</td>';
            item += '<td class="format-money">' + Invoice[i].Product[y].Price + '</td>';
            item += '<td>' + Invoice[i].Product[y].Quantity + '</td>';
            item += '<td class="format-money">' + Invoice[i].Product[y].TotalPrice + '</td>';
            item += '<td class="format-money">' + Invoice[i].Product[y].Discount + '</td>';
            item += '<td class="format-money">' + Invoice[i].Product[y].Subtotal + '</td>';
            item += '</tr>';
        }

        item += '</tbody>';
        item += '</table>';
        item += '</div>';
        item += '</div>';
        item += '<div class="row">';
        item += '<div class="col-xs-4">';
        item += '<div class="well">';
        item += '<address>';
        item += '<strong><label>' + Invoice[i].ShopTitle + '</label></strong><br />';
        item += '-<br />';
        item += '<span class="shopCity">' + Invoice[i].ShopCity + '</span>';
        item += '<br />';
        item += '<span class="shopAddress">' + Invoice[i].ShopAddress + '</span><br />';
        item += '[P/F] <span class="shopPhone">' + Invoice[i].ShopPhone + '</span><br />';
        item += '<span class="shopEmail">' + Invoice[i].ShopEmail + '</span>';
        item += '</address>';
        item += '</div>';
        item += '</div>';
        item += '<div class="col-xs-8 invoice-block">';
        item += '<ul class="list-unstyled amounts">';
        item += '<li>';
        item += '<strong>Sub Total: </strong>';
        item += '<label class="TotalPrice">';
        item += '<span class="format-money">' + Invoice[i].TotalPrice + '</span>';
        item += '</label>';
        item += '</li>';
        item += '<li>';
        item += '<strong>Voucher: </strong>';
        item += '<label class="TotalDiscount format-money">' + Invoice[i].TotalDiscount + '</label>';
        item += '</li>';
        item += '<li>';
        item += '<strong>Shipping: </strong>';
        item += '<label class="TotalShipping format-money">' + Invoice[i].TotalShipping + '</label>';
        item += '</li>';
        item += '<li>';
        item += '<strong>Grand Total: </strong>';
        item += '<label class="TotalPaid format-money">' + Invoice[i].TotalPaid + '</label>';
        item += '</li>';
        item += '</ul>';
        item += '</div>';
        item += '</div>';
        item += '</div>';
        item += '</div>';
    }

    $(".list-invoice").html(item);

    $(".format-money").formatCurrency({ region: 'id-ID' });
}