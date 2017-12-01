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
using NT.Lib;

namespace KHCNCT.Web.SiteEntities.Modules
{
    public partial class tin_cung_chuyen_muc : ModuleBaseControl
    {

        /// <summary>
        /// trang hien tai
        /// </summary>
        /// 
        public int PageIndex
        {
            get
            {
                int p = 1;
                if (Request.QueryString["p"] != null) int.TryParse(Request.QueryString["p"], out p);
                return p;
            }
        }

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
        public NewsContent CurrentNews
        {
            set { _CurrentNews = value; }
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

        public void LoadContents()
        {

            List<NewsContent> lstNews = (from nc in db.NewsContents
                                         where nc.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan
                                                 && (nc.CategoryId == CurrentNews.CategoryId || nc.NewsCategory.ParentId == CurrentNews.CategoryId)
                                         orderby nc.ViewCount descending, nc.PublishFrom.Value descending, nc.CreatedTime.Value descending
                                         select nc).Take(10).ToList<NewsContent>();

            rptFootColNews.DataSource = lstNews;
            rptFootColNews.DataBind();

            PortalPage pageToRedirect = db.PortalPages.SingleOrDefault<PortalPage>(pp=>pp.CategoryId == CurrentNews.CategoryId);
            if(pageToRedirect!=null)
            {
                hplCategory.NavigateUrl = Globals.Common.GenerateUrl(pageToRedirect.Id, "cid=" + CurrentNews.CategoryId.ToString());
                hplViewAll.NavigateUrl = hplCategory.NavigateUrl;
            }
        }
    }
}