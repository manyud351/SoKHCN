<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tin-chuyen-muc-trang-chu.ascx.cs"
    Inherits="KHCNCT.Web.SiteEntities.Modules.tin_chuyen_muc_trang_chu" %>
<asp:Repeater ID="rptCategory" runat="server" 
    onitemdatabound="rptCategory_ItemDataBound">
    <ItemTemplate>
        <div class="col-lg-4 col-sm-6">
            <div class="third-title">
                <asp:Hyperlink ID="hplCategoryName" runat="server">
                    <%# Eval("CategoryName") %></asp:Hyperlink></div>
            <div class="topic-border">
                <div class="image-text image-views-border">
                    <asp:Image ID="imgFirstNews" runat="server" CssClass="image-topic" />
                    <div class="views-border-bg">
                    </div>
                    <div class="views-border-text">
                        <asp:HyperLink ID="hplFirstNewsTitle" runat="server"></asp:HyperLink></div>
                </div>
                <div class="snew-list">
                    <asp:Repeater ID="rptNews" runat="server">
                        <ItemTemplate>
                            <div class="snew-list-item">
                                <a href='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
