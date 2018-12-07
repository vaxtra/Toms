<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="HiddenIDCategory" />
    </header><!-- #header end -->
    <section id="slider" class="slider-element slider-parallax swiper_wrapper full-screen clearfix">
        <div class="slider-parallax-inner">

            <div class="swiper-container swiper-parent">
                <div class="swiper-wrapper">
                    <asp:ListView ID="lvSlideshow" runat="server">
                        <ItemTemplate>
                            <div class="swiper-slide dark" style='background-image: url(/assets/images/post/<%# Eval("MediaUrl") %>');>
                                <div class="container clearfix">
                                    <div class="slider-caption slider-caption-left">
                                        <h2 data-caption-animate="fadeInUp"><%# Eval("Title") %></h2>
                                        <p class="d-none d-sm-block" data-caption-animate="fadeInUp" data-caption-delay="200"><%# Eval("Description") %></p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <div class="slider-arrow-left"><i class="icon-angle-left"></i></div>
                <div class="slider-arrow-right"><i class="icon-angle-right"></i></div>
            </div>

            <a href="#" data-scrollto="#content" data-offset="100" class="dark one-page-arrow"><i class="icon-angle-down infinite animated fadeInDown"></i></a>

        </div>
    </section>


    <!-- Content
		============================================= -->
    <section id="content">

        <div class="content-wrap">

            <div class="container clearfix">
                <div class="row clearfix">

                    <div class="col-xl-5">
                        <div class="heading-block topmargin">
                            <h1>
                                <asp:Label ID="lblHomeBlockTitle" runat="server" Text=""></asp:Label></h1>
                        </div>
                        <p class="lead">
                            <asp:Label ID="lblHomeBlockDescription" runat="server" Text=""></asp:Label>
                        </p>
                        <a href="#" runat="server" id="HomeBlockLink" class="button button-border button-dark button-rounded button-large noleftmargin topmargin-sm">Get Started</a>
                    </div>

                    <div class="col-xl-7">

                        <div style="position: relative; margin-bottom: -60px;" class="ohidden" data-height-xl="426" data-height-lg="567" data-height-md="470" data-height-md="287" data-height-xs="183">
                            <asp:Image ID="imgHomeBlock" runat="server" data-animate="fadeInUp" data-delay="10" alt="Chrome" />
                            <%--<img src="/assets/frontend/images/services/howitworks2.png" data-animate="fadeInUp" data-delay="10" alt="Chrome">--%>
                        </div>

                    </div>

                </div>
            </div>

            <div class="section nobottommargin">
                <div class="container clear-bottommargin clearfix">

                    <div class="row topmargin-sm clearfix">
                        <asp:ListView ID="lvServices" runat="server">
                            <ItemTemplate>
                                <div class="col-lg-4 bottommargin">
                                    <img src='/assets/images/post/<%# Eval("MediaUrl") %>' />
                                    <div class="heading-block nobottomborder" style="margin-bottom: 15px;">

                                        <h4><%# Eval("Title") %></h4>
                                    </div>
                                    <p>
                                        <%# Eval("Description") %>
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>

                        <%--<div class="col-lg-4 bottommargin">
                            <i class="i-plain color i-large icon-line2-screen-desktop inline-block" style="margin-bottom: 15px;"></i>
                            <div class="heading-block nobottomborder" style="margin-bottom: 15px;">

                                <h4>Material Stokist</h4>
                            </div>
                            <p>Manajemen penggunaan material setiap project oleh vendor</p>
                        </div>
                        <div class="col-lg-4 bottommargin">
                            <i class="i-plain color i-large icon-line2-screen-desktop inline-block" style="margin-bottom: 15px;"></i>
                            <div class="heading-block nobottomborder" style="margin-bottom: 15px;">

                                <h4>Dokumen Repository</h4>
                            </div>
                            <p>Manajemen evident data project</p>
                        </div>--%>
                    </div>

                </div>
            </div>

            <div class="container clearfix">

                <div class="heading-block topmargin-lg center">
                    <h2>More Feature </h2>

                </div>

                <div class="row bottommargin-sm">

                    <div class="col-lg-4 col-md-6 bottommargin">

                        <asp:ListView ID="lvFeatureLeft" runat="server">
                            <ItemTemplate>
                                <div class="feature-box fbox-right topmargin" data-animate="fadeIn">
                                    <div class="fbox-icon">
                                        <img src='/assets/images/post/<%# Eval("MediaUrl") %>' />
                                    </div>
                                    <h3><%# Eval("Title") %></h3>
                                    <p><%# Eval("Description") %></p>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>


                        <%--<div class="feature-box fbox-right topmargin" data-animate="fadeIn" data-delay="200">
                            <div class="fbox-icon">
                                <a href="Blog.aspx"><i class="icon-line-paper"></i></a>
                            </div>
                            <h3>Dashboard</h3>
                            <p>Lorem ipsum dolor sit amet</p>
                        </div>

                        <div class="feature-box fbox-right topmargin" data-animate="fadeIn" data-delay="400">
                            <div class="fbox-icon">
                                <a href="Blog.aspx"><i class="icon-line-layers"></i></a>
                            </div>
                            <h3>Plan</h3>
                            <p>Lorem ipsum dolor sit amet</p>
                        </div>
                        <div class="feature-box fbox-right topmargin" data-animate="fadeIn" data-delay="400">
                            <div class="fbox-icon">
                                <a href="Blog.aspx"><i class="icon-line-layers"></i></a>
                            </div>
                            <h3>Tasks</h3>
                            <p>Lorem ipsum dolor sit amet</p>
                        </div>--%>
                    </div>

                    <div class="col-lg-4 d-md-none d-lg-block bottommargin center">
                        <asp:Image ID="imgFeature" runat="server" />
                        <%--<img src="/assets/frontend/images/services/iphone7.png" alt="iphone 2">--%>
                    </div>

                    <div class="col-lg-4 col-md-6 bottommargin">

                        <asp:ListView ID="lvFeatureRight" runat="server">
                            <ItemTemplate>
                                <div class="feature-box topmargin" data-animate="fadeIn">
                                    <div class="fbox-icon">
                                        <img src='/assets/images/post/<%# Eval("MediaUrl") %>' />
                                    </div>
                                    <h3><%# Eval("Title") %></h3>
                                    <p><%# Eval("Description") %></p>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                </div>

            </div>
            <div class="section parallax dark nobottommargin" style="background-image: url('/assets/frontend/images/services/home-testi-bg.jpg'); padding: 100px 0;" data-bottom-top="background-position:0px 300px;" data-top-bottom="background-position:0px -300px;">

                <div class="heading-block center">
                    <h3>Testimonials</h3>
                </div>

                <div class="fslider testimonial testimonial-full" data-animation="fade" data-arrows="false">
                    <div class="flexslider">
                        <div class="slider-wrap">
                            <asp:ListView ID="lvTestimonial" runat="server">
                                <ItemTemplate>
                                    <div class="slide">
                                        <div class="testi-image">
                                            <a href="#">
                                                <img src="/assets/images/post/<%# Eval("MediaUrl") %>" alt="">
                                            </a>
                                        </div>
                                        <div class="testi-content">
                                            <p><%# Eval("Description") %></p>
                                            <div class="testi-meta">
                                                <%# Eval("Title") %>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>

                            <%--<div class="slide">
                                <div class="testi-image">
                                    <a href="#">
                                        <img src="/assets/frontend/images/testimonials/2.jpg" alt="Customer Testimonails"></a>
                                </div>
                                <div class="testi-content">
                                    <p>Merasa terbantu melalui aplikasi TOMP'S, Laporan project dapat sesuai dengan yang terjadi di lapangan (realtime)</p>
                                    <div class="testi-meta">
                                        Sandre Pakpahan
											<span>Supervisor PT.Jevan</span>
                                    </div>
                                </div>
                            </div>
                            <div class="slide">
                                <div class="testi-image">
                                    <a href="#">
                                        <img src="/assets/frontend/images/testimonials/1.jpg" alt="Customer Testimonails"></a>
                                </div>
                                <div class="testi-content">
                                    <p>Menghemat SDM, Doc tersimpan di TOMP'S, anytime anywehere monitor progress project mudah untuk mengatur anggaran sesauai dengan real project dapat menyusun planning project selanjutnya</p>
                                    <div class="testi-meta">
                                        Slamet Riyanto
											<span>GM Telkom Samarinda</span>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>

            </div>








            <div class="container clearfix">

                <div class="col_one_third bottommargin-sm center" style="margin-top: 25px;">
                    <asp:Image ID="imgTour" runat="server" />
                    <%--<img data-animate="fadeInLeft" src="/assets/frontend/images/services/imac.png" alt="Iphone">--%>
                </div>

                <div class="col_two_third bottommargin-sm col_last">

                    <div class="heading-block topmargin-sm">
                        <h3>
                            <asp:Label ID="lblTourTitle" runat="server" Text=""></asp:Label></h3>
                    </div>

                    <p>
                        <asp:Label ID="lblTourDescription" runat="server" Text=""></asp:Label>
                    </p>

                    <a href="#" runat="server" id="TourLink" class="button button-border button-dark button-rounded button-large noleftmargin topmargin-sm">Request Demo</a>

                </div>

            </div>





            <div class="container clearfix bottommargin-sm">

                <div id="oc-clients" class="owl-carousel image-carousel carousel-widget" data-margin="60" data-loop="true" data-nav="false" data-autoplay="5000" data-pagi="false" data-items-xs="2" data-items-sm="3" data-items-md="4" data-items-lg="5" data-items-xl="6">
                    <asp:ListView ID="lvClient" runat="server">
                        <ItemTemplate>
                            <div class="oc-item">
                                <a href="#">
                                    <img src="/assets/images/post/<%# Eval("MediaUrl") %>" alt="Clients"></a>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>

                </div>


            </div>
            <div class="row clearfix common-height">

                <div class="col-lg-6 center col-padding" style="background: url('/assets/frontend/images/services/main-bg.jpg') center center no-repeat; background-size: cover;">
                    <div>&nbsp;</div>
                </div>

                <div class="col-lg-6 center col-padding" style="background-color: #F5F5F5;">
                    <div>
                        <div class="heading-block nobottomborder">

                            <h3><asp:Label ID="lblSupportTitle" runat="server" Text=""></asp:Label></h3>
                        </div>

                        <div class="center bottommargin">
                            <a href="https://www.youtube.com/watch?v=B7Z6j7g17mA" data-lightbox="iframe" style="position: relative;">
                                <asp:Image ID="imgSupportRight" runat="server" />
                                <%--<img src="/assets/frontend/images/services/video.jpg" alt="Video">--%>
                                <span class="i-overlay nobg">
                                    <asp:Label ID="lblSupportDescription" runat="server" Text=""></asp:Label>
                                </span>
                            </a>
                        </div>
                        <p class="lead nobottommargin">
                            <asp:Label ID="lblSupportContactInfo" runat="server" Text=""></asp:Label>
                            <%--<address>
                                <strong>Headquarters:</strong><br>
                                Jakdiva, LT 6 Menara Multimedia<br>
                                JL Kebon Sirih, Jakarta Pusat<br>
                            </address>
                            <abbr title="Phone Number"><strong>Phone:</strong></abbr>
                            (91) 8547 632521<br>
                            <abbr title="Fax"><strong>Fax:</strong></abbr>
                            (91) 11 4752 1433<br>
                            <abbr title="Email Address"><strong>Email:</strong></abbr>
                            hello@tomps.id--%>
                        </p>
                    </div>
                </div>

            </div>

        </div>

    </section>
    <!-- #content end -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="assets/frontend/scripts/default.js?v=1.1.1"></script>

</asp:Content>

