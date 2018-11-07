<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="unfinish.aspx.cs" Inherits="vt_unfinish" %>

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
            Your payment process is unfinished, because you choose to back to our website before your payment finished.<br />
            Please Re-order on our website again, Thank you<br /><br />
            <a href="/Default.aspx">Back to Home</a>
        </h3>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJS" Runat="Server">
</asp:Content>

