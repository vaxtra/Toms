<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="adminwitcommerce_catalog_category_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
    <link href="/assets/global/plugins/select2/select2.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Update Category
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
    <li><a href="/adminwitcommerce/catalog/category">Category</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="javascript:;">Update</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <input type="hidden" id="HiddenIDCategory" />
    <input type="hidden" id="HiddenIDCategoryParent" />
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-book"></i>Update Category</div>
                    <div class="actions">
                        <a href="./Default.aspx" class="btn btn-default backLink btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;Back to List</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form class="horizontal-form" id="cat_update">
                        <div class="form-body">
                            <input type="hidden" id="hiddenIdCategoryParent" />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="alert alert-danger display-hide">
                                        <button class="close" data-close="alert"></button>
                                        You have some form errors. Please check below.
                                    </div>
                                    <div class="alert-success display-hide">
                                        <button class="close" data-close="alert"></button>
                                        Data submit succesfully
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label class="control-label">Image</label>
                                        <div class="input-group">
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div class="fileinput-new thumbnail">
                                                    <img src="/assets/img/noimage.jpg" alt="" style="max-width: 300px;" id="imgPreview" />
                                                </div>
                                                <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 300px;">
                                                </div>
                                                <div>
                                                    <span class="btn btn-block btn-file default">
                                                        <span class="fileinput-new">Select image</span>
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
                                    <div class="row">
                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <label class="control-label">Name <span class="required">*</span></label>
                                                <input type="text" class="form-control" id="tbName" name="Name" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-xs-3 col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Active</label>
                                                <div class="icon-group">
                                                    <input type="checkbox" id="cbActive" name="cbActive" class="make-switch" data-on="success" data-off="danger" checked="checked" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label">Description</label>
                                                <textarea id="tbDescription" name="tbDescription" class="form-control summernote" rows="5"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions right">
                                <a class="btn btn-default backLink" href="Default.aspx">Cancel</a>
                                <button type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script src="/assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/catalog/category/update.js"></script>
</asp:Content>

