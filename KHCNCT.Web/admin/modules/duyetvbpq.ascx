<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="duyetvbpq.ascx.cs" Inherits="KHCNCT.Web.admin.modules.duyetvbpq" %>
<strong>PHÊ DUYỆT VĂN BẢN PHÁP QUY</strong>
<asp:GridView ID="grvThuTuc" runat="server" AutoGenerateColumns="False" 
    Width="100%" CellPadding="4" Font-Names="Verdana" Font-Size="9pt"  DataKeyNames="Id"
    ForeColor="#333333" GridLines="None" 
    onrowdatabound="grvThuTuc_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Số hiệu văn bản">
                <ItemTemplate>
                    <asp:HyperLink id="hplViewDetail" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateAdminUrl("duyetvbpq", "vbid=" + Eval("Id").ToString()) %>'>
                            <%# Eval("SoHieu") %>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Trích yếu">
                <ItemTemplate>
                    <%# Eval("TrichYeu") %>
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
                    <%# Eval("NgayCapNhat", "{0:dd/MM/yyyy HH:mm}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ngày ban hành">
                <ItemTemplate>
                    <%# Eval("NgayBH", "{0:dd/MM/yyyy HH:mm}")%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Ngày hiệu lực">
                <ItemTemplate>
                    <%# Eval("NgayHL", "{0:dd/MM/yyyy HH:mm}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Loại file">
                <ItemTemplate>
                    <%# Eval("FileTypeName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField HeaderText="Còn hiệu lực" DataField="ConHieuLuc" />
            <asp:CheckBoxField HeaderText="Duyệt" DataField="DaDuyet" />
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
        <td valign="top" style="width: 200px">
            Số hiệu VB</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblSoHieu" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">Lĩnh vực VB</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblLinhVuc" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Cơ quan ban hành</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblCQBH" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Loại VB</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblLoaiVB" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Chi tiết văn bản</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="lblChiTietVB" runat="server"></asp:Label>
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
            Ngày ban hành</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="lblNgayBH" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Ngày hiệu lực</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="lblNgayHL" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            Hiệu lực</td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="lblHieuLuc" runat="server"></asp:Label>
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