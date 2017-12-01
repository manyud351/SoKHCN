using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Web.SiteEntities.Controls;

namespace KHCNCT.Web.SiteEntities.Modules
{
    public partial class lien_he : ModuleBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            RadCaptcha1.Validate();
            if (Page.IsValid)
            {
            }
        }
    }
}