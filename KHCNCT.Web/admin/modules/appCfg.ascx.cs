using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Admin.Controls;
using KHCNCT.Data;
using NT.Lib;
using Telerik.Web.UI;
using KHCNCT.Globals;
using KHCNCT.Configuration;
using KHCNCT.Globals.Enums;
using System.Collections;
using System.Configuration;
namespace KHCNCT.Web.admin.modules
{
    public partial class appCfg : CommonBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void UpdateConfig()
        { 
            KHCNCTDataContext db = new KHCNCTDataContext(ConfigurationManager.ConnectionStrings["SoKHCNCT"].ConnectionString);
            AppConfig appCfg = db.AppConfigs.Where<AppConfig>(ap => ap.AppName == ConfigurationManager.AppSettings.GetValues("AppName")[0]).SingleOrDefault<AppConfig>();
            if (appCfg == null)
            {
                SystemConfig.InitializeConfig();
                appCfg = db.AppConfigs.Where<AppConfig>(ap => ap.AppName == ConfigurationManager.AppSettings.GetValues("AppName")[0]).SingleOrDefault<AppConfig>();
            }
            if (rntNewsOnDropMenu.Value.HasValue && rntNewsOnDropMenu.Value > 0) appCfg.DefaultNewsShowInDropMenu = (int)rntNewsOnDropMenu.Value.Value;
            if (rntNewsOnModule.Value.HasValue && rntNewsOnDropMenu.Value > 0) appCfg.DefaultNewsShowInModule = 5;
            if (rntNewsPerPage.Value.HasValue && rntNewsPerPage.Value > 0) appCfg.DefaultNewsShowOnPage = 15;

            if (rntNewsOnDropMenu.Value.HasValue && rntNewsOnDropMenu.Value > 0) appCfg.DefaultNewsImage = "~/images/no_image.png";
            if (rntNewsOnDropMenu.Value.HasValue && rntNewsOnDropMenu.Value > 0) appCfg.DefaultImage = "~/images/no_image.png";

            if (rntLevel2CateShowHomePage.Value.HasValue && rntLevel2CateShowHomePage.Value > 0) appCfg.Level2CateShowHomePage = 3;
            if (rntCateInDropMenu.Value.HasValue && rntCateInDropMenu.Value > 0) appCfg.MaxCategoryShowInDropMenu = 5;
            //if (rntNewsOnDropMenu.Value.HasValue && rntNewsOnDropMenu.Value > 0) appCfg.MaxWordLenthInTitle = 150;
            //if (rntNews_CategoryLevel.Value.HasValue && rntNews_CategoryLevel.Value > 0) appCfg.News_CategoryLevel = 2;

            if (rntMaxThumbNewsImgHeight.Value.HasValue && rntMaxThumbNewsImgHeight.Value > 0) appCfg.News_ImageThumbHeight = 100;
            if (rntMaxThumbNewsImgWidth.Value.HasValue && rntMaxThumbNewsImgWidth.Value > 0) appCfg.News_ImageThumbWidth = 120;

            if (rntMaxNewsImgHeight.Value.HasValue && rntMaxNewsImgHeight.Value > 0) appCfg.News_MaxImageHeight = 300;
            if (rntMaxNewsImgWidth.Value.HasValue && rntMaxNewsImgWidth.Value > 0) appCfg.News_MaxImageWidth = 350;

            if (rntFeatureNews.Value.HasValue && rntFeatureNews.Value > 0) appCfg.NumOfFeatureNews = 10;
            if (rntMostViewNews.Value.HasValue && rntMostViewNews.Value > 0) appCfg.NumOfMostViewNews = 10;
            if (rntRootCateShowHomepage.Value.HasValue && rntRootCateShowHomepage.Value > 0) appCfg.RootCateShowHomePage = 12;
            //if (rntNewsOnDropMenu.Value.HasValue && rntNewsOnDropMenu.Value > 0) appCfg.DefaultAdminUploadQouta = 10000;
            //if (rntNewsOnDropMenu.Value.HasValue && rntNewsOnDropMenu.Value > 0) appCfg.DefaultUserUploadQuota = 50000;
            if (rntMaxVideoShowHomepage.Value.HasValue && rntMaxVideoShowHomepage.Value > 0) appCfg.MaxVideoShowHomepage = 5;
            if (rntMaxVideoShowOnPage.Value.HasValue && rntMaxVideoShowOnPage.Value > 0) appCfg.MaxVideoShowOnPage = 10;
            
            db.SubmitChanges();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UpdateConfig();
            }
        }
    }
}