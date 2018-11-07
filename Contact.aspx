<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!-- Google Map Area Start -->
    <div class="goolge_map_area section_padding">
        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d7348.986283585175!2d107.58154579781962!3d-6.886010465184368!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x2e68e6870f2957cb%3A0x5bdaf7433b63418a!2sWIT.+Indonesia!5e0!3m2!1sen!2sid!4v1449168664486" width="100%" height="850" frameborder="0" style="border:0" allowfullscreen></iframe>
    </div>
<!-- Google Map Area End -->

    <section id="content">

			<div class="content-wrap">

				<div class="container clearfix">

					<!-- Postcontent
					============================================= -->
					<div class="postcontent nobottommargin">

						<h3>Send us an Email</h3>

						<div class="contact-widget">

							<div class="contact-form-result"></div>

							<form class="nobottommargin" id="template-contactform" name="template-contactform" action="include/sendemail.php" method="post">

								<div class="form-process"></div>

								<div class="col_one_third">
									<label for="template-contactform-name">Name <small>*</small></label>
									<input type="text" id="template-contactform-name" name="template-contactform-name" value="" class="sm-form-control required" />
								</div>

								<div class="col_one_third">
									<label for="template-contactform-email">Email <small>*</small></label>
									<input type="email" id="template-contactform-email" name="template-contactform-email" value="" class="required email sm-form-control" />
								</div>

								<div class="col_one_third col_last">
									<label for="template-contactform-phone">Phone</label>
									<input type="text" id="template-contactform-phone" name="template-contactform-phone" value="" class="sm-form-control" />
								</div>

								<div class="clear"></div>

								<div class="col_two_third">
									<label for="template-contactform-subject">Subject <small>*</small></label>
									<input type="text" id="template-contactform-subject" name="template-contactform-subject" value="" class="required sm-form-control" />
								</div>

								<div class="col_one_third col_last">
									<label for="template-contactform-service">Services</label>
									<select id="template-contactform-service" name="template-contactform-service" class="sm-form-control">
										<option value="">-- Select One --</option>
										<option value="Wordpress">Wordpress</option>
										<option value="PHP / MySQL">PHP / MySQL</option>
										<option value="HTML5 / CSS3">HTML5 / CSS3</option>
										<option value="Graphic Design">Graphic Design</option>
									</select>
								</div>

								<div class="clear"></div>

								<div class="col_full">
									<label for="template-contactform-message">Message <small>*</small></label>
									<textarea class="required sm-form-control" id="template-contactform-message" name="template-contactform-message" rows="6" cols="30"></textarea>
								</div>

								<div class="col_full hidden">
									<input type="text" id="template-contactform-botcheck" name="template-contactform-botcheck" value="" class="sm-form-control" />
								</div>

								<div class="col_full">
									<button class="button button-3d nomargin" type="submit" id="template-contactform-submit" name="template-contactform-submit" value="submit">Send Message</button>
								</div>

							</form>
						</div>

					</div><!-- .postcontent end -->

					<!-- Sidebar
					============================================= -->
					<div class="sidebar col_last nobottommargin">

						<address>
										<strong>Headquarters:</strong><br>
										Jakdiva, LT 6 Menara Multimedia<br>
										JL Kebon Sirih, Jakarta Pusat<br>
									</address>
									<abbr title="Phone Number"><strong>Phone:</strong></abbr> (91) 8547 632521<br>
									<abbr title="Fax"><strong>Fax:</strong></abbr> (91) 11 4752 1433<br>
									<abbr title="Email Address"><strong>Email:</strong></abbr> hello@tomps.id

						<div class="widget noborder notoppadding">

							<div class="fslider customjs testimonial twitter-scroll twitter-feed" data-username="envato" data-count="3" data-animation="slide" data-arrows="false">
								<i class="i-plain i-small color icon-twitter nobottommargin" style="margin-right: 15px;"></i>
								<div class="flexslider" style="width: auto;">
									<div class="slider-wrap">
										<div class="slide"></div>
									</div>
								</div>
							</div>

						</div>

						<div class="widget noborder notoppadding">

							<a href="#" class="social-icon si-small si-dark si-facebook">
								<i class="icon-facebook"></i>
								<i class="icon-facebook"></i>
							</a>

							<a href="#" class="social-icon si-small si-dark si-twitter">
								<i class="icon-twitter"></i>
								<i class="icon-twitter"></i>
							</a>

							<a href="#" class="social-icon si-small si-dark si-dribbble">
								<i class="icon-dribbble"></i>
								<i class="icon-dribbble"></i>
							</a>

							<a href="#" class="social-icon si-small si-dark si-forrst">
								<i class="icon-forrst"></i>
								<i class="icon-forrst"></i>
							</a>

							<a href="#" class="social-icon si-small si-dark si-pinterest">
								<i class="icon-pinterest"></i>
								<i class="icon-pinterest"></i>
							</a>

							<a href="#" class="social-icon si-small si-dark si-gplus">
								<i class="icon-gplus"></i>
								<i class="icon-gplus"></i>
							</a>

						</div>

					</div><!-- .sidebar end -->

				</div>

			</div>

		</section><!-- #content end -->

<%--<section class="contact_area section_padding">
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-sm-12 col-xs-12">
                <div class="contact_title">
                    <h2>Leave A Message</h2>
                </div>
                <div class="contact_wrapper">
                    <form action="contact-us.html" />
                        <div class="contact_content fix">
                            <div class="contact_content_left">
                                <div class="contact_name">
                                    <input type="text" placeholder="Your Name*" />
                                </div>
                                <div class="contact_name">
                                    <input type="email" placeholder="Your Email*" />
                                </div>
                                <div class="contact_name">
                                    <input type="text" placeholder="Your Phone" />
                                </div>
                            </div>
                            <div class="contact_content_right">
                                <div class="contact_name">
                                    <textarea cols="30" rows="6" placeholder="Your Message*"></textarea>
                                </div>
                            </div>
                            <div class="send">
                                <button>Send Message</button>
                            </div>
                            <br /><br /><br />
                        </div>
                    </form>
                </div>
            </div>
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="contact_address_wrapper">
                     <div class="single_address_single fix">
                         <div class="single_contact_title floatleft">
                             <h2>Address:</h2>
                         </div>
                         <div class="single_contact_details floatright">
                             <p>Jl. Galunggung 1 No. 4, Lkr. Sel., Lengkong, Kota Bandung, Jawa Barat 40263</p>
                         </div>
                     </div>
                     <div class="single_address_single fix">
                         <div class="single_contact_title floatleft">
                             <h2>Phone:</h2>
                         </div>
                         <div class="single_contact_details floatright">
                             <p>(022) 73280572<br /></p>
                         </div>
                     </div>
                     <div class="single_address_single fix">
                         <div class="single_contact_title floatleft">
                             <h2>Email:</h2>
                         </div>
                         <div class="single_contact_details floatright">
                             <p>niion.id@gmail.com</p>
                         </div>
                     </div>
                </div>
            </div>
        </div>
    </div>
</section>--%>
<!-- Contact Area End -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" Runat="Server">
    <script src="assets/frontend/scripts/contactus.js"></script>
    <script>
        $(document).ready(function () {
            $(".leftmens").fadeOut();
            $(".logo").fadeOut();
            $(window).scroll(function () {
                if ($(window).scrollTop() > 85) {
                    $(".leftmens").fadeIn();
                    $(".logo").fadeIn();
                }
                else {
                    $(".leftmens").fadeOut();
                    $(".logo").fadeOut();
                }
            });
        });
    </script>
</asp:Content>

