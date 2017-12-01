using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using KHCNCT.Data;
using System.Configuration;

namespace KHCNCT.Web.SiteEntities.Controls
{
    public partial class SiteWrapper : PageBaseControl
    {
        string _SkinPath = "";
        private void LoadSkin(string template)
        {
            string skinPath = _SkinPath + template;
            string cssPath = skinPath + "/css/style.css";

            HtmlLink cssLink = new HtmlLink();
            cssLink.Href = ResolveUrl(cssPath);
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");

            this.Page.Header.Controls.Add(cssLink);
        }

        /// <summary>
        /// Load js file cho gian hang (neu co)
        /// </summary>
        /// <param name="store"></param>
        private void LoadScripts(string template)
        {
            string skinPath = _SkinPath + template;
            string scriptPath = skinPath + "/scripts/script.js";
            if (System.IO.File.Exists(Server.MapPath(scriptPath)))
            {
                this.Page.ClientScript.RegisterClientScriptInclude("pid" + PageId.ToString(), ResolveUrl(scriptPath));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageBaseControl page = null;

            if (PageId > 0)
            {
                PortalPage pPage = db.PortalPages.SingleOrDefault<PortalPage>(p => p.Id == PageId);
                if (pPage != null)
                {
                    PageDef def = db.PageDefs.SingleOrDefault<PageDef>(pd => pd.Id == pPage.PageDef);
                    if (def != null)
                    {
                        page = (PageBaseControl)LoadControl(def.PageDefViewControl);
                    }
                    page.CurrentPage = pPage;
                }
            }
            else if (Request.QueryString["pn"] != null) 
            {
                PortalPage pPage = db.PortalPages.SingleOrDefault<PortalPage>(p => p.PageKey == Request.QueryString["pn"]);
                if (pPage != null)
                {
                    PageDef def = db.PageDefs.SingleOrDefault<PageDef>(pd => pd.Id == pPage.PageDef);
                    if (def != null)
                    {
                        page = (PageBaseControl)LoadControl(def.PageDefViewControl);
                    }
                    page.CurrentPage = pPage;
                }
            }

            if (page == null)
            {
                PortalPage pPage = db.PortalPages.SingleOrDefault<PortalPage>(p => p.IsHomepage == true);
                if (pPage != null)
                {
                    PageDef def = db.PageDefs.SingleOrDefault<PageDef>(pd => pd.Id == pPage.PageDef);
                    if (def != null)
                    {
                        page = (PageBaseControl)LoadControl(def.PageDefViewControl);
                    }
                    page.CurrentPage = pPage;
                }
            }

            this.site_wrapper.Controls.Add(page);
        }
    }
}