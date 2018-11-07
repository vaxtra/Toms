function Manufacturer_GetDetail(e) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Manufacturer.asmx/GetDetail",
        data: JSON.stringify({
            idManufacturer: e
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, n) {
            if (e.d != null) {
                Manufacturer_SetForm(e.d)
            } else {
                toastr.error("<b>Manufacturer is not exists.</b>");
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
        }
    })
}

function Manufacturer_SetForm(e) {
    var t = new Date;
    document.title = e.Name + " - WIT. Commerce";
    $("#page-title").html(e.Name + " <small>Manufacturer</small>");
    $("#breadcrumbName").html(e.Name);
    $("#hiddenIdManufacturer").val(e.IDManufacturer);
    var n = function (e) {
        $("#imgLogo").attr("src", e.data)
    };
    var r = function (n) {
        $("#imgLogo").attr("src", window.location.origin + "/assets/images/manufacturer/" + e.Logo + "?v=" + t.getTime())
    };
    getImageDataURL(window.location.origin + "/assets/images/manufacturer/" + e.Logo + "?v=" + t.getTime(), n, r, .3);

    $("#tbName").val(e.Name);
    $("#tbEmail").val(e.Email);
    $("#tbPhone").val(e.Phone);
    $("#tbConsignment").val(e.Consignment);
    $("#tbAddress").val(e.Address);
    if (e.Active) {
        $(".bootstrap-switch-id-cbActive").removeClass("bootstrap-switch-off");
        $(".bootstrap-switch-id-cbActive").addClass("bootstrap-switch-on");
        $("#cbActive").attr("checked", "true")
    } else {
        $(".bootstrap-switch-id-cbActive").removeClass("bootstrap-switch-on");
        $(".bootstrap-switch-id-cbActive").addClass("bootstrap-switch-off");
        $("#cbActive").attr("checked", "false")
    }
    $("#tbShortDescription").code(e.ShortDescription);
    $("#tbDescription").code(e.Description);
    Validation($("#formManufacturer_Update"), $("#hiddenIdManufacturer").val())
}

function Validation(e, t) {
    var n = $(".alert-danger", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        ignore: "",
        messages: {},
        rules: {
            fuLogo: {
                extension: "png|jpe?g|gif",
                size: true
            },
            tbName: {
                required: true,
                minlength: 3,
                maxlength: 25,
                exists: true
            },
            tbEmail: {
                email: true,
                maxlength: 50
            },
            tbPhone: {
                phone: true,
                maxlength: 20
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
        invalidHandler: function (e, t) {
            n.show();
            Metronic.scrollTo(n, -200);
            $("#simple").trigger("click")
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
            n.hide();
            var r = document.getElementById("fuLogo").files[0];
            var i = $("#tbName").val();
            var s = $("#tbEmail").val();
            var o = $("#tbPhone").val();
            var u = $("#tbConsignment").val();
            var a = true;
            if ($(".bootstrap-switch-id-cbActive").hasClass("bootstrap-switch-off")) {
                a = false
            }
            var f = $("#tbAddress").val();
            var l = $("#tbShortDescription").code();
            var c = $("#tbDescription").code();
            if ($("#fuLogo").val() !== "") {
                var h = new FileReader;
                h.onload = function (e) {
                    var n = e.target.result;
                    var r = $("#fuLogo").val().split("/").pop().split("\\").pop();
                    Manufacturer_Update(t, n, r, i, s, o, u, a, f, l, c)
                };
                h.readAsDataURL(r)
            } else {
                Manufacturer_Update(t, "", "", i, s, o, u, a, f, l, c)
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
    jQuery.validator.addMethod("phone", function (e, t, n) {
        return /^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$/.test(e)
    }, jQuery.validator.format("Please enter a valid phone number."));
    jQuery.extend(jQuery.validator.messages, {
        extension: jQuery.validator.format("Allowed Extension are : png|jpe?g|gif.")
    });
    jQuery.validator.addMethod("exists", function (e, n, r) {
        return Manufacturer_ValidationName_Update(t, e)
    }, "Manufacturer is already exists.")
}

function Manufacturer_ValidationName_Update(e, t) {
    var n = false;
    $("#tbName").attr("readonly", true).attr("disabled", true).addClass("spinner");
    $.ajax({
        async: false,
        type: "POST",
        url: window.location.origin + "/api/Backend_Manufacturer.asmx/ValidationName_Update",
        data: '{"idManufacturer":"' + e + '", "name":"' + t + '"}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, r) {
            if (e.d) n = true
        },
        error: function (e, t, r) {
            if (e.status === 0) {
                toastr.error("<b>Not connect. Please, Verify Network.</b>")
            } else if (e.status == 404) {
                toastr.error("<b>Requested page not found. [404]</b>")
            } else if (e.status == 500) {
                toastr.error("<b>Internal Server Error [500]</b>")
            } else if (r === "parsererror") {
                toastr.error("<b>Requested JSON parse failed</b>")
            } else if (r === "timeout") {
                toastr.error("<b>Time out error</b>")
            } else if (r === "abort") {
                toastr.error("<b>Ajax request aborted</b>")
            } else {
                toastr.error("<b>Uncaught Error.</b>")
            }
            n = true
        }
    });
    $("#tbName").attr("readonly", false).attr("disabled", false).removeClass("spinner");
    return n
}

function Manufacturer_Update(e, t, n, r, i, s, o, u, a, f, l) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Manufacturer.asmx/Update",
        data: JSON.stringify({
            idManufacturer: e,
            imgLogo: t,
            fnLogo: n,
            name: r,
            email: i,
            phone: s,
            consignment: o,
            active: u,
            address: a,
            shortDescription: f,
            description: l
        }),
        async: true,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, n) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
                Metronic.scrollTo($("#formManufacturer_Update"), -200)
            } else toastr.error(e.d.Deskripsi)
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
        }
    });
    window.setTimeout(function () {
        location.href = "./Default.aspx"
    }, 1e3)
}
$(document).ready(function () {
    $(document).ajaxStart(function () {
        Metronic.blockUI({
            boxed: true
        })
    });
    $(document).ajaxStop(function () {
        Metronic.unblockUI();
        console.clear()
    });
    var e = queryString("id");
    if (e !== "") {
        TouchSpin.init();
        $(".summernote").summernote({
            height: 200
        });
        $("#menu_catalog").addClass("active");
        $("#submenu_manufacturer").addClass("active");
        $("#arrow_catalog").addClass("open");
        Manufacturer_GetDetail(e)
    } else {
        toastr.error("<b>Manufacturer is not exists.</b>");
        window.setTimeout(function () {
            location.href = "./Default.aspx"
        }, 1e3)
    }
})