<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="thong-bao.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Modules.thong_bao" %>
<div class="topic-line-bottom">
    <asp:HyperLink ID="hplCaption" runat="server"></asp:HyperLink></div>
<div class="list-group">
    <asp:Repeater ID="rptNews" runat="server">
        <ItemTemplate>
            <a href='<%# KHCNCT.Globals.Common.GenerateUrl(iDetailPageId,"nid=" + Eval("Id").ToString()) %>' class="list-group-item"><%# Eval("NewsTitle") %></a>
        </ItemTemplate>
    </asp:Repeater>
</div>
