<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="thu-tuc-hanh-chinh.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Pages.thu_tuc_hanh_chinh" %>
<!-- content-first: nội dung chính -->
			 	<div class="content-first">
			 		<div class="row">
			 			<div class="col-md-4">
			 				<div class="topic-line-bottom">THỦ TỤC HÀNH CHÍNH</div>
			 				<ul class="menu-left">
					            <asp:Repeater id="rptDanhMucThuTuc" runat="server">
                                <ItemTemplate>
                                    <asp:HyperLink id="hplTenDanhMuc" runat="server" CssClass="sub-menu-left sub-title"  NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl("thu-tuc-hanh-chinh","dmttid=" + Eval("Id").ToString()) %>'><%# Eval("TenDanhMuc")%></asp:HyperLink>
                                </ItemTemplate>
                            </asp:Repeater>
							</ul>
			 			</div>
			 			<div class="col-md-8">
			 				<div class="topic-line-bottom"><asp:Label ID="lblCaption" runat="server"></asp:Label></div>
			 				<div class="sub-form-input" style="border: #dedede solid 0px;">
                                <asp:GridView ID="grvThuTucHanhChinh" runat="server" AllowPaging="true" 
                                    AutoGenerateColumns="false" GridLines="None" PageSize="10" 
                                    onpageindexchanging="grvThuTucHanhChinh_PageIndexChanging" Width="100%" 
                                    onrowcommand="grvThuTucHanhChinh_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>                                            
                                                <div class="row-title-hanhchinh">
										            <asp:LinkButton id="lkbTenThuTuc" runat="server" CommandName="ViewDetail" CommandArgument='<%# Eval("Id") %>'><span class="title-hanhchinh"><%# Eval("TenThuTuc") %></span></asp:LinkButton>
                                                    <br/>
										            <div style="padding-left: 20px;">
											            <span> Ngày cập nhật: <%# Eval("NgayTao","{0:dd/MM/yyyy}") %></span><br />
											            Tài liệu đính kèm 
                                                        <asp:LinkButton ID="lkbViewFile" runat="server" CommandName="Download" CommandArgument='<%# Eval("Id") %>'><span class="glyphicon glyphicon-download-alt"></span></asp:LinkButton>
											             &nbsp;&nbsp;
											            Lượt xem: <%# Eval("ViewCount") %>
                                                        &nbsp;&nbsp;
                                                        Lượt tải: <%# Eval("DownloadCount")%>
										            </div>										
										            <div style="padding-bottom: 10px;"></div>
									            </div>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                 <asp:Panel ID="pnlViewDetail" runat="server">
                                    
                                    <div class="row-title-hanhchinh">
										<span class="title-hanhchinh"><asp:Label ID="lblTenThuTuc" runat="server"></asp:Label></span>&nbsp;&nbsp;
                                        <span><asp:Label ID="lblNgayTao" runat="server"></asp:Label></span> 
                                        Tài liệu đính kèm 
                                        <asp:LinkButton ID="lkbDownload" runat="server">
                                            <span class="glyphicon glyphicon-download-alt"></span> 
                                        </asp:LinkButton>
                                        &nbsp;&nbsp;
										Lượt xem: <asp:Label ID="lblViewCount" runat="server"></asp:Label>&nbsp;&nbsp;Lượt tải: <asp:Label ID="lblDownloadCount" runat="server"></asp:Label>
										<div class="detail-hanhchinh">
											<asp:Label ID="lblDetail" runat="server"></asp:Label>
										</div>										
										<div style="padding-bottom: 10px;"></div>
									</div>	

                                 </asp:Panel>
    						</div>
			 			</div>
			 		</div>
			 	</div>
