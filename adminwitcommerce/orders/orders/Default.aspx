<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="adminwitcommerce_orders_orders_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/data-tables/DT_bootstrap.css" />
    <link type="text/css" href="/assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Orders
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
    <li><a href="Default.aspx">Orders</a></li>
    <li class="pull-right">
        <%--<div id="dashboard-report-range" class="dashboard-date-range tooltips blue" data-placement="top" data-original-title="Filter By Date range">
            <i class="fa fa-calendar"></i>
            <span></span>
            <i class="fa fa-angle-down"></i>
        </div>--%>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="portlet box blue-hoki">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>Filter Order
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <%--<a href="#portlet-config" data-toggle="modal" class="config"></a>--%>
                    </div>
                    <%--<div class="actions">
                        <div class="btn-group">
                            <a class="btn btn-sm btn-default dropdown-toggle" href="#" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">Filter By <i class="fa fa-angle-down"></i>
                            </a>
                            <div class="dropdown-menu hold-on-click dropdown-checkboxes pull-right">
                                <label>
                                    <input type="checkbox" />
                                    Finance</label>
                                <label>
                                    <input type="checkbox" checked="" />
                                    Membership</label>
                                <label>
                                    <input type="checkbox" />
                                    Customer Support</label>
                                <label>
                                    <input type="checkbox" checked="" />
                                    HR</label>
                                <label>
                                    <input type="checkbox" />
                                    System</label>
                            </div>
                        </div>
                        <a href="#" class="btn btn-default btn-sm">
                            <i class="fa fa-pencil"></i>Edit </a>
                    </div>--%>
                </div>
                <div class="portlet-body form">
                    <form class="form-horizontal">
                        <div class="form-body">
                            <div class="form-group">
                                <label class="col-md-3 control-label">Date Range</label>
                                <div class="col-md-4">
                                    <div id="dashboard-report-range" class="dashboard-date-range tooltips blue" data-placement="top" data-original-title="Filter By Date range">
                                        <i class="fa fa-calendar"></i>
                                        <span></span>
                                        <i class="fa fa-angle-down"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Order Status</label>
                                <div class="col-md-4">
                                    <div class="checkbox-list orderstatus-list">
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <button type="submit" class="btn btn-circle blue btn-submit-filter">Submit</button>
                                    <button type="button" class="btn btn-circle default">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-book"></i>Products</div>
                    <div class="actions">
                        <a id="btnPrintShippingLabel" href="#" class="btn btn-success btn-sm">PRINT SELECTED SHIPPING LABEL</a>
                        <a id="btnPrintInvoice" href="#" class="btn btn-info btn-sm">PRINT SELECTED INVOICE</a>
                        <a class="btn btn-reset btn-default btn-sm"><i class="fa fa-refresh"></i>&nbsp;Reset Filter </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="bootstrap_alerts"></div>
                    <table class="table table-striped table-bordered table-hover" id="dtProduct">
                        <thead>
                            <tr>
								<th class="table-checkbox">
									<input type="checkbox" class="group-checkable" data-set="#dtProduct .checkboxes" />
								</th>
                                <th>Reference</th>
                                <th>Customer</th>
                                <th>Total</th>
                                <th>Payment</th>
                                <th>Status</th>
                                <th>Date</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/data-tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/DT_bootstrap.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.js"></script>
    <script type="text/javascript" src="/assets/global/scripts/datatable.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/table-managed.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/orders/orders/default.js?v=1.1.2"></script>
</asp:Content>
