<%@ Page Title="Products - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/assets/frontend/css/components/pricing-table.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="HiddenActiveFilter" />
      </header>
    	<section id="page-title">

			<div class="container clearfix">
				<h1>Pricing</h1>
				<span>Start selling your Subscription Plans attractively</span>
				<ol class="breadcrumb">
					<li class="breadcrumb-item"><a href="#">Pay Yearly</a></li>
					<li class="breadcrumb-item"><a href="#">Add-Ons</a></li>
				</ol>
			</div>

		</section><!-- #page-title end -->
	<section id="content">

			<div class="content-wrap">

					<section class="section pricing-section header-stick dark nomargin" style="background-color: #333;">

						<div class="container clearfix">
							<h2 class="pricing-section--title center">TOMPS ID</h2>
							<div id="ProductList" class="pricing pricing--sonam">
								<%--<div class="pricing--item">
									<h3 class="pricing--title">Startup</h3>
									<div class="pricing--price"><span class="pricing--currency">$</span>9.90</div>
									<p class="pricing--sentence">Small business solution</p>
									<ul class="pricing--feature-list">
										<li class="pricing--feature">Unlimited calls</li>
										<li class="pricing--feature">Free hosting</li>
										<li class="pricing--feature">40MB of storage space</li>
									</ul>
									<button class="pricing--action">Choose plan</button>
								</div>
								<div class="pricing--item">
									<h3 class="pricing--title">Standard</h3>
									<div class="pricing--price"><span class="pricing--currency">$</span>29,90</div>
									<p class="pricing--sentence">Medium business solution</p>
									<ul class="pricing--feature-list">
										<li class="pricing--feature">Unlimited calls</li>
										<li class="pricing--feature">Free hosting</li>
										<li class="pricing--feature">10 hours of support</li>
										<li class="pricing--feature">Social media integration</li>
										<li class="pricing--feature">1GB of storage space</li>
									</ul>
									<button class="pricing--action">Choose plan</button>
								</div>
								<div class="pricing--item">
									<h3 class="pricing--title">Professional</h3>
									<div class="pricing--price"><span class="pricing--currency">$</span>59,90</div>
									<p class="pricing--sentence">Gigantic business solution</p>
									<ul class="pricing--feature-list">
										<li class="pricing--feature">Unlimited calls</li>
										<li class="pricing--feature">Free hosting</li>
										<li class="pricing--feature">Unlimited hours of support</li>
										<li class="pricing--feature">Social media integration</li>
										<li class="pricing--feature">Anaylitcs integration</li>
										<li class="pricing--feature">Unlimited storage space</li>
									</ul>
									<button class="pricing--action">Choose plan</button>
								</div>--%>
							</div>
						</div>
					</section>

                <section class="section pricing-section nomargin" style="background-color: #FFF;">
						<div class="container clearfix">
							<div class="fancy-title title-dotted-border title-center">
						<h3>Comparison Table</h3>
					</div>

					<table class="table table-hover table-comparison nobottommargin">
					  <thead>
						<tr>
						  <th>&nbsp;</th>
						  <th>Starter</th>
						  <th>Professional</th>
						  <th>Business</th>
						</tr>
					  </thead>
					  <tbody>
						<tr>
						  <td>Space</td>
						  <td>1 GB</td>
						  <td>5 GB</td>
						  <td>20 GB</td>
						</tr>
						<tr>
						  <td>Bandwidth</td>
						  <td>10 GB</td>
						  <td>100 GB</td>
						  <td>500 GB</td>
						</tr>
						<tr>
						  <td>Email Accounts</td>
						  <td>100</td>
						  <td>1000</td>
						  <td>5000</td>
						</tr>
						<tr>
						  <td>MySQL Accounts</td>
						  <td>100</td>
						  <td>1000</td>
						  <td>5000</td>
						</tr>
						<tr>
						  <td>SSH Access</td>
						  <td><i class="icon-remove"></i></td>
						  <td><i class="icon-remove"></i></td>
						  <td><i class="icon-ok"></i></td>
						</tr>
						<tr>
						  <td>SMTP Management</td>
						  <td><i class="icon-remove"></i></td>
						  <td><i class="icon-remove"></i></td>
						  <td><i class="icon-ok"></i></td>
						</tr>
						<tr>
						  <td>FTP Access</td>
						  <td><i class="icon-remove"></i></td>
						  <td><i class="icon-ok"></i></td>
						  <td><i class="icon-ok"></i></td>
						</tr>
						<tr>
						  <td>Google Adwords Credit</td>
						  <td><i class="icon-remove"></i></td>
						  <td><i class="icon-ok"></i></td>
						  <td><i class="icon-ok"></i></td>
						</tr>
						<tr>
						  <td>Monthly Price</td>
						  <td>Free</td>
						  <td>$9.99</td>
						  <td>$19.99</td>
						</tr>
						<tr>
						  <td>Quaterly Price</td>
						  <td>Free</td>
						  <td>$19.99</td>
						  <td>$49.99</td>
						</tr>
						<tr>
						  <td>Yearly Price</td>
						  <td>Free</td>
						  <td>$69.99</td>
						  <td>$149.99</td>
						</tr>
						<tr>
						  <td>&nbsp;</td>
						  <td><a href="#" class="btn btn-secondary">Sign Up</a></td>
						  <td><a href="#" class="btn btn-secondary">Sign Up</a></td>
						  <td><a href="#" class="btn btn-secondary">Sign Up</a></td>
						</tr>
					  </tbody>
					</table>
						</div>
					</section>
				</div>
        
		
		</section><!-- #content end -->
    <!-- New Collection Women Are End -->
    <!--Product List Area End-->
    <hr class="margin-hr" style="margin:0 auto;padding-bottom:32px;" />
    <!--Product List Area End-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
   
    <script src="<%= Page.ResolveUrl("/assets/frontend/scripts/Product.js?v=1.1.2") %>"></script>
</asp:Content>

