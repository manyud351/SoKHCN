<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="chi-tiet.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Pages.chi_tiet" %>
<%@ Register Src="~/SiteEntities/Modules/xem-nhieu.ascx" TagName="XemNhieu" TagPrefix="uc1" %>
<%@ Register Src="~/SiteEntities/Modules/tin-lien-quan.ascx" TagName="TinLienQuan"
    TagPrefix="uc2" %>
<%@ Register Src="../Modules/tin-cung-chuyen-muc.ascx" TagName="TinCungChuyenMuc"
    TagPrefix="uc3" %>
<!-- content-first: nội dung chính -->
<div class="content-first">
    <div class="row">
        <div class="col-md-8">
            <!-- Đường dẫn -->
            <div class="first-head">
                <ol class="breadcrumb">
                    <li><a href="#">Home</a></li>
                    <li><a href="#">Library</a></li>
                    <li class="active">Data</li>
                </ol>
            </div>
            <!-- /Đường dẫn -->
            <!-- Tin chi tiết -->
            <div class="snew">
                <div class="date-title">
                    <asp:Literal ID="ltrNewsDate" runat="server"></asp:Literal>
                </div>
                <div class="snew-title">
                    <asp:Literal ID="ltrNewsTitle" runat="server"></asp:Literal>
                </div>
                <div class="font-pluss">
                    <asp:Literal ID="ltrNewsAuthor" runat="server"></asp:Literal>
                </div>
                <div class="snew-head">
                    <asp:Literal ID="ltrNewsDescription" runat="server"></asp:Literal>
                </div>
                <asp:Image ID="imgNews" runat="server" />
                <div class="snew-content">
                    <asp:Literal ID="ltrNewsContent" runat="server"></asp:Literal>
                </div>
                <div style="text-align: right">
                    <asp:Literal ID="ltrSource" runat="server"></asp:Literal></div>
            </div>
            <!-- /Tin chi tiết -->
            <!-- File -->
            <div class="date-title">
                <asp:Literal ID="ltrFiles" runat="server">File đính kèm</asp:Literal>
            </div>
            <asp:Repeater ID="rptFiles" runat="server">
                <ItemTemplate>
                    <a href='<%# Eval("FilePath").ToString() %>' target="_blank">
                        <%# Eval("FileName") %></a>
                </ItemTemplate>
            </asp:Repeater>
            <!-- End File -->
            <uc3:TinCungChuyenMuc ID="TinCungChuyenMuc1" runat="server" />
        </div>
        <div class="col-md-4">
            <uc1:XemNhieu ID="XemNhieu1" runat="server" />
            <div style="clear: both">
            </div>
            <uc2:TinLienQuan ID="TinLienQuan1" runat="server" />
        </div>
    </div>
</div>
