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
    public partial class duyettthc : CommonBaseControl
    {
        private int _ttId;
        public int ThuTucId
        {
            get
            {
                if (_ttId == 0 && Request.QueryString["ttid"] != null) int.TryParse(Request.QueryString["ttid"], out _ttId);
                return _ttId;
            }
        }

        private ThuTucHanhChinh_ChiTiet _ThuTuc;
        public ThuTucHanhChinh_ChiTiet ThuTuc
        {
            get
            {
                if (_ThuTuc == null && ThuTucId > 0)
                {
                    _ThuTuc = db.ThuTucHanhChinh_ChiTiets.SingleOrDefault<ThuTucHanhChinh_ChiTiet>(ttct => ttct.Id == _ttId);
                }
                return _ThuTuc;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ThuTuc != null)
                {
                    BindInfo(ThuTuc);
                    pnlApproved.Visible = true;
                    grvThuTuc.Visible = false;
                }
                else
                {
                    pnlApproved.Visible = false;
                    grvThuTuc.Visible = true;
                    LoadThuTuc();
                }
            }
        }

        private void BindInfo(ThuTucHanhChinh_ChiTiet thutuc)
        {
            lblTenThuTuc.Text = thutuc.TenThuTuc;
            lblMoTaThuTuc.Text = thutuc.MoTa;
            lblChiTietThuTuc.Text = thutuc.ChiTiet;
            lblDanhMuc.Text = thutuc.ThuTucHanhChinh_DanhMuc.TenDanhMuc;
            hplViewFile.NavigateUrl = thutuc.FileTaiVe;
            if (thutuc.HieuLuc == true)
            {
                lblTrangThai.Text = "Đã duyệt";
                lblTrangThai.ForeColor = System.Drawing.Color.Blue;
                btnSave.Text = " BỎ DUYỆT ";
            }
            else
            {
                lblTrangThai.Text = "Chưa duyệt";
                lblTrangThai.ForeColor = System.Drawing.Color.Red;
                btnSave.Text = "  DUYỆT  ";
            }
        }

        protected void LoadThuTuc()
        {
            var lstThuTuc = (from tt in db.ThuTucHanhChinh_ChiTiets
                             join ft in db.FileTypes on tt.LoaiFile equals ft.FileExt
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


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ThuTuc != null)
            {
                ThuTuc.HieuLuc = !ThuTuc.HieuLuc;
                db.SubmitChanges();
                pnlApproved.Visible = false;
                grvThuTuc.Visible = true;
                LoadThuTuc();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlApproved.Visible = false;
            grvThuTuc.Visible = true;
            LoadThuTuc();
        }
    }
}