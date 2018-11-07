<%@ Page Title="Authentication - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Authentication.aspx.cs" Inherits="Authentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="product_list_area section_padding checkoutpage">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-xs-12">
                    <div class="breadcumb_list">
                        <ul class="breadcumb">
                            <li><a href="#">
                                <h2>AUTHENTICATION</h2>
                            </a></li>
                        </ul>
                    </div>
                    <div id="bootstrap_alert"></div>
                    <div class="col-md-6 col-sm-6 col-xs-12 border-right">
                        <div class="box-dnd">
                            <div class="container-fluid">
                                <form id="FormRegister">
                                    <h4 style="margin-top:10px;">CREATE YOUR ACCOUNT</h4>
                                    <div class="form-group">
                                        <p>Enter your e-mail address to create an account.</p>
                                    </div>
                                    <div class="form-group">
                                        <label>Email :</label>
                                        <input type="email" name="Email" class="form-control margbot15" id="tbEmailSignUp" placeholder="Email" maxlength="50" />
                                    </div>
                                    <button type="submit" class="site-button-dark" id="btnSignUp">CREATE YOUR ACCOUNT</button>
                                </form>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  col-sm-6 col-xs-12 border-right">
                        <div class="box-dnd">
                            <div class="container-fluid">
                                <form id="FormLogin">
                                    <h4 style="margin-top:10px;">ALREADY REGISTERED?</h4>
                                    <div class="form-group">
                                        <label>Email :</label>
                                        <input type="text" class="form-control" placeholder="Email" name="Email" />
                                        <span class="help-block">
                                            <span style="color: Red; display: none;">Please, fill this form</span>
                                            <span style="color: Red; display: none;">Email is invalid</span>
                                        </span>
                                    </div>
                                    <div class="form-group">
                                        <label>Password :</label>
                                        <input type="password" class="form-control" placeholder="Password" name="Password" />
                                        <span class="help-block">
                                            <span style="color: Red; display: none;">Please, fill this form</span>
                                        </span>
                                    </div>
                                    <div class="form-group">
                                        <a href="#" class="btn-forgot">Forgot Password?</a>
                                    </div>
                                    <input type="submit" value="SUBMIT" class="site-button-dark" />
                                </form>
                                <form id="FormForgot" style="display: none;">
                                    <h4 style="margin-top:10px;">PLEASE ENTER YOUR EMAIL</h4>
                                    <div class="form-group">
                                        <label>Email :</label>
                                        <input type="text" class="form-control EmailForgot" placeholder="Email" name="Email" />
                                        <span class="help-block">
                                            <span style="color: Red; display: none;">Please, fill this form</span>
                                            <span style="color: Red; display: none;">Email is invalid</span>
                                        </span>
                                    </div>
                                    <input type="submit" value="SUBMIT" class="site-button-dark" />
                                </form>
                            </div>
                        </div>
                    </div>
                    <%--<div class="col-md-4  col-sm-4 col-xs-12">
                        <div class="box-dnd">
                            <div class="container-fluid">
                                <form id="FormGues">
                                    <h4 style="margin-top:10px;">Guest Checkout</h4>

                                    <a href="GuestCheckOut.aspx" class="site-button-dark" id="btnGuest">GUEST CHECKOUT</a>
                                </form>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/authentication.js"></script>
</asp:Content>

