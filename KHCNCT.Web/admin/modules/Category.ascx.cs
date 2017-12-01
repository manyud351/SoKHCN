using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Data;
using System.Reflection;
using KHCNCT.Globals;
using KHCNCT.Globals.Enums.Store;
using NT.Lib;
using Telerik.Web.UI;
using System.Drawing;
using KHCNCT.Admin.Controls;

namespace KHCNCT.admin.modules
{
    public partial class _Category : CommonBaseControl
    {
        protected long CategoryId
        {
            get
            {
                long id = -1;
                if (Request.QueryString["cateid"] != null) long.TryParse(Request.QueryString["cateid"], out id);
                return id;
            }
        }

        private NewsCategory _CurrentCategory;
        public NewsCategory CurrentCategory
        {
            get 
            {
                if (_CurrentCategory == null) _CurrentCategory = db.NewsCategories.SingleOrDefault<NewsCategory>(c => c.Id == CategoryId);
                return _CurrentCategory;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                BindCategoriesToCombobox(0);
                rcbParentCategory.Items.Insert(0, new RadComboBoxItem("", ""));
                //ckbBuy.Checked = ckbSell.Checked = true;
                divIcon.Visible = false;
            }

            if (CategoryId > 0)
            {
                LoadCategory(CategoryId);
            }
        }

        /// <summary>
        /// Load chi tiet mot danh muc de cap nhat
        /// </summary>
        /// <param name="id"></param>
        private void LoadCategory(long id)
        {
            NewsCategory sc = db.NewsCategories.SingleOrDefault<NewsCategory>(s => s.Id == id);
            if (sc != null)
            {
                txtCategoryName.Text = sc.CategoryName;
                if (sc.ParentId.HasValue)
                {
                    RadComboBoxItem item = rcbParentCategory.Items.FindItemByValue(sc.ParentId.ToString());
                    if (item != null) item.Selected = true;
                }
                
                rntOrder.Value = sc.Order;
                if (!String.IsNullOrEmpty(sc.Icon))
                {
                    divIcon.Visible = true;
                    imgIcon.ImageUrl = sc.Icon;
                }
                else
                {
                    divIcon.Visible = false;
                }
            }
        }

        private void BindCategoriesToCombobox(long parentId)
        {
            List<NewsCategory> lstCM;

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
                         where cm.ParentId ==parentId
                         orderby cm.Order
                         select cm).ToList<NewsCategory>();
            }
            
            for (int i = 0; i < lstCM.Count; i++)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                string bullet = "";
                for (int j = 1; j < lstCM[i].Level.Value; j++)
                {
                    bullet += "--";
                }

                item.Text = bullet + " " + lstCM[i].CategoryName;
                item.Value = lstCM[i].Id.ToString();
                rcbParentCategory.Items.Add(item);

                BindCategoriesToCombobox(lstCM[i].Id);
            }
        }

        /// <summary>
        /// Load cac danh muc san pham rong gian hang nguoi dung
        /// </summary>
        private void LoadCategories()
        {
            List<NewsCategory> lsCate = new List<NewsCategory>();
            GetChuyenMucHienThi(0, lsCate);
            grvCategories.DataSource = lsCate;
            grvCategories.DataBind();
        }

        private void GetChuyenMucHienThi(long parentId, List<NewsCategory> lstCMReturn)
        {
            List<NewsCategory> lstCM;
            //
            if (parentId <= 0)
            {
                //neu id chuyen muc cha <= 0 --> load cac chuyen muc cap 1
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

            //them cac chuyen muc vao dropdown list
            for (int i = 0; i < lstCM.Count; i++)
            {
                string bullet = "";
                for (int j = 1; j < lstCM[i].Level.Value; j++)
                {
                    bullet += "--";
                }
                lstCM[i].CategoryName = bullet + " " + lstCM[i].CategoryName;
                //lstCM.InsertRange(i, GetChuyenMucHienThi(lstCM[i].Id));
                lstCMReturn.Add(lstCM[i]);

                GetChuyenMucHienThi(lstCM[i].Id, lstCMReturn);
            }

            //return lstCM;
        }

        protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hplMenuItem = (HyperLink)e.Item.FindControl("hplMenuItem");
                if (hplMenuItem != null)
                {
                    Type t = e.Item.DataItem.GetType();
                    string Id = t.GetProperty("Id").GetValue(e.Item.DataItem, null).ToString();
                    string CategoryName = t.GetProperty("CategoryName").GetValue(e.Item.DataItem, null).ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveCategory();
            }
        }

        private void SaveCategory()
        {
            txtCategoryName.Text = PreventSQLInjection.FilterSQLString(txtCategoryName.Text);
            NewsCategory st = null;
           
            int Order = 1;
            if (rntOrder.Value.HasValue) Order = (int)rntOrder.Value.Value;
            short level = 1;
            bool IsUpdateMode = false;

            if (CategoryId > 0)
            {
                st = db.NewsCategories.SingleOrDefault<NewsCategory>(s => s.Id == CategoryId);
            }

            if (st == null)
            {
                st = new NewsCategory();
            }
            else
            {
                Order = st.Order.Value;
                level = st.Level.Value;
                IsUpdateMode = true;
            }

            st.CategoryName = txtCategoryName.Text;
            //tao so thu tu va cap menu tu dong
            if (rcbParentCategory.SelectedIndex > 0)
            {
                int parentid = Convert.ToInt32(rcbParentCategory.SelectedValue);
                NewsCategory sibling = (from s in db.NewsCategories
                                        where s.ParentId == parentid
                                        orderby s.Order descending
                                                select s).FirstOrDefault<NewsCategory>();
                st.ParentId = parentid;

                if (sibling != null)
                {
                    //neu nguoi dung khong chu dong nhap so thu tu hien thi thi tinh toan so thu tu hien thi
                    //bang cach cong them 1 don vi vao so thu tu cua cac danh muc cung cap
                    if (!rntOrder.Value.HasValue) Order = sibling.Order.Value + 1;
                    level = sibling.Level.Value;
                }
                else {
                    NewsCategory parent = db.NewsCategories.SingleOrDefault<NewsCategory>(s => s.Id == parentid);
                    if (parent != null)
                    {
                        level = (short)(parent.Level.Value + 1);
                    }
                }
            }

            //proccess image
            if (fulIcon.HasFile)
            {
                //trong truong hop update --> xoa file cu tren dia cung truoc khi tien hanh upload
                if (!String.IsNullOrEmpty(st.Icon))
                {
                    System.IO.File.Delete(Server.MapPath(st.Icon));
                }
                string uploadFolder = "~/asset/icons/";
                //tao thu muc trong truong hop thu muc khong ton tai
                if (!System.IO.Directory.Exists(Server.MapPath(uploadFolder))) System.IO.Directory.CreateDirectory(Server.MapPath(uploadFolder));
                //them 1 khoang trang vao cuoi chuoi de tao thuan loi cho viec tao ten file
                //sau khi tao ten file xong se bo khoang trang nay
                st.CategoryName += " ";
                //tao ten file theo cong thuc: tu dau tien trong ten danh muc + 5 ky tu ngau nhien
                string fileName = st.CategoryName.Substring(0, st.CategoryName.IndexOf(" ")) + "_" + Hash.GetRandomHashKey(5) + "." + NT.Lib.Globals.GetFileExtension(fulIcon.FileName);
                //bo khoang trang cuoi cung de tra lai ten ban dau
                st.CategoryName = st.CategoryName.Substring(0, st.CategoryName.Length - 1);

                //luu file vao dia va cap nhat vao DB
                st.Icon = uploadFolder + fileName;
                fulIcon.SaveAs(Server.MapPath(st.Icon));
            }
            //end proccess image

            st.Order = Order;
            st.Level = level;
            if (!IsUpdateMode)
            {
                db.NewsCategories.InsertOnSubmit(st);
            }

            db.SubmitChanges();

            //clear cache
            Cache.Remove("categories");

            //LoadCategories();            
            //txtCategoryName.Text = "";
            //rcbParentCategory.Items.Clear();
            //BindCategoriesToCombobox(0);
            //rcbParentCategory.Items.Insert(0, new RadComboBoxItem("", ""));
            Response.Redirect(Common.GenerateAdminUrl("category"));
        }

        protected void grvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(grvCategories.DataKeys[e.RowIndex].Value);
            NewsCategory sc = db.NewsCategories.SingleOrDefault<NewsCategory>(s => s.Id == id);
            if (sc != null)
            {
                db.NewsCategories.DeleteOnSubmit(sc);
                db.SubmitChanges();
                //clear cache
                Cache.Remove("categories");

                LoadCategories();
            }
        }

        protected void grvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(grvCategories.DataKeys[e.NewEditIndex].Value);
            Response.Redirect(Common.GenerateAdminUrl("category", "cateid=" + id.ToString()));
        }

        protected void grvCategories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                NewsCategory cate = e.Row.DataItem as NewsCategory;
                if (cate != null)
                {
                    LinkButton lkbToogleHomePage = (LinkButton)e.Row.FindControl("lkbToogleHomePage");
                    LinkButton lkbToogleProductByCateModule = (LinkButton)e.Row.FindControl("lkbToogleProductByCateModule");
                    Label lblShowIn = (Label)e.Row.FindControl("lblShowIn");
                    Label lblNotLevel1Category = (Label)e.Row.FindControl("lblNotLevel1Category");
                    
                    string showIntext = "";
                    if (cate.Level == 1)
                    {
                        e.Row.Font.Bold = true;
                        
                        if (showIntext == "") showIntext = "Không hiển thị";
                        else showIntext = "Hiển thị trong: " + showIntext;

                        lblShowIn.Text = showIntext;

                        if (cate.ShowInMainMenu == true)
                        {
                            lkbToogleHomePage.Text = "Dừng hiển thị menu trang chủ";
                            lkbToogleHomePage.ForeColor = Color.Red;
                        }
                        else lkbToogleHomePage.Text = "Hiển thị ở menu trang chủ";

                        lblNotLevel1Category.Visible = false;
                        lkbToogleProductByCateModule.Visible = true;

                        if (cate.ShowInHomePage == true)
                        {
                            lkbToogleProductByCateModule.Text = "Dừng hiển thị trang chủ";
                            lkbToogleProductByCateModule.ForeColor = Color.Red;
                        }
                        else lkbToogleProductByCateModule.Text = "Hiển thị trang chủ";
                    }
                    else
                    {
                        lblShowIn.Text = "--";
                        lkbToogleHomePage.Text = "--";
                        lkbToogleHomePage.Enabled = false;
                        lblNotLevel1Category.Visible = true;
                        lkbToogleProductByCateModule.Visible = false;
                    }
                }
            }
        }

        protected void grvCategories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToogleHomePage")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                NewsCategory sc = db.NewsCategories.SingleOrDefault<NewsCategory>(s => s.Id == id);
                
                if (sc != null)
                {
                    if (!sc.ShowInHomePage.HasValue) sc.ShowInHomePage = true;
                    else sc.ShowInHomePage = !sc.ShowInHomePage;
                    db.SubmitChanges();

                    Cache.Remove("categories");

                    LoadCategories();
                }
            }
            else if (e.CommandName == "ToogleMainMenu")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                NewsCategory sc = db.NewsCategories.SingleOrDefault<NewsCategory>(s => s.Id == id);

                if (sc != null)
                {
                    if (sc.Level == 1)
                    {
                        sc.ShowInMainMenu = !sc.ShowInMainMenu;
                        db.SubmitChanges();

                        Cache.Remove("categories");

                        LoadCategories();
                    }
                }
            }
        }

        protected void grvCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCategories.PageIndex = e.NewPageIndex;
            LoadCategories();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("category"));
        }

        protected void lkbUpdateOrder_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvCategories.Rows)
            {
                RadNumericTextBox rntCateOrder = (RadNumericTextBox)row.FindControl("rntCateOrder");
                int id = Convert.ToInt32(grvCategories.DataKeys[row.RowIndex].Value);
                NewsCategory cate = db.NewsCategories.SingleOrDefault<NewsCategory>(c => c.Id == id);
                if (cate != null && rntCateOrder != null)
                {
                    if (!rntCateOrder.Value.HasValue) cate.Order = 1;
                    else cate.Order = (int)rntCateOrder.Value.Value;
                }
            }
            db.SubmitChanges();
        }

        protected void lkbDeleteIcon_Click(object sender, EventArgs e)
        {
            if (CurrentCategory != null)
            {
                if (!String.IsNullOrEmpty(CurrentCategory.Icon))
                {
                    System.IO.File.Delete(Server.MapPath(CurrentCategory.Icon));
                }
                CurrentCategory.Icon = "";
                db.SubmitChanges();
                divIcon.Visible = false;
            }
        }

    }
}