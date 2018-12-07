<%@ Page Title="About - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   	</header><!-- #header end -->

		<!-- Page Title
		============================================= -->
		<%--<section runat="server" id="AboutHeaderImage" class="page-title-parallax page-title-dark" style="padding: 250px 0; background-image: url('/assets/frontend/images/about/parallax.jpg'); background-size: cover; background-position: center center;" data-bottom-top="background-position:0px 400px;" data-top-bottom="background-position:0px -500px;">

			<div class="container clearfix">
				<h1>
                    <asp:Literal ID="ltrAboutHeaderTitle" runat="server"></asp:Literal></h1>
				<span><asp:Literal ID="ltrAboutHeaderDescription" runat="server"></asp:Literal></span>
				
			</div>

		</section><!-- #page-title end -->--%>

    	<section id="content">

			<div class="content-wrap">

				<div class="container clearfix">

					<div class="col_full">

						<div class="heading-block center nobottomborder">
							<h2><asp:Literal ID="ltrAboutUsTitle" runat="server"></asp:Literal></h2>
							<span><asp:Literal ID="ltrAboutUsDescription" runat="server"></asp:Literal></span>
						</div>

						<div class="fslider" data-pagi="false" data-animation="fade">
							<div class="flexslider">
								<div class="slider-wrap">
                                    <asp:ListView ID="lvAboutSlider" runat="server">
                                        <ItemTemplate>
                                            <div class="slide"><a href="#"><img src="/assets/images/post/<%# Eval("MediaUrl") %>" alt="About Image"></a></div>
                                        </ItemTemplate>
                                    </asp:ListView>
									
<%--									<div class="slide"><a href="#"><img src="/assets/frontend/images/about/5.jpg" alt="About Image"></a></div>
									<div class="slide"><a href="#"><img src="/assets/frontend/images/about/6.jpg" alt="About Image"></a></div>
									<div class="slide"><a href="#"><img src="/assets/frontend/images/about/7.jpg" alt="About Image"></a></div>--%>
								</div>
							</div>
						</div>

					</div>


				</div>

				

				<div class="row common-height clearfix">

					<div class="col-md-7 col-padding" runat="server" id="VisionImage" style="background: url('/assets/frontend/images/team/3.jpg') center center no-repeat; background-size: cover;"></div>

					<div class="col-md-5 col-padding">
						<div>
							<div class="heading-block">
								<span class="before-heading color">
                                    <asp:Literal ID="ltrVisionTitle" runat="server"></asp:Literal></span>
								<h3><asp:Literal ID="ltrVisionShortDescription" runat="server"></asp:Literal></h3>
							</div>

							<div class="row clearfix">

								<div class="col-lg-12">
									<p><asp:Literal ID="ltrVisionDescription" runat="server"></asp:Literal></p>
									
									
								</div>

								

							</div>

						</div>
					</div>

				</div>
                <div class="divider"><i class="icon-circle"></i></div>
				<div class="row common-height bottommargin-lg clearfix">

					<div class="col-md-5 col-padding" style="background-color: #F5F5F5;">
						<div>
							<div class="heading-block">
								<span class="before-heading color"><asp:Literal ID="ltrMissionTitle" runat="server"></asp:Literal></span>
								<h3><asp:Literal ID="ltrMissionShortDescription" runat="server"></asp:Literal></h3>
							</div>

							<div class="row clearfix">

								<div class="col-lg-12">
									<p><asp:Literal ID="ltrMissionDescription" runat="server"></asp:Literal></p>
								
								</div>

								

							</div>

						</div>
					</div>

					<div class="col-md-7 col-padding" runat="server" id="MissionImage" style="background: url('/assets/frontend/images/team/8.jpg') center center no-repeat; background-size: cover;"></div>

				</div>

				

					<div class="col_full clearfix">

						<h3>Gallery</h3>

						<div class="masonry-thumbs grid-4" data-big="4" data-lightbox="gallery">
                            <asp:ListView ID="lvGallery" runat="server">
                                <ItemTemplate>
                                    <a href="/assets/images/post/<%# Eval("MediaUrl") %>" data-lightbox="gallery-item"><img class="image_fade" src="/assets/images/post/<%# Eval("MediaUrl") %>" alt="Gallery Thumb 1"></a>
                                </ItemTemplate>
                            </asp:ListView>
						</div>

					</div>

			</div>

		</section><!-- #content end -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

