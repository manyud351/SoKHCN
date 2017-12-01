<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dsvbpq.ascx.cs" Inherits="KHCNCT.Web.admin.modules.dsvbpq" %>
<asp:Button ID="btnAddNew" runat="server" Text="Thêm mới văn bản pháp quy" 
    onclick="btnAddNew_Click" />
<br />
<br />
<strong>DANH SÁCH VĂN BẢN PHÁP QUY</strong>
<asp:GridView ID="grvThuTuc" runat="server" AutoGenerateColumns="False" 
    Width="100%" CellPadding="4" Font-Names="Verdana" Font-Size="9pt"  DataKeyNames="Id"
    ForeColor="#333333" GridLines="None" onrowdeleting="grvThuTuc_RowDeleting">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="hplEdit" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateAdminUrl("vbpq", "vbid=" + Eval("Id").ToString()) %>'>Sửa</asp:HyperLink>
                    <asp:LinkButton ID="lkbDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Bạn có chắc muốn xóa nội dung này?')" CommandArgument='<%# Eval("Id").ToString() %>'>Xóa</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="STT">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Số hiệu VB">
                <ItemTemplate>
                    <%# Eval("SoHieu") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Trích yếu">
                <ItemTemplate>
                    <%# Eval("TrichYeu") %>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Người tạo">
                <ItemTemplate>
                    <%# Eval("NguoiTao") %>
                </ItemTemplate>
            </asp:TemplateField>--%>
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
