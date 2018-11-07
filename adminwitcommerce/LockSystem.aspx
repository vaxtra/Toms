<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="LockSystem.aspx.cs" Inherits="adminwitcommerce_LockSystem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    WIT. COMMERCE LOCK SYSTEM
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <%--    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Catalog</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Product</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Import Product</a></li>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <input type="hidden" id="HiddenIDAdmin" />
    <div class="row">
        <div class="col-md-12">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption"><i class="fa fa-edit"></i>WIT. COMMERCE LOCK SYSTEM</div>
                    <div class="actions">
                        <a href="/adminwitcommerce/catalog/product/" class="btn btn-default btn-sm"><i class="fa fa-mail-reply"></i>&nbsp; list</a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <form class="form-horizontal" id="form_carrier" runat="server">
                        <div class="form-body">
                            <div id="bootstrap_alerts"></div>
                            <div class="form-group">
                                <label class="control-label col-md-3">System Status : </label>
                                <div class="col-md-5">
                                    <label class="SystemStatusMessage"><i class="fa fa-unlock"></i> UNLOCKED</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">LOCK SYSTEM? <span class="required">*</span></label>
                                <div class="col-md-5">
                                    <button type="button" class="btn red LockSystem"><i class="fa fa-lock"></i>&nbsp;LOCK SYSTEM</button>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script src="/assets/admin/pages/scripts/LockSystem.js"></script>
</asp:Content>

