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
    public partial class tin_noi_bat : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContents();
        }

        private void LoadContents()
        {
            List<NewsContent> lstNews = (from nc in db.NewsContents
                                         where nc.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan
                                                 && nc.ShowInFeature == true
                                         orderby nc.PublishFrom.Value descending, nc.CreatedTime.Value descending
                                         select nc).Take(6).ToList<NewsContent>();
            int totalNews = lstNews.Count;
            if(totalNews > 0)
            {
                imgFirstNews.ImageUrl = lstNews[0].ImagePath;
                hplFirstNews.Text = lstNews[0].NewsTitle;
                hplFirstNews.NavigateUrl = Common.GenerateUrl(57,"nid=" + lstNews[0].Id.ToString());
                ltrDescription.Text = lstNews[0].Description;
            }
            if (totalNews > 1)
            {
                rptRightColNews.DataSource = lstNews.GetRange(1, 2);
                rptRightColNews.DataBind();
            }
            if (totalNews > 3)
            {
                rptFootColNews.DataSource = lstNews.GetRange(3, 3);
                rptFootColNews.DataBind();
            }
        }
    }
}