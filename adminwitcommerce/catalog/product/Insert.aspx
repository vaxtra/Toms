<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Insert.aspx.cs" Inherits="adminwitcommerce_catalog_product_Insert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    New Product
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
    <li><a href="default.aspx">Products</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">New Product</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="tabbable tabbable-custom boxless">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#product" data-toggle="tab">Information</a></li>
            <li class=""><a href="#metatab" data-toggle="tab">Meta Data</a></li>
            <li class=""><a href="#categories" data-toggle="tab">Categories</a></li>
            <li class=""><a href="#images" data-toggle="tab">Images</a></li>
            <li class=""><a href="#combination" data-toggle="tab">Combinations</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="product">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Product Information</div>
                    </div>
                    <div class="portlet-body form">
                        <form action="#" id="formProductInformation_Insert" class="horizontal-form">
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
                                            <label class="control-label">Manufacturer<span class="required">*</span></label>
                                            <input type="hidden" id="IDManufacturer" name="IDManufacturer" class="form-control input" data-placeholder="Choose a Manufacturer" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Reference Code</label>
                                            <div class="input-group">
                                                <input type="text" id="ReferenceCode" name="ReferenceCode" class="form-control input" maxlength="25" placeholder="Reference Code" />
                                                <div class="input-group-addon">
                                                    <i class="fa fa-barcode"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Name<span class="required">*</span></label>
                                            <div class="input-group">
                                                <input type="text" id="Name" name="Name" class="form-control input" maxlength="25" placeholder="Product Name" />
                                                <div class="input-group-addon">
                                                    <i class="fa fa-tags"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Price Before Discount<span class="required">*</span></label>
                                            <div class="input-group">
                                                <input type="text" id="PriceBeforeDiscount" name="PriceBeforeDiscount" class="form-control money input" maxlength="15" value="0" />
                                                <div class="input-group-addon">
                                                    IDR
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Price After Discount</label>
                                            <div class="input-group">
                                                <input type="text" id="Price" name="Price" class="form-control money" maxlength="15" value="0" readonly="true" />
                                                <div class="input-group-addon">
                                                    IDR
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-6 col-xs-6 col-sm-6">
                                                <div class="form-group">
                                                    <label class="control-label">Discount</label>
                                                    <input type="text" id="DiscountPercent" name="Discount" class="form-control persentase input" maxlength="15" value="0" />
                                                    <input type="text" id="DiscountNominal" name="Discount" class="form-control money hide input" maxlength="15" value="0" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Discount Type</label>
                                                    <div class="icon-group">
                                                        <input type="checkbox" id="TypeDiscountPercent" name="TypeDiscountPercent" class="make-switch input" data-on-text="&nbsp;%&nbsp;&nbsp;" data-off-text="&nbsp;IDR&nbsp;" checked="checked" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Calculate</label>
                                                    <a href="#" id="btnCalculate" class="btn btn-block green">Calculate</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-6 col-xs-6 col-sm-6">
                                                <div class="form-group">
                                                    <label class="control-label">Package Duration (Days)</label>
                                                    <input type="text" id="Weight" name="Weight" class="form-control int cmb input" maxlength="6" value="0" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">Display / Active</label>
                                                    <div class="icon-group">
                                                        <input type="checkbox" id="Active" name="Active" class="make-switch input" data-on="success" data-off="warning" checked="checked" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--CURRENCY SECTION--%>
                                <div class="row hidden">
                                    <div class="col-md-12">
                                        <div class="panel-group accordion" id="accordion3">
                                        </div>
                                    </div>
                                </div>
                                <%--END CURRENCY SECTION--%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Size & Fit</label>
                                            <input type="text" name="ShortDescription" class="form-control input summernote" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Description</label>
                                            <input type="text" name="Description" class="form-control summernote input" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Note</label>
                                            <input type="text" name="Note" class="form-control summernote input" />
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
            <div class="tab-pane" id="metatab">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Search Engine Optimization</div>
                    </div>
                    <div class="portlet-body form">
                        <form action="#" class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="alert alert-danger">
                                                <button class="close" data-close="alert"></button>
                                                You must save this product before managing search engine optimization.
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="categories">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Categories</div>
                        <div class="actions">
                            <a href="./Default.aspx" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp;Products</a>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <form action="#" class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="alert alert-danger">
                                                <button class="close" data-close="alert"></button>
                                                You must save this product before managing categories.
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="images">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Images</div>
                    </div>
                    <div class="portlet-body form">
                        <form action="#" class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="alert alert-danger">
                                                <button class="close" data-close="alert"></button>
                                                You must save this product before adding images.
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="combination">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Product Combinations</div>
                    </div>
                    <div class="portlet-body form">
                        <form action="#" class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="alert alert-danger">
                                                <button class="close" data-close="alert"></button>
                                                You must save this product before adding combinations.
                                            </div>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/catalog/product/insert.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".money").inputmask("decimal", { allowPlus: false, allowMinus: false, rightAlignNumerics: false, radixPoint: ",", autoGroup: true, groupSeparator: ".", groupSize: 3 });

            TouchSpinWeight.init();
            TouchSpinPercent.init();
            TouchSpinInteger.init();
            $(".summernote").summernote({ height: 150 });
            $('#TypeDiscountPercent').on('switchChange.bootstrapSwitch', function (event, state) {
                if (state) {
                    if ($('#DiscountPercent').parent().hasClass('bootstrap-touchspin')) {
                        $('#DiscountPercent').addClass('input');
                        $('#DiscountPercent').parent().removeClass('hide');
                    }
                    $('#DiscountNominal').addClass('hide');
                } else {
                    if ($('#DiscountPercent').parent().hasClass('bootstrap-touchspin')) {
                        $('#DiscountNominal').addClass('input');
                        $('#DiscountPercent').parent().addClass('hide');
                    }
                    $('#DiscountNominal').removeClass('hide');
                }
            });
        });
    </script>
</asp:Content>

