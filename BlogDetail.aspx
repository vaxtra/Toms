<%@ Page Title="Blog Detail - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="BlogDetail.aspx.cs" Inherits="BlogDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- New Collection Women Area -->
    <section class="new_women_collection" id="headermage" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-12 padding-bottom-70">
                    <div class="breadcumb_list" style="border-color: #fff;">
                        <ul class="breadcumb">
                            <li><a href="#" style="color: #fff;">
                                <asp:Label ID="lblTitlePost" runat="server" Text=""></asp:Label></a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- New Collection Women Are End -->
    <div class="full_blog_area">
        <div class="container">

            <div class="row">

                <div class="col-md-10 col-sm-9 col-xs-12">
                    <div class="full_blog single">
                        <div class="full_blog_content">
                            <div class="full_blog_desc">
                                <h4 style="line-height: 32px; margin-bottom: 40px;">
                                    <asp:Label ID="lblShortDescription" runat="server" Text=""></asp:Label>
                                </h4>
                                <p style="line-height: 24px;">
                                    <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>
    <asp:ListView ID="lvPhotoBlog" runat="server">
        <ItemTemplate>
            <section class="new_women_collection" style="background: url(/assets/images/post/<%# Eval("MediaUrl") %>) no-repeat center center/cover; min-height: 600px; margin-bottom: 40px;">
                <div class="container">
                    <div class="row">
                    </div>
                </div>
            </section>
        </ItemTemplate>
    </asp:ListView>
    <!--Product List Area End-->
    <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/js/modernizr.js"></script>

    <script src="assets/frontend/js/jquery.flexslider.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $('.flexslider').flexslider({
                animation: "slide",
            });
        });
    </script>
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

