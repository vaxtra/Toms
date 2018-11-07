<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="OurStore.aspx.cs" Inherits="OurStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Product List Area -->
    <div class="product_list_area section_padding">

        <div class="container">

            <div class="row">

                <div class="col-md-12 col-sm-9 col-xs-12">
                    <div class="breadcumb_list" style="margin-left: -15px;">
                        <ul class="breadcumb">
                            <li><a href="/Home">Home</a></li>
                            <li>/</li>
                            <li><a href="/Store">Our Stores</a></li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="section-title">
                <h2 style="margin-top: 10px;">
                    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
            </div>
        </div>

    </div>
    <div class="container" style="z-index: 999">
        <div class="row">
            <asp:ListView ID="lvStore" runat="server">
                <ItemTemplate>
                    <div class="col-md-10 col-sm-10">
                        <h5 class="aligncenter" style="margin: 10px auto;"><%# Eval("Title") %></h5>
                        <label class="aligncenter"><%# Eval("Description") %></label>
                        <div class="col-md-3">
                            <img class="img-responsive" src="/assets/images/post/<%# Eval("MediaUrl") %>" />

                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>

        </div>
    </div>
    <!--Product List Area End-->
    <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

