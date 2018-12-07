<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="adminwitcommerce_catalog_product_Update" %>

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

        .accordion a {
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Update Product
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
    <li><a href="#" class="productName">Update Product</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <input type="hidden" id="HiddenIDProduct" />
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
                        <form action="#" id="formProductInformation_Update" class="horizontal-form">
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
                                            <label class="control-label">Manufacturer</label>
                                            <div class="input-group">
                                                <input type="text" id="Manufacturer" name="Manufacturer" class="form-control input" readonly="" placeholder="Manufacturer" />
                                                <div class="input-group-addon">
                                                    <i class="fa fa-gears"></i>
                                                </div>
                                            </div>
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
                                                <input type="text" id="Name" name="Name" class="form-control input" placeholder="Product Name" />
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
                                            <div class="col-md-4 col-xs-4 col-sm-4">
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
                                            <div class="col-md-2 col-xs-2 col-sm-2">
                                                <div class="form-group">
                                                    <label class="control-label">Calculate</label>
                                                    <a href="#" id="btnCalculate" class="btn btn-block green">Calculate</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-6 col-xs-6 col-sm-6 hidden">
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
                                            <input type="text" name="ShortDescription" id="ShortDescription" class="form-control input summernote" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Description</label>
                                            <input type="text" name="Description" id="Description" class="form-control summernote input" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Note</label>
                                            <input type="text" name="Note" id="Note" class="form-control summernote input" />
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
                        <div class="form-body">
                            <form id="formSEO" class="horizontal-form">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="bootstrap_alerts_meta"></div>
                                        <div class="alert alert-danger display-hide">
                                            <button class="close" data-close="alert"></button>
                                            You have some form errors. Please check below.
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Meta Title</label>
                                            <div class="input-group">
                                                <input type="text" id="Meta" name="Meta" class="form-control input" placeholder="Meta Title" />
                                                <div class="input-group-addon">
                                                    <i class="fa fa-gears"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Meta Description</label>
                                            <textarea id="MetaDescription" rows="5" name="MetaDescription" class="form-control input" placeholder="Meta Description"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Meta Keywords</label>
                                            <textarea id="MetaKeyword" rows="5" name="MetaKeyword" class="form-control input" placeholder="Meta Keywords"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions right">
                                    <a href="Default.aspx" class="btn default">Cancel</a>
                                    <button id="btnSubmitSEO" type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="categories">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Categories</div>
                    </div>
                    <div class="portlet-body form">
                        <div class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="bootstrap_alerts_cat"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div id="treeCategory"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <h5>
                                            <span class="label label-danger">NOTE: </span>
                                            <br />
                                            <br />
                                            <i class="fa fa-tags icon-state-danger"></i>&nbsp;: Category is not active.
                                            <br />
                                            <i class="fa fa-tags icon-state-success"></i>&nbsp;: Category is active.
                                        </h5>
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
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Default Category</div>
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
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <input type="hidden" id="DefaultCategory" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-actions right">
                                <a href="Default.aspx" class="btn default">Cancel</a>
                                <button id="submitCategoryDefault" type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                            </div>
                        </div>
                    </div>
                </div>
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
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="combination">


                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Product Combinations</div>
                        <div class="actions">
                            <a class="btn btn-default btn-sm new-cmb"><i class="fa fa-plus"></i>&nbsp;New Combination</a>
                            <a class="btn btn-default btn-sm edit-qty"><i class="fa fa-pencil-square-o"></i>&nbsp;Edit Quantity</a>
                            <a class="btn btn-default btn-sm save-qty"><i class="fa fa-save"></i>&nbsp;Save Quantity</a>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div id="bootstrap_alert_combination"></div>
                            </div>
                        </div>
                        <table class="table table-striped table-bordered table-hover" id="dtCombination">
                            <thead>
                                <tr>
                                    <th class="text-center">Attribute - Value pair
                                    </th>
                                    <th class="text-center">Reference Code
                                    </th>
                                    <th class="text-center">Impact on price
                                    </th>
                                    <th class="text-center">Impact on weight
                                    </th>
                                    <th class="text-center">Quantity
                                    </th>
                                    <th class="text-center">Position
                                    </th>
                                    <th class="text-center">Actions
                                    </th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
                <div class="for-scroll"></div>
                <div class="portlet" id="panelCombination">
                    <div class="portlet-title">
                        <div id="text-combination" class="caption">New Combination</div>
                        <input type="hidden" id="HiddenIDCombination" />
                    </div>
                    <div class="portlet-body form">
                        <form action="#" class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <input type="hidden" id="hiddenIdCombination" />
                                        <div class="form-group">
                                            <label class="control-label">Attribute</label>
                                            <input type="hidden" id="Attributes" class="form-control select2" />
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Values</label>
                                            <input type="hidden" id="Values" class="form-control select2" />
                                        </div>
                                        <div class="form-group">
                                            <button id="btnAddCombinationValue" class="btn btn-block btn-sm blue"><i class="fa fa-check"></i>&nbsp;Add</button>
                                        </div>
                                    </div>
                                    <div class="col-md-6 right">
                                        <div class="form-group">
                                            <select multiple="" class="form-control" id="listCombination">
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <button id="btnDeleteCombinationValue" class="btn btn-block btn-sm red"><i class="fa fa-trash-o"></i>&nbsp;Delete</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Reference Code</label>
                                            <div class="input-group">
                                                <input type="text" name="ReferenceCode" class="form-control cmb" maxlength="25" placeholder="Reference Code" />
                                                <div class="input-group-addon">
                                                    <i class="fa fa-barcode"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Quantity</label>
                                            <div class="input-group">
                                                <input type="text" name="Quantity" class="form-control int cmb" value="0" />
                                            </div>
                                        </div>
                                    </div>

                                    <!-- HIDE DISCOUNT -->
                                    <div class="col-md-3 hidden">
                                        <div class="form-group">
                                            <label class="control-label">Discount</label>
                                            <input type="text" name="Discount" class="form-control Discount input" maxlength="25" placeholder="Discount" readonly="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-3 hidden">
                                        <div class="form-group">
                                            <label class="control-label">Discount Type</label>
                                            <input type="text" name="TypeDiscountPercent" class="form-control TypeDiscountPercent input" maxlength="25" placeholder="Amount / Percent" readonly="true" />
                                        </div>
                                    </div>
                                    <!-- END HIDE DISCOUNT -->
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Impact on Price</label>
                                            <div class="input-group">
                                                <input type="text" id="ImpactPrice" name="ImpactPrice" class="form-control money cmb" maxlength="25" placeholder="Impact on Price" />
                                                <div class="input-group-addon">
                                                    IDR
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Price Before Impact</label>
                                            <div class="input-group">
                                                <input type="text" name="PriceBeforeImpact" class="form-control money Price input" maxlength="25" placeholder="Price Before Impact" readonly="true" />
                                                <div class="input-group-addon">
                                                    IDR
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 hidden">
                                        <div class="form-group">
                                            <label class="control-label">Calculate Price</label>
                                            <a href="#" id="btnCalculateImpactPrice" class="btn btn-block green">Calculate Price</a>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Price after impact</label>
                                            <div class="input-group">
                                                <input type="text" name="PriceAfterImpact" class="form-control input money cmb" maxlength="25" placeholder="Price" readonly="true" />
                                                <div class="input-group-addon">
                                                    IDR
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row hidden">
                                    <!-- HIDE BASE PRICE -->
                                    <div class="col-md-6 hidden">
                                        <div class="form-group">
                                            <label class="control-label">Base Price</label>
                                            <div class="input-group">
                                                <input type="text" name="BasePrice" class="form-control money cmb" maxlength="25" />
                                                <div class="input-group-addon">IDR</div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- END HIDE BASE PRICE -->
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Package Duration (Days)</label>
                                            <div class="input-group">
                                                <input type="text" id="ImpactWeight" name="ImpactWeight" class="form-control int cmb" maxlength="6" value="0" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 hidden">
                                        <div class="form-group">
                                            <label class="control-label">Calculate Weight</label>
                                            <a href="#" id="btnCalculateImpactWeight" class="btn btn-block green">Calculate Weight</a>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Weight Before Impact</label>
                                            <div class="input-group">
                                                <input type="text" name="WeightBeforeImpact" class="form-control cmb Weight input" maxlength="25" placeholder="Weight Before Impact" readonly="true" />
                                                <div class="input-group-addon">
                                                    Kg
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Weight after impact</label>
                                            <div class="input-group">
                                                <input type="text" name="Weight" class="form-control cmb input" maxlength="25" placeholder="Impact on Weight" readonly="true" />
                                                <div class="input-group-addon">
                                                    Kg
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Images</label>
                                            <div id="listImages" class="checkbox-list">
                                                <%--<label class="checkbox-inline statusCover">
                                                    <input type="checkbox" name="sport[]" checked="checked" value="football" />
                                                    <img style="max-width: 75px; margin: 3px;" src="/assets/images/product/meilisa-1.jpg" alt="image" class="img-responsive" />
                                                </label>
                                                <label class="checkbox-inline statusCover">
                                                    <input type="checkbox" name="sport[]" checked="checked" value="football" />
                                                    <img style="max-width: 75px; margin: 3px" src="/assets/images/product/meilisa-2.jpg" alt="image" class="img-responsive" />
                                                </label>
                                                <label class="checkbox-inline statusCover">
                                                    <input type="checkbox" name="sport[]" checked="checked" value="football" />
                                                    <img style="max-width: 75px; margin: 3px" src="/assets/images/product/meilisa-3.jpg" alt="image" class="img-responsive" />
                                                </label>
                                                <label class="checkbox-inline statusCover">
                                                    <input type="checkbox" name="sport[]" checked="checked" value="football" />
                                                    <img style="max-width: 75px; margin: 3px" src="/assets/images/product/meilisa-4.jpg" alt="image" class="img-responsive" />
                                                </label>
                                                <label class="checkbox-inline statusCover">
                                                    <input type="checkbox" name="sport[]" checked="checked" value="football" />
                                                    <img style="max-width: 75px; margin: 3px" src="/assets/images/product/meilisa-5.jpg" alt="image" class="img-responsive" />
                                                </label>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions right">
                                <a id="btnCancelCombination" class="btn default" href="Default.aspx">Cancel</a>
                                <button id="btnSaveCombination" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                            </div>
                        </form>
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
    <script type="text/javascript" src="/assets/global/plugins/data-tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jstree/dist/jstree.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/dropzone/dropzone.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/catalog/product/update.js"></script>
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

            $('#modal_UploadImages').on('hidden.bs.modal', function () {
                Preload(+$("#HiddenIDProduct").val());
            })
        });
    </script>
</asp:Content>

