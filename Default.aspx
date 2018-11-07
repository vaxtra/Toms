<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="HiddenIDCategory" />
    <div class="slider-area">
        <div class="bend niceties preview-2">
            <div id="ensign-nivoslider" class="slides">
                <asp:ListView ID="lvSlideshow" runat="server">
                    <ItemTemplate>
                        <a href="<%# Eval("Description") %>">
                            <img src="/assets/images/post/<%# Eval("MediaUrl") %>" alt="" /></a>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
    <!-- slider end-->
    <!-- Banner Area Start  -->
    <div class="banner_area section_padding">
        <div class="container">
            <hr />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 findoutlook">
                    <h2>Shop</h2>
                    <p class="alignright-abs"><a href="/Blog">Find out more</a></p>
                </div>
                <div class="shophome">
                </div>
            </div>
            <hr class="margin-hr" />
        </div>
    </div>
    <!-- Banner Area End -->
    <!-- Featured Items Area Start -->
    <section class="banner_area section_padding">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 findoutlook">
                    <h2>Latest Trending</h2>
                    <p class="alignright-abs"><a href="#">Find out more</a></p>
                </div>
                <div class="col-md-6 col-sm-5">
                    <div class="banner_left_side lookmage">
                        <a id="linkLookbook" runat="server" href="#">
                            <asp:Image ID="imgLookbook" runat="server" />
                        </a>
                    </div>
                    <p class="p-margin">
                        <a id="linkLookbook2" runat="server" href="#">
                            <asp:Label ID="lblLookbook" runat="server" Text=""></asp:Label></a>
                    </p>
                </div>
                <div class="LatestTrending">
                </div>
                <%--                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="single_feature text-center">
                        <div class="feature-img">
                            <a href="#">
                                <img src="/assets/frontend/images/banners/banner6.jpg" alt="Feature Image1" />
                            </a>
                            <span class="img-hover">
                                <a href="#">
                                    <img src="/assets/frontend/images/banners/banner6.jpg" alt="Feature Image1" />
                                </a>
                            </span>
                        </div>
                        <div class="feature_text">
                            <a href="#"><h4>Product name demo #02</h4></a>
                            <span>$399.99<del>$499.99</del></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="single_feature text-center">
                        <div class="feature-img">
                            <a href="#">
                                <img src="/assets/frontend/images/banners/banner7.jpg" alt="Feature Image1" />
                            </a>
                            <span class="img-hover">
                                <a href="#">
                                    <img src="/assets/frontend/images/banners/banner7.jpg" alt="Feature Image1" />
                                </a>
                            </span>
                        </div>
                        <div class="feature_text">
                            <a href="#"><h4>Product name demo #02</h4></a>
                            <span>$399.99<del>$499.99</del></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="single_feature text-center">
                        <div class="feature-img">
                            <a href="#">
                                <img src="/assets/frontend/images/banners/banner7.jpg" alt="Feature Image1" />
                            </a>
                            <span class="img-hover">
                                <a href="#">
                                    <img src="/assets/frontend/images/banners/banner7.jpg" alt="Feature Image1" />
                                </a>
                            </span>
                        </div>
                        <div class="feature_text">
                            <a href="#"><h4>Product name demo #02</h4></a>
                            <span>$399.99<del>$499.99</del></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="single_feature text-center">
                        <div class="feature-img">
                            <a href="#">
                                <img src="/assets/frontend/images/banners/banner6.jpg" alt="Feature Image1" />
                            </a>
                            <span class="img-hover">
                                <a href="#">
                                    <img src="/assets/frontend/images/banners/banner6.jpg" alt="Feature Image1" />
                                </a>
                            </span>
                        </div>
                        <div class="feature_text">
                            <a href="#"><h4>Product name demo #02</h4></a>
                            <span>$399.99<del>$499.99</del></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="single_feature text-center">
                        <div class="feature-img">
                            <a href="#">
                                <img src="/assets/frontend/images/banners/banner7.jpg" alt="Feature Image1" />
                            </a>
                            <span class="img-hover">
                                <a href="#">
                                    <img src="/assets/frontend/images/banners/banner7.jpg" alt="Feature Image1" />
                                </a>
                            </span>
                        </div>
                        <div class="feature_text">
                            <a href="#"><h4>Product name demo #02</h4></a>
                            <span>$399.99<del>$499.99</del></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="single_feature text-center">
                        <div class="feature-img">
                            <a href="#">
                                <img src="/assets/frontend/images/banners/banner7.jpg" alt="Feature Image1" />
                            </a>
                            <span class="img-hover">
                                <a href="#">
                                    <img src="/assets/frontend/images/banners/banner7.jpg" alt="Feature Image1" />
                                </a>
                            </span>
                        </div>
                        <div class="feature_text">
                            <a href="#"><h4>Product name demo #02</h4></a>
                            <span>$399.99<del>$499.99</del></span>
                        </div>
                    </div>
                </div>--%>
            </div>
            <hr class="margin-hr" />
        </div>
    </section>
    <section class="variable_products section_padding">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-xs-12">
                    <div class="section_title">
                        <h2>
                            <asp:Label ID="lblTitleHomeBlock" runat="server" Text=""></asp:Label></h2>
                    </div>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <p>
                        <asp:Label ID="lblShortDescHomeBlock" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblDescHomeBlock" runat="server" Text=""></asp:Label>
                    </p>
                </div>
                <div class="col-md-2 col-sm-6 col-xs-12">
                    <div class="col-md-10 col-xs-12">
                        <a href="#">
                            <img src="/assets/frontend/images/banners/colors icon.jpg" alt="icon" /></a>
                    </div>
                    <div class="col-md-10 col-xs-12">
                        <a href="#">
                            <img src="/assets/frontend/images/banners/foldable icon.jpg" alt="icon" /></a>
                    </div>
                    <div class="col-md-10 col-xs-12">
                        <a href="#">
                            <img src="/assets/frontend/images/banners/water resist icon.jpg" alt="icon" /></a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="col-md-12 col-xs-12">
                        <a href="#">
                            <asp:Image ID="imgHomeBlock" runat="server" /></a>
                    </div>
                </div>
            </div>
            <hr class="margin-hr" />
        </div>
    </section>
    <!--Variable Products End-->
    <!-- Blog Area -->
    <section class="blog_area section_padding">


        <div class="container">
            <div class="row">
                <asp:ListView ID="lvVideoBlock" runat="server">
                    <ItemTemplate>
                        <div data-post="<%# Eval("Title") %>" class="container-slider hide-mobile">
                            <%# Eval("ShortDescription") %>
                        </div>
                        <div data-post="<%# Eval("Title") %>" class="container-slider visible-mobile full-width">
                            <%# Eval("ShortDescription") %>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="row">
                <div class="featured_items">
                    <div class="feature_item_filter">
                        <ul>
                            <asp:ListView ID="lvCategoryVideo" runat="server">
                                <ItemTemplate>
                                    <li><a data-toggle="<%# Eval("Title") %>" aria-controls="all"><%# Eval("Title") %></a></li>
                                </ItemTemplate>
                            </asp:ListView>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <hr class="p-margin" />
    </section>
    <!-- Bg New Collection Area End -->
    <div class="popuppromo">
        <div class="container">
            <div class="col-md-4 nopad">
                <img src="./assets/frontend/images/banners/emailbanner.jpg" />
            </div>
            <div class="col-md-4 bannernews">
                <h4>Always preserved on<br />
                    all updates & promo. <span>& Get 10% off now!</span></h4>
                <div class="sosmedpop">
                    <ul>
                        <li><a target="_blank" href="https://www.instagram.com/niion_id/"><i class="fa fa-instagram"></i></a></li>
                        <li><a target="_blank" href="https://www.facebook.com/NIION.INDONESIA/"><i class="fa fa-facebook"></i></a></li>
                        <li><a target="_blank" href="https://www.youtube.com/user/niionindonesia"><i class="fa fa-youtube"></i></a></li>

                    </ul>
                </div>
            </div>
            <div class="col-md-4 subscribe">
                <label>Get all the info first.</label>
                <form id="FormSubscr">
                    <input class="textsub" name="Email" type="text" placeholder="Enter your email" />
                    <input type="submit" class="butsub" value="Submit" />
                </form>
            </div>
        </div>
        <div class="closepoppromo"><i class="fa fa-times"></i></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/default.js?v=1.1"></script>
    <script src="assets/frontend/js/modernizr.js"></script>

    <script src="assets/frontend/js/jquery.flexslider.js"></script>
    <script>
        $(document).ready(function () {

            $(".popuppromo").delay(4000).fadeIn();

            $(".closepoppromo").click(function () {
                $(".popuppromo").fadeOut();

            });
        });
    </script>
</asp:Content>

