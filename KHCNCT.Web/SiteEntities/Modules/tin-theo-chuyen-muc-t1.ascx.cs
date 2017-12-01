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
    public partial class tin_theo_chuyen_muc_t1 : ModuleBaseControl
    {
        protected string ModuleTitle;
        protected int iDetailPage;

        public string ModTitle
        {
            get
            {
                return ModTitle;
            }
            set { ModTitle = value; }
        }

        private int _CategoryId;
        public int CategoryId
        {
            get
            {
                return _CategoryId;
            }
            set { _CategoryId = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            hplModTitle.Text = ModTitle;
            LoadContents();
        }

        private void LoadContents()
        {
            List<NewsContent> lstNews = (from nc in db.NewsContents
                                         where nc.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan
                                                 && nc.ShowInMostView == true
                                         orderby nc.ViewCount descending, nc.PublishFrom.Value descending, nc.CreatedTime.Value descending
                                         select nc).Take(5).ToList<NewsContent>();
            int totalNews = lstNews.Count;

            ModuleConfiguration configImgTopic = GetModuleConfig("DetailPage");
            if (configImgTopic != null) iDetailPage = Convert.ToInt32(configImgTopic.Value);

            if (totalNews > 0)
            {

                imgFirstNews.ImageUrl = lstNews[0].ImagePath;
                hplFirstNews.Text = lstNews[0].NewsTitle;
                hplFirstNews.NavigateUrl = Common.GenerateUrl(iDetailPage, "nid=" + lstNews[0].Id.ToString());
            }
            
            if (totalNews > 1)
            {
                lstNews.RemoveAt(0);
                rptNextNews.DataSource = lstNews;
                rptNextNews.DataBind();
            }
        }
    }
}