$(document).ready(function () {
    $(document).ajaxStop(function () {
        Metronic.unblockUI();
        //console.clear();
    });
    TouchSpinWeight.init();
    Toastr.init();
    TouchSpin.init();
    $("#menu_catalog").addClass("active");
    $("#submenu_products").addClass("active");
    $("#arrow_catalog").addClass("open");
    $('.money').inputmask('decimal', {
        rightAlignNumerics: false,
        groupSeparator: '.',
        autoGroup: true
    });

    Manufacturer_GetAll();

    ProductInformation_ValidationInsert($('#formProductInformation_Insert'));

    $('#btnSimulate').click(function (e) {
        e.preventDefault();
        var discount = 0;
        var typeDiscount = false;
        if ($("#cbTypeDiscount").is(":checked")) {
            typeDiscount = true;
            discount = $("#tbDiscountPersentase").val();
        } else {
            discount = $("#tbDiscountMoney").val();
        }
        var priceSale = $("#tbBasePrice").val();
        Simulate(priceSale, typeDiscount, discount);
    });

    $('#cbTypeDiscount').on('switchChange.bootstrapSwitch', function (event, state) {
        if (state) {
            if ($('#tbDiscountPersentase').parent().hasClass('bootstrap-touchspin')) {
                $('#tbDiscountPersentase').parent().removeClass('hide');
            }
            $('#tbDiscountMoney').addClass('hide');
        } else {
            if ($('#tbDiscountPersentase').parent().hasClass('bootstrap-touchspin')) {
                $('#tbDiscountPersentase').parent().addClass('hide');
            }
            $('#tbDiscountMoney').removeClass('hide');
        }
    });
});

function Simulate(priceSale, typeDiscount, discount)
{
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/Simulate",
        data: JSON.stringify({
            priceSale: priceSale,
            typeDiscount: typeDiscount,
            discount: discount
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, n, r) {
            if (e.d.Status == "Success") {
                $("#tbPrice").val(e.d.Value);
            } else if (e.d.Status == "Warning") {
                toastr.warning(e.d.Deskripsi);
            } else {
                toastr.error(e.d.Deskripsi);
            }
        },
        beforeSend: function () {
            $("#tbPrice").attr("readonly", true).attr("disabled", true).addClass("spinner");
        },
        complete: function () {
            $("#tbPrice").attr("readonly", true).attr("disabled", true).removeClass("spinner");
        }
    });
}

function Manufacturer_GetAll() {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Manufacturer.asmx/GetData_Active",
        data: '{}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var data = response.d;
            var d = [];

            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    d.push({ id: data[i].IDManufacturer, text: data[i].Name });
                }
                $("#ddlManufacturer").select2({
                    data: d
                });
            }
            else {
                toasr.error('Failed load Manufacturer.', 'Failed');
            }
        },
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true,
                message: 'Processing...'
            });
        },
        complete: function () {
            Metronic.unblockUI();
        }
    });
}

function ProductInformation_ValidationInsert(e) {
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        ignore: "",
        messages: {},
        rules: {
            tbName: {
                required: true,
                minlength: 1,
                maxlength: 50
            },
            tbBasePrice: {
                required: true
            },
            tbWeight: {
                required: true
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
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        success: function (e) {
            e.closest(".form-group").removeClass("has-error")
        },
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();
            var referenceCode = $("#tbReference").val();
            var name = $("#tbName").val();
            var idManufacturer = $("#ddlManufacturer").val();
            var priceCogs = $("#tbCostPrice").val();
            var discount = 0;
            var typeDiscount = false;
            if ($("#cbTypeDiscount").is(":checked")) {
                typeDiscount = true;
                discount = $("#tbDiscountPersentase").val();
            } else {
                discount = $("#tbDiscountMoney").val();
            }
            var priceSale = $("#tbBasePrice").val();
            var weight = $("#tbWeight").val();
            var active = false;
            if ($("#cbActive").is(":checked")) {
                active = true;
            }
            ProdutInformation_Insert(referenceCode, name, idManufacturer, priceCogs, priceSale, typeDiscount, discount, weight, active);
        }
    });
    //jQuery.validator.addMethod("exists", function (e, t, n) {
    //    var idAttribute = $("#hiddenIdAttribute").val();
    //    return Value_ValidationName_Insert(idAttribute, e);
    //}, "Value is already exists.")
}

function ProdutInformation_Insert(referenceCode, name, idManufacturer, priceCogs, priceSale, typeDiscount, discount, weight, active) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/Add_Information",
        data: JSON.stringify({
            referenceCode: referenceCode,
            name: name,
            idManufacturer: idManufacturer,
            priceCogs: priceCogs,
            priceSale: priceSale,
            typeDiscount: typeDiscount,
            discount: discount,
            weight: weight,
            active: active
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, n, r) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
                window.setTimeout(function () {
                    location.href = "./Update.aspx?id="+ e.d.Value
                }, 1e3);

            } else if (e.d.Status == "Warning") {
                toastr.warning(e.d.Deskripsi)
            } else {
                toastr.error(e.d.Deskripsi)
            }
        },
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true,
                message: 'Processing...'
            });
        }
    });
}