<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="user.ascx.cs" Inherits="KHCNCT.admin.modules.SUser.suser" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .style1
    {
        height: 44px;
    }
</style>
<script type="text/javascript">
    function gotoPage(pageIndex) {
        var hdfPageIndex = document.getElementById('<%= hdfPageIndex.ClientID %>');
        hdfPageIndex.value = pageIndex;
        document.getElementById('<%= btnSearch.ClientID %>').click();
    }

    
</script>

<div id="divEditUser" runat="server">
    <h3>CẬP NHẬT THÔNG TIN NGƯỜI DÙNG</h3>
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                Tên đăng nhập
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblUsername" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Email
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblEmail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Họ lót
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtHoLot" runat="server" ValidationGroup="RegisterUser"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp; tên
                <asp:TextBox ID="txtTen" runat="server" ValidationGroup="RegisterUser"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvHoLot" runat="server" ControlToValidate="txtHoLot"
                    ErrorMessage="Bạn phải nhập họ lót. " SetFocusOnError="True" Display="Dynamic"
                    ValidationGroup="RegisterUser" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvTen" runat="server" Display="Dynamic" ErrorMessage="Bạn phải nhập tên"
                    ControlToValidate="txtTen" SetFocusOnError="True" 
                    ValidationGroup="RegisterUser" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Mật khẩu truy nhập
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:HyperLink ID="hplChangePass" runat="server">Đổi mật khẩu</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                Địa chỉ
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Điện thoại
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server" MaxLength="11"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
            </td>
            <td class="style1">
            </td>
            <td class="style1">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="   Lưu lại   " />
                &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="   Huỷ bỏ   " />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</div>

<div id="divManageUsers" runat="server">
<div style="width:100%; text-align: right">
<asp:HyperLink ID="hplAddNew" runat="server"><img src="/images/User.png" border="0" height="12px" width="12px" /> Thêm mới tài khoản</asp:HyperLink>
</div>
<h3>Tìm kiếm người dùng</h3>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>Tên</td>
        <td>
            <asp:TextBox ID="txtFiterName" runat="server" Width="220px"></asp:TextBox>
        </td>
        <td>Tên đăng nhập</td>
        <td>
            <asp:TextBox ID="txtFilterUsername" runat="server" Width="220px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Trạng thái</td>
        <td colspan="3">
            <asp:DropDownList ID="ddlFilterBlock" runat="server">
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="Đang khoá" Value="1"></asp:ListItem>
                <asp:ListItem Text="Đang sử dụng" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Đăng ký từ ngày</td>
        <td>
            
            <telerik:RadDatePicker ID="rdpFilterFromDate" Runat="server">
            </telerik:RadDatePicker>
            
        </td>
        <td>đến ngày</td>
        <td>
            
            <telerik:RadDatePicker ID="rdpFilterToDate" Runat="server">
            </telerik:RadDatePicker>
            
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>        
        <td colspan="2" align="center">
            <asp:Button ID="btnSearch" runat="server" Text="   Tìm kiếm   " 
                onclick="btnSearch_Click" /></td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<br />
    <asp:Literal ID="ltrTotalResult" runat="server"></asp:Literal>
<br />
<hr />
<br />
    
    <br />
    <asp:HiddenField ID="hdfPageIndex" runat="server" />
    <asp:GridView ID="grvManageUsers" runat="server" AutoGenerateColumns="false" Width="100%"
        DataKeyNames="UserId" OnRowDeleting="grvManageUsers_RowDeleting" OnRowEditing="grvManageUsers_RowEditing"
        OnRowDataBound="grvManageUsers_RowDataBound" OnRowUpdating="grvManageUsers_RowUpdating" OnRowCancelingEdit="grvManageUsers_RowCancelingEdit"
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px">
        <HeaderStyle BackColor="#dddddd" Height="25px" Font-Bold="True" 
        ForeColor="Black" />
        <AlternatingRowStyle BackColor="#efefef" />
        <Columns>
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate>
                    <%# (hdfPageIndex.Value != "") ? (Convert.ToInt32(hdfPageIndex.Value) - 1) * pageSize + Container.DataItemIndex + 1 : Container.DataItemIndex + 1%>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sửa">
                <ItemTemplate>
                    <asp:HyperLink ID="hplEdit" runat="server" NavigateUrl='<%# "~/admin/?mod=user&act=edit&uid=" + Eval("UserId").ToString() %>'>
                        <asp:Image ToolTip="Sửa" ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif"
                            BorderStyle="None" Width="14px" />
                    </asp:HyperLink>
                </ItemTemplate>
                <ItemStyle Font-Strikeout="false" Width="50px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reset pass">
                <ItemTemplate>
                    <asp:HyperLink ID="hplResetPass" runat="server" NavigateUrl='<%# "~/admin/?mod=user&act=resetpass&uid=" + Eval("UserId").ToString() %>'>
                        <asp:Image ToolTip="Đổi mật khẩu" ID="imgResetPass" runat="server" ImageUrl="~/images/Key.png"
                            BorderStyle="None" Width="14px" />
                    </asp:HyperLink>
                </ItemTemplate>
                <ItemStyle Font-Strikeout="false" Width="50px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Xoá">
                <ItemTemplate>
                    <asp:ImageButton ToolTip="Xóa người dùng khỏi gian hàng" ID="imbDelete" runat="server" ImageUrl="~/images/delete.gif"
                        BorderStyle="None" Width="14px" CommandName="Delete" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa người dùng này chứ?')" />
                </ItemTemplate>
                <ItemStyle Font-Strikeout="false" Width="50px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UserID">
                <ItemTemplate>
                    <%# Eval("UserId")%>
                    <asp:HiddenField ID="hdfUserId" runat="server" Value='<%# Eval("UserId") %>' />
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UserAccount">
                <ItemTemplate>
                    <%# Eval("AccountName")%>
                </ItemTemplate>
                <ItemStyle Width="140px" Font-Bold="true"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Họ tên">
                <ItemTemplate>
                    <%# Eval("FirstSurName")%>
                    <%# Eval("LastName") %>
                </ItemTemplate>
                 <ItemStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ngày đăng ký">
                <ItemTemplate>
                    <%# Eval("CreatedTime", "{0:dd/MM/yyyy HH:mm}")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Khoá">
                <ItemTemplate>
                    <asp:ImageButton ID="imbBlock" runat="server" CommandName="Edit" Width="14px" /><br />
                    <asp:Literal ID="ltrBlockInfo" runat="server"></asp:Literal>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"  Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phân quyền hệ thống">
                <ItemTemplate>
                    <asp:HyperLink ID="hplRight" runat="server" NavigateUrl='<%# "~/admin/default.aspx?mod=user&act=right&uid=" + Eval("UserId").ToString() %>'>
                        <asp:Image ToolTip="Phân quyền" ID="imgRight" runat="server" ImageUrl="~/admin/assets/images/User.png"
                            BorderStyle="None" Width="14px" />
                    </asp:HyperLink>
                 </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phân công chủ đề">
                <ItemTemplate>
                    <asp:HyperLink ID="hplCate" runat="server" NavigateUrl='<%# "~/admin/default.aspx?mod=user&act=cate&uid=" + Eval("UserId").ToString() %>'>
                        <asp:Image ToolTip="Phân quyền" ID="imgCate" runat="server" ImageUrl="~/admin/assets/images/categories-300x225.jpg"
                            BorderStyle="None" Width="20px" Height="14px" />
                    </asp:HyperLink>
                 </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"   Width="120px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <div style="width:100%; text-align: center">
        <asp:Literal ID="ltrPaging" runat="server"></asp:Literal>
    </div>
</div>

<div id="divAddNew" runat="server">
<br />
<blockquote>
<span style="font-style: italic; color: blue">
Chức năng này cho phép thêm một tài khoản người dùng riêng cho gian hàng (<strong>Lưu ý:</strong> tai khoản này chỉ có giá trị riêng trong gian hàng này).
</span>
</blockquote>
<br />

    <h3>THÊM MỚI TÀI KHOẢN NGƯỜI DÙNG</h3>
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <b>TẠO TÀI KHOẢN NGƯỜI DÙNG</b>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div id="username_status" style="width: 400px; float: left">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Tên đăng nhập
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Literal ID="ltrUsernamePrefix" runat="server"></asp:Literal>
                <asp:TextBox ID="txtUsername2" runat="server" ValidationGroup="RegisterUser"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername2"
                    ErrorMessage="Bạn phải nhập Username. " SetFocusOnError="True" Display="Dynamic"
                    ValidationGroup="RegisterUser" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Email
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtEmail2" runat="server" ValidationGroup="RegisterUser"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail2"
                    ErrorMessage="Bạn phải nhập email. " SetFocusOnError="True" Display="Dynamic"
                    ValidationGroup="RegisterUser" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Họ lót
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtHoLot2" runat="server" ValidationGroup="RegisterUser"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp; tên
                <asp:TextBox ID="txtTen2" runat="server" ValidationGroup="RegisterUser"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHoLot2"
                    ErrorMessage="Bạn phải nhập họ lót. " SetFocusOnError="True" Display="Dynamic"
                    ValidationGroup="RegisterUser" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ErrorMessage="Bạn phải nhập tên" ControlToValidate="txtTen2" SetFocusOnError="True"
                    ValidationGroup="RegisterUser" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Mật khẩu truy nhập
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" ValidationGroup="RegisterUser"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword2" runat="server" Display="Dynamic" ErrorMessage="Bạn phải nhập mật khẩu"
                    ControlToValidate="txtPassword2" SetFocusOnError="True" 
                    ValidationGroup="RegisterUser" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Nhập lại mật khẩu
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtReTypePassword2" runat="server" TextMode="Password" ValidationGroup="RegisterUser"></asp:TextBox>
                <asp:CompareValidator ID="cpvPassword2" runat="server" ControlToCompare="txtPassword2"
                    ControlToValidate="txtReTypePassword2" Display="Dynamic" ErrorMessage="Mật khẩu nhập lại không khớp"
                    ValidationGroup="RegisterUser" ForeColor="Red"></asp:CompareValidator>
            </td>
        </tr>
        
        <tr>
            <td>
                Địa chỉ
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtDiaChi2" runat="server" ValidationGroup="RegisterUser"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Điện thoại
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtDT2" runat="server" MaxLength="11" ValidationGroup="RegisterUser"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Phải kích hoạt
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="ckbMustActivate" runat="server" Text="Người dùng phải kích hoạt tài khoản" />
            </td>
        </tr>
        <tr>
            <td>
                Thông báo
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="ckbSendAlertCreateUserEmail" runat="server" Text="Gửi mail thông báo" />
            </td>
        </tr>
        <tr>
            <td class="style1">
            </td>
            <td class="style1">
            </td>
            <td class="style1">
                <asp:Button ID="btnSaveNewUser" runat="server" ValidationGroup="RegisterUser" Text="   Lưu lại   "
                    OnClick="btnSaveNewUser_Click" />
                &nbsp;<asp:Button ID="btnCancelAddNewUser" runat="server" Text="   Huỷ bỏ   " CausesValidation="false"
                    OnClick="btnCancelAddNewUser_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</div>

<div id="divChangePass" runat="server">
    <h3>ĐỔI MẬT KHẨU NGƯỜI DÙNG</h3>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label><br />
    <div style="width: 150px; float: left">
        Username:
    </div>
    <div style="float: left">
        <asp:Label ID="lblUsername3" runat="server" Font-Bold="true"></asp:Label>
    </div>
    <div style="clear: both">
    </div>
    <div style="width: 150px; float: left">
        Mật khẩu mới:
    </div>
    <div style="float: left">
        <asp:TextBox ID="txtNewPass3" runat="server" TextMode="Password" ValidationGroup="ChangePass"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Bạn phải nhập mật khẩu mới"
            ControlToValidate="txtNewPass3" Display="Dynamic" SetFocusOnError="True" 
            ValidationGroup="ChangePass" ForeColor="Red"></asp:RequiredFieldValidator>
    </div>
    <div style="clear: both">
    </div>
    <div style="width: 150px; float: left">
        Nhập kiểm tra:
    </div>
    <div style="float: left">
        <asp:TextBox ID="txtRetypePass3" runat="server" TextMode="Password" ValidationGroup="ChangePass"></asp:TextBox>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Mật khẩu nhập lại không khớp, hãy thử lại"
            ControlToCompare="txtNewPass3" ControlToValidate="txtRetypePass3" Display="Dynamic"
            SetFocusOnError="True" ValidationGroup="ChangePass" ForeColor="Red"></asp:CompareValidator>
    </div>
    <div style="clear: both">
    </div>
    <div style="width: 150px; float: left">
        Gửi mail thông báo:
    </div>
    <div style="float: left">
        <asp:CheckBox ID="ckbSendAlertChangePassEmail" runat="server" />
    </div>
    <div style="clear: both">
    </div>
    <br />
    <asp:Button ID="btnChangePass" runat="server" Text="Đổi mật khẩu" ValidationGroup="ChangePass"
        OnClick="btnChangePass_Click" />
    &nbsp;<asp:Button ID="btnCancelAddNewUser0" runat="server" Text="   Trở về   " CausesValidation="false"
        OnClick="btnCancelAddNewUser_Click" />
</div>

<div id="divUserRight" runat="server">
<h1>PHÂN QUYỂN TRUY CẬP HỆ THỐNG</h1>
<asp:Label ID="lblUsername4" runat="server" Font-Bold="true"></asp:Label>
<hr />
    <telerik:RadTreeView ID="rtvAdminMod" Runat="server" Height="450px"  CheckBoxes="true"  BorderWidth="1px"
                    SingleExpandPath="False">
                </telerik:RadTreeView>
    <asp:Button ID="btnSaveRight" runat="server" Text=" Lưu lại " 
        onclick="btnSaveRight_Click" />
&nbsp;&nbsp;
    <asp:Button ID="btnRightCancel" runat="server" onclick="btnRightCancel_Click" 
        Text="Trở lại" />
</div>

<div id="divUserCate" runat="server">
<h1>PHÂN CÔNG CHỦ ĐỀ</h1>
<asp:Label ID="lblUsername5" runat="server" Font-Bold="true"></asp:Label>
<hr />
    <telerik:RadTreeView ID="rtvCategory" Runat="server" Height="450px" BorderWidth="1px" CheckBoxes="true"
                    SingleExpandPath="False">
                </telerik:RadTreeView>
    <asp:Button ID="btnSaveCate" runat="server" Text=" Lưu lại " onclick="btnSaveCate_Click" 
        />
&nbsp;&nbsp;
    <asp:Button ID="btnCateCancel" runat="server" onclick="btnRightCancel_Click" 
        Text="Trở lại" />
</div>