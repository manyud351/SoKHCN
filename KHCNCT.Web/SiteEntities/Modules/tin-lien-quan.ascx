<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tin-lien-quan.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Modules.tin_lien_quan" %>
<div class="topic-line-bottom">TIN LIÊN QUAN</div>
<div class="image-views-border">
    <asp:Image ID="imgFirstNews" runat="server" CssClass="image-topic" />
    <div class="views-border-bg">
    </div>
    <div class="views-border-text">
        <asp:HyperLink ID="hplFirstNews" runat="server"></asp:HyperLink>
    </div>
</div>
<div class="snew-list snew-list-background">
    <asp:Repeater ID="rptFootColNews" runat="server">
        <ItemTemplate>
            <div class="snew-list-item"><a href='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></a></div>
        </ItemTemplate>
    </asp:Repeater>
</div>
