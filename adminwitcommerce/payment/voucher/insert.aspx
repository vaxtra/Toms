<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="insert.aspx.cs" Inherits="adminwitcommerce_voucher_insert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-summernote/summernote.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    New Voucher
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="default.aspx">Voucher</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">New Voucher</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-edit"></i>New Voucher</div>
                    <div class="actions">
                        <a href="./Default.aspx" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;Voucher</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form class="form-horizontal" id="form_voucher">
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
                                <label class="control-label col-md-3">Code<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" class="form-control" name="Code" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Customer</label>
                                <div class="col-md-5">
                                    <input type="hidden" class="form-control" id="IDCustomer" name="IDCustomer" />
                                    <label class="help-block">Leave blank if voucher is not limited for single customer</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Description</label>
                                <div class="col-md-5">
                                    <textarea class="form-control" rows="4" name="Description"></textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Voucher Type</label>
                                <div class="col-md-5">
                                    <input type="checkbox" name="VoucherType" checked="checked" data-off-text="IDR" data-on-text=" % " class="make-switch form-control input" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Value<span class="required">*</span></label>
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <input type="text" name="Value" maxlength="2" class="form-control typePercent" value="0" />
                                        <div class="input-group-addon">% </div>
                                    </div>
                                    <div class="input-group hidden">
                                        <input type="text" name="Value" maxlength="15" class="form-control typeAmount money" value="0" />
                                        <div class="input-group-addon">IDR</div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Total Available</label>
                                <div class="col-md-5">
                                    <input type="text" class="form-control" name="TotalAvailable" value="0" />
                                    <label class="help-block">The cart rule will be applied to the first "X" customers only.</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Minimum Amount</label>
                                <div class="col-md-5">
                                    <input type="text" class="form-control money" name="MinimumAmount" value="0" />
                                    <label class="help-block">fill zero if you want to skip minimum amount</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Start Date</label>
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <input class="form-control" type="text" name="StartDate" id="StartDate" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">End Date</label>
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <input class="form-control" type="text" name="EndDate" id="EndDate" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
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
    <script type="text/javascript" src="/assets/admin/pages/scripts/payment/voucher/insert.js"></script>
</asp:Content>

