<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="vbpq.ascx.cs" Inherits="KHCNCT.Web.admin.modules.vbpq" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="~/admin/Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>
<strong>CẬP NHẬT VĂN BẢN PHÁP QUY</strong>
<table style="width: 100%;" cellpadding="2" cellspacing="2" border="0">
    <tr>
        <td valign="top">
            Số hiệu văn bản</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtSoHieuVB" runat="server" Width="500px"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtSoHieuVB" ErrorMessage="*" ForeColor="Red" 
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Trích yếu</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtTrichYeu" runat="server" Width="500px" Height="120px" 
                TextMode="MultiLine"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="txtTrichYeu" ErrorMessage="*" ForeColor="Red" 
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Lĩnh vực</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:DropDownList ID="ddlLinhVuc" runat="server" Width="500px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="ddlLinhVuc" ErrorMessage="*" ForeColor="Red" InitialValue=""
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Cơ quan ban hành</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:DropDownList ID="ddlCQBH" runat="server" Width="500px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="ddlCQBH" ErrorMessage="*" ForeColor="Red" InitialValue=""
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Loại văn bản</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:DropDownList ID="ddlLoaiVB" runat="server" Width="500px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ControlToValidate="ddlLoaiVB" ErrorMessage="*" ForeColor="Red" InitialValue=""
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Ngày ban hành</td>
        <td>
            &nbsp;
        </td>
        <td>
            <telerik:RadDatePicker ID="rdpNgayBH" Runat="server">
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="rdpNgayBH" ErrorMessage="*" ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Ngày hiệu lực</td>
        <td>
            &nbsp;
        </td>
        <td>
            <telerik:RadDatePicker ID="rdpNgayHL" Runat="server">
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ControlToValidate="rdpNgayHL" ErrorMessage="*" ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Tình trạng</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:RadioButton ID="rdConHL" runat="server" Text="Còn hiệu lực" GroupName="HieuLuc"/>
            <asp:RadioButton ID="RadioButton1" runat="server" Text="Hết hiệu lực" GroupName="HieuLuc"/>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Chi tiết văn bản</td>
        <td>
            &nbsp;</td>
        <td>
            <uc1:Editor ID="Editor1" runat="server" />
        </td>
    </tr>
    <tr>
        <td valign="top">
            File đính kèm</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:HyperLink ID="hplFileVanBan" runat="server"></asp:HyperLink>
            <asp:FileUpload ID="fulVanBan" runat="server" />
            <asp:RequiredFieldValidator ID="rfvVanBan" runat="server" 
                ControlToValidate="fulVanBan" ErrorMessage="*" ForeColor="Red" 
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td valign="top">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Lưu lại" 
                onclick="btnSave_Click" />
&nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Hủy bỏ" 
                onclick="btnCancel_Click" />
        </td>
    </tr>
</table>
