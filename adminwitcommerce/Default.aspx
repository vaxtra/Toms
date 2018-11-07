<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="adminwitcommerce_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Dashboard
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="dashboard-stat blue-madison">
                <div class="visual">
                    <i class="fa fa-briefcase fa-icon-medium"></i>
                </div>
                <div class="details">
                    <div class="number TotalSales money-format">
                        -
                    </div>
                    <div class="desc">
                        Lifetime Sales
                    </div>
                </div>
                <a class="more" href="#">View more <i class="m-icon-swapright m-icon-white"></i>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="dashboard-stat red-intense">
                <div class="visual">
                    <i class="fa fa-shopping-cart"></i>
                </div>
                <div class="details">
                    <div class="TotalOrder number">
                        -
                    </div>
                    <div class="desc">
                        Total Orders
                    </div>
                </div>
                <a class="more" href="orders/orders/Default.aspx">View more <i class="m-icon-swapright m-icon-white"></i>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="dashboard-stat green-haze">
                <div class="visual">
                    <i class="fa fa-group fa-icon-medium"></i>
                </div>
                <div class="details">
                    <div class="number TotalCustomer">
                        -
                    </div>
                    <div class="desc">
                        Total Customers
                    </div>
                </div>
                <a class="more" href="customer/customer/Default.aspx">View more <i class="m-icon-swapright m-icon-white"></i>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="dashboard-stat green-haze">
                <div class="visual">
                    <i class="fa fa-group fa-icon-medium"></i>
                </div>
                <div class="details">
                    <div class="number AverageOrder money-format-average">
                        -
                    </div>
                    <div class="desc">
                        Average Orders
                    </div>
                </div>
                <a class="more" href="orders/orders/Default.aspx">View more <i class="m-icon-swapright m-icon-white"></i>
                </a>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <!-- Begin: life time stats -->
            <div class="portlet box blue-steel">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-thumb-tack"></i>Overview
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="javascript:;" class="reload"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <ul class="nav nav-tabs">
                        <li class="active">
                            <a href="#overview_1" data-toggle="tab">Recent Orders </a>
                        </li>
                        <li>
                            <a href="#overview_2" data-toggle="tab">Best Sellers </a>
                        </li>
                        <li>
                            <a href="#overview_3" data-toggle="tab">New Customers </a>
                        </li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Orders <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu" role="menu">
                                <li>
                                    <a href="#overview_4" tabindex="-1" data-toggle="tab">Latest 10 Orders </a>
                                </li>
                                <li>
                                    <a href="#overview_4" tabindex="-1" data-toggle="tab">Pending Orders </a>
                                </li>
                                <li>
                                    <a href="#overview_4" tabindex="-1" data-toggle="tab">Completed Orders </a>
                                </li>
                                <li>
                                    <a href="#overview_4" tabindex="-1" data-toggle="tab">Rejected Orders </a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="overview_1">
                            <div class="table-responsive">
                                <table class="table table-striped table-hover table-bordered" id="tableRecentOrder">
                                    <thead>
                                        <tr>
                                            <th>Customer Name
                                            </th>
                                            <th>Qty
                                            </th>
                                            <th>Amount
                                            </th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="text-center" colspan="4">NO DATA
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane" id="overview_2">
                            <div class="table-responsive">
                                <table class="table table-striped table-hover table-bordered" id="tableBestSeller">
                                    <thead>
                                        <tr>
                                            <th>Product Name
                                            </th>
                                            <th>Qty
                                            </th>
                                            <th>Amount
                                            </th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="text-center" colspan="4">NO DATA
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane" id="overview_3">
                            <div class="table-responsive">
                                <table class="table table-striped table-hover table-bordered" id="tableNewCustomer">
                                    <thead>
                                        <tr>
                                            <th>Customer Name
                                            </th>
                                            <th>Email
                                            </th>
                                            <th>Phone
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="text-center" colspan="3">NO DATA
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane" id="overview_4">
                            <div class="table-responsive">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Customer Name
                                            </th>
                                            <th>Date
                                            </th>
                                            <th>Amount
                                            </th>
                                            <th>Status
                                            </th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <a href="#">David Wilson </a>
                                            </td>
                                            <td>3 Jan, 2013
                                            </td>
                                            <td>$625.50
                                            </td>
                                            <td>
                                                <span class="label label-sm label-warning">Pending </span>
                                            </td>
                                            <td>
                                                <a href="#" class="btn default btn-xs green-stripe">View </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <a href="#">Amanda Nilson </a>
                                            </td>
                                            <td>13 Feb, 2013
                                            </td>
                                            <td>$12625.50
                                            </td>
                                            <td>
                                                <span class="label label-sm label-warning">Pending </span>
                                            </td>
                                            <td>
                                                <a href="#" class="btn default btn-xs green-stripe">View </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <a href="#">Jhon Doe </a>
                                            </td>
                                            <td>20 Mar, 2013
                                            </td>
                                            <td>$125.00
                                            </td>
                                            <td>
                                                <span class="label label-sm label-success">Success </span>
                                            </td>
                                            <td>
                                                <a href="#" class="btn default btn-xs green-stripe">View </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <a href="#">Bill Chang </a>
                                            </td>
                                            <td>29 May, 2013
                                            </td>
                                            <td>$12,125.70
                                            </td>
                                            <td>
                                                <span class="label label-sm label-info">In Process </span>
                                            </td>
                                            <td>
                                                <a href="#" class="btn default btn-xs green-stripe">View </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <a href="#">Paul Strong </a>
                                            </td>
                                            <td>1 Jun, 2013
                                            </td>
                                            <td>$890.85
                                            </td>
                                            <td>
                                                <span class="label label-sm label-success">Success </span>
                                            </td>
                                            <td>
                                                <a href="#" class="btn default btn-xs green-stripe">View </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <a href="#">Jane Hilson </a>
                                            </td>
                                            <td>5 Aug, 2013
                                            </td>
                                            <td>$239.85
                                            </td>
                                            <td>
                                                <span class="label label-sm label-danger">Canceled </span>
                                            </td>
                                            <td>
                                                <a href="#" class="btn default btn-xs green-stripe">View </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <a href="#">Patrick Walker </a>
                                            </td>
                                            <td>6 Aug, 2013
                                            </td>
                                            <td>$1239.85
                                            </td>
                                            <td>
                                                <span class="label label-sm label-success">Success </span>
                                            </td>
                                            <td>
                                                <a href="#" class="btn default btn-xs green-stripe">View </a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End: life time stats -->
        </div>
        <div class="col-md-6">
            <!-- Begin: life time stats -->
            <div class="portlet box red-sunglo tabbable">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-bar-chart-o"></i>Revenue
                    </div>
                    <div class="tools">
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="javascript:;" class="reload"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="portlet-tabs">
                        <ul class="nav nav-tabs" style="margin-right: 50px">
                                <%--<li>
                                    <a href="#portlet_tab2" data-toggle="tab" id="statistics_amounts_tab">Amounts </a>
                                </li>--%>
                            <li class="active">
                                <a href="#portlet_tab1" data-toggle="tab">Orders </a>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="portlet_tab1">
                                <div id="statistics_1" class="chart">
                                </div>
                            </div>
                            <div class="tab-pane" id="portlet_tab2">
                                <div id="statistics_2" class="chart">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="well no-margin no-border">
                        <%--<div class="row">
                            <div class="col-md-3 col-sm-3 col-xs-6 text-stat">
                                <span class="label label-success">Revenue: </span>
                                <h3>$1,234,112.20</h3>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-6 text-stat">
                                <span class="label label-info">Tax: </span>
                                <h3>$134,90.10</h3>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-6 text-stat">
                                <span class="label label-danger">Shipment: </span>
                                <h3>$1,134,90.10</h3>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-6 text-stat">
                                <span class="label label-warning">Orders: </span>
                                <h3>235090</h3>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
            <!-- End: life time stats -->
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script src="/assets/global/plugins/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/assets/global/plugins/flot/jquery.flot.categories.js" type="text/javascript"></script>
    <script src="/assets/admin/pages/scripts/ecommerce-index.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/default.js"></script>
    <script>
        jQuery(document).ready(function () {
            //EcommerceIndex.init();
        });
    </script>
</asp:Content>

