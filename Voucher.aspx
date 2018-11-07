<%@ Page Title="Vouchers - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Voucher.aspx.cs" Inherits="Voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="product_list_area section_padding checkoutpage">
        <section class="product-list wow fadeInUp">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12 title-box">
                        <h2>VOUCHERS</h2>
                        <h5>YOUR LIST VOUCHERS</h5>
                    </div>
                    <div class="col-md-12 breadcrumb padtopbot10">
                        <div class="col-md-3 border-right">
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
                        <div class="col-md-9 sidebar">
                            <div class="box-dnd detailmyaccount table-responsive">
                                <h4 class="bordbot">MY VOUCHER</h4>
                                <table class="table table-striped voucher-table voucher-list">
                                    <thead>
                                        <tr>
                                            <th>CODE VOUCHER</th>
                                            <th>TYPE</th>
                                            <th>AMOUNT</th>
                                            <th>START DATE</th>
                                            <th>EXPIRED</th>
                                            <th>USED</th>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/voucher.js"></script>
</asp:Content>

