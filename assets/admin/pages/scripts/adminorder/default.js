var REST = new OF({
    'url': '/api/services.asmx/request'
});

var basket = [];
var deliveryFee = 0;
var totalWeight = 0;
var totalDeliveryFee = 0;
var subtotal = 0;
var totalPaidOrder = 0;
var countryData = [];
var provinceData = [];
var idShipping = 0;
var total = 0;


$(function () {
    Preload();
    LoadPayment();

    $("input[name=TypeCustomer]").change(function () {
        if ($(this).val() == "4") {
            LoadDataAdmin();
        }
        else {
            $("input[name=FirstName]").val("");
            $("input[name=LastName]").val("");
            $("input[name=Email]").val("");
            $("input[name=Phone]").val("");

            $("input[name=FirstName]").removeAttr("disabled");
            $("input[name=LastName]").removeAttr("disabled");
            $("input[name=Gender]").removeAttr("disabled");
            $("input[name=Email]").removeAttr("disabled");
            $("input[name=Phone]").removeAttr("disabled");
        }
    });

    $('#sameAsCustomer').on('change', function () {
        if ($(this).prop('checked')) {
            $('input[name=DeliveryName]').val($('input[name=FirstName]').val() + ' ' + $('input[name=LastName]').val());
            $('input[name=DeliveryPhone]').val($('input[name=Phone]').val());
        }
        else {
            $('input[name=DeliveryName]').val('');
            $('input[name=DeliveryPhone]').val('');
        }
    });

    $('#sameAsDelivery').on('change', function () {
        if ($(this).prop('checked')) {
            $('input[name=BillingName]').val($('input[name=DeliveryName]').val());
            $('input[name=BillingPhone]').val($('input[name=DeliveryPhone]').val());
            $('textarea[name=BillingAddress]').val($('textarea[name=DeliveryAddress]').val());
            $('input[name=BillingPostalCode]').val($('input[name=DeliveryPostalCode]').val());

            $('#BillingProvince').select2('data', $('#DeliveryProvince').select2('data')).select2("readonly", true);
            $('#BillingCity').select2('data', $('#DeliveryCity').select2('data')).select2("readonly", true);
            $('#BillingDistrict').select2('data', $('#DeliveryDistrict').select2('data')).select2("readonly", true);
        }
        else {
            var Country = [];
            for (var i = 0; i < countryData; i++) {
                Country.push({
                    id: countryData[i].IDCountry,
                    text: countryData[i].Name
                });

                $("#BillingCountry").select2({
                    data: Country,
                    allowClear: false
                }).select2("data", Country[0]).select2("readonly", false);
            }

            var Province = [];
            for (var i = 0; i < provinceData.length; i++) {
                Province.push({
                    id: provinceData[i].IDProvince,
                    text: provinceData[i].Name
                });
            }

            $("#BillingProvince").select2({
                data: Province,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: Province[0].id, text: Province[0].text };
                    callback(data);
                }
            });

            $("#BillingProvince").on("change", function () {
                LoadBillingCity(+$(this).select2("data").id);
            });

            LoadBillingCity(Province[0].id);
        }
    });

    InitDT("#dtProduct");

    $('#btnProductNext').click(function (e) {
        e.preventDefault();
        $('.tab-pane').removeClass("active");
        $("#tabCustomer").addClass("active");
    });
    //$('#btnProductBack').click(function (e) {
    //    e.preventDefault();
    //    $('.tab-pane').removeClass("active");
    //    $("tabcustomer").addClass("active");
    //});
    $('#btnCustomerNext').click(function (e) {
        e.preventDefault();
        $('.tab-pane').removeClass("active");
        $("#tabSummary").addClass("active");
        $("a[href=#tabCustomer]").parent().removeClass("active");
        $("a[href=#tabSummary]").parent().addClass("active");
    });
    $('#btnCustomerBack').click(function (e) {
        e.preventDefault();
        $('.tab-pane').removeClass("active");
        $("#tabProduct").addClass("active");
        $("a[href=#tabCustomer]").parent().removeClass("active");
        $("a[href=#tabProduct]").parent().addClass("active");
    });
    $('#btnSummaryNext').click(function (e) {
        e.preventDefault();
        SubmitOrder();
    });
    $('#btnSummaryBack').click(function (e) {
        e.preventDefault();
        $('.tab-pane').removeClass("active");
        $("#tabCustomer").addClass("active");
        $("a[href=#tabSummary]").parent().removeClass("active");
        $("a[href=#tabCustomer]").parent().addClass("active");
    });
});

function SubmitOrder() {

    var customer = {
        FirstName: $("input[name=FirstName]").val(),
        LastName: $("input[name=LastName]").val(),
        Gender: $("input[name=Gender]").val(),
        Email: $("input[name=Email]").val(),
        Phone: $("input[name=Phone]").val(),
        IDCustomerGroup: parseInt($("input[name=TypeCustomer]:checked").val())
    };

    var shippingAddress = {
        Name: $("input[name=DeliveryName]").val(),
        Phone: $("input[name=DeliveryPhone]").val(),
        Address: $("textarea[name=DeliveryAddress]").val(),
        PostalCode: $("input[name=DeliveryPostalCode]").val(),
        IDCountry: 1,
        IDProvince: $("#DeliveryProvince").select2('data').id,
        IDCity: $("#DeliveryCity").select2('data').id,
        IDDistrict: $("#DeliveryDistrict").select2('data').id
    };

    var billingAddress = {
        Name: $("input[name=BillingName]").val(),
        Phone: $("input[name=BillingPhone]").val(),
        Address: $("textarea[name=BillingAddress]").val(),
        PostalCode: $("input[name=BillingPostalCode]").val(),
        IDCountry: 1,
        IDProvince: $("#BillingProvince").select2('data').id,
        IDCity: $("#BillingCity").select2('data').id,
        IDDistrict: $("#BillingDistrict").select2('data').id
    };

    var CartData = {};
    CartData["Products"] = basket;
    CartData["Customer"] = customer;
    CartData["DeliveryAddress"] = shippingAddress;
    CartData["BillingAddress"] = billingAddress;
    CartData["IDPaymentMethod"] = $("#PaymentMethod").select2('data').id;
    CartData["PaymentMethod"] = $("#PaymentMethod").select2('data').text;
    CartData["IDShipping"] = idShipping;
    CartData["TotalWeight"] = totalWeight;
    CartData["TotalShipping"] = totalDeliveryFee;
    CartData["Subtotal"] = subtotal;
    CartData["TotalPaid"] = totalPaidOrder;



    REST.onSuccess = function (result) {
        if (!result.d.success) {
            bootbox.alert(result.d.message);
        }
        else {
            Metronic.alert({
                container: "#bootstrap_alert",
                place: "append",
                type: "success",
                message: result.d.message + ", Please check on order page",
                close: true,
                reset: true,
                focus: true,
                //closeInSeconds: 5,
                icon: "check"
            });
        }
    };
    REST.sendRequest({
        'c': 'beord',
        'm': 'admord',
        'data': CartData
    });
}

function UpdateCartTable() {
    item = "";
    if (basket.length > 0) {
        totalWeight = 0;
        subtotal = 0;
        for (var i = 0; i < basket.length; i++) {
            
            total = Number(basket[i].qty) * Number(basket[i].price.replace(",", ""));
            subtotal += total;
            totalWeight += parseFloat(basket[i].weight * Number(basket[i].qty));
            item += '<tr>';
            item += '<td><img width="100px" src="' + basket[i].photo + '" /></td>';
            item += '<td>' + basket[i].name + '</td>';
            item += '<td>' + basket[i].qty + '</td>';
            item += '<td>' + basket[i].price + '</td>';
            item += '<td class="format-money">' + total + '</td>';
            item += '<td><a href="#" data-id="' + basket[i].idcom + '" class="btn btn-sm red tooltips deleteCart" data-original-title="delete"><i class="glyphicon glyphicon-trash"></i></a></td>';
            item += '</tr > ';
        }
        $('.cartTable tbody').html(item);

        $('.deleteCart').click(function (e) {
            e.preventDefault();
            DeleteAdminCart($(this).data('id'));
            UpdateCartTable();
        });

        var totalShipping = Math.ceil(totalWeight) * deliveryFee;
        totalDeliveryFee = totalShipping;
        totalDeliveryFee = deliveryFee;
        var totalPaid = subtotal + totalDeliveryFee;
        totalPaidOrder = totalPaid;

        $(".totalShipping").html(totalDeliveryFee);
        $(".totalPaid").html(totalPaid);

        $(".format-money").formatCurrency({
            region: "id-ID"
        })
        
    }
    else {
        var item = '<tr><td colspan="6">No Data</td></tr>';
        $('.cartTable tbody').html(item);

        $(".totalShipping").html("0");
        $(".totalPaid").html("0");
    }
}

function AddToCart(element) {
    var name = $(element).data('name');
    var idCom = $(element).data("id");
    var idProd = $(element).data("idprod");
    var price = $(element).data("price");
    var photo = $(element).data("photo");
    var weight = $(element).data("weight");
    var qty = 1;
    var product = { idprod: idProd, idcom: idCom, name: name, price: price, photo: photo, qty: qty, weight: weight };
    var cek = checkAndAdd(idCom);
    if (cek === null)
        basket.push(product);
    else {
        cek.qty += 1;
    }
    UpdateCartTable();

    LoadShipping();
}

function DeleteAdminCart(id) {
    var cek = checkAndAdd(id);
    basket = basket.filter(function (el) {
        return el.idcom !== id;
    });
}

function checkAndAdd(id) {

    var data = $.grep(basket, function (e) { return e.idcom === id; });
    if (data.length === 0)
        return null;
    else
        return data[0];
    //var found = basket.some(function (el) {
    //    if (el.idcom === id)
    //        return el;
    //    else
    //        return null;
    //});
    //return found;
    //if (!found) { basket.push({ id: id, username: name }); }
}

function LoadPayment() {
    REST.onSuccess = function (result) {
        var data = result.d.data.Payment;
        if (result.d.success) {
            var City = [];
            for (var i = 0; i < data.length; i++) {
                City.push({
                    id: data[i].IDPaymentMethod,
                    text: data[i].Name + " [" + data[i].AccountNumber + "]"
                });
            }

            $("#PaymentMethod").select2({
                data: City,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: City[0].id, text: City[0].text };
                    callback(data);
                }
            });
        }
    };
    REST.sendRequest({
        'c': 'femaster',
        'm': 'preload',
        'data': {
            'RequestData': ['Payment']
        }
    });
}

function GetIDShipping(iddistrict, name, service, desc, price, etd) {
    $.ajax({
        url: "/modules/adminorder/Handler.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "adminorder",
            m: "idshipping",
            data: {
                iddistrict: iddistrict,
                name: name,
                service: service,
                description: desc,
                price: price,
                etd: etd
            }
        }),
        beforeSend: function () {
            //Metronic.blockUI({
            //    boxed: true
            //});
        },
        success: function (result) {
            console.log(result);
            idShipping = result.data;
        },
        complete: function () {
            //Metronic.unblockUI();
        },
        error: function (result) {
            bootbox.alert(result.d.message, function () {
                location.reload();
            });
        }
    });
}

function LoadShipping() {
    if (totalWeight != 0) {
        $.ajax({
            url: "/modules/adminorder/Handler.ashx",
            contentType: "application/json; charset=utf-8",
            type: 'POST',
            dataType: "json",
            data: JSON.stringify({
                c: "adminorder",
                m: "shipping",
                data: {
                    'IDDistrict': $("#DeliveryDistrict").select2("data").id,
                    'TotalWeight': totalWeight
                }
            }),
            beforeSend: function () {
                //Metronic.blockUI({
                //    boxed: true
                //});
            },
            success: function (result) {
                var item = '';
                for (var i = 0; i < result.data.length; i++) {
                    item += '<label><input type="radio" name="shipping" data-etd="' + result.data[i].Information + '" data-name="' + result.data[i].Name + '" data-desc="' + result.data[i].Information + '" data-tariff="' + result.data[i].TotalPrice + '" value="' + result.data[i].TotalPrice + '" /> Rp ' + result.data[i].TotalPrice + ' - ' + result.data[i].Name + '</label>';
                }
                $('.shipping-list').html(item);

                console.log($("#DeliveryDistrict").select2("data").id);

                $('input[name=shipping]').on('change', function () {
                    deliveryFee = +$(this).data('tariff');
                    GetIDShipping($("#DeliveryDistrict").select2("data").id, $(this).data("name"), $(this).data("service"), $(this).data("desc"), $(this).data('tariff'), $(this).data('etd'));
                    UpdateCartTable();
                });
            },
            complete: function () {
                //Metronic.unblockUI();
            },
            error: function (result) {
                bootbox.alert(result.d.message, function () {
                    location.reload();
                });
            }
        });
    }
}


function InitDT(element) {
    $(element).each(function () {
        $(element).dataTable().fnDestroy();
    });
    var e = $(element).dataTable({
        oLanguage: {
            sProcessing: '<span>Processing...</span>',
            sLengthMenu: "_MENU_ records",
            oPaginate: {
                sPrevious: "Prev",
                sNext: "Next"
            },
            sAjaxRequestGeneralError: "Could not complete request. Please check your internet connection",
            sEmptyTable: "No records to display",
            sZeroRecords: "No matching records found"
        },
        aLengthMenu: [
            [5, 10, 20, 50, -1],
            [5, 10, 20, 50, "All"]
        ],
        iDisplayLength: 20,
        bSortClasses: false,
        bStateSave: true,
        bPaginate: true,
        bAutoWidth: false,
        bProcessing: true,
        bServerSide: true,
        bDestroy: true,
        bRetrieve: true,
        sPaginationType: "bootstrap",
        sAjaxSource: "/api/services.asmx/dtpro_combi",
        fnInitComplete: function () {
            this.fnSetFilteringDelay(500);
        },
        aoColumns: [
            { mDataProp: "Photo", sWidth: "10%" },
            { mDataProp: "Name", sWidth: "15%" },
            { mDataProp: "ReferenceCode", sWidth: "15%" },
            { mDataProp: "TotalDiscount", sWidth: "10%" },
            { mDataProp: "Price", sWidth: "10%" },
            { mDataProp: "Quantity", sWidth: "5%" },
            { mDataProp: "IDProduct", sWidth: "10%" }
        ],
        aoColumnDefs: [
            { bSortable: false, aTargets: [0, 6] },
            { sClass: "text-center", aTargets: ['_all'] }
        ],
        fnRowCallback: function (nRow, aaData, dIndex, rIndex) {
            var oSettings = e.fnSettings();
            var iTotalRecords = oSettings.fnRecordsTotal();
            if (aaData.Photo != null)
                $("td:eq(0)", nRow).html('<img class="img-responsive" src="/assets/images/product/' + aaData.Photo + '" />');
            else
                $("td:eq(0)", nRow).html('<img class="img-responsive" src="/assets/images/noimage.jpg" />');

            //$("td:eq(6)", nRow).empty();
            //var s = '<a href="#" class="btn btn-sm btn_changestatus green tooltips" data-id="' + aaData.IDProduct + '" data-original-title="Click to Deactivate"><i class="glyphicon glyphicon-ok" title="Active"></i></a>';
            //if (!aaData.Active) s = '<a href="#" class="btn btn-sm red tooltips btn_changestatus" data-id="' + aaData.IDProduct + '" data-original-title="Click to Activate"><i class="glyphicon glyphicon-remove" title="Deactive"></i></a>';
            //$("td:eq(6)", nRow).append(s);

            //var p = '';
            //if (aaData.SequenceNumber != 1)
            //    p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-up green btn-sm tooltips" data-original-title="Up Position"><i class="glyphicon glyphicon-arrow-up"></i></button> ';
            //if (aaData.SequenceNumber != iTotalRecords)
            //    p += '<button data-id="' + aaData.IDProduct + '" class="btn btn-down red btn-sm tooltips" data-original-title="Down Position"><i class="glyphicon glyphicon-arrow-down"></i></button>';
            //$("td:eq(7)", nRow).html(p);

            var o = "";
            //o += '<a href="#" data-id="' + aaData.IDProduct + '" class="btn_view btn btn-sm green tooltips" data-original-title="View"><i class="glyphicon glyphicon-eye-open"></i></a>';
            //o += '<a href="update.aspx?id=' + aaData.IDProduct + '" data-id="' + aaData.IDProduct + '" class="btn_edit btn btn-sm yellow tooltips" data-original-title="Update"><i class="glyphicon glyphicon-edit"></i></a>';
            o += '<a href="#" data-weight="' + aaData.Weight + '" data-maxqty="' + aaData.Quantity + '" data-name="' + aaData.Name + '" data-price="' + aaData.Price + '" data-photo="/assets/images/product/' + aaData.Photo + '" data-idprod="' + aaData.IDProduct + '" data-id="' + aaData.IDProduct_Combination + '" data-nama="' + aaData.Name + '" class="btn btn-sm blue tooltips addToCart" data-original-title="add to cart"><i class="glyphicon glyphicon-plus"></i></a>';
            $("td:eq(6)", nRow).empty().append(o);
        },
        fnDrawCallback: function (e) {
            //$(".btn_view").on("click", function (e) {
            //    e.preventDefault();
            //    GetDetail(+$(this).data("id"));
            //});

            //$(".btn_changestatus").click(function (e) {
            //    e.preventDefault();
            //    StatusToggle(+$(this).data("id"));
            //});

            //$(".btn_delete").on("click", function (e) {
            //    e.preventDefault();
            //    var button = $(this);
            //    bootbox.confirm("Are you sure to <b>delete</b>?", function (result) {
            //        if (result) {
            //            Delete(+button.data("id"));
            //        }
            //    });
            //});

            //$(".btn-up").click(function (e) {
            //    e.preventDefault();
            //    UpProductPosition(+$(this).data("id"));
            //});

            //$(".btn-down").click(function (e) {
            //    e.preventDefault();
            //    DownProductPosition(+$(this).data("id"));
            //});

            $(".addToCart").click(function (e) {
                e.preventDefault();
                AddToCart($(this));
            });

            $("a,button", this.fnGetNodes()).tooltip({
                delay: 0,
                track: true,
                fade: 250
            });
        }
    });
    jQuery(element + "_wrapper .dataTables_filter input").addClass("form-control input-medium input-inline");
    jQuery(element + "_wrapper .dataTables_length select").addClass("form-control input-xsmall");
    jQuery(element + "_wrapper .dataTables_length select").select2();
}

function Preload() {
    REST.onSuccess = function (result) {
        var data = result.d.data;
        if (result.d.success) {
            var Country = [];
            countryData = data.Country;
            for (var i = 0; i < data.Country.length; i++) {
                Country.push({
                    id: data.Country[i].IDCountry,
                    text: data.Country[i].Name
                });

                $("#DeliveryCountry").select2({
                    data: Country,
                    allowClear: false
                }).select2("data", Country[0]).select2("readonly", true);

                $("#BillingCountry").select2({
                    data: Country,
                    allowClear: false
                }).select2("data", Country[0]).select2("readonly", true);
            }

            var Province = [];
            provinceData = data.Province;
            for (var i = 0; i < data.Province.length; i++) {
                Province.push({
                    id: data.Province[i].IDProvince,
                    text: data.Province[i].Name
                });
            }

            $("#DeliveryProvince").select2({
                data: Province,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: Province[0].id, text: Province[0].text };
                    callback(data);
                }
            });

            $("#BillingProvince").select2({
                data: Province,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: Province[0].id, text: Province[0].text };
                    callback(data);
                }
            });

            $("#DeliveryProvince").on("change", function () {
                LoadDeliveryCity(+$(this).select2("data").id);
            });

            $("#BillingProvince").on("change", function () {
                LoadBillingCity(+$(this).select2("data").id);
            });

            LoadDeliveryCity(Province[0].id);
            LoadBillingCity(Province[0].id);
        }
    };
    REST.sendRequest({
        'c': 'fereg',
        'm': 'regpreload',
        'data': {}
    });
}

function LoadDeliveryCity(idProvince) {
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

            $("#DeliveryCity").select2({
                data: City,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: City[0].id, text: City[0].text };
                    callback(data);
                }
            });

            LoadDeliveryDistrict(City[0].id);
            $("#DeliveryCity").on("change", function () {
                console.log('goblok:' + $("#DeliveryCity").select2("data").id);
                LoadDeliveryDistrict(+$(this).select2("data").id);
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

function LoadDeliveryDistrict(idCity) {
    $.ajax({
        url: "/modules/adminorder/Handler.ashx",
        contentType: "application/json; charset=utf-8",
        type: 'POST',
        dataType: "json",
        data: JSON.stringify({
            c: "adminorder",
            m: "district",
            data: {
                IDCity: idCity
            }
        }),
        beforeSend: function () {
            //Metronic.blockUI({
            //    boxed: true
            //});
        },
        success: function (result) {
            var data = result.data;
            if (result.success) {
                var District = [];
                for (var i = 0; i < data.length; i++) {
                    District.push({
                        id: data[i].IDDistrict,
                        text: data[i].Name,
                        code: data[i].Destination_Code
                    });
                }

                $("#DeliveryDistrict").select2({
                    data: District,
                    allowClear: false,
                    width: "100%",
                    initSelection: function (element, callback) {
                        var data = { id: District[0].id, text: District[0].text };
                        $("#DestinationCode").val(District[0].code);
                        callback(data);
                    }
                });

                $("#DeliveryDistrict").unbind();

                $("#DeliveryDistrict").trigger('change');

                $("#DeliveryDistrict").on('change', function () {
                    LoadShipping();
                });
            }
        },
        complete: function () {
            //Metronic.unblockUI();
        },
        error: function (result) {
            bootbox.alert(result.message, function () {
                location.reload();
            });
        }
    });

}

function LoadBillingCity(idProvince) {
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

            $("#BillingCity").select2({
                data: City,
                allowClear: false,
                width: "100%",
                initSelection: function (element, callback) {
                    var data = { id: City[0].id, text: City[0].text };
                    callback(data);
                }
            });

            LoadBillingDistrict(City[0].id);
            $("#BillingCity").on("change", function () {
                LoadBillingDistrict(+$(this).select2("data").id);
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

function LoadBillingDistrict(idCity) {
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

            $("#BillingDistrict").select2({
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
    var e = $("#OrderForm");
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
            Email: {
                required: true,
                email: true
            },
            Phone: {
                required: true
            },
            DeliveryName: {
                required: true
            },
            DeliveryPhone: {
                required: true
            },
            DeliveryAddress: {
                required: true
            },
            PostalCode: {
                required: true
            },
            DeliveryAddress: {
                required: true
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
            console.log(sendData);

            if ($("input[name=TypeCustomer]").val() == "admin") {
                REST.onSuccess = function (result) {
                    var data = result.d.data;
                    t.show();
                    if (result.d.success) {
                        Metronic.alert({
                            container: "#bootstrap_alert",
                            place: "append",
                            type: "success",
                            message: result.d.message,
                            close: true,
                            reset: true,
                            focus: true,
                            //closeInSeconds: 5,
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
                            //closeInSeconds: 5,
                            icon: "warning"
                        });
                    }
                };
                REST.sendRequest({
                    'c': 'fereg',
                    'm': 'regadmord',
                    'data': sendData
                });
            }
            else {
                REST.onSuccess = function (result) {
                    var data = result.d.data;
                    t.show();
                    if (result.d.success) {
                        Metronic.alert({
                            container: "#bootstrap_alert",
                            place: "append",
                            type: "success",
                            message: result.d.message,
                            close: true,
                            reset: true,
                            focus: true,
                            //closeInSeconds: 5,
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
                            //closeInSeconds: 5,
                            icon: "warning"
                        });
                    }
                };
                REST.sendRequest({
                    'c': 'fereg',
                    'm': 'reg',
                    'data': sendData
                });
            }
        }
    });
}

function LoadDataAdmin()
{
    REST.onSuccess = function (result) {
        if (!result.d.success) {
            bootbox.alert(result.d.message);
        }
        else {
            var data = result.d.data.AdminOrderAccount;
            $("input[name=FirstName]").val(data.FirstName);
            $("input[name=LastName]").val(data.LastName);
            $("input[name=Gender][value=" + data.Gender + "]").prop("checked", "checked");
            $("input[name=Email]").val(data.Email);
            $("input[name=Phone]").val(data.PhoneNumber);

            $("input[name=FirstName]").attr("disabled", "disabled");
            $("input[name=LastName]").attr("disabled", "disabled");
            $("input[name=Gender]").attr("disabled", "disabled");
            $("input[name=Email]").attr("disabled", "disabled");
            $("input[name=Phone]").attr("disabled", "disabled");
        }
    };
    REST.sendRequest({
        'c': 'bemaster',
        'm': 'preload',
        'data': {
            'RequestData': ['AdminOrderAccount']
        }
    });
}