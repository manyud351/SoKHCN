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
    public partial class dsvbpq : CommonBaseControl
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
            var lstThuTuc = (from tt in db.VBPQ_VanBanChiTiets
                             join ft in db.FileTypes on tt.LoaiFile equals ft.FileExt
                             where tt.UserId == UserId
                             orderby tt.NgayTao
                             select new { 
                                tt.Id,
                                tt.SoHieu,
                                tt.ConHieuLuc,
                                tt.CQBH,
                                tt.ChiTiet,
                                TrangThai = (tt.DaDuyet == true ? "Đã duyệt" : "Chưa duyệt"),
                                tt.DownloadCount,
                                tt.FileDinhKem,
                                TenLinhVuc = tt.VBPQ_LinhVuc.TenLinhVuc,
                                tt.LinhVuc,
                                tt.LoaiFile,
                                tt.LoaiVB,
                                TenLoaiVB = tt.VBPQ_LoaiVB.LoaiVB,
                                tt.NgayBH,
                                tt.NgayCapNhat,
                                tt.NgayHL,
                                tt.NgayTao,
                                tt.TrichYeu,
                                tt.UserId,
                                NguoiTao = tt.SysUser.DisplayName,
                                FileTypeName = ft.FileTypeName,                                
                                tt.ViewCount
                             });
            grvThuTuc.DataSource = lstThuTuc.ToList();
            grvThuTuc.DataBind();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(KHCNCT.Globals.Common.GenerateAdminUrl("vbpq"));
        }

        protected void grvThuTuc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int _ttid = Convert.ToInt32(e.Keys[0].ToString());
            VBPQ_VanBanChiTiet tt = db.VBPQ_VanBanChiTiets.SingleOrDefault<VBPQ_VanBanChiTiet>(ttct => ttct.Id == _ttid && ttct.UserId == UserId);
            if (tt != null)
            {
                db.VBPQ_VanBanChiTiets.DeleteOnSubmit(tt);
                db.SubmitChanges();
                LoadThuTuc();
            }
        }
    }
}