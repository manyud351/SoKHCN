<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category.ascx.cs" Inherits="KHCNCT.admin.modules._Category" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlAddNew" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                Tên danh mục
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtCategoryName" runat="server" 
                    ValidationGroup="UpdateCategory"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCategoryName" Display="Dynamic" 
                    ErrorMessage="Bạn phải nhập tên danh mục" ForeColor="Red" 
                    SetFocusOnError="True" ValidationGroup="UpdateCategory"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Danh mục cha
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadComboBox ID="rcbParentCategory" runat="server" 
                    ValidationGroup="UpdateCategory"></telerik:RadComboBox>
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
                Hiển thị danh mục:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:CheckBox ID="ckbMainMenu" runat="server" Text="Menu trang chủ" /> &nbsp;
                <asp:CheckBox ID="ckbHomepage" runat="server" Text="Trang chủ" />
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
                <asp:LinkButton ID="lkbDelete" runat="server" OnClientClick="return confirm('Bạn có chắc muốn xóa chuyên mục này chứ?')" CommandName="Delete">  
                    <asp:Image ToolTip="Xóa" ID="imgXoa" runat="server" ImageUrl="~/images/delete.gif" BorderStyle="None" />
                </asp:LinkButton>                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Tên chuyên mục">
            <ItemTemplate>
                <asp:Image ID="imgIcon" runat="server" ImageUrl=<%# Eval("Icon") %> Width="16px" Visible='<%# Eval("Icon")!=null && Eval("Icon").ToString() != ""%>' />
                <asp:Literal ID="ltrCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Thứ tự">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Center" Width="80px" />
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="rntCateOrder" runat="server" Width="25px"
                    ValidationGroup="UpdateCateOrder" Value='<%# Convert.ToDouble(Eval("Order")) %>' NumberFormat-DecimalDigits="0"> 
                </telerik:RadNumericTextBox>
                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                    MinimumValue="1" MaximumValue="9999" Type="Integer"
                                    ControlToValidate="rntCateOrder" Display="Dynamic"         
                    Text="*"                            
                    ErrorMessage="Giá trị nhập vào phải lớn hơn 0 và nhỏ hơn hoặc bằng 9999" ForeColor="Red" 
                                    SetFocusOnError="True" ValidationGroup="UpdateCateOrder"></asp:RangeValidator>
            </ItemTemplate>
            <FooterTemplate>
                <img src="/images/Floppy.png" width="12px" height="12px" alt="Cập nhật" />
                <asp:LinkButton ID="lkbUpdateOrder" runat="server" 
                    ToolTip="Cập nhật thứ tự hiện thị" ValidationGroup="UpdateCateOrder" 
                    onclick="lkbUpdateOrder_Click">Cập nhật</asp:LinkButton>
            </FooterTemplate>
            <FooterStyle Font-Size="12px" HorizontalAlign="Center" Font-Bold="true" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Hiển thị trong">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Center" Width="250px" />
            <ItemTemplate>
                <asp:Label ID="lblShowIn" runat="server"></asp:Label>             
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="(1) Hiển thị trang chủ">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Center" Width="240px" />
            <ItemTemplate>
                <asp:Image ID="imgShowInHomePage" runat="server" ImageUrl="~/images/Home.png" Width="16px" Visible='<%# (Eval("ShowInHomePage")!=null) && Convert.ToBoolean(Eval("ShowInHomePage"))%>' />
                <asp:LinkButton ID="lkbToogleProductByCateModule" runat="server" CommandName="ToogleHomePage" CommandArgument='<%# Eval("Id") %>'>Hiển thị trang chủ</asp:LinkButton>                
                <asp:Label ID="lblNotLevel1Category" runat="server" ToolTip="Không phải danh mục cấp 1">--</asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="(2) Hiển thị menu chính">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Center" Width="240px" />
            <ItemTemplate>
                <asp:Image ID="imgShowInHomePage2" runat="server" ImageUrl="~/images/icon-folder-64.png" Width="16px" Visible='<%# (Eval("ShowInMainMenu")!=null) && Convert.ToBoolean(Eval("ShowInMainMenu"))%>' />
                <asp:LinkButton ID="lkbToogleHomePage" runat="server" CommandName="ToogleMainMenu" CommandArgument='<%# Eval("Id") %>'>  
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
<div style="margin: 10px auto; width: 80%">
    <div style="width: 100%; border: solid 2px #ccc; background-color: #eee; min-height: 50px; padding: 10px">
    <strong><u>Hướng dẫn: </u></strong><br />
    <br />
        <span class="admin-desc"><b>(1) Hiển thị trang chủ:</b> danh mục được cấu hình hiển thị trang chủ thì các sản phẩm thuộc danh mục đó và các danh mục con của nó sẽ 
                                được hiển thị trong module sản phẩm theo danh mục ở trang chủ. Số lượng danh mục hiển thị tối đa không qua số danh mục tối đa được cấu hình
                                hiển thị ở trang chủ (chỉ menu cấp 1)</span> <br /><br />
        <span class="admin-desc"><b>(2) Hiển thị menu trang chủ:</b> danh mục được cấu hình hiển thị menu trang chủ thì sẽ xuất hiện ở menu chính ở tranh chủ. Số lượng 
                                danh mục hiển thị menu tối đa không qua số danh mục tối đa được cấu hình  hiển thị ở menu trang chủ (chỉ menu cấp 1)</span>
    </div>
</div>