<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="update.aspx.cs" Inherits="adminwitcommerce_catalog_manufacturer_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
    <link href="/assets/global/plugins/bootstrap-summernote/summernote.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Update Manufacturer
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="/adminwitcommerce/catalog">Catalog</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="default.aspx">Manufacturers</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Update Manufacturer</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <input type="hidden" id="HiddenIDManufacturer" />
    <div class="row">
        <div class="col-md-12">
            <form class="horizontal-form" id="form_manufacturer">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption"><i class="fa fa-edit"></i>Update Manufacturer</div>
                        <div class="actions">
                            <a href="./Default.aspx" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;Manufacturers</a>
                            <input type="checkbox" name="Active" checked="checked" class="make-switch form-control input" />
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-body">
                            <div id="bootstrap_alerts"></div>
                            <div id="alert" class="alert alert-danger display-none">
                                <button class="close" data-dismiss="alert"></button>
                                You have some form errors. Please check below.
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label class="control-label">Image</label>
                                        <div class="input-group">
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div class="fileinput-new thumbnail">
                                                    <img src="/assets/img/noimage.jpg" alt="" style="max-width: 300px;" id="imgLogo" />
                                                </div>
                                                <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 300px;">
                                                </div>
                                                <div>
                                                    <span class="btn blue blue-madison btn-block btn-file default">
                                                        <span class=" fileinput-new">Select image</span>
                                                        <span class="fileinput-exists">Change</span>
                                                        <input type="file" id="fuImage" name="fuImage" class="file" />
                                                    </span>
                                                    <a href="#" class="btn red btn-block default fileinput-exists" data-dismiss="fileinput">Remove
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group">
                                        <label class="control-label">Name<span class="required">*</span></label>
                                        <input type="text" name="Name" class="form-control input" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Phone</label>
                                        <input type="text" name="Phone" class="form-control input" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                        <input type="email" name="Email" class="form-control input" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">Address</label>
                                        <textarea name="Address" class="form-control input" rows="5"></textarea>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Description</label>
                                        <input type="text" name="Description" class="form-control input summernote-simple" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions right">
                            <a href="Default.aspx" class="btn default">Cancel</a>
                            <button type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-summernote/summernote.min.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/catalog/manufacturer/update.js"></script>
</asp:Content>

