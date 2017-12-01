<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApproveNews.ascx.cs" Inherits="KHCNCT.admin.modules.admApproveNews" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="~/admin/Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>

<script type="text/javascript">
    function ValidateDetailInputControl(source, args) {
        var detailLength = $find("<%=Editor1.MainEditor.ClientID %>").get_html(true).length;
        args.IsValid = (detailLength > 0);
    }

    function confirmAspImageButton(button) {
        function aspImageButtonCallbackFn(arg) {
            if (arg) {
                __doPostBack(button.name, "");
                if (Telerik.Web.Browser.ff) { //work around a FireFox issue with form submission
                    button.click();
                }
            }
        }
        radconfirm("Are you sure you want to postback?", aspButtonCallbackFn, 330, 180, null, "Confirm");
    }
</script>
<asp:Panel ID="pnlAddNew" runat="server">

<table style="width:100%;">
    <tr>
        <td valign="top">
            Tiêu đề tin
        </td>
        <td>
            &nbsp;</td>
        <td>
            <asp:TextBox ID="txtTieuDe" runat="server" Width="400px"  
                ValidationGroup="UpdateNews"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="rfvTieuDe" runat="server" 
                ErrorMessage="Bạn phải nhập tiêu đề tin" ControlToValidate="txtTieuDe" 
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="UpdateNews"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Chuyên mục tin
        </td>
        <td>
            &nbsp;</td>
        <td>
            <div style="max-height: 150px; overflow: auto; width: 400px; border: solid 1px #ccc">
                    <telerik:RadComboBox ID="rcbCategories" runat="server" Width="200px">
                    </telerik:RadComboBox>
                </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rcbCategories"
                    ErrorMessage="Bạn phải chọn một danh mục" SetFocusOnError="True" Display="Dynamic"
                    Font-Bold="True" ForeColor="Red" ValidationGroup="UpdateNews"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Hình ảnh
        </td>
        <td>
            &nbsp;</td>
        <td>
            <div id="divHinhDaiDien" runat="server">
                <asp:Image ID="imhHinhDaiDien" runat="server" Width="100" Height="80" />
            </div>
            <asp:FileUpload ID="fulHinhAnh" runat="server" />
            &nbsp;<asp:RegularExpressionValidator ID="revHinhAnh" runat="server"
            ErrorMessage="Định dạng ảnh không phù hợp!"
            ControlToValidate="fulHinhAnh"                    
            ValidationExpression=".*(\.jpg|\.JPG|\.png|\.PNG|\.gif|\.GIF|\.bmp|\.BMP)$" 
            Display="Dynamic" SetFocusOnError="True" ValidationGroup="UpdateNews"></asp:RegularExpressionValidator>
            <br />
            (gif, jpg, png, bmp)
        </td>
    </tr>
    <tr>
        <td valign="top">
            Mô tả</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:TextBox ID="txtMoTa" runat="server" Height="100px" TextMode="MultiLine" 
                Width="395px" ValidationGroup="UpdateNews"></asp:TextBox>
             &nbsp;<asp:RequiredFieldValidator ID="rfvMoTa" runat="server" 
                ErrorMessage="Bạn phải nhập mô tả" ControlToValidate="txtMoTa" 
                Display="Dynamic" SetFocusOnError="True" ValidationGroup="UpdateNews"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Nội dung tin</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:CustomValidator ID="ctvChiTiet" runat="server" 
                ClientValidationFunction="ValidateDetailInputControl" 
                ErrorMessage="Bạn phải nhập nội dung chi tiết" Display="Dynamic"
                onservervalidate="ctvChiTiet_ServerValidate" SetFocusOnError="True" 
                ValidationGroup="UpdateNews"></asp:CustomValidator>
            <uc1:Editor ID="Editor1" runat="server" />
            <asp:Label ID="lblChitiet" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Nguồn
        </td>
        <td>
            &nbsp;</td>
        <td>
            <asp:TextBox ID="txtSource" runat="server" Width="400px" 
                ValidationGroup="UpdateNews"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Tác giả
        </td>
        <td>
            &nbsp;</td>
        <td>
            <asp:TextBox ID="txtAuthor" runat="server" Width="400px" 
                ValidationGroup="UpdateNews"></asp:TextBox>
        </td>
    </tr>
   <tr id="trApprove1" runat="server">
        <td>
            Phê duyệt</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:DropDownList ID="ddlApprove" runat="server">
                <asp:ListItem Value="1">Đồng ý duyệt</asp:ListItem>
                <asp:ListItem Value="0">Không đồng ý duyệt</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trApprove2" runat="server">
        <td>
            Nội dung phê duyệt</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:TextBox ID="txtApprovementDetail" runat="server" Height="100px" TextMode="MultiLine" 
                Width="395px" ValidationGroup="UpdateNews"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" 
                Text="Thực hiện" ValidationGroup="UpdateNews" />

            &nbsp;&nbsp;

            <asp:Button ID="btnCancel" runat="server"  Text="Hủy bỏ" CausesValidation='false' onclick="btnCancel_Click"/>
        </td>
    </tr>
</table>

</asp:Panel>

<asp:Panel ID="pnlManage" runat="server">
    Chuyên mục:&nbsp; 
<telerik:RadComboBox ID="rcbCategoryShow" runat="server" Width="300px">
                    </telerik:RadComboBox> 
                    Trạng thái tin
<telerik:RadComboBox ID="rcbApproved" runat="server">
    <Items>
        <telerik:RadComboBoxItem Text="Tất cả" />
        <telerik:RadComboBoxItem Text="Chưa duyệt" Value="1" />
        <telerik:RadComboBoxItem Text="Đã duyệt - Chưa xuất bản" Value="2"  />        
        <telerik:RadComboBoxItem Text="Đã xuất bản" Value="3" />
        <telerik:RadComboBoxItem Text="Không duyệt" Value="1" />
        <telerik:RadComboBoxItem Text="Không xuất bản" Value="-2" />
        <telerik:RadComboBoxItem Text="Tin đã thu hồi" Value="-3" />
    </Items>
</telerik:RadComboBox> 

                    <asp:Button ID="btnShowCategory" runat="server" 
        onclick="btnShowCategory_Click" Text="Hiển thị" />
                    <hr />
    <telerik:RadGrid ID="rgNews" runat="server" AllowPaging="true"
        AutoGenerateColumns="False" AllowSorting="True"  OnItemCommand="rgNews_ItemCommand"
                        OnItemDataBound="rgNews_ItemDataBound" OnPageIndexChanged="rgNews_PageIndexChanged"
                        PageSize="20" OnPageSizeChanged="rgNews_PageSizeChanged"
        ondeletecommand="rgNews_DeleteCommand" oneditcommand="rgNews_EditCommand">
        <MasterTableView EditMode="EditForms" DataKeyNames="Id">
            <Columns>
                <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" UpdateText="Cập nhật" EditText="Sửa"
                    CancelText="Huỷ bỏ" UniqueName="btnEdit">
                </telerik:GridEditCommandColumn>--%>
            <telerik:GridTemplateColumn HeaderText="Phê duyệt">
                    <ItemTemplate>
                        <asp:ImageButton CommandName="Edit" Text="Phê duyệt" ID="imgSend" ImageUrl="~/admin/assets/images/Edit.png" Height="16" Width="16" runat="server"/>
                    </ItemTemplate>
            </telerik:GridTemplateColumn>
            
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:Image ID="imgDetail" runat="server" Width='70px' Height='50px'  ImageUrl='<%# Eval("ImagePath") %>'/>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:HyperLink ID="hplNewsTitle" runat="server" Text='<%# Eval("NewsTitle") %>' NavigateUrl='<%# KHCNCT.Globals.Common.GenerateAdminUrl("approvenews", "newsid=" + Eval("Id").ToString()) %>'></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Chuyên mục">
                    <ItemTemplate>
                        <%# Eval("CategoryName") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Người gửi">
                    <ItemTemplate>
                        <strong><%# Eval("AuthorFullname")%></strong> - <%# Eval("AccountName")%> 
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="LastUpdatedTime" HeaderText="Ngày cập nhật">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastSentTime" HeaderText="Ngày gửi">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Trạng thái">
                    <ItemTemplate>
                        <strong><%# Eval("Approvement") %></strong>
                        <br />                        
                        <%-- <span style="color:#aaa"><%# Eval("ApprovedBy").ToString() + " - " + Eval("ApprovedTime", "{0:dd/MM/yyyy HH:mm}")%></span>--%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <PagerStyle Mode="NextPrevNumericAndAdvanced" Position="TopAndBottom" ShowPagerText="true" Visible="true" />
    </telerik:RadGrid>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
</asp:Panel>