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
    TouchSpin.init();
    tfm_path = '/plugins/tinyfilemanager'
    tinymce.init({
        height: 300,
        selector: ".tinymce",
        plugins: [
        "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
        "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
        "save table contextmenu directionality emoticons template paste textcolor tinyfilemanager.net wordcount"
        ],
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons",
        relative_urls: true,
        document_base_url: 'http://dressing.wurs.com/'
    });

    $("#menu_content").addClass("active");
    $("#submenu_page").addClass("active");
    $("#menu_content").addClass("open");
    Validation($("#formPage_Insert"));
});

function Validation(e) {
    var t = $(".alert-danger", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        ignore: "",
        messages: {},
        rules: {
            tbTitle: {
                required: true,
                minlength: 3,
                maxlength: 25
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
            Metronic.scrollTo(t, -200);
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
            var title = $("#tbTitle").val();
            var active = true;
            if (!$("#cbActive").is(":checked")) {
                active = false
            }
            var content = tinymce.get('tbContent').getContent();
            Page_Insert(title, active, content);
        }
    });
}

function Page_Insert(title, active, content) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/api/Backend_Page.asmx/Add",
        data: JSON.stringify({
            title: title,
            active: active,
            content: content
        }),
        async: true,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e, t, n) {
            if (e.d.Status == "Success") {
                toastr.success(e.d.Deskripsi);
                $("#formPage_Insert").clearForm();
                Metronic.scrollTo($("#formPage_Insert"), -200);
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
    })
}