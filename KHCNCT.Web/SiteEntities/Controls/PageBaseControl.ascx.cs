using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Data;

namespace KHCNCT.Web.SiteEntities.Controls
{
    public partial class PageBaseControl : BaseControl
    {

        private PortalPage _CurrentPage;
        public PortalPage CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set { _CurrentPage = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}