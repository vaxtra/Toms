<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="reset-password.aspx.cs" Inherits="reset_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <input type="hidden" id="token" />
    <div class="col-md-12">
        <div class="col-md-12">
            <div id="bootstrap_alert"></div>
        </div>
        <div class="col-md-6 col-md-offset-3 margtop margbot padpad">
<div class="box-dnd">
            <label class="authen-title">Reset Password</label>
            <div class="authentication-content">
                <p>Please enter the e-mail address used to register. We will e-mail you your new password.</p>
                <form id="FormReset">
                    <div class="col-md-12 form-group">
                        <label class="col-md-4">New Password</label>
                        <input type="password" class="textbox-ouval col-md-8" id="Password" name="Password" />
                    </div>
                    <div class="col-md-12 form-group">
                        <label class="col-md-4">Re-type Password</label>
                        <input type="password" class="textbox-ouval col-md-8" id="Password1" name="Password2" />
                    </div>
                    <div class="full-content" style="padding:30px;padding-bottom:0">
                        <button type="submit" class="btn-erigo btn">Submit</button>
                        <a href="Authentication.aspx" class="authen-href">Back to Login</a>
                    </div>
                </form>
            </div>
        </div></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" Runat="Server">
        <script src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script src="/assets/frontend/scripts/reset-password.js"></script>
</asp:Content>

