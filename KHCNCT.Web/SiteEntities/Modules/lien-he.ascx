<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="lien-he.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Modules.lien_he" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="date-title">
    <%= DateTime.Now.ToString("dd/HH/yyyy HH:mm") %></div>
<!-- Tin chi tiết -->
<div class="topic-line-bottom">
    LIÊN HỆ</div>
<div class="feedback">
    <div class="user-title">SỞ KHOA HỌC VÀ CÔNG NGHỆ THÀNH PHỐ CẦN THƠ</div>
				 				<div class="row" >
				 					<p style="text-align: center;">
				 						<img src="assets/images/img-sokhcn.jpg" style="width:80%;" /> 
				 					</p>
				 				</div
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Họ tên (*)</label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control" 
                ValidationGroup="GopY"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Số điện thoại</label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtDienThoai" runat="server" CssClass="form-control" 
                style="height: 25px" ValidationGroup="GopY"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Email (*)</label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" 
                ValidationGroup="GopY"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Địa chỉ</label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" 
                ValidationGroup="GopY"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Nội dung (*)</label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtNoiDung" runat="server" CssClass="form-control" 
                TextMode="MultiLine" ValidationGroup="GopY"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Nhập mã an ninh (*)</label>
        <div class="col-sm-10">
            <telerik:RadCaptcha ID="RadCaptcha1" runat="server" EnableRefreshImage="True" CaptchaTextBoxLabel=""
                                            CssClass="validator_error_message" CaptchaLinkButtonText="Đổi mã khác"  ValidationGroup="GopY"
                                            ErrorMessage="Sai mã bảo mật, hãy nhập lại">
                                        </telerik:RadCaptcha>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10">
            <asp:Button ID="btnSend" runat="server" Csslass="btn btn-primary" Text="Gửi đi"  ValidationGroup="GopY"
                onclick="btnSend_Click" />
        </div>
    </div>
</div>
