<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="adminwitcommerce_shipping_region_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/data-tables/DT_bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Region
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Shipping</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Region</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-6">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-person"></i>Country</div>
                </div>
                <div class="portlet-body">
                    <div id="bootstrap_alerts"></div>
                    <div id="alert" class="alert alert-danger display-none">
                        <button class="close" data-dismiss="alert"></button>
                        You have some form errors. Please check below.
                    </div>
                    <table class="table table-striped table-bordered table-hover" id="dtCountry">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-person"></i>Province</div>
                </div>
                <div class="portlet-body">
                    <div id="Div1"></div>
                    <div id="Div2" class="alert alert-danger display-none">
                        <button class="close" data-dismiss="alert"></button>
                        You have some form errors. Please check below.
                    </div>
                    <table class="table table-striped table-bordered table-hover" id="dtProvince">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-person"></i>City</div>
                </div>
                <div class="portlet-body">
                    <div id="Div3"></div>
                    <div id="Div4" class="alert alert-danger display-none">
                        <button class="close" data-dismiss="alert"></button>
                        You have some form errors. Please check below.
                    </div>
                    <table class="table table-striped table-bordered table-hover" id="dtCity">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-person"></i>District</div>
                </div>
                <div class="portlet-body">
                    <div id="Div5"></div>
                    <div id="Div6" class="alert alert-danger display-none">
                        <button class="close" data-dismiss="alert"></button>
                        You have some form errors. Please check below.
                    </div>
                    <table class="table table-striped table-bordered table-hover" id="dtDistrict">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Actions</th>
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
    <script type="text/javascript" src="/assets/admin/pages/scripts/shipping/region/default.js"></script>
</asp:Content>

