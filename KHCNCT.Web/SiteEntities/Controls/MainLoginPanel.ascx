<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainLoginPanel.ascx.cs"   Inherits="KHCNCT.Web.SiteEntities.Controls.MainLoginPanel" %>
<div class="box1-sidebar-left">
    <div class="box1-sidebar-left-title">
        <div class="box1-sidebar-left-title-r">
        </div>
        <div class="box1-sidebar-left-title-l">
        </div>
        <div class="box1-sidebar-left-title-bg">
            <h3>
                Đăng nhập</h3>
        </div>
    </div>
    <div class="box1-sidebar-left-content">

        <div id="divLogin" runat="server">
            <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
            Username:<br />
            <asp:TextBox ID="txtUsername" runat="server" Width="90%" 
                ValidationGroup="MainLoginPanel"></asp:TextBox>
            <br />
            Pass:<br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="90%" 
                ValidationGroup="MainLoginPanel"></asp:TextBox>
            <br />
            <input type="checkbox" id="ckbRememberMe" name="ckbRememberMe" />Nhớ mật khẩu
            <br />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" 
                OnClick="btnLogin_Click" ValidationGroup="MainLoginPanel" />
            <br />
            <asp:HyperLink ID="hplRegister" runat="server">Đăng ký mới</asp:HyperLink> &nbsp;&nbsp;
            <asp:HyperLink ID="hplForgotPass" runat="server">Quên mật khẩu?</asp:HyperLink>
        </div>
        <div id="divLogined" runat="server" style="text-align: center">
            <asp:HyperLink ID="hplUserPage" runat="server">Trang cá nhân</asp:HyperLink><br />
            (số đơn hàng: )<br />
            <asp:HyperLink ID="hplLogout" runat="server">Thoát</asp:HyperLink><br />
        </div>

    </div>
    <div class="box1-sidebar-left-bottom"></div>
</div>
<div class="clear">
</div>
