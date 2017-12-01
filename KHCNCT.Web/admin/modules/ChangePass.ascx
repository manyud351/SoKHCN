<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePass.ascx.cs"
    Inherits="EStore.admin.modules.ChangePass" %>
<asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label><br />
<div style="width:150px; float: left">
Mật khẩu cũ:
</div>
<div style="float: left">
<asp:TextBox ID="txtOldPass" runat="server" TextMode="Password"  
    ValidationGroup="ChangePass"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
    ErrorMessage="Bạn phải nhập mật khẩu cũ" ControlToValidate="txtOldPass" 
    Display="Dynamic" SetFocusOnError="True" ValidationGroup="ChangePass"></asp:RequiredFieldValidator>
</div>
<div style="clear: both"></div>
<div style="width:150px; float: left">
Mật khẩu mới:
</div>
<div style="float: left">
<asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" 
    ValidationGroup="ChangePass"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ForeColor="Red"
    ErrorMessage="Bạn phải nhập mật khẩu mới" ControlToValidate="txtNewPass" 
    Display="Dynamic" SetFocusOnError="True" ValidationGroup="ChangePass"></asp:RequiredFieldValidator>
</div>
<div style="clear: both"></div>
<div style="width:150px; float: left">
Nhập kiểm tra:
</div>
<div style="float: left">
<asp:TextBox ID="txtRetypePass" runat="server" TextMode="Password" 
    ValidationGroup="ChangePass"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ForeColor="Red"
    ErrorMessage="Bạn phải nhập lại mật khẩu mới" ControlToValidate="txtRetypePass" 
    Display="Dynamic" SetFocusOnError="True" ValidationGroup="ChangePass"></asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator1" runat="server" 
    ErrorMessage="Mật khẩu nhập lại không khớp, hãy thử lại"  ForeColor="Red"
    ControlToCompare="txtNewPass" ControlToValidate="txtRetypePass" 
    Display="Dynamic" SetFocusOnError="True" ValidationGroup="ChangePass"></asp:CompareValidator>
</div>
<div style="clear: both"></div>
<br />
<asp:Button ID="btnChangePass" runat="server" Text="Đổi mật khẩu" 
    OnClick="btnChangePass_Click" ValidationGroup="ChangePass" />
