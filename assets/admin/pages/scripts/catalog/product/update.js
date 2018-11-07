var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {

    $("#panelCombination").hide();
    $(".save-qty").hide();

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

    //$("#DefaultCategory").select2();
    if (!queryString("id") || isNaN(queryString("id")))
        window.location = "./default.aspx";
    else
        $("#HiddenIDProduct").val(queryString("id"));

    $("#btnCalculate").click(function (e) { e.preventDefault(); Calculate(); });

    $("#ImpactPrice").on("change", function (e) {
        e.preventDefault();

        var before = Number(ReplacePoint($(".input[name=PriceBeforeImpact]").val()));
        var after = Number(ReplacePoint($(".cmb[name=ImpactPrice]").val()));
        var result = before + after;
        $(".input[name=PriceAfterImpact]").val(result);
    });

    $("#ImpactWeight").click(function (e) {
        e.preventDefault();
        var before = Number($(".input[name=WeightBeforeImpact]").val());
        var impact = +$(".cmb[name=ImpactWeight]").val();
        var result = Number((before + impact)).toFixed(2);
        $(".input[name=Weight]").val(result);
    });

    $("#submitCategory").click(function (e) {
        e.preventDefault();
        SubmitCategories();
    });

    $("#btnSubmitInformation").click(function (e) {
        e.preventDefault();
        $("#formProductInformation_Update").submit();
    });

    $("#submitCategoryDefault").click(function (e) {
        e.preventDefault();
        UpdateDefaultCategory();
    });

    $("#btnSubmitSEO").click(function (e) {
        e.preventDefault();
        UpdateSEO();
    });

    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_products").addClass("active");
    $("#HiddenIDAttribute").val(queryString("id"));
    Preload(+$("#HiddenIDProduct").val());

    UpdateInformationValidation();

    //COMBINATION
    $("#btnAddCombinationValue").click(function (e) {
        e.preventDefault();
        if ($("#Attributes").select2("data") == null || $("#Values").select2("data") == null) {
            bootbox.alert("Please choose attribute and value");
        }
        else {
            var text = $("#Attributes").select2("data").text + ' : ' + $("#Values").select2("data").text;
            var idValue = $("#Values").select2("data").id;
            var item = '<option value="' + idValue + '">' + text + '</option>';
            $("#listCombination").append(item);
        }
    });

    $("#btnDeleteCombinationValue").click(function (e) {
        e.preventDefault();
        if ($("#listCombination option").length == 0) {
            Metronic.alert({
                container: "#bootstrap_alert_combination",
                place: "append",
                type: "danger",
                message: "Cannot delete, empty data",
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "warning"
            });
        }
        else {
            var count = 0;
            $.each($("#listCombination option"), function (indexInArray, valueOfElement) {
                if ($(this).prop('selected')) {
                    count++;
                    $(this).remove();
                }
            });
            if (count == 0) {
                Metronic.alert({
                    container: "#bootstrap_alert_combination",
                    place: "append",
                    type: "danger",
                    message: "Please choose data to delete",
                    close: true,
                    reset: true,
                    focus: true,
                    closeInSeconds: "5",
                    icon: "warning"
                });
            }
        }
    });

    $("#btnSaveCombination").click(function (e) {
        e.preventDefault();
        if ($("#text-combination").text() == "New Combination") {
            InsertCombination();
        }
        else {
            UpdateCombination();
        }

    });

    $("#btnCancelCombination").click(function (e) {
        e.preventDefault();
        $("#panelCombination").fadeOut();
        Metronic.scrollTo("#bootstrap_alert_combination");
    });

    $(".new-cmb").click(function (e) {
        e.preventDefault();
        $("#panelCombination").fadeIn();
        Metronic.scrollTo($(".for-scroll"));
    });

    $(".edit-qty").click(function () {
        $(this).hide();
        $("#panelCombination").fadeOut();
        $(".save-qty").show();
        $(".text-qty").removeClass("hidden");
        $(".label-qty").addClass("hidden");
    });

    $(".save-qty").click(function () {
        $(this).hide();
        $("#panelCombination").fadeOut();
        $(".edit-qty").show();
        $(".text-qty").addClass("hidden");
        $(".label-qty").removeClass("hidden");
        var idCombination = {};
        var qtyCombination = {};

        idCombination = GetIDCombination();
        qtyCombination = GetQty();

        SaveQuantity(idCombination, qtyCombination);

    });
    //END COMBINATION
});

function InsertCombination() {
    if ($("#listCombination option").length == 0) {
        Metronic.alert({
            container: "#bootstrap_alert_combination",
            place: "append",
            type: "danger",
            message: "Please add attribute and value first",
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: "5",
            icon: "warning"
        });
    }
    else {
        var data = {};
        var arrValue = [];
        $.each($("#listCombination option"), function (indexInArray, valueOfElement) {
            arrValue.push(+$(this).val());
        });

        var Photos = []
        $.each($("input[name='ImageCombination']"), function (indexInArray, valueOfElement) {
            if ($(this).prop("checked")) {
                Photos.push(+$(this).val());
            }
        });

        data["IDProduct"] = parseInt(queryString("id"));
        data["Values"] = arrValue;
        data["Photos"] = Photos;

        $.each($(".cmb"), function (indexInArray, valueOfElement) {
            if ($(valueOfElement).attr("name") == "ReferenceCode") {
                data[$(valueOfElement).attr("name")] = $(valueOfElement).val();
            }
            else if ($(valueOfElement).attr("name") == "ImpactWeight")
                data[$(valueOfElement).attr("name")] = Number($(this).val());
            else
                data[$(valueOfElement).attr("name")] = Number(ReplacePoint($(valueOfElement).val()));
        });

        data["Discount"] = 0; //disount selalu 0

        REST.onSuccess = function (result) {
            if (result.d.success) {
                $("#panelCombination").fadeOut();
                Metronic.alert({
                    container: "#bootstrap_alert_combination",
                    place: "append",
                    type: "success",
                    message: result.d.message,
                    close: true,
                    reset: true,
                    focus: true,
                    closeInSeconds: "5",
                    icon: "check"
                });

                $(".cmb").val("");
                $("#listCombination option").remove();
                $("input[name=ImageCombination]").prop("checked", false);
                $(".statusCover span").removeClass("checked");
                ReloadCombinationTable(result.d.data);
            }
            else {
                Metronic.alert({
                    container: "#bootstrap_alert_combination",
                    place: "append",
                    type: "danger",
                    message: result.d.message,
                    close: true,
                    reset: true,
                    focus: true,
                    closeInSeconds: "5",
                    icon: "warning"
                });
            }
        };
        REST.sendRequest({
            'c': 'bepro',
            'm': 'iprocom',
            'data': data
        });
    }
}

function UpdateCombination(id) {
    if ($("#listCombination option").length == 0) {
        Metronic.alert({
            container: "#bootstrap_alert_combination",
            place: "append",
            type: "danger",
            message: "Please add attribute and value first",
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: "5",
            icon: "warning"
        });
    }
    else {

        var data = {};
        var arrValue = [];
        $.each($("#listCombination option"), function (indexInArray, valueOfElement) {
            arrValue.push(+$(this).val());
        });

        var Photos = []
        $.each($("input[name='ImageCombination']"), function (indexInArray, valueOfElement) {
            if ($(this).prop("checked")) {
                Photos.push(+$(this).val());
            }
        });

        data["IDProduct_Combination"] = +$("#HiddenIDCombination").val();
        data["IDProduct"] = parseInt(queryString("id"));
        data["Values"] = arrValue;
        data["Photos"] = Photos;

        $.each($(".cmb"), function (indexInArray, valueOfElement) {
            if ($(valueOfElement).attr("name") == "ReferenceCode") {
                data[$(valueOfElement).attr("name")] = $(valueOfElement).val();
            }
            else if ($(valueOfElement).attr("name") == "ImpactWeight")
                data[$(valueOfElement).attr("name")] = Number($(this).val());
            else
                data[$(valueOfElement).attr("name")] = Number(ReplacePoint($(valueOfElement).val()));
        });

        REST.onSuccess = function (result) {
            if (result.d.success) {
                $("#panelCombination").fadeOut();
                Metronic.alert({
                    container: "#bootstrap_alert_combination",
                    place: "append",
                    type: "success",
                    message: result.d.message,
                    close: true,
                    reset: true,
                    focus: true,
                    closeInSeconds: "5",
                    icon: "check"
                });

                $(".cmb").val("");
                $("#listCombination option").remove();
                $("input[name=ImageCombination]").prop("checked", false);
                $(".statusCover span").removeClass("checked");
                ReloadCombinationTable(result.d.data);
            }
            else {
                Metronic.alert({
                    container: "#bootstrap_alert_combination",
                    place: "append",
                    type: "danger",
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
            'c': 'bepro',
            'm': 'uprocom',
            'data': data
        });
        console.log($("#HiddenIDCombination").val());
        $("#text-combination").text("New Combination");
    }
}

function UpdateSEO() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            Metronic.alert({
                container: "#bootstrap_alerts_meta",
                place: "append",
                type: "success",
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_meta",
                place: "append",
                type: "danger",
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "warning"
            });
        }
    };

    var sendData = {};
    sendData["IDProduct"] = +$("#HiddenIDProduct").val();
    $(".input", "#formSEO").each(function (index, element) {
        sendData[$(element).attr("name")] = $(element).val();
    });

    REST.sendRequest({
        'c': 'bepro',
        'm': 'uproseo',
        'data': sendData
    });
}

function UpdateInformationValidation() {
    var e = $("#formProductInformation_Update");
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
                minlength: 3
            },
            PriceBeforeDiscount: {
                required: true
            },
            Weight: {
                required: true
            }
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
            UpdateInformation();
        }
    });
}

function UpdateInformation() {
    var sendData = {};
    sendData["IDProduct"] = +$("#HiddenIDProduct").val();
    $("#formProductInformation_Update").find(".input").each(function (index, element) {
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
            sendData[$(this).attr("name")] = +$(this).val();
        }
        else if ($(this).attr("name") == "Tax") {
            sendData[$(this).attr("name")] = Number(ReplacePoint($(this).val()));
        }
        else {
            sendData[$(this).attr("name")] = $(this).val();
        }
    });

    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
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
        'm': 'uproinfo',
        'data': sendData
    });
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

function Preload(id) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var product = data.d.data.Product;
            $(".productName").html(product.Name);
            $("#treeCategory").jstree({
                'plugins': ['themes', 'html_data', 'checkbox', 'ui'],
                "checkbox": {
                    "three_state": false,
                },
                'core': {
                    "themes": {
                        "responsive": true
                    },
                    'data': data.d.data.TreeCategories
                },
                "types": {
                    "default": {
                        "icon": "fa fa-folder icon-state-warning icon-lg"
                    },
                    "file": {
                        "icon": "fa fa-file icon-state-warning icon-lg"
                    }
                }
            });
            //LOAD TAB INFORMATION
            $.each(product, function (indexInArray, valueOfElement) {
                if (indexInArray == "TypeDiscountPercent") {
                    $("#" + indexInArray).bootstrapSwitch("state", valueOfElement);
                    if (valueOfElement)
                        $("." + indexInArray).val("Percent");
                    else
                        $("." + indexInArray).val("Amount");
                }
                else if (indexInArray == "ShortDescription") {
                    $("#" + indexInArray).code(valueOfElement);
                }
                else if (indexInArray == "Description") {
                    $("#" + indexInArray).code(valueOfElement);
                }
                else if (indexInArray == "Note") {
                    $("#" + indexInArray).code(valueOfElement);
                }
                else {
                    $("#" + indexInArray).val(valueOfElement);
                    $("." + indexInArray).val(valueOfElement);
                }
            });

            if (product["TypeDiscountPercent"]) {
                $("#DiscountPercent").val(product["Discount"]);
            }
            else
                $("#DiscountNominal").val(product["Discount"]);

            Calculate();//calculate final pice

            //END LOAD TAB INFORMATION

            //LOAD TAB CATEGORIES
            var categories = data.d.data.SelectedCategories;
            ReloadCategories(data.d.data.DefaultCategory, categories);
            //END LOAD TAB CATEGORIES

            //LOAD TAB IMAGES
            ReloadImage(data.d.data.Photos);
            //END LOAD TAB IMAGES

            //LOAD TAB COMBINATION
            ReloadAttribute(data.d.data.Attributes)
            ReloadCombinationTable(data.d.data.Combinations);
            //END LOAD COMBINATION

            //LOAD CURRENCY
            LoadCurrency(data.d.data.Currency);
            //END LOAD CURRENCY
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
        'm': 'upreload',
        'data': { 'id': id }
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
            item += '<input type="text" name="PriceBeforeDiscount-' + data[i].Name + '" class="form-control money input additional-currency currency-priceBeforeDiscount" maxlength="15" value="0" data-rate="' + data[i].ConversionRate + '" />';
            item += '<div class="input-group-addon">' + data[i].ISOCode + '</div>';
            item += '</div>';
            item += '</div>';
            item += '</div>';


            item += '<div class="col-md-3">';
            item += '<div class="form-group">';
            item += '<label class="control-label">Total Discount<span class="required">*</span></label>';
            item += '<div class="input-group">';
            item += '<input type="text" name="totalDiscount-' + data[i].Name + '" class="form-control money input additional-currency currency-totalDiscount" maxlength="15" value="0" data-rate="' + data[i].ConversionRate + '" />';
            item += '<div class="input-group-addon">' + data[i].ISOCode + '</div>';
            item += '</div>';
            item += '</div>';
            item += '</div>';

            item += '<div class="col-md-3">';
            item += '<div class="form-group">';
            item += '<label class="control-label">Price<span class="required">*</span></label>';
            item += '<div class="input-group">';
            item += '<input type="text" name="price-' + data[i].Name + '" class="form-control money input additional-currency currency-price" maxlength="15" value="0" data-rate="' + data[i].ConversionRate + '" />';
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

function ReloadCombinationTable(data) {
    var row = '';
    for (var i = 0; i < data.length; i++) {
        row += '<tr class="text-center">';
        row += '<td>' + data[i].Name + '</td>';
        row += '<td>' + data[i].ReferenceCode + '</td>';
        row += '<td>' + data[i].ImpactPrice + '</td>';
        row += '<td>' + data[i].ImpactWeight + '</td>';
        row += '<td><label class="label-qty">' + data[i].Quantity + '</label><input type="text" id="qtyfor' + data[i].IDProduct_Combination + '" class="text-qty hidden" value="' + data[i].Quantity + '" data-idcombination="' + data[i].IDProduct_Combination + '" /></td>';
        row += '<td>' + data[i].SequenceNumber + '</td>';
        row += '<td><a href="#" data-id="' + data[i].IDProduct_Combination + '" class="edit-cmb btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a><a href="#" data-id="' + data[i].IDProduct_Combination + '" data-name="' + data[i].Name + '" class="delete-cmb btn btn-sm red tooltips" data-original-title="Delete"><i class="glyphicon glyphicon-trash"></i></a></td>';
        row += '</tr>';
    }
    if (data.length == 0) {
        row += '<tr><td colspan="7" class="text-center"><b>NO DATA</b></td></tr>';
    }
    $("#dtCombination tbody").html(row);
    $("a").tooltip();

    $(".edit-cmb").click(function (e) {
        e.preventDefault();
        $("#panelCombination").fadeIn();
        REST.onSuccess = function (result) {
            if (result.d.success) {
                $("#text-combination").text("Update Combination");
                $("#HiddenIDCombination").val(result.d.data.IDProduct_Combination);
                Metronic.scrollTo($(".for-scroll"));

                $.each(result.d.data, function (indexInArray, valueOfElement) {
                    $(".cmb[name='" + indexInArray + "']").val(valueOfElement);
                });

                var values = result.d.data.Values;
                $("#listCombination").html("");
                for (var i = 0; i < values.length; i++) {
                    var item = '<option value="' + values[i].IDValue + '">' + values[i].Name + '</option>';
                    $("#listCombination").append(item);
                }

                var photos = result.d.data.Photos;
                $("input[name=ImageCombination]").prop("checked", false);
                $(".statusCover span").removeClass("checked");
                $.each(photos, function (indexInArray, valueOfElement) {
                    console.log(valueOfElement);
                    $("input[name=ImageCombination][value=" + valueOfElement + "]").prop("checked", true);
                    $("input[name=ImageCombination][value=" + valueOfElement + "]").parent().addClass("checked");
                });
            }
            else {
                Metronic.alert({
                    container: "#bootstrap_alert_combination",
                    place: "append",
                    type: "danger",
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
            'c': 'bepro',
            'm': 'preloaducom',
            'data': { 'id': +$(this).data("id") }
        });
    });

    $(".delete-cmb").click(function (e) {
        e.preventDefault();
        var btn = this;
        bootbox.confirm("Are you sure to delete " + $(this).data("name") + " ?", function (result) {
            if (result) {
                DeleteProductCombination(+$(btn).data('id'));
            }
        });
    });
}

function DeleteProductCombination(id) {
    REST.onSuccess = function (data) {
        if (data.d.success) {
            Metronic.alert({
                container: "#bootstrap_alert_combination",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });

            ReloadCombinationTable(data.d.data);
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alert_combination",
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
        'm': 'dprocom',
        'data': { 'id': id }
    });
}

function ReloadAttribute(data) {
    var attributes = [];
    for (var i = 0; i < data.length; i++) {
        attributes.push({
            id: data[i].IDAttribute,
            text: data[i].Name
        });
    }
    $("#Attributes").select2({
        data: attributes,
        allowClear: true
    }).select2("data", null).on("change", function () {
        ReloadValueByAttribute(+$(this).val());
    });
}

function ReloadValueByAttribute(id) {
    REST.onSuccess = function (response) {
        var data = response.d.data;
        var values = [];
        for (var i = 0; i < data.length; i++) {
            values.push({
                id: data[i].IDValue,
                text: data[i].Name
            });
        }
        $("#Values").select2({
            data: values,
            allowClear: false
        });
    };
    REST.sendRequest({
        'c': 'bepro',
        'm': 'rval',
        'data': { 'IDAttribute': id }
    });
}

function ReloadImage(data) {
    var item = "";
    var itemCmb = ""; //untuk tab combination
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            item += "<tr>" +
            "<td><center><img class='img-responsive' style='max-height:150px;width:auto;' src='/assets/images/product/" + data[i].Preview + "?v=" + new Date().getTime() + "' /></center></td>" +
            "<td>" + data[i].Photo + "</td>";
            if (data[i].IsCover)
                item += '<td><a data-id="' + data[i].IDProduct_Photo + '" class="btn btn-sm btn_changestatus green tooltips" href="javascript:;"><i title="Active" class="glyphicon glyphicon-ok"></i></a></td>';
            else
                item += '<td><a data-id="' + data[i].IDProduct_Photo + '" class="btn btn-sm btn_changestatus red tooltips" href="javascript:;"><i title="Active" class="glyphicon glyphicon-remove"></i></a></td>';
            item += '<td><button data-name="' + data[i].Photo + '" data-id="' + data[i].IDProduct_Photo + '" title="set as cover" class="btn btn-cover btn-sm yellow"><i class="fa fa-thumb-tack"></i> Set as Cover</button> <button data-name="' + data[i].Photo + '" data-id="' + data[i].IDProduct_Photo + '" class="btn btn-delete btn-sm red" title="delete"><i class="fa fa-trash-o"></i> Delete</button></td>' +
            "</tr>";

            itemCmb += '<label class="checkbox-inline statusCover">';
            itemCmb += '<input type="checkbox" name="ImageCombination" value="' + data[i].IDProduct_Photo + '" />';
            itemCmb += '<img style="max-width: 75px; margin: 3px;" src="/assets/images/product/' + data[i].Photo + '" alt="image" class="img-responsive" />';
            itemCmb += '</label>';
        }
        $("#dtProductImage tbody").html(item);
        $("#listImages").html(itemCmb);
        Metronic.init();

        $(".btn-delete").click(function (e) {
            e.preventDefault();
            var name = $(this).data("name");
            var id = $(this).data("id");
            bootbox.confirm("Are you sure to delete <b>" + name + "</b> ?", function (result) {
                if (result) {
                    DeletePhoto(id);
                }
            });
        });

        $(".btn-cover").click(function (e) {
            e.preventDefault();
            var name = $(this).data("name");
            var id = $(this).data("id");
            bootbox.confirm("Are you sure to set <b>" + name + "</b> as cover ?", function (result) {
                if (result) {
                    SetCoverPhoto(id);
                }
            });
        });
    }
    else {
        $("#dtProductImage tbody").html("<tr><td class='text-center' colspan='4'>No records to display</tr>");
    }

}

function DeletePhoto(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            ReloadImage(result.data.Photos);
            Metronic.alert({
                container: "#bootstrap_alerts_photo",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_photo",
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
        'm': 'dprophoto',
        'data': { 'id': id }
    });
}

function SetCoverPhoto(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            ReloadImage(result.data.Photos);
            Metronic.alert({
                container: "#bootstrap_alerts_photo",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_photo",
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
        'm': 'setcover',
        'data': { 'id': id }
    });
}

function ReloadCategories(DefaultCategory, SelectedCategories) {
    //LOAD TAB CATEGORIES 
    var selectedCat = {};
    var cat = [];
    for (var i = 0; i < SelectedCategories.length; i++) {
        if (SelectedCategories[i].IDProduct_Category == DefaultCategory.IDProduct_Category) {
            selectedCat = {
                id: SelectedCategories[i].IDProduct_Category,
                text: SelectedCategories[i].Name
            };
        }

        cat.push({
            id: SelectedCategories[i].IDProduct_Category,
            text: SelectedCategories[i].Name
        });
    }

    if (selectedCat != null) {
        $("#DefaultCategory").select2({
            data: cat
        }).select2("data", selectedCat);
    }
    else {
        $("#DefaultCategory").select2({
            data: cat
        });
    }

    if (cat.length === 0) {
        Metronic.alert({
            container: "#bootstrap_alerts_cat",
            place: "append",
            type: "danger",
            message: "Please, choose category before set default category for this product.",
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: "0",
            icon: "warning"
        });
    }
    //END LOAD TAB CATEGORIES
}

function ChangeDefaultCategory(idCategory) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            Metronic.alert({
                container: "#bootstrap_alerts_catdef",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
            ReloadCategories(data.d.data.Categories, data.d.data.SelectedCategories);
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_catdef",
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
        'm': 'changedefcat',
        'data': {
            'idcat': idCategory
        },
    });
}

function DeleteCategory(idCategory) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            Metronic.alert({
                container: "#bootstrap_alerts_cat",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
            ReloadCategories(result.data.Categories, result.data.SelectedCategories);
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_cat",
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
        'm': 'dcatpro',
        'data': {
            'idprocat': idCategory
        }
    });
}

function UpdateDefaultCategory() {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            Metronic.alert({
                container: "#bootstrap_alerts_catdef",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: false,
                closeInSeconds: "0",
                icon: "check"
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_catdef",
                place: "append",
                type: "danger",
                message: data.d.message,
                close: true,
                reset: true,
                focus: false,
                closeInSeconds: "0",
                icon: "warning"
            });
        }
    };

    if ($("#DefaultCategory").select2("data") != null) {
        //GET DATA
        var sendData = {};
        sendData["IDProduct_Category"] = +$("#DefaultCategory").select2("data").id;
        //END GET DATA

        REST.sendRequest({
            'c': 'bepro',
            'm': 'uprodefcat',
            'data': sendData
        });
    }
    else {
        Metronic.alert({
            container: "#bootstrap_alerts_cat",
            place: "append",
            type: "danger",
            message: "Please choose default category, or check category if there's no category yet",
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: "0",
            icon: "warning"
        });
    }
}

function SubmitCategories() {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            ReloadCategories(data.d.data.DefaultCategory, data.d.data.SelectedCategories);
            Metronic.alert({
                container: "#bootstrap_alerts_cat",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alerts_cat",
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

    //GET DATA
    var sendData = {};
    sendData["IDCategory"] = $("#treeCategory").jstree(true).get_selected();;
    sendData["IDProduct"] = +$("#HiddenIDProduct").val();
    //END GET DATA

    REST.sendRequest({
        'c': 'bepro',
        'm': 'uprocat',
        'data': sendData
    });
}

function SaveQuantity(idCombination, qty) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {

            Metronic.alert({
                container: "#bootstrap_alert_combination",
                place: "append",
                type: "success",
                message: data.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "0",
                icon: "check"
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alert_combination",
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

    //GET DATA
    var sendData = {};
    sendData["IDCombination"] = idCombination;
    sendData["qty"] = qty;
    //END GET DATA

    REST.sendRequest({
        'c': 'bepro',
        'm': 'uprocomqty',
        'data': sendData
    });
}

function GetIDCombination() {
    var id = [];
    $(".text-qty").each(function (index, element) {

        id.push(+$(this).data('idcombination'));
    });
    return id;
}

function GetQty() {
    var qty = [];
    $(".text-qty").each(function (index, element) {
        qty.push(+$(this).val());
    });
    return qty;
}
