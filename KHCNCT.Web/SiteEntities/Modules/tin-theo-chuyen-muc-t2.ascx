<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tin-theo-chuyen-muc-t2.ascx.cs"
    Inherits="KHCNCT.Web.SiteEntities.Modules.tin_theo_chuyen_muc_t2" %>
    <div class="fifth-title">
        <asp:HyperLink ID="hplCaption" runat="server"></asp:HyperLink></div>
    <div class="topic-border">
        <asp:HyperLink ID="hplImageTopic" runat="server">
            <asp:Image ID="imgTopic" runat="server" CssClass="image-topic" BorderStyle="None" />
        </asp:HyperLink>
        <div class="snew-list">
            <asp:Repeater ID="rptNews" runat="server">
                <ItemTemplate>
                    <div class="snew-list-item">
                        <a href='<%# KHCNCT.Globals.Common.GenerateUrl(iDetailPageId,"nid=" + Eval("Id").ToString()) %>'>
                            <%# Eval("NewsTitle") %></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
