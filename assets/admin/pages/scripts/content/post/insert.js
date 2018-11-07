var REST = new OF({
    'url': '/api/services.asmx/Request',
});

function Post_Insert() {
    var sendData = {};
    sendData["postTitle"] = $("#tbTitle").val();
    sendData["postShortContent"] = $("#tbShortContent").code();
    sendData["postContent"] = $("#tbContent").code();
    sendData["active"] = $('input[name="cbActive"]').bootstrapSwitch("state");
    sendData["idPage"] = +$("#ddlPage").val();

    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            $("input", ".form-group").val("");
            //$(".alert-success").show();
            //Metronic.scrollTo($("#alert-succes"));
            window.location = "Update.aspx?id=" + data.d.data;
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "success",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "checked"
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "warning"
            });
        }
    }
    REST.sendRequest({
        'c': 'bepost',
        'm': 'ipost',
        'data': sendData
    });
}

function Post_Insert_Validation() {
    var e = $("#post_insert");
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            tbShortContent: {
                required: true,
            },
            tbContent: {
                required: true,
            },
            tbTitle: {
                required: true,
                minlength: 3
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
            Metronic.scrollTo(t, -200)
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
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();
            Post_Insert();
        }
    });
}

function Preload_Page()
{
    REST.onSuccess = function (data) {
        if (data.d.success) {
            var page = data.d.data;

            var ddlPage = [];
            for (var i = 0; i < page.length; i++) {
                ddlPage.push({
                    id: page[i].IDPage,
                    text: page[i].Page_Title
                });
            }

            $("#ddlPage").select2({
                data: ddlPage,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: ddlPage[0].id, text: ddlPage[0].text };
                    callback(data);
                }
            }).select2("data", ddlPage[0]).select2("readonly", false);

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
        'c': 'bepag',
        'm': 'preload',
        'data': { }
    });
}

$(document).ready(function () {

    $(".summernote-simple").summernote({
        height: 150,
        toolbar: [
          ['style', ['bold', 'italic', 'underline', 'clear']],
          ['font', ['strikethrough']],
          ['fontsize', ['fontsize']],
          ['color', ['color']],
          ['para', ['ul', 'ol', 'paragraph']],
          ['height', ['height']]
        ]
    });

    Post_Insert_Validation($("#post_insert"));

    Preload_Page();

    $("#btnSubmit").click(function (e) {
        e.preventDefault();
        var form = $("#post_insert");
        if (form.valid())
            Post_Insert();
    });
});