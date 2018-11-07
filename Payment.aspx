<%@ Page Title="Payment Method - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="product-list wow fadeInUp">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 title-box">
                    <h2>PAYMENT</h2>
                    <h5>CHOOSE YOUR PAYMENT METHOD</h5>
                </div>
                <div class="col-md-12 address">
                    <div class="col-md-12 margtop paddingnya">
                        <p style="padding-left: 5px;">
                            Please select your preferred payment method to pay the amount of <span class="orange-price">
                                <label class="Subtotal format-money">0</label></span> (tax incl.)
                        </p>

                    </div>
                    <div class="col-md-12 paad">
                        <div class="table-responsive">
                            <table class="table table-payment table-hover">
                                <thead>
                                    <tr>
                                        <th>Bank Wire</th>
                                        <th>Account Owner</th>
                                        <th>Account Number</th>
                                    </tr>
                                </thead>
                                <tbody class="payment-list">
                                    <%--                        <tr style="cursor: pointer" class="quarter payment-method">
                            <td>
                                <img style="width: 150px" src="./assets/frontend/images/1.png" alt="Alternate Text" />
                                - <a></a></td>
                            <td>
                                <label>12345678</label>
                            </td>
                            <td>
                                <label>BCA Bandung</label>
                            </td>
                        </tr>
                        <tr style="cursor: pointer" class="quarter payment-method">
                            <td>
                                <img style="width: 150px" src="./assets/frontend/images/1.png" alt="Alternate Text" />
                                - <a></a></td>
                            <td>
                                <label>12345678</label>
                            </td>
                            <td>
                                <label>BCA Bandung</label>
                            </td>
                        </tr>--%>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <hr class="hr-guten" />
                        <div class="pull-left">
                            <a class="btn-prev site-button-light" href="Shipping.aspx">PREV</a>
                        </div>
                        <div class="pull-right">
                            <a href="#" class="btn-next site-button-dark">NEXT</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/payment.js"></script>
</asp:Content>

