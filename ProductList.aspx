<%@ Page Title="Products - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="HiddenActiveFilter" />
    <section id="ProductBanner" runat="server" class="new_women_collection background-one">
        <div class="container">
            <div class="row">
                <div class="col-md-12">

                </div>
            </div>
        </div>
    </section>
    <!-- New Collection Women Are End -->
    <!--Product List Area -->
    <div class="product_list_area section_padding">
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 col-xs-12">
                    <div class="product_single_sidebar">
                        <div class="sidebar_title">
                            <p>Filter</p>
                        </div>
                        <div class="sidebar_menu" id="cate-toggle">
                            <ul>
                                <li class="has-sub">
                                    <a href="#">Categories</a>
                                    <ul id="ProductCategories" class="category-sub">
<%--                                        <li><a href="#">Hip Bag</a></li>
                                        <li><a href="#">Outer</a></li>
                                        <li><a href="#">Lunar</a></li>--%>
                                    </ul>
                                </li>
                                <li class="has-sub">
                                    <a href="#">Price</a>
                                    <ul class="category-sub">
                                        <li><a class="pricefilt" data-min="0" data-max="100000">0 - 100000 IDR</a></li>
                                        <li><a class="pricefilt" data-min="100000" data-max="200000">100000 - 200000 IDR</a></li>
                                        <li><a class="pricefilt" data-min="200000" data-max="300000">200000 - 300000 IDR</a></li>
                                        <li><a class="pricefilt" data-min="300000" data-max="400000">300000 - 400000 IDR</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="breadcumb_list">
                        <ul class="breadcumb">
                            <li><a href="/Home">Home</a></li>
                            <li>/</li>
                            <li><a href="/Categories">Categories</a></li>
                            <li>/</li>
                            <li><a href="#" class="categorynya"></a></li>
                        </ul>
                    </div>
                    <div class="product_area">
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>
                        <div class="list_product">
                            <div class="row" id="ProductList">
<%--                                <div class="col-md-3 col-sm-6 col-xs-6">
                                    <div class="single_feature text-center">
                                        <div class="feature-img">
                                            <a href="product-details.html">
                                                <img src="./images/variable_products/products/hipbag/1.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/1.jpg" alt="Feature Image1" />
                                                </a>
                                            </span>
                                        </div>
                                        <div class="feature_text">
                                            <a href="product-details.html"><h4>Product name demo #02</h4></a>
                                            <span>$399.99<del>$499.99</del></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-6">
                                    <div class="single_feature text-center">
                                        <div class="feature-img">
                                            <a href="#">
                                                <img src="./images/variable_products/products/hipbag/2.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/2.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/3.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/3.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/4.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/4.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/5.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/5.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/6.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/6.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/7.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/7.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/9.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/9.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/10.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/10.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/11.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/11.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/12.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/12.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/13.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/13.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/14.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/14.jpg" alt="Feature Image1" />
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
                                                <img src="./images/variable_products/products/hipbag/15.jpg" alt="Feature Image1" />
                                            </a>
                                            <span class="img-hover">
                                                <a href="#">
                                                    <img src="./images/variable_products/products/hipbag/15.jpg" alt="Feature Image1" />
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
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-6 col-xs-12">
                                <div class="pagination_area">
                                    <div class="pagi right paging-prod">
<%--                                        <a href="#" class="floatleft"><i class="fa fa-arrow-left"> Previous </i></a>
                                        <!--<ul>
                                            <li><a href="#"><i class="fa fa-caret-left"></i></a></li>
                                            <li><a href="#" class="active">1</a></li>
                                            <li><a href="#">2</a></li>
                                            <li><a href="#">3</a></li>
                                            <li><a href="#"><i class="fa fa-caret-right"></i></a></li>
                                        </ul>-->
                                       <a href="#" class="right"> Next <i class="fa fa-arrow-right"></i></a>--%>
                                    </div>
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
    <!--Product List Area End-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="<%= Page.ResolveUrl("/assets/frontend/scripts/Product.js?v=1.1.2") %>"></script>
    <script>
        $(window).scroll(function () {
            if ($(window).scrollTop() > 130) {
                $('.product_single_sidebar').addClass("fixing");
            }
            else {
                $('.product_single_sidebar').removeClass("fixing");
            }
        });
    </script>
</asp:Content>

