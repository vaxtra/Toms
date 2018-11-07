<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="vt_error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style>
        .loading
        {
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="col-md-12 breadcrumbs">
    </div>
    <div class="col-md-12 padpad margtotop">
        <h3 class="text-center">
            We're Sorry there's a problem in your payment process.<br />
            Please check again your credit card number and payment data for Veritrans Payment<br /><br />
            <a href="/Default.aspx">Back to Home</a>
        </h3>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJS" Runat="Server">
</asp:Content>

