<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="ProductListNoPaging.aspx.cs" Inherits="ProductListNoPaging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="product-list wow fadeInUp">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 title-box">
                    <h2 class="categorynya">ALL PRODUCTS</h2>
                    <h5>Showing <span class="totalproductnya"></span>results</h5>
                </div>
                <!-- end col-12 -->
                <div class="col-md-3 left-sidebar">
                    <div class="categories">
                        <h4 class="title">CATEGORIES</h4>
                        <ul id="ProductCategories">
                            <%--<li><a href="#">FASHION</a><span>(32)</span></li>
                            <li><a href="#">PHOTOS</a><span>(45)</span></li>
                            <li><a href="#">MODELS</a> <span>(22)</span></li>
                            <li><a href="#">AMAZING PHOTOS</a><span>(11)</span></li>
                            <li><a href="#">CLOTHING</a><span>(55)</span></li>
                            <li><a href="#">JEANS</a><span>(67)</span></li>--%>
                        </ul>
                    </div>
                    <!-- end categories -->
                    <div class="price-range">
                        <h4 class="title">FILTER BY PRICE</h4>
                        <input type="text" id="amount" readonly />
                        <div id="slider-range"></div>
                    </div>
                    <!-- end price-range 
                    <div class="colors">
                        <h4 class="title">COLORS</h4>
                        <ul class="color-list">
                            <li><span class="color1"></span></li>
                            <li class="active"><span class="color2"></span></li>
                            <li><span class="color3"></span></li>
                            <li><span class="color4"></span></li>
                            <li><span class="color5"></span></li>
                            <li><span class="color6"></span></li>
                        </ul>
                    </div>
                    <!-- end colors -->
                    <div class="sizes">
                        <h4 class="title">SIZES</h4>
                        <ul class="size-filter">
                            <%--                            <li>
                                <input type="checkbox">
                                <a href="#">S</a><span>(32)</span></li>
                            <li>
                                <input type="checkbox" checked>
                                <a href="#">M</a><span>(45)</span></li>
                            <li>
                                <input type="checkbox">
                                <a href="#">L</a> <span>(22)</span></li>
                            <li>
                                <input type="checkbox">
                                <a href="#">XL</a><span>(11)</span></li>
                            <li>
                                <input type="checkbox">
                                <a href="#">SM</a><span>(55)</span></li>
                            <li>
                                <input type="checkbox">
                                <a href="#">XXL</a><span>(67)</span></li>--%>
                        </ul>
                        <br />
                        <button type="button" class="site-button-dark filterBy-size"><span>FILTER</span></button>
                    </div>
                    <!-- end sizes 
                    <div class="brands">
                        <h4 class="title">BRANDS</h4>
                        <ul>
                            <li>
                                <input type="checkbox">
                                <a href="#">VENDO</a><span>(32)</span></li>
                            <li>
                                <input type="checkbox" checked>
                                <a href="#">SODALES</a><span>(45)</span></li>
                            <li>
                                <input type="checkbox">
                                <a href="#">WROSLER</a> <span>(22)</span></li>
                            <li>
                                <input type="checkbox">
                                <a href="#">ARMANI</a><span>(11)</span></li>
                            <li>
                                <input type="checkbox">
                                <a href="#">CALVIN CKALIN</a><span>(55)</span></li>
                            <li>
                                <input type="checkbox">
                                <a href="#">GUUCCCIO</a><span>(67)</span></li>
                        </ul>
                    </div>
                    <!-- end brands -->
                </div>
                <!-- end col-3 -->
                <div class="col-md-9">
                    <div class="row spacing-row">
                        <div class="col-xs-12 spacing">
                            <ul class="breadcrumb">
                                <li><a href="#">Home</a></li>
                                <li class="arrow">»</li>
                                <li><a class="ProductCategory" href="#">Shop all product</a></li>
                            </ul>
                        </div>
                        <!-- end col-12 -->
                        <div id="ProductList">
                            <%--<div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb1.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$20.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb2.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$120.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb3.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$230.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb4.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$190.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb5.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$500.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb6.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$99.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb2.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$100.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb1.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$990.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb3.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$1.220.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb4.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$700.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb5.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$200.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>
                            </div>
                            <!-- end col-4 -->
                            <div class="col-md-4 col-sm-6 spacing">
                                <div class="product-box">
                                    <span class="left-corner"></span><span class="right-corner"></span>
                                    <div class="product-image">
                                        <img src="images/product-thumb6.jpg" alt="Image">
                                        <div class="product-buttons"><a href="#" class="wishlist-button"><i class="fa fa-heart-o"></i></a><a href="#" class="quickview-button" data-toggle="modal" data-target="#quick-view"><i class="ion-eye"></i></a></div>
                                        <!-- end product-buttons -->
                                    </div>
                                    <h4 class="product-name">PC Plenti Denim</h4>
                                    <span class="product-category">BAGS</span> <span class="product-price">$70.00</span> <a href="item-page.html" class="site-button-dark"><span>VIEW</span></a>
                                </div>--%>
                            <%--</div>--%>
                            <!-- end col-4 -->
                        </div>
                        <div class="col-xs-12">
                            <nav>
                                <ul class="pagination paging-prod">
<%--                                    <li><a href="#">1</a></li>
                                    <li><a href="#">2</a></li>
                                    <li class="active"><a href="#">3 <span class="sr-only">(current)</span></a></li>
                                    <li><a href="#">4</a></li>
                                    <li><a href="#">5</a></li>--%>
                                </ul>
                            </nav>
                        </div>
                    </div>
                    <!-- end row -->
                </div>
                <!-- end col-12 -->
            </div>
            <!-- end row -->
        </div>
        <!-- end container -->
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/ProductNoPaging.js"></script>
</asp:Content>

