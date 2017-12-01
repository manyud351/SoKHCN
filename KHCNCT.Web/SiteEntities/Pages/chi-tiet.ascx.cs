using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Web.SiteEntities.Controls;
using KHCNCT.Data;
using KHCNCT.Globals.Enums;

namespace KHCNCT.Web.SiteEntities.Pages
{
    public partial class chi_tiet : PageBaseControl
    {
        private int _OriginId;
        protected int OriginId
        {
            get
            {
                if ((_OriginId == 0) && (Request.QueryString["oid"] != null))
                {
                    int.TryParse(Request.QueryString["oid"], out _OriginId);
                }
                return _OriginId;
            }
        }

        private int _NewsId;
        protected int NewsId
        {
            get
            {
                if ((_NewsId == 0) && (Request.QueryString["nid"] != null))
                {
                    int.TryParse(Request.QueryString["nid"], out _NewsId);
                }
                return _NewsId;
            }
        }

        NewsContent _CurrentNews;
        NewsContent CurrentNews
        {
            get {
                if (_CurrentNews == null)
                {
                    _CurrentNews = db.NewsContents.SingleOrDefault<NewsContent>(nc => nc.Id == NewsId && (nc.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan));
                }
                return _CurrentNews;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) ProcessNews();
        }

        private void ProcessNews()
        {
            _CurrentNews = db.NewsContents.SingleOrDefault<NewsContent>(nc => nc.Id == NewsId && (nc.ApprovementStatus == (short)EnumNewsApprovementStatus.DaXuatBan));
            if (CurrentNews != null)
            {
                if (!String.IsNullOrEmpty(CurrentNews.ImagePath)) imgNews.ImageUrl = CurrentNews.ImagePath;
                else imgNews.Visible = false;
                //ltrNewsAuthor.Text = CurrentNews.SysUser.DisplayName;
                ltrNewsContent.Text = CurrentNews.Content.Replace("src=\"/DesktopModules/", "src=\"http://sokhcn.cantho.gov.vn/DesktopModules/");
                ltrNewsDate.Text = CurrentNews.CreatedTime.Value.ToString("dd/MM/yyyy HH:mm");
                ltrNewsDescription.Text = CurrentNews.Description;
                ltrNewsTitle.Text = CurrentNews.NewsTitle;
                ltrSource.Text = CurrentNews.Source;
                
                if (CurrentNews.NewsFiles.Count > 0)
                {
                    ltrFiles.Visible = true;
                    rptFiles.DataSource = CurrentNews.NewsFiles;
                    rptFiles.DataBind();
                }
                else ltrFiles.Visible = false;

                TinCungChuyenMuc1.CurrentNews = CurrentNews;
                TinCungChuyenMuc1.LoadContents();
            }
            else
            {
                ltrNewsTitle.Text = "Không tìm thấy thông tin";
            }
        }
    }
}