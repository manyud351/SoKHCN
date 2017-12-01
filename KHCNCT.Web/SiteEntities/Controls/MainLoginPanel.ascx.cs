using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Globals;
using KHCNCT.Data;
using NT.Lib;


namespace KHCNCT.Web.SiteEntities.Controls
{
    enum LoginPanelView { Login, Logined}

    public partial class MainLoginPanel : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsLogined) DisplayView(LoginPanelView.Logined);
            else DisplayView(LoginPanelView.Login);
        }

        private void DisplayView(LoginPanelView view)
        {
            if (view == LoginPanelView.Login)
            {
                hplForgotPass.NavigateUrl = Common.GenerateUrl("resetpass");
                hplRegister.NavigateUrl = Common.GenerateUrl("register");
                divLogin.Visible = true;
                divLogined.Visible = false;
            }
            else if (view == LoginPanelView.Logined)
            {
                hplUserPage.NavigateUrl = Common.GenerateUrl("personal");
                hplLogout.NavigateUrl = Common.GenerateUrl("logout", "returnurl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery));
                divLogin.Visible = false;
                divLogined.Visible = true;
            }
        }

        /// <summary>
        /// dang ky cac session luu giu phien lam viec cua nguoi dung da dang nhap thanh cong
        /// </summary>
        /// <param name="user"></param>
        private void RegisterLoginSession(UserAccount userAccount)
        {
            SysUser user = db.SysUsers.SingleOrDefault<SysUser>(u=> u.Id == userAccount.UserId);
            if (user != null)
            {
                Session["UserId"] = user.Id;
                //Session["GroupId"] = user.UserGroup.GroupId;
                //Session["GroupName"] = user.UserGroup.SysGroup.GroupName;
                //Session["PrivilegeLevel"] = user.UserGroup.SysGroup.PrivilegeLevel;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserAccount userAccount  = db.UserAccounts.SingleOrDefault<UserAccount>(u => u.AccountName == AccountUtilities.ProcessUsername(txtUsername.Text)
                                                                && u.Password == Hash.GetHashMD5Value(txtPassword.Text));
            if (userAccount != null)
            {
                if (userAccount.IsActivated == false) //tai khoan chua kich hoat
                {
                    lblMessage.Text = Resources.AccountMessage.AccountNotActivated;
                }
                else if (userAccount.IsDisabled == true) //tai khoan dang tam khoa
                {
                    lblMessage.Text = String.Format(Resources.AccountMessage.AccountBlock, userAccount.DisabledReason);
                }
                else //nguoi dung co the dang nhap
                {
                    RegisterLoginSession(userAccount);
                    if (Request.QueryString["returnurl"] != null) Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["returnurl"]));
                    else Response.Redirect(Request.Url.ToString());
                    //else Response.Redirect(Common.GenerateUrl("profile"));
                }
            }
            else
            {
                lblMessage.Text = Resources.AccountMessage.LoginFailed;
            }
        }
    }
}