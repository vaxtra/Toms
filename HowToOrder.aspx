<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="HowToOrder.aspx.cs" Inherits="HowToOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!--Product List Area -->
    <div class="product_list_area section_padding">

        <div class="container">

            <div class="row">

                <div class="col-md-12 col-sm-9 col-xs-12">
                    <div class="breadcumb_list" style="margin-left: -15px;">
                        <ul class="breadcumb">
                            <li><a href="/Home">Home</a></li>
                            <li>/</li>
                            <li><a href="/How-to-order">How to Order</a></li>
                        </ul>
                    </div>
                    <div class="product_area">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6 col-md-offset-2">
                                    <asp:Image ID="ImageHowto1" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="list_product nomarg">
                            <div class="row">
                                <div class="col-md-10">
                                    <asp:Label ID="lblShortDescription" runat="server" Text=""></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Product List Area End-->
    <hr class="margin-hr" style="margin:0 auto;padding-bottom:32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" Runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

