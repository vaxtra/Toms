var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_products").addClass("active");
    Preload();
    ProductInsertValidation();

    $("#btnCalculate").click(function (e) { e.preventDefault(); Calculate(); });

    $("#PriceBeforeDiscount").on('change', function () {
        var defaultCurrency = Number(ReplacePoint($(this).val()));

        $(".currency-priceBeforeDiscount").each(function (index, element) {
            var result = defaultCurrency / +$(element).data("rate");

            $(element).val(result);
        });
        //ambil rate tiap textbox terus di bagi. terus isi ke value textbox
    });

    $("#Price").on('change', function () {
        var defaultCurrency = Number(ReplacePoint($(this).val()));

        $(".currency-price").each(function (index, element) {
            var result = defaultCurrency / +$(element).data("rate");

            $(element).val(result);
        });
    });
});

function ProductInsertValidation() {
    var e = $("#formProductInformation_Insert");
    var t = $(".alert", e);

    //$.validator.addMethod("Select2Required", function (value, element, params) {
    //    console.log(params[0]);
    //    return ($(params[0]).select2("val") === "");
    //}, "Choose option");

    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        rules: {
            Name: {
                required: true,
                minlength: 3,
                maxlength: 25
            },
            PriceBeforeDiscount: {
                required: true
            },
            Weight: {
                required: true
            },
        },
        messages: {},
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
            t.fadeIn();
            Metronic.scrollTo(t, -200);
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        submitHandler: function (e) {
            t.hide();
            ProductInsert();
        }
    });
}

function ProductInsert() {
    var sendData = {};
    $("#formProductInformation_Insert").find(".input").each(function (index, element) {
        if ($(this).attr("name") == "IDManufacturer") {
            sendData[$(this).attr("name")] = +$(this).val();
        }
        else if ($(this).attr("name") == "PriceBeforeDiscount") {
            sendData[$(this).attr("name")] = Number(ReplacePoint($(this).val()));
        }
        else if ($(this).attr("name") == "TypeDiscountPercent") {
            sendData[$(this).attr("name")] = $(this).bootstrapSwitch("state");
        }
        else if ($(this).attr("name") == "Discount") {
            if ($("#TypeDiscountPercent").bootstrapSwitch("state"))
                sendData[$(this).attr("name")] = +$("#DiscountPercent").val();
            else
                sendData[$(this).attr("name")] = Number(ReplacePoint($("#DiscountNominal").val()));
        }
        else if ($(this).attr("name") == "Weight") {
            sendData[$(this).attr("name")] = +$(this).val();
        }
        else if ($(this).attr("name") == "Length") {
            sendData[$(this).attr("name")] = +$(this).val();
        }
        else if ($(this).attr("name") == "Width") {
            sendData[$(this).attr("name")] = +$(this).val();
        }
        else if ($(this).attr("name") == "Height") {
            sendData[$(this).attr("name")] = +$(this).val();
        }
        else if ($(this).attr("name") == "Active") {
            sendData[$(this).attr("name")] = $(this).bootstrapSwitch("state");
        }
        else if ($(this).attr("name") == "ShortDescription" || $(this).attr("name") == "Description" || $(this).attr("name") == "Note") {
            sendData[$(this).attr("name")] = $(this).code();
        }
        else if ($(this).attr("name") == "price-Dollar" || $(this).attr("name") == "PriceBeforeDiscount-Dollar") {
            sendData[$(this).attr("name")] = $(this).val();
            sendData["IDCurrency"] = +$(this).data("id");
        }
        else {
            sendData[$(this).attr("name")] = $(this).val();
        }
    });

    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            window.location = 'update.aspx?id=' + data.d.data;
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "warning"
            });
        }
    };

    REST.sendRequest({
        'c': 'bepro',
        'm': 'ipro',
        'data': sendData
    });
}

function Preload() {
    REST.onSuccess = function (data) {
        var manufacturer = data.d.data.Manufacturer;

        if (manufacturer != null) {
            var dataManufacturer = [];
            for (var i = 0; i < manufacturer.length; i++) {
                dataManufacturer.push({
                    id: manufacturer[i].IDManufacturer,
                    text: manufacturer[i].Name
                });

                $("#IDManufacturer").select2({
                    data: dataManufacturer,
                    allowClear: false
                }).select2("data", dataManufacturer[0]);
            }
        }
        else {
            bootbox.alert("Data manufacturer is null");
        }

        if (data.d.data.Currency != null) {
            LoadCurrency(data.d.data.Currency);
        }
    };
    REST.sendRequest({
        'c': 'bepro',
        'm': 'preload',
        'data': {}
    });
}

function LoadCurrency(data) {
    if (data.length > 0) {
        var item = '';
        for (var i = 0; i < data.length; i++) {
            item += '<div class="panel panel-primary">';
            item += '<div class="panel-heading">';
            item += '<h4 class="panel-title">';
            item += '<a class="accordion-toggle accordion-toggle-styled" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_1">Price in <b>' + data[i].Name + ' </b></a>';
            item += '</h4>';
            item += '</div>';
            item += '<div id="collapse_3_1" class="panel-collapse in">';
            item += '<div class="panel-body">';

            item += '<div class="row">';

            item += '<div class="col-md-3">';
            item += '<div class="form-group">';
            item += '<label class="control-label">Price Before Discount<span class="required">*</span></label>';
            item += '<div class="input-group">';
            item += '<input type="text" name="PriceBeforeDiscount-' + data[i].Name + '" class="form-control money input additional-currency currency-priceBeforeDiscount" maxlength="15" value="0" data-rate="' + data[i].ConversionRate + '" data-id="' + data[i].IDCurrency + '" />';
            item += '<div class="input-group-addon">' + data[i].ISOCode + '</div>';
            item += '</div>';
            item += '</div>';
            item += '</div>';


            item += '<div class="col-md-3">';
            item += '<div class="form-group">';
            item += '<label class="control-label">Total Discount<span class="required">*</span></label>';
            item += '<div class="input-group">';
            item += '<input type="text" name="totalDiscount-' + data[i].Name + '" class="form-control money input additional-currency currency-totalDiscount" maxlength="15" value="0" data-rate="' + data[i].ConversionRate + '" data-id="' + data[i].IDCurrency + '" />';
            item += '<div class="input-group-addon">' + data[i].ISOCode + '</div>';
            item += '</div>';
            item += '</div>';
            item += '</div>';

            item += '<div class="col-md-3">';
            item += '<div class="form-group">';
            item += '<label class="control-label">Price<span class="required">*</span></label>';
            item += '<div class="input-group">';
            item += '<input type="text" name="price-' + data[i].Name + '" class="form-control money input additional-currency currency-price" maxlength="15" value="0" data-rate="' + data[i].ConversionRate + '" data-id="' + data[i].IDCurrency + '" />';
            item += '<div class="input-group-addon">' + data[i].ISOCode + '</div>';
            item += '</div>';
            item += '</div>';
            item += '</div>';

            item += '</div>';

            item += '</div>';
            item += '</div>';
            item += '</div>';
        }

        $("#accordion3").html(item);
    }
}

function Calculate() {
    if ($("#TypeDiscountPercent").bootstrapSwitch("state")) {
        var price = Number(ReplacePoint($("#PriceBeforeDiscount").val()));
        var percent = +$("#DiscountPercent").val();
        var result = price - (price * percent / 100);
        $("#Price").val(result);

        var defaultCurrency = result;

        $(".currency-price").each(function (index, element) {
            var result = defaultCurrency / +$(element).data("rate");

            $(element).val(result);
        });
    }
    else {
        var price = Number(ReplacePoint($("#PriceBeforeDiscount").val()));
        var discount = Number(ReplacePoint($("#DiscountNominal").val()));
        var result = price - discount;
        $("#Price").val(result);

        var defaultCurrency = result;

        $(".currency-price").each(function (index, element) {
            var result = defaultCurrency / +$(element).data("rate");

            $(element).val(result);
        });
    }

    //$("#Price").val($("#PriceBeforeDiscount").val());
}