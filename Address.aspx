<%@ Page Title="Address - NIION" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Address.aspx.cs" Inherits="Address" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="IDDeliveryAddress" />
    <input type="hidden" id="IDBillingAddress" />
    <div class="product_list_area section_padding checkoutpage">
        <section class="product-list wow fadeInUp">
            <div class="container">
                <div class="col-md-12 address margtotop">
                    <div class="col-md-12 nopadding">
                        <div class="col-md-12 nopadding">
                            <div class="col-md-12 form-group margtop2 peraddressan">
                            </div>
                            <div class="clearfix"></div>
                            <%-- <div class="col-md-4">
                    <div class="form-group margtop3">
                        <label>
                            <select id="ddlAddress2"></select>
                        </label>
                    </div>
                    <hr class="shadowline2" />
                    <h5>Your Billing Address</h5>
                    <div class="address-list billing-address">
                    </div>
                </div>--%>
                            <div class="col-md-6">
                                <h4 class="addressDetail">Address Detail <span class="panahAccor">></span></h4>
                                <div class="addressAccordion bungaddress">
                                    <input id="cbSame" checked="checked" type="checkbox" style="margin-right: 5px; margin-bottom: 16px;" /><label>Use the same address for billing.</label>
                                    <div class="clearfix"></div>
                                    <div class="col-md-6 col-sm-6">
                                        <div class="form-group margtop3">
                                            <label>
                                                <select id="ddlAddress" style="width: 100%;"></select>
                                            </label>
                                        </div>
                                        <hr style="margin-top: 5px;" class="shadowline2" />
                                        <h5>Your Delivery Address</h5>
                                        <div class="address-list delivery-address">
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6">

                                        <div class="form-group margtop3">
                                            <label>
                                                <select id="ddlAddress2" style="width: 100%;"></select>

                                            </label>
                                        </div>
                                        <hr class="shadowline2" />
                                        <h5>Your Billing Address</h5>
                                        <div class="address-list billing-address">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="col-md-12">
                                        <a class="btn btn-block btn-black btn-newAddress" href="NewAddress.aspx?back=Address.aspx">New Address</a>
                                    </div>
                                    <hr class="shadowline2" />

                                    <div class="col-md-12" style="margin-top: 15px;">
                                        <div class="form-group">
                                            <textarea id="Notes" class="form-control" placeholder="Your Notes" cols="20" rows="4"></textarea>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <h4 class="shippingDetail hidden">Shipping <span class="panahAccor">></span></h4>
                                <div class="bungshipping shippingAccordion hidden">
                                    <div class="table-responsive">
                                        <table id="table_shipping" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th class="text-center">Image</th>
                                                    <th>Carrier</th>
                                                    <th>Shipment</th>
                                                    <th>Price</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <h4 class="paymentDetail">Payment <span class="panahAccor">></span></h4>
                                <div class="bungaddress paymentAccordion">
                                    <div class="table-responsive">
                                        <table class="table table-payment table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Bank</th>
                                                    <th>Account Owner</th>
                                                    <th>Account Number</th>
                                                </tr>
                                            </thead>
                                            <tbody class="payment-list">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 mt-30">
                            <h4 class="summaryDetail">Your Shopping Cart <span class="panahAccor">></span></h4>
                            <div class="summaryAccordion bungaddress">

                                <div class="table-responsive">
                                    <table id="cart-list" class="table table-bordered table-hover table-condensed table-striped table-summary">
                                        <thead>
                                            <tr>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="text-center" colspan="6">DATA PEMBAYARAN ANDA</td>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="4" class="pertotalan little-table">Total Produk (tax Incl.) :</td>
                                                <td class="little-table align-right">
                                                    <span class="TotalPrice format-money">0</span>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="4" class="pertotalan little-table">Voucher :</td>
                                                <td class="little-table align-right">
                                                    <span class="format-money Voucher">0</span>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td colspan="4" class="pertotalan little-table">Ongkos Kirim :</td>
                                                <td class="little-table align-right">
                                                    <span class="format-money ShippingPrice">0</span>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td colspan="4" class="pertotalan little-table">Total (tax Incl.) :</td>
                                                <td class="little-table align-right">
                                                    <span class="Subtotal format-money">0</span>
                                                </td>

                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4 mt-30">
                                <a class="btn btn-block btn-black btn-renew hidden" href="#">RENEW</a>
                                <a class="btn btn-block btn-black placeorderdet" href="#">CONFIRM ORDER</a>
                                <a class="btn btn-block btn-black btn-upgrade hidden" href="#">UPGRADE</a>
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/address.js?v=2.1.8"></script>
    <script>
        if ($(window).width() < 768) {

            $(".addressDetail").click(function () {
                $(".addressAccordion").slideToggle();
            });

            $(".summaryDetail").click(function () {
                $(".summaryAccordion").slideToggle();
            });

            $(".paymentDetail").click(function () {
                $(".paymentAccordion").slideToggle();
            });
        }
    </script>
</asp:Content>

