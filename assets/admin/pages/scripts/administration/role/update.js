var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $("#menu_administration").addClass("active");
    $("#arrow_administration").addClass("open");
    $("#submenu_role").addClass("active");
    if (queryString('id')) {
        $("#HiddenIDRole").val(queryString("id"));
        Preload(parseInt(queryString('id')));

    }
    else {
        window.location = 'default.aspx';
    }

    $("#submitMenu").click(function (e) {
        e.preventDefault();
        SubmitMenus();
    });

    $("#submitOrderStatus").click(function (e) {
        e.preventDefault();
        SubmitOrderStatus();
    });

    $("#submitNotification").click(function (e) {
        e.preventDefault();
        UpdateNotification();
    });

    Validate_Update();
});

function LoadRole(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        if (i == 1)
            item += '<option selected="selected" value="' + data[i].IDRole + '">' + data[i].Name + '</option>';
        else
            item += '<option value="' + data[i].IDRole + '">' + data[i].Name + '</option>';
    }

    $(".role").html(item);
}

function Preload(id) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            var role = result.d.data.Role;
            //LoadRole(result.d.data.Role);
            $("input[name=Name]").val(role.Name);

            $("#treeMenu").jstree({
                'plugins': ['themes', 'html_data', 'checkbox', 'ui'],
                "checkbox": {
                    "three_state": false,
                },
                'core': {
                    "themes": {
                        "responsive": true
                    },
                    'data': result.d.data.TreeMenus
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

            //LOAD TAB MENUS
            var menus = result.d.data.SelectedMenus;
            ReloadMenus(menus);
            //END LOAD TAB MENUS

            $("#treeNotif").jstree({
                'plugins': ['themes', 'html_data', 'checkbox', 'ui'],
                "checkbox": {
                    "three_state": false,
                },
                'core': {
                    "themes": {
                        "responsive": true
                    },
                    'data': result.d.data.TreeOrderStatus
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

            //LOAD TAB MENUS
            var OrderStatus = result.d.data.SelectedOrderStatus;
            var Notification = result.d.data.Notification;
            ReloadOrderStatus(OrderStatus, Notification);
            //END LOAD TAB MENUS

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
        'c': 'berole',
        'm': 'detrole',
        'data': {
            'IDRole': id
        }
    });
}

function LoadRole(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<option value="' + data[i].IDRole + '">' + data[i].Name + '</option>';
    }

    $(".role").html(item);
}

function Validate_Update() {
    var e = $("#form_role");
    var t = $(".alert", e);
    e.validate({
        errorElement: "span",
        errorClass: "help-block",
        focusInvalid: true,
        onkeyup: false,
        messages: {},
        rules: {
            Name: {
                required: true,
                maxlength: 100
            },
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
            var sendData = {};
            $(".input", e).each(function (index, element) {
                sendData[$(this).attr("name")] = $(this).val();
                //console.log($(this).attr("name"));
            });
            if (isNaN(parseInt(queryString('id'))))
                window.location = 'default.aspx';
            sendData["IDRole"] = parseInt(queryString('id'));
            Update(sendData);
        }
    });
}

function Update(data) {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "success",
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "check"
            });
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "danger",
                message: result.d.message,
                close: true,
                reset: true,
                focus: true,
                closeInSeconds: 5,
                icon: "warning"
            });
        }
    };
    REST.sendRequest({
        'c': 'berole',
        'm': 'urole',
        'data': data
    });
}

function ReloadMenus(SelectedMenus) {
    //LOAD TAB CATEGORIES 
    var selectedCat = {};
    var cat = [];
    for (var i = 0; i < SelectedMenus.length; i++) {
        if (SelectedMenus[i].IDRole_Menu) {
            selectedCat = {
                id: SelectedMenus[i].IDRole_Menu,
                text: SelectedMenus[i].Name
            };
        }

        cat.push({
            id: SelectedMenus[i].IDRole_Menu,
            text: SelectedMenus[i].Name
        });
    }

    if (cat.length === 0) {
        Metronic.alert({
            container: "#bootstrap_alerts_menu",
            place: "append",
            type: "danger",
            message: "Please, choose Menus before you post this information.",
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: "0",
            icon: "warning"
        });
    }
    //END LOAD TAB CATEGORIES
}

function SubmitMenus() {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            ReloadMenus(data.d.data.SelectedMenus);
            Metronic.alert({
                container: "#bootstrap_alerts_menu",
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
                container: "#bootstrap_alerts_menu",
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

    // GET UNDETERMINED DATA JS TREE (PARENT MENU)
    var selected = $("#treeMenu").jstree(true).get_selected(), i, j;
    for (i = 0, j = selected.length; i < j; i++) {
        selected = selected.concat($("#treeMenu").jstree(true).get_node(selected[i]).parents);
    }
    selected = $.unique(selected);

    selected = $.grep(selected, function (data) {
        return data != "#";
    })
    // END GET UNDETERMINED DATA JS TREE (PARENT MENU)

    //GET DATA
    var sendData = {};
    sendData["IDMenu"] = selected
    sendData["IDRole"] = +$("#HiddenIDRole").val();
    //END GET DATA

    REST.sendRequest({
        'c': 'berole',
        'm': 'urolemenu',
        'data': sendData
    });
}

function ReloadOrderStatus(SelectedStatus, Notification) {
    //LOAD TAB CATEGORIES 
    var selectedOrdStat = {};
    var ordStat = [];
    for (var i = 0; i < SelectedStatus.length; i++) {
        if (SelectedStatus[i].IDOrderStatus == Notification.IDOrderStatus) {
            selectedOrdStat = {
                id: SelectedStatus[i].IDOrderStatus,
                text: SelectedStatus[i].Name
            };
        }

        ordStat.push({
            id: SelectedStatus[i].IDOrderStatus,
            text: SelectedStatus[i].Name
        });
    }

    if (selectedOrdStat != null) {
        $("#Notification").select2({
            data: ordStat
        }).select2("data", selectedOrdStat);
    }
    else {
        $("#Notification").select2({
            data: ordStat
        });
    }


    if (ordStat.length === 0) {
        Metronic.alert({
            container: "#bootstrap_alerts_handle",
            place: "append",
            type: "danger",
            message: "Please, choose Order Statuses before you post this information.",
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: "0",
            icon: "warning"
        });
    }
    //END LOAD TAB CATEGORIES
}
function SubmitOrderStatus() {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            ReloadOrderStatus(data.d.data.SelectedOrderStatus, data.d.data.Notification);
            Metronic.alert({
                container: "#bootstrap_alerts_handle",
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
                container: "#bootstrap_alerts_handle",
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
    sendData["IDOrderStatus"] = $("#treeNotif").jstree(true).get_selected();
    sendData["IDRole"] = +$("#HiddenIDRole").val();
    //END GET DATA

    REST.sendRequest({
        'c': 'berole',
        'm': 'uroleordstat',
        'data': sendData
    });
}

function UpdateNotification() {
    REST.onSuccess = function (data) {
        var result = data.d;
        if (result.success) {
            Metronic.alert({
                container: "#bootstrap_alerts_notif",
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
                container: "#bootstrap_alerts_notif",
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

    if ($("#Notification").select2("data") != null) {
        //GET DATA
        var sendData = {};
        sendData["IDOrderStatus"] = +$("#Notification").select2("data").id;
        sendData["IDRole"] = +$("#HiddenIDRole").val();
        //END GET DATA

        REST.sendRequest({
            'c': 'berole',
            'm': 'unotif',
            'data': sendData
        });
    }
    else {
        Metronic.alert({
            container: "#bootstrap_alerts_cat",
            place: "append",
            type: "danger",
            message: "Please choose notification, or check order status if there's no category yet",
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: "0",
            icon: "warning"
        });
    }
}
