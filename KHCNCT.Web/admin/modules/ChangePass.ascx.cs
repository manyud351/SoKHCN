using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Admin.Controls;
using KHCNCT.Data;
using NT.Lib;
using KHCNCT.Globals;

namespace EStore.admin.modules
{
    public partial class ChangePass : CommonBaseControl
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
 
        }
        
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SysUser user = db.SysUsers.SingleOrDefault<SysUser>(u => u.Id == UserId);
                if (user != null)
                {
                    UserAccount acc = user.UserAccount;
                    string oldPass = Hash.GetHashMD5Value(txtOldPass.Text);
                    if (acc.Password == oldPass)
                    {
                        acc.Password = Hash.GetHashMD5Value(txtNewPass.Text);
                        db.SubmitChanges();
                        lblMessage.Text = Resources.AccountMessage.ChangePassSuccess;
                    }
                    else
                    {
                        lblMessage.Text = Resources.AccountMessage.OldPassInvalid;
                    }
                }
            }
        }
    }
}