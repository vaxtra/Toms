<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="adminwitcommerce_orders_orders_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Detail Order
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="/adminwitcommerce/Orders/orders/">Orders</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Detail</a></li>
    <li class="btn-group">
        <a class="btn blue hidden-print btn-invoice" style="color: #fff;"><i class="fa fa-book"></i><span>Invoice</span>
        </a>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="tab-pane active" id="tab_1">
        <input type="hidden" id="HiddenStatus" />
        <input type="hidden" id="HiddenIDOrder" />
        <div class="summary">
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div id="bootstrap_alerts"></div>
                    <div class="portlet ">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-cogs"></i>Order Details
                            </div>
                            <div class="actions">
                                <a href="#" class="btn red default btn-sm" id="btnUbahStatus"><i class="fa fa-pencil"></i>Edit</a>
                                <a href="#" class="btn blue default btn-sm hidden" id="btnSaveStatus"><i class="fa fa-save"></i>Save</a>
                                <a href="#" class="btn default btn-sm hidden" id="btnCancel">Cancel</a>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Reference :
                                </div>
                                <div class="col-md-7 value Reference">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Invoice #:
                                </div>
                                <div class="col-md-7 value InvoiceNumber">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Order Date:
                                </div>
                                <div class="col-md-7 value Date">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Order Last Update:
                                </div>
                                <div class="col-md-7 value DateUpdate">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Order Status:
                                </div>
                                <div class="col-md-7 value OrderStatus">
                                </div>
                            </div>
                            <div class="row static-info ShippingNumber-div hidden">
                                <div class="col-md-5 name">
                                    Shipping Number:
                                </div>
                                <div class="col-md-7 value ShippingNumber">
                                    <input type="text" name="ShippingNumber" id="ShippingNumber" />
                                </div>
                            </div>
                            <div class="row static-info hidden panelShippingNo">
                                <div class="col-md-5 name">
                                    Shipping Number:
                                </div>
                                <div class="col-md-7 value shipping-no">
                                    <input type="text" id="TextboxShippingNo" class="form-control" />
                                    <label id="lblShippingNo" class="hidden ShippingNumber"></label>
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Total Paid:
                                </div>
                                <div class="col-md-7 value TotalPaid">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Payment Information:
                                </div>
                                <div class="col-md-7 value PaymentMethod">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Shipping Information:
                                </div>
                                <div class="col-md-7 value ShippingName">
                                    -
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="portlet ">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-cogs"></i>Customer Information
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Customer Name:
                                </div>
                                <div class="col-md-7 value CustomerName">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Email:
                                </div>
                                <div class="col-md-7 value CustomerEmail">
                                    -
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-sm-12">
                    <div class="portlet ">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-cogs"></i>Billing Address
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row static-info">
                                <div class="col-md-6 value billing-address">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-12">
                    <div class="portlet ">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-cogs"></i>Delivery Address
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row static-info">
                                <div class="col-md-6 value delivery-address">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-12">
                    <div class="portlet ">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-cogs"></i>Confirm Payment Attachment
                            </div>
                            <div class="actions">
                                <button id="btnDetailConfirm" class="btn red"><i class="fa fa-eye"></i>View Detail</button>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row static-info">
                                <div class="col-md-12">
                                    <img src="/assets/images/noimage.jpg" class="img-responsive" id="imgBankwire" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12">
                <div class="portlet ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Shopping Cart
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-responsive">
                            <table class="table table-hover table-bordered table-striped product-table">
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
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
            </div>
            <div class="col-md-6">
                <div class="well">
                    <div class="row static-info align-reverse">
                        <div class="col-md-8 name">
                            Sub Total:
                        </div>
                        <div class="col-md-3 value TotalPriceProduct money">
                            0 IDR
                        </div>
                    </div>
                    <div class="row static-info align-reverse">
                        <div class="col-md-8 name">
                            Voucher:
                        </div>
                        <div class="col-md-3 value TotalDiscount money">
                            0 IDR
                        </div>
                    </div>
                    <div class="row static-info align-reverse">
                        <div class="col-md-8 name">
                            Shipping:
                        </div>
                        <div class="col-md-3 value TotalShipping money">
                            0 IDR
                        </div>
                    </div>
                    <div class="row static-info align-reverse">
                        <div class="col-md-8 name">
                            Grand Total:
                        </div>
                        <div class="col-md-3 value TotalPaid money bold">
                            0 IDR
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="basic" tabindex="-1" role="basic" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Detail Payment Confirmation</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Name:</label>
                                        <div class="col-md-9">
                                            <p class="form-control-static Name">
                                                -
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Email:</label>
                                        <div class="col-md-9">
                                            <p class="form-control-static Email">
                                                -
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Phone:</label>
                                        <div class="col-md-9">
                                            <p class="form-control-static PhoneNumber">
                                                -
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Bank:</label>
                                        <div class="col-md-9">
                                            <p class="form-control-static Bank">
                                                -
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Amount:</label>
                                        <div class="col-md-9">
                                            <p class="form-control-static Amount">
                                                -
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Payment Date:</label>
                                        <div class="col-md-9">
                                            <p class="form-control-static PaymentDate">
                                                -
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Notes:</label>
                                        <div class="col-md-9">
                                            <p class="form-control-static Notes">
                                                -
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            <div class="modal-footer">
                <button type="button" class="btn default" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script src="/assets/admin/pages/scripts/Orders/orders/detail.js"></script>
</asp:Content>

