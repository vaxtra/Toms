<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="admin-order-account.aspx.cs" Inherits="adminwitcommerce_Config_admin_order_account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Shop Information
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Configuration</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Admin Order Account</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-cogs"></i>Admin Order Account</div>
                </div>
                <div class="portlet-body form">
                    <form class="form-horizontal" id="OrderForm">
                        <div class="form-body">
                            <div id="bootstrap_alert"></div>
                            <div class="alert alert-danger display-none">
                                <button class="close" data-dismiss="alert"></button>
                                You have some form errors. Please check below.
                           
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">First Name<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" name="FirstName" class="form-control input" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Last Name<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" name="LastName" class="form-control input" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Jenis Kelamin</label>
                                <div class="col-md-4">
                                    <div class="radio-list">
                                        <label class="radio-inline">
                                            <input type="radio" name="Gender" value="L" checked="checked" />Male
                                           
                                        </label>
                                        <label class="radio-inline">
                                            <input type="radio" name="Gender" value="P" />Female</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Email</label>
                                <div class="col-md-4">
                                    <input type="email" class="form-control input" placeholder="Email Address" name="Email" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Telepon</label>
                                <div class="col-md-4">
                                    <input type="text" class="form-control input" placeholder="" name="PhoneNumber" />
                                </div>
                            </div>
                        </div>
                        <div class="form-actions right">
                            <a href="Default.aspx" class="btn default">Cancel</a>
                            <button type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/config/admin-order-account.js?v=1.1"></script>
</asp:Content>

