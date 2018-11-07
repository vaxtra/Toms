<%@ Page Title="Blog List - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Blog.aspx.cs" Inherits="Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Product List Area -->
    <div class="full_blog_area section_padding padding-bottom-70">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-9 col-xs-12">
                    <div class="breadcumb_list">
                        <ul class="breadcumb">
                            <li><a href="#">
                                <h2>Journal</h2>
                            </a></li>
                        </ul>
                    </div>
                    <!--Breadcumb Area End -->
                    <div class="row">
                        <asp:ListView ID="lvLatestBlog" runat="server">
                            <ItemTemplate>
                                <div class="col-md-6">
                                    <div class="full_blog">
                                        <div class="full_blog_title">
                                            <a href="/Post/<%# Eval("Title").ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").Replace("!", "").ToLower() %>">
                                                <h2><%# Eval("Title") %></h2>
                                            </a>
                                        </div>

                                        <div class="full_blog_desc">
                                            <p>
                                                <%# Eval("ShortDescription") %>
                                            </p>
                                        </div>
                                        <div class="full_blog_submit">
                                            <div class="read_more_btn">
                                                <p class="p-margin"><a href="/Post/<%# Eval("Title").ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").Replace("!", "").ToLower() %>"><%# string.Format("{0:dd/MM/yy}", Eval("Date")) %></a></p>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="full_blog">
                                        <div class="full_blog_caption">
                                            <a href="/Post/<%# Eval("Title").ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").Replace("!", "").ToLower() %>">
                                                <img src="/assets/images/post/<%# Eval("Photo") %>" alt="<%# Eval("Title") %>" /></a>
                                        </div>

                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <div class="row">
                        <asp:ListView ID="lvBlog" runat="server">
                            <ItemTemplate>
                                <div class="col-md-3 col-sm-5">
                                    <div class="blog">
                                        <a href="/Post/<%# Eval("Title").ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").Replace("!", "").ToLower() %>">
                                            <p><%# Eval("Title") %></p>
                                        </a>
                                        <div class="blog_caption">
                                            <a href="/Post/<%# Eval("Title").ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").Replace("!", "").ToLower() %>">
                                                <img src="/assets/images/post/<%# Eval("Photo") %>" alt="blog" /></a>
                                        </div>
                                        <div class="read_more_btn">
                                            <p class="p-margin"><a href="/Post/<%# Eval("Title").ToString().Trim().Replace(" ", "-").Replace("\"", "").Replace(".", "").Replace("!", "").ToLower() %>"><%# string.Format("{0:dd/MM/yy}", Eval("Date")) %></a></p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                    <div class="pagination_area">
                        <div class="pagi">

                            <ul>
                                <li><a href="#"><i class="fa fa-plus"></i></a></li>
                            </ul>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

