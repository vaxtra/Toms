var REST_Master = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    $(".logout").click(function (e) {
        e.preventDefault();
        Logout();
    });

    PreloadFirstMaster();
});

function PreloadFirstMaster() {
    REST_Master.onSuccess = function (result) {
        if (result.d.success) {

            var SystemStatus = result.d.data.SystemStatus;
            if (SystemStatus == "true") {
                bootbox.alert("YOUR SYSTEM HAS BEEN LOCKED DUE PAYMENT ADMINISTRATION, PLEASE CONTACT OUR FINANCE MANAGER", function (e) {
                    window.location = 'Default.aspx';
                });
            }

            $.each(result.d.data, function (indexInArray, valueOfElement) {
                $("." + indexInArray).html(valueOfElement);
            });

            $(".EmployeeName").html(result.d.data.Employee.Name);

            var Menu = result.d.data.Menu;
            var RoleMenu = result.d.data.RoleMenu;
            if (Menu) {
                LoadMenu(Menu, RoleMenu);
            }

            var Notification = result.d.data.OrderNotification;
            if (Notification) {
                LoadNotification(Notification);
            }


        }
        else {
            if (result.d.message == 'Invalid token!') {
                Logout();
                //window.location = 'login-soft.aspx?returnUrl=' + window.location.pathname;
            }
        }
    };
    REST_Master.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'RequestData': ["RoleMenu", "Menu", "OrderNotification", "SystemStatus"],
            'IDRole': +$("#HiddenIDRole").val()
        }
    });
}

function Logout() {
    REST.onSuccess = function (result) {
        if (result.d.success) {
            window.location = '/adminwitcommerce/login-soft.aspx?returnUrl=' + window.location.pathname;
        }
    };
    REST.sendRequest({
        'c': 'beauth',
        'm': 'logout',
        'data': {}
    });
}

function LoadMenu(data, RoleMenu) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        if (data[i].IDMenu == 1) {
            item += '<li class="start" data-idmenu="' + data[i].IDMenu + '">';
            item += '<a href="/' + data[i].Link + '">';
            item += '<i class="fa fa-home"></i>';
            item += '<span class="title">' + data[i].Name + '</span>';
            item += '<span class="selected"></span>';
            item += '</a>';
            item += '</li>';
        }
        else {
            item += '<li class="hidden" id="menu_' + data[i].AttributeValue + '" data-idmenu="' + data[i].IDMenu + '">';
            item += '<a href="javascript:;">';
            item += '<i class="fa fa-' + data[i].Icon + '"></i>';
            item += '<span class="title">' + data[i].Name + '</span>';
            item += '<span id="arrow_' + data[i].Name + '" class="arrow"></span>';
            item += '</a>';
            if (data[i].SubMenu.length > 0) {
                item += '<ul class="sub-menu">';
                for (var y = 0; y < data[i].SubMenu.length; y++) {
                    item += '<li class="hidden" id="submenu_' + data[i].SubMenu[y].AttributeValue + '" data-idmenu="' + data[i].SubMenu[y].IDMenu + '">';
                    item += '<a href="/' + data[i].SubMenu[y].Link + '">' + data[i].SubMenu[y].Name + '</a>';
                    item += '</li>';
                }
                item += '</ul>';
            }
            item += '</li>';
        }
    }

    $(".page-sidebar-menu").append(item);

    for (var i = 0; i < RoleMenu.length; i++) {
        if ($("#menu_" + RoleMenu[i].Name.split(" ").join("_").toLowerCase()).data("idmenu") == RoleMenu[i].IDMenu)
        {
            $("#menu_" + RoleMenu[i].Name.split(" ").join("_").toLowerCase()).removeClass("hidden");
            $("#menu_" + RoleMenu[i].Name.split(" ").join("_").toLowerCase()).addClass("show");
        }

        if ($("#submenu_" + RoleMenu[i].Name.split(" ").join("_").toLowerCase()).data("idmenu") == RoleMenu[i].IDMenu) {
            $("#submenu_" + RoleMenu[i].Name.split(" ").join("_").toLowerCase()).removeClass("hidden");
            $("#submenu_" + RoleMenu[i].Name.split(" ").join("_").toLowerCase()).addClass("show");
        }
    }

}

function LoadNotification(data)
{
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<li>';
        item += '<a href="#">';
        item += '<span class="label label-sm label-icon label-success">';
        item += '<i class="fa fa-bell-o"></i>';
        item += '</span>';
        item += 'New Order - ' + data[i].Reference + '';
        if (data[i].DateTime.Days != 0) {
            item += '<span class="time">' + data[i].DateTime.Days + ' Days ago</span><br/>';
        }
        else if (data[i].DateTime.Hours != 0) {
            item += '<span class="time">' + data[i].DateTime.Hours + ' Hours ago</span><br/>';
        }
        else if (data[i].DateTime.Minutes != 0) {
            item += '<span class="time">' + data[i].DateTime.Minutes + ' Min ago</span><br/>';
        }
        item += '<span style="background:' + data[i].Color + '; color:#fff; padding:2px; float:right;">' + data[i].StatusName + '</span>';
        item += '<div class="clearfix"></div>'
        item += '</a>';
        item += '</li>';
    }

    $(".OrderNotification").html(item);

    $(".TotalOrderNotification").text(data.length);


}