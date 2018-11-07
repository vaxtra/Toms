var REST = new OF({
    'url': '/api/services.asmx/request'
});

function Preload(id) {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            $("#viewModal").modal("show");
            $(".modal-title").text(result.data.Name);
            var item = '';
            $.each(result.data, function (indexInArray, valueOfElement) {
                if (indexInArray == "Active") {
                    $("input[type='checkbox']").bootstrapSwitch("state", valueOfElement);
                }
                else if (indexInArray == "Logo") {
                    $("#imgLogo").attr("src", "/assets/images/manufacturer/" + result.data.Logo + "?d=" + Date());
                }
                else if (indexInArray == "Description") {
                    $("[name='" + indexInArray + "']", "#form_manufacturer").code(valueOfElement);
                }
                else {
                    $("[name='" + indexInArray + "']", "#form_manufacturer").val(valueOfElement);
                }
            });
            $("#view_manufacturer").find(".form-body").empty();
            $("#view_manufacturer").find(".form-body").append(item);
        }
    };
    REST.sendRequest({
        'c': 'beman',
        'm': 'upreload',
        'data': { 'id': id }
    });
}

function Update() {
    var e = $("#form_manufacturer");
    var t = $(".alert-danger", e);
    var s = $(".alert-success", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Name: {
                required: true
            }
        },
        errorPlacement: function (e, t) {
            if (t.parent(".input-group").size() > 0) {
                e.insertAfter(t.parent(".input-group"));
            } else if (t.attr("data-error-container")) {
                e.appendTo(t.attr("data-error-container"));
            } else if (t.parents(".radio-list").size() > 0) {
                e.appendTo(t.parents(".radio-list").attr("data-error-container"));
            } else if (t.parents(".radio-inline").size() > 0) {
                e.appendTo(t.parents(".radio-inline").attr("data-error-container"));
            } else if (t.parents(".checkbox-list").size() > 0) {
                e.appendTo(t.parents(".checkbox-list").attr("data-error-container"));
            } else if (t.parents(".checkbox-inline").size() > 0) {
                e.appendTo(t.parents(".checkbox-inline").attr("data-error-container"));
            } else if (t.parents(".input-group").size() > 0) {
                e.insertAfter(t.parents(".input-group"));
            } else {
                e.insertAfter(t);
            }
        },
        invalidHandler: function (e, n) {
            t.show();
            Metronic.scrollTo(t, -200);
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
        submitHandler: function (e) {
            t.hide();
            s.hide();

            var sendData = {};
            sendData["IDManufacturer"] = +$("#HiddenIDManufacturer").val();
            $("#form_manufacturer .input").each(function (index, element) {
                if ($(this).attr("name") == "Description") {
                    sendData[$(this).attr("name")] = $(this).code();
                }
                else if ($(this).attr("name") == "Active") {
                    sendData[$(this).attr("name")] = $(this).bootstrapSwitch("state");
                }
                else {
                    sendData[$(this).attr("name")] = $(this).val();
                }
            });

            sendData["baseImage"] = "";
            sendData["fnImage"] = "";
            if ($(".fileinput-preview").find("img").length == 1) {
                sendData["baseImage"] = $(".fileinput-preview").find("img").eq(0).attr("src");
                sendData["fnImage"] = $("#fuImage").val();
            }

            REST.onSuccess = function (data) {
                var result = data.d;
                if (result.success) {
                    Metronic.alert({
                        container: "#bootstrap_alerts",
                        place: "append", // append or prepent in container 
                        type: "success",  // alert's type
                        message: result.message,  // alert's message
                        close: true, // make alert closable
                        reset: true, // close all previouse alerts first
                        focus: true, // auto scroll to the alert after shown
                        closeInSeconds: "0", // auto close after defined seconds
                        icon: "check" // put icon before the message
                    });
                }
                else {
                    Metronic.alert({
                        container: "#bootstrap_alerts",
                        place: "append", // append or prepent in container 
                        type: "danger",  // alert's type
                        message: result.message,  // alert's message
                        close: true, // make alert closable
                        reset: true, // close all previouse alerts first
                        focus: true, // auto scroll to the alert after shown
                        closeInSeconds: "0", // auto close after defined seconds
                        icon: "warning" // put icon before the message
                    });
                }
            };

            REST.sendRequest({
                'c': 'beman',
                'm': 'uman',
                'data': sendData
            });
        }
    });
}

$(document).ready(function () {
    if (!queryString("id"))
        window.location = "./default.aspx";

    $("#menu_catalog").addClass("active");
    $("#arrow_catalog").addClass("open");
    $("#submenu_manufacturer").addClass("active");

    $("input[type='checkbox']").bootstrapSwitch();
    $(".summernote-simple").summernote({
        height: 150,
        toolbar: [
          ['style', ['bold', 'italic', 'underline', 'clear']],
          ['font', ['strikethrough']],
          ['fontsize', ['fontsize']],
          ['color', ['color']],
          ['para', ['ul', 'ol', 'paragraph']],
        ]
    });

    $("#HiddenIDManufacturer").val(queryString("id"));
    Preload(+$("#HiddenIDManufacturer").val());
    Update();
});