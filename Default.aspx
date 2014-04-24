<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Utilities"%>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Ext.Net.Examples" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.ResourceManager1.DirectEventUrl = this.Request.Url.AbsoluteUri;

            Theme theme = Ext.Net.Theme.Gray;

            if (this.Session["Ext.Net.Theme"] != null)
            {
                theme = (Theme)this.Session["Ext.Net.Theme"];
            }
                
            GrayThemeItem.Checked = false;
            ((Ext.Net.CheckMenuItem)this.FindControl(theme.ToString() + "ThemeItem")).Checked = true;

            this.TriggerField1.Focus();
            this.CheckMenuItemScriptMode.Checked = Convert.ToBoolean(this.Session["Ext.Net.SourceFormatting"]);

            //ResourceManager.RegisterControlResources<TagLabel>();

            if (this.Request.QueryString["clearExamplesCache"] != null)
            {
                this.Page.Cache.Remove("ExamplesTreeNodes");
            }
        }
    }

    protected void GetExamplesNodes(object sender, NodeLoadEventArgs e)
	{
		if (e.NodeID == "Root")
		{
            Ext.Net.NodeCollection nodes = this.Page.Cache["ExamplesTreeNodes"] as Ext.Net.NodeCollection;

            if (nodes == null)
            {
                nodes = UIHelpers.BuildTreeNodes(false);
                this.Page.Cache.Add("ExamplesTreeNodes", nodes, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }

            e.Nodes = nodes;
		}
	}

	[DirectMethod]
	public string GetThemeUrl(string theme, bool reload)
	{
		Theme temp = (Theme)Enum.Parse(typeof(Theme), theme);

		this.Session["Ext.Net.Theme"] = temp;

        if (reload)
        {
            //X.Reload();
            return "";
        }

        return this.ResourceManager1.GetThemeUrl(temp);
	}
	
	[DirectMethod]
	public static int GetHashCode(string s)
	{
		return Math.Abs("/Examples".ConcatWith(s).ToLower().GetHashCode());
	}

    [DirectMethod]
    public void ChangeScriptMode(bool debug)
    {
        if (debug)
        {
            this.Session["Ext.Net.ScriptMode"] = Ext.Net.ScriptMode.Debug;
            this.Session["Ext.Net.SourceFormatting"] = true;            
        }
        else
        {
            this.Session["Ext.Net.ScriptMode"] = Ext.Net.ScriptMode.Release;
            this.Session["Ext.Net.SourceFormatting"] = false;
        }

        this.Response.Redirect("");
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
	<title>Mikrobar</title>
	<link rel="stylesheet" href="resources/css/main.css" />
	<script src="resources/js/main.js"></script>
    <script src="resources/js/enums.js"></script>
</head>
<body>
	<ext:ResourceManager ID="ResourceManager1" runat="server" />
	
    <ext:History runat="server">
		<Listeners>
			<Change Fn="change" />
		</Listeners>
	</ext:History>

	<ext:Viewport runat="server" Layout="BorderLayout">
		<Items>
			<ext:Panel 
				runat="server" 
				Header="false"
				Region="North"
				Border="false"
				Height="90">
				<Content>
					<!--div class="sup-header">
						<div class="notification small">
							<a href="http://ext.net/store/">
							<strong class="text-danger">SALE</strong>
							Big discounts on all Ext.NET Pro Bundles. Ends --- --.
							</a>
						</div>
					</div-->
					<div class="header">
						<div class="left">
							<div class="title">
								Mikrobar Depo Yonetim Sistemi <span class="title-sub">(2.0)</span>
							</div>
							<div class="badge">
								Barset Bilgi Sistemleri
							</div>
						</div>
						<div class="right">
                            <div class="button-group">							    
							    <a class="button" href="http://examples1.ext.net"><span class="f-icon-cogs"></span> Mobil Versiyon</a>
							    <a class="button" href="Default.aspx"><span class="f-icon-home"></span> Home</a>
                            </div>
						</div>
					</div>
				</Content>    
			</ext:Panel>
			<ext:Panel 
				runat="server"
				Region="West" 
				Layout="Fit" 
				Width="240" 
				Header="false"
				Collapsible="true" 
				Split="true" 
				CollapseMode="Mini" 
				Margins="0 0 4 4"
				Border="false">
				<Items>
					<ext:TreePanel 
						ID="exampleTree" 
						runat="server" 
						Header="false"
						AutoScroll="true" 
						Lines="false"
						UseArrows="true"
						CollapseFirst="false"
						RootVisible="false"
                        Animate="false"
                        HideHeaders="true">
						<TopBar>
							<ext:Toolbar runat="server">
								<Items>
									<ext:TriggerField 
										ID="TriggerField1" 
										runat="server" 
										EnableKeyEvents="true" 
										Flex="1"
										EmptyText="Filter Examples...">
										<Triggers>
											<ext:FieldTrigger Icon="Clear" HideTrigger="true" />
										</Triggers>
										<Listeners>
											<KeyUp Fn="keyUp" Buffer="500" />
											<TriggerClick Fn="clearFilter" />
											<SpecialKey Fn="filterSpecialKey" Delay="1" />
										</Listeners>
									</ext:TriggerField>
											
									<ext:Button runat="server" Icon="Cog" ToolTip="Options">
										<Menu>
											<ext:Menu runat="server">
												<Items>
													<ext:MenuItem runat="server" Text="Expand All" IconCls="icon-expand-all">
														<Listeners>
															<Click Handler="#{exampleTree}.expandAll(false);" />
														</Listeners>
													</ext:MenuItem>
															
													<ext:MenuItem runat="server" Text="Collapse All" IconCls="icon-collapse-all">
														<Listeners>
															<Click Handler="#{exampleTree}.collapseAll(false);" />
														</Listeners>
													</ext:MenuItem>

													<ext:MenuSeparator runat="server" />

													<ext:CheckMenuItem runat="server" Text="NEW Only">
														<Listeners>
															<CheckChange Fn="filterNewExamples" />
														</Listeners>
													</ext:CheckMenuItem>

                                                    <ext:CheckMenuItem
                                                        ID="CheckMenuItemScriptMode"
                                                        runat="server" 
                                                        Text="Debug Mode">
	                                                    <Listeners>
		                                                    <CheckChange Handler="App.direct.ChangeScriptMode(checked);" />
	                                                    </Listeners>
                                                    </ext:CheckMenuItem>

													<ext:MenuSeparator runat="server" />

													<ext:MenuItem runat="server" Text="Theme" Icon="Paintcan">
														<Menu>
															<ext:Menu runat="server">
																<Items>
																	<ext:CheckMenuItem ID="DefaultThemeItem" runat="server" Text="Default" Group="theme" />
																	<ext:CheckMenuItem ID="GrayThemeItem" runat="server" Text="Gray" Group="theme" Checked="true" />
																	<ext:CheckMenuItem ID="AccessThemeItem" runat="server" Text="Access" Group="theme" />
                                                                    <ext:CheckMenuItem ID="NeptuneThemeItem" runat="server" Text="Neptune" Group="theme" />
																</Items>
																<Listeners>
																	<Click Fn="themeChange" />
																</Listeners>
															</ext:Menu>
														</Menu>
													</ext:MenuItem>

                                                    <ext:MenuSeparator runat="server" />

                                                    <ext:MenuItem runat="server" Text="Search by" Icon="Find">
                                                        <Menu>
                                                            <ext:Menu runat="server">
                                                                <Items>
                                                                    <ext:CheckMenuItem 
                                                                        ID="SearchByTitles" 
                                                                        runat="server" 
                                                                        Checked="true" 
                                                                        Text="Titles" />

                                                                    <ext:CheckMenuItem 
                                                                        ID="SearchByTags" 
                                                                        runat="server" 
                                                                        Checked="true" 
                                                                        Text="Tags" />
                                                                </Items>
                                                            </ext:Menu>
                                                        </Menu>
                                                    </ext:MenuItem>

                                                    <ext:MenuItem runat="server" Text="Tag Cloud" Icon="WeatherClouds">
                                                        <Listeners>
                                                            <Click Fn="showTagCloud" />
                                                        </Listeners>
                                                    </ext:MenuItem>
												</Items>
											</ext:Menu>
										</Menu>
									</ext:Button>
								</Items>
							</ext:Toolbar>
						</TopBar>
						
						<Store>
							<ext:TreeStore runat="server" OnReadData="GetExamplesNodes">
								<Proxy>
									<ext:PageProxy>
										<RequestConfig Method="GET" Type="Load" />
									</ext:PageProxy>
								</Proxy>
								<Root>
									<ext:Node NodeID="Root" Expanded="true" />
								</Root>
                                <Fields>
                                    <ext:ModelField Name="tags" />
                                    <ext:ModelField Name="isNew" />
                                </Fields>
							</ext:TreeStore>
						</Store>
                        <ColumnModel>
                            <Columns>
                                <ext:TreeColumn runat="server" DataIndex="text" Flex="1">
                                    <Renderer Fn="treeRenderer" />
                                </ext:TreeColumn>
                            </Columns>
                        </ColumnModel>
						<Listeners>
							<ItemClick Handler="onTreeItemClick(record, e);" />
							<AfterRender Fn="onTreeAfterRender" />
						</Listeners>
					</ext:TreePanel>
				</Items>
			</ext:Panel>
			<ext:TabPanel 
				ID="ExampleTabs" 
				runat="server" 
				Region="Center" 
				Margins="0 4 4 0" 
				Cls="tabs"
				MinTabWidth="115">
				<Items>
					<ext:Panel 
						ID="tabHome" 
						runat="server" 
						Title="Home"
						HideMode="Offsets"
						Icon="Application">
						<Loader runat="server" Mode="Frame" Url="Home/">
							<LoadMask ShowMask="true" />
						</Loader>
					</ext:Panel>
				</Items>
				<Listeners>
					<TabChange Fn="addToken" />
				</Listeners>
				<Plugins>
					<ext:TabCloseMenu runat="server" />
				</Plugins>
			</ext:TabPanel>
		</Items>
	</ext:Viewport>
	
	<ext:Window 
		ID="LinkWindow" 
		runat="server"
		Modal="true"
		Hidden="true"
		Icon="Link"
		Layout="absolute"
		Width="400"
		Height="110"
		Title="Direct Link"
		Closable="false"
		Resizable="false">
		<Items>
			<ext:TextField 
				ID="exampleLink" 
				runat="server"
				Width="364"
				Cls="dlText"
				X="10"
				Y="10"
				SelectOnFocus="true"
				ReadOnly="true"
				/>
		</Items>
		<Listeners>
			<Show Handler="exampleLink.setValue(this.exampleName);" />
		</Listeners>
		<Buttons>
			<ext:Button 
				runat="server"
				Text=" Open"
				Icon="ApplicationDouble">
				<Listeners>
					<Click Handler="window.open(LinkWindow.exampleName);" />
				</Listeners>
				<ToolTips>
					<ext:ToolTip runat="server" Title="Open Example in the separate window" />
				</ToolTips>
			</ext:Button>
			<ext:Button 
				runat="server"
				Text=" Open (Direct)"
				Icon="ApplicationGo">
				<Listeners>
					<Click Handler="window.open(LinkWindow.exampleUrl, '_blank');" />
				</Listeners>
				<ToolTips>
					<ext:ToolTip runat="server" Title="Open Example in the separate window using a direct link" />
				</ToolTips>
			</ext:Button>
			<ext:Button runat="server" Text="Close">
				<Listeners>
					<Click Handler="this.findParentByType('window').hide(null);" />
				</Listeners>
			</ext:Button>
		</Buttons>
	</ext:Window>
	
	<script>
        (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
        (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
        m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
        })(window,document,'script','//www.google-analytics.com/analytics.js','ga');
 
        ga('create', 'UA-19135912-7', 'ext.net');
        ga('send', 'pageview');
	</script>
</body>
</html>