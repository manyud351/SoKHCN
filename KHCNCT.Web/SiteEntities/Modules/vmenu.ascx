<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="vmenu.ascx.cs" Inherits="KHCNCT.Web.SiteEntities.Modules.vmenu" %>
<!-- menu -->
			<div class="menu">
				<nav class="navbar navbar-default">
					<div class="container-fluid">
					    	<!-- Brand and toggle get grouped for better mobile display -->
						 <div class="navbar-header">
						      	<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
						        		<span class="sr-only">Toggle navigation</span>
						        		<span class="icon-bar"></span>
						        		<span class="icon-bar"></span>
						        		<span class="icon-bar"></span>
						      	</button>
						      	<a class="navbar-brand" href="default.aspx">Trang chủ</a>
						 </div>
                          <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
						    <ul class="nav navbar-nav">
						 <!-- Collect the nav links, forms, and other content for toggling -->
                         <asp:Repeater ID="rptMainMenu" runat="server">
                            <ItemTemplate>
                            
								<!--Giới thiệu-->
								<div class="dropdown clearfix"><a href='<%# Eval("Id") %>' class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><%# Eval("PageDisplayName") %><span class="caret"></span></a>
	   								<ul class="dropdown-menu"  role="menu" aria-labelledby="dropdownMenu">
	 									<div class="row">
										        <div class="col-xs-12 col-md-3 margin-bottom-20px">
                                                    <asp:Repeater ID="rptSubMenuItem" runat="server" DataSource='<%# GetPages(Convert.ToInt32(Eval("Id"))) %>'>
                                                        <ItemTemplate>
                                                            <li><a href='<%# KHCNCT.Globals.Common.GenerateUrl(Convert.ToInt32(Eval("Id")),"cid=" + Eval("CategoryId").ToString()) %>'><%# Eval("PageDisplayName")%></a></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
										        </div>
										        <div class="col-xs-12 col-md-9">
										          		<div class="row">
                                                            <asp:Repeater ID="rptLastedNews" runat="server" DataSource='<%# GetNews(Convert.ToInt32(Eval("CategoryId"))) %>'>
                                                                <ItemTemplate>
                                                                    <div class="col-lg-3 col-sm-3 col-xs-6">
																        <asp:Image ID="imgNews" runat="server" ImageUrl='<%# Eval("ImagePath") %>' CssClass="image-navbar" /> 
																        <div class="snew-title"><a href='<%# KHCNCT.Globals.Common.GenerateUrl(57,"nid=" + Eval("Id").ToString()) %>'><%# Eval("NewsTitle") %></a> </div>
															        </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
														</div>
														<div><br /></div>
										        </div>
							          	</div>
	    							</ul>
    							</div>
					
                            </ItemTemplate>
                         </asp:Repeater>
                         </ul>
						 </div><!-- /.navbar-collapse -->
                    </div><!-- /.container-fluid -->
				</nav>
			</div>
			<!-- /menu -->
