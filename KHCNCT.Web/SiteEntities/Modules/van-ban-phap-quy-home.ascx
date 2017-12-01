<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="van-ban-phap-quy-home.ascx.cs"
    Inherits="KHCNCT.Web.SiteEntities.Modules.van_ban_phap_quy_home" %>
    <div class="fifth-title">
        <asp:HyperLink ID="hplCaption" runat="server" NavigateUrl='~/default.aspx?pn=van-ban-phap-quy'>Văn bản pháp quy</asp:HyperLink></div>
    <div class="topic-border">
        <asp:HyperLink ID="hplImageTopic" runat="server">
            <asp:Image ID="imgTopic" runat="server" CssClass="image-topic" BorderStyle="None" ImageUrl="~/images/vbpq.jpg" />
        </asp:HyperLink>
        <div class="snew-list">
            <asp:Repeater ID="rptNews" runat="server">
                <ItemTemplate>
                    <div class="snew-list-item">
                        <a href='<%# KHCNCT.Globals.Common.GenerateUrl("van-ban-phap-quy","vbid=" + Eval("Id").ToString()) %>'>
                            <%# Eval("TrichYeu") %></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
