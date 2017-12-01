<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="foot_categories.ascx.cs"
    Inherits="KHCNCT.Web.SiteEntities.Modules.foot_categories" %>
<asp:Repeater ID="rptCategories" runat="server">
    <ItemTemplate>
        <div class="col-lg-3 col-sm-4 col-xs-6">
            <div class="seventh-title"><a href='<%# KHCNCT.Globals.Common.GenerateUrl(Convert.ToInt32(Eval("PageToRedirect")), "cid=" + Eval("Id").ToString()) %>'><%# Eval("CategoryName") %></a></div>
            <ul class="show-foot">
                <asp:Repeater id="rptSubCategories" runat="server" DataSource='<%# GetCategories(Convert.ToInt32(Eval("Id"))) %>'>
                    <ItemTemplate>
                        <li><a href='<%# KHCNCT.Globals.Common.GenerateUrl(Convert.ToInt32(Eval("PageToRedirect")), "cid=" + Eval("Id").ToString()) %>'><%# Eval("CategoryName") %></a></li>    
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </ItemTemplate>
</asp:Repeater>
