<%@ Page Language="C#" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<script runat="server">
    protected void ReloadData(object sender, DirectEventArgs e)
    {
        this.Chart1.GetStore().DataBind();
        this.Chart2.GetStore().DataBind();
        this.Chart3.GetStore().DataBind();
    }
</script>    

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Gauge Chart - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />

     <script>
        var saveChart = function saveChart (chart) {
            Ext.MessageBox.confirm('Confirm Download', 'Would you like to download the chart as an image?', function (choice) {
                if (choice == 'yes') {
                    chart.save({
                        type: 'image/png'
                    });
                }
            });
        };
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Gauge</h1>

        <p>Displaying three custom gauge charts bound to different data stores with different configuration options and easings.</p>
        
        <p>Click on <b>Reload Data</b> to update the information.</p>

        <ext:Panel 
            runat="server"
            Title="Gauge Charts"
            Width="800"
            Height="250">
            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button 
                            runat="server" 
                            Text="Reload Data" 
                            Icon="ArrowRefresh" 
                            OnDirectClick="ReloadData" 
                            />

                        <ext:Button 
                            runat="server" 
                            Text="Save 1st Chart" 
                            Icon="Disk"
                            Handler="saveChart(#{Chart1});"
                            />

                        <ext:Button 
                            runat="server" 
                            Text="Save 2nd Chart" 
                            Icon="Disk"
                            Handler="saveChart(#{Chart2});"
                            />

                        <ext:Button 
                            runat="server" 
                            Text="Save 3rd Chart" 
                            Icon="Disk"
                            Handler="saveChart(#{Chart3});"
                            />
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <ext:Chart 
                    ID="Chart1" 
                    runat="server"
                    StyleSpec="background:#fff;"
                    InsetPadding="25"
                    Flex="1">
                    <AnimateConfig Easing="ElasticIn" Duration="1000" />
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
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Axes>
                        <ext:GaugeAxis Minimum="0" Maximum="100" Steps="10" Margin="-10" />
                    </Axes>
                    <Series>
                        <ext:GaugeSeries AngleField="Data1" Donut="0" ColorSet="#F49D10,#ddd" />
                    </Series>
                </ext:Chart>

                <ext:Chart 
                    ID="Chart2" 
                    runat="server"
                    Animate="true"
                    StyleSpec="background:#fff;"
                    InsetPadding="25"
                    Flex="1">
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
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Axes>
                        <ext:GaugeAxis Minimum="0" Maximum="100" Steps="10" Margin="7" />
                    </Axes>
                    <Series>
                        <ext:GaugeSeries AngleField="Data1" Donut="30" ColorSet="#82B525,#ddd" />
                    </Series>
                </ext:Chart>

                <ext:Chart 
                    ID="Chart3" 
                    runat="server"
                    StyleSpec="background:#fff;"
                    InsetPadding="25"
                    Flex="1">
                    <AnimateConfig Easing="BounceOut" Duration="500" />
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
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Axes>
                        <ext:GaugeAxis Minimum="0" Maximum="100" Steps="10" Margin="7" />
                    </Axes>
                    <Series>
                        <ext:GaugeSeries AngleField="Data1" Donut="80" ColorSet="#3AA8CB,#ddd" />
                    </Series>
                </ext:Chart>
            </Items>
        </ext:Panel>
    </form>    
</body>
</html>