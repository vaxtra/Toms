<%@ Page Title="Privacy Policy & Terms of Condition - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="PrivacyandTerms.aspx.cs" Inherits="PrivacyandTerms" %>

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
                            <li><a href="/Privacy-term">Privacy and Term</a></li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="section-title">
                <h2 style="margin-top:10px;"><asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
            </div>
        </div>

    </div>
    <div class="container" style="z-index:999">
        <div class="row">
            <div class="col-md-10 col-sm-12">
                <asp:Label ID="lblShortDescription" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
            </div>
            
        </div>
    </div>
    <!--Product List Area End-->
    <hr class="margin-hr" style="margin:0 auto;padding-bottom:32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" Runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

