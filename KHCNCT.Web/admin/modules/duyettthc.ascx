<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="duyettthc.ascx.cs" Inherits="KHCNCT.Web.admin.modules.duyettthc" %>
<strong>PHÊ DUYỆT THỦ TỤC HÀNH CHÍNH</strong>
<asp:GridView ID="grvThuTuc" runat="server" AutoGenerateColumns="False" 
    Width="100%" CellPadding="4" Font-Names="Verdana" Font-Size="9pt"  DataKeyNames="Id"
    ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tên thủ tục">
                <ItemTemplate>
                    <asp:HyperLink id="hplViewDetail" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateAdminUrl("duyettthc", "ttid=" + Eval("Id").ToString()) %>'>
                            <%# Eval("TenThuTuc") %>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Danh mục thủ tục">
                <ItemTemplate>
                    <%# Eval("TenDanhMuc") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Người tạo">
                <ItemTemplate>
                    <%# Eval("NguoiTao") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ngày tạo">
                <ItemTemplate>
                    <%# Eval("NgayTao", "{0:dd/MM/yyyy HH:mm}")%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Cập nhật lần cuối">
                <ItemTemplate>
                    <%# Eval("CapNhatLanCuoi", "{0:dd/MM/yyyy HH:mm}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Loại file">
                <ItemTemplate>
                    <%# Eval("FileTypeName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Trạng thái">
                <ItemTemplate>
                    <%# Eval("TrangThai")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lượt xem/tải">
                <ItemTemplate>
                    <%# Eval("ViewCount")%> / <%# Eval("DownloadCount") %>
                </ItemTemplate>
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

<asp:Panel ID="pnlApproved" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="2" border="0">
    <tr>
        <td valign="top">
            Tên thủ tục</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblTenThuTuc" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Danh mục thủ tục</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblDanhMuc" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Mô tả thủ tục</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblMoTaThuTuc" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Chi tiết thủ tục</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="lblChiTietThuTuc" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            File đính kèm</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:HyperLink ID="hplViewFile" runat="server">Xem file</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Trạng thái</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="lblTrangThai" runat="server"></asp:Label>
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
            <asp:Button ID="btnSave" runat="server" 
                onclick="btnSave_Click" />
&nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Hủy bỏ" 
                onclick="btnCancel_Click" />
        </td>
    </tr>
</table>
</asp:Panel>