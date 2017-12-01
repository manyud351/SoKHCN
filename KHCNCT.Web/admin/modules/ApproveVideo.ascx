<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApproveVideo.ascx.cs" Inherits="KHCNCT.Web.admin.modules.ApproveVideo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript" src="/assets/scripts/swfobject.js"></script>
<script type="text/javascript" src="/assets/scripts/jwplayer.js"></script>
<div id="divManage" runat="server">
<strong>PHÊ DUYỆT VIDEO</strong>
    <br />
Chuyên mục video:&nbsp; 
<telerik:RadComboBox ID="rcbCategoryShow" runat="server" Width="300px">
                    </telerik:RadComboBox> 
                    Trạng thái video
<telerik:RadComboBox ID="rcbApproved" runat="server">
    <Items>
        <telerik:RadComboBoxItem Text="Tất cả" />
        <telerik:RadComboBoxItem Text="Chưa duyệt" Value="0" />
        <telerik:RadComboBoxItem Text="Đã duyệt" Value="1"  />        
        <telerik:RadComboBoxItem Text="Không duyệt" Value="2"  />        
    </Items>
</telerik:RadComboBox> 
<asp:Button ID="btnShowCategory" runat="server" 
        onclick="btnShowCategory_Click" Text="Hiển thị" />
        <br />
        <hr />

<asp:GridView ID="grvVideo" runat="server" DataKeyNames="Id" 
        AutoGenerateColumns="False"
        AllowPaging="true" PageSize="10"
        CellPadding="4" 
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="hplPreview" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateAdminUrl("approvevideo","act=preview","vid=" + Eval("Id").ToString()) %>'>
                        <asp:Image ID="imgPreview" runat="server" ImageUrl='<%# Eval("PreviewImage") %>' Width='80px' Height='70px' BorderStyle="None" />
                    </asp:HyperLink>
                </ItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Video">
                <ItemTemplate>
                    <strong>Tiêu đề: </strong>
                    <asp:HyperLink ID="hplPreview2" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateAdminUrl("approvevideo","act=preview","vid=" + Eval("Id").ToString()) %>' Font-Bold="true">
                        <%# Eval("VideoTitle")%>
                    </asp:HyperLink>
                    
                    <br />
                    <span style="color: #ccc">Filename: </span> <%# Eval("OriginalFileName")%>

                </ItemTemplate>
                <ItemStyle Width="400px" />
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
            <asp:TemplateField HeaderText="Trạng thái">
                <ItemTemplate>
                    <asp:Literal ID="ltrApprovementStatus" runat="server" Text='<%# Eval("ApprovementDescription") %>'></asp:Literal>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lượt xem">
                <ItemTemplate>
                    <%# Eval("ViewCount") %>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hiển thị trang chủ">
                <ItemTemplate>
                    <asp:CheckBox ID="ckbShowHomePage" runat="server" Checked='<%# Convert.ToBoolean(Eval("ShowInHomepage")) %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sửa">
                <ItemTemplate>
                    <asp:HyperLink ID="lkbEdit" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateAdminUrl("approvevideo","act=update", "vid=" + Eval("Id").ToString()) %> '>
                        <asp:Image ID="imgEdit" runat="server" BorderStyle="None" AlternateText="Sửa" ImageUrl="~/images/Edit.png" Width="14px" /> 
                    </asp:HyperLink>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" VerticalAlign="Top" />
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
               <asp:DropDownList ID="ddlCategory" runat="server" ValidationGroup="UpdateVideo">
               </asp:DropDownList>
           &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                   ErrorMessage="Bạn phải chọn một chuyên mục video" InitialValue="" 
                   ControlToValidate="ddlCategory" ForeColor="Red" SetFocusOnError="True" 
                   ValidationGroup="UpdateVideo"></asp:RequiredFieldValidator>
           </td>
        </tr>
        <tr>
           <td>Tiêu đề Video </td>
           <td>&nbsp;</td>
           <td>
               <asp:TextBox ID="txtVideoTitle" runat="server" ValidationGroup="UpdateVideo"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                   ErrorMessage="Bạn phải nhập tiêu đề video" 
                   ControlToValidate="txtVideoTitle" ForeColor="Red" SetFocusOnError="True" 
                   ValidationGroup="UpdateVideo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
           <td>File Video</td>
           <td>&nbsp;</td>
           <td>
                <div id="divVideo" runat="server">
                    <asp:HiddenField ID="hdfVideoFile" runat="server" /><asp:HiddenField ID="hdfPreviewImage" runat="server" Value=''/>
                    <div id='video_container'>
                    </div>
                    <script type='text/javascript'>
                        var vFile = document.getElementById('<%= hdfVideoFile.ClientID %>').value;
                        var iFile = document.getElementById('<%= hdfPreviewImage.ClientID %>').value;
                        jwplayer('video_container').setup({
                            flashplayer: '/assets/players/player.swf',
                            file: vFile,
                            image: iFile,
                            height: 250,
                            width: 300
                        });
                    </script>
                </div>
            </td>
        </tr><tr>
           <td>Ảnh đại diện </td>
           <td>&nbsp;</td>
           <td>
                <div id="divImgPreview" runat="server">
                    <asp:Image ID="imgPreview" runat="server" Width='80px' Height='70px' />
                    <br style="clear: both" />
                </div>
        </tr>
        <tr>
           <td>Mô tả</td>
           <td>&nbsp;</td>
           <td><asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
           <td>Thứ tự hiển thị</td>
           <td>&nbsp;</td>
           <td>
               <telerik:RadNumericTextBox ID="rntOrder" runat="server" NumberFormat-DecimalDigits="0">
               </telerik:RadNumericTextBox></td>
        </tr>
        <tr>
           <td>Hiển thị trang chủ</td>
           <td>&nbsp;</td>
           <td>
                <asp:CheckBox ID="ckbShowInHomepage" runat="server" />
            </td>
        </tr>
        <tr>
           <td>Phê duyệt</td>
           <td>&nbsp;</td>
           <td>
               <asp:DropDownList ID="ddlApproveVideo" runat="server" ValidationGroup="UpdateVideo">
                    <asp:ListItem Value="" Text="--"></asp:ListItem>
                    <asp:ListItem Value="Đồng ý"></asp:ListItem>
                    <asp:ListItem Value="Không đồng ý"></asp:ListItem>
               </asp:DropDownList>
               &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                   ErrorMessage="Bạn phải chọn hành động phê duyệt" InitialValue="" 
                   ControlToValidate="ddlApproveVideo" ForeColor="Red" SetFocusOnError="True" 
                   ValidationGroup="UpdateVideo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
           <td>Ý kiến phê duyệt</td>
           <td>&nbsp;</td>
           <td><asp:TextBox ID="txtApprovementDes" runat="server" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
           <td>&nbsp;</td>
           <td>&nbsp;</td>
           <td>
               <asp:Button ID="btnSave" runat="server" Text="Lưu lại" 
                   onclick="btnSave_Click" ValidationGroup="UpdateVideo" /> 
               &nbsp;&nbsp;
               <asp:Button ID="btnCancel" runat="server" Text="Hủy bỏ" 
                   onclick="btnCancel_Click" /> 
               </td>
        </tr>
        <tr>
           <td colspan="3">
            <strong>QUÁ TRÌNH PHÊ DUYỆT</strong>
           </td>
        </tr>
        <tr>
           <td>&nbsp;</td>
           <td>&nbsp;</td>
           <td>
               <asp:GridView ID="grvApprovementHis" runat="server" AutoGenerateColumns="false" 
                   Width="800px"> 
                <Columns>
                    <asp:TemplateField HeaderText = "STT">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "Thời gian">
                        <ItemTemplate>
                            <%# Eval("ApprovedTime", "{0:HH:mm dd/MM/yy}")%>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "Chi tiết">
                        <ItemTemplate>
                            <strong>+ Kết quả phê duyệt: </strong> <%# Eval("ApprovementResult")%><br />
                            <strong>+ Nội dung phê duyệt: </strong> <%# Eval("ApprovementDes")%><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "Người duyệt">
                        <ItemTemplate>
                            <%# Eval("ApprovedBy")%>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                </Columns>
                   <RowStyle VerticalAlign="Top" />
               </asp:GridView>
            </td>
        </tr>
    </table>
</div>
<div id="divPreview" runat="server">
    <asp:HiddenField ID="hdfPreviewVideoFile" runat="server" /><asp:HiddenField ID="hdfPreviewPreviewImage" runat="server" Value=''/>
    <div id='divPreviewVideo'></div>
    <script type='text/javascript'>        
        var vPreviewFile = document.getElementById('<%= hdfPreviewVideoFile.ClientID %>').value;
        var iPreviewFile = document.getElementById('<%= hdfPreviewPreviewImage.ClientID %>').value;
        jwplayer('divPreviewVideo').setup({
            flashplayer: '/assets/players/player.swf',
            file: vPreviewFile,
            image: iPreviewFile,
            height: 450,
            width: 500
        });
    </script>
    <asp:Button ID="btnReturn" runat="server" Text="Trở về" 
                   onclick="btnCancel_Click" /> 
</div>