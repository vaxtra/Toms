<%@ Page Title="NIION INDONESIA | Summary" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="product_list_area section_padding checkoutpage">
        <section class="product-list wow fadeInUp">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12 title-box">
                        <h2>SUMMARY</h2>
                        <h5>YOUR CART SUMMARY</h5>
                    </div>
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table id="cart-list" class="table table-bordered table-hover table-condensed table-striped table-summary">
                                <thead>
                                    <tr>
                                        <th>Product</th>
                                        <th>Name</th>
                                        <th>Price Unit</th>
                                        <th>Qty</th>
                                        <th>Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-center" colspan="6">NO PRODUCT</td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td colspan="4" class="pertotalan little-table">Total Products (tax Incl.) :</td>
                                        <td class="little-table align-right">
                                            <span class="TotalPrice format-money">0</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="pertotalan little-table">Voucher :</td>
                                        <td class="little-table align-right">
                                            <span class="format-money Voucher">0</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="pertotalan little-table">Shipping :</td>
                                        <td class="little-table align-right">
                                            <span class="format-money Shipping">0</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="pertotalan little-table">Total(tax Incl.) :</td>
                                        <td class="little-table align-right">
                                            <span class="Subtotal format-money">0</span>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <div class="col-md-12 nopadding">
                            <div class="pull-left">
                                <form id="form_voucher">
                                    <input type="text" class="form-control voucher" placeholder="Your Voucher" name="Voucher" />
                                    <button type="submit" class="site-button-dark">SUBMIT</button>
                                </form>
                            </div>

                            <div class="pull-right">
                                <a class="site-button-dark" href="Address.aspx">NEXT</a>
                            </div>
                        </div>
                        <div class="overlayPromo">
                            <div class="laycontPromo">
                                <h3>CONGRATULATION</h3>
                                <span class="PromoText"></span>
                                <a class="ProductUrl" href="./ProductPromo.aspx">GET THIS PROMO!</a>
                                <a class="ProductCancel" href="#">Cancel</a>
                                <i class="fa fa-times"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/summary.js?v=1.1.1"></script>
</asp:Content>

