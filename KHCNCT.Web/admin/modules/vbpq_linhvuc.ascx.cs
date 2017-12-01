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
    public partial class vbpq_linhvuc : CommonBaseControl
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
        public int IdLV
        {
            get
            {
                if (_idcq == 0 && Request.QueryString["idlv"] != null)
                    int.TryParse(Request.QueryString["idlv"], out _idcq);
                return _idcq;
            }
        }

        private VBPQ_LinhVuc _LinhVucVB;
        public VBPQ_LinhVuc LinhVucVB
        {
            get
            {
                if (_LinhVucVB == null) _LinhVucVB = db.VBPQ_LinhVucs.SingleOrDefault<VBPQ_LinhVuc>(cq => cq.Id == IdLV);
                return _LinhVucVB;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowPanel();
            if (!IsPostBack)
            {
                if (LinhVucVB != null)
                {
                    txtCQBH.Text = LinhVucVB.TenLinhVuc;
                    txtGhiChu.Text = LinhVucVB.GhiChu;
                }
                else
                {
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            List<VBPQ_LinhVuc> lstData = (from c in db.VBPQ_LinhVucs
                                           orderby c.TenLinhVuc
                                          select c).ToList<VBPQ_LinhVuc>();
            grvCQBH.DataSource = lstData;
            grvCQBH.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("vbpq_linhvuc"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Page.IsValid)
                {
                    if (LinhVucVB == null)
                    {
                        VBPQ_LinhVuc vc = new VBPQ_LinhVuc();
                        vc.TenLinhVuc = txtCQBH.Text;
                        vc.GhiChu = txtGhiChu.Text;

                        db.VBPQ_LinhVucs.InsertOnSubmit(vc);
                        db.SubmitChanges();
                    }
                    else
                    {
                        LinhVucVB.TenLinhVuc = txtCQBH.Text;
                        LinhVucVB.GhiChu = txtGhiChu.Text;

                        db.SubmitChanges();
                    }
                    Response.Redirect(Common.GenerateAdminUrl("vbpq_linhvuc"));
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
            VBPQ_LinhVuc vCate = db.VBPQ_LinhVucs.SingleOrDefault<VBPQ_LinhVuc>(vc => vc.Id == id);
            if (vCate != null)
            {
                db.VBPQ_LinhVucs.DeleteOnSubmit(vCate);
                db.SubmitChanges();

                LoadData();
            }
        }

        protected void lkbAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("vbpq_linhvuc", "act=update"));
        }
    }
}