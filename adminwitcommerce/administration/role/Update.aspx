<%@ Page Title="" Language="C#" MasterPageFile="~/adminwitcommerce/AdministratorMasterPage.master" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="adminwitcommerce_administration_role_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" />
    <link rel="stylesheet" type="text/css" href="/assets/global/plugins/jstree/dist/themes/default/style.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="Server">
    Update Role
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <a href="/adminwitcommerce/">Dashboard</a>
    </li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="Default.aspx">Administration</a></li>
    <li><i class="fa fa-angle-right"></i></li>
    <li><a href="#">Update Role</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <input type="hidden" id="HiddenIDRole" />
    <div class="tabbable tabbable-custom boxless">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#role" data-toggle="tab">Information</a></li>
            <li class=""><a href="#permissions" data-toggle="tab">Permissions</a></li>
            <li class=""><a href="#notif" data-toggle="tab">Notification & Order Status</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="role">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Role Data</div>
                    </div>
                    <div class="portlet-body form">
                        <form class="form-horizontal" id="form_role">
                            <div class="form-body">
                                <div id="bootstrap_alert"></div>
                                <div class="alert alert-danger display-none">
                                    <button class="close" data-dismiss="alert"></button>
                                    You have some form errors. Please check below.
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3">Name<span class="required">*</span></label>
                                    <div class="col-md-5">
                                        <input type="text" name="Name" class="form-control input" />
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
            <div class="tab-pane" id="permissions">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Permission</div>
                    </div>
                    <div class="portlet-body form">
                        <div class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="bootstrap_alerts_menu"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div id="treeMenu"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions right">
                                <a href="Default.aspx" class="btn default">Cancel</a>
                                <button id="submitMenu" type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="notif">
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Order Status Handle</div>
                    </div>
                    <div class="portlet-body form">
                        <div class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="bootstrap_alerts_handle"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div id="treeNotif"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions right">
                                <a href="Default.aspx" class="btn default">Cancel</a>
                                <button id="submitOrderStatus" type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="portlet">
                    <div class="portlet-title">
                        <div class="caption">Notification</div>
                    </div>
                    <div class="portlet-body form">
                        <div class="horizontal-form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="bootstrap_alerts_notif"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <input type="hidden" id="Notification" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-actions right">
                                <a href="Default.aspx" class="btn default">Cancel</a>
                                <button id="submitNotification" type="submit" class="btn blue"><i class="fa fa-save"></i>&nbsp;Save Changes</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="Server">
    <script type="text/javascript" src="/assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
    <script type="text/javascript" src="/assets/admin/pages/scripts/administration/role/update.js"></script>
    <script type="text/javascript" src="/assets/global/plugins/jstree/dist/jstree.min.js"></script>
</asp:Content>

