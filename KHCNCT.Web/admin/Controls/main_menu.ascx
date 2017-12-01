<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="main_menu.ascx.cs" Inherits="EStore.admin.controls.main_menu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadMenu ID="rmnMainMenu" runat="Server" Skin="Black" OnItemClick="rmnMainMenu_ItemClick" CssClass="main_menu">
    <%--<Items>
        <telerik:RadMenuItem runat="server" Text="Thông tin chung" NavigateUrl="~/admin/?mod=summary">
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="Quản trị nội dung">
            <Items>
                <telerik:RadMenuItem runat="server" Text="Tin chào bán/tìm mua">
                    <Items>
                        <telerik:RadMenuItem runat="server" Text="Quản trị - phê duyệt tin đăng" NavigateUrl='~/admin/?mod=approveitem'>
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Quản trị đơn hàng" NavigateUrl='~/admin/?mod=manageorder'>
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Footer" NavigateUrl="~/admin/?mod=footer">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Thông tin đầu trang" NavigateUrl="~/admin/?mod=hometopads">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị danh mục sản phẩm" NavigateUrl="~/admin/?mod=category">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị Tỉnh/thành" NavigateUrl="~/admin/?mod=province">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị Quốc gia" NavigateUrl="~/admin/?mod=country">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Thông tin doanh nghiệp" NavigateUrl="~/admin/?mod=cominfo">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị bài viết" NavigateUrl="~/admin/?mod=articlecategory">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Đăng tin tức" NavigateUrl="~/admin/?mod=news">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị thông tin cần biết" NavigateUrl="~/admin/?mod=snews">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị thông báo" NavigateUrl="~/admin/?mod=annoucement">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị thiết bị">
                    <Items>
                        <telerik:RadMenuItem runat="server" Text="Cập nhật danh mục thiết bị" NavigateUrl='~/admin/?mod=danhmucthietbi'>
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Cập nhật thiết bị" NavigateUrl='~/admin/?mod=thietbi'>
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị tin tức 2">
                    <Items>
                        <telerik:RadMenuItem runat="server" Text="Danh mục tin tức 2" NavigateUrl='~/admin/?mod=gennewscate'>
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Cập nhật cập nhật tin tức  2" NavigateUrl='~/admin/?mod=gennews'>
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="Gian hàng">
            <Items>
                <telerik:RadMenuItem runat="server" Text="Quản trị sản phẩm gian hàng" NavigateUrl='~/admin/?mod=approvestoreitem'>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị gian hàng" NavigateUrl="~/admin/?mod=store">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Giao diện" NavigateUrl="~/admin/?mod=storeskin">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Tên miền riêng">
                    <Items>
                        <telerik:RadMenuItem runat="server" Text="Quản lý tên miền riêng đang hoạt động" NavigateUrl="~/admin/?mod=storedomain">
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Duyệt đăng ký/gia hạn tên miền riêng" NavigateUrl="~/admin/?mod=storedomainreg">
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="Chuyên gia">
            <Items>
                <telerik:RadMenuItem runat="server" Text="Danh mục" NavigateUrl="#">
                    <Items>
                        <telerik:RadMenuItem runat="server" Text="Học hàm" NavigateUrl="~/admin/?mod=spec_hh">
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Học vị" NavigateUrl="~/admin/?mod=spec_hv">
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Lĩnh vực" NavigateUrl="~/admin/?mod=spec_f">
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Thông tin chuyên gia" NavigateUrl="#">
                    <Items>
                        <telerik:RadMenuItem runat="server" Text="Duyệt đăng ký chuyên gia" NavigateUrl="~/admin/?mod=spec_reg&isupdate=false">
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Duyệt sửa đổi thông tin chuyên gia" NavigateUrl="~/admin/?mod=spec_reg&isupdate=true">
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Quản lý thông tin chuyên gia" NavigateUrl="~/admin/?mod=spec">
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Dự án tư vấn" NavigateUrl="#">
                    <Items>
                        <telerik:RadMenuItem runat="server" Text="Duyệt dự án" NavigateUrl="~/admin/?mod=spec_prj">
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Duyệt thông tin tư vấn" NavigateUrl="~/admin/?mod=spec_cmt">
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="Dịch vụ">
            <Items>
                <telerik:RadMenuItem runat="server" Text="Quảng cáo sản phẩm" NavigateUrl="~/admin/?mod=adsreg&type=product">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quảng cáo gian hàng" NavigateUrl="~/admin/?mod=adsreg&type=store">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Banner quảng cáo" NavigateUrl="~/admin/?mod=banner">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="Báo cáo thống kê">
            <Items>
                <telerik:RadMenuItem runat="server" Text="Thống kê tin chào bán/tìm mua" NavigateUrl="~/admin/?mod=reports/rptProduct">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Thống kê gian hàng" NavigateUrl="~/admin/?mod=reports/rptStore">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Thống kê sản phẩm gian hàng" NavigateUrl="~/admin/?mod=reports/rptStoreProduct">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Thống kê giao dịch" NavigateUrl="~/admin/?mod=reports/rptOrder">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="-" IsSeparator="true">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Thống kê chuyên gia" NavigateUrl="~/admin/?mod=reports/spec_stat">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Thống kê dự án tư vấn" NavigateUrl="~/admin/?mod=reports/rptSpecPrj">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" Text="Quản trị hệ thống">
            <Items>
                <telerik:RadMenuItem runat="server" Text="Cấu hình hệ thống" NavigateUrl="~/admin/?mod=configstore">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quản trị người dùng" NavigateUrl="~/admin/?mod=user">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Phân quyền" NavigateUrl="~/admin/?mod=userright">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Nick support" NavigateUrl="~/admin/?mod=support">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Lịch biểu" NavigateUrl="#">
                    <Items>
                        <telerik:RadMenuItem runat="server" Text="Lập lịch" NavigateUrl="~/admin/?mod=schedule">
                        </telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" Text="Xem log" NavigateUrl="~/admin/?mod=schedulelog">
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Quảng cáo" NavigateUrl="~/admin/?mod=adsmanage">
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
    </Items>--%>
</telerik:RadMenu>
