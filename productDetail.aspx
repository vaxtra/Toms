<%@ Page Title="Product - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="productDetail.aspx.cs" Inherits="productDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/frontend/css/raxus.css" rel="stylesheet" />
    <style>
        #mySlider {
            width: 100%;
            margin: auto;
            height: 540px;
        }

        *, *:after, *:before {
            border-box: 0px;
            box-sizing: border-box;
        }

        .clearfix:before, .clearfix:after {
            content: '';
            display: table;
        }

        .clearfix:after {
            clear: both;
        }


        #mySlider .mini-images li {
            width: 70px;
            height: 70px;
        }

        #pro_0 {
            min-height: 650px;
        }

        @media screen and (max-width: 640px) {
            #mySlider {
                height: 250px;
            }
        }

        .hm {
            padding-top: 12px;
            padding-bottom: 9px;
        }

        .zoom img::selection {
            background-color: transparent;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Product List Area -->
    <div class="product_list_area section_padding padding-bottom-70">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 bungkusproduk">
                    <div class="breadcumb_list" style="margin-left: -15px;">
                        <ul class="breadcumb">
                            <li><a href="/Products">Shop</a></li>
                            <li>/</li>
                            <li><a href="#" class="DefaultCategory"></a></li>
                            <li>/</li>
                            <li><a href="#" class="ProductNameCrumb"></a></li>
                        </ul>
                    </div>
                    <!--Breadcumb Area End -->
                    <div class="product_area">
                        <div class="list_product">
                            <div class="row">
                                <div class="col-md-7 col-sm-7 col-xs-12">
                                    <div class="product_caption col-md-10">
                                        <div class="tab-content potoslide">
                                            <%--                                            <div role="tabpanel" class="tab-pane fade in active" id="pro_one">
                                                <a href="./images/variable_products/products/hipbag/1.jpg" data-lightbox="image-1" data-title="Black Skirt"><img src="./images/variable_products/products/hipbag/1.jpg" alt="details" /></a>
                                            </div>
                                            <div role="tabpanel" class="tab-pane fade in" id="pro_two">
                                                <a href="./images/variable_products/products/hipbag/2.jpg" data-lightbox="image-1" data-title="Black Skirt"><img src="./images/variable_products/products/hipbag/2.jpg" alt="details" /></a>
                                            </div>
                                            <div role="tabpanel" class="tab-pane fade in" id="pro_three">
                                                <a href="./images/variable_products/products/hipbag/3.jpg" data-lightbox="image-1" data-title="Black Skirt"><img src="./images/variable_products/products/hipbag/3.jpg" alt="details" /></a>
                                            </div>
                                            <div role="tabpanel" class="tab-pane fade in" id="pro_four">
                                                <a href="./images/variable_products/products/hipbag/4.jpg" data-lightbox="image-1" data-title="Black Skirt"><img src="./images/variable_products/products/hipbag/4.jpg" alt="details" /></a>
                                            </div>--%>
                                        </div>
                                    </div>
                                    <div class="details_feature_item_filter col-md-2 nopad">
                                        <ul class="thumbfoto">
                                            <%--                                            <li class="active">
                                                <a href="#pro_one" data-toggle="tab" aria-controls="pro_one">
                                                    <img src="./images/variable_products/products/hipbag/1.jpg" alt="details" />
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#pro_two" data-toggle="tab" aria-controls="pro_two">
                                                    <img src="./images/variable_products/products/hipbag/2.jpg" alt="details" />
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#pro_three" data-toggle="tab" aria-controls="pro_three">
                                                    <img src="./images/variable_products/products/hipbag/3.jpg" alt="details" />
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#pro_four" data-toggle="tab" aria-controls="pro_four">
                                                    <img src="./images/variable_products/products/hipbag/4.jpg" alt="details" />
                                                </a>
                                            </li>--%>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-md-5 col-sm-5 col-xs-12">
                                    <div class="product_content">
                                        <div class="product_title">
                                            <a href="#">
                                                <h2 class="ProductName"></h2>
                                            </a>
                                        </div>
                                        <div class="product_price">
                                            <p class="PriceBeforeDiscount format-money" style="color:#ff0000;text-decoration:line-through;"></p>
                                            <p class="ProductPrice format-money"></p>
                                        </div>
                                        <div class="col-md-5 nopad">
                                            <div class="product_details_qty">
                                                <div class="quantity-area">
                                                    <div class="cart-quantity cart-plus-minus">
                                                        <input type="text" class="qty cart-plus-minus-box" value="1" name="quantity" placeholder="QTY" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 product-cart-area-list nopad">
                                            <div class="btn-add-to-cart cart-btn add-to-cart hidden">
                                                <button>Add to Cart</button>
                                            </div>
                                            <br /><br />
                                            <div class="add-to-cart-info hidden">
                                                <p>Please login / Register to start with this product</p>
                                                <a class="btn btn-default btn-black" style="float:left;" href="/Authentication">Login / Register</a>
                                            </div>
                                            <div class="add-to-cart-exist hidden">
                                                <p>You Have an Active package on this product, for further info check your package page</p>
                                                <a class="btn btn-default btn-black" style="float:left;" href="/Package">Package</a>
                                            </div>
                                        </div>
                                        <div class="col-md-12 product-cart-area-list nopad infoship">
                                            <div class="background-info"></div>
                                        </div>
                                        <div class="col-md-12 product-cart-area-list nopad infoship">
                                            <div class="col-md-6 alignleft nomarg nopad">
                                                <%--<p>Free Shipping & Returns<br />
                                                    Orders over 500.000 IDR</p>--%>
                                            </div>
                                            <div class="col-md-6 nomarg nopad">
                                                <%--<a class="alignright" href="#">See details</a>--%>
                                            </div>
                                        </div>
                                        <form method="POST" />
                                        <div class="product_color_size hidden">
                                            <div class="col-md-12 col-xs-12 nopad">
                                                <h4>Avalaible Colour</h4>
                                                <div class="col-md-12 nopad bungcolor color-list">
                                                    <%--                                                    <div class="col-md-4 col-xs-4 nopad">
                                                        <div class="selectColor"></div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4 nopad">
                                                        <div class="selectColor"></div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4 nopad">
                                                        <div class="selectColor"></div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4 nopad">
                                                        <div class="selectColor"></div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4 nopad">
                                                        <div class="selectColor"></div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-4 nopad">
                                                        <div class="selectColor"></div>
                                                    </div>--%>
                                                </div>
                                            </div>

                                            <div class="single-option sizes hidden">
                                                <label for="name">Size :</label>
                                                <div class="select-size" id="SelectSize">
                                                </div>
                                            </div>
                                        </div>
                                        </form>
                                        <div class="product_social_lists">
                                            <div class="product_share_title">
                                                <h4></h4>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" id="headingOne">
                                                    <h4 class="panel-title">
                                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">Product Info <i class="fa fa-plus"></i>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                                    <div class="panel-body">
                                                        <div class="service-acc-text Description">
                                                        </div>
                                                        <br />
                                                        <div class="Note">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="review_tab">
                                    <h3>Folding Instruction <a href="#" class="playvid">Play the video<i class="fa fa-play"></i></a></h3>
                                    <div class="service-acc-text ShortDescription">
                                    </div>
                                    <%--<img src="./images/details_product/gifprod.jpg" class="img-responsive" />--%>
                                    <!--<div class="review_feature_item_filter">
                                        <ul>
                                            <li class="active">
                                                <a href="#rev_one" data-toggle="tab" aria-controls="rev_one">
                                                    Description
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#rev_two" data-toggle="tab" aria-controls="rev_two">
                                                    Review
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#rev_three" data-toggle="tab" aria-controls="rev_three">
                                                    Add Tag
                                                </a>
                                            </li>
                                        </ul>
                                    </div>-->
                                    <!--<div class="tab-content">
                                        <div role="tabpanel" class="tab-pane fade in active" id="rev_one">
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Delectus ipsam nostrum veritatis soluta perspiciatis! Sequi libero pariatur dolore nobis saepe modi officiis ipsum atque. Officia quibusdam quis nisi dignissimos reprehenderit quas quod unde esse dolorem neque debitis numquam illum quaerat, distinctio libero repellat incidunt commodi? Aut reprehenderit ullam error placeat.</p>
                                        </div>
                                        <div role="tabpanel" class="tab-pane fade in" id="rev_two">
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Delectus ipsam nostrum veritatis soluta perspiciatis! Sequi libero pariatur dolore nobis saepe modi officiis ipsum atque. Officia quibusdam quis nisi dignissimos reprehenderit quas quod unde esse dolorem neque debitis numquam illum quaerat, distinctio libero repellat incidunt commodi? Aut reprehenderit ullam error placeat.</p>
                                        </div>
                                        <div role="tabpanel" class="tab-pane fade in" id="rev_three">
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Delectus ipsam nostrum veritatis soluta perspiciatis! Sequi libero pariatur dolore nobis saepe modi officiis ipsum atque. Officia quibusdam quis nisi dignissimos reprehenderit quas quod unde esse dolorem neque debitis numquam illum quaerat, distinctio libero repellat incidunt commodi? Aut reprehenderit ullam error placeat.</p>
                                        </div>
                                    </div>-->
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="upsell_title">
                                <h3>Related items</h3>
                            </div>
                            <div class="related-products">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Product List Area End-->
    <hr class="margin-hr" style="margin:0 auto;padding-bottom:32px;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/js/raxus-slider.min.js"></script>
    <script src="/assets/frontend/js/jquery.zoom.js" type="text/javascript"></script>
    <script src="/assets/frontend/scripts/ProductDetail.js?v=1.2"></script>
</asp:Content>

