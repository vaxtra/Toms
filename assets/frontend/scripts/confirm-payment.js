var REST = new OF({
    'url': '/api/services.asmx/Request'
});

$(document).ready(function () {
    Preload();
    $(".date-picker").datepicker({
        format: 'dd/mm/yyyy'
    });

    $(".time-picker").timepicker({
        minuteStep: 1,
        showMeridian: false
    });

    $("input[name=Image]").on('change', (function (e) {
        e.preventDefault();
        readURL(this);
    }));
    $("#btnLogout").click(function () {
        Logout();
    });

    $("#IDOrderGuest").focusout(function () {
        GetIDOrderByReference($(this).val());
    });

    SubmitConfirm();
});

function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#HiddenImage").val(e.target.result);
            //$('#blah').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

function Preload() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            if (result.d.data.token != undefined) {
                $("#token").val(result.d.data.token);
            }
            if (result.d.data.Cart) {
                $(".TotalPrice").text(result.d.data.Cart.TotalPrice);
                $(".Subtotal").text(result.d.data.Cart.TotalPrice);
                $(".TotalQuantity").text(result.d.data.Cart.TotalQuantity);
            }
            var Customer = result.d.data.Customer;
            if (Customer != null) {
                if (Customer.IDCustomer_Group != 2) {
                    $("#HiddenIDCustomer").val(Customer.IDCustomer);
                    $(".FullName").text(Customer.FirstName + " " + Customer.LastName);
                    $(".EmailConfirm").val(Customer.Email);
                    $(".loginbut").text("Welcome, " + Customer.FirstName);
                    $(".loginbut").attr("href", "MyAccount.aspx");
                    $(".btn-logout").show();
                    $(".ddlOrder").show();
                    $("#IDOrderGuest").hide();
                }
                else {
                    $(".loginbut").text("GUEST");
                    $(".loginbut").attr("href", "#");
                    $(".btn-logout").hide();

                    $(".ddlOrder").hide();
                    $(".textorder").show();
                    $("#IDOrderGuest").show();
                    $("input[name=Email]").removeAttr("readonly");
                }
            }
            else {
                $(".loginbut").text("LOGIN");
                $(".btn-logout").hide();
                $(".ddlOrder").hide();
                $(".textorder").show();
                $("#IDOrderGuest").show();
                $("input[name=Email]").removeAttr("readonly");
                //window.location = "Authentication.aspx";
            }
            var order = result.d.data.OrderNumber;
            if (order != null)
                LoadOrderNumber(order);

            if (result.d.data.CartSummary) {
                var listCartSummary = result.d.data.CartSummary.Product;
                var TotalPrice = result.d.data.CartSummary.TotalPrice;
                LoadListCartSummary(listCartSummary, TotalPrice);
            }

            var Category = result.d.data.Category;
            if (Category != null) {
                LoadCategoryProduct(Category);
            }

            var Manufacturer = result.d.data.Manufacturer;
            if (Manufacturer != null) {
                LoadManufacturerProduct(Manufacturer);
            }

            var ExpiredNotification = result.d.data.ExpiredNotification;
            if (ExpiredNotification) {
                LoadNotification(ExpiredNotification);
            }
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Customer', 'Cart', 'OrderNumber', 'CartSummary', 'Category', 'Manufacturer', 'ExpiredNotification']
        }
    });
}

function LoadOrderNumber(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<option value="' + data[i].IDOrder + '">' + data[i].Reference + '</option>';
    }
    $(".ddlOrder").html(item);
}

function Insert_Data() {
    var sendData = {};
    var form = $("#form_submit")
    if ($("#HiddenIDCustomer").val() == undefined || $("#HiddenIDCustomer").val() == null || $("#HiddenIDCustomer").val() == "") {
        $(".input-data", "#form_submit").each(function (index, element) {
            if ($(element).attr("name") == "Amount")
                sendData[$(element).attr("name")] = +$(element).val();
            else if ($(element).attr("name") == "IDOrderGuest" || $(element).attr("name") == "IDOrder")
                sendData["IDOrder"] = +$("#HiddenIDOrderGuest").val();
            else
                sendData[$(element).attr("name")] = $(element).val();
        });

    }
    else {
        $(".input-data", "#form_submit").each(function (index, element) {
            if ($(element).attr("name") == "IDOrder" || $(element).attr("name") == "Amount")
                sendData[$(element).attr("name")] = +$(element).val();
            else
                sendData[$(element).attr("name")] = $(element).val();
        });
    }
    if ($("#HiddenImage").val()) {
        sendData["baseImage"] = $("#HiddenImage").val();
        //sendData["fnImage"] = $("input[name=Image]").val();
        sendData["fnImage"] = $('input[name=Image]').val().replace(/C:\\fakepath\\/i, '');
    }
    else {
        sendData["baseImage"] = "";
        sendData["fnImage"] = "";
    }

    sendData["PaymentDate"] = moment($(".input-data[name=Date]").val() + ' ' + $(".input-data[name=Time]").val(), "DD/MM/YYYY HH:mm").format("YYYY-MM-DD HH:mm");

    REST.onSuccess = function (result) {
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
        'c': 'fecopay',
        'm': 'icopay',
        'data': sendData
    });
}

function SubmitConfirm() {
    var e = $("#form_submit");
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {
            Image: {
                required: "You must upload attachment"
            },
            Name: {
                required: "Name cannot blank",
                minlength: "Name, minimum 3 character"
            },
            IDOrder: {
                required: "Please choose your order"
            },
            Email: {
                required: "Email cannot blank",
                email: "Email format is not valid"
            },
            PhoneNumber: {
                required: "Phone number cannot blank"
            },
            Bank: {
                required: "Bank cannot blank"
            },
            Amount: {
                required: "Amount cannot blank",
            },
            Date: {
                required: "Date cannot blank"
            },
            Time: {
                required: "time cannot blank"
            }
        },
        rules: {
            Image: {
                extension: "png|jpe?g|gif",
                size: true,
                required: true
            },
            Name: {
                required: true,
                minlength: 3,
            },
            IDOrder: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            PhoneNumber: {
                required: true,
                number: true
            },
            Bank: {
                required: true
            },
            Amount: {
                required: true,
                number: true
            },
            Date: {
                required: true
            },
            Time: {
                required: true
            }
        },
        errorPlacement: function (e, t) {
            $(".alert").append(e);
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
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            //$(e).closest(".form-group").removeClass("has-error")
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();
            if ($("#HiddenIDCustomer").val() == undefined || $("#HiddenIDCustomer").val() == null || $("#HiddenIDCustomer").val() == "") {
                if ($("#HiddenIDOrderGuest").val() == "" || $("#HiddenIDOrderGuest").val() == undefined || $("#HiddenIDOrderGuest").val() == null) {
                    bootbox.alert("Order Number Not Found");
                }
                else {
                    Insert_Data();
                }
            }
            else {
                Insert_Data();
            }
        }
    });
    jQuery.validator.addMethod("size", function (e, t, n) {
        if (jQuery(t).attr("type") === "file") {
            if (t.files[0]) {
                file = t.files[0];
                size = file.size / 1024 / 1024;
                if (size > 2) {
                    return false
                }
            }
        }
        return true
    }, jQuery.validator.format("Maximum file size is 2MB."));

    jQuery.extend(jQuery.validator.messages, {
        extension: jQuery.validator.format("Allowed Extension are : .png .jpeg .jpg .gif.")
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

function LoadCategoryProduct(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<a href="/Products.aspx?idCategory=' + data[i].IDCategory + '">' + data[i].Name + '</a>'
    }

    $(".CategoryMenu").html(item);
}

function LoadManufacturerProduct(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<a href="/Products.aspx?idManufacturer=' + data[i].IDManufacturer + '">' + data[i].Name + '</a>'
    }

    $(".ManufacturerMenu").html(item);
}
function GetIDOrderByReference(reference) {
    REST.onSuccess = function (result) {
        if (result.d != null) {
            $("#HiddenIDOrderGuest").val(result.d);
        }
        else {
            bootbox.alert("Order Number not found");
        }
    };
    REST.sendRequest({
        'c': 'fecopay',
        'm': 'getidorder',
        'data': { "Reference": reference }
    });
}
function LoadNotification(data) {
    var item = '';
    var endDate;
    var currentDate = new Date();
    console.log(datediff(currentDate, endDate));
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            endDate = new Date(data[i].EndDateYear, data[i].EndDateMonth, data[i].EndDateDay, data[i].EndDateHour, data[i].EndDateMinute, data[i].EndDateSecond, data[i].EndDateMiliSecond);
            item += '<div class="top-cart-items">';
            item += '<div class="top-cart-item clearfix">';
            item += '<div class="top-cart-item-desc">';
            if (datediff(currentDate, endDate) <= 0) {
                item += '<p>Your ' + data[i].ProductName + ' is expired</p>';
            }
            if (datediff(currentDate, endDate) <= 60) {
                item += '<p>Your ' + data[i].ProductName + ' will expire in ' + datediff(currentDate, endDate) + ' day(s)</p>';
            }
            item += '</div>';
            item += '</div>';
            item += '</div>';
        }
    }
    else {
        item += '<div class="top-cart-items">';
        item += '<p>You have no notification about your package</p>';
        item += '</div>';
    }

    $(".notif-list").html(item);

    $("#top-cart-trigger span").text(data.length);
}

function datediff(first, second) {
    // Take the difference between the dates and divide by milliseconds per day.
    // Round to nearest whole number to deal with DST.
    return Math.round((second - first) / (1000 * 60 * 60 * 24));
}