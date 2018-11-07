<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="faq" %>

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
                            <li><a href="/faq">FAQ</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="section-title">
            <h2 style="margin-top: 10px;">
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
        </div>
    </div>
    <div class="container" style="z-index: 999">
        <div class="row">
            <%--<div class="col-md-6 col-sm-6">
                <asp:Label ID="lblShortDescription" runat="server" Text=""></asp:Label>
            </div>--%>
            <div id="accordion">
                <asp:ListView ID="lvfaq" runat="server">
                    <ItemTemplate>
                        <h2 style="margin-top: 10px; cursor:pointer;font-size:13px;"><i class="fa fa-arrow-circle-o-right"></i> <%# Eval("Title") %></h2>
                        <div style="height: auto !important;">
                            <p>
                                <%# Eval("Description") %>
                            </p>
                        </div>
                    </ItemTemplate>
                </asp:ListView>

                <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <!--Product List Area End-->
    <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
    <script>
        $(function () {
            $("#accordion").accordion({
                collapsible: true,
                minHeight: 500,
                minWidth: 200,
                heightStyle: "fill"
            });
        });
    </script>
</asp:Content>

