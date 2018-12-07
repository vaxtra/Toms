<%@ Page Title="Order History - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="OrderHistory.aspx.cs" Inherits="OrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <section id="page-title">

        <div class="container clearfix" style="text-align: center">
            <h1 class="FullName">ORDER HISTORY</h1>
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
							 <div class="box-dnd detailmyaccount table-responsive">
                                <h4 class="bordbot">ORDER HISTORY</h4>
                                <table class="table table-striped order-history">
                                    <thead>
                                        <tr>
                                            <th>NO ORDER</th>
                                            <th>TRANSACTION DATE</th>
                                            <th>TOTAL</th>
                                            <th>PAYMENT METHOD</th>
                                            <th>RESI</th>
                                            <th>STATUS</th>
                                            <th>ACTION</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                            </div>

							

						</div>

					</div>

				</div>


		</section><!-- #content end -->
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/history.js"></script>
</asp:Content>

