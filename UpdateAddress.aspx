<%@ Page Title="Update Your Address - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="UpdateAddress.aspx.cs" Inherits="UpdateAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/select2/select2.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="HiddenIDAddress" />
    <input type="hidden" id="IDCountry" name="IDCountry" />
    <input type="hidden" id="IDProvince" name="IDProvince" />
    <input type="hidden" id="IDCity" name="IDCity" />
    <input type="hidden" id="IDDistrict" name="IDDistrict" />
    <div class="product_list_area section_padding checkoutpage">
        <section class="product-list wow fadeInUp">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12 title-box thankstit">
                        <h2>UPDATE ADDRESS</h2>
                        <h5>UPDATE YOUR ADDRESS ACCOUNT</h5>
                    </div>
                    <div class="col-md-12 titik">
                        <div id="bootstrap_alert"></div>
                        <form id="form_address">
                            <div class="col-md-12 box-dnd">
                                <div class="col-md-12">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="control-label">People Name</label>
                                            <input type="text" class="form-control guten-input" name="PeopleName" maxlength="50" /><sup>*</sup>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="control-label">Telephone</label>
                                            <input type="text" class="form-control guten-input" name="Phone" /><sup>*</sup>
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
                                    <div class="col-md-5 hidden">
                                        <div class="form-group">
                                            <label class="control-label">Address 2</label>
                                            <input id="txtAddress2" type="text" class="form-control guten-input" name="Address2" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="control-label">Country</label><div class="clearfix"></div>
                                            <input type="hidden" id="Country" name="Country" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Province</label><div class="clearfix"></div>
                                            <input type="hidden" id="Province" name="Province" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">City</label><div class="clearfix"></div>
                                            <input type="hidden" id="City" name="City" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">District</label><div class="clearfix"></div>
                                            <input type="hidden" id="District" name="District" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="control-label">Zip / postal code</label>
                                            <input type="text" class="form-control guten-input" name="PostalCode" maxlength="10" /><sup>*</sup>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="control-label">Additional Info</label>
                                            <input type="text" id="txtAdditionalInformation" class="form-control guten-input" /><sup>*</sup>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 hidden">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label">Name of your delivery address</label>
                                            <input type="text" class="form-control guten-input" name="Name" maxlength="50" /><sup>*</sup>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 padpad">
                                    <button id="btnSubmit" type="submit" class="site-button-dark">SUBMIT</button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/global/plugins/select2/select2.min.js"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script src="assets/frontend/scripts/update-address.js?v=1.1"></script>
</asp:Content>

