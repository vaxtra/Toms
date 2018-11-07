function OF(opt) {
    //Toastr.init();

    this.options = {
        'url': 'services.asmx/Request',
        'type': 'POST',
        'dataType': 'json',
        'data': null,
        'c': '',
        'm': ''
    };

    //SET INIT OPTIONS
    var _options = this.options;
    if (opt != undefined) {
        $.each(opt, function (indexInArray, valueOfElement) {
            _options[indexInArray] = opt[indexInArray];
        });
    }
    //END SET INIT OPTIONS

    this.onSuccess = function (data) {
    };

    this.onComplete = function (data) {
    };

    this.onError = function (data) {
        Metronic.unblockUI();
    };

    this.onComplete = function () {
        Metronic.unblockUI();
    };

    this.beforeSend = function () {
        Metronic.blockUI({
            boxed: true
        });
    };

    this.sendRequest = function (param) {
        if (param == undefined) {
            param = {
                'data': {
                    'c': this.options['c'],
                    'm': this.options['m'],
                    'data1': this.options['data']
                }
            };
        }
        if ($("#token").val()) {
            $.ajax({
                type: this.options['type'],
                contentType: "application/json; charset=utf-8",
                dataType: this.options['dataType'],
                url: this.options['url'],
                data: JSON.stringify({ "data": param, 'token': $("#token").val() }),
                success: this.onSuccess,
                error: this.onError,
                complete: this.onComplete,
                beforeSend: this.beforeSend
            }).done(function () {
                Metronic.unblockUI();
            });
        }
        else {
            $.ajax({
                type: this.options['type'],
                contentType: "application/json; charset=utf-8",
                dataType: this.options['dataType'],
                url: this.options['url'],
                data: JSON.stringify({ "data": param }),
                success: this.onSuccess,
                error: this.onError,
                complete: this.onComplete,
                beforeSend: this.beforeSend
            }).done(function () {
                Metronic.unblockUI();
            });
        }
    };
}