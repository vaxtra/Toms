var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {

    $("#menu_orders").addClass("active");
    $("#arrow_orders").addClass("open");
    $("#submenu_orders").addClass("active");

    if (queryString('id')) {
        $("#HiddenIDOrder").val(queryString("id"));
        Preload(+$("#HiddenIDOrder").val());
    }

    $(".btn-invoice").click(function (e) {
        e.preventDefault();
        window.location = "invoice.aspx?id=" + $("#HiddenIDOrder").val();
    });

    $("#btnUbahStatus").click(function (e) {
        if ($("#HiddenStatus").val() == "Cancel") {
            bootbox.alert('order canceled, cannot change status order');
        }
        else {
            e.preventDefault();
            $(this).addClass("hidden");
            $("#btnSaveStatus").removeClass("hidden");
            $("#btnCancel").removeClass("hidden");

            LoadStatusOrder();
        }
    });

    $("#btnCancel").click(function (e) {
        e.preventDefault();
        $(this).addClass("hidden");
        $("#btnSaveStatus").addClass("hidden");
        $("#btnUbahStatus").removeClass("hidden");

        $(".OrderStatus").html($("#HiddenStatus").val());

        if ("Delivered" == $("#HiddenStatus").val())
            $(".ShippingNumber-div").removeClass("hidden");
        else
            $(".ShippingNumber-div").addClass("hidden");
    });

    $("#btnSaveStatus").click(function (e) {
        e.preventDefault();
        var shippingNo = "";
        if ($("#ddlOrderStatus option:selected").text() == "Cancel") {
            bootbox.confirm('Are You sure to cancel order?', function (result) {
                if (result) {
                    if ($("#ShippingNumber").val())
                        shippingNo = $("#ShippingNumber").val();
                    UpdateOrderStatus(+$("#HiddenIDOrder").val(), +$("#ddlOrderStatus").val(), shippingNo);
                }
            });
        }
        else {
            if ($("#ShippingNumber").val())
                shippingNo = $("#ShippingNumber").val();
            UpdateOrderStatus(+$("#HiddenIDOrder").val(), +$("#ddlOrderStatus").val(), shippingNo);
        }
    });

    $("#btnDetailConfirm").click(function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        if (id == 0) {
            bootbox.alert('Payment not confirmed yet');
        }
        else {
            REST.onSuccess = function (result) {
                if (result.d.success) {
                    $.each(result.d.data, function (indexInArray, valueOfElement) {
                        if (indexInArray == 'PaymentDate')
                            $("." + indexInArray).html(moment(valueOfElement).format("DD-MM-YYYY HH:mm"));
                        else
                            $("." + indexInArray).html(valueOfElement);
                    });
                    $("#basic").modal('show');
                }
            };
            REST.sendRequest({
                'c': 'beord',
                'm': 'rconford',
                'data': { 'IDPaymentConfirmation': +$(this).data('id') }
            });
        }
    });
});

function Preload(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            $.each(result.d.data.DetailOrder, function (indexInArray, valueOfElement) {
                if (valueOfElement != null) {
                    if (indexInArray == "Date" | indexInArray == "DateUpdate")
                        $("." + indexInArray).text(moment(valueOfElement).format("DD/MM/YYYY HH:mm"));
                    else if (indexInArray == "BillingAddress") {
                        $(".billing-address").html(valueOfElement.PeopleName + '<br/>' + valueOfElement.Address + '<br/>' + valueOfElement.DistrictName + '<br/>' + valueOfElement.CityName + '<br/>' + valueOfElement.ProvinceName + '<br/>' + valueOfElement.CountryName + '<br/>' + valueOfElement.PostalCode + '<br/>' + valueOfElement.Phone + '<br/>');
                    }
                    else if (indexInArray == "DeliveryAddress") {
                        $(".delivery-address").html(valueOfElement.PeopleName + '<br/>' + valueOfElement.Address + '<br/>' + valueOfElement.DistrictName + '<br/>' + valueOfElement.CityName + '<br/>' + valueOfElement.ProvinceName + '<br/>' + valueOfElement.CountryName + '<br/>' + valueOfElement.PostalCode + '<br/>' + valueOfElement.Phone + '<br/>');
                    }
                    else if (indexInArray == "Product") {
                        LoadProduct(valueOfElement);
                    }
                    else if (indexInArray == "ShippingNumber") {
                        if (valueOfElement != "") {
                            $("#ShippingNumber").val(valueOfElement);
                            $(".ShippingNumber-div").removeClass("hidden");
                        }
                    }
                    else if (indexInArray == "IDPaymentConfirm") {
                        $("#btnDetailConfirm").data('id', valueOfElement);
                    }
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
            'IDOrder': id,
            'RequestData': ['DetailOrder']
        }
    });
}

function LoadProduct(data) {
    var row = '';
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            row += '<tr>';
            row += '<td>' + data[i].ProductName + '</td>';
            row += '<td>' + data[i].CombinationName + '</td>';
            row += '<td>' + data[i].ReferenceCode + '</td>';
            row += '<td class="money">' + data[i].OriginalPrice + '</td>';
            row += '<td>' + data[i].Quantity + '</td>';
            row += '<td class="money">' + data[i].TotalPrice + '</td>';
            row += '<td class="money">' + data[i].TotalDiscount + '</td>';
            row += '<td class="money">' + data[i].Subtotal + '</td>';
            row += '</tr>';
        }
        $(".product-table tbody").html(row);
        $(".money").formatCurrency({ region: 'id-ID' });
    }
}

function LoadStatusOrder() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var data = result.d.data;
            var ddl = '<select id="ddlOrderStatus">';
            for (var i = 0; i < data.length; i++) {
                if (data[i].Name == $("#HiddenStatus").val()) {
                    ddl += '<option data-isdelivery="' + data[i].Delivery + '" selected="true" value="' + data[i].IDOrderStatus + '">' + data[i].Name + '</option>';
                    if (data[i].Delivery) {
                        $(".ShippingNumber-div").removeClass("hidden");
                    }
                }
                else
                    ddl += '<option data-isdelivery="' + data[i].Delivery + '" value="' + data[i].IDOrderStatus + '">' + data[i].Name + '</option>';


            }
            ddl += '</select>';
            $(".OrderStatus").html(ddl);

            $("#ddlOrderStatus").change(function (e) {
                e.preventDefault();
                if ($("option:selected", this).data("isdelivery"))
                    $(".ShippingNumber-div").removeClass("hidden");
                else
                    $(".ShippingNumber-div").addClass("hidden");
                //console.log($("option:selected", this).data("isdelivery"));
            });
        }
        //if ($("#HiddenStatus").val()) {
        //    $("#ddlOrderStatus").val($("#HiddenStatus").val());
        //}
    };
    REST.sendRequest({
        'c': 'beord',
        'm': 'rstord',
        'data': {}
    });
}

function UpdateOrderStatus(idOrder, idOrderStatus, shippingNumber) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
            setTimeout(function () {
                location.reload();
            }, 1000);
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "error",
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "warning"
            });
        }
    };
    REST.sendRequest({
        'c': 'beord',
        'm': 'ustord',
        'data': { 'IDOrderStatus': idOrderStatus, 'IDOrder': idOrder, 'ShippingNumber': shippingNumber }
    });
}