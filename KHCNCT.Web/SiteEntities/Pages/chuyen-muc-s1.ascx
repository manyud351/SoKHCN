<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="chuyen-muc-s1.ascx.cs"
    Inherits="KHCNCT.Web.SiteEntities.Pages.chuyen_muc_s1" %>
<%@ Register Src="~/SiteEntities/Modules/thong-bao.ascx" TagName="ThongBao" TagPrefix="uc1" %>
<%@ Register Src="~/SiteEntities/Modules/xem-nhieu.ascx" TagName="XemNhieu" TagPrefix="uc2" %>
<%@ Register Src="~/SiteEntities/Modules/tin-theo-chuyen-muc-t2.ascx" TagName="TinTheoChuyenMucT2"
    TagPrefix="uc5" %>
<!-- content-first: nội dung chính -->
<div id="divContentFirst" runat="server" class="content-first">
    <div class="row">
        <div class="col-md-8">
            <div class="first-head">
                <ol class="breadcrumb">
                    <li><a href="#">Home</a></li>
                    <li><a href="#">Library</a></li>
                    <li class="active">Data</li>
                </ol>
            </div>
            <div class="first-content">
                <div class="row">
                    <div class="col-lg-8 col-sm-8">
                        <asp:Image ID="imgFirstNews" runat="server" Style="width: 100%; max-height: 410px;" />
                        <div class="snew-title">
                            <asp:HyperLink ID="hplFirstNewsTitle" runat="server"></asp:HyperLink>
                        </div>
                        <div class="snew-abstract">
                            <asp:Literal ID="ltrFirstNewsDescription" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-4">
                        <asp:Repeater ID="rptNextTopNews" runat="server">
                            <ItemTemplate>
                                <div class="col-lg-12 col-sm-12 col-xs-6">
                                    <asp:Image ID="imgNextTopNews" runat="server" CssClass="snew-image-item" ImageUrl='<%# Eval("ImagePath") %>' />
                                    <div class="snew-title">
                                        <asp:HyperLink ID="hplNextTopNews" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></asp:HyperLink></div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="first-foot">
                <div class="row">
                    <asp:Repeater ID="rptTopFootNews" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 col-sm-4 col-xs-4">
                                <asp:Image ID="imgNextTopNews" runat="server" CssClass="snew-image-item" ImageUrl='<%# Eval("ImagePath") %>' />
                                <div class="snew-title">
                                    <asp:HyperLink ID="hplNextTopNews" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></asp:HyperLink></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <uc1:ThongBao ID="ThongBao1" runat="server" />
            <uc2:XemNhieu ID="XemNhieu1" runat="server" />
        </div>
    </div>
</div>
<!-- /content-first -->
<!-- content-second: banner -->
<div  id="divContentSecond" runat="server" class="content-second">
    <img src="image/banner.jpg" class="image-banner" />
</div>
<!-- /content-second -->
<!-- Tin tiếp theo -->
<div class="snews-next">
    <div class="row">
        <div class="col-lg-8">
            <div class="topic-line-bottom">
                TIN TIẾP THEO</div>
            <asp:Repeater ID="rptNextNews" runat="server">
                <ItemTemplate>
                    <div class="snew-title-next"><asp:HyperLink ID="hplNextNews" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></asp:HyperLink></div>
                    <div class="date-created"><%# Eval("CreatedTime","{0:dd/MM/yyyy HH:mm}") %></div>
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Image ID="imgNextNews" runat="server" CssClass="snew-image-item" ImageUrl='<%# Eval("ImagePath") %>' />
                        </div>
                        <div class="col-sm-8">
                            <div class="snew-contextual">
                                <%# Eval("Description") %>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Literal ID="ltrPaging" runat="server"></asp:Literal>
        </div>
        <div class="col-lg-4">
            <uc5:TinTheoChuyenMucT2 ID="TinTheoChuyenMucT21" runat="server" ModKey="van-ban-phap-quy" />
            <uc5:TinTheoChuyenMucT2 ID="TinTheoChuyenMucT22" runat="server" ModKey="ho-tro-phat-trien" />
        </div>
    </div>
</div>
<!-- /Tin tiếp theo -->
