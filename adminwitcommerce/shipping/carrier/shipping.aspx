<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="shipping.aspx.cs" Inherits="adminwitcommerce_shipping_carrier_shipping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/data-tables/DT_bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Shipping Management
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Carriers</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Shipping Management</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-book"></i>Shipping Price</div>
                </div>
                <div class="portlet-body">
                    <div id="bootstrap_alert"></div>
                    <table class="table table-striped table-bordered table-hover" id="dtShipping">
                        <thead>
                            <tr>
                                <th>City</th>
                                <th>District</th>
                                <th>Price (per KG)</th>
                                <th>Update</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/data-tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/DT_bootstrap.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/shipping/carrier/shipping.js"></script>
</asp:Content>

