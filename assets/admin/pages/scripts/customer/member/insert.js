var REST = new OF({
    'url': '/api/services.asmx/request'
});

var format = "";

$(document).ready(function () {
    Preload();
    SubmitRegistration();
    $(".alert").hide();
    if (queryString("email")) {
        $("input[name=Email]").val(queryString("email"));
        $("input[name=Email]").attr("readonly", "true");
    }
});

function Preload() {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            var Country = [];
            for (var i = 0; i < data.Country.length; i++) {
                Country.push({
                    id: data.Country[i].IDCountry,
                    text: data.Country[i].Name
                });

                $("#Country").select2({
                    data: Country,
                    allowClear: false
                }).select2("data", Country[0]).select2("readonly", true);
            }

            var Province = [];
            for (var i = 0; i < data.Province.length; i++) {
                Province.push({
                    id: data.Province[i].IDProvince,
                    text: data.Province[i].Name
                });
            }

            $("#Province").select2({
                data: Province,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: Province[0].id, text: Province[0].text };
                    callback(data);
                }
            });

            $("#Province").on("change", function () {
                LoadCity(+$(this).select2("data").id);
            });

            LoadCity(Province[0].id);
        }
    };
    REST.sendRequest({
        'c': 'fereg',
        'm': 'regpreload',
        'data': {}
    });

    $.ajax({
        url: "/modules/MemberPoint/MemberPoint.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "MemberPoint",
            t: "be",
            m: "listgr",
            data: {}
        }),
        beforeSend: function () {
            Metronic.blockUI({
                boxed: true
            });
        },
        success: function (result) {
            var data = result.data
            var item = '';
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    item += '<option value=' + data[i].IDCustomer_Group + ' selected="selected">' + data[i].Name + '</option>';
                }
                else {
                    item += '<option value=' + data[i].IDCustomer_Group + '>' + data[i].Name + '</option>';
                }
            }

            $("#selectMember").html(item);
        },
        error: function (result) {
            Metronic.alert({
                container: "#bootstrap_alerts",
                place: "append",
                type: "danger",
                message: result.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: "3",
                icon: "warning"
            });
        },
        complete: function () {
            Metronic.unblockUI();
        },
    });
}

function LoadCity(idProvince) {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            var City = [];
            for (var i = 0; i < data.length; i++) {
                City.push({
                    id: data[i].IDCity,
                    text: data[i].Name
                });
            }

            $("#City").select2({
                data: City,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: City[0].id, text: City[0].text };
                    callback(data);
                }
            });

            LoadDistrict(City[0].id);
            $("#City").on("change", function () {
                LoadDistrict(+$(this).select2("data").id);
            });
        }
    };
    REST.sendRequest({
        'c': 'fereg',
        'm': 'rcty',
        'data': {
            'IDProvince': idProvince
        }
    });
}

function LoadDistrict(idCity) {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            var District = [];
            for (var i = 0; i < data.length; i++) {
                District.push({
                    id: data[i].IDDistrict,
                    text: data[i].Name
                });
            }

            $("#District").select2({
                data: District,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: District[0].id, text: District[0].text };
                    callback(data);
                }
            });
        }
    };
    REST.sendRequest({
        'c': 'fereg',
        'm': 'rdis',
        'data': {
            'IDCity': idCity
        }
    });
}

function SubmitRegistration() {
    var e = $("#FormMember");
    var t = $("#bootstrap_alert", e);
    jQuery.validator.addMethod("birthdate", function (e, t, n) {
        var date = moment($("#txtBirthdate").val(), "DD-MM-YYYY").format("YYYY-MM-DD");
        if (date == "Invalid date" || $("#txtBirthdate").val() == null || $("#txtBirthdate").val() == undefined)
            return false;
        else
            return true;
    }, jQuery.validator.format("Invalid birthdate"));
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            Password: {
                required: true,
                minlength: 5
            },
            RetypePassword: {
                required: true,
                minlength: 5,
                equalTo: "#password"
            },
            Email: {
                required: true,
                email: true
            },
            Address: {
                required: true
            },
            PhoneNumber: {
                required: true
            },
            PostalCode: {
                required: true
            },
            AddressName: {
                required: true
            },
            Birthdate: {
                birthdate: true
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
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "danger",
                message: "You have some form errors. Please check below. ",
                close: true,
                reset: true,
                focus: true,
                //closeInSeconds: 5,
                icon: "warning"
            });
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
        onkeyup: false,
        submitHandler: function (e) {
            t.hide();

            var sendData = {};
            $("input", e).each(function (index, element) {
                // element == this
                if (element.name != undefined && element.name != "") {
                    if (element.name == "Gender") {
                        sendData[element.name] = $('input:radio[name=' + element.name + ']:checked').val();
                        //console.log($('input:radio[name=' + element.name + ']:checked').val());
                    }
                    else if (element.name == "Country" || element.name == "Province" || element.name == "City" || element.name == "District") {
                        sendData[element.name] = $(element).select2("data").id;
                        //console.log($(element).select2("data").text);
                    }
                    else if (element.name == "Newsletter") {
                        if ($("#cbNewsletter").prop("checked")) {
                            sendData[element.name] = true;
                        }
                        else {
                            sendData[element.name] = false;
                        }
                    }
                    else {
                        sendData[element.name] = $(element).val();
                        //console.log($(element).val());
                    }
                }
            });
            sendData["Birthdate"] = moment($("#txtBirthdate").val(), "DD-MM-YYYY").format("YYYY-MM-DD");
            sendData["Address"] = $("#txtAddress").val();
            sendData["Address2"] = $("#txtAddress2").val();
            sendData["IDCustomerGroup"] = $("#selectMember").val();
            console.log(sendData);

            $.ajax({
                url: "/modules/MemberPoint/MemberPoint.ashx",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    c: "MemberPoint",
                    t: "be",
                    m: "addmem",
                    data: sendData
                }),
                beforeSend: function () {
                    Metronic.blockUI({
                        boxed: true
                    });
                },
                success: function (result) {
                    if (result.data.success) {
                        Metronic.alert({
                            container: "#bootstrap_alerts",
                            place: "append",
                            type: "success",
                            message: result.data.message,
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
                            message: result.data.message,
                            close: true,
                            reset: true,
                            focus: true,
                            closeInSeconds: "0",
                            icon: "warning"
                        });
                    }
                },
                error: function (result) {
                    Metronic.alert({
                        container: "#bootstrap_alerts",
                        place: "append",
                        type: "danger",
                        message: result.data.message,
                        close: true,
                        reset: true,
                        focus: true,
                        closeInSeconds: "0",
                        icon: "warning"
                    });
                },
                complete: function () {
                    Metronic.unblockUI();
                },
            });
        }
    });
}