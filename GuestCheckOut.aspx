<%@ Page Title="Guest Checkout | WIT COMMERCE" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="GuestCheckOut.aspx.cs" Inherits="GuestCheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/select2/select2.css" rel="stylesheet" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="product-list wow fadeInUp">
        <div class="container">
            <div class="row register">
                <div class="col-xs-12 title-box">
                    <h2>GUEST CHECK OUT</h2>
                </div>
                <form id="FormReg">
                    <div class="col-md-12">
                        <div id="bootstrap_alert"></div>
                    </div>
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
                                    <input type="text" class="form-control asuwww guten-input" name="FirstName" /><sup>*</sup>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Last Name</label>
                                    <input type="text" class="form-control asuwww guten-input" name="LastName" /><sup>*</sup>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Email</label>
                                    <input type="text" class="form-control asuwww guten-input" name="Email" /><sup>*</sup>
                                </div>
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Mobile Phone / Phone</label>
                                    <input type="text" class="form-control asuwww guten-input" name="PhoneNumber" maxlength="15" /><sup>*</sup>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Date of Birth</label>
                                    <input type="text" id="txtBirthdate" class="form-control asuwww guten-input datepicker" /><sup>*</sup>
                                </div>
                            </div>
                        </div>
                    <div class="col-md-12">
                        <hr class="hr-dot" style="margin-top: 20px;" />
                        <h3 class="text-center reg">DELIVERY ADDRESS</h3>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-5">
                            <div class="form-group">
                                <label class="control-label">Name of your delivery address</label>
                                <input type="text" class="form-control guten-input" name="AddressName" maxlength="50" /><sup>*</sup>
                            </div>
                        </div>
                    </div>

                        <div class="col-md-12">
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label class="control-label">Address 1</label>
                                    <input id="txtAddress" type="text" class="form-control guten-input" name="Address" /><sup>*</sup>
                                </div>
                            </div>
                            <div class="col-md-5" style="visibility: hidden;">
                                <div class="form-group">
                                    <label class="control-label">Address 2</label>
                                    <input id="txtAddress2" type="text" class="form-control guten-input" name="Address2" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label">Country</label><br />
                                    <input type="hidden" id="Country" name="Country" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label">Province</label>
                                    <input type="hidden" id="Province" name="Province" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">City</label><br />
                                    <input type="hidden" id="City" name="City" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">District</label><br />
                                    <input type="hidden" id="District" name="District" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Zip / postal code</label>
                                    <input type="text" class="form-control guten-input" name="PostalCode" maxlength="10" /><sup>*</sup>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 padpad">
                            <button id="btnSubmit" type="submit" class="site-button-dark margtop15">SUBMIT</button>
                        </div>

                </form>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/global/plugins/select2/select2.min.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="/assets/frontend/scripts/guest-checkout.js"></script>
    <script>
        $(".datepicker").datepicker({
            format: "dd-mm-yyyy",
            todayBtn: "linked",
            autoclose: true,
            todayHighlight: true
        });
    </script>
</asp:Content>

