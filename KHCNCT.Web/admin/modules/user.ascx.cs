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

namespace KHCNCT.admin.modules.SUser
{
    /// <summary>
    /// Dinh nghia cac View cua Control
    /// </summary>
    enum View { Manage, AddNew, AssignRight, AssignCategory, Edit, ResetPass, Delete }

    public partial class suser : CommonBaseControl
    {
        /*===========================================================
          Cac thuoc tinh rieng cua control
         ===========================================================*/
        #region Protected Properties


        /// <summary>
        /// Chua User ID cua nguoi dung can quan tri (khac voi UserId la ID cua nguoi dung hien tai)
        /// </summary>
        protected int UID
        {
            get
            {
                int uid = 0;
                if (Request.QueryString["uid"] != null) int.TryParse(Request.QueryString["uid"], out uid);
                return uid;
            }
        }

        /// Chua User ID cua nguoi dung can quan tri (khac voi UserId la ID cua nguoi dung hien tai)
        /// </summary>
        protected int RealUserId
        {
            get
            {
                int ruid = 0;
                if (Request.QueryString["ruid"] != null) int.TryParse(Request.QueryString["ruid"], out ruid);
                return ruid;
            }
        }

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
                        case "add": return View.AddNew;
                        case "edit": return View.Edit;
                        case "right": return View.AssignRight;
                        case "cate": return View.AssignCategory;
                        case "resetpass": return View.ResetPass;
                        case "delete": return View.Delete;
                        default: return View.Manage;
                    }
                }
                else return View.Manage;
            }
        }


        protected int pageSize = 20;

        private int _CurrPage;
        
        /// <summary>
        /// 
        /// </summary>
        public int CurrPage
        {
            get
            {
                if (hdfPageIndex != null) int.TryParse(hdfPageIndex.Value, out _CurrPage);
                if (_CurrPage == 0) _CurrPage = 1;
                return _CurrPage;
            }
        }
        
        #endregion


        /*===========================================================
          Cac ham load thong tin tuong ung len control
         ===========================================================*/
        #region Load Info

        private void LoadCategories(int parentId, RadTreeNode parentNode)
        {
            UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.UserId == UID);
            if (user != null)
            {
                lblUsername5.Text = user.AccountName;

                List<NewsCategory> lstCategories;
                if(parentId == 0) 
                {
                    rtvCategory.Nodes.Clear();
                    lstCategories = db.NewsCategories.Where<NewsCategory>(nc => nc.Level == 1).ToList<NewsCategory>();
                }
                else lstCategories = db.NewsCategories.Where<NewsCategory>(nc => nc.ParentId == parentId).ToList<NewsCategory>();

                List<CategoryAssignUser> lstCateUsers = db.CategoryAssignUsers.Where<CategoryAssignUser>(g => g.UserId == UID).ToList<CategoryAssignUser>();

                for (int i = 0; i < lstCategories.Count; i++)
                {
                    RadTreeNode item = new RadTreeNode();
                    item.Text = lstCategories[i].CategoryName;
                    item.Value = lstCategories[i].Id.ToString();
                    if (lstCategories[i].Level < SystemConfig.ApplicationConfig.News_CategoryLevel)
                    {
                        //item.Checkable = false;
                        item.Font.Bold = true;
                    }

                    if (lstCateUsers.Find(x => x.CategoryId == lstCategories[i].Id) != null)
                    {
                        item.Checked = true;
                        if (parentNode != null)
                        {
                            parentNode.Expanded = true;
                            parentNode.ForeColor = System.Drawing.Color.Red;
                        }
                        //item.Checked = true;
                    }

                    if (parentNode == null)
                    {
                        rtvCategory.Nodes.Add(item);
                    }
                    else parentNode.Nodes.Add(item);
                    LoadCategories(lstCategories[i].Id, item);
                }                
            }
        }


        private void LoadAdminMods()
        {
            UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.UserId == UID);
            if (user != null)
            {
                rtvAdminMod.Nodes.Clear();

                lblUsername4.Text = user.AccountName;

                List<AdminUserRight> lstUserAssign = db.AdminUserRights.Where<AdminUserRight>(sua => sua.UserId == UID).ToList<AdminUserRight>();

                List<AdminModGroup> lstModGroups = db.AdminModGroups.Where<AdminModGroup>(g => g.ShowInMenu == true).ToList<AdminModGroup>();
                foreach (AdminModGroup group in lstModGroups)
                {
                    RadTreeNode item = new RadTreeNode();
                    item.Text = group.GroupName;
                    item.Value = group.Id.ToString();
                    item.Checkable = false;
                    item.Font.Bold = true;

                    List<AdminMod> lstMods = group.AdminMods.Where<AdminMod>(m => m.ShowInMenu == true).ToList<AdminMod>();
                    foreach (AdminMod mod in lstMods)
                    {
                        RadTreeNode childItem = new RadTreeNode();
                        childItem.Text = mod.ModName;
                        childItem.Value = mod.ModKey.ToString();
                        childItem.Checkable = true;
                        if (lstUserAssign.Find(x => x.ModKey == mod.ModKey) != null)
                        {
                            childItem.Checked = true;
                            //item.Checked = true;
                            item.ForeColor = System.Drawing.Color.Red;
                            item.Expanded = true;
                        }
                        item.Nodes.Add(childItem);
                    }

                    rtvAdminMod.Nodes.Add(item);
                }
            }            
        }

        /// <summary>
        /// Thuc hien tim kiem User theo dieu kien loc
        /// </summary>
        private void DoSearchUsers()
        {
            NT.Lib.PreventSQLInjection.ClearSQLInjectionInAllControls(divManageUsers.Controls);
            string strSQL = "select * from [vUserAccount] ";
            string strCount = " select count(UserId) from [vUserAccount] ";
            string strCreteria = "";
            string order = " order by CreatedTime desc";
            
            if (txtFilterUsername.Text != "")
            {
                strCreteria += " and [AccountName] like N'%" + txtFilterUsername.Text + "%' ";
            }
            if (txtFiterName.Text != "")
            {
                strCreteria += " and (FirstSurName like N'%" + txtFiterName.Text + "%' or LastName like N'%" + txtFiterName.Text + "%') ";
            }
            
            if (ddlFilterBlock.SelectedIndex > 0)
            {
                strCreteria += " and IsBlocked =" + ddlFilterBlock.SelectedValue + " ";
            }
            if (rdpFilterFromDate.SelectedDate != null)
            {
                strCreteria += " and CreatedTime >='" + rdpFilterFromDate.SelectedDate.Value.ToString("yyyy-MM-dd") + "' ";
            }
            if (rdpFilterToDate.SelectedDate != null)
            {
                strCreteria += " and CreatedTime <='" + rdpFilterToDate.SelectedDate.Value.ToString("yyyy-MM-dd") + "' ";
            }
            
            //build last queyry
            //if (strCreteria != "") strCreteria = " where ua.UserId = su.UserId " + strCreteria;
            if (strCreteria != "") strCreteria = " where " + strCreteria.Substring(4);//remove keyword 'and'

            //count total record
            DateTime startSearh = DateTime.Now;
            int totalRecord = db.ExecuteQuery<int>(strCount + strCreteria).SingleOrDefault<int>();            
            ltrPaging.Text = NT.Lib.Utilities.GenratePagingString(totalRecord, pageSize, CurrPage, 20, "Trang: ", "<span style=\'color:#000\'>{0}</span>", "<span style=\'color:#aaa\'>{0}</span>", "javascript:gotoPage({0})");
            
            strSQL = strSQL + strCreteria + order;

            List<vUserAccount> lstUser = db.ExecuteQuery<vUserAccount>(strSQL).Skip(pageSize * (CurrPage - 1)).Take(pageSize).ToList<vUserAccount>();
            grvManageUsers.DataSource = lstUser;
            grvManageUsers.DataBind();

            ltrTotalResult.Text = "Tìm thấy <strong>" + totalRecord.ToString() + "</strong> kết quả trong " + DateTime.Now.Subtract(startSearh).Milliseconds.ToString() + " ms" ;
        }


        /// <summary>
        /// Load thong tin nguoi dung trong truong hop Reset mat khau (View = ResetPass)
        /// </summary>
        private void LoadUserResetPass()
        {
            if (UID > 0)
            {
                UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.UserId == UID);
                if (user != null)
                {
                    lblUsername3.Text = user.AccountName;
                }
            }
        }
        
        /// <summary>
        /// Hien thi View hien tai cua control. Moi thoi diem chi co 1 View hien thi
        /// </summary>
        private void ShowPanel()
        {
            divEditUser.Visible = (CurrentView == View.Edit);
            divManageUsers.Visible = (CurrentView == View.Manage);
            divAddNew.Visible = (CurrentView == View.AddNew);
            divChangePass.Visible = (CurrentView == View.ResetPass);
            divUserRight.Visible = (CurrentView == View.AssignRight);
            divUserCate.Visible = (CurrentView == View.AssignCategory);
        }

        /// <summary>
        /// Load danh sach tat ca nguoi dung (tru nguoi dung o cap cao nhat: PortalOwner) vao Grid trong truong hop hien thi View Manage
        /// </summary>
        private void LoadUsers()
        {
            List<UserAccount> lstUsers = db.UserAccounts.OrderBy<UserAccount, String>(u => u.AccountName).ToList<UserAccount>();
            grvManageUsers.DataSource = lstUsers;
            grvManageUsers.DataBind();
        }

        /// <summary>
        /// Hien thi thong tin chi tiet cua mot nguoi su dung khi sua thong tin cua nguoi dung do (View = Edit)
        /// </summary>
        private void LoadUserInfo()
        {
            if (UID > 0)
            {
                UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.UserId == UID);
                if (user != null)
                {
                    lblUsername.Text = user.AccountName;
                    txtAddress.Text = user.SysUser.Address;
                    lblEmail.Text = user.SysUser.Email;
                    txtHoLot.Text = user.SysUser.FirstSurName;
                    txtPhone.Text = user.SysUser.Cell;
                    txtTen.Text = user.SysUser.LastName;
                    hplChangePass.NavigateUrl = "~/admin/?mod=user&act=resetpass&uid=" + user.UserId.ToString();
               }
            }
        }

        #endregion
        /*===========================================================
          Cac ham xu ly su kien cua control
         ===========================================================*/
        #region Events Handler
        /// <summary>
        /// Xu ly load trang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowPanel();

            hplAddNew.NavigateUrl = Common.GenerateAdminUrl("user", "act=add");

            if (CurrentView == View.Edit && UID > 0)
            {
                LoadUserInfo();
            }
            else if (CurrentView == View.AddNew)
            {
                //
            }
            else if (CurrentView == View.AssignRight)
            {
                LoadAdminMods();
            }
            else if (CurrentView == View.AssignCategory)
            {
                LoadCategories(0, null);
            }
            else if (CurrentView == View.Manage)
            {
                //LoadUsers();
                DoSearchUsers();
            }
            else if (CurrentView == View.ResetPass)
            {
                LoadUserResetPass();
            }
        }

        
        /// <summary>
        /// Xu ly block/unblock 1 nguoi dung voi ly do block va thoi gian block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvManageUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(grvManageUsers.DataKeys[e.NewEditIndex].Value);
            UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.UserId == id);
            //user dang bi khoa
            if ((user != null) && (user.IsDisabled.HasValue) && (user.IsDisabled.Value))
            {
                user.IsDisabled = false;
                db.SubmitChanges();
            }
            else
            {
                user.IsDisabled = true;
                user.DisabledTime = DateTime.Now;
                db.SubmitChanges();
            }
            //LoadUsers();
            DoSearchUsers();
        }

        /// <summary>
        /// thuc hien xoa mot nguoi dung (chi xoa tam thoi, co the khoi phuc lai duoc)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvManageUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(grvManageUsers.DataKeys[e.RowIndex].Value);
            UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.UserId == id);
            if (user != null)
            {
                db.AdminUserRights.DeleteAllOnSubmit<AdminUserRight>(from s in user.AdminUserRights select s);
                db.UserAccounts.DeleteOnSubmit(user);
                db.SubmitChanges();

                //LoadUsers();
                DoSearchUsers();
            }
        }

        /// <summary>
        /// Thuc hien cap nhat thong tin nguoi dung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (UID > 0)
            {
                UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.UserId == UID);
                if (user != null)
                {
                    PreventSQLInjection.ClearSQLInjectionInAllControls(this.Controls);
                    SysUser sUser = user.SysUser;
                    sUser.Address = txtAddress.Text.Trim();
                    sUser.FirstSurName = txtHoLot.Text.Trim();
                    sUser.Cell = txtPhone.Text.Trim();
                    sUser.LastName = txtTen.Text.Trim();

                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Huy bo thao tac hien hanh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("user"));
        }

        /// <summary>
        /// Huy bo thao tac hien hanh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelAddNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("user"));
        }

        /// <summary>
        /// Tao moi nguoi su dung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveNewUser_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //xu ly cat bo khoang trang trong ten dang nhap va loc SQL Injection
                txtUsername2.Text = AccountUtilities.ProcessUsername(txtUsername2.Text);
                string sUsername = txtUsername2.Text;
                //kiem tra tinh hop le cua ten dang nhap
                if (AccountUtilities.IsValidUsername(txtUsername2.Text))
                {
                    UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.AccountName == sUsername);
                    //neu chua co nguoi dung nao su dung ten dang nhap nay thi co the dang ky duoc
                    if (user == null)
                    {
                        SysUser sUser = new SysUser();
                        sUser.FirstSurName = txtHoLot2.Text;
                        sUser.LastName = txtTen2.Text;
                        sUser.Address = txtDiaChi2.Text;
                        sUser.Cell = txtDT2.Text;
                        sUser.Email = txtEmail2.Text;
                        sUser.CreatedTime = DateTime.Now;
                        sUser.CreatedBy = UserInfo.UserAccount.AccountName;

                        user = new UserAccount();

                        user.CreatedTime = DateTime.Now;                                             
                        user.AccountName = sUsername;
                        user.Password = Hash.GetHashMD5Value(txtPassword2.Text);//ma hoa mat khau dang md5
                        user.IsDisabled = false;
                        user.SysGroupId = (int)KHCNCT.Globals.Enums.Role.UserRole.InternalUser;

                        sUser.UserAccount = user;

                        db.SysUsers.InsertOnSubmit(sUser);
                        db.SubmitChanges();

                        if (ckbSendAlertCreateUserEmail.Checked)
                            MailController.SendAlertStoreAccountCreated2(txtEmail2.Text, txtUsername2.Text, txtPassword2.Text, "" , txtHoLot2 + " " + txtTen2.Text);
                       
                        Response.Redirect(Common.GenerateAdminUrl("user"));
                    }
                    else
                    {
                        //ten truy cap da ton tai
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "",
                                                                        "$('#username_status').html('" + Resources.AccountMessage.UsernameUnAvailable.Replace("'", "\\'") + "');" +
                                                                        "setfocusonerrortextbox('" + txtUsername2.ClientID + "');", true);
                    }
                }
                else
                {
                    //ten dang nhap khong hop le
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "",
                                                                        "$('#username_status').html('<span class=\"validator_error_message\">" + Resources.AccountMessage.InvalidUsername + "</span>');" +
                                                                        "setfocusonerrortextbox('" + txtUsername2.ClientID + "');", true);
                }
            }
        }

        /// <summary>
        /// Bind cac hinh anh tuong ung voi tung trang thai cua tai khoan nguoi dung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvManageUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imbActivated = (ImageButton)e.Row.FindControl("imbActivated");
                ImageButton imbPermanentDelete = (ImageButton)e.Row.FindControl("imbPermanentDelete");
                ImageButton imbDelete = (ImageButton)e.Row.FindControl("imbDelete");
                ImageButton imbRecover = (ImageButton)e.Row.FindControl("imbRecover");

                Image imgEdit = (Image)e.Row.FindControl("imgEdit");
                Image imgResetPass = (Image)e.Row.FindControl("imgResetPass");

                vUserAccount user = (vUserAccount)e.Row.DataItem;
                if (user != null)
                {
                  
                    if (e.Row.RowIndex != grvManageUsers.EditIndex)
                    {
                        ImageButton imbBlock = (ImageButton)e.Row.FindControl("imbBlock");

                        if (user.IsDisabled.HasValue && user.IsDisabled.Value)
                        {
                            imbBlock.ImageUrl = "~/images/lock.png";
                            Literal ltrBlockInfo = (Literal)e.Row.FindControl("ltrBlockInfo");
                            //ltrBlockInfo.Text = "Khoá từ: " + user.BlockedTime.Value.ToString("dd/MM/yyyy");                            
                            ltrBlockInfo.Text = "Đang tạm khóa";                            
                            e.Row.BackColor = System.Drawing.Color.Yellow;
                            e.Row.ToolTip = "Tài khoản đang tạm khoá";
                        }
                        else imbBlock.ImageUrl = "~/images/tip.png";
                    }

                }
            }
        }


        /// <summary>
        /// Cap nhat thong tin khoa tai khoan nguoi dung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvManageUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(grvManageUsers.DataKeys[e.RowIndex].Value);
            UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.UserId == id);
            if (user != null)
            {
                TextBox txtBlockReason = (TextBox)grvManageUsers.Rows[e.RowIndex].FindControl("txtBlockReason");
                RadDatePicker rdtBlockFrom = (RadDatePicker)grvManageUsers.Rows[e.RowIndex].FindControl("rdtBlockFrom");
                RadDatePicker rdtBlockTo = (RadDatePicker)grvManageUsers.Rows[e.RowIndex].FindControl("rdtBlockTo");

                user.IsDisabled = true;
                if (rdtBlockFrom.SelectedDate.HasValue) user.DisabledTime = DateTime.Now;
                
                db.SubmitChanges();                
            }

            grvManageUsers.EditIndex = -1;
            //LoadUsers();
            DoSearchUsers();
        }

        /// <summary>
        /// Huy bo thao tac khoa nguoi dung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvManageUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvManageUsers.EditIndex = -1;
            //LoadUsers();
            DoSearchUsers();
        }

        /// <summary>
        /// Bind cac hinh anh tuong ung voi tung trang thai cua tai khoan nguoi dung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvStoreUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vUserAccount user = (vUserAccount)e.Row.DataItem;
                if (user != null)
                {
                    ImageButton imbBlock = (ImageButton)e.Row.FindControl("imbBlock");

                    if (user.IsDisabled.HasValue && user.IsDisabled.Value)
                    {
                        Literal ltrBlockInfo = (Literal)e.Row.FindControl("ltrBlockInfo");
                        ltrBlockInfo.Text = "Khoá từ: " + user.DisabledTime.Value.ToString("dd/MM/yyyy");
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                        e.Row.ToolTip = "Tài khoản đang tạm khoá";
                    }

                }
            }
        }


        /// <summary>
        /// Thuc hien thay doi mat khau cho nguoi dung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UserAccount user = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.UserId == UID);
                if (user != null)
                {                    
                    user.Password = Hash.GetHashMD5Value(txtNewPass3.Text);
                    SysUser sUser = user.SysUser;
                    db.SubmitChanges();
                    if (ckbSendAlertChangePassEmail.Checked)
                    {
                        MailController.SendResetPassByAdminEmail(sUser.Email, user.AccountName, sUser.FirstSurName + " " + sUser.LastName, txtNewPass3.Text);
                    }
                    lblMessage.Text = Resources.AccountMessage.ChangePassSuccess;
                }
            }
        }


        /// <summary>
        /// Tim kiem nguoi dung theo dieu kien loc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearchUsers();
        }

        protected void btnSaveCate_Click(object sender, EventArgs e)
        {
            db.CategoryAssignUsers.DeleteAllOnSubmit<CategoryAssignUser>(from s in db.CategoryAssignUsers where s.UserId == UID select s);
            db.SubmitChanges();

            foreach (RadTreeNode rNode in rtvCategory.Nodes)
            {
                //root node
                if (rNode.Checked == true)
                {
                    CategoryAssignUser rSua = new CategoryAssignUser();
                    rSua.UserId = UID;
                    rSua.CategoryId = Convert.ToInt32(rNode.Value);
                    rSua.IsDisabled = false;
                    rSua.AssignedBy = AccountInfo.AccountName;
                    rSua.AssignedTime = DateTime.Now;
                    //sua.AssignedTime = DateTime.Now;

                    db.CategoryAssignUsers.InsertOnSubmit(rSua);
                }
                if (rNode.Nodes.Count > 0)
                {
                    foreach (RadTreeNode node in rNode.Nodes)
                    {
                        //not root node
                        if (node.Checked == true)
                        {
                            CategoryAssignUser sua = new CategoryAssignUser();
                            sua.UserId = UID;
                            sua.CategoryId = Convert.ToInt32(node.Value);
                            sua.IsDisabled = false;
                            sua.AssignedBy = AccountInfo.AccountName;
                            sua.AssignedTime = DateTime.Now;
                            //sua.AssignedTime = DateTime.Now;

                            db.CategoryAssignUsers.InsertOnSubmit(sua);
                        }
                    }
                }
            }
            db.SubmitChanges();
        }

        protected void btnSaveRight_Click(object sender, EventArgs e)
        {
            db.AdminUserRights.DeleteAllOnSubmit<AdminUserRight>(from s in db.AdminUserRights where s.UserId == UID select s);
            db.SubmitChanges();

            foreach (RadTreeNode rNode in rtvAdminMod.Nodes)
            {
                if (rNode.Nodes.Count > 0)
                {
                    foreach (RadTreeNode node in rNode.Nodes)
                    {
                        //not root node
                        if (node.Checked == true)
                        {
                            AdminUserRight sua = new AdminUserRight();
                            sua.UserId = UID;
                            sua.ModKey = node.Value;
                            sua.Allowed = true;
                            //sua.AssignedTime = DateTime.Now;

                            db.AdminUserRights.InsertOnSubmit(sua);
                        }
                    }
                }
            }
            db.SubmitChanges();
        }

        protected void btnRightCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("user"));
        }

        #endregion

        

        

        //===========================================================
    }
}