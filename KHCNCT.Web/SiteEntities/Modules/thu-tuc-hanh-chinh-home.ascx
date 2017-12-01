<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="thu-tuc-hanh-chinh-home.ascx.cs"
    Inherits="KHCNCT.Web.SiteEntities.Modules.thu_tuc_hanh_chinh_home" %>
    <div class="fifth-title">
        <asp:HyperLink ID="hplCaption" runat="server" NavigateUrl='~/default.aspx?pn=thu-tuc-hanh-chinh'>Thủ tục hành chính</asp:HyperLink></div>
    <div class="topic-border">
        <asp:HyperLink ID="hplImageTopic" runat="server">
            <asp:Image ID="imgTopic" runat="server" CssClass="image-topic" BorderStyle="None" ImageUrl="~/images/tthc.jpg" />
        </asp:HyperLink>
        <div class="snew-list">
            <asp:Repeater ID="rptNews" runat="server">
                <ItemTemplate>
                    <div class="snew-list-item">
                        <asp:HyperLink ID="hplThuTuc" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl("thu-tuc-hanh-chinh","ttid=" + Eval("Id").ToString()) %>'>
                            <%# Eval("TenThuTuc") %></asp:HyperLink>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
