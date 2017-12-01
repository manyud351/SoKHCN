<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tin-theo-chuyen-muc-t1.ascx.cs"
    Inherits="KHCNCT.Web.SiteEntities.Modules.tin_theo_chuyen_muc_t1" %>
<div class="fifth-title"><asp:HyperLink ID="hplModTitle" runat="server"></asp:HyperLink></div>
<div class="topic-border">
    <asp:Image ID="imgFirstNews" runat="server" CssClass="image-topic" />
    <div class="snew-list">
        <div class="snew-list-item">
            <asp:HyperLink ID="hplFirstNews" runat="server"></asp:HyperLink>
        </div>
        <asp:Repeater ID="rptNextNews" runat="server">
            <ItemTemplate>
                <div class="snew-list-item">
                    <a href='<%# KHCNCT.Globals.Common.GenerateUrl(iDetailPage,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></a>
                </div>    
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
