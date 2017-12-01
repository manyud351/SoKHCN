<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="KHCNCT.Admin.modules.Login" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .login-style1
    {
        width: 120px;
        height: 15px;
    }
    .login-style2
    {
        height: 15px;
    }
    .login-style3
    {
        height: 15px;
    }
</style>
<div style="border:1px solid  #CCCCCC; width: 450px; min-height: 180px;
    margin: auto auto">
    <div style="background-image: url('../../images/Box.png'); height: 38px; width: 100%">
        <img alt="" src="../../images/lockscreen.png"  style="float: left; margin-top: 10px; margin-left: 10px"/>
        <div style="float: left; margin: 10px; width: 350px; text-align: center">            
            <h3>ĐĂNG NHẬP HỆ THỐNG</h3>
        </div>
        <div style="clear: both"></div>
    </div>
    <div style="margin: 10px auto; width: 98%; text-align:left">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <table cellpadding="0" cellspacing="0" border="0" width="100%" 
            style="height: 171px">
            <tr>
                <td style="width:150px" rowspan="3">
                    <img alt="" src="../../images/login.png" />
                </td>
                <td class="login-style1">
                    <strong>Username:</strong>
                </td>
                <td class="login-style2">
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="login-style3">
                    <strong>Pass:</strong>
                </td>
                <td class="login-style3">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="login-style3">
                    <strong>Mã bảo mật:</strong>
                </td>
                <td class="login-style3">
                   <telerik:RadCaptcha ID="RadCaptcha1" runat="server" EnableRefreshImage="True" CaptchaTextBoxLabel=""
                                            CssClass="validator_error_message" CaptchaLinkButtonText="Đổi mã khác" 
                                            ErrorMessage="Sai mã bảo mật, hãy nhập lại">
                                        </telerik:RadCaptcha>
                </td>
            </tr>
            <tr>
                <td valign="top">
                </td>
                <td valign="top">
                    <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" OnClick="btnLogin_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>
    </div>
</div>
