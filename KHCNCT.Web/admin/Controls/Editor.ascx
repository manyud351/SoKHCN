<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Editor.ascx.cs" Inherits="KHCNCT.Admin.Controls.Editor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Namespace="CKEditor.NET" Assembly="CKEditor.NET" TagPrefix="CKEditor" %> 
<%@ Register Namespace="CKFinder" Assembly="CKFinder" TagPrefix="CKFinder" %>

<%--<telerik:RadEditor ID="MainEditor" runat="server" ContentFilters="DefaultFilters,MakeUrlsAbsolute"
    onfileupload="MainEditor_FileUpload" onfiledelete="MainEditor_FileDelete">
</telerik:RadEditor>--%>
<CKEditor:CKEditorControl ID="MainEditor" Height="500" runat="server"></CKEditor:CKEditorControl>
<CKFinder:FileBrowser ID="FileBrowser1"   Width="0" Height="0" runat="server" OnLoad="FileBrowser1_Load"></CKFinder:FileBrowser> 