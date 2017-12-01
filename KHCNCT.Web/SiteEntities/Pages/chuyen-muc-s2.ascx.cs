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

namespace KHCNCT.Web.SiteEntities.Pages
{
    public partial class chuyen_muc_s2 : PageBaseControl
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

        private int _OriginId;
        protected int OriginId
        {
            get
            {
                if ((_OriginId == 0) && (Request.QueryString["oid"] != null))
                {
                    int.TryParse(Request.QueryString["oid"], out _OriginId);
                }
                return _OriginId;
            }
        }

        private int _CategoryId;
        protected int CategoryId
        {
            get
            {
                if ((_CategoryId == 0) && (Request.QueryString["cid"] != null))
                {
                    int.TryParse(Request.QueryString["cid"], out _CategoryId);
                }
                return _CategoryId;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadNewsByCategory();
        }

        /// <summary>
        /// tao chuoi sql tim kiem san pham cho mot danh muc, neu danh muc cha duoc chon thi cac san pham thuoc danh muc con cua
        /// no cug duoc chon
        /// </summary>
        /// <param name="parentId">danh muc cha</param>
        /// <returns></returns>
        public string GenerateCategorySearchCondition(int parentId)
        {
            List<NewsCategory> lstCM;
            string catemenu = "";

            if (parentId == 0) //neu danh muc cha = 0 --> load tat ca danh muc cap 1
            {
                lstCM = (from cm in db.NewsCategories
                         where cm.Level == 1
                         orderby cm.Order
                         select cm).ToList<NewsCategory>();
            }
            else
            {
                //nguoc lai load cac chuyen muc co chuyen muc cha la 'parentId'
                lstCM = (from cm in db.NewsCategories
                         where cm.ParentId == parentId
                         orderby cm.Order
                         select cm).ToList<NewsCategory>();
            }

            if (lstCM.Count > 0)
            {
                for (int i = 0; i < lstCM.Count; i++)
                {
                    catemenu += " or CategoryId = " + lstCM[i].Id.ToString(); //chuoi sql dang: or CategoryId = 1 or CategoryId = 2 ...
                    catemenu += GenerateCategorySearchCondition(lstCM[i].Id);
                }
            }
            return catemenu;
        }

        private void LoadNewsByCategory()
        {
            if (CategoryId > 0)
            {
                int skipRecord = (PageIndex - 1) * Configuration.SystemConfig.ApplicationConfig.DefaultNewsShowOnPage;
                int takeRecord = Configuration.SystemConfig.ApplicationConfig.DefaultNewsShowOnPage;
                //bo ra 5 tin load o dau trang
                if (PageIndex == 1)
                {
                    takeRecord += 5;
                }
                else
                {
                    divContentFirst.Visible = false;
                    skipRecord += 5;
                }

                string strSelect = "select n.* from NewsContent n inner join NewsCategory c on n.CategoryId = c.Id  ";
                string strCount = "select count(n.Id) from NewsContent n inner join NewsCategory c on n.CategoryId = c.Id  ";
                string strCreteria = " where n.ApprovementStatus = " + ((short)EnumNewsApprovementStatus.DaXuatBan).ToString() + " and Hidden = 0 " +
                                " and (CategoryId = " + CategoryId.ToString() + GenerateCategorySearchCondition(CategoryId) + ") ";
                string strOrder =  " order by PublishFrom desc, CreatedTime desc, LastUpdatedTime desc ";

                int totalRecord = db.ExecuteQuery<Int32>(strCount + strCreteria).SingleOrDefault<Int32>();

                List<NewsContent> lstNews = db.ExecuteQuery<NewsContent>(strSelect + strCreteria + strOrder).Skip(skipRecord).Take(takeRecord).ToList<NewsContent>();
                //update default images
                for (int i = 0; i < lstNews.Count; i++)
                {
                    if (lstNews[i].ImagePath == "") lstNews[i].ImagePath = Configuration.SystemConfig.ApplicationConfig.DefaultImage;
                }

                if (lstNews.Count > 1)
                {
                    if (PageIndex == 1)
                    {
                        if (lstNews[0].ImagePath != "") imgFirstNews.ImageUrl = lstNews[0].ImagePath;
                        else imgFirstNews.ImageUrl = Configuration.SystemConfig.ApplicationConfig.DefaultImage;

                        hplFirstNewsTitle.Text = lstNews[0].NewsTitle;
                        hplFirstNewsTitle.NavigateUrl = Globals.Common.GenerateUrl(57, "nid=" + lstNews[0].Id.ToString());

                        lstNews.RemoveAt(0);

                        int getRange = 4;
                        if (lstNews.Count < 4)
                        {
                            getRange = lstNews.Count;
                        }

                        rptNextTopNews.DataSource = lstNews.GetRange(0, getRange);
                        rptNextTopNews.DataBind();

                        lstNews.RemoveRange(0, getRange);
                    }

                    rptNextNews.DataSource = lstNews;
                    rptNextNews.DataBind();
                }

                ltrPaging.Text = Utilities.GenratePagingString(totalRecord, Configuration.SystemConfig.ApplicationConfig.DefaultNewsShowOnPage, PageIndex, 20, "<span class='pagingTitle'>Trang: </span>", "<span class='selected-page'>{0}</span>", "<span class='unselected-page'>{0}</span>", Globals.Common.GenerateUrl(PageId, "cid=" + CategoryId.ToString(), "p={0}"));
                
            }
        }
    }
}