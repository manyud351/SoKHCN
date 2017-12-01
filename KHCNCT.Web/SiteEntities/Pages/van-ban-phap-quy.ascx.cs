using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Web.SiteEntities.Controls;
using KHCNCT.Data;
using KHCNCT.Globals;
using System.Collections;
using NT.Lib;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace KHCNCT.Web.SiteEntities.Pages
{
    public partial class van_ban_phap_quy : PageBaseControl
    {
        protected int pageSize = 20;//default
        protected int pageShow = 20;

        private int _CurrPage;


        /// <summary>
        /// 
        /// </summary>
        public int CurrPage
        {
            get
            {
                if (hdfPageIndex != null) int.TryParse(hdfPageIndex.Value, out _CurrPage);
                if (_CurrPage == 0) _CurrPage = 1;
                return _CurrPage;
            }
        }

        private enum VBPQ_View { General, Detail }

        private int _dmttid;
        public int IdCQBH
        {
            get
            {
                if (Request.QueryString["cqbh"] != null) int.TryParse(Request.QueryString["cqbh"], out _dmttid);
                return _dmttid;
            }
        }

        private int _ttid;
        public int IdLinhVuc
        {
            get
            {
                if (Request.QueryString["lv"] != null) int.TryParse(Request.QueryString["lv"], out _ttid);
                return _ttid;
            }
        }

        private int _vbid;
        public int IdVanBan
        {
            get
            {
                if (Request.QueryString["vbid"] != null) int.TryParse(Request.QueryString["vbid"], out _vbid);
                return _vbid;
            }
        }

        private VBPQ_VanBanChiTiet _VanBan;
        public VBPQ_VanBanChiTiet VanBan
        {
            get
            {
                if (_VanBan == null) _VanBan = db.VBPQ_VanBanChiTiets.SingleOrDefault<VBPQ_VanBanChiTiet>(vb => vb.Id == IdVanBan);
                return _VanBan;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCQBH();
                LoadLinhVuc();
                LoadLoaiVB();

                if (VanBan != null)
                {
                    ShowPanel(VBPQ_View.Detail);
                    BindInfo(VanBan);
                    LoadRelatedVB(VanBan.CQBH.Value);
                    lblVBPQTitle.Text = VanBan.SoHieu;
                }
                else if (IdCQBH > 0)
                {
                    SearchVBByCQBH(IdCQBH);
                    ShowPanel(VBPQ_View.General);
                }
                else if (IdLinhVuc > 0)
                {
                    SearchVBByLinhVuc(IdLinhVuc);
                    ShowPanel(VBPQ_View.General);
                }
                else
                {
                    ShowPanel(VBPQ_View.General);
                    SearchVB();
                    lblVBPQTitle.Text = "Tất cả văn bản";
                }
            }
        }

        private void SearchVBByLinhVuc(int linhvuc)
        {
            string strSQL = "select vb.Id, SoHieu, TrichYeu, FileDinhKem, LoaiFile, CQBH, NgayBH, NgayHL, TenCoQuanBH, ConHieuLuc ";
            string strFromClause = " from VBPQ_VanBanChiTiet vb inner join VBPQ_CoQuanBH cq on vb.CQBH = cq.Id ";
            string strCreteria = " where vb.DaDuyet = 1 and LinhVuc=" + linhvuc.ToString();
            string strOrder = " order by NgayCapNhat desc";

            strSQL = strSQL + strFromClause + strCreteria + strOrder;

            //dung ADO.NET thay cho LINQ vi han che trong viec select du lieu tuy bien
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SoKHCNCT"].ConnectionString);
            con.Open();
            //dem tat ca record
            string strCount = "select count(vb.Id)  ";
            int totalRecord = Convert.ToInt32(new SqlCommand(strCount + strFromClause + strCreteria, con).ExecuteScalar());

            SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
            DataTable lstSTA = new DataTable();
            da.Fill(lstSTA);

            rptVBPQ.DataSource = lstSTA;
            rptVBPQ.DataBind();

            con.Close();
            con.Dispose();

            con.Close();


            //tao chuoi phan trang
            ltrPaging.Text = NT.Lib.Utilities.GenratePagingString(totalRecord, pageSize, CurrPage, pageShow, "Trang: ", "<span style=\'color:#000\'>{0}</span>", "<span style=\'color:#aaa\'>{0}</span>", "javascript:gotoPage({0})");
        }

        private void SearchVBByCQBH(int cqbh)
        {
            string strSQL = "select vb.Id, SoHieu, TrichYeu, FileDinhKem, LoaiFile, CQBH, NgayBH, NgayHL, TenCoQuanBH, ConHieuLuc ";
            string strFromClause = " from VBPQ_VanBanChiTiet vb inner join VBPQ_CoQuanBH cq on vb.CQBH = cq.Id ";
            string strCreteria = " where vb.DaDuyet = 1 and CQBH=" + cqbh.ToString();
            string strOrder = " order by NgayCapNhat desc";

            strSQL = strSQL + strFromClause + strCreteria + strOrder;

            //dung ADO.NET thay cho LINQ vi han che trong viec select du lieu tuy bien
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SoKHCNCT"].ConnectionString);
            con.Open();
            //dem tat ca record
            string strCount = "select count(vb.Id)  ";
            int totalRecord = Convert.ToInt32(new SqlCommand(strCount + strFromClause + strCreteria, con).ExecuteScalar());

            SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
            DataTable lstSTA = new DataTable();
            da.Fill(lstSTA);

            rptVBPQ.DataSource = lstSTA;
            rptVBPQ.DataBind();

            con.Close();
            con.Dispose();

            con.Close();


            //tao chuoi phan trang
            ltrPaging.Text = NT.Lib.Utilities.GenratePagingString(totalRecord, pageSize, CurrPage, pageShow, "Trang: ", "<span style=\'color:#000\'>{0}</span>", "<span style=\'color:#aaa\'>{0}</span>", "javascript:gotoPage({0})");
        }

        private void SearchVB()
        {
            PreventSQLInjection.ClearSQLInjectionInAllControls(this.divSearchCreteria.Controls);

            string strSQL = "select vb.Id, SoHieu, TrichYeu, FileDinhKem, LoaiFile, CQBH, NgayBH, NgayHL, TenCoQuanBH, ConHieuLuc ";
            string strFromClause = " from VBPQ_VanBanChiTiet vb inner join VBPQ_CoQuanBH cq on vb.CQBH = cq.Id ";
            string strCreteria = " where vb.DaDuyet = 1 ";
            string strOrder = " order by NgayCapNhat desc";

            if (txtSoHieu.Text != "") strCreteria += " and SoHieu like N'%" + txtSoHieu.Text + "%'";
            if (txtTrichYeu.Text != "") strCreteria += " and TrichYeu like N'%" + txtTrichYeu.Text.Replace(" ", "%") + "%'";
            if (rdbConHL.Checked) strCreteria += " and ConHieuLuc = 1";
            if (rdbHetHL.Checked) strCreteria += " and ConHieuLuc = 0";

            if (ddlCQBH.SelectedIndex > 0) strCreteria += " and CQBH = " + ddlCQBH.SelectedValue;
            if (ddlLinhVuc.SelectedIndex > 0) strCreteria += " and CQBH = " + ddlLinhVuc.SelectedValue;
            if (ddlLoaiVB.SelectedIndex > 0) strCreteria += " and CQBH = " + ddlLoaiVB.SelectedValue;
            if (rdpFromDate.SelectedDate != null) strCreteria += " and NgayBH >= '" + rdpFromDate.SelectedDate.Value.ToString() + "'";
            if (rdpToDate.SelectedDate != null) strCreteria += " and NgayBH <= '" + rdpToDate.SelectedDate.Value.ToString() + "'";

            strSQL = strSQL + strFromClause + strCreteria + strOrder;

            //dung ADO.NET thay cho LINQ vi han che trong viec select du lieu tuy bien
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SoKHCNCT"].ConnectionString);
            con.Open();
            //dem tat ca record
            string strCount = "select count(vb.Id)  ";
            int totalRecord = Convert.ToInt32(new SqlCommand(strCount + strFromClause + strCreteria, con).ExecuteScalar());

            SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
            DataTable lstSTA = new DataTable();
            da.Fill(lstSTA);

            rptVBPQ.DataSource = lstSTA;
            rptVBPQ.DataBind();

            con.Close();
            con.Dispose(); 

            con.Close();
            
            
            //tao chuoi phan trang
            ltrPaging.Text = NT.Lib.Utilities.GenratePagingString(totalRecord, pageSize, CurrPage, pageShow, "Trang: ", "<span style=\'color:#000\'>{0}</span>", "<span style=\'color:#aaa\'>{0}</span>", "javascript:gotoPage({0})");
        }

        private void LoadRelatedVB(int cq)
        {
            IList lsVBPQ = (from vb in db.VBPQ_VanBanChiTiets
                                               where vb.DaDuyet.HasValue && vb.DaDuyet == true && vb.CQBH == cq
                            select new
                            {
                                vb.Id,
                                vb.NgayBH,
                                vb.NgayHL,
                                vb.SoHieu,
                                vb.TrichYeu,
                                vb.FileDinhKem,
                                TenCoQuanBH = vb.VBPQ_CoQuanBH.TenCoQuanBH
                            }
                        ).ToList();

            rptRelatedVB.DataSource = lsVBPQ;
            rptRelatedVB.DataBind();
        }

        private void ShowPanel(VBPQ_View vBPQ_View)
        {
            if (vBPQ_View == VBPQ_View.Detail) divDetail.Visible = true;
            else divDetail.Visible = false;
            divGeneral.Visible = !divDetail.Visible;
        }

        private void BindInfo(VBPQ_VanBanChiTiet vb)
        {
            lblCQBH.Text = vb.VBPQ_CoQuanBH.TenCoQuanBH;
            lblLinhVuc.Text = vb.VBPQ_LinhVuc.TenLinhVuc;
            lblLoaiVB.Text = vb.VBPQ_LoaiVB.LoaiVB;
            lblNgayBH.Text = vb.NgayBH.Value.ToString("dd/MM/yyyy");
            lblNgayHL.Text = vb.NgayHL.Value.ToString("dd/MM/yyyy");
            lblSoHieu.Text = vb.SoHieu;
            lblTrichYeu.Text = vb.TrichYeu;
            hplFileDownload.NavigateUrl = vb.FileDinhKem;
        }

        private void LoadCQBH()
        {
            IList lstCQBH = (from cq in db.VBPQ_CoQuanBHs
                             orderby cq.TenCoQuanBH
                             select new
                             {
                                 cq.Id,
                                 cq.TenCoQuanBH,
                                 SoLuongVB = (cq.VBPQ_VanBanChiTiets.Where<VBPQ_VanBanChiTiet>(vb => vb.DaDuyet.HasValue && vb.DaDuyet == true).Count())
                             }).ToList();
            rptCQBH.DataSource = lstCQBH;
            rptCQBH.DataBind();

            ddlCQBH.DataSource = lstCQBH;
            ddlCQBH.DataTextField = "TenCoQuanBH";
            ddlCQBH.DataValueField = "Id";
            ddlCQBH.DataBind();
            ddlCQBH.Items.Insert(0, new ListItem("-- Tất cả -- ", ""));
        }

        private void LoadLoaiVB()
        {
            IList lstCQBH = (from cq in db.VBPQ_LoaiVBs
                             orderby cq.LoaiVB
                             select new
                             {
                                 cq.Id,
                                 cq.LoaiVB
                             }).ToList();
            
            ddlLoaiVB.DataSource = lstCQBH;
            ddlLoaiVB.DataTextField = "LoaiVB";
            ddlLoaiVB.DataValueField = "Id";
            ddlLoaiVB.DataBind();
            ddlLoaiVB.Items.Insert(0, new ListItem("-- Tất cả -- ", ""));
        }

        private void LoadLinhVuc()
        {
            IList lstCQBH = (from cq in db.VBPQ_LinhVucs
                             orderby cq.TenLinhVuc
                             select new
                             {
                                 cq.Id,
                                 cq.TenLinhVuc,
                                 SoLuongVB = (cq.VBPQ_VanBanChiTiets.Where<VBPQ_VanBanChiTiet>(vb => vb.DaDuyet.HasValue && vb.DaDuyet == true).Count())
                             }).ToList();
            rptLinhVuc.DataSource = lstCQBH;
            rptLinhVuc.DataBind();

            ddlLinhVuc.DataSource = lstCQBH;
            ddlLinhVuc.DataTextField = "TenLinhVuc";
            ddlLinhVuc.DataValueField = "Id";
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem("-- Tất cả -- ", ""));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchVB();
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            SearchVB();
        }
    }
}