jQuery(document).ready(function () {
    // Setup Error
    $.ajaxSetup({
        error: function (jqXHR, errorType, exception) {
            if (jqXHR.status === 0) {
                toastr.error('<b>Not connect. Please, Verify Network.</b>');
            } else if (jqXHR.status == 404) {
                toastr.error('<b>Requested page not found. [404]</b>');
            } else if (jqXHR.status == 500) {
                toastr.error('<b>Internal Server Error [500]</b>');
            } else if (exception === 'parsererror') {
                toastr.error('<b>Requested JSON parse failed</b>');
            } else if (exception === 'timeout') {
                toastr.error('<b>Time out error</b>');
            } else if (exception === 'abort') {
                toastr.error('<b>Ajax request aborted</b>');
            } else {
                toastr.error('<b>Uncaught Error.');
            }
        }
    });

    //MASKING TEXTBOX
    $(".number-only").inputmask({
        'mask': '9',
        'repeat': '*',
        'greedy': false
    });

    $('.money').inputmask("decimal", {
        allowPlus: false,
        allowMinus: false,
        rightAlignNumerics: false,
        radixPoint: ",",
        autoGroup: true,
        groupSeparator: ".",
        groupSize: 3
    });
});

//DATETIME PICKER
var DateTimePicker = function () {
    return {
        init: function () {
            $(".datetime-picker").datetimepicker({
                isRTL: Metronic.isRTL(),
                format: "DD-MMM-YYYY  hh:mm",
                autoclose: true,
                todayBtn: true,
                startDate: moment().format("DD-MMM-YYYY  hh:mm"),
                pickerPosition: (Metronic.isRTL() ? "bottom-right" : "bottom-left"),
                minuteStep: 10
            });
        }
    }

}();


//SUMMERNOTE INIT
var Summernote = function () {
    return {
        init: function () {
            $(".summernote").summernote({ height: 150 });
            $(".summernote-simple").summernote({
                height: 150,
                toolbar: [
                    ['style', ['bold', 'italic', 'underline', 'clear']],
                    ['font', ['strikethrough']],
                    ['color', ['color']],
                    ['para', ['ul', 'ol', 'paragraph']],
                    ['height', ['height']],
                ]
            });
        }
    };
}();
//END SUMMERNOTE INIT

function queryString(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function ReplacePoint(param) {
    return param.replace(/\./g, "");
}

// Persentase
var TouchSpinPercent = function () {
    return {
        init: function () {
            $.ajax({
                type: "GET",
                url: window.location.origin + "/assets/global/plugins/bootstrap-touchspin/bootstrap.touchspin.js",
                success: function () {
                    $(".persentase").TouchSpin({
                        buttondown_class: 'btn blue',
                        buttonup_class: 'btn blue',
                        min: 0,
                        max: 100,
                        step: 5,
                        decimals: 0,
                        boostat: 8,
                        maxboostedstep: 10,
                        postfix: '%'
                    });
                },
                dataType: "script",
                cache: false
            });
        }
    };
}();

// Weight
var TouchSpinWeight = function () {
    return {
        init: function () {
            $.ajax({
                type: "GET",
                url: window.location.origin + "/assets/global/plugins/bootstrap-touchspin/bootstrap.touchspin.js",
                success: function () {
                    $(".weight").TouchSpin({
                        buttondown_class: 'btn blue',
                        buttonup_class: 'btn blue',
                        min: 0,
                        max: 100,
                        step: 0.1,
                        decimals: 2,
                        boostat: 8,
                        maxboostedstep: 10,
                        postfix: 'Kg'
                    });
                },
                dataType: "script",
                cache: false
            });
        }
    };
}();

//INTEGER SPIN
var TouchSpinInteger = function () {
    return {
        init: function () {
            $.ajax({
                type: "GET",
                url: window.location.origin + "/assets/global/plugins/bootstrap-touchspin/bootstrap.touchspin.js",
                success: function () {
                    $(".int").TouchSpin({
                        //verticalbuttons : true,
                        //verticalupclass: 'glyphicon glyphicon-chevron-up',
                        //verticaldownclass: 'glyphicon glyphicon-chevron-down',
                        buttondown_class: 'btn blue glyphicon',
                        buttonup_class: 'btn blue glyphicon',
                        min: 0,
                        initval: "0",
                        max: 10000,
                        step: 1,
                        decimals: 0,
                        boostat: 8,
                        maxboostedstep: 10,
                        mousewheel: false,
                    });
                },
                dataType: "script",
                cache: false
            });
        }
    };
}();
// IDR
var TouchSpinMoney = function () {
    return {
        init: function () {
            $.ajax({
                type: "GET",
                url: window.location.origin + "/assets/global/plugins/bootstrap-touchspin/bootstrap.touchspin.js",
                success: function () {
                    $(".money").TouchSpin({
                        buttondown_class: 'btn blue',
                        buttonup_class: 'btn blue',
                        min: 0,
                        max: 10000000000,
                        step: 1000,
                        decimals: 2,
                        boostat: 8,
                        maxboostedstep: 10,
                        postfix: 'IDR'
                    });
                },
                dataType: "script",
                cache: false
            });
        }
    };
}();

// Toastr
var Toastr = function () {
    return {
        init: function () {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-top-right",
                "onclick": null,
                "showDuration": "500",
                "hideDuration": "100",
                "timeOut": "0",
                "extendedTimeOut": "0",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
        }
    };
}();

// Reset Form
$.fn.clearForm = function () {
    return this.each(function () {
        var type = this.type, tag = this.tagName.toLowerCase();
        if (tag == 'form')
            return $(':input', this).clearForm();
        if (type == 'text' || type == 'password' || tag == 'textarea')
            this.value = '';
        else if (type == 'checkbox' || type == 'radio') {
            this.checked = false;
        }
        else if (tag == 'select')
            this.selectedIndex = -1;
    });
};

// Get Image Base64
function getImageDataURL(url, success, error) {
    var data, canvas, ctx;
    var img = new Image();
    img.onload = function () {
        // Create the canvas element.
        canvas = document.createElement('canvas');
        canvas.width = img.width * 0.3;
        canvas.height = img.height * 0.3;
        // Get '2d' context and draw the image.
        ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
        // Get canvas data URL
        try {
            data = canvas.toDataURL();
            success({ image: img, data: data });
        } catch (e) {
            error(e);
        }
    }
    // Load image URL.
    try {
        img.src = url;
    } catch (e) {
        error(e);
    }
}
