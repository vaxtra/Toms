<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="LookbookDetail.aspx.cs" Inherits="LookbookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="new_women_collection" id="coverlook" runat="server">
        <div class="row padding-bottom-70">
            <div class="col-md-12 col-md-offset-12" style="margin-top: 24px; padding: 0 20px auto 20px;">
                <div class="breadcumb_list " style="border-bottom: none">
                    <ul class="breadcumb col-md-offset-1">
                        <li><a href="#">
                            <h4 style="margin-bottom: 8px;">
                                <asp:Label ID="lblCategoryLookbook" runat="server" Text=""></asp:Label>
                                /
                                <asp:Label ID="lblTitleLookbook" runat="server" Text=""></asp:Label></h4>
                        </a></li>
                    </ul>
                </div>
                <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 0; width: 540px; position: absolute; left: 68px; padding-bottom: 8px;" />
                <div class="full_blog_area section_padding">
                    <div class="col-md-10 col-md-offset-1" style="padding: 0">
                        <p style="line-height: 24px; margin-top: 16px; margin-bottom: 32px;">
                            <asp:Label ID="lblShortDescription" runat="server" Text=""></asp:Label>
                        </p>
                        <p style="line-height: 24px; margin-top: 16px; margin-bottom: 32px;">
                            <asp:Label ID="lblDescriptionLookbook" runat="server" Text=""></asp:Label>
                        </p>
                        <!-- Testimonial Area Start -->
                        <div id="fawesome-carousel" class="carousel slide" data-ride="carousel">
                            <ol class="carousel-indicators">
                                <li data-target="#fawesome-carousel" data-slide-to="0" class="active"></li>
                                <li data-target="#fawesome-carousel" data-slide-to="1"></li>
                                <li data-target="#fawesome-carousel" data-slide-to="2"></li>
                            </ol>
                            <div class="carousel-inner" role="listbox">
                                <asp:ListView ID="lvLookbookPhoto" runat="server">
                                    <ItemTemplate>
                                        <div class="item <%# (int)Eval("index") == 0 ? "active" : "" %>">
                                            <img src="/assets/images/post/<%# Eval("MediaUrl") %>" alt="lookbook 1" />
                                        </div>
                                    </ItemTemplate>

                                </asp:ListView>
                            </div>
                        </div>
                        <!-- Testimonial Area End -->
                    </div>
                </div>
            </div>

        </div>


    </section>


    <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

