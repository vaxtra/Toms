<%@ Page Title="Authentication - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Authentication.aspx.cs" Inherits="Authentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section id="page-title">

			<div class="container clearfix">
				<h1>Client Area</h1>
				
			</div>

		</section><!-- #page-title end -->

		<!-- Content
		============================================= -->
		<section id="content">

			<div class="content-wrap">

				<div class="container clearfix">

					<div class="tabs divcenter nobottommargin clearfix" id="tab-login-register" style="max-width: 500px;">

					<ul class="tab-nav tab-nav2 center clearfix">
							<li class="inline-block"><a href="#tab-login">Sign Up</a></li>
							<li class="inline-block"><a href="#tab-register">Sign In</a></li>
						</ul>

						<div class="tab-container">

							<div class="tab-content clearfix" id="tab-login">
								<div class="card nobottommargin">
									<div class="card-body" style="padding: 40px;">
                                           <form id="FormRegister">
                                    <h4 style="margin-top:10px;">CREATE YOUR ACCOUNT</h4>
                                    <div class="form-group">
                                        <p>Enter your e-mail address to create an account.</p>
                                    </div>
                                    <div class="form-group">
                                        <label>Email :</label>
                                        <input type="email" name="Email" class="form-control margbot15" id="tbEmailSignUp" placeholder="Email" maxlength="50" />
                                    </div>
                                    <button type="submit" class="button button-3d button-black nomargin" id="btnSignUp">CREATE YOUR ACCOUNT</button>
                                </form>
										
									</div>
								</div>
							</div>

							<div class="tab-content clearfix" id="tab-register">
								<div class="card nobottommargin">
									<div class="card-body" style="padding: 40px;">
									
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
                                    <input type="submit" value="SUBMIT" class="button button-3d button-black nomargin" />
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
                                    <input type="submit" value="SUBMIT" class="button button-3d button-black nomargin" />
                                </form>
										
									</div>
								</div>
							</div>

						</div>

					</div>

				</div>

			</div>

		</section><!-- #content end -->
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/authentication.js"></script>
</asp:Content>

