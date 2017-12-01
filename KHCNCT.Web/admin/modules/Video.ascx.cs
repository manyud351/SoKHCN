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

namespace KHCNCT.Web.admin.modules
{
    /// <summary>
    /// Dinh nghia cac View cua Control
    /// </summary>
    enum View { Manage, Update, Preview }

    public partial class Video : CommonBaseControl
    {
        string VideoFolder
        {
            get
            {
                return "~/upload/videos/files/";
            }
        }

        string PreviewImageFolder
        {
            get
            {
                return "~/upload/videos/preview/";
            }
        }

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
                if (_currentVideo == null && VideoId > 0) _currentVideo = db.VideoFiles.SingleOrDefault<VideoFile>(v => v.Id == VideoId & v.UserId == UserId);
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
                LoadVideos();
                hplAddNewVideo.NavigateUrl = KHCNCT.Globals.Common.GenerateAdminUrl("video", "act=update");
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

        private void LoadVideos()
        {
            string dfi = SystemConfig.ApplicationConfig.DefaultImage;
            IList lstVideos = (from vf in db.VideoFiles
                              where vf.UserId == UserId
                              orderby vf.CreatedTime descending
                              select new { 
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
            grvVideo.DataSource = lstVideos;
            grvVideo.DataBind();
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
            Response.Redirect(Common.GenerateAdminUrl("video"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                VideoFile video = CurrentVideo;
                bool isUpdate = true;

                if (video == null)
                {
                    if (!fulVideoFile.HasFile)
                    {
                        return;
                    }
                    else
                    {
                        video = new VideoFile();
                        video.CreatedTime = DateTime.Now;
                        video.UserId = UserId;
                        video.ViewCount = 0;

                        video.ApprovementStatus = (short)ApprovedStatus.DangChoDuyet;

                        isUpdate = false;
                    }
                }
                else if (CurrentVideo.ApprovementStatus == (short)ApprovedStatus.DaDuyet) return;

                //kiem tra kieu va kich co file
                if (NT.Lib.Globals.IsVideoFile(fulVideoFile.FileName))
                {
                    //xoa anh cu neu co (truong hop cap nhat)
                    if (!String.IsNullOrEmpty(video.VideoPath))
                    {
                        System.IO.File.Delete(Server.MapPath(video.VideoPath));
                    }

                    string uploadFolder = Server.MapPath(VideoFolder);
                    if (!System.IO.Directory.Exists(uploadFolder)) System.IO.Directory.CreateDirectory(uploadFolder);

                    //tao chuoi ten file ngau nhien
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Hash.GetRandomHashKey(4) + "." + NT.Lib.Globals.GetFileExtension(fulVideoFile.FileName);

                    //luu duong dan anh vao db
                    video.VideoPath = VideoFolder + fileName;
                    video.OriginalFileName = fulVideoFile.FileName;

                    //luu vao o cung
                    fulVideoFile.SaveAs(Server.MapPath(video.VideoPath));
                }

                //neu co anh dai dien
                if (fulImage.HasFile)
                {
                    //kiem tra kieu va kich co file
                    if (NT.Lib.Globals.IsImageFile(fulImage.FileName))
                    {
                        //xoa anh cu neu co (truong hop cap nhat)
                        if (!String.IsNullOrEmpty(video.PreviewImage))
                        {
                            System.IO.File.Delete(Server.MapPath(video.PreviewImage));
                        }

                        string uploadFolder = Server.MapPath(PreviewImageFolder);
                        if (!System.IO.Directory.Exists(uploadFolder)) System.IO.Directory.CreateDirectory(uploadFolder);

                        //tao chuoi ten file ngau nhien
                        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Hash.GetRandomHashKey(4) + "." + NT.Lib.Globals.GetFileExtension(fulImage.FileName);

                        //resize anh
                        System.Drawing.Image img = System.Drawing.Image.FromStream(fulImage.PostedFile.InputStream);
                        System.Drawing.Image imgResized = Utilities.ResizeImage(img, SystemConfig.ApplicationConfig.News_MaxImageWidth, SystemConfig.ApplicationConfig.News_MaxImageWidth);
                        imgResized.Save(uploadFolder + fileName);//luu file vao o cung

                        //luu duong dan anh vao db
                        video.PreviewImage = PreviewImageFolder + fileName;
                    }
                }

                video.VideoTitle = txtVideoTitle.Text;
                video.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                video.Order = Convert.ToInt32(rntOrder.Value);
                video.VideoDescription = txtDescription.Text;
                video.ApprovementStatus = (short)ApprovedStatus.DangChoDuyet;

                if (!isUpdate) db.VideoFiles.InsertOnSubmit(video);
                db.SubmitChanges();

                Response.Redirect(Common.GenerateAdminUrl("video"));
            }
        }

        protected void grvVideo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(grvVideo.DataKeys[e.RowIndex].Value);
            VideoFile vFile = db.VideoFiles.SingleOrDefault<VideoFile>(v => v.Id == id);
            if (vFile != null)
            {
                //xoa anh cu neu co (truong hop cap nhat)
                if (!String.IsNullOrEmpty(vFile.PreviewImage))
                {
                    System.IO.File.Delete(Server.MapPath(vFile.PreviewImage));
                }
                //xoa anh cu neu co (truong hop cap nhat)
                if (!String.IsNullOrEmpty(vFile.VideoPath))
                {
                    System.IO.File.Delete(Server.MapPath(vFile.VideoPath));
                }
                //xoa binh luan neu co
                db.VideoComments.DeleteAllOnSubmit<VideoComment>(from vc in db.VideoComments where vc.VideoId == id select vc);

                db.VideoFiles.DeleteOnSubmit(vFile);

                db.SubmitChanges();
                Response.Redirect(Common.GenerateAdminUrl("video"));
            }
        }
    }
}