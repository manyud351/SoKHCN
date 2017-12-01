<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="vbpq_cqbh.ascx.cs" Inherits="KHCNCT.Web.admin.modules.vbpq_cqbh" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="~/admin/Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>
<div id="divUpdate" runat="server">
<strong>CẬP NHẬT CƠ QUAN VĂN BẢN PHÁP QUY</strong>
<table style="width: 100%;" cellpadding="2" cellspacing="2" border="0">
    <tr>
        <td valign="top">
            Tên cơ quan ban hành</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtCQBH" runat="server" Width="500px"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtCQBH" ErrorMessage="*" ForeColor="Red" 
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Ghi chú</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtGhiChu" runat="server" Width="500px" Height="120px" 
                TextMode="MultiLine"></asp:TextBox>
        </td>
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
</div>
<div id="divManage" runat="server">
<strong>CƠ QUAN BAN HÀNH VĂN BẢN PHÁP QUY</strong>
<br />
<asp:LinkButton ID="lkbAddNew" runat="server" onclick="lkbAddNew_Click">Thêm Cơ quan ban hành văn bản</asp:LinkButton>
    <br /><br />
    <asp:GridView ID="grvCQBH" runat="server" DataKeyNames="Id" 
        AutoGenerateColumns="False"
         CellPadding="4" 
        ForeColor="#333333" GridLines="None" onrowdeleting="grvCQBH_RowDeleting">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tên cơ quan ban hành">
                <ItemTemplate>
                    <%# Eval("TenCoQuanBH")%>
                </ItemTemplate>
                <ItemStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ghi chú">
                <ItemTemplate>
                    <%# Eval("GhiChu") %>
                </ItemTemplate>
                <ItemStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sửa">
                <ItemTemplate>
                    <asp:HyperLink ID="lkbEdit" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateAdminUrl("vbpq_cqbh","act=update", "idcq=" + Eval("Id").ToString()) %> '>
                        <asp:Image ID="imgEdit" runat="server" BorderStyle="None" AlternateText="Sửa" ImageUrl="~/images/Edit.png" Width="14px" /> 
                    </asp:HyperLink>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Xóa">
                <ItemTemplate>
                    <asp:LinkButton ID="lkbDelete" runat="server" OnClientClick="return confirm('Bạn có chắc muốn xóa cơ quan này chứ?')" CommandName="Delete">
                        <asp:Image ID="imgDelete" runat="server" BorderStyle="None" AlternateText="Xóa" ImageUrl="~/images/Delete.png" Width="14px" />
                    </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</div>