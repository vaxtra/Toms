<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="adminwitcommerce_report_shipping_distribution_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/data-tables/DT_bootstrap.css" />
    <link type="text/css" href="/assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Shipping Distribution
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
        <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="#">Report</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="/adminwitcommerce/report/shipping-distribution/Default.aspx">Shipping Distribution</a>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
        <div class="row">
        <div class="col-lg-12">
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
                    </div>
                </form>
            </div>
        </div>
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-book"></i>Shipping Distribution</div>
                    <div class="actions">
                        <a class="btn btn-reset btn-default btn-sm"><i class="fa fa-refresh"></i>&nbsp;Reset Filter </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="bootstrap_alerts"></div>
                    <table class="table table-striped table-bordered table-hover" id="dtShippingDis">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Shipping Method</th>
                                <th>Total Orders</th>
                                <th>Total Sales</th>
                                <th>Total Shipping Cost</th>
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
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/DT_bootstrap.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/report/shipping-distribution/shipping-distribution.js"></script>
</asp:Content>

