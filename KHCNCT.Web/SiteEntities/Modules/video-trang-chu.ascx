<%@ Control Language="C#" AutoEventWireup="true" Inherits="KHCNCT.Modules.video_trang_chu"
    CodeBehind="video-trang-chu.ascx.cs" %>
<div class="col-lg-6 col-sm-6">
    <div class="fourth-title-red">
        <img src="/assets/images/quayphim.jpg" style="height: 30px; width: 30px" />
        <asp:HyperLink ID="hplVideo" runat="server">Video Clip</asp:HyperLink>
    </div>
    <div class="fourth-content-red">
        <div class="embed-responsive embed-responsive-16by9">
            <a href="<%=vVideoDauTienLink %>">
                <asp:Label ID="lblTieuDeVideoDauTien" runat="server" Text="" Font-Bold="true"></asp:Label></a>
            <div id='video_container'>
            </div>
            <script type='text/javascript'>
                jwplayer('video_container').setup({
                    flashplayer: 'assets/players/player.swf',
                    file: '<%=vVideoUrl %>',
                    image: '<%=vPreviewImageUrl %>',
                    height: "100%",
                    width: "100%",
                    aspectratio: "16:9",
                    events: {
                        onPlay: function () {
                            jQuery.noConflict();
                            jQuery.ajax({
                                url: '<%= "lib/videocounter.aspx?vid=" + firstVideoId.ToString() %>',
                                success: function () {
                                }
                            });
                        }
                    }
                });
            </script>
        </div>
        <div class="snew-title">
            <asp:HyperLink ID="hplVideoTitle" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl("video") %>'>
                Video tiếp theo
            </asp:HyperLink>
        </div>
        <div>
            <asp:Repeater ID="rptOtherVideos" runat="server">
                <ItemTemplate>
                    <div class="video-image-sub">
                         <asp:HyperLink ID="hplImgPreview" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl("video","vid=" + Eval("Id").ToString()) %>'>
                            <asp:Image runat="server" Id="imgPreview" CssClass="image-picture-video" ImageUrl='<%# Eval("PreviewImage") %>' />
                        </asp:HyperLink>
                        <div class="snew-title">
                            <asp:HyperLink ID="hplVideoTitle" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl("video","vid=" + Eval("Id").ToString()) %>'>
                                <%# Eval("VideoTitle") %>
                            </asp:HyperLink>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="clear">
            </div>
        </div>
    </div>
</div>
