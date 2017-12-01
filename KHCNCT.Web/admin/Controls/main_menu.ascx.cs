using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Admin.Controls;
using KHCNCT.Data;
using Telerik.Web.UI;
using NT.Lib;
using KHCNCT.Globals;

namespace EStore.admin.controls
{
    public partial class main_menu : CommonBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildMainMenu();
        }

        private void BuildMainMenu()
        {
            List<AdminUserRight> lstUserAssign = db.AdminUserRights.Where<AdminUserRight>(sua => sua.UserId == UserId).ToList<AdminUserRight>();

            rmnMainMenu.Items.Clear();
            RadMenuItem seperatorHT = new Telerik.Web.UI.RadMenuItem();
            seperatorHT.IsSeparator = true;
            seperatorHT.Text = "-";
            List<AdminModGroup> lstGroup = db.AdminModGroups.Where<AdminModGroup>(adm=>adm.ShowInMenu == true).OrderBy<AdminModGroup, int>(adm => adm.Order.Value).ToList<AdminModGroup>();
            foreach (AdminModGroup group in lstGroup)
            {
                RadMenuItem mnuGroup = new Telerik.Web.UI.RadMenuItem(group.MenuText);
                foreach (AdminMod mod in group.AdminMods)
                {
                    if ((IsAdmin || lstUserAssign.Where<AdminUserRight>(sua => sua.ModKey == mod.ModKey).SingleOrDefault<AdminUserRight>() != null) && mod.ShowInMenu == true)
                    {
                        RadMenuItem mnuItem = new Telerik.Web.UI.RadMenuItem(mod.ModName, ResolveUrl(Common.GenerateAdminUrl(mod.ModKey)));
                        mnuGroup.Items.Add(mnuItem);
                    }
                }
                if (mnuGroup.Items.Count > 0) rmnMainMenu.Items.Add(mnuGroup);
            }

            RadMenuItem mnuLogout = new Telerik.Web.UI.RadMenuItem("Đăng xuất");
            mnuLogout.Value = "logout";
            rmnMainMenu.Items.Add(mnuLogout);
        }

        protected void rmnMainMenu_ItemClick(object sender, RadMenuEventArgs e)
        {
            if (e.Item.Value == "logout")
            {
               KHCNCT.Admin.modules.Login.LogOut(Request);
               Response.Redirect("~/admin/default.aspx");
            }
        }

    }
}