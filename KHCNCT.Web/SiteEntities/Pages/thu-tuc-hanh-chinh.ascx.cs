using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Web.SiteEntities.Controls;
using KHCNCT.Data;
using KHCNCT.Globals;

namespace KHCNCT.Web.SiteEntities.Pages
{
    public partial class thu_tuc_hanh_chinh : PageBaseControl
    {
        private int _dmttid;
        public int DanhMucTT
        {
            get
            {
                if (Request.QueryString["dmttid"] != null) int.TryParse(Request.QueryString["dmttid"], out _dmttid);
                return _dmttid;
            }
        }

        private int _ttid;
        public int ThuTucId
        {
            get
            {
                if (Request.QueryString["ttid"] != null) int.TryParse(Request.QueryString["ttid"], out _ttid);
                return _ttid;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhMucThuTuc();
                if (ThuTucId > 0)
                {
                    pnlViewDetail.Visible = true;
                    grvThuTucHanhChinh.Visible = false;
                    BindInfo(ThuTucId);
                }
                else
                {
                    pnlViewDetail.Visible = false;
                    grvThuTucHanhChinh.Visible = true;
                    LoadThuTuc();
                }
            }
        }

        private void BindInfo(int ThuTucId)
        {
            ThuTucHanhChinh_ChiTiet thutuc = db.ThuTucHanhChinh_ChiTiets.SingleOrDefault<ThuTucHanhChinh_ChiTiet>(tt => tt.Id == ThuTucId && tt.HieuLuc == true);
            if (thutuc != null)
            {
                thutuc.ViewCount = thutuc.ViewCount + 1;
                db.SubmitChanges();

                lblTenThuTuc.Text = thutuc.TenThuTuc;
                lblCaption.Text = thutuc.ThuTucHanhChinh_DanhMuc.TenDanhMuc;
                lblDetail.Text = thutuc.ChiTiet;
                lblNgayTao.Text = thutuc.NgayTao.Value.ToString("dd/MM/yyyy");
                lblViewCount.Text = thutuc.ViewCount.Value.ToString();
                lblDownloadCount.Text = thutuc.DownloadCount.Value.ToString();
                //hplDownload.NavigateUrl = thutuc.FileTaiVe;
            }
        }

        protected void LoadThuTuc()
        {
            if (DanhMucTT > 0)
            {
                ThuTucHanhChinh_DanhMuc dm = db.ThuTucHanhChinh_DanhMucs.SingleOrDefault<ThuTucHanhChinh_DanhMuc>(dmtt => dmtt.Id == DanhMucTT);
                if (dm != null) lblCaption.Text = dm.TenDanhMuc;
                var lstThuTuc = (from tt in db.ThuTucHanhChinh_ChiTiets
                                 join ft in db.FileTypes on tt.LoaiFile equals ft.FileExt
                                 where tt.HieuLuc == true && tt.DanhMuc == DanhMucTT
                                 orderby tt.NgayTao
                                 select new
                                 {
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
                                     tt.ViewCount,
                                     tt.DownloadCount
                                 });
                grvThuTucHanhChinh.DataSource = lstThuTuc.ToList();
                grvThuTucHanhChinh.DataBind();
            }
            else
            {
                lblCaption.Text = "&nbsp;";
                var lstThuTuc = (from tt in db.ThuTucHanhChinh_ChiTiets
                                 join ft in db.FileTypes on tt.LoaiFile equals ft.FileExt
                                 where tt.HieuLuc == true
                                 orderby tt.NgayTao
                                 select new
                                 {
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
                                     tt.ViewCount,
                                     tt.DownloadCount
                                 });
                grvThuTucHanhChinh.DataSource = lstThuTuc.ToList();
                grvThuTucHanhChinh.DataBind();
            }
        }

        private void LoadDanhMucThuTuc()
        {
            List<ThuTucHanhChinh_DanhMuc> lstDMTT = (from dm in db.ThuTucHanhChinh_DanhMucs
                                                     orderby dm.ThuTu
                                                     select dm).ToList<ThuTucHanhChinh_DanhMuc>();
            rptDanhMucThuTuc.DataSource = lstDMTT;
            rptDanhMucThuTuc.DataBind();
        }

        protected void grvThuTucHanhChinh_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvThuTucHanhChinh.PageIndex = e.NewPageIndex;
            LoadThuTuc();
        }

        protected void grvThuTucHanhChinh_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ThuTucHanhChinh_ChiTiet tt = db.ThuTucHanhChinh_ChiTiets.SingleOrDefault<ThuTucHanhChinh_ChiTiet>(ttct => ttct.Id == id && ttct.HieuLuc == true);
                if (tt != null)
                {
                    tt.DownloadCount = tt.DownloadCount + 1;
                    db.SubmitChanges();
                    Response.Redirect(tt.FileTaiVe);
                }
            }
            else if (e.CommandName == "ViewDetail")
            {
                Response.Redirect(KHCNCT.Globals.Common.GenerateUrl("thu-tuc-hanh-chinh", "ttid=" + e.CommandArgument));
                //ThuTucHanhChinh_ChiTiet tt = db.ThuTucHanhChinh_ChiTiets.SingleOrDefault<ThuTucHanhChinh_ChiTiet>(ttct => ttct.Id == id);
                //if (tt != null)
                //{
                //    tt.ViewCount = tt.ViewCount + 1;
                //    db.SubmitChanges();
                    
                //}
            }
        }

        protected void lkbDownload_Click(object sender, EventArgs e)
        {
            ThuTucHanhChinh_ChiTiet tt = db.ThuTucHanhChinh_ChiTiets.SingleOrDefault<ThuTucHanhChinh_ChiTiet>(ttct => ttct.Id == ThuTucId && ttct.HieuLuc == true);
            if (tt != null)
            {
                tt.DownloadCount = tt.DownloadCount + 1;
                db.SubmitChanges();
                Response.Redirect(tt.FileTaiVe);
            }
        }
    }
}