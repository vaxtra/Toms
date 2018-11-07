<%@ Page Title="NIION INDONESIA | Register" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/select2/select2.css" rel="stylesheet" />
    <link href="/assets/global/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="product_list_area section_padding checkoutpage">
        <section class="product-list wow fadeInUp">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12 title-box">
                        <h2>REGISTER</h2>
                        <h5>CREATE NEW ACCOUNT</h5>
                    </div>
                </div>
                <div class="col-md-12 titik">
                    
                    <form id="FormReg">
                        <div class="col-md-12 box-dnd">
                            <div id="bootstrap_alert"></div>
                            <div class="col-md-12">
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
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">First Name</label>
                                        <input type="text" class="form-control guten-input" name="FirstName" maxlength="25" /><sup>*</sup>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Last Name</label>
                                        <input type="text" class="form-control guten-input" name="LastName" maxlength="25" /><sup>*</sup>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                        <input id="tbEmailSignUp" type="text" class="form-control guten-input" name="Email" maxlength="50" /><sup>*</sup>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Your Password</label>
                                        <input id="password" type="password" class="form-control guten-input" name="Password" /><sup>*</sup>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Retype Your Password</label>
                                        <input type="password" class="form-control guten-input" name="RetypePassword" /><sup>*</sup>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Mobile Phone / Phone</label>
                                        <input type="text" class="form-control guten-input" name="PhoneNumber" maxlength="15" /><sup>*</sup>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">Date of Birth</label>
                                        <div class="form-group">
                                            <input type="text" class="form-control datepicker" id="txtBirthdate" placeholder="" name="Birthdate" />
                                            <sup style="margin: 0">*</sup>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 title-box">
                            <h2>DELIVERY ADDRESS</h2>
                        </div>
                        <div class="col-md-12 box-dnd">
                            <div class="col-md-12">
                                <div class="col-md-6 hidden">
                                    <div class="form-group">
                                        <label class="control-label">Name of your delivery address</label>
                                        <input type="text" class="form-control guten-input" name="AddressName" maxlength="50" placeholder="ex: Kantor, Rumah" /><sup>*</sup>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">Address 1</label>
                                        <input id="txtAddress" type="text" class="form-control guten-input" name="Address" /><sup>*</sup>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-5 hidden">
                                    <div class="form-group">
                                        <label class="control-label">Address 2</label>
                                        <input id="txtAddress2" type="text" class="form-control guten-input" name="Address2" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">Country</label><br />
                                        <input type="hidden" id="Country" name="Country" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">Province</label>
                                        <input type="hidden" id="Province" name="Province" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">City</label>
                                        <input type="hidden" id="City" name="City" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">District</label>
                                        <input type="hidden" id="District" name="District" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">Zip / postal code</label>
                                        <input type="text" class="form-control guten-input" name="PostalCode" maxlength="10" /><sup>*</sup>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">
                                            <input type="checkbox" name="Newsletter" id="cbNewsletter" />
                                            Sign up for newsletter for more information about our store & promo</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 padpad">
                                <button id="btnSubmit" type="submit" class="site-button-dark margtop15">SUBMIT</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </section>
    </div>
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

