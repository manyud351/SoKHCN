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
    public partial class tthc : CommonBaseControl
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

        string virtualUploadFolder
        {
            get
            {
                return "~/upload/" + UserId.ToString() + "/thutuchanhchinh/";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhMucThuTuc();
                if (ThuTuc != null)
                {
                    BindInfo(ThuTuc);
                    rfvThuTuc.Enabled = false;
                }
                else
                {
                    rfvThuTuc.Enabled = true;
                    hplFileThuTuc.Visible = false;
                }
            }
        }

        private void BindInfo(ThuTucHanhChinh_ChiTiet thutuc)
        {
            txtTenThuTuc.Text = thutuc.TenThuTuc;
            txtMoTa.Text = thutuc.MoTa;
            Editor1.MainEditor.Text = thutuc.ChiTiet;
            ListItem li = ddlDanhMuc.Items.FindByValue(thutuc.DanhMuc.ToString());
            if (li != null) li.Selected = true;
            hplFileThuTuc.NavigateUrl = thutuc.FileTaiVe;
            hplFileThuTuc.Text = "Xem file";
            hplFileThuTuc.Visible = true;
        }

        private void LoadDanhMucThuTuc()
        {
            List<ThuTucHanhChinh_DanhMuc> lstDMTT = (from dm in db.ThuTucHanhChinh_DanhMucs
                                                     orderby dm.ThuTu
                                                     select dm).ToList<ThuTucHanhChinh_DanhMuc>();
            ddlDanhMuc.DataSource = lstDMTT;
            ddlDanhMuc.DataTextField = "TenDanhMuc";
            ddlDanhMuc.DataValueField = "Id";
            ddlDanhMuc.DataBind();

            ddlDanhMuc.Items.Insert(0, new ListItem("", ""));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("dstthc"));
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
                ThuTucHanhChinh_ChiTiet tt = ThuTuc;

                bool isUpdate = true;
                if (tt == null)
                {
                    tt = new ThuTucHanhChinh_ChiTiet();
                    tt.NgayTao = DateTime.Now;
                    isUpdate = false;
                    tt.UserId = UserId;
                    tt.ViewCount = 0;
                    tt.DownloadCount = 0;
                }

                tt.TenThuTuc = txtTenThuTuc.Text;
                tt.MoTa = txtMoTa.Text;
                tt.ChiTiet = Editor1.MainEditor.Text;
                tt.DanhMuc = Convert.ToInt32(ddlDanhMuc.SelectedValue);

                if (fulThuTuc.HasFile)
                {
                    string ext = NT.Lib.Globals.GetFileExtension(fulThuTuc.FileName);
                    if (IsValidFile(ext))
                    {
                        //xoa file cu neu co (truong hop cap nhat)
                        if (!String.IsNullOrEmpty(tt.FileTaiVe))
                        {
                            System.IO.File.Delete(Server.MapPath(tt.FileTaiVe));
                        }

                        string uploadFolder = Server.MapPath(virtualUploadFolder);
                        if (!System.IO.Directory.Exists(uploadFolder)) System.IO.Directory.CreateDirectory(uploadFolder);

                        //tao chuoi ten file ngau nhien
                        string fileName = "ThuTuc-" + Hash.GetRandomHashKey(8) + "." + ext;
                        fulThuTuc.SaveAs(uploadFolder + fileName);
                        tt.FileTaiVe = (virtualUploadFolder + fileName);
                        tt.LoaiFile = ext;
                    }
                }
                tt.CapNhatLanCuoi = DateTime.Now;
                tt.HieuLuc = false;
                //tt.HieuLuc = true;

                if (!isUpdate) db.ThuTucHanhChinh_ChiTiets.InsertOnSubmit(tt);
                db.SubmitChanges();

                Response.Redirect(KHCNCT.Globals.Common.GenerateAdminUrl("dstthc"));
            }
        }
    }
}