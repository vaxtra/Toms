<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="adminwitcommerce_voucher_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/data-tables/DT_bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Voucher
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li>
        <a href="/adminwitcommerce/voucher">Voucher</a>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-tags"></i>Voucher</div>
                    <div class="actions">
                        <a href="insert.aspx" class="btn backLink btn-default btn-sm"><i class="fa fa-plus"></i>&nbsp;New Voucher </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="bootstrap_alerts"></div>
                    <table class="table table-striped table-bordered table-hover" id="dtVoucher">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Name</th>
                                <th>Code</th>
                                <th>Value</th>
                                <th>Available</th>
                                <th>Used</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="viewModal" class="modal fade" aria-hidden="true">
        <form class="form-horizontal" method="post">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title title-value-modal">Detail Voucher</h4>
                    </div>
                    <div class="modal-body" id="view_manufacturer">
                        <div class="form-body">
                            <div class="form-group">
                                <label class="col-md-4 control-label">Name :</label>
                                <label class="form-control-static Name"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">Code :</label>
                                <label class="form-control-static Code"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">Description :</label>
                                <label class="form-control-static Description"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">Voucher Type :</label>
                                <label class="form-control-static VoucherType"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">Value :</label>
                                <label class="form-control-static Value"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">Total Available :</label>
                                <label class="form-control-static TotalAvailable"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">Used :</label>
                                <label class="form-control-static Used"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">Minimum Amount :</label>
                                <label class="form-control-static MinimumAmount"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">Start Date :</label>
                                <label class="form-control-static StartDate"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">End date :</label>
                                <label class="form-control-static EndDate"></label>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">Active :</label>
                                <label class="form-control-static Active"></label>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="#" data-dismiss="modal" class="btn btn-block red btn-cancel">Close</a>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/data-tables/DT_bootstrap.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/payment/voucher/default.js"></script>
</asp:Content>

