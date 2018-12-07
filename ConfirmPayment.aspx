<%@ Page Title="Confirm Payment - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="ConfirmPayment.aspx.cs" Inherits="ConfirmPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
    <link href="/assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="HiddenImage" />
    <input type="hidden" id="HiddenIDOrderGuest" />

    <section id="page-title">

        <div class="container clearfix" style="text-align: center">
            <h1 class="FullName">CONFIRM ORDER</h1>
            <h5>Confirm Your Order</h5>
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
                                <h4 style="margin-top: 10px;">CONFIRM PAYMENT</h4>
                                <div id="bootstrap_alert"></div>
                                <form id="form_submit">
                                    <div class="container-fluid form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">No Order :</label>
                                            <div class="col-lg-7">
                                                <select class="form-control ddlOrder input-data" name="IDOrder">
                                                </select>
                                                <input id="IDOrderGuest" type="text" class="form-control textOrder input-data" name="IDOrderGuest" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Account Holder Name :</label>
                                            <div class="col-lg-7">
                                                <input type="text" name="Name" required="" placeholder="Account Holder Name" class="form-control input-data" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block"></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Email :</label>
                                            <div class="col-lg-7">
                                                <input type="text" name="Email" readonly="readonly" required="" placeholder="Email" class="form-control input-data EmailConfirm" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block"></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Phone Number :</label>
                                            <div class="col-lg-7">
                                                <input type="text" name="PhoneNumber" required="" placeholder="Phone Number" class="form-control input-data" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block"></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Transfer Date :</label>
                                            <div class="col-lg-7">
                                                <input type="text" name="Date" required="" placeholder="Transfer Date" class="form-control datepicker input-data" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block"></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Bank :</label>
                                            <div class="col-lg-7">
                                                <input type="text" name="Bank" required="" placeholder="Bank / Account Number" class="form-control input-data" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block"></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Transfer Time :</label>
                                            <div class="col-lg-7">
                                                <input type="text" name="Time" placeholder="Transfer Time" class="form-control time-picker input-data" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block"></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">TOTAL :</label>
                                            <div class="col-lg-7">
                                                <input type="text" name="Amount" placeholder="0 IDR" class="form-control input-data" />
                                                <label style="color: red; width: 100%;">* Don't use dot(.) or comma(,)!</label>
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block"></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Notes :</label>
                                            <div class="col-lg-7">
                                                <input type="text" name="Notes" placeholder="Notes" class="form-control input-data" />
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block"></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">File Upload :</label>
                                            <div class="col-lg-7">
                                                <input type="file" class="fileup input-data" id="fuImage" name="Image" />
                                                <label style="color: red; width: 100%;">* Upload your transfer payment (Max. size 2MB) </label>
                                            </div>
                                            <div class="col-lg-3">
                                                <span class="help-block"></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-lg-offset-3 col-lg-7">
                                                <input type="submit" class="site-button-dark" value="SAVE" />
                                                <a class="site-button-light" href="./MyAccount.aspx">CANCEL</a>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label style="color: red; width: 100%;">* Jika mengalami kesulitan dalam melakukan Konfirmasi pembayaran silahkan hubungi Kami  </label>
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
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script>
        $(".datepicker").datepicker({
            format: "dd-mm-yyyy",
            todayBtn: "linked",
            autoclose: true,
            todayHighlight: true
        });
    </script>
    <script src="/assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script src="assets/frontend/scripts/confirm-payment.js"></script>
</asp:Content>

