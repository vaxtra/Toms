<%@ Page Title="PROMO PRODUCT" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="ProductPromo.aspx.cs" Inherits="ProductPromo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .sizing:first-child {
            border-left: 1px solid #111;
        }

        .sizing {
            float: left;
            border: 1px solid #111;
            border-left: 0;
            padding: 12px 17px;
            margin-top: 10px;
            cursor: pointer;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="HiddenIDCategoryPromo" />
    <input type="hidden" id="HiddenIDPromo" />
    <div class="col-md-12">
        <h3 class="centerBro">Select Free <span class="freeprod"></span> Products</h3>
        <div id="ProductList" class="row prodlist">
<%--            <div class="col-md-3 singlepromo">
                <img src="assets/frontend/img/product-sample.png" class="img-full" />
                <div class="desc-prodlist">
                    <div class="col-md-12">
                        <hr class="hr-erigo" />
                        <div class="col-md-9 nopadding">
                            <label>White T's</label>
                            <span>IDR 200.000,00</span>
                        </div>
                        <div class="col-md-3 nopadding">
                            <input type="text" class="form-control pull-right" value="1" />
                        </div>
                    </div>
                    <div class="col-md-12 nopadding">
                        <div style="margin: auto; display: table;">
                            <div class="sizing">S</div>
                            <div class="sizing">M</div>
                            <div class="sizing">L</div>
                            <div class="sizing">XL</div>
                        </div>
                    </div>
                    <div class="col-md-12 text-center margtotop">
                        <a href="#" class="btn btn-erigo">ADD TO BONUS</a>
                    </div>
                </div>
            </div>--%>
            <!--                    <a href="productDetail.aspx" class="col-md-2 col-md-offset-1">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's</label>
                <span>IDR 200.00</span>
            </div>
        </a>
<%--        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2 col-md-offset-1">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2 col-md-offset-1">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>
        <a href="productDetail.aspx" class="col-md-2">
            <img src="assets/frontend/img/product-sample.png" class="img-full" />
            <div class="desc-prodlist">
                <label>White T's With Polka</label>
                <span>IDR 200.00</span>
            </div>
        </a>--%>-->
        </div>
    </div>
    <div class="col-md-12">
        <a id="SubmitPromo" href="#" class="btn btn-erigo">SUBMIT TO CART</a>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="Server">
    <script src="assets/frontend/scripts/ProductPromo.js"></script>
</asp:Content>

