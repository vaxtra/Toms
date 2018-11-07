<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="CheckShippingCost.aspx.cs" Inherits="CheckShippingCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/assets/global/plugins/select2/select2.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="container">
                <form class="form-horizontal">
                    <div class="form-group">
                        <label class="control-label">Provinsi</label>
                        <select id="province" class="form-control">
                            <option>Pilih provinsi</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="control-label">Kota</label>
                        <select id="city" class="form-control">
                            <option>Pilih kota</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="control-label">kecamatan</label>
                        <select id="district" class="form-control">
                            <option>Pilih kecamatan</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="control-label">Biaya</label>
                        <label class="shipping-price" class="control-label"></label>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/global/plugins/select2/select2.min.js"></script>
    <script>
        $(document).ready(function () {
            $("select").select2();
            CallHandler();
        });

        function CallHandler() {
            $.ajax({
                url: "modules/ShippingCost/ShippingCost.ashx",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    c: "shipping",
                    m: "province",
                    data: {}
                }),
                success: OnComplete,
                error: OnFail
            });
            return false;
        }

        function OnComplete(result) {
            LoadProvince(result.data);
        }
        function OnFail(result) {
            console.log('Request Failed');
        }

        function LoadProvince(data) {
            var item = '<option value="0">Pilih Provinsi</option>';
            for (var i = 0; i < data.length; i++) {
                item += '<option value="' + data[i].IDProvince + '">' + data[i].Name + '</option>';
            }
            $("#province").html(item);
            $("#province").select2();

            $("#province").on("change", function (e) {
                LoadCity($(this).val());
            });
        }

        function LoadCity(id) {
            $.ajax({
                url: "modules/ShippingCost/ShippingCost.ashx",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    c: "shipping",
                    m: "city",
                    data: {
                        ID: id
                    }
                }),
                success: function (result) {
                    var item = '';
                    for (var i = 0; i < result.data.length; i++) {
                        item += '<option value="' + result.data[i].IDCity + '">' + result.data[i].Name + '</option>';
                    }
                    $("#city").html(item);
                    $("#city").select2();

                    $("#city").on("change", function (e) {
                        LoadDistrict($(this).val());
                    })
                },
                error: OnFail
            });
        }

        function LoadDistrict(id) {

            $.ajax({
                url: "modules/ShippingCost/ShippingCost.ashx",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    c: "shipping",
                    m: "district",
                    data: {
                        ID: id
                    }
                }),
                success: function (result) {
                    var item = '<option value="0">Pilih Provinsi</option>';
                    for (var i = 0; i < result.data.length; i++) {
                        item += '<option value="' + result.data[i].IDDistrict + '">' + result.data[i].Name + '</option>';
                    }
                    $("#district").html(item);
                    $("#district").select2();

                    $("#district").click(function (e) {
                        e.preventDefault();
                        LoadCost($(this).val());
                    });
                },
                error: OnFail
            });
        }

        function LoadCost(id) {
            Metronic.blockUI({
                boxed: true
            });
            $.ajax({
                url: "modules/ShippingCost/ShippingCost.ashx",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                dataType: "json",
                data: JSON.stringify({
                    c: "shipping",
                    m: "cost",
                    data: {
                        ID: id
                    }
                }),
                success: function (result) {
                    if (result.data != null) {
                        $(".shipping-price").html(result.data.Price);
                    }
                    Metronic.unblockUI();

                },
                error: OnFail
            });
        }
    </script>
</asp:Content>

