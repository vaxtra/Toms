<%@ Page Title="My Account - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="product_list_area section_padding checkoutpage">
        <section class="product-list wow fadeInUp">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12 title-box nomarg">
                        <h2 class="FullName">MY ACCOUNT</h2>
                        <h5>YOUR DETAIL ACCOUNT</h5>
                    </div>
                </div>
                <div class="col-md-12 padtopbot10 accounts">
                    <div class="col-md-3">
                        <ul class="myaccount">
                            <li><a href="/MyAccount"><span class="glyphicon glyphicon-user margright"></span>My Profile</a></li>
                            <li><a href="/ChangePassword"><span class="glyphicon glyphicon-wrench margright"></span>Change Password</a></li>
                            <li><a href="/Myaddress"><span class="glyphicon glyphicon-home margright"></span>My Address</a></li>
                            <li><a href="/ConfirmPayment"><span class="glyphicon glyphicon-credit-card margright"></span>Confirm Payment</a></li>
                            <li><a href="/OrderHistory"><span class="glyphicon glyphicon-shopping-cart margright"></span>Order History</a></li>
                            <li><a href="/Voucher"><span class="glyphicon glyphicon-certificate margright"></span>Voucher</a></li>
                            <li><a href="/Wishlist"><span class="glyphicon glyphicon-bookmark margright"></span>Wishlist</a></li>
                            <li>
                                <a id="btnLogout" href="#"><span class="glyphicon glyphicon-new-window margright"></span>Logout</a></li>
                        </ul>
                    </div>
                    <div class="col-md-9 sidebar border-left">
                        <div class="box-dnd detailmyaccount">
                            <h4>MY PROFILE</h4>
                            <div id="bootstrap_alert"></div>
                            <form id="personal_form">
                                <div class="container-fluid form-horizontal">
                                    <div class="form-group">
                                        <label class="col-lg-3 control-label">Gender : </label>
                                        <div class="col-md-6">
                                            <input type="radio" id="radioMale" checked="checked" class="radio-grand" name="Gender" value="L" /><label for="radioMale">&nbsp;Male&nbsp;</label>
                                            <input type="radio" id="radioFemale" class="radio-grand" name="Gender" value="P" /><label for="radioFemale">&nbsp;Female&nbsp;</label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-lg-3 control-label">Email :</label>
                                        <div class="col-lg-6">
                                            <input type="text" placeholder="Email" name="Email" class="form-control" />
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="help-block">
                                                <span style="color: Red; display: none;">Please fill this form</span>
                                                <span style="color: Red; display: none;">Email is invalid</span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-lg-3 control-label">First Name :</label>
                                        <div class="col-lg-6">
                                            <input type="text" placeholder="First Name" name="FirstName" class="form-control" maxlength="50" />
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="help-block">
                                                <span style="color: Red; display: none;">Please fill this form</span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-lg-3 control-label">Last Name :</label>
                                        <div class="col-lg-6">
                                            <input type="text" placeholder="Last Name" name="LastName" class="form-control" maxlength="50" />
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="help-block">
                                                <span style="color: Red; display: none;">Please fill this form</span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-lg-3 control-label">Date of Birth :</label>
                                        <div class="col-lg-6">
                                            <input type="text" id="ttl" placeholder="Birthdate" name="Birthday" class="form-control datepicker" maxlength="10" />
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="help-block">
                                                <span style="color: Red; display: none;">Please fill this form</span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-lg-3 control-label">Phone :</label>
                                        <div class="col-lg-6">
                                            <input type="text" placeholder="Phone" name="PhoneNumber" class="form-control" maxlength="50" />
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="help-block">
                                                <span style="color: Red; display: none;">Please fill this form</span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-lg-offset-3 col-lg-7">
                                            <input type="checkbox" name="IsSubscribe" />Sign Up for our Newsletter!
                                   
                                        </div>
                                    </div>
                                    <div class="form-group margtop15">
                                        <div class="col-lg-offset-3 col-lg-7">
                                            <input type="submit" class="site-button-dark" value=" SAVE " />
                                            <a class="site-button-light" href="./MyAccount.aspx">CANCEL</a>
                                        </div>
                                    </div>
                                </div>
                            </form>
                            <div class="member-wrap hidden">
                                <h4>MemberData</h4>
                                <div class="container-fluid form-horizontal">
                                    <div class="form-group">
                                        <label class="col-lg-3 control-label">Member Type :</label>
                                        <div class="col-lg-6 control-label" style="text-align: left;">
                                            <span class="MemberType">Black Member</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-lg-3 control-label">Point You Have :</label>
                                        <div class="col-lg-6 control-label" style="text-align: left;">
                                            <span class="Point">10</span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-lg-9 control-label" style="text-align: left;">You can use your point as a discount for your transaction. Your point is increasing every transaction that you didn't use your point </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script src="assets/frontend/scripts/personal-information.js"></script>
    <script>
        $(".datepicker").datepicker({
            format: "dd-mm-yyyy",
            todayBtn: "linked",
            autoclose: true,
            todayHighlight: true
        });
    </script>
</asp:Content>

