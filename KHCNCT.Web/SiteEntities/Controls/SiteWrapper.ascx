<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteWrapper.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Controls.SiteWrapper" %>
<%@ Register src="~/SiteEntities/Modules/vmenu.ascx" tagname="MainMenu" tagprefix="uc1" %>
<%@ Register src="~/SiteEntities/Modules/foot_categories.ascx" tagname="FootCategories" tagprefix="uc2" %>
<%@ Register src="~/SiteEntities/Modules/slider.ascx" tagname="Slider" tagprefix="uc3" %>
<div class="page-content">
<!-- header -->
			<div class="header">
			 	<div class="header-first">
			 		<div class="line-header line-header-sub" >
					 	<ul>
					 		<li><a href='<%= KHCNCT.Globals.Common.GenerateUrl(59) %>'>Liên hệ</a></li>
					 		<li><a href='<%= KHCNCT.Globals.Common.GenerateUrl(58) %>'>Góp ý</a></li>
					 		<li><a href="10.danhsachhoidap.html">Hỏi đáp</a></li>
					 		<li><a href="">Lịch làm việc</a></li>
					 	</ul>
				 	</div>
			 		<div class="form-header form-header-sub">
					 	<form  role="search">
						         <input type="text" class="lang-image search-fo" placeholder="Search">
						         <button type="submit" class="lang-image"><span class="glyphicon glyphicon-search"></span></button>
						        	<a href=""><img src="assets/images/vietnam.jpg" class="lang-image"/> </a>
				 	         	<a href=""><img src="assets/images/english.jpg" class="lang-image" /></a>
						 </form>
					</div>
					<div class="clear"></div>
				 </div>
			 	<div class="header-second">
			 		<div class="row">
		 				<div class="col-md-2">
		 					<img src="assets/images/LogoSoKHCN.png" style="height: 100px;"/> 
		 				</div>
		 				<div class="col-md-4">
		 					<div class="F-title text-border">
					 			KHOA HỌC VÀ CÔNG NGHỆ <br/>
					 			THÀNH PHỐ CẦN THƠ
					 		</div>	
		 				</div>
	 				</div>
			 	</div>
			</div>
			<!-- /header -->
			<uc1:MainMenu id="MainMenu1" runat="server" />
            			<div class="content">
<div id="site_wrapper" runat="server"></div>

<!-- content-sixth: liên kết -->
			 	<div class="content-sixth block-content">
					
				<uc3:Slider id="Slider1" runat="server" />
			 	</div>
			 	<!-- /content-sixth -->

<!-- content-seventh: footer -->
			 	<div class="content-seventh block-content">
			 		<div class="row">
			 			<uc2:FootCategories id="FootCategories1" runat="server" />
			 		</div>
			 	</div>
			 	<!-- /content-seventh -->
<div class="content-eighth block-content">
			 		Bản quyền @ 2010 thuộc về Sở Khoa học và Công nghệ thành phố Cần Thơ <br/>
			 		Địa chỉ: Số 02, Lý Thường kiệt, phường Tân An, quận Ninh Kiều, thành phố Cần Thơ <br/>
			 		Điện thoại: 0710 3 820 672; Fax: 0710 3 821 471; Email: sokhcn@cantho.gov.vn <br/>
			 		Trưởng Ban biên tập: Bà Trần Hoài Phương - Phó Giám đốc Sở Khoa học và Công nghệ thành phố Cần Thơ
			 	</div>
			 	<!-- /content-eighth -->
			 	<div>
			 		<a href="#" id="go_top"></a>
			 	</div>
			</div>
			<!-- /content -->
		</div>
		<!-- page-content -->