<%@ Page Title="Order Detail - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="OrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="product-list wow fadeInUp">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 title-box">
                    <h2>ORDER DETAIL</h2>
                    <h5>YOUR ORDER DETAIL HISTORY</h5>
                </div>
     <div class="col-md-12 titik">
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
                <span class="ShippingName"></span> - <span class="ShippingInfo"></span></h5>
            <div class="ShippingImage"></div>
        </div>
        <div class="col-md-3">
            <h4>Payment Method</h4>
            <hr class="shadowline2">
            <h5 style="text-align: center;">
                A/N <span class="PaymentOwner"></span> - <span class="PaymentAccountNumber"></span></h5>
            <div class="PaymentImage"></div>
        </div>
    </div>
    <div class="col-md-12 titik">
        <div class="table-responsive">
            <table id="detailOrder" class="table table-bordered table-hover table-condensed table-striped table-summary">
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
                            <span class="TotalPriceProduct format-money">0</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="pertotalan little-table">Voucher :</td>
                        <td class="little-table align-right">
                            <span class="format-money TotalDiscount">0</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="pertotalan little-table">Shipping :</td>
                        <td class="little-table align-right">
                            <span class="TotalShipping format-money">0</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="pertotalan little-table">Total(tax Incl.) :</td>
                        <td class="little-table align-right">
                            <span class="TotalPaid format-money">0</span>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <div class="row">
            <div class="col-md-6">
                <a class="btn btn-block btn-gray btn-upgrade" href="#">UPGRADE</a>
            </div>
            <div class="col-md-6">
                <a class="btn btn-block btn-gray btn-renew" href="#">RENEW</a>
                
            </div>
        </div>
        <br /><br /><br /><br />
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4">
                <a class="btn btn-block btn-black" href="OrderHistory.aspx">CLOSE</a>
            </div>
            <div class="col-md-4">
            </div>
        </div>
    </div>
                </div>
            </div>
        </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" Runat="Server">
    <script src="assets/frontend/scripts/history-detail.js"></script>
</asp:Content>

