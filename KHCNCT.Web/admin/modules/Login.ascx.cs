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
using KHCNCT.Globals.Enums.Role;

namespace KHCNCT.Admin.modules
{
    enum LoginPanelView { Login, Logined}

    public partial class Login : CommonBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsLogined) DisplayView(LoginPanelView.Logined);
            //else DisplayView(LoginPanelView.Login);
        }

        private void DisplayView(LoginPanelView view)
        {
            if (view == LoginPanelView.Login)
            {
               
                
            }
            else if (view == LoginPanelView.Logined)
            {
               
            }
        }

        /// <summary>
        /// dang ky cac session luu giu phien lam viec cua nguoi dung da dang nhap thanh cong
        /// </summary>
        /// <param name="user"></param>
        private void RegisterLoginSession(SysUser user)
        {
            Session["UserId"] = user.UserAccount.UserId;
            Session["GroupId"] = (short)user.UserAccount.SysGroupId;
            Session["User"] = user;
            Session["CKBaseURL"] = ResolveUrl("~/upload/userfiles/" + user.UserAccount.UserId.ToString() + "/");
        }

        public static void LogOut(HttpRequest req)
        {
            req.RequestContext.HttpContext.Session["UserId"] = -1;
            req.RequestContext.HttpContext.Session["GroupId"] = (short)UserRole.Guest;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            RadCaptcha1.Validate();
            if (Page.IsValid)
            {
                SysUser user = db.SysUsers.SingleOrDefault<SysUser>(u => u.UserAccount.AccountName == AccountUtilities.ProcessUsername(txtUsername.Text)
                                                                    && u.UserAccount.Password == Hash.GetHashMD5Value(txtPassword.Text));
                if (user != null)
                {
                    UserAccount userAcc = user.UserAccount;
                    if (userAcc.IsActivated == false) //tai khoan chua kich hoat
                    {
                        lblMessage.Text = Resources.AccountMessage.AccountNotActivated;
                    }
                    else if (userAcc.IsDisabled == true) //tai khoan dang tam khoa
                    {
                        lblMessage.Text = String.Format(Resources.AccountMessage.AccountBlock, userAcc.DisabledReason);
                    }
                    else if (userAcc.IsExpired == true) //tai khoan het han (chua xoa hoan toan khoi he thong)
                    {
                        lblMessage.Text = Resources.AccountMessage.AccountExpired;
                    }
                    else if ((userAcc.SysGroupId != (int)KHCNCT.Globals.Enums.Role.UserRole.Administrator)
                                   && (userAcc.SysGroupId != (int)KHCNCT.Globals.Enums.Role.UserRole.InternalUser))//nguoi dung ko co quyen dang nhap
                    {
                        lblMessage.Text = Resources.AccountMessage.AccessDenied;
                    }
                    else
                    {
                        RegisterLoginSession(user);
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
}