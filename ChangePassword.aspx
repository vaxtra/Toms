<%@ Page Title="Change Your Password - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <section id="page-title">

        <div class="container clearfix" style="text-align: center">
            <h1 class="FullName"></h1>
            <h5>YOUR DETAIL ACCOUNT</h5>
        </div>

    </section>
     <section id="content">
				<div class="container clearfix">

					<div id="side-navigation" class="tabs customjs">

						<div class="col_one_third nobottommargin">

							<ul class="sidenav">
								 <li><a href="/MyAccount"><span class="glyphicon glyphicon-user margright"></span>My Profile</a></li>
								 <li><a href="/ChangePassword"><span class="glyphicon glyphicon-wrench margright"></span>Change Password</a></li>
                                <li><a href="/Myaddress"><span class="glyphicon glyphicon-home margright"></span>My Address</a></li>
                                <li><a href="/ConfirmPayment"><span class="glyphicon glyphicon-credit-card margright"></span>Confirm Payment</a></li>
                                <li><a href="/OrderHistory"><span class="glyphicon glyphicon-shopping-cart margright"></span>Order History</a></li>
                                <li><a href="/Voucher"><span class="glyphicon glyphicon-certificate margright"></span>Voucher</a></li>
                                <li><a href="/Package"><span class="glyphicon glyphicon-bookmark margright"></span>Package</a></li>
                                <li>
                                    <a id="btnLogout" href="#"><span class="glyphicon glyphicon-new-window margright"></span>Logout</a></li>
							</ul>

						</div>

						<div class="col_two_third col_last nobottommargin">

							<div id="snav-content1">
								 <div class="box-dnd detailmyaccount">
                                <h4>CHANGE PASSWORD</h4>
                                <div id="bootstrap_alert_pass"></div>
                                <form id="password_form">
                                    <div class="container-fluid form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Old Password :</label>
                                            <div class="col-lg-6">
                                                <input type="password" name="OldPassword" placeholder="Old Password" class="form-control" maxlength="50" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block">
                                                    <span style="color: Red; display: none;">Please fill this form</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">New Password :</label>
                                            <div class="col-lg-6">
                                                <input id="NewPassword" type="password" name="NewPassword" placeholder="New Password" class="form-control" maxlength="25" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block">
                                                    <span style="color: Red; display: none;">Please fill this form</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Re-type New Password :</label>
                                            <div class="col-lg-6">
                                                <input type="password" name="ConfirmPassword" placeholder="Re-type New Password" class="form-control" maxlength="25" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block">
                                                    <span style="color: Red; display: none;">Please fill this form</span>
                                                    <span style="color: Red; display: none;">Invalid, please try again</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-lg-offset-3 col-lg-7">
                                                <input type="submit" class="site-button-dark" value=" SAVE" />
                                                <a class="site-button-light" href="./ChangePassword.aspx">CANCEL</a>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                            </div>

							

						</div>

					</div>

				</div>


		</section><!-- #content end -->
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/UpdatePassword.js"></script>
</asp:Content>

