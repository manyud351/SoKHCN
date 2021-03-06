using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Linq;
using System.Data.Linq;
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

namespace KHCNCT.Modules
{
    public partial class video_trang_chu : ModuleBaseControl
    {
        public string videoPath = "~/upload/media/video";
        protected string vVideoDauTienLink;
        protected int firstVideoId;
        
        //VIDEOCHUYENMUCController objCMController = new VIDEOCHUYENMUCController();
        //VIDEOController objVIDEOController = new VIDEOController();

        public static string vVideoUrl;
        public static string vPreviewImageUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadVideos();
            hplVideo.NavigateUrl = Common.GenerateUrl("video");
        }

        private void LoadVideos()
        {
            var lstVideos = (from v in db.VideoFiles
                                   where v.ApprovementStatus == (short)ApprovedStatus.DaDuyet && v.ShowInHomepage == true
                                   orderby v.Order, v.CreatedTime descending
                                   select new { 
                                       v.Id,
                                       v.VideoTitle,
                                       v.VideoPath,
                                       v.ViewCount,
                                       v.VideoDescription,
                                       v.CategoryId,
                                       PreviewImage = (string.IsNullOrEmpty(v.PreviewImage) ? SystemConfig.ApplicationConfig.DefaultVideoImage : v.PreviewImage)
                                   }
                               ).Take(SystemConfig.ApplicationConfig.MaxVideoShowHomepage).ToList();
            if (lstVideos.Count >= 1)
            {
                lblTieuDeVideoDauTien.Text = lstVideos[0].VideoTitle;
                vVideoDauTienLink = Common.GenerateUrl("video", "vid=" + lstVideos[0].Id.ToString());
                vVideoUrl = ResolveUrl(lstVideos[0].VideoPath);
                firstVideoId = lstVideos[0].Id;
                if (!string.IsNullOrEmpty(lstVideos[0].PreviewImage)) vPreviewImageUrl = ResolveUrl(lstVideos[0].PreviewImage);

                lstVideos.RemoveAt(0);
            }
            else this.Visible = false;

            rptOtherVideos.DataSource = lstVideos;
            rptOtherVideos.DataBind();
        }

        
    }
}