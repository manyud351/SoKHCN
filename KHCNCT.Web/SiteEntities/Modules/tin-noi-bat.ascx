<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tin-noi-bat.ascx.cs"
    Inherits="KHCNCT.Web.SiteEntities.Modules.tin_noi_bat" %>
<div class="first-head">
    <ol class="breadcrumb">
        <li><a href="#">Home</a></li>
        <li><a href="#">Library</a></li>
        <li class="active">Data</li>
    </ol>
</div>
<div class="first-content">
    <div class="row">
        <div class="col-lg-8 col-sm-8">
            <asp:Image ID="imgFirstNews" runat="server" Style="width: 100%; max-height: 380px;" />
            <div class="snew-title">
                <asp:HyperLink ID="hplFirstNews" runat="server"> 
                </asp:HyperLink>
            </div>
            <div class="snew-abstract">
                <asp:Literal ID="ltrDescription" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="col-lg-4 col-sm-4">
            <asp:Repeater ID="rptRightColNews" runat="server">
                <ItemTemplate>
                    <div class="col-lg-12 col-sm-12 col-xs-6">
                        <asp:Image ID="imgRightColNews" runat="server" ImageUrl='<%# Eval("ImagePath") %>'
                            CssClass="snew-image-item" />
                        <div class="snew-title">
                            <asp:HyperLink ID="hplRightColNews" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></asp:HyperLink>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
<div class="first-foot">
    <div class="row">
        <asp:Repeater ID="rptFootColNews" runat="server">
            <ItemTemplate>
                <div class="col-lg-4 col-sm-4 col-xs-4">
                    <asp:Image ID="imgFootColNews" runat="server" ImageUrl='<%# Eval("ImagePath") %>'
                            CssClass="snew-image-item" />
                        <div class="snew-title">
                            <asp:HyperLink ID="hplFootColNews" runat="server" NavigateUrl='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></asp:HyperLink>
                        </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
