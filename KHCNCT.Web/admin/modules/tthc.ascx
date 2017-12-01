<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tthc.ascx.cs" Inherits="KHCNCT.Web.admin.modules.tthc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="~/admin/Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>
<strong>CẬP NHẬT THỦ TỤC HÀNH CHÍNH</strong>
<table style="width: 100%;" cellpadding="2" cellspacing="2" border="0">
    <tr>
        <td valign="top">
            Tên thủ tục</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtTenThuTuc" runat="server" Width="500px"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtTenThuTuc" ErrorMessage="*" ForeColor="Red" 
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Danh mục thủ tục</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:DropDownList ID="ddlDanhMuc" runat="server" Width="500px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="ddlDanhMuc" ErrorMessage="*" ForeColor="Red" InitialValue=""
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Mô tả thủ tục</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtMoTa" runat="server" Height="120px" TextMode="MultiLine" 
                Width="500px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtMoTa" ErrorMessage="*" ForeColor="Red" 
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Chi tiết thủ tục</td>
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
            <asp:HyperLink ID="hplFileThuTuc" runat="server"></asp:HyperLink>
            <asp:FileUpload ID="fulThuTuc" runat="server" />
            <asp:RequiredFieldValidator ID="rfvThuTuc" runat="server" 
                ControlToValidate="fulThuTuc" ErrorMessage="*" ForeColor="Red" 
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
