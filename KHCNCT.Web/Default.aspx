<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KHCNCT.Web.Default" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="SiteEntities/Controls/SiteWrapper.ascx" tagname="SiteWrapper" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
		 <meta name="viewport" content="width=device-width, initial-scale=1">
		 <meta name="description" content="Sở Khoa học và Công nghệ thành phố Cần Thơ">
		 <meta name="keyword" content="sokhcn, skhcn, so khoa hoc va cong nghe, khoa hoc cong nghe can tho">
		 <title>Khoa học và Công nghệ thành phố Cần Thơ</title>
		 <link href="/templates/_default/css/sokhcn.css" rel="stylesheet" type="text/css">
         <script type="text/javascript" src="/templates/_default/scripts/jquery-2.1.4.js"></script>
         <script type="text/javascript" src="/assets/scripts/swfobject.js"></script>
         <script type="text/javascript" src="/assets/scripts/jwplayer.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AjaxFrameworkMode="Enabled">
    </telerik:RadScriptManager>
    <div>
    <uc1:SiteWrapper ID="SiteWrapper1" runat="server" />
    
    </div>

    </form>

    <script type="text/javascript" src="/templates/_default/scripts/sokhcn.js"></script>
		<script type="text/javascript">
		    $(window).scroll(function () {
		        if ($(window).scrollTop() == 0) {
		            $('#go_top').stop(false, true).fadeOut(600);
		        } else {
		            $('#go_top').stop(false, true).fadeIn(600);
		        }
		    });
		</script>
</body>
</html>
