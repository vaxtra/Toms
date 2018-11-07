<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Wishlist.aspx.cs" Inherits="Wishlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="product_list_area section_padding checkoutpage">
        <section class="product-list wow fadeInUp">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12 title-box">
                        <h2>WISHLIST</h2>
                        <h5>YOUR WISHLIST PRODUCT</h5>
                    </div>
                    <div class="col-md-12 padtopbot10">
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
                            <div class="box-dnd detailmyaccount table-responsive">
                                <h4 class="bordbot">MY WISHLIST</h4>
                                <table class="table table-striped wishlist">
                                    <thead>
                                        <tr>
                                            <th>PRODUCT</th>
                                            <th>NAME</th>
                                            <th>COMBINATION</th>
                                            <th>DATE ADDED</th>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="Server">
    <script src="/assets/frontend/scripts/wishlist.js"></script>
</asp:Content>

