$(document).ready(function () {
    $(document).ajaxStop(function () {
        Metronic.unblockUI();
        //console.clear();
    });
    var idProduct = queryString("id");
    if (idProduct !== "") {
        TouchSpinWeight.init();
        Toastr.init();
        TouchSpin.init();
        $(".summernote").summernote({
            height: 200
        });
        $("#menu_catalog").addClass("active");
        $("#submenu_products").addClass("active");
        $("#arrow_catalog").addClass("open");
        $('.money').inputmask('decimal', {
            rightAlignNumerics: false,
            groupSeparator: '.',
            autoGroup: true
        });
        $("#hiddenIdProduct").val(idProduct);
        Manufacturer_GetAll();
        ProductInformation_GetDetail(idProduct);
    } else {
        toastr.error("<b>Product is not exists.</b>");
        window.setTimeout(function () {
            location.href = "./Default.aspx"
        }, 1e3)
    }

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

    $('#btnSimulate').click(function (e) {
        e.preventDefault();
        var discount = 0;
        var typeDiscount = true;
        if ($(".bootstrap-switch-id-cbTypeDiscount").hasClass("bootstrap-switch-off")) {
            typeDiscount = false;
            discount = $("#tbDiscountMoney").val();
        } else {
            discount = $("#tbDiscountPersentase").val();
        }
        var priceSale = $("#tbBasePrice").val();
        Simulate(priceSale, typeDiscount, discount);
    });

    $('#modal_CombinationInsert').on('shown.bs.modal', function(e) {
        e.preventDefault();
        Attribute_GetAll();
    })


    $('#modal_UploadImages').on('hidden.bs.modal', function (e) {
        e.preventDefault();
        ProductImages_GetAll(idProduct);
    })

     $('#modal_CombinationInsert').on('hidden.bs.modal', function (e) {
        e.preventDefault();
        ProductImagesCombination_GetAll(idProduct);
        $('#modal_CombinationImages').modal('show');
    })

    var information_tab = true;
    var description_tab = true;
    var categories_tab = true;
    var images_tab = true;

    $('#information_tab').on('shown.bs.tab', function (e) {
        e.preventDefault();
        if (information_tab) {
            Manufacturer_GetAll();
            ProductInformation_GetDetail(idProduct);
            information_tab = false;
        };
    });
    $('#description_tab').on('shown.bs.tab', function (e) {
        e.preventDefault();
        if (description_tab) {
            ProductDescription_GetDetail(idProduct);
            description_tab = false;
        };
    });
    $('#categories_tab').on('shown.bs.tab', function (e) {
        e.preventDefault();
        if (categories_tab) {
            Category_GetDataByIDCategoryParent(idProduct);
            categories_tab = false;
        };
    });

    $('#images_tab').on('shown.bs.tab', function (e) {
        e.preventDefault();
        if (images_tab) {
            ProductImages_GetAll(idProduct);
            images_tab = false;
        };
    });
})

function Manufacturer_GetAll() {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Manufacturer.asmx/GetAll",
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
        }
    });
}

function ProductInformation_GetDetail(idProduct) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/GetDetail_Information",
        data: JSON.stringify({
            idProduct: idProduct
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, n) {
            if (e.d != null) {
                ProductInformation_SetForm(e.d)
            } else {
                toastr.error("<b>Product is not exists.</b>");
                window.setTimeout(function () {
                    location.href = "./Default.aspx"
                }, 1e3)
            }
        },
        error: function (e, t, n) {
            if (e.status === 0) {
                toastr.error("<b>Not connect. Please, Verify Network.</b>")
            } else if (e.status == 404) {
                toastr.error("<b>Requested page not found. [404]</b>")
            } else if (e.status == 500) {
                toastr.error("<b>Internal Server Error [500]</b>")
            } else if (n === "parsererror") {
                toastr.error("<b>Requested JSON parse failed</b>")
            } else if (n === "timeout") {
                toastr.error("<b>Time out error</b>")
            } else if (n === "abort") {
                toastr.error("<b>Ajax request aborted</b>")
            } else {
                toastr.error("<b>Uncaught Error.</b>")
            }
            window.setTimeout(function () {
                location.href = "./Default.aspx"
            }, 1e3)
        },
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true,
                message: 'Processing...'
            });
        }
    })
}

function ProductDescription_GetDetail(idProduct) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/GetDetail_Description",
        data: JSON.stringify({
            idProduct: idProduct
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, n) {
            if (e.d != null) {
                ProductDescription_SetForm(e.d);
            } else {
                toastr.error("<b>Product is not exists.</b>");
                window.setTimeout(function () {
                    location.href = "./Default.aspx"
                }, 1e3)
            }
        },
        error: function (e, t, n) {
            if (e.status === 0) {
                toastr.error("<b>Not connect. Please, Verify Network.</b>")
            } else if (e.status == 404) {
                toastr.error("<b>Requested page not found. [404]</b>")
            } else if (e.status == 500) {
                toastr.error("<b>Internal Server Error [500]</b>")
            } else if (n === "parsererror") {
                toastr.error("<b>Requested JSON parse failed</b>")
            } else if (n === "timeout") {
                toastr.error("<b>Time out error</b>")
            } else if (n === "abort") {
                toastr.error("<b>Ajax request aborted</b>")
            } else {
                toastr.error("<b>Uncaught Error.</b>")
            }
            window.setTimeout(function () {
                location.href = "./Default.aspx"
            }, 1e3)
        },
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true,
                message: 'Processing...'
            });
        }
    })
}

function ProductInformation_SetForm(data) {
    var t = new Date;
    document.title = data.Name + " - WIT. Commerce";
    $("#page-title").html(data.Name + " <small>Product</small>");
    $("#breadcrumbName").html(data.Name);
    $("#hiddenIdManufacturer").val(data.IDProduct);

    $("#tbReference").val(data.ReferenceCode);
    $("#tbName").val(data.Name);
    $("#ddlManufacturer").select2('val', data.IDManufacturer);
    $("#tbCostPrice").val(data.PriceCogs);
    $("#tbBasePrice").val(data.PriceSale);
    $("#tbPrice").val(data.PriceSaleAfterDiscount);
    $("#tbWeight").val(data.Weight);

    if (data.TypeDiscountPercent) {
        $(".bootstrap-switch-id-cbTypeDiscount").removeClass("bootstrap-switch-off");
        $(".bootstrap-switch-id-cbTypeDiscount").addClass("bootstrap-switch-on");

        if ($('#tbDiscountPersentase').parent().hasClass('bootstrap-touchspin')) {
            $('#tbDiscountPersentase').removeClass('hide');
            $('#tbDiscountPersentase').parent().removeClass('hide');
            $('#tbDiscountPersentase').val(data.Discount);
        }
        $('#tbDiscountMoney').addClass('hide');
    } else {
        $(".bootstrap-switch-id-cbTypeDiscount").removeClass("bootstrap-switch-on");
        $(".bootstrap-switch-id-cbTypeDiscount").addClass("bootstrap-switch-off");
        if ($('#tbDiscountPersentase').parent().hasClass('bootstrap-touchspin')) {
            $('#tbDiscountPersentase').removeClass('hide');
            $('#tbDiscountPersentase').parent().addClass('hide');
        }
        $('#tbDiscountMoney').removeClass('hide');
        $('#tbDiscountMoney').val(data.Discount);
    }

    if (data.Active) {
        $(".bootstrap-switch-id-cbActive").removeClass("bootstrap-switch-off");
        $(".bootstrap-switch-id-cbActive").addClass("bootstrap-switch-on");
    } else {
        $(".bootstrap-switch-id-cbActive").removeClass("bootstrap-switch-on");
        $(".bootstrap-switch-id-cbActive").addClass("bootstrap-switch-off");
    }
    ProductInformation_ValidationUpdate($('#formProductInformation_Update'));
}

function ProductDescription_SetForm(data) {
    $("#tbShortDescription").code(data.ShortDescription);
    $("#tbDescription").code(data.Description);
    $("#tbNote").code(data.Note);
    ProductDescription_ValidationUpdate($('#formProductDescription_Update'));
}

function Simulate(priceSale, typeDiscount, discount) {
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
    })
}

function ProductInformation_ValidationUpdate(e) {
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
            var idProduct = $("#hiddenIdProduct").val();
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
            ProdutInformation_Update(idProduct, referenceCode, name, idManufacturer, priceCogs, priceSale, typeDiscount, discount, weight, active);
        }
    });
    //jQuery.validator.addMethod("exists", function (e, t, n) {
    //    var idAttribute = $("#hiddenIdAttribute").val();
    //    return Value_ValidationName_Insert(idAttribute, e);
    //}, "Value is already exists.")
}

function ProductDescription_ValidationUpdate(e) {
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        ignore: "",
        messages: {},
        rules: {
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
            var idProduct = $("#hiddenIdProduct").val();
            var shortDescription = $('#tbShortDescription').code();
            var description = $('#tbDescription').code();
            var note = $('#tbNote').code();
            ProdutDescription_Update(idProduct, shortDescription, description, note);
        }
    });
}

function ProdutInformation_Update(idProduct, referenceCode, name, idManufacturer, priceCogs, priceSale, typeDiscount, discount, weight, active) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/Update_Information",
        data: JSON.stringify({
            idProduct: idProduct,
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
                ProductInformation_GetDetail(idProduct);
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

function ProdutDescription_Update(idProduct, shortDescription, description, note) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/Update_Description",
        data: JSON.stringify({
            idProduct: idProduct,
            shortDescription: shortDescription,
            description: description,
            note: note
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, n, r) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
                ProductDescription_GetDetail(idProduct);
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

function Category_GetDataByIDCategoryParent(idProduct) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/GetTree_ProductCategory",
        data: '{"idProduct":"' + idProduct + '"}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, xhr) {
            var i, j, r = Array();
            $("#tree_Category").jstree('destroy');
            $('#tree_Category').on('changed.jstree', function (e, data) {
                for (i = 0, j = data.selected.length; i < j; i++) {
                    r.push(data.instance.get_node(data.selected[i]).text);
                }
                r = Array();
            }).jstree({
                'plugins': ['themes', 'html_data', 'checkbox', 'ui'],
                'core': {
                    "themes": {
                        "responsive": true
                    },
                    'data': data.d
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

            $('#submitCategory').click(function (e, data) {
                var arr = $("#tree_Category").jstree("get_selected");
                ProdutCategory_Update(idProduct, arr);
            });

        },
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true,
                message: 'Processing...'
            });
        }
    });
}

function ProdutCategory_Update(idProduct, idCategory) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/Update_Category",
        data: JSON.stringify({
            idProduct: idProduct,
            idCategory: idCategory
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, n, r) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
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

function ProductImages_GetAll(idProduct) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/GetDetail_Images",
        data: JSON.stringify({
            idProduct: idProduct
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var data = response.d;
            var s="";
            if (data != null && data.length != 0) {
                for (var i = 0; i < data.length; i++) {
                    s+="<tr>";
                    s+="<td>";
                    s+="<img class='img-responsive' src='"+window.location.origin +"/assets/images/product/"+data[i].ImageSrc+"' alt='' />";
                    s+="</td>";
                    s+="<td>";
                    s+=data[i].ImageSrc;
                    s+="</td>";
                    s+="<td>";
                    if (data[i].IsCover) {
                        s+="<a href='javascript:;' class='btn btn-sm btn_changestatus green tooltips' data-original-title='Cover' data_id='"+data[i].IDProduct_Image+"' data_name='"+data[i].ImageSrc+"'><i class='glyphicon glyphicon-ok'></i></a>";
                    }
                    else
                    {
                        s+="<a href='javascript:;' class='btn btn-sm btn_changestatus red tooltips' data-original-title='Not Cover' data_id='"+data[i].IDProduct_Image+"' data_name='"+data[i].ImageSrc+"'><i class='glyphicon glyphicon-remove'></i></a>";   
                    }
                    s+="</td>";
                    s+="<td>";
                    s+="<a href='javascript:;' class='btn red btn-sm btn_delete' data_id='"+data[i].IDProduct_Image+"' data_name='"+data[i].ImageSrc+"'><i class='glyphicon glyphicon-trash'></i> Remove</a>";
                    s+="</td>";
                    s+="</tr>";
                }
            }
            else{
               s+="<tr>";
               s+="<td colspan='4'>";
               s+="Sorry, this product don't have images.";
               s+="</td>";
               s+="</tr>";
           }
           $("#dtProductImage").empty();
           $("#dtProductImage").append(s);
           $("#sectionImages").empty();
           $("#sectionImages").append('Images ('+data.length+')');


           $('.btn_changestatus').click(function (e) {
            e.preventDefault();
            var t = $(this).attr("data_id");
            var i = $(this).attr("data-original-title");

            if (i== "Cover") {
                bootbox.alert("Sorry, this image is already cover for this product");
            }
            else{
                bootbox.confirm("Are you sure change cover for this product with this image?", function (j) {
                    if (j) {
                        Update_StatusImage(idProduct, t);
                    }
                });
            }
        });

           $('.btn_delete').click(function (e) {
            e.preventDefault();
            var t = $(this).attr("data_id");
            var i = $(this).attr("data_name");
            bootbox.confirm("Are you sure want to delete <b>" + i + "</b> ?", function (j) {
                if (j) {
                    Delete_Image(idProduct, t);
                }
            });
        });
       },
       beforeSend: function () {
        Metronic.blockUI({
            boxed: true,
            message: 'Processing...'
        });
    }
});
}

function Update_StatusImage(idProduct, idProduct_Image) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/Update_StatusImage",
        data: JSON.stringify({
            idProduct: idProduct,
            idProduct_Image: idProduct_Image
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, n, r) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
                ProductImages_GetAll(idProduct);
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

function Delete_Image(idProduct, idProduct_Image) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/Delete_Image",
        data: JSON.stringify({
            idProduct: idProduct,
            idProduct_Image: idProduct_Image
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, n, r) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
                ProductImages_GetAll(idProduct);
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

function Attribute_GetAll() {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Attribute.asmx/GetAll",
        data: '{}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var data = response.d;
            var d = [];

            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    d.push({ id: data[i].IDAttribute, text: data[i].Name });
                }
                $("#ddlAttribute").select2({
                    data: d
                }).on("select2-selecting", function (e) {
                    Value_GetAll(e.val);
                });

                $("#ddlAttribute").select2("val", data[0].IDAttribute);
                Value_GetAll(data[0].IDAttribute);
            }
            else {
                toasr.error('Failed load Attributes.');
            }
        },
        beforeSend: function () {
            Metronic.blockUI({
                target: '#modal_CombinationInsert',
                overlayColor: 'none',
                cenrerY: true,
                boxed: true,
                message: 'Processing...'
            });
        },
        complete: function () {
            Metronic.unblockUI('#modal_CombinationInsert');
        }
    });
}

function Value_GetAll(idAttribute) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Value.asmx/GetAll",
        data: JSON.stringify({
            idAttribute: idAttribute
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var data = response.d;
            var d = [];

            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    d.push({ id: data[i].IDValue, text: data[i].Name });
                }
                $("#ddlValue").select2({
                    data: d
                });
            }
            else {
                toasr.error('Failed load Values.');
            }
        },
        beforeSend: function () {
            Metronic.blockUI({
                target: '#modal_CombinationInsert',
                overlayColor: 'none',
                cenrerY: true,
                boxed: true,
                message: 'Processing...'
            });
        },
        complete: function () {
            Metronic.unblockUI('#modal_CombinationInsert');
        }
    });
}



function ProductImagesCombination_GetAll(idProduct) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Product.asmx/GetDetail_Images",
        data: JSON.stringify({
            idProduct: idProduct
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var data = response.d;
            var s="";
            //load image untuk kombinasi
            var image = '';
            for (var i = 0; i < 20; i++) {
                image += '<label class="col-md-2"><input type="checkbox" id="cbImage' + data[0].IDProduct_Image + '" value="' + data[0].IDProduct_Image + '"><img style="max-width:100px;" src="'+ window.location.origin+'/assets/images/product/' + data[0].ImageSrc + '" alt="image" class="img-responsive" /></label>';
            }
            $("#listImages").empty();
            $("#listImages").append(image);
       },
       beforeSend: function () {
        Metronic.blockUI({
            boxed: true,
            message: 'Processing...'
        });
    }
});
}