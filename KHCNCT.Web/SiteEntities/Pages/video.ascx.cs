using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using KHCNCT.Web.SiteEntities.Controls;
using KHCNCT.Data;
using KHCNCT.Globals.Enums;
using KHCNCT.Globals;
using KHCNCT.Configuration;

namespace KHCNCT.Page
{
    public partial class pVideo : PageBaseControl
    {
        protected string vVideoDauTienLink;
        protected string vVideoId;
        //VIDEOCHUYENMUCController objCMController = new VIDEOCHUYENMUCController();
        //VIDEOController objVIDEOController = new VIDEOController();

        public static string vPlay;
        public static string vPreviewImageUrl;

        protected int vPlayId;

        private int _vCateId;
        public int VideoCategoryId
        {
            get
            { 
                if (_vCateId == 0 && Request.QueryString["vcid"]!=null) int.TryParse(Request.QueryString["vcid"], out _vCateId);
                return _vCateId;
            }
        }

        private int _VideoId;
        public int VideoId
        {
            get
            { 
                if (_VideoId == 0 && Request.QueryString["vid"]!=null) int.TryParse(Request.QueryString["vid"], out _VideoId);
                return _VideoId;
            }
        }

        private VideoFile _CurrentVideo;
        public VideoFile CurrentVideo
        {
            get
            {
                if (_CurrentVideo == null) _CurrentVideo = db.VideoFiles.SingleOrDefault<VideoFile>(vf => vf.Id == VideoId && vf.ApprovementStatus == (short)ApprovedStatus.DaDuyet);
                return _CurrentVideo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            divVideoNotFound.Visible = false;

            if (VideoId > 0)
            {
                LoadVideo();
            }
            else if (VideoCategoryId > 0)
            {
                LoadVideosByCategory();
            }
            else LoadVideos();
            LoadVideoCategories();
        }

        private void LoadVideo()
        {
            VideoFile vFile = CurrentVideo;
            if (vFile != null)
            {
                divVideoNotFound.Visible = false;
                divVideoContent.Visible = true;

                vPlay = ResolveUrl(vFile.VideoPath);
                vPlayId = vFile.Id;

                LoadTopVideos();
            }
            else
            {
                divVideoNotFound.Visible = true;
                divVideoContent.Visible = false;
            }
        }

        private void LoadTopVideos()
        {
            //lay cac video cung chuyen muc tru video dang hien thi
            List<VideoFile> lstVideos = (from v in db.VideoFiles
                                         where v.ApprovementStatus == (short)ApprovedStatus.DaDuyet && v.CategoryId == CurrentVideo.CategoryId && v.Id != VideoId
                                         orderby v.Order, v.CreatedTime descending
                                         select v).Take(SystemConfig.ApplicationConfig.MaxVideoShowOnPage - 1).ToList<VideoFile>();
            //neu khong tim thay load tat ca video tru video dang hien thi
            if (lstVideos.Count == 0)
            {
                lstVideos = (from v in db.VideoFiles
                             where v.ApprovementStatus == (short)ApprovedStatus.DaDuyet && v.Id != VideoId
                                         orderby v.Order, v.CreatedTime descending
                                         select v).Take(SystemConfig.ApplicationConfig.MaxVideoShowOnPage - 1).ToList<VideoFile>();
            }

            //cap nhat anh dai dien mac dinh khi video ko co anh dai dien
            for (int i = 0; i < lstVideos.Count; i++) if (String.IsNullOrEmpty(lstVideos[i].PreviewImage)) lstVideos[i].PreviewImage = SystemConfig.ApplicationConfig.DefaultVideoImage;
            
            //
            if (lstVideos.Count == 1 || lstVideos.Count == 2)
            {
                rptTopVideos.DataSource = lstVideos;
                rptTopVideos.DataBind();
            }
            else
            {
                rptTopVideos.DataSource = lstVideos.GetRange(1, 2);
                rptTopVideos.DataBind();
                
                lstVideos.RemoveRange(1, 2);

                //truong hop video con lai cua chuyen muc lien quan ko du so luong hien thi tren trang -> hien thi them video chuyen muc khac
                if (SystemConfig.ApplicationConfig.MaxVideoShowOnPage - 3 > lstVideos.Count)
                {
                    List<VideoFile> lstOtherCateVideos = (from v in db.VideoFiles 
                                                          where v.ApprovementStatus == (short)ApprovedStatus.DaDuyet && v.Id != VideoId && v.CategoryId != CurrentVideo.CategoryId
                                                          orderby v.Order, v.CreatedTime descending
                                                          select v).Take(SystemConfig.ApplicationConfig.MaxVideoShowOnPage - 3 - lstVideos.Count).ToList<VideoFile>();
                    for (int i = 0; i < lstOtherCateVideos.Count; i++) if (String.IsNullOrEmpty(lstOtherCateVideos[i].PreviewImage)) lstOtherCateVideos[i].PreviewImage = SystemConfig.ApplicationConfig.DefaultVideoImage;

                    lstVideos.AddRange(lstOtherCateVideos);
                }
                rptNextVideo.DataSource = lstVideos;
                rptNextVideo.DataBind();
            }
        }

        /// <summary>
        /// Load tat ca video hien co
        /// </summary>
        private void LoadVideos()
        {
            List<VideoFile> lstVideos = (from v in db.VideoFiles
                                         where v.ApprovementStatus == (short)ApprovedStatus.DaDuyet
                                         orderby v.Order, v.CreatedTime descending
                                         select v).Take(SystemConfig.ApplicationConfig.MaxVideoShowOnPage).ToList<VideoFile>();
            
            for (int i = 0; i < lstVideos.Count; i++) if (String.IsNullOrEmpty(lstVideos[i].PreviewImage)) lstVideos[i].PreviewImage = SystemConfig.ApplicationConfig.DefaultVideoImage;

            if (lstVideos.Count > 1)
            {
                lblTieuDe.Text = lstVideos[0].VideoTitle;
                vVideoDauTienLink = Common.GenerateUrl("video", "vid=" + lstVideos[0].Id.ToString());
                vPlay = ResolveUrl(lstVideos[0].VideoPath);
                vPlayId = lstVideos[0].Id;
                if (!string.IsNullOrEmpty(lstVideos[0].PreviewImage)) vPreviewImageUrl = ResolveUrl(lstVideos[0].PreviewImage);
                else vPreviewImageUrl = ResolveUrl(SystemConfig.ApplicationConfig.DefaultVideoImage);

                lstVideos.RemoveAt(0);
            }

            //after removed
            if (lstVideos.Count > 0)
            {
                if (lstVideos.Count <= 2)
                {
                    rptTopVideos.DataSource = lstVideos;
                    rptTopVideos.DataBind();
                }
                else
                {
                    rptTopVideos.DataSource = lstVideos.GetRange(1, 2);
                    rptTopVideos.DataBind();

                    lstVideos.RemoveRange(1, 2);
                }
            }

            rptNextVideo.DataSource = lstVideos;
            rptNextVideo.DataBind();
        }

        private void LoadVideosByCategory()
        {
            List<VideoFile> lstVideos = (from v in db.VideoFiles
                                         where v.ApprovementStatus == (short)ApprovedStatus.DaDuyet && v.CategoryId == VideoCategoryId
                                         orderby v.Order, v.CreatedTime descending
                                         select v).Take(SystemConfig.ApplicationConfig.MaxVideoShowOnPage).ToList<VideoFile>();

            for (int i = 0; i < lstVideos.Count; i++) if (String.IsNullOrEmpty(lstVideos[i].PreviewImage)) lstVideos[i].PreviewImage = SystemConfig.ApplicationConfig.DefaultVideoImage;

            if (lstVideos.Count > 1)
            {
                lblTieuDe.Text = lstVideos[0].VideoTitle;
                vVideoDauTienLink = Common.GenerateUrl("video", "vid=" + lstVideos[0].Id.ToString());
                vPlay = ResolveUrl(lstVideos[0].VideoPath);
                vPlayId = lstVideos[0].Id;
                if (!string.IsNullOrEmpty(lstVideos[0].PreviewImage)) vPreviewImageUrl = ResolveUrl(lstVideos[0].PreviewImage);
                else vPreviewImageUrl = ResolveUrl(SystemConfig.ApplicationConfig.DefaultVideoImage);

                lstVideos.RemoveAt(0);
            }
            else
            {
                divVideoContent.Visible = false;
                divVideoNotFound.Visible = true;
            }

            //after removed
            if (lstVideos.Count > 0)
            {
                if (lstVideos.Count <= 2)
                {
                    rptTopVideos.DataSource = lstVideos;
                    rptTopVideos.DataBind();
                }
                else
                {
                    rptTopVideos.DataSource = lstVideos.GetRange(1, 2);
                    rptTopVideos.DataBind();

                    lstVideos.RemoveRange(1, 2);
                }
            }

            //truong hop video con lai cua chuyen muc lien quan ko du so luong hien thi tren trang -> hien thi them video chuyen muc khac
            if (SystemConfig.ApplicationConfig.MaxVideoShowOnPage - 3 > lstVideos.Count)
            {
                List<VideoFile> lstOtherCateVideos = (from v in db.VideoFiles
                                                      where v.ApprovementStatus == (short)ApprovedStatus.DaDuyet && v.CategoryId != VideoCategoryId
                                                      orderby v.Order, v.CreatedTime descending
                                                      select v).Take(SystemConfig.ApplicationConfig.MaxVideoShowOnPage - 3 - lstVideos.Count).ToList<VideoFile>();
                for (int i = 0; i < lstOtherCateVideos.Count; i++) if (String.IsNullOrEmpty(lstOtherCateVideos[i].PreviewImage)) lstOtherCateVideos[i].PreviewImage = SystemConfig.ApplicationConfig.DefaultVideoImage;

                lstVideos.AddRange(lstOtherCateVideos);
            }

            rptNextVideo.DataSource = lstVideos;
            rptNextVideo.DataBind();
        }


        private void LoadVideoCategories()
        {
            List<VideoCategory> lstCate = (from vc in db.VideoCategories
                                           orderby vc.Order
                                           select vc).ToList<VideoCategory>();
            rptVideoCategories.DataSource = lstCate;
            rptVideoCategories.DataBind();
        }

    }
}