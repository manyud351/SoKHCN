<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EStore.admin.Default" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/admin/controls/main.ascx" tagname="main" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trang quan tri</title>
    <script type="text/javascript" src="/Scripts/jquery.1.6.2.js"></script>
    <script type="text/javascript" src="/Scripts/highslide/highslide-with-html.js"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/highslide/highslide.css" />
    <link rel="stylesheet" type="text/css" href="/admin/css/tabs.css" />
    <link rel="stylesheet" type="text/css" href="/admin/css/cd-tabs.css" />
    <script src="/Scripts/tabs.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/cd-tab.js"></script>
    <!--[if lt IE 7]>
        <link rel="stylesheet" type="text/css" href="/Scripts/highslide/highslide-ie6.css" />
    <![endif]-->
    <style type="text/css">
        body
        {
            font-family: Verdana, Tahoma;
            font-size: 8pt;	
        }
        input
        {
        	font-size: 8pt;
        }
        
        .admin-desc
        {
            color: #555;
            font-weight: normal !important;	
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>

    <uc1:main ID="main1" runat="server" />
    
    </form>
</body>
</html>
