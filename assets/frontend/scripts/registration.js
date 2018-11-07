var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    Preload();
    PreloadCart();
    SubmitRegistration();
    $(".alert").hide();
    if (queryString("email")) {
        $("input[name=Email]").val(queryString("email"));
        $("input[name=Email]").attr("readonly", "true");
    }
});

function PreloadCart() {
    REST.onSuccess = function (result) {
        if (result.d.success) {

            var currency = result.d.data.Currency;
            if (currency != null) {
                LoadCurrency(currency);
            }

            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }

            if (result.d.data.Cart) {
                $(".TotalPrice").text(result.d.data.Cart.TotalPrice + ' IDR');
                $(".TotalQuantity").text(result.d.data.Cart.TotalQuantity);
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
                    window.location = "Authentication.aspx";
                }
            }
            else {
                $(".loginbut").text("LOGIN / REGISTER");
                $(".btn-logout").hide();
            }


            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                var TotalQuantity = result.d.data.CartSummary.TotalQuantity;
                $(".TotalQuantity").text(TotalQuantity);
                LoadListCartSummary(listCartSummary, TotalPrice);
            }
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'CartSummary', 'Currency']
        }
    });
}

function AutoLogin() {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            //PreloadMaster();
            if (queryString("back")) {
                setTimeout(function () {
                    window.location.href = queryString("back"); //will redirect to your blog page (an ex: blog.html)
                }, 3000); //will call the function after 2 secs.
            }
            else {
                setTimeout(function () {
                    window.location.href = "MyAccount.aspx"; //will redirect to your blog page (an ex: blog.html)
                }, 3000); //will call the function after 2 secs.
            }
        }
        else {
            bootbox.alert(result.d.message);
        }
    };
    REST.sendRequest({
        'c': 'feauth',
        'm': 'login',
        'data': {
            'e': $("#tbEmailSignUp").val(),
            'p': $("#password").val()
        }
    });
}

function Preload() {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            var Country = [];
            for (var i = 0; i < data.Country.length; i++) {
                Country.push({
                    id: data.Country[i].IDCountry,
                    text: data.Country[i].Name
                });

                $("#Country").select2({
                    data: Country,
                    allowClear: false
                }).select2("data", Country[0]).select2("readonly", true);
            }

            var Province = [];
            for (var i = 0; i < data.Province.length; i++) {
                Province.push({
                    id: data.Province[i].IDProvince,
                    text: data.Province[i].Name
                });
            }

            $("#Province").select2({
                data: Province,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: Province[0].id, text: Province[0].text };
                    callback(data);
                }
            });

            $("#Province").on("change", function () {
                LoadCity(+$(this).select2("data").id);
            });

            LoadCity(Province[0].id);
        }
    };
    REST.sendRequest({
        'c': 'fereg',
        'm': 'regpreload',
        'data': {}
    });
}

function LoadCity(idProvince) {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            var City = [];
            for (var i = 0; i < data.length; i++) {
                City.push({
                    id: data[i].IDCity,
                    text: data[i].Name
                });
            }

            $("#City").select2({
                data: City,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: City[0].id, text: City[0].text };
                    callback(data);
                }
            });

            LoadDistrict(City[0].id);
            $("#City").on("change", function () {
                LoadDistrict(+$(this).select2("data").id);
            });
        }
    };
    REST.sendRequest({
        'c': 'fereg',
        'm': 'rcty',
        'data': {
            'IDProvince': idProvince
        }
    });
}

function LoadDistrict(idCity) {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            var District = [];
            for (var i = 0; i < data.length; i++) {
                District.push({
                    id: data[i].IDDistrict,
                    text: data[i].Name
                });
            }

            $("#District").select2({
                data: District,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: District[0].id, text: District[0].text };
                    callback(data);
                }
            });
        }
    };
    REST.sendRequest({
        'c': 'fereg',
        'm': 'rdis',
        'data': {
            'IDCity': idCity
        }
    });
}

function SubmitRegistration() {
    var e = $("#FormReg");
    var t = $("#bootstrap_alert", e);
    jQuery.validator.addMethod("birthdate", function (e, t, n) {
        var date = moment($("#txtBirthdate").val(), "DD-MM-YYYY").format("YYYY-MM-DD");
        if (date == "Invalid date" || $("#txtBirthdate").val() == null || $("#txtBirthdate").val() == undefined)
            return false;
        else
            return true;
    }, jQuery.validator.format("Invalid birthdate"));
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            Password: {
                required: true,
                minlength: 5
            },
            RetypePassword: {
                required: true,
                minlength: 5,
                equalTo: "#password"
            },
            Email: {
                required: true,
                email: true
            },
            Address: {
                required: true
            },
            PhoneNumber: {
                required: true
            },
            PostalCode: {
                required: true
            },
            AddressName: {
                required: true
            },
            Birthdate: {
                birthdate: true
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

            var sendData = {};
            $("input", e).each(function (index, element) {
                // element == this
                if (element.name != undefined && element.name != "") {
                    if (element.name == "Gender") {
                        sendData[element.name] = $('input:radio[name=' + element.name + ']:checked').val();
                        //console.log($('input:radio[name=' + element.name + ']:checked').val());
                    }
                    else if (element.name == "Country" || element.name == "Province" || element.name == "City" || element.name == "District") {
                        sendData[element.name] = $(element).select2("data").id;
                        //console.log($(element).select2("data").text);
                    }
                    else if (element.name == "Newsletter") {
                        if ($("#cbNewsletter").prop("checked")) {
                            sendData[element.name] = true;
                        }
                        else {
                            sendData[element.name] = false;
                        }
                    }
                    else {
                        sendData[element.name] = $(element).val();
                        //console.log($(element).val());
                    }
                }
            });
            sendData["Birthdate"] = moment($("#txtBirthdate").val(), "DD-MM-YYYY").format("YYYY-MM-DD");
            sendData["Address"] = $("#txtAddress").val();
            sendData["Address2"] = $("#txtAddress2").val();
            console.log(sendData);

            REST.onSuccess = function (result) {
                var data = result.d.data;
                t.show();
                if (result.d.success) {
                    Metronic.alert({
                        container: "#bootstrap_alert",
                        place: "append",
                        type: "success",
                        message: result.d.message,
                        close: true,
                        reset: true,
                        focus: true,
                        //closeInSeconds: 5,
                        icon: "check"
                    });
                    AutoLogin();
                }
                else {
                    Metronic.alert({
                        container: "#bootstrap_alert",
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
                'c': 'fereg',
                'm': 'reg',
                'data': sendData
            });
        }
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