<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="import-product.aspx.cs" Inherits="adminwitcommerce_catalog_import_product_import_product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Import Product
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Catalog</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Product</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Import Product</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-edit"></i>Import Data</div>
                    <div class="actions">
                        <a href="/adminwitcommerce/catalog/product/" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp; list</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form class="form-horizontal" id="form_carrier" runat="server">
                        <div class="form-body">
                            <div id="alertError" runat="server" class="alert alert-danger display-none">
                                <button class="close" data-dismiss="alert"></button>
                                <asp:Literal ID="lblError" runat="server"></asp:Literal>
                            </div>
                            <div id="alertSuccess" runat="server" class="alert alert-success display-none">
                                <button class="close" data-dismiss="alert"></button>
                                <asp:Literal ID="lblSuccess" runat="server"></asp:Literal>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">File <span class="required">*</span></label>
                                <div class="col-md-5">
                                    <asp:FileUpload ID="fuFile" CssClass="fileinput" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="col-md-offset-3">
                                <a href="Default.aspx" class="btn btn-default">Cancel</a>
                                <asp:Button ID="btnImport" runat="server" Text="Import" CssClass="btn blue" OnClick="btnImport_Click" />
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script>
        $(document).ready(function () {
            $("#submenu_import_product").addClass("active");
            $("#arrow_catalog").addClass("open");
            $("#menu_catalog").addClass("active");
        });
    </script>
</asp:Content>

