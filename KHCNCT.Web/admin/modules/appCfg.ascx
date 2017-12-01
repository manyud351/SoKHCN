<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="appCfg.ascx.cs" Inherits="KHCNCT.Web.admin.modules.appCfg" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<table width="100%" border="0" cellpadding="2" cellspacing="2">
    <tr>
        <td style="width:300px">Hình đại diện mặc định tin tức
        </td>
        <td>&nbsp;
        </td>
        <td>
            <div id="divDefaultImage" runat="server">
                <asp:Image ID="imgDefaultNewsImg" runat="server" Width="100px" Height="80px"/>
            </div>
            <asp:FileUpload ID="fulUDefaultNewsImg" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Hình đại diện mặc định chung
        </td>
        <td>&nbsp;
        </td>
        <td>
            <div id="div1" runat="server">
                <asp:Image ID="imgDefaultImg" runat="server" Width="100px" Height="80px"/>
            </div>
            <asp:FileUpload ID="fulUDefaultImg" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Chiều rộng (tối đa) ảnh tin tức
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntMaxNewsImgWidth" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>
    <tr>
        <td>Chiều cao (tối đa) ảnh tin tức
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntMaxNewsImgHeight" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

     <tr>
        <td>Chiều rộng (tối đa) ảnh tin tức thu nhỏ
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntMaxThumbNewsImgWidth" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>
    <tr>
        <td>Chiều cao (tối đa) ảnh tin tức thu nhỏ
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntMaxThumbNewsImgHeight" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>
    <tr>
        <td>Số tin tức/1 trang
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntNewsPerPage" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <tr>
        <td>Số tin tức nổi bật
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntFeatureNews" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <tr>
        <td>Số tin tức xem nhiều
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntMostViewNews" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <tr>
        <td>Số tin tức trong module
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntNewsOnModule" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <tr>
        <td>Số tin tức hiển thị trong menu thả
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntNewsOnDropMenu" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <tr>
        <td>Số danh mục hiển thị trong menu thả
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntCateInDropMenu" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <tr>
        <td>Số danh mục gốc hiển thị trang chủ
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntRootCateShowHomepage" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <tr>
        <td>Số danh mục cấp 2 hiển thị trang chức
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntLevel2CateShowHomePage" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <%--<tr>
        <td>Cấp danh mục được đăng tin
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntNews_CategoryLevel" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>--%>
    
    <tr>
        <td>Số Video hiển thị trang chủ
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntMaxVideoShowHomepage" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <tr>
        <td>Số Video hiển thị trên một trang
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntMaxVideoShowOnPage" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

    <tr>
        <td>Hình đại diện video mặc định
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <telerik:RadNumericTextBox ID="rntDefaultVideoImage" Runat="server">
            </telerik:RadNumericTextBox>            
        </td>
    </tr>

     <tr>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>            
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
            CommandName="Update" Text="Update" ValidationGroup="Insert" 
                onclick="UpdateButton_Click" />
        &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
            CausesValidation="False" CommandName="Cancel" Text="Cancel" />        
        </td>
    </tr>

</table>