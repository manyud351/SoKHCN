<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tin-cung-chuyen-muc.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Modules.tin_cung_chuyen_muc" %>
<div class="topic-line-bottom">
    <asp:HyperLink ID="hplCategory" runat="server">CÙNG CHUYÊN MỤC</asp:HyperLink></div>
<div class="snew-list snew-list-background">
    <asp:Repeater ID="rptFootColNews" runat="server">
        <ItemTemplate>
            <div class="snew-list-item"><a href='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></a></div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:HyperLink ID="hplViewAll" runat="server">Xem thêm</asp:HyperLink>
</div>
