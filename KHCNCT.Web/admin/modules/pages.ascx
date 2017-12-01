<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pages.ascx.cs" Inherits="KHCNCT.admin.modules.pages" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlAddNew" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                Tên trang
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtPageName" runat="server" 
                    ValidationGroup="UpdateCategory" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtPageName" Display="Dynamic" 
                    ErrorMessage="Bạn phải nhập tên trang" ForeColor="Red" 
                    SetFocusOnError="True" ValidationGroup="UpdateCategory"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Trang cha
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadComboBox ID="rcbParentPage" runat="server" Width="300px" ValidationGroup="UpdateCategory"></telerik:RadComboBox>
            </td>
        </tr>
        <%--<tr>
            <td>
                URL
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" 
                    ValidationGroup="UpdateCategory"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtCategoryName" Display="Dynamic" 
                    ErrorMessage="Bạn phải nhập tên danh mục" ForeColor="Red" 
                    SetFocusOnError="True" ValidationGroup="UpdateCategory"></asp:RequiredFieldValidator>
            </td>
        </tr>--%>
        <tr>
        <td valign="top">
            Chuyên mục tin
        </td>
        <td>
            &nbsp;</td>
        <td>
            <div style="max-height: 150px; overflow: auto; width: 400px;">
                    <telerik:RadComboBox ID="rcbCategories" runat="server" Width="300px">
                    </telerik:RadComboBox>
                </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rcbCategories"
                    ErrorMessage="Bạn phải chọn một danh mục" SetFocusOnError="True" Display="Dynamic"
                    Font-Bold="True" ForeColor="Red" ValidationGroup="UpdateNews"></asp:RequiredFieldValidator>
        </td>
    </tr>      
        <tr>
            <td>
                Thứ tự hiển thị
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadNumericTextBox ID="rntOrder" runat="server" Width="40px" 
                    ValidationGroup="UpdateCategory"  NumberFormat-DecimalDigits="0">
                </telerik:RadNumericTextBox>
                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                    MinimumValue="1" MaximumValue="9999" Type="Integer"
                                    ControlToValidate="rntOrder" Display="Dynamic"                                     
                    ErrorMessage="Giá trị nhập vào phải lớn hơn 0 và nhỏ hơn hoặc bằng 9999" ForeColor="Red" 
                                    SetFocusOnError="True" ValidationGroup="UpdateCategory"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td>
                Biểu tượng
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <div id="divIcon" runat="server" style="width: 100%">
                    <asp:Image ID="imgIcon" runat="server" Width="24px" Height="24px" />
                    <br />
                    <asp:LinkButton ID="lkbDeleteIcon" runat="server" CausesValidation="false" 
                        onclick="lkbDeleteIcon_Click">Xoá ảnh</asp:LinkButton>
                </div>
                <asp:FileUpload ID="fulIcon" runat="server"  /> <span class="admin-desc">Chỉ hỗ trợ các định dạng ảnh: .gif, .jpg, .jpeg, .png, .bmp</span>
                <asp:RegularExpressionValidator ID="revIcon" runat="server"
                    ErrorMessage="Định dạng ảnh không phù hợp!"
                    ControlToValidate="fulIcon"                    
                    ValidationExpression=".*(\.jpg|\.JPG|\.png|\.PNG|\.gif|\.GIF|\.bmp|\.BMP)$" 
                    Display="Dynamic" SetFocusOnError="True" ValidationGroup="UpdateCategory"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Hiển thị menu:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="ckbMainMenu" runat="server" Text="Menu trang chủ" /> &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
            <td>                
                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Lưu lại" 
                    ValidationGroup="UpdateCategory" />                
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Huỷ bỏ" 
                    onclick="btnCancel_Click" CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="UpdateCateOrder" Font-Bold="true" ForeColor="Red" DisplayMode="BulletList" />
<asp:GridView Width="100%" ID="grvCategories" runat="server" 
    AutoGenerateColumns="False"  DataKeyNames="Id"
        onrowdeleting="grvCategories_RowDeleting" 
        ShowFooter="true"
    onrowediting="grvCategories_RowEditing" 
    onrowcommand="grvCategories_RowCommand" 
    onrowdatabound="grvCategories_RowDataBound" AllowPaging="True" 
    onpageindexchanging="grvCategories_PageIndexChanging" PageSize="30">
    <AlternatingRowStyle BackColor="#DEDEDE" />
    <Columns>
        <asp:TemplateField HeaderText="Sửa">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="30px" />
            <ItemTemplate>
                <asp:LinkButton ID="lkbEdit" runat="server" CommandName="Edit">
                    <asp:Image  ToolTip="Sửa" ID="imgSua" runat="server" ImageUrl="~/images/edit.gif"  BorderStyle="None"/>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="X&#243;a">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Center" Width="30px" />
            <ItemTemplate>
                <asp:LinkButton ID="lkbDelete" runat="server" OnClientClick="return confirm('Bạn có chắc muốn xóa trang này chứ?')" CommandName="Delete">  
                    <asp:Image ToolTip="Xóa" ID="imgXoa" runat="server" ImageUrl="~/images/delete.gif" BorderStyle="None" />
                </asp:LinkButton>                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Tên trang">
            <ItemTemplate>
                <asp:Image ID="imgIcon" runat="server" ImageUrl='<%# Eval("Icon") %>' Width="16px" Visible='<%# Eval("Icon")!=null && Eval("Icon").ToString() != ""%>' />
                <asp:Literal ID="ltrCategoryName" runat="server" Text='<%# Eval("PageName") %>'></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Thứ tự">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Center" Width="80px" />
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="rntCateOrder" runat="server" Width="25px"
                    ValidationGroup="UpdateCateOrder" Value='<%# Convert.ToDouble(Eval("Order")) %>' NumberFormat-DecimalDigits="0"> 
                </telerik:RadNumericTextBox>
                <asp:RangeValidator ID="RangeValidator1" runat="server" MinimumValue="1" MaximumValue="9999" Type="Integer" ControlToValidate="rntCateOrder" Display="Dynamic" Text="*"                            
                    ErrorMessage="Giá trị nhập vào phải lớn hơn 0 và nhỏ hơn hoặc bằng 9999" ForeColor="Red" SetFocusOnError="True" ValidationGroup="UpdateCateOrder"></asp:RangeValidator>
            </ItemTemplate>
            <FooterTemplate>
                <img src="/images/Floppy.png" width="12px" height="12px" alt="Cập nhật" />
                <asp:LinkButton ID="lkbUpdateOrder" runat="server" 
                    ToolTip="Cập nhật thứ tự hiện thị" ValidationGroup="UpdateCateOrder" 
                    onclick="lkbUpdateOrder_Click">Cập nhật</asp:LinkButton>
            </FooterTemplate>
            <FooterStyle Font-Size="12px" HorizontalAlign="Center" Font-Bold="true" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Hiển thị menu chính">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Center" Width="240px" />
            <ItemTemplate>
                <asp:LinkButton ID="lkbToogleHomePage" runat="server" CommandName="ToogleMainMenu" CommandArgument='<%# Eval("Id") %>' >  
                    <asp:Image ID="imgShowInHomePage2" runat="server" ImageUrl="~/images/checked.gif" Width="16px"  Visible='<%# (Eval("ShowInMainMenu")!=null) && Convert.ToBoolean(Eval("ShowInMainMenu"))%>'/>
                    <asp:Image ID="imgShowInHomePage1" runat="server" ImageUrl="~/images/unchecked.png" Width="16px"  Visible='<%# (Eval("ShowInMainMenu")==null) || !Convert.ToBoolean(Eval("ShowInMainMenu"))%>'/>
                </asp:LinkButton>                
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Thứ tự">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Right" Width="60px" />
            <ItemTemplate>
                <a id="A2"  href='<%#DataBinder.Eval(Container,"DataItem.Id")%>' runat="server" oncopy="return false" oncontextmenu="return false">
                    <asp:Image ID="imgXuong" runat="server" ToolTip="Xuống" ImageUrl="~/images/down.png"  Width="16" BorderStyle="None"/></a>
                <a id="A3" href='<%#DataBinder.Eval(Container,"DataItem.Id")%>' runat="server" oncopy="return false" oncontextmenu="return false">
                    <asp:Image ID="imgLen" ToolTip="Lên" runat="server" ImageUrl="~/images/up.png" Width="16" BorderStyle="None"/></a>
            </ItemTemplate>
        </asp:TemplateField>--%>
    </Columns>
    <HeaderStyle BackColor="#777777" ForeColor="#fefefe" Font-Size="11px" Height="30px" />
    <FooterStyle BorderStyle="None" BorderWidth="0px" BorderColor="White" />
</asp:GridView> 