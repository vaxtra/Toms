<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!-- Google Map Area Start -->
    <div class="goolge_map_area section_padding">
        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3960.689284120273!2d107.62358041427716!3d-6.927694194994594!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x2e68e702ca78f3d9%3A0xada143ef0420d48e!2sNIION!5e0!3m2!1sen!2sid!4v1523009563490" width="100%" height="450" frameborder="0" style="border:0" allowfullscreen></iframe>
    </div>
<!-- Google Map Area End -->
<!-- Contact Area Start -->
<section class="contact_area section_padding">
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
</section>
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

