using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NT.Lib;
using Telerik.Web.UI.Editor.DialogControls;
using KHCNCT.Globals.Enums.Role;
using System.EnterpriseServices;
using KHCNCT.Data;
using KHCNCT.Configuration;

namespace KHCNCT.Admin.Controls
{
    public partial class Editor : CommonBaseControl
    {
        private string _ModuleUploadFolder;

        /// <summary>
        /// Get/set thu muc upload file cua Editor
        /// </summary>
        public string ModuleUploadFolder
        {
            get { return _ModuleUploadFolder; }
            set { _ModuleUploadFolder = value;}
        }


        /// <summary>
        /// Do rong cua Editor
        /// </summary>
        public Unit Width
        {
            get { return MainEditor.Width; }
            set { MainEditor.Width = value; }
        }

        /// <summary>
        /// Do cao cua Editor
        /// </summary>
        public Unit Height
        {
            get { return MainEditor.Height; }
            set { MainEditor.Height = value; }
        }


        protected string virtualUploadFolder
        {
            get
            {
                return ModuleUploadFolder;
            }
        }

        private SysUser _UserInfo;
        protected SysUser UserInfo
        {
            get
            {
                if (_UserInfo == null)
                {
                    _UserInfo = db.SysUsers.SingleOrDefault<SysUser>(u => u.Id == UserId);
                }
                return _UserInfo;
            }
        }


        /// <summary>
        /// Get trang thai 
        /// </summary>
        public bool CanUploadMore
        {
            get
            { 
                //neu dung luong upload cua nguoi dung/gian hang hien tai chua dat toi quota thi nguoi dung moi co upload tiep
                //neu ko thi nguoi dung chi co the chon file hoac xoa file dang ton tai tren server
                //if ((CurrentUserRole == Globals.Enums.Role.UserRole.PortalAdmin) || (CurrentUserRole == Globals.Enums.Role.UserRole.PortalOwner))
                {
                    return true;
                }
                //else return false;
            }
        }

        //public void RegisterUploadFolder(string uploadFolder)
        //{
        //    ModuleUploadFolder = uploadFolder;
        //    RegisterUploadFolder();
        //}

        //public void RegisterUploadFolder()
        //{
        //    if (MainEditor != null)
        //    {
        //        if (CanUploadMore)
        //        {
        //            MainEditor.ImageManager.UploadPaths = new string[] { virtualUploadFolder };
        //        }

        //        MainEditor.ImageManager.DeletePaths = new string[] { virtualUploadFolder };
        //        MainEditor.ImageManager.ViewPaths = new string[] { virtualUploadFolder };
        //    }
        //}

        protected void FileBrowser1_Load(object sender, EventArgs e)
        {
            FileBrowser1 = new CKFinder.FileBrowser();
            FileBrowser1.BasePath = "/ckfinder/";
            FileBrowser1.SetupCKEditor(MainEditor);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (CanUploadMore)
            //{
            //    MainEditor.ImageManager.UploadPaths = new string[] { virtualUploadFolder };
            //}

            //MainEditor.ImageManager.DeletePaths = new string[] { virtualUploadFolder };
            //MainEditor.ImageManager.ViewPaths = new string[] { virtualUploadFolder };
        }

        /// <summary>
        /// xu ly su kien khi co file upload. ham nay duoc goi tu cua so FileUploadDialog khi nguoi dung upload file len
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fileName"></param>
        /// <returns>tra ve gia tri true/false de chi nguoi dung co the upload file hay ko</returns>
        public bool MainEditor_FileUpload(object sender, string fileName)
        {
            try
            {
                ImageManagerDialog imgDialog = (ImageManagerDialog)sender; 

                if ((imgDialog.Page.Session["UserId"] == null) || (imgDialog.Page.Session["UserId"].ToString() == "-1") || (imgDialog.Page.Session["Role"] == null))
                {
                    //user chua dang nhap
                    return false;
                }
                else
                {
                    //user khong co quyen upload file
                    UserRole currentRole = (UserRole)Enum.Parse(typeof(UserRole), imgDialog.Page.Session["Role"].ToString());
                    if (currentRole == UserRole.Guest) return false;
                }

                //thu muc upload cua gian hang hien tai
                //string uploadPath = "~/users/" + imgDialog.Page.Session["UserId"].ToString() + "/upload/" + ModuleUploadFolder;
                //System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(Utilities.ServerMapPath(uploadPath));

                //int userId = Convert.ToInt32(imgDialog.Page.Session["UserId"]);
                //SysUser user = db.SysUsers.SingleOrDefault<SysUser>(u => u.Id == userId);
                //long maxQuota;
                //****************************************************
                //can sua lai sau
                //****************************************************
                //if ((user != null)) maxQuota = SystemConfig.ApplicationConfig.DefaultAdminUploadQouta;
                //else maxQuota = SystemConfig.ApplicationConfig.DefaultUserUploadQuota;

                //System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(Utilities.ServerMapPath(virtualUploadFolder));

                //return (Utilities.SizeOfFolder(dirInfo) < maxQuota);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool MainEditor_FileDelete(object sender, string fileName)
        {
            return default(bool);
        }
    }
}