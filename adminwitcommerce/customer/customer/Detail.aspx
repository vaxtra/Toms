<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="adminwitcommerce_customer_customer_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/data-tables/DT_bootstrap.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Detail Customer
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="/adminwitcommerce/Customer/customer/">Customer</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Detail</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="tab-pane active" id="tab_1">
        <input type="hidden" id="HiddenIDCustomer" />
        <div class="summary">
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div id="bootstrap_alerts"></div>
                    <div class="portlet ">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-cogs"></i>Customer Information
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    First Name :
                                </div>
                                <div class="col-md-7 value FirstName">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Last Name
                                </div>
                                <div class="col-md-7 value LastName">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Email
                                </div>
                                <div class="col-md-7 value Email">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Birthday
                                </div>
                                <div class="col-md-7 value Birthday">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Gender
                                </div>
                                <div class="col-md-7 value Gender">
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Phone Number:
                                </div>
                                <div class="col-md-7 value PhoneNumber">
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
                                <i class="fa fa-cogs"></i>Transaction Information
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Date Registered :
                                </div>
                                <div class="col-md-7 value DateRegistered">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Total Valid Orders : 
                                </div>
                                <div class="col-md-7 value">
                                    <span class="TotalValidOrder">-</span>   For ( <span class="TotalValidBuy money">-</span> )
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Total Invalid Orders : 
                                </div>
                                <div class="col-md-7 value">
                                    <span class="TotalInvalidOrder">-</span>   For ( <span class="TotalInvalidBuy money">-</span> )
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Age : 
                                </div>
                                <div class="col-md-7 value Age">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Newsletter
                                </div>
                                <div class="col-md-7 value Subscribe">
                                    -
                                </div>
                            </div>
                            <div class="row static-info">
                                <div class="col-md-5 name">
                                    Status :
                                </div>
                                <div class="col-md-7 value Status">
                                    -
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-sm-6">
                <div class="portlet ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Address Information
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-responsive">
                            <table class="table table-hover table-bordered table-striped address-table">
                                <thead>
                                    <tr>
                                        <th>Name
                                        </th>
                                        <th>People Name
                                        </th>
                                        <th>Address
                                        </th>
                                        <th>Phone Number
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
            <div class="col-md-6 col-sm-6">
                <div class="portlet ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Products
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-responsive">
                            <table class="table table-hover table-bordered table-striped productBought-table">
                                <thead>
                                    <tr>
                                        <th>Date
                                        </th>
                                        <th>Product Name
                                        </th>
                                        <th>Combination
                                        </th>
                                        <th>Quantity
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
            <div class="col-md-12 col-sm-12">
                <div class="portlet ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Order History
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-responsive">
                            <table class="table table-hover table-bordered table-striped order-table">
                                <thead>
                                    <tr>
                                        <th>Reference
                                        </th>
                                        <th>Date
                                        </th>
                                        <th>Shipping
                                        </th>
                                        <th>Payment
                                        </th>
                                        <th>Status
                                        </th>
                                        <th>Products
                                        </th>
                                        <th>Total
                                        </th>
                                        <th>Action
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
    <script type="text/javascript" src="/assets/global/plugins/data-tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/DT_bootstrap.js"></script>
    <script src="/assets/admin/pages/scripts/customer/customer/detail.js"></script>
</asp:Content>

