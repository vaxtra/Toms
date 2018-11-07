<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="adminwitcommerce_customer_newsletter_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/data-tables/DT_bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Newsletter
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Newsletter</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-person"></i>Newsletter</div>
                </div>
                <div class="portlet-body">
                    <div id="bootstrap_alerts"></div>
                    <div id="alert" class="alert alert-danger display-none">
                        <button class="close" data-dismiss="alert"></button>
                        You have some form errors. Please check below.
                    </div>
                    <div class="tabbable tabbable-custom boxless">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#Non-Member" data-toggle="tab">Non-Member</a></li>
                            <li class=""><a href="#Member" data-toggle="tab">Member</a></li>
                        </ul>
                        <form runat="server">
                        <div class="tab-content">
                            <div class="tab-pane active" id="Non-Member">
                                
                                    <asp:Button ID="btnExportNonMember" runat="server" Text="Export Data" CssClass="btn green" style="margin:20px 0;" OnClick="btnExportNonMember_Click" />
                                
                                <table class="table table-striped table-bordered table-hover" id="dtNewsletter">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Email</th>
                                            <th>Date</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                            <div class="tab-pane" id="Member">
                                    <asp:Button ID="btnNewsletterMember" runat="server" Text="Export Data" CssClass="btn green" style="margin:20px 0;" OnClick="btnNewsletterMember_Click" />
                                <table class="table table-striped table-bordered table-hover" id="dtCustomerSub">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Name</th>
                                            <th>Email</th>
                                            <th>Phone Number</th>
                                            <th>Date Registered</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="viewModal" class="modal fade" aria-hidden="true">
        <form class="form-horizontal" method="post">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title title-value-modal">Manufacturer</h4>
                    </div>
                    <div class="modal-body" id="view_manufacturer">
                        <div class="form-body">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="#" data-dismiss="modal" class="btn btn-block red btn-cancel">Close</a>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/data-tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/DT_bootstrap.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/customer/newsletter/default.js"></script>
</asp:Content>

