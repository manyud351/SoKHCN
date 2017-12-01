<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="trang-chu.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Pages.trang_chu" %>
<%@ Register src="~/SiteEntities/Modules/thong-bao.ascx" tagname="ThongBao" tagprefix="uc1" %>
<%@ Register src="~/SiteEntities/Modules/xem-nhieu.ascx" tagname="XemNhieu" tagprefix="uc2" %>
<%@ Register src="~/SiteEntities/Modules/tin-noi-bat.ascx" tagname="TinNoiBat" tagprefix="uc3" %>
<%@ Register src="~/SiteEntities/Modules/tin-chuyen-muc-trang-chu.ascx" tagname="TinChuyenMucTrangChu" tagprefix="uc4" %>
<%@ Register src="~/SiteEntities/Modules/tin-theo-chuyen-muc-t2.ascx" tagname="TinTheoChuyenMucT2" tagprefix="uc5" %>
<%@ Register src="~/SiteEntities/Modules/video-trang-chu.ascx" tagname="VideoTrangChu" tagprefix="uc6" %>
<%@ Register src="~/SiteEntities/Modules/thu-tuc-hanh-chinh-home.ascx" tagname="ThuTucHanhChinh" tagprefix="uc7" %>
<%@ Register src="~/SiteEntities/Modules/van-ban-phap-quy-home.ascx" tagname="VanBanPhapQuy" tagprefix="uc8" %>


			 	<!-- content-first: nội dung chính -->
			 	<div class="content-first">
			 		<div class="row">
			 			<div class="col-md-8">
			 				<uc3:TinNoiBat id="TinNoiBat1" runat="server" />
			 			</div>
			 			<div class="col-md-4">
			 				<uc1:ThongBao id="ThongBao1" runat="server" />
			 				<uc2:XemNhieu id="XemNhieu1" runat="server" />
			 			</div>
			 		</div>
			 	</div>
			 	<!-- /content-first -->
			 	<!-- content-second: banner -->
			 	<div class="content-second">
			 		<img src="/assets/images/banner.jpg" class="image-banner" /> 
			 	</div>
			 	<!-- /content-second -->
			 	<!-- content-third: lĩnh vực -->
			 	<div class="content-third block-content">
			 		<div class="row">
			 			<uc4:TinChuyenMucTrangChu id="TinChuyenMucTrangChu1" runat="server" />
			 		</div>
			 	</div>
			 	<!-- /content-third -->
			 	<!-- content-fourth: video clip, hình ảnh -->
			 	<div class="content-fourth block-content">
			 		<div class="row">

                        <uc6:VideoTrangChu id="VideoTrangChu1" runat="server" />
                        
                        <div class="col-lg-6 col-sm-6">
			 				<div class="fourth-title-yellow">
			 					<img src="/assets/images/chuphinh.jpg" style="height: 30px; width: 30px"/> 
			 				 	<a href="6.thuvienanhhoatdong.html">Hình ảnh</a>
			 				 </div>
			 				<div class="fourth-content-yellow">
								<img src="/assets/images/hinhanh2.jpg" style="width:100%; margin-bottom:0px;"/> 
								<div class="snew-title">Hình ảnh tiếp theo</div>
			 					<div>
				 					<div class="video-image-sub">
				 						<img src="/assets/images/video4.png" class="image-picture-video"/> 
				 						<div class="snew-title">Xe chữa cháy của “nhà sáng chế không bằng cấp” Phan Đình Phương </div>
				 					</div>
				 					<div class="video-image-sub">
				 						<img src="/assets/images/video5.png" class="image-picture-video"/> 
				 						<div class="snew-title">Phi cơ F–35C lần đầu bay từ hệ thống phóng điện từ</div>
				 					</div>
				 					<div class="video-image-sub">
				 						<img src="/assets/images/video6.png" class="image-picture-video"/> 
				 						<div class="snew-title">Thẩm định công nghệ dự án: Đầu tư Nhà máy xử lý chất thải công nghiệp và y tế nguy hại tại...</div>
				 					</div>
				 					<div class="clear"></div>
			 					</div>
			 				</div>
			 			</div>
			 		</div>
			 	</div>
			 	<!-- /content-fourth -->
			 	<!-- content-fifth: thủ tục hành chính, văn bản pháp quy, hỗ trợ phát triển -->
			 	<div class="content-fifth block-content">
			 		<div class="row">
			 			<div class="col-lg-4 col-sm-6">
							<uc7:ThuTucHanhChinh id="ThuTucHanhChinh1" runat="server"/>
			 			</div>
			 			<div class="col-lg-4 col-sm-6">
							<uc8:VanBanPhapQuy id="VanBanPhapQuy1" runat="server"/>
			 			</div>
			 			<div class="col-lg-4 col-sm-6">
							<uc5:TinTheoChuyenMucT2 id="TinTheoChuyenMucT23" runat="server"  ModKey="ho-tro-phat-trien"/>
			 			</div>
			 		</div>
			 	</div>
			 	<!-- /content-fifth -->
			 	
