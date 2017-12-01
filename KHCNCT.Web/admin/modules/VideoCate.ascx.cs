using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Admin.Controls;
using KHCNCT.Globals;
using KHCNCT.Data;
using NT.Lib;
using KHCNCT.Configuration;
using KHCNCT.Globals.Enums;

namespace KHCNCT.Web.admin.modules
{

    public partial class VideoCate : CommonBaseControl
    {
        /// <summary>
        /// View hien tai cua control. Moi thoi diem chi co 1 View hien thi
        /// </summary>
        private View CurrentView
        {
            get
            {
                if (Request.QueryString["act"] != null)
                {
                    switch (Request.QueryString["act"])
                    {
                        case "update": return View.Update;
                        case "manage": return View.Manage;
                        default: return View.Manage;
                    }
                }
                else return View.Manage;
            }
        }

        private int _vid;
        protected int CateVideoId
        {
            get
            {
                if (_vid == 0 && Request.QueryString["cvid"] != null) int.TryParse(Request.QueryString["cvid"], out _vid);
                return _vid;
            }
        }

        private VideoCategory _currentCate;
        protected VideoCategory CurrentCateVideo
        {
            get
            {
                if (_currentCate == null && CateVideoId > 0) _currentCate = db.VideoCategories.SingleOrDefault<VideoCategory>(v => v.Id == CateVideoId);
                return _currentCate;
            }
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowPanel();
            if (!IsPostBack)
            {
                LoadVideoCategories();
                if (CurrentView == View.Update && CurrentCateVideo != null)
                {
                    txtCategoryName.Text = CurrentCateVideo.CategoryName;
                    ckbShowHomePage.Checked = CurrentCateVideo.ShowInHomepage.Value;
                    rntOrder.Value = CurrentCateVideo.Order.Value;
                }
            }
        }

        private void LoadVideoCategories()
        {
            List<VideoCategory> lstCate = db.VideoCategories.OrderBy<VideoCategory, int>(vc => vc.Order.Value).ToList<VideoCategory>();
            grvVideoCategory.DataSource = lstCate;
            grvVideoCategory.DataBind();
        }

        /// <summary>
        /// Hien thi View hien tai cua control. Moi thoi diem chi co 1 View hien thi
        /// </summary>
        private void ShowPanel()
        {
            divManage.Visible = (CurrentView == View.Manage);
            divUpdate.Visible = (CurrentView == View.Update);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("videocate"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Page.IsValid)
                {
                    if (CurrentCateVideo == null)
                    {
                        VideoCategory vc = new VideoCategory();
                        vc.CategoryName = txtCategoryName.Text;
                        vc.CreatedBy = UserInfo.UserAccount.AccountName;
                        vc.CreatedTime = DateTime.Now;
                        vc.Order = Convert.ToInt32(rntOrder.Value);
                        vc.ShowInHomepage = ckbShowHomePage.Checked;

                        db.VideoCategories.InsertOnSubmit(vc);
                        db.SubmitChanges();
                    }
                    else
                    {
                        CurrentCateVideo.CategoryName = txtCategoryName.Text;
                        CurrentCateVideo.Order = Convert.ToInt32(rntOrder.Value);
                        CurrentCateVideo.ShowInHomepage = ckbShowHomePage.Checked;

                        db.SubmitChanges();
                    }
                    Response.Redirect(Common.GenerateAdminUrl("videocate"));
                }
            }
        }

        protected void lkbAddNewCate_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("videocate","act=update"));
        }

        protected void grvVideoCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvVideoCategory.EditIndex = e.NewEditIndex;
            LoadVideoCategories();
        }

        protected void grvVideoCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvVideoCategory.EditIndex = -1;
            LoadVideoCategories();
        }

        protected void grvVideoCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            
        }

        protected void grvVideoCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(grvVideoCategory.DataKeys[e.RowIndex].Value);
            VideoCategory vCate = db.VideoCategories.SingleOrDefault<VideoCategory>(vc => vc.Id == id);
            if (vCate != null)
            {
                db.VideoCategories.DeleteOnSubmit(vCate);
                db.SubmitChanges();

                LoadVideoCategories();
            }
        }
    }
}