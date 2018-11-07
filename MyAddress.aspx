<%@ Page Title="My Address - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="MyAddress.aspx.cs" Inherits="MyAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="product_list_area section_padding checkoutpage">
    <section class="product-list wow fadeInUp">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 title-box">
                    <h2 class="FullName">MY ADDRESS</h2>
                    <h5>YOUR ADDRESS LIST</h5>
                </div>
                <div class="col-md-12 padpad margtotop">
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
                        <div class="col-md-12">
                            <h4 class="bordbot margbot">MY ADDRESS </h4>
                            <p class="text-align:justify;">Please configure your default billing and delivery addresses when placing an order. You may also add additional addresses, which can be useful for sending gifts or receiving an order at your office. <a href="./NewAddress.aspx"><b>Click Here</b></a> for New Address.</p>
                        </div>
                        <div class="listaddress">
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/addresses.js"></script>
</asp:Content>

