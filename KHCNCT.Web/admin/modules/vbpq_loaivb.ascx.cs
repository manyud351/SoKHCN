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
    public partial class vbpq_loaivb : CommonBaseControl
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
        public int IdLoaiVB
        {
            get
            {
                if (_idcq == 0 && Request.QueryString["idlvb"] != null)
                    int.TryParse(Request.QueryString["idlvb"], out _idcq);
                return _idcq;
            }
        }

        private VBPQ_LoaiVB _LoaiVB;
        public VBPQ_LoaiVB LoaiVB
        {
            get
            {
                if (_LoaiVB == null) _LoaiVB = db.VBPQ_LoaiVBs.SingleOrDefault<VBPQ_LoaiVB>(cq => cq.Id == IdLoaiVB);
                return _LoaiVB;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowPanel();
            if (!IsPostBack)
            {
                if (LoaiVB != null)
                {
                    txtCQBH.Text = LoaiVB.LoaiVB;
                }
                else
                {
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            List<VBPQ_LoaiVB> lstData = (from c in db.VBPQ_LoaiVBs
                                           orderby c.LoaiVB
                                           select c).ToList<VBPQ_LoaiVB>();
            grvCQBH.DataSource = lstData;
            grvCQBH.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("vbpq_loaivb"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Page.IsValid)
                {
                    if (LoaiVB == null)
                    {
                        VBPQ_LoaiVB vc = new VBPQ_LoaiVB();
                        vc.LoaiVB = txtCQBH.Text;

                        db.VBPQ_LoaiVBs.InsertOnSubmit(vc);
                        db.SubmitChanges();
                    }
                    else
                    {
                        LoaiVB.LoaiVB = txtCQBH.Text;

                        db.SubmitChanges();
                    }
                    Response.Redirect(Common.GenerateAdminUrl("vbpq_loaivb"));
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
            VBPQ_LoaiVB vCate = db.VBPQ_LoaiVBs.SingleOrDefault<VBPQ_LoaiVB>(vc => vc.Id == id);
            if (vCate != null)
            {
                db.VBPQ_LoaiVBs.DeleteOnSubmit(vCate);
                db.SubmitChanges();

                LoadData();
            }
        }

        protected void lkbAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(Common.GenerateAdminUrl("vbpq_loaivb", "act=update"));
        }
    }
}