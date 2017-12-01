<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="main.ascx.cs" Inherits="KHCNCT.Admin.Controls.main" %>

<%@ Register src="main_menu.ascx" tagname="main_menu" tagprefix="uc1" %>

<style type="text/css">
    .banner
    {
        height: 130px;
        font-weight: bold;
        color: #FFFFFF;
        background-size: 100% 100%;
        background-repeat: no-repeat;
        background-image: url('assets/images/first-bg.jpg')
    }
    
     .logo
     {
        margin-left: 50px;   
     }
     
     .main_menu
     {
            background-color: #1586d1;border-color: #e7e7e7; 
     }
     
     .RadMenu_Black .rmRootGroup {
        border: none !important;
        background-repeat: repeat-x;
        background-color: #1586d1 !important;
        background-image: none !important;
    }
    
    .RadMenu_Black .rmGroup, .RadMenu_Black .rmGroup .rmVertical
    {
        background: #1586d1 !important;
        background-image: none !important;
    }
    
    .RadMenu_Black .rmRootGroup, .RadMenu_Black .rmLink, .RadMenu_Black .rmText, .RadMenu_Black .rmLeftArrow, .RadMenu_Black .rmRightArrow, .RadMenu_Black .rmTopArrow, .RadMenu_Black .rmBottomArrow
    {
        background-image: none !important;
    }
     .RadMenu .rmLink:hover
     {
        background-color: #fff;
        color:#1586d1 !important;
     }
     
    .style2
    {
        height: 128px;
    }
    .style3
    {
        height: 350px;
    }
    .style4
    {
        height: 35px;
    }
    .righ-top-panel
    {
        float: right; margin-right: 10px; width: 200px; text-align: right;
    }
    
    .righ-top-panel a
    {
    	text-decoration: none;
    	color: #fff;
    }
    
    .righ-top-panel a:hover
    {
    	text-decoration: underline;
    }
    
</style>


<table style="width:100%;" cellpadding="0" cellspacing="0">
    <tr>
        <td class="banner">
            <div class="logo">
                <img src="assets/images/LogoSoKHCN.png" height="120px" />
            </div>
&nbsp;&nbsp;&nbsp; KHOA HỌC VÀ CÔNG NGHỆ <br/>
&nbsp;&nbsp;&nbsp; THÀNH PHỐ CẦN THƠ
        </td>
    </tr>
    <tr>
        <td class="style4" style="background-color: #1586d1;border-color: #e7e7e7;">
            <uc1:main_menu ID="main_menu1" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="style3" valign="top" style="background-image: url('../../images/background.png'); background-repeat: repeat-x">
        <br />
        <br />
        <br />
<div id="ContentPane" runat="server">
    
</div>
        </td>
    </tr>
    <tr>
        <td class="style2" style="background-image: url('../../images/footer.png')">
        </td>
    </tr>
</table>
