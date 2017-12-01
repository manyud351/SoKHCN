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
    public partial class van_ban_phap_quy_home : ModuleBaseControl
    {
        protected int iCategoryId = 0;
        protected int iCaptionPageId = 0;
        protected int iDetailPageId = 0;
        protected int iNumOfNewsShowOnModule = 5;
        //public string sModKey;
        
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
            List<VBPQ_VanBanChiTiet> lstNews = (from nc in db.VBPQ_VanBanChiTiets
                                         where nc.DaDuyet == true
                                         orderby nc.NgayTao.Value descending
                                                select nc).Take(iNumOfNewsShowOnModule).ToList<VBPQ_VanBanChiTiet>();
            rptNews.DataSource = lstNews;
            rptNews.DataBind();                        
        }
    }
}