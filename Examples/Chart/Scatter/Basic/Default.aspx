<%@ Page Language="C#" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<script runat="server">
    protected void ReloadData(object sender, DirectEventArgs e)
    {
        this.Chart1.GetStore().DataBind();
    }
</script>    

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Scatter Chart Basic - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />

    <script>
        var saveChart = function() {
            Ext.MessageBox.confirm('Confirm Download', 'Would you like to download the chart as an image?', function (choice) {
                if(choice == 'yes'){
                    this.up("window").down("chart").save({
                        type: 'image/png'
                    });
                }
            }, this);
        };
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Scatter Basic Example</h1>

	    <div style="margin: 10px;">
	       <p>
	        Display 3 sets of random data in scatter series. Reload data will randomly generate a new set of data in the store.
	       </p>
	    </div>

        <ext:Panel 
            runat="server"
            Width="800"
            Height="600"
            Title="Scatter Chart"
            Layout="FitLayout">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" Text="Reload Data" Icon="ArrowRefresh" >
                            <DirectEvents>
                                <Click OnEvent="ReloadData" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button runat="server" Text="Save Chart" Handler="saveChart" Icon="Disk" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <ext:Chart 
                    ID="Chart1" 
                    runat="server" 
                    StyleSpec="background:#fff;"
                    Animate="true"
                    Theme="Category2"
                    InsetPadding="50">
                    <Store>
                         <ext:Store 
                            runat="server" 
                            Data="<%# Ext.Net.Examples.ChartData.GenerateData() %>" 
                            AutoDataBind="true">                           
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Name" />
                                        <ext:ModelField Name="Data1" />
                                        <ext:ModelField Name="Data2" />
                                        <ext:ModelField Name="Data3" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Axes>
                        <ext:NumericAxis Fields="Data1,Data2,Data3" Title="Number of Hits">
                            <GridConfig>
                                <Odd Opacity="1" Fill="#ddd" Stroke="#bbb" StrokeWidth="0.5" />
                            </GridConfig>
                        </ext:NumericAxis>                            

                        <ext:CategoryAxis Position="Bottom" Fields="Name" Title="Month of the Year" />                            
                    </Axes>
                    <Series>
                        <ext:ScatterSeries XField="Name" YField="Data1">
                            <MarkerConfig Type="Circle" Radius="7" Size="7" />
                            <HighlightConfig Size="10" Radius="10" />
                            <Tips TrackMouse="true" Width="100" Height="28">
                                <Renderer Handler="this.setTitle(storeItem.get('Name') + ': ' + storeItem.get('Data1'));" />
                            </Tips>
                        </ext:ScatterSeries>
                        <ext:ScatterSeries XField="Name" YField="Data2">
                            <MarkerConfig Type="Diamond" Radius="7" Size="7" />
                            <HighlightConfig Size="10" Radius="10" />
                            <Tips runat="server" TrackMouse="true" Width="100" Height="28">
                                <Renderer Handler="this.setTitle(storeItem.get('Name') + ': ' + storeItem.get('Data2'));" />
                            </Tips>
                        </ext:ScatterSeries>
                        <ext:ScatterSeries XField="Name" YField="Data3">
                            <MarkerConfig Type="Square" Radius="7" Size="7" />
                            <HighlightConfig Size="10" Radius="10" />
                            <Tips runat="server" TrackMouse="true" Width="100" Height="28">
                                <Renderer Handler="this.setTitle(storeItem.get('Name') + ': ' + storeItem.get('Data3'));" />
                            </Tips>
                        </ext:ScatterSeries>
                    </Series>
                </ext:Chart>
            </Items>
        </ext:Panel>
    </form>    
</body>
</html>