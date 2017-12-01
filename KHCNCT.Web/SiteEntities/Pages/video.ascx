<%@ Control Language="C#" AutoEventWireup="true" Inherits="KHCNCT.Page.pVideo" CodeBehind="video.ascx.cs" %>
<br />
<div class="content-first">
    <div class="row">
        <div class="col-md-3">
            <div class="clear">
            </div>
            <ul class="menu-left">
                <asp:Repeater ID="rptVideoCategories" runat="server">
                    <ItemTemplate>
                        <a href='<%# KHCNCT.Globals.Common.GenerateUrl("video","vcid=" + Eval("Id").ToString()) %>'
                            class="sub-menu-left sub-title">
                            <%# Eval("CategoryName") %></a>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="col-md-9">
            <div id="divVideoContent" runat="server">
                <div class="col-md-8">
                    <div class="embed-responsive embed-responsive-16by9">
                        <div id='main_video_container'>
                        </div>
                        <script type='text/javascript'>
                            jwplayer('main_video_container').setup({
                                flashplayer: 'assets/players/player.swf',
                                file: '<%=vPlay %>',
                                image: '<%=vPreviewImageUrl %>',
                                height: "100%",
                                width: '100%',
                                aspectratio: "16:9",
                                events: {
                                    onPlay: function () {
                                        jQuery.noConflict();
                                        jQuery.ajax({
                                            url: '<%= "lib/videocounter.aspx?vid=" + vPlayId.ToString() %>',
                                            success: function () {
                                            }
                                        });
                                    }
                                }
                            });
                        </script>
                    </div>
                    <div class="snew-head">
                        <asp:Label ID="lblTieuDe" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        <div class="col-md-4">
            <asp:Repeater ID="rptTopVideos" runat="server">
                <ItemTemplate>
                    <div class="col-md-12 col-lg-12 margin-top: 40px">
                        <div class="image-views-border">
                            <asp:HyperLink ID="hplPreview" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl("video","vid=" + Eval("Id").ToString()) %>'>
                                <asp:Image ID="imgPreview" runat="server" BorderStyle="None" ImageUrl='<%# Eval("PreviewImage") %>'  CssClass="image-topic"/>
                                <div class="snew-head">
                                    <%# Eval("VideoTitle") %></div>
                            </asp:HyperLink>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="divVideoNotFound" runat="server">
            <asp:Label ID="lblVideoNotFound" runat="server">Không tìm thấy video</asp:Label>
        </div>
        </div>
    </div>
</div>
<div class="background-while">
    <div class="image-line-bottom">
        Video tiếp theo</div>
    <div class="row">
        <asp:Repeater ID="rptNextVideo" runat="server">
            <ItemTemplate>
                <div class="col-md-3 col-xs-6  margin-bottom-20px">
                    <asp:HyperLink ID="hplPreview" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl("video","vid=" + Eval("Id").ToString()) %>'>
                                <asp:Image ID="imgPreview" runat="server" BorderStyle="None" ImageUrl='<%# Eval("PreviewImage") %>'  CssClass="image-topic"/>
                                <div class="snew-head">
                                    <%# Eval("VideoTitle") %></div>
                            </asp:HyperLink>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
