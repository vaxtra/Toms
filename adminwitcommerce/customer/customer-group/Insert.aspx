<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Insert.aspx.cs" Inherits="adminwitcommerce_customer_customer_group_Insert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-summernote/summernote.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    New Customer Group
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="default.aspx">Customer Group</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">New Customer Group</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-edit"></i>New Customer Group</div>
                    <div class="actions">
                        <a href="./Default.aspx" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;Customer Group</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form class="form-horizontal" id="form_customerGroup">
                        <div class="form-body">
                            <div id="bootstrap_alerts"></div>
                            <div id="alert" class="alert alert-danger display-none">
                                <button class="close" data-dismiss="alert"></button>
                                You have some form errors. Please check below.
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Name<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" class="form-control" name="Name" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">IsPoint</label>
                                <div class="col-md-5">
                                    <input type="checkbox" class="make-switch form-control" name="IsPoint" />
                                </div>
                            </div>
                            <div class="member_point hidden">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Minimum Transaction<span class="required">*</span></label>
                                    <div class="col-md-5">
                                        <input type="text" name="MinimumTransaction" class="form-control money" value="0" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3">Maximum Transaction<span class="required">*</span></label>
                                    <div class="col-md-5">
                                        <input type="text" name="MaximumTransaction" class="form-control money" value="0" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3">Aquired Point<span class="required">*</span></label>
                                    <div class="col-md-5">
                                        <input type="text" class="form-control money" name="AquiredPoint" value="0" />

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3">Discount Point<span class="required">*</span></label>
                                    <div class="col-md-5">
                                        <input type="text" class="form-control money" name="DiscountPoint" value="0" />

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Active</label>
                                <div class="col-md-3">
                                    <input type="checkbox" class="make-switch form-control" name="Active" />
                                </div>
                            </div>
                        </div>
                        <div class="form-actions fluid">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <a href="Default.aspx" class="btn default">Cancel</a>
                                    <button type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                                </div>
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
    <script type="text/javascript" src="/assets/admin/pages/scripts/customer/customer-group/insert.js"></script>
</asp:Content>

