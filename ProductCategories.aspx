<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="ProductCategories.aspx.cs" Inherits="ProductCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!--Product List Area -->
    <div class="product_list_area section_padding">

        <div class="container">

            <div class="row">

                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="breadcumb_list" style="margin-left: -15px;">
                        <ul class="breadcumb">
                            <li><a href="/">Home</a></li>
                        </ul>
                    </div>
                    <div class="product_area">
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>
                        <div class="list_product nomarg">
                            <div class="row">
                                <div id="ProductList">

                                </div>
                               <div class="clear"></div>
                                <div class="col-md-12">
                                    <a class="shoparrival" href="/Products">Shop New Arrival</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Product List Area End-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" Runat="Server">
    <script src="assets/frontend/scripts/ProductCategories.js"></script>
</asp:Content>

