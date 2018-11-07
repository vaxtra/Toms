$(document).ready(function () {
    Toastr.init();
    $("#menu_catalog").addClass("active");
    $("#submenu_products").addClass("active");
    $("#arrow_catalog").addClass("open");

    $(document).ajaxStop(function () {
        //console.clear();
    });
});
