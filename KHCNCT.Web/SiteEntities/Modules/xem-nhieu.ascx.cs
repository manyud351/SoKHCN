using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Web.SiteEntities.Controls;
using KHCNCT.Data;
using KHCNCT.Globals.Enums;
using KHCNCT.Globals;

namespace KHCNCT.Web.SiteEntities.Modules
{
    public partial class xem_nhieu : ModuleBaseControl
    {
        protected int iDetailPageId = 0;
        protected int iNumOfNewsShowOnModule = 10;
        protected string sModKey = "xem-nhieu";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ModKey = sModKey;

                LoadContents();
            }
        }

        private void LoadContents()
        {
            if (ModuleConfigs != null)
            {
                int.TryParse(GetModuleConfigValue("DetailPage"), out iDetailPageId);
                int.TryParse(GetModuleConfigValue("NumOfNewsShowOnModule"), out iNumOfNewsShowOnModule);

                List<NewsContent> lstNews = (from nc in db.NewsContents
                                             where nc.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan && nc.Hidden == false
                                                   && nc.ShowInMostView == true
                                             orderby nc.ViewCount descending, nc.PublishFrom.Value descending, nc.CreatedTime.Value descending
                                             select nc).Take(iNumOfNewsShowOnModule).ToList<NewsContent>();
                int totalNews = lstNews.Count;
                if (totalNews > 0)
                {
                    imgFirstNews.ImageUrl = lstNews[0].ImagePath;
                    hplFirstNews.Text = lstNews[0].NewsTitle;
                    hplFirstNews.NavigateUrl = Common.GenerateUrl(iDetailPageId, "nid=" + lstNews[0].Id.ToString());
                }

                if (totalNews > 1)
                {
                    lstNews.RemoveAt(0);
                    rptFootColNews.DataSource = lstNews;
                    rptFootColNews.DataBind();
                }
            }
        }
    }
}