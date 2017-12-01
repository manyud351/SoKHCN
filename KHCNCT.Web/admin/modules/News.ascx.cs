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

namespace KHCNCT.admin.modules
{
    public partial class admNews : CommonBaseControl
    {
        private int _newsid;
        protected int NewsId
        {
            get
            {
                if (_newsid == 0 && Request.QueryString["newsid"] != null) int.TryParse(Request.QueryString["newsid"], out _newsid);
                return _newsid;
            }
        }

        string virtualUploadFolder
        {
            get
            {
                return "~/upload/" + UserId.ToString() + "/news/";
            }
        }

        string ThumbFolder
        {
            get
            {
                return "thumbs";
            }
        }

        private List<CategoryAssignUser> _lstCateUsers = null;
        public List<CategoryAssignUser> ListCateAssignUsers
        {
            get
            {
                if (_lstCateUsers == null) _lstCateUsers = db.CategoryAssignUsers.Where<CategoryAssignUser>(cau => cau.UserId == UserId).ToList<CategoryAssignUser>();
                return _lstCateUsers;
            }
        }

        protected void ctvChiTiet_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (Editor1.MainEditor.Text.Length > 0);
        }


        protected void Page_Load(object sender, EventArgs e)
        {            
            CreateUserDirectoryForNews();
            //Session["CKBaseURL"] = Server.MapPath(virtualUploadFolder);
            //Editor1.MainEditor.ImageManager.UploadPaths = new string[] { virtualUploadFolder + "images"};
            //Editor1.MainEditor.ImageManager.DeletePaths = new string[] { virtualUploadFolder + "images"};
            //Editor1.MainEditor.ImageManager.ViewPaths = new string[] { virtualUploadFolder + "images" };

            //Editor1.MainEditor.DocumentManager.UploadPaths = new string[] { virtualUploadFolder + "docs" };
            //Editor1.MainEditor.DocumentManager.DeletePaths = new string[] { virtualUploadFolder + "docs" };
            //Editor1.MainEditor.DocumentManager.ViewPaths = new string[] { virtualUploadFolder + "docs" };

            if (!IsPostBack)
            {
                if (NewsId > 0)
                {
                    LoadCategories(0, null, rcbCategories);
                    //ShrinkCategoryCombobox(); 
                    ChangeMode(true);
                    LoadNewsContent(NewsId);
                }
                else
                {
                    ChangeMode(false);
                    LoadShowCategories(0, null, rcbCategoryShow);
                    //ShrinkShowCategoryCombobox();
                    rcbCategoryShow.Items.Insert(0, new RadComboBoxItem("Tất cả", ""));
                    //LoadNewsContent();
                }
            }
        }

        private void CreateUserDirectoryForNews()
        {
            if (!System.IO.Directory.Exists(Server.MapPath(virtualUploadFolder + "images")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(virtualUploadFolder + "images"));
            }

            if (!System.IO.Directory.Exists(Server.MapPath(virtualUploadFolder + "docs")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(virtualUploadFolder + "docs"));
            }
        }

        private void ShrinkShowCategoryCombobox()
        {
            for (int i = 0; i < rcbCategoryShow.Items.Count; i++)
            {
                if (rcbCategoryShow.Items[i].Font.Bold == true && ((i == (rcbCategoryShow.Items.Count - 1) || (rcbCategoryShow.Items[i + 1].Font.Bold == true))))
                {
                    rcbCategoryShow.Items.Remove(i);
                    i--;
                }
            }
        }

        private void ShrinkCategoryCombobox()
        {
            for (int i = 0; i < rcbCategories.Items.Count; i++)
            {
                if (rcbCategories.Items[i].Font.Bold == true && ((i == (rcbCategories.Items.Count - 1) || (rcbCategories.Items[i + 1].Font.Bold == true))))
                {
                    rcbCategories.Items.Remove(i);
                    i--;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ProcessNewsContent();
            }
        }

        private void ProcessNewsContent()
        {
            NewsContent st = null;
            bool IsInUpdateMode = false;
            if (NewsId > 0)
            {
                st = db.NewsContents.SingleOrDefault<NewsContent>(s => s.Id == NewsId && s.UserId == UserId);
            }

            if (st == null)
            {
                st = new NewsContent();
                st.ViewCount = 0;
                st.CreatedBy = UserInfo.UserAccount.AccountName;
            }
            else
            {
                //neu da duyet roi thi ko the cap nhat duoc nua
                if (st.ApprovementStatus >= (short)EnumNewsApprovementStatus.DaDuyetChuaXuatBan) Response.Redirect(Common.GenerateAdminUrl("news"));
                IsInUpdateMode = true;
            }

            PreventSQLInjection.ClearSQLInjectionInAllControls(this);
            st.NewsTitle = txtTieuDe.Text;
            st.Description = txtMoTa.Text;
            //st.Content = Editor1.MainEditor.Content;
            st.Content = Editor1.MainEditor.Text;
            st.LastUpdatedTime = DateTime.Now;
            st.CategoryId = Convert.ToInt32(rcbCategories.SelectedValue);
            //st.Hidden = !ckbShow.Checked;

            st.OriginAuthor = txtAuthor.Text;
            st.Source = txtSource.Text;

            //upload hinh dai dien cua ban tin
            if (fulHinhAnh.HasFile)
            {
                //kiem tra kieu va kich co file
                //if ((NT.Lib.Globals.IsImageFile(fulHinhAnh.FileName)) && (fulHinhAnh.FileContent.Length <= SystemConfig.ApplicationConfig.MaxUserImageSizeUpload))
                if (NT.Lib.Globals.IsImageFile(fulHinhAnh.FileName))
                {
                    //xoa anh cu neu co (truong hop cap nhat)
                    if (!String.IsNullOrEmpty(st.ImagePath))
                    {
                        System.IO.File.Delete(Server.MapPath(st.ImagePath));
                    }

                    string uploadFolder = Server.MapPath(virtualUploadFolder + ThumbFolder);
                    if (!System.IO.Directory.Exists(uploadFolder)) System.IO.Directory.CreateDirectory(uploadFolder);

                    //tao chuoi ten file ngau nhien
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Hash.GetRandomHashKey(4) + "." + NT.Lib.Globals.GetFileExtension(fulHinhAnh.FileName);

                    //resize anh
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fulHinhAnh.PostedFile.InputStream);
                    System.Drawing.Image imgResized = Utilities.ResizeImage(img, SystemConfig.ApplicationConfig.News_MaxImageWidth, SystemConfig.ApplicationConfig.News_MaxImageWidth);
                    imgResized.Save(uploadFolder + fileName);//luu file vao o cung
                    
                    //luu duong dan anh vao db
                    st.ImagePath = virtualUploadFolder + ThumbFolder + fileName;
                }
            }
            
            if (!ckbSend.Checked)
            {
                st.ApprovementStatus = (short)EnumNewsApprovementStatus.ChuaGui;
            }
            else st.ApprovementStatus = (short)EnumNewsApprovementStatus.ChuaDuyet;

            //auto publish news
            //st.ApprovementStatus = (short)EnumNewsApprovementStatus.DaXuatBan;
            //st.ShowInDropdownMenu = true;
            //st.ShowInFeature = ckbFeature.Checked;
            //st.ShowInMostView = true;
            //st.ShowInNewest = ckbNewest.Checked;
            //st.Hidden = false;
            //st.PublishFrom = DateTime.Now;
            //st.PublishTo = DateTime.MaxValue;

            if (!IsInUpdateMode)
            {
                //st.StoreId = StoreId;
                st.CreatedTime = st.LastUpdatedTime;
                st.UserId = UserId;
                db.NewsContents.InsertOnSubmit(st);
            }

            db.SubmitChanges();

            //NT.Lib.Utilities.ClearAllInputControls(pnlAddNew.Controls);

            //ChangeMode(false);
            Response.Redirect(Common.GenerateAdminUrl("news"));
            //LoadNewsContent();
        }

        /// <summary>
        /// Load danh muc vao cay danh muc
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="parentNode"></param>
        /// <param name="rtvDestination"></param>
        private void LoadCategories(int parentId, RadComboBoxItem parentNode, RadComboBox rtvDestination)
        {
            List<NewsCategory> lstCM = null;
            if (parentId == 0)
            {
                lstCM = (from cm in db.NewsCategories
                         //where cm.Level == 1 && cm.Hide != true
                         where cm.Level == 1
                         orderby cm.Order
                         select cm).ToList<NewsCategory>();
            }
            else
            {
                //nguoc lai load cac chuyen muc co chuyen muc cha la 'parentId'
                lstCM = (from cm in db.NewsCategories
                         //where cm.ParentId == parentId && cm.Hide != true
                         where cm.ParentId == parentId
                         orderby cm.Order
                         select cm).ToList<NewsCategory>();
            }

            for (int i = 0; i < lstCM.Count; i++)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = lstCM[i].CategoryName;
                item.Value = lstCM[i].Id.ToString();
                //item.Enabled = true;

                //prarentNode = null -> Root Category
                string prefix = "";
                if (parentNode == null)
                {
                    //item.Enabled = false;
                }
                else
                {
                    for (int ii = 0; ii < lstCM[i].Level; ii++) prefix += "&nbsp;";
                }
                item.Text = Server.HtmlDecode(prefix + "- " + item.Text);

                //if (lstCM[i].Level == SystemConfig.ApplicationConfig.News_CategoryLevel)
                //{
                //    if (ListCateAssignUsers.Find(x => x.CategoryId == lstCM[i].Id) != null)
                //    {
                //        //item.Enabled = true;
                //        rtvDestination.Items.Add(item);
                //    }
                //}
                //else if (lstCM[i].Level < SystemConfig.ApplicationConfig.News_CategoryLevel)
                //{
                //    item.Font.Bold = true;
                //    rtvDestination.Items.Add(item);
                //    LoadCategories(lstCM[i].Id, item, rtvDestination);
                //    //item.Enabled = false;
                //}         

                if (ListCateAssignUsers.Find(x => x.CategoryId == lstCM[i].Id) != null)
                {
                    if (lstCM[i].Level == 1) item.Font.Bold = true;
                    rtvDestination.Items.Add(item);
                    LoadCategories(lstCM[i].Id, item, rtvDestination);
                }
            }
        }

        private void LoadShowCategories(int parentId, RadComboBoxItem parentNode, RadComboBox rtvDestination)
        {
            List<NewsCategory> lstCM = null;
            if (parentId == 0)
            {
                lstCM = (from cm in db.NewsCategories
                         //where cm.Level == 1 && cm.Hide != true
                         where cm.Level == 1
                         orderby cm.Order
                         select cm).ToList<NewsCategory>();
            }
            else
            {
                //nguoc lai load cac chuyen muc co chuyen muc cha la 'parentId'
                lstCM = (from cm in db.NewsCategories
                         //where cm.ParentId == parentId && cm.Hide != true
                         where cm.ParentId == parentId
                         orderby cm.Order
                         select cm).ToList<NewsCategory>();
            }
            
            for (int i = 0; i < lstCM.Count; i++)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = lstCM[i].CategoryName;
                item.Value = lstCM[i].Id.ToString();
                //item.Enabled = true;

                //prarentNode = null -> Root Category
                string prefix = "";
                if (parentNode == null)
                {
                    //item.Enabled = false;
                }
                else
                {
                    for (int ii = 0; ii < lstCM[i].Level; ii++) prefix += "&nbsp;";
                }
                item.Text = Server.HtmlDecode(prefix + "- " + item.Text);
                
                //if (lstCM[i].Level == SystemConfig.ApplicationConfig.News_CategoryLevel)
                //{
                //    if (ListCateAssignUsers.Find(x => x.CategoryId == lstCM[i].Id) != null)
                //    {
                //        item.Font.Bold = false;
                //        rtvDestination.Items.Add(item);
                //    }
                //}
                //else if (lstCM[i].Level < SystemConfig.ApplicationConfig.News_CategoryLevel)
                //{
                //    rtvDestination.Items.Add(item);
                //    LoadShowCategories(lstCM[i].Id, item, rtvDestination);
                //    item.Font.Bold = true;
                //}

                if (ListCateAssignUsers.Find(x => x.CategoryId == lstCM[i].Id) != null)
                {
                    if (lstCM[i].Level == 1) item.Font.Bold = true;
                    rtvDestination.Items.Add(item);
                    LoadShowCategories(lstCM[i].Id, item, rtvDestination);
                }
            }
        }
        
        protected void LoadNewsContent(long id)
        {
            NewsContent news = db.NewsContents.SingleOrDefault<NewsContent>(sa => sa.Id == id);
            if (news != null)
            {
                txtTieuDe.Text = news.NewsTitle;
                txtMoTa.Text = news.Description;
                //Editor1.MainEditor.Content = news.Content;
                Editor1.MainEditor.Text = news.Content;
                ckbSend.Checked = (news.ApprovementStatus > 0);
                ckbSend.Enabled = false;
                if (String.IsNullOrEmpty(news.ImagePath)) divHinhDaiDien.Visible = false;
                else
                {
                    divHinhDaiDien.Visible = true;
                    imhHinhDaiDien.ImageUrl = news.ImagePath;
                }
                txtAuthor.Text = news.OriginAuthor;
                txtSource.Text = news.Source;
                RadComboBoxItem rcbItem = rcbCategories.Items.FindItemByValue(news.CategoryId.Value.ToString());
                if (rcbItem != null) rcbItem.Selected = true;
            }
        }

        protected void LoadNewsContent()
        {
            IList lstSTA;
            if (rcbCategoryShow.SelectedIndex == 0)
            {
                if(rcbApproved.SelectedIndex == 0)
                {
                    lstSTA = (from s in db.NewsContents
                              where s.UserId == UserId
                              orderby s.LastUpdatedTime descending, s.CreatedTime descending
                              select new
                              {
                                  s.ApprovementStatus,
                                  s.CategoryId,
                                  s.Content,
                                  s.CreatedBy,
                                  s.CreatedTime,
                                  s.Description,
                                  s.Hidden,
                                  s.Id,
                                  s.ImagePath,
                                  s.LastSentTime,
                                  s.LastUpdatedTime,
                                  s.NewsApprovementStatus,
                                  s.NewsCategory,
                                  s.NewsFiles,
                                  s.NewsTitle,
                                  s.NewsTitleUrl,
                                  s.NewsTypeId,
                                  s.OriginAuthor,
                                  s.OrinalId,
                                  s.PublishFrom,
                                  s.PublishTo,
                                  s.ShowInFeature,
                                  s.ShowInDropdownMenu,
                                  s.ShowInMostView,
                                  s.ShowInNewest,
                                  s.Source,
                                  s.SyncDate,
                                  s.UserId,
                                  s.ViewCount,
                                  CategoryName = s.NewsCategory.CategoryName,
                                  Approvement = (from na in db.NewsApprovementStatus where na.Id == s.ApprovementStatus select na.Description).SingleOrDefault()
                              }).Take(2000).ToList();
                }
                else
                {
                    lstSTA = (from s in db.NewsContents
                              where s.UserId == UserId && s.ApprovementStatus == Convert.ToInt16(rcbApproved.SelectedValue)
                              orderby s.LastUpdatedTime descending, s.CreatedTime descending
                              select new
                              {
                                  s.ApprovementStatus,
                                  s.CategoryId,
                                  s.Content,
                                  s.CreatedBy,
                                  s.CreatedTime,
                                  s.Description,
                                  s.Hidden,
                                  s.Id,
                                  s.ImagePath,
                                  s.LastSentTime,
                                  s.LastUpdatedTime,
                                  s.NewsApprovementStatus,
                                  s.NewsCategory,
                                  s.NewsFiles,
                                  s.NewsTitle,
                                  s.NewsTitleUrl,
                                  s.NewsTypeId,
                                  s.OriginAuthor,
                                  s.OrinalId,
                                  s.PublishFrom,
                                  s.PublishTo,
                                  s.ShowInFeature,
                                  s.ShowInDropdownMenu,
                                  s.ShowInMostView,
                                  s.ShowInNewest,
                                  s.Source,
                                  s.SyncDate,
                                  s.UserId,
                                  s.ViewCount,
                                  CategoryName = s.NewsCategory.CategoryName,
                                  Approvement = (from na in db.NewsApprovementStatus where na.Id == s.ApprovementStatus select na.Description).SingleOrDefault()
                              }).Take(2000).ToList();
                }
            }
            else
            {
                if (rcbApproved.SelectedIndex == 0)
                {
                    lstSTA = (from s in db.NewsContents
                              where (s.CategoryId == Convert.ToInt32(rcbCategoryShow.SelectedValue) || s.NewsCategory.ParentId == Convert.ToInt32(rcbCategoryShow.SelectedValue)) && s.UserId == UserId
                              orderby s.LastUpdatedTime descending, s.CreatedTime descending
                              select new
                              {
                                  s.ApprovementStatus,
                                  s.CategoryId,
                                  s.Content,
                                  s.CreatedBy,
                                  s.CreatedTime,
                                  s.Description,
                                  s.Hidden,
                                  s.Id,
                                  s.ImagePath,
                                  s.LastSentTime,
                                  s.LastUpdatedTime,
                                  s.NewsApprovementStatus,
                                  s.NewsCategory,
                                  s.NewsFiles,
                                  s.NewsTitle,
                                  s.NewsTitleUrl,
                                  s.NewsTypeId,
                                  s.OriginAuthor,
                                  s.OrinalId,
                                  s.PublishFrom,
                                  s.PublishTo,
                                  s.ShowInFeature,
                                  s.ShowInDropdownMenu,
                                  s.ShowInMostView,
                                  s.ShowInNewest,
                                  s.Source,
                                  s.SyncDate,
                                  s.UserId,
                                  s.ViewCount,
                                  CategoryName = s.NewsCategory.CategoryName,
                                  Approvement = (from na in db.NewsApprovementStatus where na.Id == s.ApprovementStatus select na.Description).SingleOrDefault()
                              }).Take(2000).ToList();
                }
                else
                {
                    lstSTA = (from s in db.NewsContents
                              where (s.CategoryId == Convert.ToInt32(rcbCategoryShow.SelectedValue) || s.NewsCategory.ParentId == Convert.ToInt32(rcbCategoryShow.SelectedValue)) && s.UserId == UserId
                                    && s.ApprovementStatus == Convert.ToInt16(rcbApproved.SelectedValue)
                              orderby s.LastUpdatedTime descending, s.CreatedTime descending
                              select new
                              {
                                  s.ApprovementStatus,
                                  s.CategoryId,
                                  s.Content,
                                  s.CreatedBy,
                                  s.CreatedTime,
                                  s.Description,
                                  s.Hidden,
                                  s.Id,
                                  s.ImagePath,
                                  s.LastSentTime,
                                  s.LastUpdatedTime,
                                  s.NewsApprovementStatus,
                                  s.NewsCategory,
                                  s.NewsFiles,
                                  s.NewsTitle,
                                  s.NewsTitleUrl,
                                  s.NewsTypeId,
                                  s.OriginAuthor,
                                  s.OrinalId,
                                  s.PublishFrom,
                                  s.PublishTo,
                                  s.ShowInFeature,
                                  s.ShowInDropdownMenu,
                                  s.ShowInMostView,
                                  s.ShowInNewest,
                                  s.Source,
                                  s.SyncDate,
                                  s.UserId,
                                  s.ViewCount,
                                  CategoryName = s.NewsCategory.CategoryName,
                                  Approvement = (from na in db.NewsApprovementStatus where na.Id == s.ApprovementStatus select na.Description).SingleOrDefault()
                              }).Take(2000).ToList();
                }
            }
            rgNews.DataSource = lstSTA;
            rgNews.DataBind();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            LoadCategories(0, null, rcbCategories);
            //ShrinkCategoryCombobox(); 
            ChangeMode(true);
            divHinhDaiDien.Visible = false;
        }

        protected void ChangeMode(bool IsAddNewOrUpdateMode)
        {
            pnlAddNew.Visible = IsAddNewOrUpdateMode;
            pnlManage.Visible = !IsAddNewOrUpdateMode;
        }

        protected void rgNews_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            String id = dataItem.GetDataKeyValue("Id").ToString();
            NewsContent news = db.NewsContents.SingleOrDefault<NewsContent>(sm => sm.Id == Convert.ToInt32(id));
            if (news != null)
            {
                db.NewsFiles.DeleteAllOnSubmit<NewsFile>(from nf in db.NewsFiles where nf.NewsId == news.Id select nf);
                if (!String.IsNullOrEmpty(news.ImagePath))
                {
                    System.IO.File.Delete(Server.MapPath(news.ImagePath));
                }
                db.NewsContents.DeleteOnSubmit(news);
                db.SubmitChanges();
                LoadNewsContent();
            }
        }

        protected void rgNews_EditCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            String id = dataItem.GetDataKeyValue("Id").ToString();
            Response.Redirect(Common.GenerateAdminUrl("news", "newsid=" + id));
            //_newsid = Convert.ToInt32(id);

            //if (rcbCategories.Items.Count == 0)
            //{
            //    LoadCategories(0, null, rcbCategories);
            //    //ShrinkCategoryCombobox(); 
            //}
            //ChangeMode(true);
            //LoadNewsContent(NewsId);
            //rgNews.EditIndexes.Clear();
        }

        protected void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadNewsContent();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("news"));
            //ChangeMode(false);
            //LoadNewsContent();
        }


        protected void rgNews_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item != null && (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                int id = Convert.ToInt32(dataItem.GetDataKeyValue("Id"));
                NewsContent st = db.NewsContents.SingleOrDefault<NewsContent>(s => s.Id == id);
                if (e.CommandName == "Send" && st != null && st.ApprovementStatus <= (short)EnumNewsApprovementStatus.ChuaGui)
                {
                    st.ApprovementStatus = (short)EnumNewsApprovementStatus.ChuaDuyet;
                    db.SubmitChanges();
                    LoadNewsContent();
                }
            }
        }

        protected void rgNews_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.AlternatingItem) || (e.Item.ItemType == GridItemType.Item))
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                int id = Convert.ToInt32(dataItem.GetDataKeyValue("Id"));
                NewsContent st = db.NewsContents.SingleOrDefault<NewsContent>(s => s.Id == id);
                if (st != null)
                {
                    if (st.ApprovementStatus > (short)EnumNewsApprovementStatus.ChuaGui)
                        e.Item.FindControl("imgSend").Visible = false;
                }
            }
        }

        protected void rgNews_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            rgNews.CurrentPageIndex = e.NewPageIndex;
            LoadNewsContent();
        }

        protected void rgNews_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            rgNews.PageSize = e.NewPageSize;
            rgNews.CurrentPageIndex = 1;
            e.Canceled = true;
            LoadNewsContent();
        }
    }
}