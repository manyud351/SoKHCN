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
    public partial class admPublishNews : CommonBaseControl
    {
        protected int NewsId
        {
            get
            {
                int id = -1;
                if (Request.QueryString["newsid"] != null) int.TryParse(Request.QueryString["newsid"], out id);
                return id;
            }
        }

        string virtualUploadFolder
        {
            get
            {
                return "~/upload/news/";
            }
        }

        string ThumbFolder
        {
            get
            {
                return "thumbs";
            }
        }

        protected void ctvChiTiet_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (Editor1.MainEditor.Text.Length > 0);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Editor1.MainEditor.ImageManager.UploadPaths = new string[] { virtualUploadFolder };
            //Editor1.MainEditor.ImageManager.DeletePaths = new string[] { virtualUploadFolder };
            //Editor1.MainEditor.ImageManager.ViewPaths = new string[] { virtualUploadFolder };

            if (!IsPostBack)
            {
                if (NewsId > 0)
                {
                    LoadCategories(0, null, rcbCategories);
                    ChangeMode(true);
                    LoadNewsContent(NewsId);
                }
                else
                {
                    ChangeMode(false);
                    LoadShowCategories(0, null, rcbCategoryShow);
                    rcbCategoryShow.Items.Insert(0, new RadComboBoxItem("Tất cả", ""));
                    //LoadNewsContent();
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
            
            if (NewsId > 0)
            {
                st = db.NewsContents.SingleOrDefault<NewsContent>(s => s.Id == NewsId && (s.ApprovementStatus == (short)EnumNewsApprovementStatus.DaDuyetChuaXuatBan
                                                                                            || s.ApprovementStatus == (short)EnumNewsApprovementStatus.ThuHoiTinDaXuatBan));
            }

            if (st == null)
            {
                return;
            }
           
            PreventSQLInjection.ClearSQLInjectionInAllControls(this);
            st.NewsTitle = txtTieuDe.Text;
            st.Description = txtMoTa.Text;
            //st.Content = Editor1.MainEditor.Content;
            st.Content = Editor1.MainEditor.Text;
            st.LastUpdatedTime = DateTime.Now;
            st.CategoryId = Convert.ToInt32(rcbCategories.SelectedValue);
            st.Source = txtSource.Text;
            st.OriginAuthor = txtAuthor.Text;
            //st.Hidden = !ckbShow.Checked;

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

            NewsApprovementHistory apHis = new NewsApprovementHistory();
            apHis.NewsId = st.Id;

            if (ddlApprove.SelectedValue == "1")
            {
                st.ApprovementStatus = (short)EnumNewsApprovementStatus.DaXuatBan;
                apHis.IsApproved = true;
            }
            else
            {
                st.ApprovementStatus = (short)EnumNewsApprovementStatus.KhongXuatBan;
                apHis.IsApproved = false;
            }
            
            apHis.AppStatustId = st.ApprovementStatus;
            apHis.ApprovedBy = UserInfo.UserAccount.AccountName;
            apHis.Comment = txtApprovementDetail.Text;
            apHis.CreatedTime = DateTime.Now;
            NewsApprovementHistory nah = db.NewsApprovementHistories.OrderByDescending<NewsApprovementHistory, DateTime>(nn => nn.CreatedTime.Value).FirstOrDefault<NewsApprovementHistory>(nnn => nnn.NewsId == st.Id);
            //Int16 lastSeq = (from na in db.NewsApprovementHistories where na.NewsId == st.Id orderby na.CreatedTime descending select na.Sequence.Value).Take(1).SingleOrDefault<Int16>();
            short lastSeq = 0;
            if (nah != null) lastSeq = nah.Sequence.Value;
            apHis.Sequence = (short)(lastSeq + 1);

            st.ShowInDropdownMenu = true;
            st.ShowInFeature = ckbFeature.Checked;
            st.ShowInMostView = ckbHot.Checked;
            st.ShowInNewest = ckbNewest.Checked;
            st.Hidden = false;
            st.PublishFrom = DateTime.Now;
            st.PublishTo = DateTime.MaxValue;


            db.NewsApprovementHistories.InsertOnSubmit(apHis);

            db.SubmitChanges();

            Response.Redirect(Common.GenerateAdminUrl("publishnews"));
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
                    for (int ii = 0; ii < lstCM[i].Level; ii++) prefix += " ";
                }
                item.Text = prefix + "- " + item.Text;

                if (lstCM[i].Level >= SystemConfig.ApplicationConfig.News_CategoryLevel) item.Enabled = true;
                else item.Enabled = false;

                rtvDestination.Items.Add(item);

                LoadCategories(lstCM[i].Id, item, rtvDestination);
            }
        }

        private void LoadShowCategories(int parentId, RadComboBoxItem parentNode, RadComboBox rtvDestination)
        {
            List<NewsCategory> lstCM = null;
            if (parentId == 0)
            {
                lstCM = (from cm in db.NewsCategories
                         where cm.Level == 1 && cm.Hide != true
                         orderby cm.Order
                         select cm).ToList<NewsCategory>();
            }
            else
            {
                //nguoc lai load cac chuyen muc co chuyen muc cha la 'parentId'
                lstCM = (from cm in db.NewsCategories
                         where cm.ParentId == parentId && cm.Hide != true
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
                    for (int ii = 0; ii < lstCM[i].Level; ii++) prefix += " ";
                }
                item.Text = prefix + "- " + item.Text;

                if (lstCM[i].Level >= SystemConfig.ApplicationConfig.News_CategoryLevel) item.Font.Bold = false;
                else item.Font.Bold = true;

                rtvDestination.Items.Add(item);

                LoadShowCategories(lstCM[i].Id, item, rtvDestination);
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
                txtAuthor.Text = news.OriginAuthor;
                txtSource.Text = news.Source;

                RadComboBoxItem rcbItem = rcbCategories.Items.FindItemByValue(news.CategoryId.Value.ToString());
                if (rcbItem != null) rcbItem.Selected = true;
                
                if (String.IsNullOrEmpty(news.ImagePath)) divHinhDaiDien.Visible = false;
                else
                {
                    divHinhDaiDien.Visible = true;
                    imhHinhDaiDien.ImageUrl = news.ImagePath;
                }

                if (news.ApprovementStatus == (short)EnumNewsApprovementStatus.DaDuyetChuaXuatBan || news.ApprovementStatus == (short)EnumNewsApprovementStatus.ThuHoiTinDaXuatBan)
                {
                    txtTieuDe.ReadOnly = false;
                    txtMoTa.ReadOnly =false;
                    Editor1.MainEditor.Visible = true;
                    lblChitiet.Text = "";
                    txtAuthor.ReadOnly = false;
                    txtSource.ReadOnly = false;
                    btnSave.Visible = true;
                    trPublish1.Visible = trPublish2.Visible = trPublish3.Visible = true;
                }
                else
                {
                    txtTieuDe.ReadOnly = true;
                    txtMoTa.ReadOnly = true;
                    Editor1.MainEditor.Visible = false;
                    lblChitiet.Text = news.Content;
                    txtAuthor.ReadOnly = true;
                    txtSource.ReadOnly = true;
                    btnSave.Visible = false;
                    trPublish1.Visible = trPublish2.Visible = trPublish3.Visible = false;
                }
            }
        }

        protected void LoadNewsContent()
        {
            IList lstSTA;
            if (rcbCategoryShow.SelectedIndex == 0)
            {
                if (rcbApproved.SelectedIndex == 0)
                {
                    lstSTA = (from s in db.NewsContents
                              where (s.ApprovementStatus == (short)EnumNewsApprovementStatus.DaDuyetChuaXuatBan || s.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan
                                        || s.ApprovementStatus == (short)EnumNewsApprovementStatus.ThuHoiTinDaXuatBan)
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
                                  AuthorFullname = (from su in db.SysUsers where su.Id == s.UserId select su.DisplayName).SingleOrDefault(),
                                  AccountName = (from su in db.UserAccounts where su.UserId == s.UserId select su.AccountName).SingleOrDefault(),
                                  s.ViewCount,
                                  CategoryName = s.NewsCategory.CategoryName,
                                  Approvement = (from na in db.NewsApprovementStatus where na.Id == s.ApprovementStatus select na.Description).SingleOrDefault(),
                                  ApprovedBy = (from ah in db.NewsApprovementHistories where ah.NewsId == s.Id orderby ah.CreatedTime descending select ah.ApprovedBy).Take(1).SingleOrDefault(),
                                  ApprovedTime = (from ah in db.NewsApprovementHistories where ah.NewsId == s.Id orderby ah.CreatedTime descending select ah.CreatedTime).Take(1).SingleOrDefault()
                              }).Take(1000).ToList();
                }
                else
                {
                    lstSTA = (from s in db.NewsContents
                              where (s.ApprovementStatus == Convert.ToInt16(rcbApproved.SelectedValue))
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
                                  AuthorFullname = (from su in db.SysUsers where su.Id == s.UserId select su.DisplayName).SingleOrDefault(),
                                  AccountName = (from su in db.UserAccounts where su.UserId == s.UserId select su.AccountName).SingleOrDefault(),
                                  s.ViewCount,
                                  CategoryName = s.NewsCategory.CategoryName,
                                  Approvement = (from na in db.NewsApprovementStatus where na.Id == s.ApprovementStatus select na.Description).SingleOrDefault(),
                                  ApprovedBy = (from ah in db.NewsApprovementHistories where ah.NewsId == s.Id orderby ah.CreatedTime descending select ah.ApprovedBy).Take(1).SingleOrDefault(),
                                  ApprovedTime = (from ah in db.NewsApprovementHistories where ah.NewsId == s.Id orderby ah.CreatedTime descending select ah.CreatedTime).Take(1).SingleOrDefault()
                              }).Take(1000).ToList();
                }
            }
            else
            {
                if (rcbApproved.SelectedIndex == 0)
                {
                    lstSTA = (from s in db.NewsContents
                              where (s.CategoryId == Convert.ToInt32(rcbCategoryShow.SelectedValue) || s.NewsCategory.ParentId == Convert.ToInt32(rcbCategoryShow.SelectedValue))
                                    && (s.ApprovementStatus == (short)EnumNewsApprovementStatus.DaDuyetChuaXuatBan || s.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan
                                        || s.ApprovementStatus == (short)EnumNewsApprovementStatus.ThuHoiTinDaXuatBan)
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
                                  AuthorFullname = (from su in db.SysUsers where su.Id == s.UserId select su.DisplayName).SingleOrDefault(),
                                  AccountName = (from su in db.UserAccounts where su.UserId == s.UserId select su.AccountName).SingleOrDefault(),
                                  s.ViewCount,
                                  CategoryName = s.NewsCategory.CategoryName,
                                  Approvement = (from na in db.NewsApprovementStatus where na.Id == s.ApprovementStatus select na.Description).SingleOrDefault(),
                                  ApprovedBy = (from ah in db.NewsApprovementHistories where ah.NewsId == s.Id orderby ah.CreatedTime descending select ah.ApprovedBy).Take(1).SingleOrDefault(),
                                  ApprovedTime = (from ah in db.NewsApprovementHistories where ah.NewsId == s.Id orderby ah.CreatedTime descending select ah.CreatedTime).Take(1).SingleOrDefault()
                              }).Take(1000).ToList();
                }
                else
                {
                    lstSTA = (from s in db.NewsContents
                              where (s.ApprovementStatus == Convert.ToInt16(rcbApproved.SelectedValue))
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
                                  AuthorFullname = (from su in db.SysUsers where su.Id == s.UserId select su.DisplayName).SingleOrDefault(),
                                  AccountName = (from su in db.UserAccounts where su.UserId == s.UserId select su.AccountName).SingleOrDefault(),
                                  s.ViewCount,
                                  CategoryName = s.NewsCategory.CategoryName,
                                  Approvement = (from na in db.NewsApprovementStatus where na.Id == s.ApprovementStatus select na.Description).SingleOrDefault(),
                                  ApprovedBy = (from ah in db.NewsApprovementHistories where ah.NewsId == s.Id orderby ah.CreatedTime descending select ah.ApprovedBy).Take(1).SingleOrDefault(),
                                  ApprovedTime = (from ah in db.NewsApprovementHistories where ah.NewsId == s.Id orderby ah.CreatedTime descending select ah.CreatedTime).Take(1).SingleOrDefault()
                              }).Take(1000).ToList();
                }
            }
            rgNews.DataSource = lstSTA;
            rgNews.DataBind();
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
            Response.Redirect(Common.GenerateAdminUrl("publishnews", "newsid=" + id));
        }

        protected void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadNewsContent();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("publishnews"));
        }


        protected void rgNews_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item != null && (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                int id = Convert.ToInt32(dataItem.GetDataKeyValue("Id"));
                NewsContent st = db.NewsContents.SingleOrDefault<NewsContent>(s => s.Id == id);
                if (e.CommandName == "Send" && st != null && st.ApprovementStatus <= (short)EnumNewsApprovementStatus.DaDuyetChuaXuatBan)
                {
                    st.ApprovementStatus = (short)EnumNewsApprovementStatus.DaDuyetChuaXuatBan;
                    db.SubmitChanges();
                    LoadNewsContent();
                }
                else if (e.CommandName == "WithDraw" && st != null && st.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan)
                {
                    st.ApprovementStatus = (short)EnumNewsApprovementStatus.ThuHoiTinDaXuatBan;
                    
                    NewsApprovementHistory newHis = new NewsApprovementHistory();
                    newHis.ApprovedBy = UserInfo.UserAccount.AccountName;
                    newHis.CreatedTime = DateTime.Now;
                    newHis.NewsId = st.Id;
                    newHis.Comment = "Thu hồi tin đã xuất bản";
                    newHis.AppStatustId = (short)EnumNewsApprovementStatus.ThuHoiTinDaXuatBan;

                    NewsApprovementHistory nah = db.NewsApprovementHistories.OrderByDescending<NewsApprovementHistory, DateTime>(nn => nn.CreatedTime.Value).FirstOrDefault<NewsApprovementHistory>(nnn => nnn.NewsId == st.Id);
                    //Int16 lastSeq = (from na in db.NewsApprovementHistories where na.NewsId == st.Id orderby na.CreatedTime descending select na.Sequence.Value).Take(1).SingleOrDefault<Int16>();
                    short lastSeq = 0;
                    if (nah != null) lastSeq = nah.Sequence.Value;
                    newHis.Sequence = (short)(lastSeq + 1);

                    db.NewsApprovementHistories.InsertOnSubmit(newHis); 

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
                    if (st.ApprovementStatus == (short)EnumNewsApprovementStatus.DaDuyetChuaXuatBan || st.ApprovementStatus == (short)EnumNewsApprovementStatus.ThuHoiTinDaXuatBan)
                    {
                        e.Item.FindControl("imgSend").Visible = true;
                        e.Item.FindControl("imgWithdraw").Visible = false;
                    }
                    else if (st.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan)
                    {
                        e.Item.FindControl("imgSend").Visible = false;
                        e.Item.FindControl("imgWithdraw").Visible = true;
                    }
                }
            }
            //else if (e.Item is GridPagerItem)
            //{
            //    GridPagerItem pager = (GridPagerItem)e.Item;
            //    RadComboBox PageSizeComboBox = (RadComboBox)pager.FindControl("PageSizeComboBox");
            //    Label ChangePageSizeLabel = (Label)pager.FindControl("ChangePageSizeLabel");
            //    PageSizeComboBox.Visible = false;
            //    ChangePageSizeLabel.Visible = false;
            //}
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