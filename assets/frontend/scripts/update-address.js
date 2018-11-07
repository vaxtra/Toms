var REST = new OF({
    'url': '/api/services.asmx/request'
});

var RESTd = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";
var firstLoad = true;

$(document).ready(function () {
    if (queryString("id")) {
        $("#HiddenIDAddress").val(queryString("id"));
        Preload(parseInt(queryString("id")));
    }
    else
        window.location = 'MyAddress.aspx';

    if (queryString("back")) {
        var back = queryString("back");
    }

    $(".btn-save").click(function (e) {
        e.preventDefault();
        var form = $("#form_address");
        form.submit();
        //if (form.valid())
        //    SubmitAddress();
    });
    //SubmitAddress();
    var e = $("#form_address");
    var t = $("#bootstrap_alert", e);
    e.validate({
        errorElement: "p",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        rules: {
            PeopleName: {
                required: true
            },
            Address: {
                required: true
            },
            Phone: {
                required: true
            },
            PostalCode: {
                required: true
            },
            Name: {
                required: true
            }
        },
        messages: {
            PeopleName: {
                required: 'People name is blank'
            },
            Address: {
                required: 'Addres is blank'
            },
            Phone: {
                required: 'Phone is blank'
            },
            PostalCode: {
                required: 'postal code is blank'
            },
            Name: {
                required: 'Name is blank'
            }
        },
        errorPlacement: function (e, t) {
            //if (t.parent(".input-group").size() > 0) {
            //    e.insertAfter(t.parent(".input-group"))
            //} else if (t.attr("data-error-container")) {
            //    e.appendTo(t.attr("data-error-container"))
            //} else if (t.parents(".radio-list").size() > 0) {
            //    e.appendTo(t.parents(".radio-list").attr("data-error-container"))
            //} else if (t.parents(".radio-inline").size() > 0) {
            //    e.appendTo(t.parents(".radio-inline").attr("data-error-container"))
            //} else if (t.parents(".checkbox-list").size() > 0) {
            //    e.appendTo(t.parents(".checkbox-list").attr("data-error-container"))
            //} else if (t.parents(".checkbox-inline").size() > 0) {
            //    e.appendTo(t.parents(".checkbox-inline").attr("data-error-container"))
            //} else if (t.parents(".input-group").size() > 0) {
            //    e.insertAfter(t.parents(".input-group"))
            //} else {
            //    e.insertAfter(t)
            //}
            $(".alert").append(e);
            //e.insertAfter($(".alert"));
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
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "success",
                message: "You have some form errors. Please check below. ",
                close: true,
                reset: true,
                focus: true,
                //closeInSeconds: 5,
                icon: "check"
            });

            var sendData = {};
            $("input", e).each(function (index, element) {
                // element == this
                if (element.name != undefined && element.name != "") {
                    if (element.name == "Country" || element.name == "Province" || element.name == "City" || element.name == "District") {
                        sendData[element.name] = +$(element).select2("data").id;
                        //console.log($(element).select2("data").text);
                    }
                    else {
                        sendData[element.name] = $(element).val();
                        //console.log($(element).val());
                    }
                }
            });
            sendData["Address"] = $("#txtAddress").val();
            sendData["AdditionalInformation"] = $("#txtAdditionalInformation").val();
            sendData["IDCustomer"] = +$("#HiddenIDCustomer").val();
            sendData["IDAddress"] = +$("#HiddenIDAddress").val();

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
                    if (back) {
                        setTimeout('window.location.href="' + back + '"', 3000);
                    }
                    else {
                        setTimeout('window.location.href="MyAddress.aspx"', 3000);
                    }
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
                'c': 'feaddr',
                'm': 'uaddr',
                'data': sendData
            });
        }
    });
});

function Preload(idAddress) {
    REST.onComplete = function (data) {
    };
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {

            var currency = result.d.data.Currency;
            if (currency != null) {
                LoadCurrency(currency);
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
                $(".loginbut").text("LOGIN");
                $(".btn-logout").hide();
            }

            if (result.d.data.Cart) {
                $(".TotalPrice").text(result.d.data.Cart.TotalPrice);
                $(".TotalQuantity").text(result.d.data.Cart.TotalQuantity);
            }

            if (result.d.data.Address != undefined) {
                var address = result.d.data.Address;
                $("#IDProvince").val(address.IDProvince);
                $("#IDCity").val(address.IDCity);
                $("#IDDistrict").val(address.IDDistrict);
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
                    initSelection: function (element, callback) {
                        var province = { id: address.IDProvince, text: address.ProvinceName };
                        callback(data);
                    }
                });

                $("#Province").on("change", function () {
                    LoadCity(+$(this).select2("data").id);
                });
                LoadCity(address.IDProvince);

                RESTd.onComplete = function (data) {
                    if (firstLoad) {
                        $("#Province").select2("data", { id: $("#IDProvince").val(), text: address.ProvinceName })
                        $("#City").select2("data", { id: $("#IDCity").val(), text: address.CityName })
                        $("#District").select2("data", { id: $("#IDDistrict").val(), text: address.DistrictName })
                        firstLoad = false;
                    }
                };

                LoadData(result.d.data.Address);
            }

            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);
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
            '_param_IDAddress': idAddress,
            'RequestData': ['Customer', 'Address', 'Country', 'Province', 'Cart', 'Currency', 'CartSummary']
        }
    });
}

function LoadData(data) {
    var form = $("#form_address");
    $.each(data, function (indexInArray, valueOfElement) {
        //if (indexInArray == "IDCountry") {
        //    $("#Country").select2("data", { id: valueOfElement, text: data.CountryName });
        //}
        //else if (indexInArray == "IDProvince") {
        //    $("#Province").select2("data", { id: valueOfElement, text: data.ProvinceName });
        //}
        //else if (indexInArray == "IDCity") {
        //    $("#City").select2("val", { id: valueOfElement, text: data.CityName });
        //}
        //else if (indexInArray == "IDDistrict") {
        //    $("#District").select2("val", { id: valueOfElement, text: data.DistrictName });
        //}
        //else
        if (indexInArray == "Address") {
            $("#txtAddress").val(data.Address);
        }
        else if (indexInArray == "AdditionalInformation") {
            $("#txtAdditionalInformation").val(data.AdditionalInformation);
        }
        else {
            $("input[name=" + indexInArray + "]").val(valueOfElement);
        }
        
    });
}

function LoadCity(idProvince) {
    REST.onComplete = function (data) { };
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
                //width: "150",
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
    RESTd.onSuccess = function (result) {
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
                //width: "150",
                initSelection: function (element, callback) {
                    var data = { id: District[0].id, text: District[0].text };
                    callback(data);
                }
            });
        }
    };
    RESTd.sendRequest({
        'c': 'fereg',
        'm': 'rdis',
        'data': {
            'IDCity': idCity
        }
    });
}

function LoadCurrency(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        if (data[i].Selected) {
            item += '<option data-id="' + data[i].IDCurrency + '" style="color:red" class="format-money" selected="selected">' + data[i].Name + '</option>';
            format = data[i].Format;
            conversionRate = data[i].ConversionRate;
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