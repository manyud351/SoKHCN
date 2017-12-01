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
    public partial class vmenu : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptMainMenu.DataSource = GetPages(0);
            rptMainMenu.DataBind();
        }

        protected List<PortalPage> GetPages(int parentid)
        {
            if (parentid == 0)
            {
                return (from p in db.PortalPages
                        where p.Hidden == false && p.ShowInMainMenu == true && p.PageLevel == 1
                        orderby p.Order
                        select p).ToList<PortalPage>();
            }
            else
            {
                return (from p in db.PortalPages
                        where p.Hidden == false && p.ShowInMainMenu == true && p.ParentPageId == parentid
                        orderby p.Order
                        select p).ToList<PortalPage>();
            }
        }

        protected List<NewsContent> GetNews(int cateId)
        {
            if (cateId != 0)
            {
                List<NewsContent> lstNews = (from p in db.NewsContents
                        where p.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan && (p.CategoryId == cateId || p.NewsCategory.ParentId == cateId)
                        orderby p.PublishFrom descending, p.CreatedBy descending
                        select p).Take(4).ToList<NewsContent>();
                foreach (NewsContent nc in lstNews)
                {
                    if (String.IsNullOrEmpty(nc.ImagePath)) nc.ImagePath = Configuration.SystemConfig.ApplicationConfig.DefaultImage;
                }
                return lstNews;
            }
            else
            {
                return null;
            }
        }


    }
}