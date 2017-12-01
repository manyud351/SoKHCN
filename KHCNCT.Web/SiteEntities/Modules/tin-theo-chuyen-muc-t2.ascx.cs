using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Web.SiteEntities.Controls;
using KHCNCT.Data;
using KHCNCT.Globals.Enums;

namespace KHCNCT.Web.SiteEntities.Modules
{
    public partial class tin_theo_chuyen_muc_t2 : ModuleBaseControl
    {
        protected int iCategoryId = 0;
        protected int iCaptionPageId = 0;
        protected int iDetailPageId = 0;
        protected int iNumOfNewsShowOnModule = 10;
        //public string sModKey;
        protected string sTopicImage = "~/images/no_image.png";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //ModKey = "tin-theo-chuyen-muc-t2";
            if (!IsPostBack)
            {
                LoadContents();
            }
        }

        private void LoadContents()
        {
            if (ModuleConfigs.Count > 0)
            {
                int.TryParse(GetModuleConfigValue("CategoryId"), out iCategoryId);
                int.TryParse(GetModuleConfigValue("CaptionPage"), out iCaptionPageId);
                int.TryParse(GetModuleConfigValue("DetailPage"), out iDetailPageId);
                
                ModuleConfiguration configImgTopic = GetModuleConfig("TopicImage");
                if (configImgTopic != null) sTopicImage = configImgTopic.Value;

                imgTopic.ImageUrl = sTopicImage;

                int.TryParse(GetModuleConfig("NumOfNewsShowOnModule").Value, out iNumOfNewsShowOnModule);

                NewsCategory cate = db.NewsCategories.SingleOrDefault<NewsCategory>(nc => nc.Id == iCategoryId);
                if (cate != null)
                {
                    hplCaption.Text = cate.CategoryName;
                    hplCaption.NavigateUrl = KHCNCT.Globals.Common.GenerateUrl(iCaptionPageId, "cid=" + cate.Id.ToString());
                    hplImageTopic.NavigateUrl = hplCaption.NavigateUrl;
                    List<NewsContent> lstNews = (from nc in db.NewsContents
                                                 where nc.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan && nc.Hidden == false
                                                        && (nc.CategoryId == iCategoryId || nc.NewsCategory.ParentId == iCategoryId)
                                                 orderby nc.PublishFrom.Value descending, nc.CreatedTime.Value descending
                                                 select nc).Take(iNumOfNewsShowOnModule).ToList<NewsContent>();
                    rptNews.DataSource = lstNews;
                    rptNews.DataBind();
                }
            }                                     
        }
    }
}