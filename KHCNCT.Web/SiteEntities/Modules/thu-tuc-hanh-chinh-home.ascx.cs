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
    public partial class thu_tuc_hanh_chinh_home : ModuleBaseControl
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
            List<ThuTucHanhChinh_ChiTiet> lstNews = (from nc in db.ThuTucHanhChinh_ChiTiets
                                         where nc.HieuLuc == true
                                         orderby nc.NgayTao.Value descending
                                                     select nc).Take(iNumOfNewsShowOnModule).ToList<ThuTucHanhChinh_ChiTiet>();
            rptNews.DataSource = lstNews;
            rptNews.DataBind();                        
        }
    }
}