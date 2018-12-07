<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Scheduling.aspx.cs" Inherits="Scheduling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Product List Area -->
    </header>
    <section id="slider" runat="server" class="slider-element slider-parallax full-screen dark" style="background: url(/assets/frontend/images/landing/static.jpg) center center no-repeat; background-size: cover">

        <div class="slider-parallax-inner">

            <div class="container-fluid vertical-middle clearfix">

                <div class="heading-block title-center nobottomborder">
                    <h1>
                        <asp:Literal ID="ltrHeaderTitle" runat="server"></asp:Literal></h1>
                    <span>
                        <asp:Literal ID="ltrHeaderDescription" runat="server"></asp:Literal></span>
                </div>

            </div>

        </div>

    </section>
    <div id="page-menu">

        <div id="page-menu-wrap">

            <div class="container clearfix">

                <div class="menu-title">Our <span>Features</span></div>

                <nav>
                    <ul>

                        <li><a href="Blog.aspx">
                            <div>Dashboard</div>
                        </a></li>
                        <li><a href="Blog.aspx">
                            <div>Plan</div>
                        </a></li>
                        <li><a href="Blog.aspx">
                            <div>Tasks</div>
                        </a></li>
                        <li><a href="Blog.aspx">
                            <div>Reports</div>
                        </a></li>
                        <li><a href="Blog.aspx">
                            <div>Timesheets</div>
                        </a></li>
                        <li><a href="Blog.aspx">
                            <div>Scheduling</div>
                        </a></li>
                        <li><a href="Blog.aspx">
                            <div>Integration & API</div>
                        </a></li>
                        <li><a href="Blog.aspx">
                            <div>Livechat</div>
                        </a></li>
                        <li><a href="Blog.aspx">
                            <div>Support</div>
                        </a></li>
                    </ul>
                </nav>

                <div id="page-submenu-trigger"><i class="icon-reorder"></i></div>

            </div>

        </div>

    </div>
    <!-- #page-menu end -->


    <section id="content">

        <div class="content-wrap">

            <div class="container clearfix">



                <div class="col_two_third topmargin nobottommargin">

                    <div style="position: relative;" data-height-xl="535" data-height-lg="442" data-height-md="338" data-height-sm="316" data-height-xs="201">
                        <asp:Image ID="imgDashboard" runat="server" />
                        <%--<img data-animate="fadeInLeft" src="/assets/frontend/images/landing/device1.png" alt="Mac" style="position: absolute; top: 0; left: 0;">
							<img data-animate="fadeInRight" data-delay="300" src="/assets/frontend/images/landing/device2.png" alt="iPad" style="position: absolute; top: 0; left: 0;">
							<img data-animate="fadeInUp" data-delay="600" src="/assets/frontend/images/landing/device3.png" alt="iPhone" style="position: absolute; top: 0; left: 0;">--%>
                    </div>

                </div>

                <div class="col_one_third topmargin nobottommargin col_last">

                    <h3>
                        <asp:Literal ID="ltrDashboardTitle" runat="server"></asp:Literal></h3>

                    <p>
                        <asp:Literal ID="ltrDashboardShortDescription" runat="server"></asp:Literal>
                    </p>

                    <div class="divider divider-short"><i class="icon-circle"></i></div>

                    <asp:Literal ID="ltrDashboardDescription" runat="server"></asp:Literal>

                </div>

                <div class="clear"></div>

                <div class="divider divider-short divider-center"><i class="icon-circle"></i></div>

                <div id="section-features" class="heading-block title-center page-section">
                    <h2>
                        <asp:Literal ID="ltrFeaturePointTitle" runat="server"></asp:Literal></h2>
                    <span>
                        <asp:Literal ID="ltrFeaturePointDescription" runat="server"></asp:Literal></span>
                </div>

                <div class="feature-point">
                    <asp:ListView ID="lvFeaturePoint" runat="server">
                        <ItemTemplate>
                            <div class="col_one_third">
                                <div class="feature-box fbox-plain">
                                    <div class="fbox-icon" data-animate="bounceIn">
                                        <a href="#">
                                            <img src="assets/images/post/<%# Eval("MediaUrl") %>" alt="Responsive Layout"></a>
                                    </div>
                                    <h3><%# Eval("Title") %></h3>
                                    <p><%# Eval("Description") %></p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>



                <%--<div class="col_one_third">
						<div class="feature-box fbox-plain">
							<div class="fbox-icon" data-animate="bounceIn" data-delay="200">
								<a href="#"><img src="assets/frontend/images/icons/features/retina.png" alt="Retina Graphics"></a>
							</div>
							<h3>Schedule Tasks</h3>
							<p>Simply drag the task bars on the Gantt to quickly adjust dates.</p>
						</div>
					</div>

					<div class="col_one_third col_last">
						<div class="feature-box fbox-plain">
							<div class="fbox-icon" data-animate="bounceIn" data-delay="400">
								<a href="#"><img src="assets/frontend/images/icons/features/performance.png" alt="Powerful Performance"></a>
							</div>
							<h3>Task Lists</h3>
							<p>Your team always knows what to work on with personal project task lists.</p>
						</div>
					</div>

					<div class="clear"></div>

					<div class="col_one_third">
						<div class="feature-box fbox-plain">
							<div class="fbox-icon" data-animate="bounceIn" data-delay="600">
								<a href="#"><img src="assets/frontend/images/icons/features/flag.png" alt="Responsive Layout"></a>
							</div>
							<h3>Manage Teams</h3>
							<p>Assign tasks to your team by availability and workload..</p>
						</div>
					</div>

					<div class="col_one_third">
						<div class="feature-box fbox-plain">
							<div class="fbox-icon" data-animate="bounceIn" data-delay="800">
								<a href="#"><img src="assets/frontend/images/icons/features/tick.png" alt="Retina Graphics"></a>
							</div>
							<h3>Timesheets</h3>
							<p>Change your Website's Primary Scheme instantly by simply adding the dark class to the body.</p>
						</div>
					</div>

					<div class="col_one_third col_last">
						<div class="feature-box fbox-plain">
							<div class="fbox-icon" data-animate="bounceIn" data-delay="1000">
								<a href="#"><img src="assets/frontend/images/icons/features/tools.png" alt="Powerful Performance"></a>
							</div>
							<h3>Import & Export</h3>
							<p>Use any Font you like from Google Web Fonts, Typekit or other Web Fonts. They will blend in perfectly.</p>
						</div>
					</div>

					<div class="clear"></div>

					<div class="col_one_third">
						<div class="feature-box fbox-plain">
							<div class="fbox-icon" data-animate="bounceIn" data-delay="1200">
								<a href="#"><img src="assets/frontend/images/icons/features/map.png" alt="Responsive Layout"></a>
							</div>
							<h3>Collaborate</h3>
							<p>Powerful Layout with Responsive functionality that can be adapted to any screen size. Resize browser to view.</p>
						</div>
					</div>

					<div class="col_one_third">
						<div class="feature-box fbox-plain">
							<div class="fbox-icon" data-animate="bounceIn" data-delay="1400">
								<a href="#"><img src="assets/frontend/images/icons/features/seo.png" alt="Retina Graphics"></a>
							</div>
							<h3>Share Online</h3>
							<p>Looks beautiful &amp; ultra-sharp on Retina Screen Displays. Retina Icons, Fonts &amp; all others graphics are optimized.</p>
						</div>
					</div>

					<div class="col_one_third col_last">
						<div class="feature-box fbox-plain">
							<div class="fbox-icon" data-animate="bounceIn" data-delay="1600">
								<a href="#"><img src="assets/frontend/images/icons/features/support.png" alt="Powerful Performance"></a>
							</div>
							<h3>Powerful Performance</h3>
							<p>Canvas includes tons of optimized code that are completely customizable and deliver unmatched fast performance.</p>
						</div>
					</div>

					<div class="clear"></div>--%>

                <div class="divider divider-short divider-center"><i class="icon-circle"></i></div>





                <div class="divider divider-short divider-center"><i class="icon-circle"></i></div>

                <div id="section-faqs" class="heading-block title-center page-section">
                    <h2>
                        <asp:Literal ID="ltrFaqTitle" runat="server"></asp:Literal></h2>
                    <span>ltrFaqDescription</span>
                </div>



                <div class="nobottommargin">
                    <asp:Literal ID="ltrFaq" runat="server"></asp:Literal>

                    <div class="line"></div>
                </div>

                <div class="divider divider-short divider-center"><i class="icon-circle"></i></div>


            </div>
    </section>
    <!-- #content end -->
    <%--<div class="full_blog_area section_padding padding-bottom-70">
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
    <hr class="margin-hr" style="margin: 0 auto; padding-bottom: 32px;" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

