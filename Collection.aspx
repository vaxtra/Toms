<%@ Page Title="Collection - WIT. Commerce" Language="C#" MasterPageFile="~/MasterPageDNDexe.master" AutoEventWireup="true" CodeFile="Collection.aspx.cs" Inherits="Collection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="about-us wow fadeInUp">
    </section>
    <section>
        <ul class="collection">
            <asp:ListView ID="lvCollection" runat="server">
                <ItemTemplate>
                    <li>
                        <hr style="border:1px solid #ddd" />
                        <div style="background-image:url(/assets/images/post/<%# Eval("Photo") %>);" class="floatleft wow fadeInUp">
                            <div class="front-link">
                                <h3><%# Eval("Title") %></h3>
                                <div style="display: table; margin: auto; margin-top: 30px">
                                    <div class="half">
                                        <a href="<%# Eval("ShortDescription") %>" class="site-button-dark"><span>WEBSITE</span></a>
                                    </div>
                                    <div class="half">
                                        <a href="CollectionDetail.aspx?idCollection=<%# Eval("IDPost") %>" class="site-button-dark"><span>VIEW DETAIL</span></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </ul>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script src="/assets/frontend/scripts/cms.js"></script>
</asp:Content>

