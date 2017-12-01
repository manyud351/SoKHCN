<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoCate.ascx.cs" Inherits="KHCNCT.Web.admin.modules.VideoCate" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div id="divManage" runat="server">
    <asp:LinkButton ID="lkbAddNewCate" runat="server" onclick="lkbAddNewCate_Click">Thêm danh mục Video</asp:LinkButton>
    <br /><br />
    <asp:GridView ID="grvVideoCategory" runat="server" DataKeyNames="Id" 
        AutoGenerateColumns="False"
        onrowcancelingedit="grvVideoCategory_RowCancelingEdit" 
        onrowediting="grvVideoCategory_RowEditing" 
        onrowupdating="grvVideoCategory_RowUpdating" 
        onrowdeleting="grvVideoCategory_RowDeleting" CellPadding="4" 
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Danh mục Video">
                <ItemTemplate>
                    <%# Eval("CategoryName")%>
                </ItemTemplate>
                <ItemStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Thứ tự">
                <ItemTemplate>
                    <%# Eval("Order") %>
                </ItemTemplate>
                <ItemStyle Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hiển thị trang chủ">
                <ItemTemplate>
                    <asp:CheckBox ID="ckbShowHomePage" runat="server" Checked='<%# Convert.ToBoolean(Eval("ShowInHomepage")) %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sửa">
                <ItemTemplate>
                    <asp:HyperLink ID="lkbEdit" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateAdminUrl("videocate","act=update", "cvid=" + Eval("Id").ToString()) %> '>
                        <asp:Image ID="imgEdit" runat="server" BorderStyle="None" AlternateText="Sửa" ImageUrl="~/images/Edit.png" Width="14px" /> 
                    </asp:HyperLink>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Xóa">
                <ItemTemplate>
                    <asp:LinkButton ID="lkbDelete" runat="server" OnClientClick="return confirm('Bạn có chắc muốn xóa danh mục video này chứ?')" CommandName="Delete">
                        <asp:Image ID="imgDelete" runat="server" BorderStyle="None" AlternateText="Sửa" ImageUrl="~/images/Delete.png" Width="14px" />
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
<div id="divUpdate" runat="server">
    <table border="0" width="100%">
        <tr>
           <td>Chuyên mục </td>
           <td>&nbsp;</td>
           <td>
               <asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox>
           </td>
        </tr>
        <tr>
           <td>Hiển thị trang chủ </td>
           <td>&nbsp;</td>
           <td>
               <asp:CheckBox ID="ckbShowHomePage" runat="server" />
           </td>
        </tr>
        <tr>
           <td>Thứ tự hiển thị</td>
           <td>&nbsp;</td>
           <td>
               <telerik:RadNumericTextBox ID="rntOrder" runat="server" NumberFormat-DecimalDigits="0">
               </telerik:RadNumericTextBox></td>
        </tr>
        <tr>
           <td>&nbsp;</td>
           <td>&nbsp;</td>
           <td>
               <asp:Button ID="btnSave" runat="server" Text="Lưu lại" 
                   onclick="btnSave_Click" /> 
               &nbsp;&nbsp;
               <asp:Button ID="btnCancel" runat="server" Text="Hủy bỏ" 
                   onclick="btnCancel_Click" /> 
               </td>
        </tr>
    </table>
</div>