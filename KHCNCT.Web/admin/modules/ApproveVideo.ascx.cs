using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Admin.Controls;
using KHCNCT.Globals;
using KHCNCT.Data;
using NT.Lib;
using KHCNCT.Configuration;
using KHCNCT.Globals.Enums;
using System.Collections;
using Telerik.Web.UI;

namespace KHCNCT.Web.admin.modules
{

    public partial class ApproveVideo : CommonBaseControl
    {
        /// <summary>
        /// View hien tai cua control. Moi thoi diem chi co 1 View hien thi
        /// </summary>
        private View CurrentView
        {
            get
            {
                if (Request.QueryString["act"] != null)
                {
                    switch (Request.QueryString["act"])
                    {
                        case "update": return View.Update;
                        case "manage": return View.Manage;
                        case "preview": return View.Preview;
                        default: return View.Manage;
                    }
                }
                else return View.Manage;
            }
        }

        private int _vid;
        protected int VideoId
        {
            get
            {
                if (_vid == 0 && Request.QueryString["vid"] != null) int.TryParse(Request.QueryString["vid"], out _vid);
                return _vid;
            }
        }

        private VideoFile _currentVideo;
        protected VideoFile CurrentVideo
        {
            get
            {
                if (_currentVideo == null && VideoId > 0) _currentVideo = db.VideoFiles.SingleOrDefault<VideoFile>(v => v.Id == VideoId);
                return _currentVideo;
            }
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowPanel();
            if (CurrentView == View.Preview)
            {
                if (CurrentVideo != null)
                {
                    if (!String.IsNullOrEmpty(CurrentVideo.PreviewImage))
                    {
                        hdfPreviewPreviewImage.Value = ResolveUrl(CurrentVideo.PreviewImage);
                        hdfPreviewVideoFile.Value = ResolveUrl(CurrentVideo.VideoPath);
                    }
                }
            }
            else if (CurrentView == View.Update)
            {
                divImgPreview.Visible = false;              
                if (!IsPostBack)
                {
                    LoadVideoCategories();
                    if (CurrentVideo != null)
                    {
                        ddlCategory.Items.FindByValue(CurrentVideo.CategoryId.Value.ToString()).Selected = true;
                        txtDescription.Text = CurrentVideo.VideoDescription;
                        txtVideoTitle.Text = CurrentVideo.VideoTitle;
                        rntOrder.Value = CurrentVideo.Order;
                        ckbShowInHomepage.Checked = (CurrentVideo.ShowInHomepage.HasValue && CurrentVideo.ShowInHomepage.Value);

                        LoadVideoApprHis();

                        if (CurrentVideo.PreviewImage != "")
                        {
                            divImgPreview.Visible = true;
                            if (!String.IsNullOrEmpty(CurrentVideo.PreviewImage))
                            {
                                hdfPreviewImage.Value = ResolveUrl(CurrentVideo.PreviewImage);
                                imgPreview.ImageUrl = CurrentVideo.PreviewImage;
                            }
                        }
                        else divImgPreview.Visible = false;

                        divVideo.Visible = true;
                        hdfVideoFile.Value = ResolveUrl(CurrentVideo.VideoPath);
                    }
                    else
                    {
                        divImgPreview.Visible = false;
                        divVideo.Visible = false;
                    }
                }
            }
            else
            {
                //LoadVideos();
                LoadVideoShowCategories();
            }
            
        }

        private void  LoadVideoCategories()
        {
            List<VideoCategory> lstCate = db.VideoCategories.OrderBy<VideoCategory, int>(vc => vc.Order.Value).ToList<VideoCategory>();
            ddlCategory.DataSource = lstCate;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "Id";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("", ""));
        }

        private void LoadVideoShowCategories()
        {
            List<VideoCategory> lstCate = db.VideoCategories.OrderBy<VideoCategory, int>(vc => vc.Order.Value).ToList<VideoCategory>();
            rcbCategoryShow.DataSource = lstCate;
            rcbCategoryShow.DataTextField = "CategoryName";
            rcbCategoryShow.DataValueField = "Id";
            rcbCategoryShow.DataBind();

            rcbCategoryShow.Items.Insert(0, new RadComboBoxItem("Tất cả", "0"));
        }

        private void LoadVideos()
        {
            string dfi = SystemConfig.ApplicationConfig.DefaultImage;
            IList lstVideos = null;
            if (rcbApproved.SelectedIndex > 0)
            {
                lstVideos = (from vf in db.VideoFiles
                             where vf.ApprovementStatus == Convert.ToInt16(rcbApproved.SelectedValue)
                                    && (rcbCategoryShow.SelectedIndex > 0 ? vf.CategoryId == Convert.ToInt32(rcbCategoryShow.SelectedValue) : (vf.CategoryId == vf.CategoryId))
                             orderby vf.CreatedTime descending
                             select new
                             {
                                 vf.Id,
                                 vf.Order,
                                 PreviewImage = (vf.PreviewImage == null || vf.PreviewImage == "") ? SystemConfig.ApplicationConfig.DefaultImage : vf.PreviewImage,
                                 vf.ApprovedBy,
                                 vf.ApprovedTime,
                                 vf.ApprovementStatus,
                                 ApprovementDescription = (vf.ApprovementStatus == (short)ApprovedStatus.DangChoDuyet) ? "Đang chờ duyệt" : ((vf.ApprovementStatus == (short)ApprovedStatus.DaDuyet) ? "Đã duyệt" : "Không duyệt"),
                                 vf.CategoryId,
                                 CategoryName = vf.VideoCategory.CategoryName,
                                 vf.VideoPath,
                                 vf.VideoTitle,
                                 vf.ViewCount,
                                 vf.ShowInHomepage,
                                 vf.OriginalFileName
                             }).ToList();
            }
            else
            {
                lstVideos = (from vf in db.VideoFiles
                             where (rcbCategoryShow.SelectedIndex > 0 ? vf.CategoryId == Convert.ToInt32(rcbCategoryShow.SelectedValue) : (vf.CategoryId == vf.CategoryId))
                             orderby vf.CreatedTime descending
                             select new
                             {
                                 vf.Id,
                                 vf.Order,
                                 PreviewImage = (vf.PreviewImage == null || vf.PreviewImage == "") ? SystemConfig.ApplicationConfig.DefaultImage : vf.PreviewImage,
                                 vf.ApprovedBy,
                                 vf.ApprovedTime,
                                 vf.ApprovementStatus,
                                 ApprovementDescription = (vf.ApprovementStatus == (short)ApprovedStatus.DangChoDuyet) ? "Đang chờ duyệt" : ((vf.ApprovementStatus == (short)ApprovedStatus.DaDuyet) ? "Đã duyệt" : "Không duyệt"),
                                 vf.CategoryId,
                                 CategoryName = vf.VideoCategory.CategoryName,
                                 vf.VideoPath,
                                 vf.VideoTitle,
                                 vf.ViewCount,
                                 vf.ShowInHomepage,
                                 vf.OriginalFileName
                             }).ToList();
            }
            grvVideo.DataSource = lstVideos;
            grvVideo.DataBind();
        }

        private void LoadVideoApprHis()
        {
            if (CurrentVideo != null)
            {
                IList lstAppHis = (from ah in db.VideoApprovementHis
                                   where ah.VideoId == CurrentVideo.Id
                                   orderby ah.ApprovedTime descending
                                   select new { 
                                        ah.VideoId,
                                        ah.VideoOwner,
                                        ah.VideoTitle,
                                        ah.OriginalFileName,
                                        ApprovementResult = (ah.ApprovementStatus == (short)ApprovedStatus.DaDuyet) ? "Đã duyệt" : ((ah.ApprovementStatus == (short)ApprovedStatus.KhongDuyet) ? "Không duyệt": "Đang chờ duyệt"),
                                        ah.ApprovementDes,
                                        ah.ApprovementStatus,
                                        ah.ApprovedTime,
                                        ah.ApprovedBy
                                   }).ToList();
                grvApprovementHis.DataSource = lstAppHis;
                grvApprovementHis.DataBind();
            }            
        }

        /// <summary>
        /// Hien thi View hien tai cua control. Moi thoi diem chi co 1 View hien thi
        /// </summary>
        private void ShowPanel()
        {
            divManage.Visible = (CurrentView == View.Manage);
            divPreview.Visible = (CurrentView == View.Preview);
            divUpdate.Visible = (CurrentView == View.Update);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("approvevideo"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                VideoFile video = CurrentVideo;
                if (video == null)
                {
                    return;
                }
                else
                {
                    video.VideoTitle = txtVideoTitle.Text;
                    video.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                    video.Order = Convert.ToInt32(rntOrder.Value);
                    video.VideoDescription = txtDescription.Text;
                    if(ddlApproveVideo.SelectedIndex == 1) video.ApprovementStatus = (short)ApprovedStatus.DaDuyet;
                    else if (ddlApproveVideo.SelectedIndex == 2) video.ApprovementStatus = (short)ApprovedStatus.KhongDuyet;
                    video.ApprovedBy = UserInfo.UserAccount.AccountName;
                    video.ApprovedTime = DateTime.Now;
                    video.ShowInHomepage = ckbShowInHomepage.Checked;

                    VideoApprovementHis his = new VideoApprovementHis();
                    his.VideoId = video.Id;
                    his.VideoOwner = video.UserAccount.AccountName;
                    his.VideoTitle = video.VideoTitle;
                    his.OriginalFileName = video.OriginalFileName;
                    his.ApprovementDes = txtApprovementDes.Text;
                    his.ApprovementStatus = video.ApprovementStatus;
                    his.ApprovedTime = DateTime.Now;
                    his.ApprovedBy = UserInfo.UserAccount.AccountName;

                    db.VideoApprovementHis.InsertOnSubmit(his);

                    db.SubmitChanges();

                    Response.Redirect(Common.GenerateAdminUrl("approvevideo"));
                }
            }
        }

        protected void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadVideos();
        }
    }
}