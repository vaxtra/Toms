﻿<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Insert.aspx.cs" Inherits="adminwitcommerce_promo_category_promo_Insert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-summernote/summernote.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    New Rules
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="default.aspx">Rules</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">New Rules</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-edit"></i>New Rules</div>
                    <div class="actions">
                        <a href="./Default.aspx" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;Rules</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form class="form-horizontal" id="form_promo">
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
                                <label class="control-label col-md-3">Description</label>
                                <div class="col-md-5">
                                    <textarea class="form-control" rows="4" name="Description"></textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Minimum Value<span class="required">*</span></label>
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <input type="text" name="MinimumValue" maxlength="2" class="form-control typeMinQty" value="0" />
                                        <div class="input-group-addon">QTY</div>
                                    </div>
                                </div>
                            </div>
                            <div class="panelFreeProducts">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Free Products<span class="required">*</span></label>
                                    <div class="col-md-5">
                                        <input type="text" class="form-control" name="PromoQuantity" value="0" />

                                    </div>
                                </div>
                            </div>
                            <div class="panelDiscount hidden">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Discount Type</label>
                                    <div class="col-md-5">
                                        <input type="checkbox" name="DiscountType" checked="checked" data-off-text="IDR" data-on-text=" % " class="make-switch form-control input" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3">Value<span class="required">*</span></label>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <input type="text" name="Discount" maxlength="2" class="form-control typePercent" value="0" />
                                            <div class="input-group-addon">% </div>
                                        </div>
                                        <div class="input-group hidden">
                                            <input type="text" name="Discount" maxlength="15" class="form-control typeAmount money" value="0" />
                                            <div class="input-group-addon">IDR</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panelPriceBundling hidden">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Price Bundling</label>
                                    <div class="col-md-5">
                                        <input type="text" class="form-control" name="PriceBundling" value="0" />
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
    <script type="text/javascript" src="/assets/admin/pages/scripts/promo/category-promo/insert.js"></script>
</asp:Content>

