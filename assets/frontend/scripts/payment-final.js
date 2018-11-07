var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    PreloadMaster();

    $(".btn-confirm").click(function (e) {
        e.preventDefault();
        SubmitOrder();
    });
});

function PreloadMaster() {

    REST.onComplete = function (result) { };
    REST.onSuccess = function (result) {
        if (result.d.success) {

            var currency = result.d.data.Currency;
            if (currency != null) {
                LoadCurrency(currency);
            }
            else {
                console.log('currency null');
            }

            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }
            if (result.d.data.CartSummary) {
                $.each(result.d.data.CartSummary, function (indexInArray, valueOfElement) {
                    $("." + indexInArray).text(valueOfElement);
                });
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
                window.location = 'authentication.aspx?back=address.aspx';
                $(".btn-logout").hide();
                $(".btn-login").show();
                $(".FirstName").html("-");
            }

            //LOAD PAYMENT
            var payment = result.d.data.CartSummary.PaymentMethod;
            if (payment) {
                $("#ImagePayment").attr("src", "/assets/images/Payment_Method/" + payment.Image);
                $("#DetailPayment").html(payment.AccountNumber + '<br/>a.n ' + payment.Owner + '<br/>' + payment.Description);
            }
        }
        else {
            bootbox.alert(result.d.message, function (e) {
                window.location = 'authentication.aspx';
            });
        }

        $(".format-money").formatCurrency({
            region: format
        })
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'CartSummary', 'AutoCancel', 'Currency']
        }
    });
}

function LoadCurrency(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        if (data[i].Selected) {
            item += '<a data-id="' + data[i].IDCurrency + '" style="color:red">' + data[i].Name + '</a>';
            format = data[i].Format;
        }
        else
            item += '<a data-id="' + data[i].IDCurrency + '">' + data[i].Name + '</a>';
    }

    $(".currency").html(item);
    $(".currency a").on("click", function () {
        console.log($(this).data("id"));
        ChangeCurrency($(this).data("id"));
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

function SubmitOrder() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            window.location = 'thankyou.aspx';
        }
        else
            console.log(result.d.message);
    };
    REST.sendRequest({
        'c': 'feorder',
        'm': 'iorder',
        'data': {}
    });
}