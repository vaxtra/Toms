<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="adminwitcommerce_content_page_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/jstree/dist/themes/default/style.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/dropzone/css/dropzone.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/data-tables/DT_bootstrap.css" />
    <style type="text/css">
        .table-image {
        }

            .table-image thead tr th {
                text-align: center;
            }

            .table-image tbody tr td {
                text-align: center;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Page Content
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="#">Content</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="default.aspx">Page</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#" class="productName">Update Content</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <input type="hidden" id="HiddenIDPage" />
    <input type="hidden" id="HiddenIDPageMedia" />
    <div class="tabbable tabbable-custom boxless">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#product" data-toggle="tab">Page Data</a></li>
            <li class=""><a href="#categories" data-toggle="tab">Page Categories</a></li>
            <li class=""><a href="#images" data-toggle="tab">Images</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="product">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Page Data</div>
                    </div>
                    <div class="portlet-body form">
                        <form action="#" id="formPageData_Update" class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="bootstrap_alerts"></div>
                                        <div class="alert alert-danger display-hide">
                                            <button class="close" data-close="alert"></button>
                                            You have some form errors. Please check below.
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Page Title</label>
                                            <div class="input-group">
                                                <input type="text" id="Page_Title" name="pageTitle" class="form-control input" maxlength="25" placeholder="Product Name" />
                                                <div class="input-group-addon">
                                                    <i class="fa fa-tags"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Short Content</label>
                                            <input type="text" name="pageShortContent" id="Page_ShortContent" class="form-control input summernote" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Content</label>
                                            <input type="text" name="pageContent" id="Page_Content" class="form-control summernote input" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions right">
                                <a href="Default.aspx" class="btn default">Cancel</a>
                                <button type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="categories">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption"><i class="fa fa-book"></i>Page Categories</div>
                        <div class="actions">
                            <a href="#" class="btn insertCat btn-default btn-sm"><i class="fa fa-plus"></i>&nbsp;New Category </a>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div id="bootstrap_alerts_catpage"></div>
                        <table class="table table-striped table-bordered table-hover" id="dtPageCategory">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Name</th>
                                    <th>Date Inserted</th>
                                    <th>Last Update</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
                <form id="cat_insert">
                    <div class="portlet iuCat" style="display: none;">
                        <div class="portlet-title">
                            <div class="caption">New Category</div>
                        </div>
                        <div class="portlet-body form">
                            <div class="horizontal-form">
                                <div class="form-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="bootstrap_alerts_catdef"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-7">
                                            <div class="form-group">
                                                <label class="control-label">Name <span class="required">*</span></label>
                                                <input type="text" name="Name" id="tbName" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <div class="form-group">
                                                <label class="control-label">Description</label>
                                                <textarea name="Description" id="tbDescription" class="form-control summernote"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-actions right">
                                    <a href="Default.aspx" class="btn default">Cancel</a>
                                    <button id="submitCategory" type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                                </div>

                            </div>
                        </div>
                    </div>
                </form>
                <form id="cat_update">
                    <input type="hidden" id="HiddenIDPageCategory" />
                    <div class="portlet uCat" style="display: none;">
                        <div class="portlet-title">
                            <div class="caption">Update Category</div>
                        </div>
                        <div class="portlet-body form">
                            <div class="horizontal-form">
                                <div class="form-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="bootstrap_alerts_ucatdef"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-7">
                                            <div class="form-group">
                                                <label class="control-label">Name <span class="required">*</span></label>
                                                <input type="text" name="Name" id="tbUpdateName" class="form-control input" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <div class="form-group">
                                                <label class="control-label">Description</label>
                                                <textarea name="Description" id="tbUpdateDescription" class="form-control summernote input"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-actions right">
                                    <a href="Default.aspx" class="btn default">Cancel</a>
                                    <button id="updateCategory" type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                                </div>

                            </div>
                        </div>
                    </div>
                </form>
            </div>
            <div class="tab-pane" id="images">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Images</div>
                        <div class="actions">
                            <a class="btn btn-default btn-sm" data-toggle="modal" href="#modal_UploadImages"><i class="fa fa-cloud-upload"></i>&nbsp;Upload New Image</a>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <form action="#" class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="bootstrap_alerts_photo"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table id="dtProductImage" class="table table-striped table-hover table-image">
                                            <thead>
                                                <tr>
                                                    <th style="width: 25%;">Preview
                                                    </th>
                                                    <th style="width: 25%;">File Name
                                                    </th>
                                                    <th style="width: 25%;">Is Cover
                                                    </th>
                                                    <th style="width: 25%;">Actions</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </form>
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <form id="info_insert">
                                        <div class="portlet iuCat" style="display:none;">
                                            <div class="portlet-title">
                                                <div class="caption">Update Information</div>
                                            </div>
                                            <div class="portlet-body form">
                                                <div class="horizontal-form">
                                                    <div class="form-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div id="bootstrap_alerts_meddef"></div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <div class="form-group">
                                                                    <label class="control-label">Title <span class="required">*</span></label>
                                                                    <input type="text" name="Name" id="tbTitle" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <div class="form-group">
                                                                    <label class="control-label">Description</label>
                                                                    <textarea name="Description" id="tbMediaDescription" class="form-control summernote"></textarea>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-actions right">
                                                        <a href="Default.aspx" class="btn default">Cancel</a>
                                                        <button id="submitMediaInfo" type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade bs-modal-lg" id="modal_UploadImages" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h5 class="modal-title"><span class="label label-danger">NOTE: </span>
                        &nbsp; This plugins works only on Latest Chrome, Firefox, Safari, Opera & Internet Explorer 10.</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <form id="formManufacturer_Update" runat="server" class="dropzone">
                                <div>
                                    <div class="fallback">
                                        <input name="file" type="file" multiple="" />
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12">
                            <button type="button" class="btn red btn-block" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jstree/dist/jstree.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/dropzone/dropzone.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/DT_bootstrap.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/content/page/Update.js"></script>
    <script type="text/javascript">
        Dropzone.options.myAwesomeDropzone = {
            paramName: "file", // The name that will be used to transfer the file
            maxFilesize: 2, // MB
            url: "./Update.aspx?id=<%=Request.QueryString["id"]%>",
            accept: function (file, done) {
                if (file.name == "justinbieber.jpg") {
                    done("Naha, you don't.");
                }
                else { done(); }
            },
            init: function () {
                console.log("finish upload");
                this.on("addedfile", function (file) { alert("Added file."); });
                this.in("complete", function (file) {
                    console.log("finish upload");
                });
            }
        }

        $('#modal_UploadImages').on('hidden.bs.modal', function () {
            Preload(+$("#HiddenIDPage").val());
        })

        $(document).ready(function () {
            $(".summernote").summernote({ height: 150 });
        });
    </script>
</asp:Content>

