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
    public partial class tin_lien_quan : BaseControl
    {
        private int _NewsId;
        protected int NewsId
        {
            get
            {
                if ((_NewsId == 0) && (Request.QueryString["nid"] != null))
                {
                    int.TryParse(Request.QueryString["nid"], out _NewsId);
                }
                return _NewsId;
            }
        }

        NewsContent _CurrentNews;
        NewsContent CurrentNews
        {
            get
            {
                if (_CurrentNews == null)
                {
                    _CurrentNews = db.NewsContents.SingleOrDefault<NewsContent>(nc => nc.Id == NewsId && (nc.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan));
                }
                return _CurrentNews;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(CurrentNews!=null) LoadContents();
        }

        private void LoadContents()
        {
            List<NewsContent> lstNews = (from nc in db.NewsContents
                                         where nc.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan
                                                 && (nc.CategoryId == CurrentNews.CategoryId || nc.NewsCategory.ParentId == CurrentNews.CategoryId)
                                         orderby nc.ViewCount descending, nc.PublishFrom.Value descending, nc.CreatedTime.Value descending
                                         select nc).Take(5).ToList<NewsContent>();
            int totalNews = lstNews.Count;
            if (totalNews > 0)
            {
                if (lstNews[0].ImagePath != "") imgFirstNews.ImageUrl = lstNews[0].ImagePath;
                else imgFirstNews.ImageUrl = Configuration.SystemConfig.ApplicationConfig.DefaultImage;

                hplFirstNews.Text = lstNews[0].NewsTitle;
                hplFirstNews.NavigateUrl = Common.GenerateUrl(57, "nid=" + lstNews[0].Id.ToString());

                lstNews.RemoveAt(0);
            }

            rptFootColNews.DataSource = lstNews;
            rptFootColNews.DataBind();
        }
    }
}