using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Admin.Controls;
using KHCNCT.Data;
using NT.Lib;
using Telerik.Web.UI;
using KHCNCT.Globals;
using KHCNCT.Configuration;
using KHCNCT.Globals.Enums;
using System.Collections;


namespace KHCNCT.Web.admin.modules
{
    public partial class vbpq : CommonBaseControl
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

        string virtualUploadFolder
        {
            get
            {
                return "~/upload/" + UserId.ToString() + "/vanbanphapquy/";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLinhVucVanBan();
                LoadCQBHVanBan();
                LoadLoaiVanBan();
                rdConHL.Checked = true;
                if (VanBan != null)
                {
                    BindInfo(VanBan);
                    rfvVanBan.Enabled = false;                    
                }
                else
                {
                    rfvVanBan.Enabled = true;
                    hplFileVanBan.Visible = false;
                }
            }
        }

        private void BindInfo(VBPQ_VanBanChiTiet VanBan)
        {
            txtSoHieuVB.Text = VanBan.SoHieu;
            txtTrichYeu.Text = VanBan.TrichYeu;
            Editor1.MainEditor.Text = VanBan.ChiTiet;

            ListItem liLV = ddlLinhVuc.Items.FindByValue(VanBan.LinhVuc.ToString());
            if (liLV != null) liLV.Selected = true;

            ListItem liCQBH = ddlCQBH.Items.FindByValue(VanBan.CQBH.ToString());
            if (liCQBH != null) liCQBH.Selected = true;

            ListItem liLVB = ddlLoaiVB.Items.FindByValue(VanBan.LoaiVB.ToString());
            if (liLVB != null) liLVB.Selected = true;

            rdpNgayBH.SelectedDate = VanBan.NgayBH;
            rdpNgayHL.SelectedDate = VanBan.NgayHL;

            hplFileVanBan.NavigateUrl = VanBan.FileDinhKem;
            hplFileVanBan.Text = "Xem file";
            hplFileVanBan.Visible = true;
        }

        private void LoadLinhVucVanBan()
        {
            List<VBPQ_LinhVuc> lstDMTT = (from dm in db.VBPQ_LinhVucs
                                          select dm).ToList<VBPQ_LinhVuc>();
            ddlLinhVuc.DataSource = lstDMTT;
            ddlLinhVuc.DataTextField = "TenLinhVuc";
            ddlLinhVuc.DataValueField = "Id";
            ddlLinhVuc.DataBind();

            ddlLinhVuc.Items.Insert(0, new ListItem("", ""));
        }

        private void LoadCQBHVanBan()
        {
            List<VBPQ_CoQuanBH> lstDMTT = (from dm in db.VBPQ_CoQuanBHs
                                           select dm).ToList<VBPQ_CoQuanBH>();
            ddlCQBH.DataSource = lstDMTT;
            ddlCQBH.DataTextField = "TenCoQuanBH";
            ddlCQBH.DataValueField = "Id";
            ddlCQBH.DataBind();

            ddlCQBH.Items.Insert(0, new ListItem("", ""));
        }

        private void LoadLoaiVanBan()
        {
            List<VBPQ_LoaiVB> lstDMTT = (from dm in db.VBPQ_LoaiVBs
                                         select dm).ToList<VBPQ_LoaiVB>();
            ddlLoaiVB.DataSource = lstDMTT;
            ddlLoaiVB.DataTextField = "LoaiVB";
            ddlLoaiVB.DataValueField = "Id";
            ddlLoaiVB.DataBind();

            ddlLoaiVB.Items.Insert(0, new ListItem("", ""));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("dsvbpq"));
        }

        protected bool IsValidFile(string extension)
        {
            if (String.IsNullOrEmpty(extension)) return false;
            else
            {
                string[] imgExtensions = "doc;docx;pdf;xls;xlsx;tif;tiff;rar;zip".Split(";".ToCharArray());

                bool found = false;
                int i = imgExtensions.Length - 1;

                while ((!found) && (i >= 0))
                {
                    found = extension.Equals(imgExtensions[i--]);
                }

                return found;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                VBPQ_VanBanChiTiet tt = VanBan;

                bool isUpdate = true;
                if (tt == null)
                {
                    tt = new VBPQ_VanBanChiTiet();
                    tt.NgayTao = DateTime.Now;
                    isUpdate = false;
                    tt.UserId = UserId;
                    tt.ViewCount = 0;
                    tt.DownloadCount = 0;
                }

                tt.SoHieu = txtSoHieuVB.Text;
                tt.TrichYeu = txtTrichYeu.Text;
                tt.ChiTiet = Editor1.MainEditor.Text;
                tt.LinhVuc = Convert.ToInt32(ddlLinhVuc.SelectedValue);
                tt.LoaiVB = Convert.ToInt32(ddlLoaiVB.SelectedValue);
                tt.CQBH = Convert.ToInt32(ddlCQBH.SelectedValue);

                if (fulVanBan.HasFile)
                {
                    string ext = NT.Lib.Globals.GetFileExtension(fulVanBan.FileName);
                    if (IsValidFile(ext))
                    {
                        //xoa file cu neu co (truong hop cap nhat)
                        if (!String.IsNullOrEmpty(tt.FileDinhKem))
                        {
                            System.IO.File.Delete(Server.MapPath(tt.FileDinhKem));
                        }

                        string uploadFolder = Server.MapPath(virtualUploadFolder);
                        if (!System.IO.Directory.Exists(uploadFolder)) System.IO.Directory.CreateDirectory(uploadFolder);

                        //tao chuoi ten file ngau nhien
                        string fileName = "VBPQ-" + Hash.GetRandomHashKey(8) + "." + ext;
                        fulVanBan.SaveAs(uploadFolder + fileName);
                        tt.FileDinhKem = (virtualUploadFolder + fileName);
                        tt.LoaiFile = ext;
                    }
                }
                tt.NgayCapNhat = DateTime.Now;
                tt.NgayHL = rdpNgayHL.SelectedDate;
                tt.NgayBH = rdpNgayBH.SelectedDate;
                tt.ConHieuLuc = (rdConHL.Checked);
                tt.DaDuyet = false;
                //tt.HieuLuc = true;

                if (!isUpdate) db.VBPQ_VanBanChiTiets.InsertOnSubmit(tt);
                db.SubmitChanges();

                Response.Redirect(KHCNCT.Globals.Common.GenerateAdminUrl("dsvbpq"));
            }
        }
    }
}