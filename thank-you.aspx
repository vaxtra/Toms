<%@ Page Title="Thank You! - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="thank-you.aspx.cs" Inherits="thank_you" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="page-title">

        <div class="container clearfix" style="text-align: center">
            <h1>Thank You !</h1>
            <h5>YOUR ORDER HAS BEEN PLACED SUCCESSFULLY</h5>
        </div>

    </section>
    <!-- #page-title end -->
    <div class="product_list_area section_padding checkoutpage">
        <section class="product-list wow fadeInUp">
            <div class="container">
                <div class="row">

                    <div class="col-md-12 thanks-sub ">

                        <p style="padding: 0 15px; text-align: center">
                            Here some Your Detail Order :
                   
                        </p>
                        <div class="row">
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
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4 margtotop margbot">
                                <h4 class="centerBro">You can confirm your order on <a href="MyAccount.aspx" style="color: red;">My Account</a> Page
                               
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

