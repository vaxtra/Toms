var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    if (queryString("id")) {
        if (parseInt(queryString("id")) != NaN)
        {
            var id = {};
            
            id = GetIDOrder(queryString("id"));
            
            Preload(id);
            //PreloadShopInfo();
        }
            
    }
    else
        window.location = 'default.aspx';
});

function queryString(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function Preload(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var item = '';
            var shippingLab = result.d.data;
            if (shippingLab.length > 0) {
                document.title = "Shipping Label | " + shippingLab[0].ShopTitle;
            }
            for (var i = 0; i < shippingLab.length; i++) {
                item += '<div class="row">';
                item += '<div class="col-xs-4 text-center margtop">';
                item += '<div class="col-xs-12">';
                item += '<img src="/assets/images/email_logo/' + shippingLab[i].EmailLogo + '" class="img-responsive imgLogo" style="width:100%;" />';
                item += '</div>';
                item += '<p style="font-size: 120%;margin-top: 10px;">';
                item += 'for more info or order<br />';
                item += '<span style="letter-spacing: 1px;font-weight:bold"><asp:Label ID="lblShopName" runat="server" Text="' + shippingLab[i].ShopName + '"></asp:Label></span><br />';
                item += 'or contact us<br />';
                item += '<span class="shopPhone">' + shippingLab[i].ShopPhone + '</span> (PHONE)<br />';
                item += '<span class="shopEmail">' + shippingLab[i].ShopEmail + '</span> (EMAIL)';
                item += '</p>';
                item += '</div>';
                item += '<div class="col-xs-8 bg">';
                item += '<div class="col-xs-12 bg-inner">';
                item += '<div class="col-xs-12 borbot">';
                item += '<div class="col-xs-1 text-center" style="padding: 10px 10px;">';
                item += '<i class="fa fa-user fa-lg"></i>';
                item += '</div>';
                item += '<label class="col-xs-11 labelnya">';
                item += '' + shippingLab[i].Name + '</label>';
                item += '</div>';
                item += '<div class="col-xs-12 borbot">';
                item += '<div class="col-xs-1 text-center" style="padding: 10px 10px;">';
                item += '<i class="fa fa-home fa-lg"></i>';
                item += '</div>';
                item += '<label class="col-xs-11 labelnya">';
                item += '' + shippingLab[i].Address + ' - ' + shippingLab[i].PostalCode + '<br />';
                item += '' + shippingLab[i].District + ' - ';
                item += '' + shippingLab[i].City + ', ' + shippingLab[i].Province + ', ' + shippingLab[i].Country;
                item += '</label>'; 
                item += '</div>';
                item += '<div class="col-xs-12 borbot">';
                item += '<div class="col-xs-1 text-center" style="padding: 10px 10px;">';
                item += '<i class="fa fa-phone fa-lg"></i>';
                item += '</div>';
                item += '<label class="col-xs-11 labelnya">';
                item += '' + shippingLab[i].Phone + '</label>';
                item += '</div>';
                item += '<div class="col-xs-12 borbot">';
                item += '<div class="col-xs-1 text-center" style="padding: 10px 10px;">';
                item += '<i class="fa fa-shopping-cart fa-lg"></i>';
                item += '</div>';
                item += '<label class="col-xs-11 labelnya">';
                item += '' + shippingLab[i].Product + '</label>';
                item += '</div>';
                item += '<div class="col-xs-12 borbot">';
                item += '<div class="col-xs-1 text-center" style="padding: 10px 10px;">';
                item += '<i class="fa fa-truck fa-lg"></i>';
                item += '</div>';
                item += '<label class="col-xs-11 labelnya">';
                item += '' + shippingLab[i].ShippingMethod + '</label>';
                item += '</div>';
                item += '<div class="col-xs-12 borbot" style="border-bottom: 0!important;">';
                item += '<div class="col-xs-1 text-center" style="padding: 10px 10px;">';
                item += '<i class="fa fa-info fa-lg"></i>';
                item += '</div>';
                item += '<label class="col-xs-11 labelnya">' + shippingLab[i].Notes + '</b></label>';
                item += '</div>';
                item += '</div>';
                item += '</div>';
                item += '</div>';
                item += '</div>';
            }

            $(".ShippingLabel").html(item);

            //$.each(result.d.data, function (indexInArray, valueOfElement) {
            //    $("." + indexInArray).html(valueOfElement);
            //});
        }
    };
    REST.sendRequest({
        'c': 'beord',
        'm': 'shiplab',
        'data': { 'IDOrder': id }
    });
}

//function PreloadShopInfo()
//{
//    REST.onSuccess = function (result) {
//        if (result.d.success) {
//            var ShopInfo = result.d.data.ShopInfo;
//            $(".shopPhone").text(ShopInfo[3].Value);
//            $(".shopEmail").text(ShopInfo[0].Value);
//            $(".imgLogo").attr("src", "/assets/images/email_logo/" + ShopInfo[4].Value)
//        }
//    };
//    REST.sendRequest({
//        'c': 'bemaster',
//        'm': 'preload',
//        'data': {
//            'RequestData': ['ShopInfo', 'ShopTitle', 'EmailLogo']   
//        }
//    });
//}

function GetIDOrder(id) {
    var partsid = [];
    var parts = id.split(",");
    $.each(parts, function () {
        partsid.push(parseInt(this));
    })
    return partsid;
}

