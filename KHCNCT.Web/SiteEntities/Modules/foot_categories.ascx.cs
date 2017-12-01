using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Web.SiteEntities.Controls;
using KHCNCT.Data;

namespace KHCNCT.Web.SiteEntities.Modules
{
    public partial class foot_categories : ModuleBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptCategories.DataSource = GetCategories(0);
            rptCategories.DataBind();
        }

        protected IQueryable GetCategories(int parentId)
        {
            if (parentId == 0)
            {
                var result = (from nc in db.NewsCategories
                              where nc.ShowInFootMenu == true && nc.Hide == false && nc.Level == 1
                              orderby nc.Order, nc.CategoryName
                              select new
                              {
                                  nc.CategoryName,
                                  nc.Description,
                                  nc.Id,
                                  PageToRedirect = (from pp in db.PortalPages where pp.CategoryId == nc.Id select pp.Id).SingleOrDefault<int>()
                              });
                return result;
            }
            else
            {
                var result = (from nc in db.NewsCategories
                              where nc.ShowInFootMenu == true && nc.Hide == false && nc.ParentId == parentId
                              orderby nc.Order, nc.CategoryName
                              select new
                              {
                                  nc.CategoryName,
                                  nc.Description,
                                  nc.Id,
                                  PageToRedirect = (from pp in db.PortalPages where pp.CategoryId == nc.Id select pp.Id).SingleOrDefault<int>()
                              }).Take(4);
                return result;
            }
        }
    }
}