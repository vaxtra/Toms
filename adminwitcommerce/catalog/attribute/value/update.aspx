<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="update.aspx.cs" Inherits="adminwitcommerce_catalog_attribute_value_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Update Value
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="/adminwitcommerce/catalog/">Catalog</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="/adminwitcommerce/catalog/attribute/">Attribute</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a class="backLinkParent" href="#">Value</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="#" id="breadcrumbName">Update Value</a>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <input type="hidden" id="HiddenIDValue" />
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-edit"></i>Update Value</div>
                    <div class="actions">
                        <a href="Default.aspx" class="backLink btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;Attribute Values</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form id="form_value" class="form-horizontal">
                        <div class="form-body">
                            <div id="bootstrap_alerts"></div>
                            <div class="alert alert-danger display-none">
                                <button class="close" data-dismiss="alert"></button>
                                You have some form errors. Please check below.
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Value</label>
                                <div class="col-md-5">
                                    <input type="text" id="Name" name="Name" class="form-control input" />
                                </div>
                            </div>
                            <div class="form-group color">
                                <label class="control-label col-md-3">Color (RGB)<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <div class="input-group colorpicker-default">
                                        <input type="text" id="tbRGBColor" name="RGBColor" class="form-control input" value="#000000" />
                                        <span class="input-group-addon"><i></i></span>
                                    </div>
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
    <script type="text/javascript" src="/assets/global/plugins/bootbox/bootbox.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/catalog/attribute/value/update.js"></script>
</asp:Content>

