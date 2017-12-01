<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="van-ban-phap-quy.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Pages.van_ban_phap_quy" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript">
    function gotoPage(pageIndex) {
        var hdfPageIndex = document.getElementById('<%= hdfPageIndex.ClientID %>');
        hdfPageIndex.value = pageIndex;
        document.getElementById('<%= btnSearchHide.ClientID %>').click();
    }
</script>
<asp:HiddenField ID="hdfPageIndex" runat="server" />
<div class="content-first">
			 		<div class="row">
			 			<div class="col-md-3">
			 				<div class="topic-line-bottom">VĂN BẢN PHÁP QUY</div>
			 				<ul class="menu-left">
					            <a href="/default.aspx?pn=van-ban-phap-quy" class="sub-menu-left sub-title">Tất cả văn bản  </a>
					            <a href="#" class="sub-menu-left sub-title">Cơ quan ban hành</a>
                                    <asp:Repeater ID="rptCQBH" runat="server">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hplCOBH" runat="server"  NavigateUrl='<%# "/default.aspx?pn=van-ban-phap-quy&cqbh=" + Eval("Id").ToString() %>' CssClass="sub-menu-left">+ <%# Eval("TenCoQuanBH").ToString() + " (" + Eval("SoLuongVB") + ")"  %>  </asp:HyperLink> 
                                        </ItemTemplate>
                                    </asp:Repeater>
								<a href="#" class="sub-menu-left sub-title">Lĩnh vực </a>
									<asp:Repeater ID="rptLinhVuc" runat="server">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hplLV" runat="server" NavigateUrl='<%# "/default.aspx?pn=van-ban-phap-quy&lv=" + Eval("Id").ToString() %>'  CssClass="sub-menu-left">+ <%# Eval("TenLinhVuc").ToString() + " (" + Eval("SoLuongVB") + ")"%>  </asp:HyperLink> 
                                        </ItemTemplate>
                                    </asp:Repeater>
				            </ul>
			 			</div>
			 			<div class="col-md-9">
                            <div class="topic-line-bottom">Văn bản pháp quy/ 
                                <asp:Label ID="lblVBPQTitle" runat="server"></asp:Label></div>
                            <div id="divGeneral" runat="server">			 				
			 				<div class="sub-form-input" style="border: #dedede solid 1px;">
                                <div class="form-horizontal" id="divSearchCreteria" runat="server">
			 					<div  class="row">
				 						<div class="col-md-6">
				 							<div class="form-group">
												<label class="col-sm-4 control-label">Trích yếu</label>
												<div class="col-sm-8">
												      <asp:TextBox runat="server" CssClass="form-control" ID="txtTrichYeu"></asp:TextBox>
												</div>
											</div>
											<div class="form-group">
												<label class="col-sm-4 control-label">Số hiệu</label>
												<div class="col-sm-8">
												      <asp:TextBox runat="server" CssClass="form-control" ID="txtSoHieu"></asp:TextBox>
												</div>
											</div>
											<div class="form-group">
												<label class="col-sm-4 control-label">Tình trạng</label>
												<div class="col-sm-8">
												      <asp:RadioButton ID="rdbConHL" runat="server" Text="Còn hiệu lực" GroupName="HieuLuc" />  &nbsp; &nbsp;
													  <asp:RadioButton ID="rdbHetHL" runat="server" Text="Hết hiệu lực" GroupName="HieuLuc" />
												</div>
												
											</div>
											<div class="form-group">
												<label class="col-sm-4 control-label">Từ ngày</label>
												<div class="col-sm-8">
                                                    <telerik:RadDatePicker ID="rdpFromDate" runat="server" CssClass="form-control" BorderStyle="None" Culture="Vi-vn">
                                                    </telerik:RadDatePicker>
												</div>
											</div>
				 						</div>
				 						<div class="col-md-6">
				 							<div class="form-group">
												<label class="col-sm-4 control-label">Cơ quan ban hành</label>
												<div class="col-sm-8">
													<asp:DropDownList ID="ddlCQBH" runat="server">
                                                    </asp:DropDownList>
												</div>
											</div>
											<div class="form-group">
												<label class="col-sm-4 control-label">Lĩnh vực văn bản</label>
												<div class="col-sm-8">
													<asp:DropDownList ID="ddlLinhVuc" runat="server">
                                                    </asp:DropDownList> 
												</div>
											</div>
											<div class="form-group">
												<label class="col-sm-4 control-label">Loại văn bản</label>
												<div class="col-sm-8">
												    <asp:DropDownList ID="ddlLoaiVB" runat="server">
                                                    </asp:DropDownList>  
												</div>
											</div>
											<div class="form-group">
												<label class="col-sm-4 control-label">Đến ngày</label>
												<div class="col-sm-8">
												      <telerik:RadDatePicker ID="rdpToDate" runat="server" CssClass="form-control" BorderStyle="None" Culture="Vi-vn">
                                                    </telerik:RadDatePicker>
												</div>
											</div>
				 						</div>
				 						
				 					</div>
									<div style="text-align: center;"><asp:Button ID="btnSearch" runat="server" 
                                            CssClass="btn btn-primary" Text="Tìm kiếm" onclick="btnSearch_Click1"></asp:Button></div>
                                    <asp:Button ID="btnSearchHide" runat="server" Text="" OnClick="btnSearch_Click" Width="0px" style="display: none" />
                                </div>
			 				</div>
			 				<table class="table table-striped">
								<tr>
									<td class="row-table-title">STT</td>
									<td class="row-table-title" style="max-width: 40%; text-align: center">Trích yếu</td>
									<td class="row-table-title">Số hiệu</td>
									<td class="row-table-title">Ngày ban hành</td>
									<td class="row-table-title">Cơ quan ban hành</td>
									<td class="row-table-title">Tải về</td> 
								</tr>
								<tbody>
                                    <asp:Repeater ID="rptVBPQ" runat="server">
                                        <ItemTemplate>
                                            <tr>
										        <td class="row-table-doc"><%# Container.ItemIndex + 1 %></td>
										        <td class="row-table-doc">
											        <asp:HyperLink ID="hplTrichYeu" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl(PageName, "vbid=" + Eval("Id").ToString()) %>'><%# Eval("TrichYeu") %></asp:HyperLink>
										        </td>
										        <td class="row-table-doc"><%# Eval("SoHieu") %></td>
										        <td class="row-table-doc"><%# Eval("NgayBH", "{0:dd/MM/yyyy}")%></td>
										        <td class="row-table-doc"><%# Eval("TenCoQuanBH") %></td>
										        <td class="row-table-doc"><asp:HyperLink ID="hplFileDownload" runat="server" NavigateUrl='<%# Eval("FileDinhKem") %>'><span class="glyphicon glyphicon-download-alt"></span></asp:HyperLink></td> 
									        </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    
								</tbody>
							</table>
                            <hr />
                            <asp:Literal ID="ltrPaging" runat="server"></asp:Literal>
                            </div>

                            <div id="divDetail" runat="server">
                            <asp:Label ID="lblTrichYeu" runat="server"></asp:Label>
							<table class="table table-bordered">
			 					<tr>
									<th class="col-table-title-detail">Số hiệu</th>
									<th><asp:Label ID="lblSoHieu" runat="server"></asp:Label></th>
								</tr>
								<tr>
									<th class="col-table-title-detail">Loại văn bản</th>
									<th><asp:Label ID="lblLoaiVB" runat="server"></asp:Label></th>
								</tr>
								<tr>
									<th class="col-table-title-detail">Lĩnh vực văn bản</th>
									<th><asp:Label ID="lblLinhVuc" runat="server"></asp:Label></th>
								</tr>
								<tr>
									<th class="col-table-title-detail">Cơ quan ban hành</th>
									<th><asp:Label ID="lblCQBH" runat="server"> </asp:Label></th>
								</tr>
								<tr>
									<th class="col-table-title-detail">Ngày ban hành</th>
									<th><asp:Label ID="lblNgayBH" runat="server"></asp:Label></th>
								</tr>
								<tr>
									<th class="col-table-title-detail">Ngày hiệu lực</th>
									<th><asp:Label ID="lblNgayHL" runat="server"></asp:Label></th>
								</tr>
								<tr>
									<th style="width: 200px;">File tải về</th>
									<th><asp:HyperLink ID="hplFileDownload" runat="server"><span class="glyphicon glyphicon-download-alt"></span></asp:HyperLink></th> 
								</tr>
							</table>
							<div class="topic-line-bottom">Văn bản liên quan</div>
			 				<table class="table table-striped">
								<tr>
									<td class="row-table-title">STT</td>
									<td class="row-table-title" style="max-width: 40%; text-align: center">Trích yếu</td>
									<td class="row-table-title">Số hiệu</td>
									<td class="row-table-title">Ngày ban hành</td>
									<td class="row-table-title">Cơ quan ban hành</td>
									<td class="row-table-title">Tải về</td> 
								</tr>
								<tbody>
									<asp:Repeater ID="rptRelatedVB" runat="server">
                                        <ItemTemplate>
                                            <tr>
										        <td class="row-table-doc"><%# Container.ItemIndex + 1 %></td>
										        <td class="row-table-doc">
											        <asp:HyperLink ID="hplTrichYeu" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl(PageName, "vbid=" + Eval("Id").ToString()) %>'><%# Eval("TrichYeu") %></asp:HyperLink>
										        </td>
										        <td class="row-table-doc"><%# Eval("SoHieu") %></td>
										        <td class="row-table-doc"><%# Eval("NgayBH","{0:dd/MM/yyyy}") %></td>
										        <td class="row-table-doc"><%# Eval("TenCoQuanBH") %></td>
										        <td class="row-table-doc"><asp:HyperLink ID="hplFileDownload" runat="server" NavigateUrl='<%# Eval("FileDinhKem") %>'><span class="glyphicon glyphicon-download-alt"></span></asp:HyperLink></td> 
									        </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
								</tbody>
							</table>
							
                                
                            </div>
			 			</div>
			 		</div>
			 	</div>