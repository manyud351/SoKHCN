using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Admin.Controls;
using KHCNCT.Data;

namespace KHCNCT.Web.admin.modules
{
    public partial class dstthc : CommonBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadThuTuc();
            }
        }

        protected void LoadThuTuc()
        {
            var lstThuTuc = (from tt in db.ThuTucHanhChinh_ChiTiets
                             join ft in db.FileTypes on tt.LoaiFile equals ft.FileExt
                             where tt.UserId == UserId
                             orderby tt.NgayTao
                             select new { 
                                tt.Id,
                                tt.CapNhatLanCuoi,
                                tt.DanhMuc,
                                tt.FileTaiVe,
                                tt.HieuLuc,
                                tt.MoTa,
                                tt.NgayTao,
                                tt.TenThuTuc,
                                TenDanhMuc = tt.ThuTucHanhChinh_DanhMuc.TenDanhMuc,
                                NguoiTao = tt.SysUser.DisplayName,
                                FileTypeName = ft.FileTypeName,
                                TrangThai = (tt.HieuLuc == true ? "Đã duyệt" : "Chưa duyệt"),
                                tt.ViewCount,
                                tt.DownloadCount
                             });
            grvThuTuc.DataSource = lstThuTuc.ToList();
            grvThuTuc.DataBind();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(KHCNCT.Globals.Common.GenerateAdminUrl("tthc"));
        }

        protected void grvThuTuc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int _ttid = Convert.ToInt32(e.Keys[0].ToString());
            ThuTucHanhChinh_ChiTiet tt = db.ThuTucHanhChinh_ChiTiets.SingleOrDefault<ThuTucHanhChinh_ChiTiet>(ttct => ttct.Id == _ttid && ttct.UserId == UserId);
            if (tt != null)
            {
                db.ThuTucHanhChinh_ChiTiets.DeleteOnSubmit(tt);
                db.SubmitChanges();
                LoadThuTuc();
            }
        }
    }
}