﻿<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="insert.aspx.cs" Inherits="adminwitcommerce_administration_employee_insert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    New Employee
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Administration</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">New Employee</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-people"></i>New Employee</div>
                    <div class="actions">
                        <a href="Default.aspx" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;List Employee</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form class="form-horizontal" id="form_employee">
                        <div class="form-body">
                            <div id="bootstrap_alert"></div>
                            <div class="alert alert-danger display-none">
                                <button class="close" data-dismiss="alert"></button>
                                You have some form errors. Please check below.
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Role<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <select class="form-control role" name="Role"></select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Name<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" name="Name" class="form-control input" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Email<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" name="Email" class="form-control input" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Password<span class="required">*</span></label>
                                <div class="col-md-5">
                                    <input type="password" name="Password" class="form-control input" />
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
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/administration/employee/insert.js"></script>
</asp:Content>

