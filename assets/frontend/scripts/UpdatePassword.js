var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    Preload();

    SubmitUpdatePassword();
    
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

            //SHOPPING CART

            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);

                $(".TotalPrice").text(result.d.data.CartSummary.TotalPrice);
                $(".Subtotal").text(result.d.data.CartSummary.TotalPrice);
                $(".TotalQuantity").text(result.d.data.CartSummary.TotalQuantity);
            }

            var Customer = result.d.data.Customer;
            if (Customer != null) {

                if (Customer.IDCustomer_Group != 2) {
                    $("#HiddenIDCustomer").val(Customer.IDCustomer);
                    $(".FullName").text(Customer.FirstName + " " + Customer.LastName);
                    $(".Email").text(Customer.Email);
                    $(".loginbut").text("Welcome, " + Customer.FirstName);
                    $(".loginbut").attr("href", "MyAccount.aspx");
                    $(".btn-logout").show();

                    $.each(Customer, function (indexInArray, valueOfElement) {
                        if (indexInArray == "Gender")
                            $("input[name=Gender][value=" + valueOfElement + "]").prop("checked", "checked");
                        else if (indexInArray == "Birthday") {
                            $("#ttl").val(moment(valueOfElement).format("DD-MM-YYYY"));
                            moment(valueOfElement).format("DD-MM-YYYY")
                        }
                        else
                            $("input[name=" + indexInArray + "]").val(valueOfElement);
                    });
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
                window.location = "Authentication.aspx";
            }

            $(".format-money").formatCurrency({
                region: format
            })

        }
        else {
            bootbox.alert(result.d.message, function (e) {
                window.location = 'Authentication.aspx';
            });
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'IDCustomer': +$("#HiddenIDCustomer").val(),
            'RequestData': ['Customer', 'CartSummary', 'Currency']
        }
    });
}

function SubmitUpdatePassword() {
    var e = $("#password_form");
    var t = $("#bootstrap_alert_pass", e);
    jQuery.validator.addMethod("birthdate", function (e, t, n) {
        var date = moment($("#tglLahir").val() + "-" + $("#bulanLahir").val() + "-" + $("#tahunLahir").val(), "DD-MM-YYYY").format("YYYY-MM-DD");
        if (date == "Invalid date" || ($("#tglLahir")[0].selectedIndex == 0 || $("#bulanLahir")[0].selectedIndex == 0 || $("#tahunLahir")[0].selectedIndex == 0))
            return false;
        else
            return true;
    }, jQuery.validator.format("Invalid birthdate"));
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {
            OldPassword: {
                required: "old password name cannot blank",
            },
            NewPassword: {
                required: "new password cannot blank",
            },
            ConfirmPassword: {
                required: "please re-type your new password",
                equalTo: "password not match"
            }
        },
        rules: {
            OldPassword: {
                required: true
            },
            NewPassword: {
                required: true,
                minlength: 5,
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#NewPassword"
            }
        },
        errorPlacement: function (e, t) {
            $(".alert").append(e);
        },
        invalidHandler: function (e, n) {
            t.show();
            Metronic.alert({
                container: "#bootstrap_alert_pass",
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

            var sendData = {};
            $("input", e).each(function (index, element) {
                // element == this
                if (element.name != undefined && element.name != "") {
                    sendData[element.name] = $(element).val();
                }
            });
            sendData["IDCustomer"] = +$("#HiddenIDCustomer").val();
            console.log(sendData);

            REST.onSuccess = function (result) {
                var data = result.d.data;
                t.show();
                if (result.d.success) {
                    Metronic.alert({
                        container: "#bootstrap_alert_pass",
                        place: "append",
                        type: "success",
                        message: result.d.message,
                        close: true,
                        reset: true,
                        focus: true,
                        //closeInSeconds: 5,
                        icon: "check"
                    });
                }
                else {
                    Metronic.alert({
                        container: "#bootstrap_alert_pass",
                        place: "append",
                        type: "danger",
                        message: result.d.message,
                        close: true,
                        reset: true,
                        focus: true,
                        //closeInSeconds: 5,
                        icon: "warning"
                    });
                }
            };
            REST.sendRequest({
                'c': 'fecust',
                'm': 'upass',
                'data': sendData
            });
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
