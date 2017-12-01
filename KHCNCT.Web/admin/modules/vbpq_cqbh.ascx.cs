using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Data;
using KHCNCT.Admin.Controls;
using KHCNCT.Globals;

namespace KHCNCT.Web.admin.modules
{
    public partial class vbpq_cqbh : CommonBaseControl
    {
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
                        case "update": return View.Update;
                        case "manage": return View.Manage;
                        default: return View.Manage;
                    }
                }
                else return View.Manage;
            }
        }

        private int _idcq;
        public int IdCQBH
        {
            get
            {
                if (_idcq == 0 && Request.QueryString["idcq"] != null)
                        int.TryParse(Request.QueryString["idcq"], out _idcq);
                return _idcq;
            }
        }

        private VBPQ_CoQuanBH _CoQuanBH;
        public VBPQ_CoQuanBH CoQuanBH
        {
            get
            {
                if (_CoQuanBH == null) _CoQuanBH = db.VBPQ_CoQuanBHs.SingleOrDefault<VBPQ_CoQuanBH>(cq => cq.Id == IdCQBH);
                return _CoQuanBH;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowPanel();
            if (!IsPostBack)
            {
                if (CoQuanBH != null)
                {
                    txtCQBH.Text = CoQuanBH.TenCoQuanBH;
                    txtGhiChu.Text = CoQuanBH.GhiChu;
                }
                else
                {
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            List<VBPQ_CoQuanBH> lstData = (from c in db.VBPQ_CoQuanBHs
                                           orderby c.TenCoQuanBH
                                           select c).ToList<VBPQ_CoQuanBH>();
            grvCQBH.DataSource = lstData;
            grvCQBH.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("vbpq_cqbh"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Page.IsValid)
                {
                    if (CoQuanBH == null)
                    {
                        VBPQ_CoQuanBH vc = new VBPQ_CoQuanBH();
                        vc.TenCoQuanBH = txtCQBH.Text;
                        vc.GhiChu = txtGhiChu.Text;

                        db.VBPQ_CoQuanBHs.InsertOnSubmit(vc);
                        db.SubmitChanges();
                    }
                    else
                    {
                        CoQuanBH.TenCoQuanBH = txtCQBH.Text;
                        CoQuanBH.GhiChu = txtGhiChu.Text;

                        db.SubmitChanges();
                    }
                    Response.Redirect(Common.GenerateAdminUrl("vbpq_cqbh"));
                }
            }
        }

        private void ShowPanel()
        {
            divManage.Visible = (CurrentView == View.Manage);
            divUpdate.Visible = (CurrentView == View.Update);
        }

        protected void grvCQBH_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(grvCQBH.DataKeys[e.RowIndex].Value);
            VBPQ_CoQuanBH vCate = db.VBPQ_CoQuanBHs.SingleOrDefault<VBPQ_CoQuanBH>(vc => vc.Id == id);
            if (vCate != null)
            {
                db.VBPQ_CoQuanBHs.DeleteOnSubmit(vCate);
                db.SubmitChanges();

                LoadData();
            }
        }

        protected void lkbAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("vbpq_cqbh", "act=update"));
        }
    }
}