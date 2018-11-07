<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="adminwitcommerce_adminorder_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/data-tables/DT_bootstrap.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    ADMIN ORDER
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Admin Order</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <input type="hidden" id="DestinationCode" />
    <div class="portlet light">
        <div class="portlet-title">
            <div id="bootstrap_alert"></div>
        </div>
        <div class="portlet-body form">
            <div class="tabbable-line">
                <ul class="nav nav-tabs ">
                    <li class="active">
                        <a href="#tabProduct" data-toggle="tab">Product</a>
                    </li>
                    <li>
                        <a href="#tabCustomer" data-toggle="tab">Data Customer</a>
                    </li>
                    <li>
                        <a href="#tabSummary" data-toggle="tab">Konfirmasi</a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane" id="tabCustomer">
                        <!-- BEGIN FORM-->
                        <form action="#" id="OrderForm" class="form-horizontal">
                            <div class="form-body">
                                <h3 class="form-section">Data Customer</h3>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Customer Type</label>
                                    <div class="col-md-4">
                                        <div class="radio-list">
                                            <label class="radio-inline">
                                                <input type="radio" name="TypeCustomer" value="3" checked="checked" />For Customer
                                            </label>
                                            <label class="radio-inline">
                                                <input type="radio" name="TypeCustomer" value="4" />By Admin</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Nama Depan</label>
                                    <div class="col-md-4">
                                        <input type="text" class="form-control" placeholder="first name" name="FirstName">
                                        <%--<span class="help-block">A block of help text. </span>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Nama Belakang</label>
                                    <div class="col-md-4">
                                        <input type="text" class="form-control" placeholder="last name" name="LastName" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Jenis Kelamin</label>
                                    <div class="col-md-4">
                                        <div class="radio-list">
                                            <label class="radio-inline">
                                                <input type="radio" name="Gender" value="L" checked="checked" />Male
                                            </label>
                                            <label class="radio-inline">
                                                <input type="radio" name="Gender" value="P" />Female</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Email</label>
                                    <div class="col-md-4">
                                        <input type="email" class="form-control" placeholder="Email Address" name="Email" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Telepon</label>
                                    <div class="col-md-4">
                                        <input type="email" class="form-control" placeholder="" name="Phone" />
                                    </div>
                                </div>
                            </div>
                            <h3 class="form-section">Alamat Pengiriman</h3>
                            <div class="form-group">
                                <label class="col-md-3 control-label"></label>
                                <div class="col-md-4">
                                    <div class="checkbox-list">
                                        <label class="checkbox-inline">
                                            <input type="checkbox" id="sameAsCustomer" value="1" />
                                            Sama dengan Data Customer
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Nama</label>
                                <div class="col-md-4">
                                    <input type="text" class="form-control" placeholder="" name="DeliveryName" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">No. Telepon</label>
                                <div class="col-md-4">
                                    <input type="text" class="form-control" placeholder="" name="DeliveryPhone" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Alamat</label>
                                <div class="col-md-4">
                                    <textarea class="form-control" name="DeliveryAddress"></textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Kode Pos</label>
                                <div class="col-md-4">
                                    <input type="text" class="form-control" placeholder="" name="DeliveryPostalCode" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label">Negara</label>
                                <div class="col-md-4">
                                    <input type="hidden" id="DeliveryCountry" name="Province" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Provinsi</label>
                                <div class="col-md-4">
                                    <input type="hidden" id="DeliveryProvince" name="Province" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Kota/Kabupaten</label>
                                <div class="col-md-4">
                                    <input type="hidden" id="DeliveryCity" name="City" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Kecamatan</label>
                                <div class="col-md-4">
                                    <input type="hidden" id="DeliveryDistrict" name="District" />
                                </div>
                            </div>

                            <h3 class="form-section">Alamat Billing</h3>
                            <div class="form-group">
                                <label class="col-md-3 control-label"></label>
                                <div class="col-md-4">
                                    <div class="checkbox-list">
                                        <label class="checkbox-inline">
                                            <input type="checkbox" id="sameAsDelivery" value="1">
                                            Sama dengan Alamat Pengiriman
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Nama</label>
                                <div class="col-md-4">
                                    <input type="text" class="form-control" placeholder="" name="BillingName" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">No. Telepon</label>
                                <div class="col-md-4">
                                    <input type="text" class="form-control" placeholder="" name="BillingPhone" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Alamat</label>
                                <div class="col-md-4">
                                    <textarea class="form-control" name="BillingAddress"></textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Kode Pos</label>
                                <div class="col-md-4">
                                    <input type="text" class="form-control" placeholder="" name="BillingPostalCode" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Negara</label>
                                <div class="col-md-4">
                                    <input type="hidden" id="BillingCountry" name="Province" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Provinsi</label>
                                <div class="col-md-4">
                                    <input type="hidden" id="BillingProvince" name="Province" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Kota/Kabupaten</label>
                                <div class="col-md-4">
                                    <input type="hidden" id="BillingCity" name="City" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Kecamatan</label>
                                <div class="col-md-4">
                                    <input type="hidden" id="BillingDistrict" name="District" />
                                </div>
                            </div>

                            <%--SHIPPING--%>
                            <h3 class="form-section">Pilih Shipping (SiCepat)</h3>
                            <div class="form-group">
                                <label class="col-md-3 control-label"></label>
                                <div class="col-md-4">
                                    <div class="radio-list shipping-list">
                                    </div>
                                </div>
                            </div>
                            <%--END SHIPPING--%>

                            <%--PAYMENT--%>
                            <h3 class="form-section">Cara Pembayaran</h3>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Pembayaran</label>
                                <div class="col-md-4">
                                    <input type="hidden" id="PaymentMethod" name="PaymentMethod" />
                                    <%--<span class="help-block">A block of help text. </span>--%>
                                </div>
                            </div>
                            <%--END PAYMENT--%>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <button type="submit" id="btnCustomerNext" class="btn btn-circle blue">Next</button>
                                        <button type="button" id="btnCustomerBack" class="btn btn-circle default">Back</button>
                                    </div>
                                </div>
                            </div>
                        </form>
                        <!-- END FORM-->
                    </div>
                    <div class="tab-pane active" id="tabProduct">
                        <div class="row">
                            <div class="col-md-6">
                                <table class="table table-striped table-bordered table-hover" id="dtProduct">
                                    <thead>
                                        <tr>
                                            <td>Photo</td>
                                            <td>Product</td>
                                            <td>Reference</td>
                                            <td>Discount</td>
                                            <td>Price</td>
                                            <td>Stock</td>
                                            <td></td>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <div class="portlet">
                                    <div class="portlet-title">
                                        <div class="caption">Cart</div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="table-scrollable">
                                            <table class="table table-striped table-hover cartTable">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>Product
                                                        </th>
                                                        <th>Qty
                                                        </th>
                                                        <th>Price
                                                        </th>
                                                        <th>Total
                                                        </th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="6">No Data</td>
                                                    </tr>
                                                </tbody>
                                                <%--<tfoot>
                                                    <tr>
                                                        <td class="text-right" colspan="4">Shipping</td>
                                                        <td class="totalShipping" colspan="2">0</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-right" colspan="4">Total</td>
                                                        <td class="totalPaid" colspan="2">0</td>
                                                    </tr>
                                                </tfoot>--%>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <button type="submit" id="btnProductNext" class="btn btn-circle blue">Next</button>
                                    <button type="button" id="btnProductBack" class="btn btn-circle default">Back</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="tabSummary">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet">
                                    <div class="portlet-title">
                                        <div class="caption">Cart</div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="table-scrollable">
                                            <table class="table table-striped table-hover cartTable">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>Product
                                                        </th>
                                                        <th>Qty
                                                        </th>
                                                        <th>Price
                                                        </th>
                                                        <th>Total
                                                        </th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="6">No Data</td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td class="text-right" colspan="4">Shipping</td>
                                                        <td class="totalShipping" colspan="2">0</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-right" colspan="4">Total</td>
                                                        <td class="totalPaid" colspan="2">0</td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <button type="submit" id="btnSummaryNext" class="btn btn-circle blue">Submit</button>
                                                    <%--<button type="button" id="btnSummaryBack" class="btn btn-circle default">Back</button>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/data-tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/DT_bootstrap.js"></script>
    <script src="../../assets/admin/pages/scripts/adminorder/default.js?v=1.1.2"></script>
</asp:Content>

