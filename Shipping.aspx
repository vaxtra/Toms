<%@ Page Title="Shipping - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Shipping.aspx.cs" Inherits="Shipping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="product-list wow fadeInUp">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 title-box">
                    <h2>SHIPPING</h2>
                    <h5>CHOOSE YOUR SHIPPING METHOD</h5>
                </div>
                <div class="col-md-12 titik">
                    <div class="col-md-7">
                        <input id="cbAgree" type="checkbox" /><span>I agree to the terms of service and adhere to them unconditionally.</span><a target="_blank" href="#">(read)</a>
                        <div class="table-responsive">
                            <table class="table table-shipping" id="table_shipping">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th class="text-center">Image</th>
                                        <th>Carrier</th>
                                        <th>Shipment</th>
                                        <th>Price</th>
                                    </tr>
                                </thead>
                                <tbody class="carriers">
                                    <%--                            <tr>
                                <td colspan="4" class="text-center">Carrier is not available for this address</td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="radio" value="41" name="shipping" /></td>
                                <td>
                                    <img alt="Alternate Text" src="/images/carriers/1.png" class="img-step"></td>
                                <td>Reguler</td>
                                <td>12000 IDR</td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="radio" value="842" name="shipping"></td>
                                <td>
                                    <img alt="Alternate Text" src="/images/carriers/2.png" class="img-step"></td>
                                <td>ONS</td>
                                <td>16000 IDR</td>
                            </tr>--%>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <h6>If you wish, you could put a note to your order.</h6>
                        <div class="form-group">
                            <textarea class="form-control" cols="20" rows="3"></textarea>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <hr class="hr-guten" />
                        <div class="pull-left">
                            <a class="btn-prev btn site-button-light" href="Address.aspx">PREV</a>
                        </div>
                        <div class="pull-right">
                            <a href="#" class="btn-next btn-erigo site-button-dark">NEXT</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/shipping.js?v=1.1"></script>
</asp:Content>

