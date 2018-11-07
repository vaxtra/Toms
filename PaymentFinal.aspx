<%@ Page Title="Confirm Your Order - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="PaymentFinal.aspx.cs" Inherits="PaymentFinal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="HiddenActivePromo" />
    <input type="hidden" id="HiddenMember" value="" />
    <section class="product-list wow fadeInUp">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 title-box">
                    <h2>CONFIRM ORDER</h2>
                    <h5>REVIEW AND CONFIRM YOUR ORDER</h5>
                </div>
                <div class="col-md-12 margtop padpad">
                    <div class="col-md-3">
                        <h4>Delivery Address</h4>
                        <hr class="shadowline2">
                        <div class="delivery-address">
                        </div>
                    </div>
                    <div class="col-md-3">
                        <h4>Billing Address</h4>
                        <hr class="shadowline2">
                        <div class="billing-address">
                        </div>
                    </div>
                    <div class="col-md-3">
                        <h4>Shipping</h4>
                        <hr class="shadowline2">
                        <h5 style="text-align: center">
                            <span class="ShippingName"></span>&nbsp;<span class="ShippingInfo"></span></h5>
                        <div class="col-md-8 col-md-offset-2 padpad">
                            <img class="imageShipping img-full" src="#" class="img-full">
                        </div>
                    </div>
                    <div class="col-md-3">
                        <h4>Payment Method</h4>
                        <hr class="shadowline2">
                        <h5 style="text-align: center;">
                            <span id="DetailPayment"></span></h5>
                        <div class="col-md-8 col-md-offset-2 padpad">
                            <img id="ImagePayment" src="" class="img-full">
                        </div>
                    </div>
                </div>
                <div class="col-md-12 padpad">
                    <div class="table-responsive">
                        <table id="cart-list" class="table table-bordered table-hover table-condensed table-striped table-summary">
                            <thead>
                                <tr>
                                    <th>Product</th>
                                    <th>Name</th>
                                    <th>Price Unit</th>
                                    <th>Qty</th>
                                    <th>Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="text-center" colspan="6">NO PRODUCT</td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="4" class="pertotalan little-table">Total Products (tax Incl.) :</td>
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
                                    <td colspan="4" class="pertotalan little-table">Shipping :</td>
                                    <td class="little-table align-right">
                                        <span class="format-money TotalShipping">0</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="pertotalan little-table">Total(tax Incl.) :</td>
                                    <td class="little-table align-right">
                                        <span class="Subtotal format-money">0</span>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
<%--                    <div class="promoProduct">
                        <h2 class="centerBro">Selected Free Product</h2>
                        <div class="table-responsive">
                            <table id="cartPromo-list" class="table table-bordered table-hover table-condensed table-striped table-summary">
                                <thead>
                                    <tr>
                                        <th>Product</th>
                                        <th>Name</th>
                                        <th>Qty</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-center" colspan="6">NO PRODUCT</td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="memberinfo hidden">
                                <p class="infoMsg"></p>
                                <div class="choice">
                                    <div class="col-md-6 nopadding optionspoint inc"><h4 class="typeTrans">Increasing my point</h4></div>
                                    <div class="col-md-6 nopadding optionspoint dcr">
                                        <h4 class="typeTrans">Use my point to get discount</h4>
                                        <div class="usingpoint-wrap hidden">
                                            <input type="text" class="inputpoint" /> Point
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <br /><br />
                    <div class="row">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                            <a class="btn btn-block btn-black placeorderdet" href="#">CONFIRM ORDER</a>
                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/OrderConfirm.js"></script>
</asp:Content>

