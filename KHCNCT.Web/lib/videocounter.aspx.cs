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
using KHCNCT.Data;
using KHCNCT.Globals.Enums;

namespace IDC.Module.Video
{
    public partial class DesktopModules_VIDEO_videocounter : System.Web.UI.Page
    {
        protected KHCNCTDataContext db = new KHCNCTDataContext(ConfigurationManager.ConnectionStrings["SoKHCNCT"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["vid"] != null)
            {
                int vid = 0;
                if (int.TryParse(Request.QueryString["vid"], out vid))
                {
                    InscreaseCounter(vid);
                }
            }
        }

        public void InscreaseCounter(int iVid)
        {
            VideoFile video = db.VideoFiles.SingleOrDefault<VideoFile>(v => v.Id == iVid && v.ApprovementStatus == (short)ApprovedStatus.DaDuyet);
            if (video != null)
            {
                if (video.ViewCount.HasValue) video.ViewCount++;
                else video.ViewCount = 1;

                db.SubmitChanges();
            }
        }
    }
}