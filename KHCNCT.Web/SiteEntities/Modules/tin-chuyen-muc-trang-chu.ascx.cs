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
using KHCNCT.Configuration;

namespace KHCNCT.Web.SiteEntities.Modules
{
    public partial class tin_chuyen_muc_trang_chu : ModuleBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContents();
        }

        private void LoadContents()
        {
            List<NewsCategory> lstCate = (from nc in db.NewsCategories
                                          where nc.ShowInHomePage == true && nc.Disabled == false && nc.Hide == false && nc.Level == 1
                                          orderby nc.Order, nc.CategoryName
                                          select nc).ToList<NewsCategory>();
            rptCategory.DataSource = lstCate;
            rptCategory.DataBind();           
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

        protected void rptCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            NewsCategory nc = (NewsCategory)e.Item.DataItem;
            
            HyperLink hplCategoryName = (HyperLink)e.Item.FindControl("hplCategoryName");
            int PageId = (from p in db.PortalPages where p.CategoryId == nc.Id && p.Hidden == false orderby p.Order select p.Id).Take(1).SingleOrDefault<int>();
            hplCategoryName.NavigateUrl = Globals.Common.GenerateUrl(PageId);

            string creteria = GenerateCategorySearchCondition(nc.Id);
            creteria = " and (CategoryId = " + nc.Id.ToString() + " " + creteria + ") ";
            string strSQL = " DECLARE @NumRecordReturn int " +
                            " SET @NumRecordReturn=" + SystemConfig.ApplicationConfig.DefaultNewsShowInModule.ToString() +
                             " SELECT TOP (@NumRecordReturn) [Id], [NewsTitle], [NewsTitleUrl], [CategoryId], [NewsTypeId], [Description], [ImagePath], [Content], [Source], [OriginAuthor], [CreatedBy], [CreatedTime], [LastUpdatedTime], [LastSentTime], [ApprovementStatus], [ViewCount], [PublishFrom], [PublishTo], [Hidden], [ShowInNewest], [ShowInFeature], [ShowInMostView], [UserId], [OrinalId], [SyncDate], [ShowInDropdownMenu]" +
                             " FROM [NewsContent] where [ApprovementStatus] = " + ((short)EnumNewsApprovementStatus.DaXuatBan).ToString() + creteria +
                             " order by [PublishFrom] desc, [CreatedTime] desc"; ;

            List<NewsContent> lstNews = db.ExecuteQuery<NewsContent>(strSQL).ToList<NewsContent>();
            if (lstNews.Count > 1)
            {
                Image img = (Image)e.Item.FindControl("imgFirstNews");
                HyperLink ltr = (HyperLink)e.Item.FindControl("hplFirstNewsTitle");
                if (lstNews[0].ImagePath == "") img.ImageUrl = Configuration.SystemConfig.ApplicationConfig.DefaultImage;
                else img.ImageUrl = lstNews[0].ImagePath;
                ltr.Text = lstNews[0].NewsTitle;
                ltr.NavigateUrl = Common.GenerateUrl(57, "nid=" + lstNews[0].Id.ToString());
            }
            if (lstNews.Count > 2)
            {
                Repeater rptNews = (Repeater)e.Item.FindControl("rptNews");
                rptNews.DataSource = lstNews.GetRange(1, lstNews.Count - 1);
                rptNews.DataBind();
            }
        }
    }
}