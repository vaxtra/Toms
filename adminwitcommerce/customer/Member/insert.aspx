<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="insert.aspx.cs" Inherits="adminwitcommerce_customer_Member_insert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-summernote/summernote.css" />
    <link href="/assets/global/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
    <style>
        .form-horizontal .form-group {
            margin: 0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Insert Member
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Insert Member</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <form id="FormMember">
                    <div class="portlet-title">
                        <div class="caption"><i class="fa fa-edit"></i>Personal Information</div>
                        <div class="actions">
                            <a href="./Default.aspx" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;Member List</a>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-body">
                                <div id="bootstrap_alerts"></div>
                                <div id="alert" class="alert alert-danger display-none">
                                    <button class="close" data-dismiss="alert"></button>
                                    You have some form errors. Please check below.
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label">Title</label>
                                                <div class="form-group">
                                                    <input class="guten-registerTitle" type="radio" name="Gender" value="L" checked="checked" />
                                                    Mr.
                            <input class="guten-registerTitle" type="radio" name="Gender" value="P" />
                                                    Mrs. 
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">First Name</label>
                                                <input type="text" class="form-control guten-input" name="FirstName" maxlength="25" /><sup>*</sup>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Last Name</label>
                                                <input type="text" class="form-control guten-input" name="LastName" maxlength="25" /><sup>*</sup>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Email</label>
                                                <input id="tbEmailSignUp" type="text" class="form-control guten-input" name="Email" maxlength="50" /><sup>*</sup>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Your Password</label>
                                                <input id="password" type="password" class="form-control guten-input" name="Password" /><sup>*</sup>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Retype Your Password</label>
                                                <input type="password" class="form-control guten-input" name="RetypePassword" /><sup>*</sup>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Mobile Phone / Phone</label>
                                                <input type="text" class="form-control guten-input number-only" name="PhoneNumber" maxlength="15" /><sup>*</sup>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Date of Birth</label>
                                                <div class="form-group">
                                                    <input type="text" class="form-control datepicker" id="txtBirthdate" placeholder="" name="Birthdate" />
                                                    <sup style="margin: 0">*</sup>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label class="control-label">Member</label>
                                                    <div class="form-group">
                                                        <select id="selectMember"></select>
                                                        <sup style="margin: 0">*</sup>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label class="control-label">Point</label>
                                                    <div class="form-group">
                                                        <input type="text" class="form-control number-only" placeholder="" name="Point" />
                                                    <sup style="margin: 0">*</sup>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="portlet-title">
                        <div class="caption"><i class="fa fa-edit"></i>Address Information</div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="control-label">Name of your delivery address</label>
                                                <input type="text" class="form-control guten-input" name="AddressName" maxlength="50" /><sup>*</sup>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="control-label">Address 1</label>
                                                <input id="txtAddress" type="text" class="form-control guten-input" name="Address" /><sup>*</sup>
                                            </div>
                                        </div>
                                        <div class="col-md-5" style="visibility: hidden;">
                                            <div class="form-group">
                                                <label class="control-label">Address 2</label>
                                                <input id="txtAddress2" type="text" class="form-control guten-input" name="Address2" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="control-label">Country</label><br />
                                                <input type="hidden" id="Country" name="Country" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="control-label">Province</label>
                                                <input type="hidden" id="Province" name="Province" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">City</label>
                                                <input type="hidden" id="City" name="City" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">District</label>
                                                <input type="hidden" id="District" name="District" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Zip / postal code</label>
                                                <input type="text" class="form-control guten-input" name="PostalCode" maxlength="10" /><sup>*</sup>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">
                                                    <input type="checkbox" name="Newsletter" id="cbNewsletter" />
                                                    Sign up for newsletter for more information about our store & promo</label>
                                            </div>
                                        </div>
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
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script src="/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/customer/member/insert.js"></script>
            <script>
                $(".datepicker").datepicker({
                    format: "dd-mm-yyyy",
                    todayBtn: "linked",
                    autoclose: true,
                    todayHighlight: true
                });
    </script>
</asp:Content>

