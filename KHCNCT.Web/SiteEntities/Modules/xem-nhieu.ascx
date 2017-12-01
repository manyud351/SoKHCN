<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="xem-nhieu.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Modules.xem_nhieu" %>
<div class="topic-line-bottom">
    XEM NHIỀU</div>
<div class="image-views-border">
    <asp:Image ID="imgFirstNews" runat="server" CssClass="image-topic" />
    <div class="views-border-bg">
    </div>
    <div class="views-border-text">
        <asp:HyperLink ID="hplFirstNews" runat="server"></asp:HyperLink>
    </div>
</div>
<div class="list-group">
    <asp:Repeater ID="rptFootColNews" runat="server">
        <ItemTemplate>
            <a href='<%# KHCNCT.Globals.Common.GenerateUrl(iDetailPageId,"nid=" + Eval("Id").ToString()) %>' class="list-group-item"><%# Eval("NewsTitle") %></a>
        </ItemTemplate>
    </asp:Repeater>
</div>
