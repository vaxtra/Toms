<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="invoice.aspx.cs" Inherits="adminwitcommerce_orders_orders_invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <%--<link href="<%=Page.ResolveClientUrl("~/adminwitcommerce/assets/css/pages/invoice.css")%>" rel="stylesheet" />--%>
    <link href="/assets/admin/pages/css/invoice.css" rel="stylesheet" />
    <style>
        .invoice {
            min-height: 0px !important;
            border-bottom: none !important;
        }

        .invoice-logo img {
            margin-right: 10px;
            margin-left: 10px;
        }

        @media print {
            .footer-print {
                position: fixed;
                bottom: 0;
                left: 0;
                right: 0;
                margin: auto;
                text-align: center;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Invoice <small></small>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="<%=Page.ResolveClientUrl("~/adminwitcommerce/")%>">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="<%=Page.ResolveClientUrl("~/adminwitcommerce/orders/")%>">Orders</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="javascript:;">Invoice</a></li>
    <li class="btn-group">
        <a class="btn blue hidden-print" style="color: #fff;" onclick="javascript:window.print();"><i class="fa fa-print"></i><span>Print</span>
        </a>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <!-- BEGIN PAGE CONTENT-->

    <input type="hidden" id="HiddenIdTransaksi" />

    <div class="list-invoice">

    </div>

<%--    <div class="invoice">
        <div class="row invoice-logo">
            <div class="col-xs-6 invoice-logo-space">
                <h1><b>
                    <asp:Literal ID="lblTitle" runat="server"></asp:Literal></b></h1>
            </div>
            <div class="col-xs-6">
                <p class="InvoiceNumber">
                </p>
                <p class="Date"></p>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-xs-4">
                <h4>Client:</h4>
                <ul class="list-unstyled">
                    <li class="CustomerName">-
                    </li>
                    <li class="CustomerEmail">-
                    </li>
                </ul>
            </div>
            <div class="col-xs-4">
                <h4>Billing Address:</h4>
                <div class="billing-address">
                </div>
            </div>
            <div class="col-xs-4 invoice-payment">
                <h4>Shipping Address:</h4>
                <div class="delivery-address">
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-striped table-hover product-table">
                    <thead>
                        <tr>
                            <th>Product
                            </th>
                            <th>Combination
                            </th>
                            <th>Reference
                            </th>
                            <th>Price
                            </th>
                            <th>Quantity
                            </th>
                            <th>TotalPrice
                            </th>
                            <th>Discount
                            </th>
                            <th>Subtotal
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <div class="well">
                    <address>
                        <strong><asp:Label ID="lblShopName" runat="server" Text=""></asp:Label></strong><br />
                        
                        -<br />

                        <span class="shopCity"></span>
                        <br />
                        <span class="shopAddress"></span><br />
                        [P/F] <span class="shopPhone"></span><br />
                        <span class="shopEmail"></span>
                    </address>
                </div>

            </div>
            <div class="col-xs-8 invoice-block">
                <ul class="list-unstyled amounts">
                    <li>
                        <strong>Sub Total: </strong>
                        <label class="TotalPrice">
                            0 IDR
                        </label>
                    </li>
                    <li>
                        <strong>Voucher: </strong>
                        <label class="TotalDiscount">0 IDR</label>
                    </li>
                    <li>
                        <strong>Shipping: </strong>
                        <label class="TotalShipping">0 IDR</label>
                    </li>
                    <li>
                        <strong>Grand Total: </strong>
                        <label class="TotalPaid">0 IDR</label>
                    </li>
                </ul>

            </div>
        </div>
    </div>--%>
    <!-- END PAGE CONTENT-->
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script src="/assets/admin/pages/scripts/Orders/orders/invoice.js"></script>
    <%--<script>
        var browser = navigator.userAgent.toLowerCase().indexOf('chrome') > -1 ? 'chrome' : 'other';
        if (BrowserDetect.browser.indexOf("mozilla") > -1) {
            document.write('<' + 'link rel="stylesheet" href="<%=Page.ResolveClientUrl("~/adminwitcommerce/assets/css/firefox.css")%>" />');
        } else if (BrowserDetect.browser.indexOf("chrome") > -1) {
            document.write('<' + 'link rel="stylesheet" href="<%=Page.ResolveClientUrl("~/adminwitcommerce/assets/css/chrome.css")%>" />');
        }
    </script>--%>
</asp:Content>



