<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="chuyen-muc-s2.ascx.cs"
    Inherits="KHCNCT.Web.SiteEntities.Pages.chuyen_muc_s2" %>
<!-- content-first: nội dung chính -->
<div class="content-first" id="divContentFirst" runat="server">
    <div class="date-title">
        <asp:Literal ID="ltrDate" runat="server"></asp:Literal></div>
    <div class="first-content">
        <div class="row">
            <div class="col-lg-6">
                <asp:Image ID="imgFirstNews" runat="server" Style="width: 100%; max-height: 410px;" />
                <div class="snew-title">
                    <asp:HyperLink ID="hplFirstNewsTitle" runat="server"></asp:HyperLink>
                </div>
                <div class="snew-abstract">
                    <asp:Literal ID="ltrFirstNewsDescription" runat="server"></asp:Literal>
                </div>
            </div>
            <asp:Repeater ID="rptNextTopNews" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-sm-3 col-xs-6">
                        <asp:Image ID="imgNextTopNews" runat="server" CssClass="snew-image-item" ImageUrl='<%# Eval("ImagePath") %>' />
                        <div class="snew-title">
                            <asp:HyperLink ID="hplNextTopNews" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></asp:HyperLink></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
<!-- /content-first -->
<!-- content-second: banner -->
<div class="content-second">
    <img src="/assets/images/banner.jpg" class="image-banner" />
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
                    <div class="snew-title-next">
                        <asp:HyperLink ID="hplNextNews" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></asp:HyperLink></div>
                    <div class="date-created">
                        <%# Eval("CreatedTime","{0:dd/MM/yyyy HH:mm}") %></div>
                    <div class="row">
                        <div class="col-sm-4">
                            <asp:Image ID="imgNextNews" runat="server" CssClass="snew-image-item" ImageUrl='<%# Eval("ImagePath") %>' />
                        </div>
                        <div class="col-sm-8">
                            <div class="snew-contextual">
                                <%# Eval("Description") %></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Literal ID="ltrPaging" runat="server"></asp:Literal>
        </div>
        <div class="col-lg-4">
            <div style="color: #ed1c24; font-weight: bold; padding-top: 10px; margin-top: 0px;
                margin-bottom: -20px;">
                VĂN BẰNG SHTT ĐƯỢC CẤP</div>
            <br />
            <!--<div style="color: #ed1c24;  font-weight: bold;  padding-top: 10px;   margin-top: 20px; margin-bottom:-20px;">VĂN BẰNG SHTT ĐƯỢC CẤP</div>-->
            <div class="topic-border">
                <div class="image-text image-views-border">
                    <img src="image/tbt4.jpg" class="image-topic" />
                    <div class="views-border-bg">
                    </div>
                    <div class="views-border-text">
                        Thẩm định công nghệ dự án: Đầu tư Nhà máy xử lý chất thải công nghiệp và y tế nguy
                        hại tại Ô Môn - TPCT</div>
                </div>
                <div class="snew-list">
                    <div class="snew-list-item">
                        <a href="">Thẩm định công nghệ dự án: Đầu tư Nhà máy xử lý chất thải công nghiệp và
                            y tế nguy hại tại Ô Môn - TPCT </a>
                    </div>
                    <div class="snew-list-item">
                        <a href="">Bánh xe được phát minh khoảng năm thứ 8.000 trước công nguyên tại châu Á.
                            Chiếc bánh xe cổ nhất được phát hiện là thuộc về nền văn minh Lưỡng Hà, năm thứ
                            3.500 trước công nguyên.</a>
                    </div>
                    <div class="snew-list-item">
                        <a href="">Không còn nghi ngờ gì nữa, sự ra đời của máy tính đã làm thay đổi hoàn toàn
                            cuộc sống của con người. Tuy nhiên, đây là một phát minh chưa được xác định rõ ràng
                            thời gian bắt đầu. Nhiều người cho rằng nó được phát minh vào những năm 1930, và
                            kỹ sư người Đức Konrad Zuse là nhà lập trình đầu tiên trên thế giới. </a>
                    </div>
                    <div class="snew-list-item">
                        <a href="">Không còn nghi ngờ gì nữa, sự ra đời của máy tính đã làm thay đổi hoàn toàn
                            cuộc sống của con người. Tuy nhiên, đây là một phát minh chưa được xác định rõ ràng
                            thời gian bắt đầu. Nhiều người cho rằng nó được phát minh vào những năm 1930, và
                            kỹ sư người Đức Konrad Zuse là nhà lập trình đầu tiên trên thế giới. </a>
                    </div>
                </div>
            </div>
            <div style="color: #ed1c24; font-weight: bold; padding-top: 10px; margin-top: 0px;
                margin-bottom: -20px;">
                THỦ TỤC HÀNH CHÍNH</div>
            <br />
            <div class="topic-border">
                <!---<div class="fifth-title">Thủ tục hành chính</div>--->
                <img src="image/tthc.jpg" class="image-topic" />
                <div class="snew-list">
                    <div class="snew-list-item">
                        <a href="">Thẩm định công nghệ dự án: Đầu tư Nhà máy xử lý chất thải công nghiệp và
                            y tế nguy hại tại Ô Môn - TPCT </a>
                    </div>
                    <div class="snew-list-item">
                        <a href="">Bánh xe được phát minh khoảng năm thứ 8.000 trước công nguyên tại châu Á.
                            Chiếc bánh xe cổ nhất được phát hiện là thuộc về nền văn minh Lưỡng Hà, năm thứ
                            3.500 trước công nguyên.</a>
                    </div>
                    <div class="snew-list-item">
                        <a href="">Không còn nghi ngờ gì nữa, sự ra đời của máy tính đã làm thay đổi hoàn toàn
                            cuộc sống của con người. Tuy nhiên, đây là một phát minh chưa được xác định rõ ràng
                            thời gian bắt đầu. Nhiều người cho rằng nó được phát minh vào những năm 1930, và
                            kỹ sư người Đức Konrad Zuse là nhà lập trình đầu tiên trên thế giới. </a>
                    </div>
                    <div class="snew-list-item">
                        <a href="">Không còn nghi ngờ gì nữa, sự ra đời của máy tính đã làm thay đổi hoàn toàn
                            cuộc sống của con người. Tuy nhiên, đây là một phát minh chưa được xác định rõ ràng
                            thời gian bắt đầu. Nhiều người cho rằng nó được phát minh vào những năm 1930, và
                            kỹ sư người Đức Konrad Zuse là nhà lập trình đầu tiên trên thế giới. </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- /Tin tiếp theo -->
