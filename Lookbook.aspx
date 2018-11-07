<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Lookbook.aspx.cs" Inherits="Lookbook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="full_blog_area section_padding padding-bottom-70" style="padding-bottom: 10px;">
        <div class="container">
            <div class="row">
                <div class="breadcumb_list">
                    <ul class="breadcumb">
                        <li><a href="#">
                            <h4>Lookbook</h4>
                        </a></li>
                    </ul>
                </div>
            </div>
        </div>
        <!-- New Collection Women Area -->

        <asp:ListView ID="lvLookbook" runat="server">
            <ItemTemplate>
                <section class="new_women_collection" style="background: url(/assets/images/post/<%# Eval("Photo") %>) no-repeat center center/cover; min-height: 600px; margin-top: 32px">

                    <div class="container">
                        <div class="row">
                            <div class="col-md-12" style="padding-top: 32px; z-index: 999; padding-left: 0;">
                                <div class="breadcumb_list" style="border-bottom: none;">
                                    <h4 style="margin-bottom: 4px;"><a href="/LookbookDetail/<%# Eval("Title").ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").ToLower() %>" style="color: #000; text-transform: uppercase;"><%# Eval("Category") %></a></h4>
                                    <h2><a href="/LookbookDetail/<%# Eval("Title").ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").ToLower() %>" style="color: #000; text-transform: uppercase;"><%# Eval("Title") %></a></h2>
                                </div>
                                <div class="full_blog_submit" style="position: absolute; top: 500px; width: 100%; border-top: 1px solid #000;">
                                    <div class="single_widget last_widget" style="margin-top: 10px;">
                                        <div class="widget_text">
                                            <a href="/LookbookDetail/<%# Eval("Title").ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").ToLower() %>" style="color: #fff;">Explore</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </section>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div class="col-md-12">
        <div class="full_blog_submit">
            <div class="single_widget last_widget" style="margin-top: 10px; text-align: center">
                <div class="widget_text">
                    <button style="padding: 6px 20px; width: 16%;">Load More</button>
                </div>
            </div>
        </div>
    </div>

    <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

