<%@ Page Title="NIION INDONESIA | Register" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/select2/select2.css" rel="stylesheet" />
    <link href="/assets/global/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="page-title">

			<div class="container clearfix">
				<h1>My Account</h1>
				<ol class="breadcrumb">
					<li class="breadcrumb-item"><a href="#">Home</a></li>
					<li class="breadcrumb-item"><a href="#">Pages</a></li>
					<li class="breadcrumb-item active" aria-current="page">Login</li>
				</ol>
			</div>

		</section><!-- #page-title end -->

		<!-- Content
		============================================= -->
		<section id="content">

			<div class="content-wrap">

				<div class="container clearfix">

					<div class="accordion accordion-lg divcenter nobottommargin clearfix" style="max-width: 550px;">

						<div class="acctitle"><i class="acc-closed icon-lock3"></i><i class="acc-open icon-unlock"></i>Login to your Account</div>
						<div class="acc_content clearfix">
							 <form id="FormReg">
                        <div class="col-md-12 box-dnd">
                            <div id="bootstrap_alert"></div>
                            <div class="col-md-12">
                               
                                    <div class="form-group">
                                        <label class="control-label">Title</label>
                                        <div class="form-group">
                                            <input class="guten-registerTitle" type="radio" name="Gender" value="L" checked="checked" />
                                            Mr.
                           
                                            <input class="guten-registerTitle" type="radio" name="Gender" value="P" />
                                            Mrs. 
                                   
                                        </div>
                                    </div>
                                </div>
                          
                          
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">First Name</label>
                                        <input type="text" class="form-control guten-input" name="FirstName" maxlength="25" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Last Name</label>
                                        <input type="text" class="form-control guten-input" name="LastName" maxlength="25" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                        <input id="tbEmailSignUp" type="text" class="form-control guten-input" name="Email" maxlength="50" />
                                    </div>
                                </div>
                           
                          
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Your Password</label>
                                        <input id="password" type="password" class="form-control guten-input" name="Password" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Retype Your Password</label>
                                        <input type="password" class="form-control guten-input" name="RetypePassword" />
                                    </div>
                                </div>
                           
                          
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Mobile Phone / Phone</label>
                                        <input type="text" class="form-control guten-input" name="PhoneNumber" maxlength="15" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Date of Birth</label>
                                        <div class="form-group">
                                            <input type="text" class="form-control datepicker" id="txtBirthdate" placeholder="" name="Birthdate" />
                                            <sup style="margin: 0">*</sup>
                                        </div>
                                    </div>
                                </div>
                    
                        </div>
                        <div class="col-xs-12 title-box">
                            <h2>DELIVERY ADDRESS</h2>
                        </div>
                        <div class="col-md-12 box-dnd">
                            <div class="col-md-12">
                                <div class="col-md-12 hidden">
                                    <div class="form-group">
                                        <label class="control-label">Name of your delivery address</label>
                                        <input type="text" class="form-control guten-input" name="AddressName" maxlength="50" placeholder="ex: Kantor, Rumah" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Address 1</label>
                                        <input id="txtAddress" type="text" class="form-control guten-input" name="Address" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-12 hidden">
                                    <div class="form-group">
                                        <label class="control-label">Address 2</label>
                                        <input id="txtAddress2" type="text" class="form-control guten-input" name="Address2" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Country</label><br />
                                        <input type="hidden" id="Country" name="Country" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Province</label>
                                        <input type="hidden" id="Province" name="Province" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">City</label>
                                        <input type="hidden" id="City" name="City" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">District</label>
                                        <input type="hidden" id="District" name="District" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Zip / postal code</label>
                                        <input type="text" class="form-control guten-input" name="PostalCode" maxlength="10" /><sup>*</sup>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">
                                            <input type="checkbox" name="Newsletter" id="cbNewsletter" />
                                            Sign up for newsletter for more information about our store & promo</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 padpad">
                                <button id="btnSubmit" type="submit" class="button button-3d button-black nomargin">SUBMIT</button>
                            </div>
                        </div>
                    </form>
						</div>

						
					</div>

				</div>

			</div>

		</section><!-- #content end -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="/assets/global/plugins/select2/select2.min.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script src="assets/frontend/scripts/registration.js"></script>
    <script>
        $(".datepicker").datepicker({
            startDate: '01-01-1985',
            format: "dd-mm-yyyy",
            todayBtn: "linked",
            autoclose: true,
            todayHighlight: false
        });
    </script>
</asp:Content>

