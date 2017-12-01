using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Admin.Controls;
using KHCNCT.Globals;
using KHCNCT.Data;


namespace KHCNCT.Admin.Controls
{
    public partial class main : CommonBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogined)
            {
                CommonBaseControl ctl = (CommonBaseControl)LoadControl("~/admin/modules/login.ascx");
                if (ctl != null)
                {
                    ContentPane.Controls.Add(ctl);
                }
                else
                {
                    throw new Exception("Không tìm thấy yêu cầu");
                }
            }
            else
            {
                if (Request.QueryString["mod"] != null) LoadAction(Request.QueryString["mod"]);
                //else LoadAction("summary");
            }
            //lkbLogout.Visible = IsIntenalUser;
            //hplChangePass.Visible = IsIntenalUser;
            main_menu1.Visible = IsIntenalUser;   
        }

        protected bool IsIntenalUser
        {
            get
            {
                return (CurrentRole == KHCNCT.Globals.Enums.Role.UserRole.InternalUser) || IsAdmin;
            }
        }


        protected void lkbLogout_Click(object sender, EventArgs e)
        {
            KHCNCT.Admin.modules.Login.LogOut(Request);
            Response.Redirect("~/admin/default.aspx");
        }

        protected void LoadAction(string action)
        {
            try
            {
                if (IsAdmin || IsValidUserRight(action))
                {
                    CommonBaseControl ctl = (CommonBaseControl)LoadControl("~/admin/modules/" + action + ".ascx");
                    if (ctl != null)
                    {
                        ContentPane.Controls.Add(ctl);
                    }
                }
                else
                {
                    CommonBaseControl ctl = (CommonBaseControl)LoadControl("~/admin/modules/forbidden.ascx");
                    if (ctl != null)
                    {
                        ContentPane.Controls.Add(ctl);
                    }
                    //Response.Redirect(Common.GenerateAdminUrl(action,"returnurl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
                }
            }
            catch (Exception ex)
            { 
                //
            }
        }

        private bool IsValidUserRight(string action)
        {
            if (action.ToLower() == "changepass")
            {
                return true;
            }
            else
            {
                AdminUserRight right = db.AdminUserRights.SingleOrDefault<AdminUserRight>(am => am.ModKey == action.ToLower() && am.UserId == UserId);
                return (right != null && right.Allowed.HasValue && (right.Allowed.Value == true));
            }
        }
    }
}