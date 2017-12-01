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
    public partial class duyetvbpq : CommonBaseControl
    {
        private int _ttId;
        public int VanBanId
        {
            get
            {
                if (_ttId == 0 && Request.QueryString["vbid"] != null) int.TryParse(Request.QueryString["vbid"], out _ttId);
                return _ttId;
            }
        }

        private VBPQ_VanBanChiTiet _VanBan;
        public VBPQ_VanBanChiTiet VanBan
        {
            get
            {
                if (_VanBan == null && VanBanId > 0)
                {
                    _VanBan = db.VBPQ_VanBanChiTiets.SingleOrDefault<VBPQ_VanBanChiTiet>(ttct => ttct.Id == _ttId);
                }
                return _VanBan;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (VanBan != null)
                {
                    BindInfo(VanBan);
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

        private void BindInfo(VBPQ_VanBanChiTiet thutuc)
        {
            lblSoHieu.Text = thutuc.SoHieu;
            lblCQBH.Text = thutuc.VBPQ_CoQuanBH.TenCoQuanBH;
            lblChiTietVB.Text = thutuc.ChiTiet;
            lblHieuLuc.Text = (thutuc.ConHieuLuc == true ? "Còn hiệu lực" : "Hết hiệu lực");
            lblLinhVuc.Text = thutuc.VBPQ_LinhVuc.TenLinhVuc;
            lblLoaiVB.Text = thutuc.VBPQ_LoaiVB.LoaiVB;
            lblNgayBH.Text = thutuc.NgayBH.Value.ToString("dd/MM/yyyy");
            lblNgayHL.Text = thutuc.NgayHL.Value.ToString("dd/MM/yyyy");
            hplViewFile.NavigateUrl = thutuc.FileDinhKem;
            if (thutuc.DaDuyet == true)
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
            var lstThuTuc = (from tt in db.VBPQ_VanBanChiTiets
                             join ft in db.FileTypes on tt.LoaiFile equals ft.FileExt
                             where tt.UserId == UserId
                             orderby tt.NgayTao
                             select new
                             {
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
                                 tt.NgayHL,
                                 tt.NgayCapNhat,
                                 tt.NgayTao,
                                 tt.TrichYeu,
                                 tt.UserId,
                                 tt.DaDuyet,
                                 NguoiTao = tt.SysUser.DisplayName,
                                 FileTypeName = ft.FileTypeName,
                                 tt.ViewCount
                             });
            grvThuTuc.DataSource = lstThuTuc.ToList();
            grvThuTuc.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (VanBan != null)
            {
                if (VanBan.DaDuyet == null) VanBan.DaDuyet = true;
                else VanBan.DaDuyet = !VanBan.DaDuyet;
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

        protected void grvThuTuc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > 0)
            {
                bool daDuyet = Convert.ToBoolean(e.Row.DataItem.GetType().GetProperty("DaDuyet").ToString());
                if (!daDuyet) e.Row.Font.Bold  = true;
            }
        }
    }
}