<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Insert.aspx.cs" Inherits="adminwitcommerce_content_page_Insert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    New Page
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
        <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="/adminwitcommerce/content/page/">Content</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="/adminwitcommerce/content/page/">Page</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="#">New Page</a>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
     <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-book"></i>New Page</div>
                    <div class="actions">
                        <a href="./Default.aspx" class="btn backLink btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;Back to List</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form class="horizontal-form" id="page_insert">
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="bootstrap_alert"></div>
                                    <div class="alert alert-danger display-hide">
                                        <button class="close" data-close="alert"></button>
                                        You have some form errors. Please check below.
                                    </div>
                                    <div class="alert alert-success display-hide">
                                        <button class="close" data-close="alert"></button>
                                        Data submit succesfully
                                        <%--DRF-335-16830--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="row">
                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <label class="control-label">Page Title <span class="required">*</span></label>
                                                <input type="text" class="form-control" id="tbTitle" name="tbTitle" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-xs-3 col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Active</label>
                                                <div class="icon-group">
                                                    <input type="checkbox" id="cbActive" name="cbActive" class="make-switch" data-on="success" data-off="danger" checked />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label">Short Content</label>
                                                <textarea id="tbShortContent" name="tbShortContent" class="form-control summernote-simple" rows="5"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label">Main Content</label>
                                                <textarea id="tbContent" name="tbContent" class="form-control summernote-simple" rows="5"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions right">
                                <a class="btn btn-default backLink" href="Default.aspx">Cancel</a>
                                <button type="submit" id="btnSubmit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                            </div>
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
    <script type="text/javascript" src="/assets/admin/pages/scripts/content/page/insert.js"></script>
</asp:Content>

