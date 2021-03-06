﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Data;
using System.Configuration;
using KHCNCT.Globals.Enums.Role;

namespace KHCNCT.Admin.Controls
{
    public class CommonBaseControl : System.Web.UI.UserControl
    {
        protected KHCNCTDataContext db = new KHCNCTDataContext(ConfigurationManager.ConnectionStrings["SoKHCNCT"].ConnectionString);

        private int _userId;

        protected int UserId
        {
            get
            {
                if ((_userId == 0) && (Session["UserId"] != null))
                {
                    _userId = Convert.ToInt32(Session["UserId"]);
                }
                return _userId;
            }
        }

        private SysUser _UserInfo;
        protected SysUser UserInfo
        {
            get
            {
                if (_UserInfo == null && UserId > 0)
                {
                    _UserInfo = db.SysUsers.SingleOrDefault<SysUser>(u => u.Id == UserId);
                }
                return _UserInfo;
            }
        }

        private UserAccount _account;
        protected UserAccount AccountInfo
        {
            get
            {
                if (_account == null && UserId > 0)
                {
                    _account = db.UserAccounts.SingleOrDefault<UserAccount>(ua => ua.UserId == UserId);
                }
                return _account;
            }
        }

        protected bool IsLogined
        {
            get { return (UserId > 0); }
        }

        private short _privilegeLevel;
        
        protected short PrivilegeLevel
        {
            get
            {
                if ((_privilegeLevel == 0) && (Session["PrivilegeLevel"] != null))
                {
                    _privilegeLevel = Convert.ToInt16(Session["PrivilegeLevel"]);
                }
                return _privilegeLevel;
            }
        }

        protected UserRole CurrentRole
        {
            get
            {
                if (Session["GroupId"] != null) return (UserRole)((short)Session["GroupId"]);
                else return UserRole.Guest;
            }
        }

        protected bool IsAdmin
        {
            get
            {
                return (CurrentRole == KHCNCT.Globals.Enums.Role.UserRole.Administrator);
            }
        }
    }
}