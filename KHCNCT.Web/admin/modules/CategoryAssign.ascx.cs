using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Admin.Controls;

namespace KHCNCT.Web.admin.modules
{
    public partial class CategoryAssign : CommonBaseControl
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}