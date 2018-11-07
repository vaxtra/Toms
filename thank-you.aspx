<%@ Page Title="Thank You! - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="thank-you.aspx.cs" Inherits="thank_you" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="product_list_area section_padding checkoutpage">
        <section class="product-list wow fadeInUp">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12 title-box thankstit">
                        <h2>THANK YOU!</h2>
                        <h5>YOUR ORDER HAS BEEN PLACED SUCCESSFULLY</h5>
                    </div>
                    <div class="col-md-12 thanks-sub">
                        <h3 class="text-center">Thank You for Shopping at NIION.</h3>
                        <p style="padding: 0 15px;">
                            Here some Your Detail Order :
                   
                        </p>
                        <div class="col-md-4">
                            <div class="box-dnd border-right">
                                <label>ORDER REFERENCE :</label>
                                <p class="Reference"></p>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="box-dnd border-right">
                                <label>Total Payment :</label>
                                <p class="TotalPaid format-money"></p>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="box-dnd">
                                <label>Transfer Payment to :</label>
                                <p>A/N <span class="PaymentOwner"></span>- <span class="AccountNumber"></span></p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4 margtotop margbot">
                                <h4 class="centerBro">You can confirm your order on <a href="MyAccount.aspx" style="color: red;">My Account</a> Page
                               
                                    <br />
                                    <br />
                                    OR</h4>
                                <a class="distab btn btn-black" href="Default.aspx">BACK TO SHOP</a>
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/thankyou.js"></script>
</asp:Content>

