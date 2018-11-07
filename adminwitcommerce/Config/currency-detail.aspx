<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="currency-detail.aspx.cs" Inherits="adminwitcommerce_Config_currency_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" Runat="Server">
        <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" Runat="Server">
    Update Currency
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" Runat="Server">
        <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Configuration</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Update Currency</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <input type="hidden" id="HiddenIDCurrency" />
        <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-cogs"></i>Update Currency</div>
                    <div class="actions">
                        <a href="./Currency.aspx" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;List Currency</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form class="form-horizontal" id="form_currency">
                        <div class="form-body">
                            <div id="bootstrap_alert"></div>
                            <div class="alert alert-danger display-none">
                                <button class="close" data-dismiss="alert"></button>
                                You have some form errors. Please check below.
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Name<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" name="Name" class="form-control input" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">ISO Code<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="password" name="ISOCode" class="form-control input" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">ISO Code Number<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" name="ISOCodeNumeric" class="form-control input" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Sign<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" name="Sign" class="form-control input" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Conversion Rate<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" name="ConversionRate" class="form-control input" />
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
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" Runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/config/currency-detail.js"></script>
</asp:Content>

