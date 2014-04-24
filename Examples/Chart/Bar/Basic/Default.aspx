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
    <title>Bar Chart - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />    

    <script>
        var saveChart = function (btn) {
            Ext.MessageBox.confirm('Confirm Download', 'Would you like to download the chart as an image?', function (choice) {
                if(choice == 'yes'){
                    btn.up('panel').down('chart').save({
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

        <h1>Bar Chart Sample</h1>
        
        <p>Display a sets of random data in a bar series. Reload data will randomly generate a new set of data in the store.</p>

        <ext:ChartTheme runat="server" ThemeName="CustomBlue">
            <Axis Stroke="#084594" />
            <AxisLabelLeft Fill="rgb(8,69,148)" Font="12px Arial" FontFamily="Arial" />
            <AxisLabelBottom Fill="rgb(8,69,148)" Font="12px Arial" FontFamily="Arial" />
            <AxisTitleLeft Font="bold 18px Arial" />
            <AxisTitleBottom Font="bold 18px Arial" />
        </ext:ChartTheme>

        <ext:Panel 
            runat="server"
            Title="Bar Chart"
            Width="800"
            Height="600"
            Layout="FitLayout">
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
                            Text="Save Chart" 
                            Icon="Disk"
                            Handler="saveChart"
                            />
                    </Items>
                </ext:Toolbar>
            </TopBar>

            <Items>
                <ext:Chart 
                    ID="Chart1" 
                    runat="server"
                    Shadow="true"
                    Theme="CustomBlue"
                    Animate="true">
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

                    <Background>
                        <Gradient GradientID="backgroundGradient" Angle="45">
                            <Stops>
                                <ext:GradientStop Offset="0" Color="#ffffff" />
                                <ext:GradientStop Offset="100" Color="#eaf1f8" />
                            </Stops>
                        </Gradient>
                    </Background>

                    <Axes>
                        <ext:NumericAxis                             
                            Fields="Data1"
                            Position="Bottom"
                            Grid="true"
                            Title="Number of Hits"
                            Minimum="0">
                            <Label>
                                <Renderer Handler="return Ext.util.Format.number(value, '0,0');" />
                            </Label>
                        </ext:NumericAxis>                            

                        <ext:CategoryAxis 
                            Fields="Name"
                            Position="Left"
                            Title="Month of the Year"
                            />                            
                    </Axes>

                    <Series>
                        <ext:BarSeries 
                            Axis="Bottom"
                            Highlight="true" 
                            XField="Name" 
                            YField="Data1">
                            <Tips TrackMouse="true" Width="140" Height="28">
                                <Renderer Handler="this.setTitle(storeItem.get('Name') + ': ' + storeItem.get('Data1') + ' views');" />
                            </Tips>
                            <Label 
                                Display="InsideEnd" 
                                Field="Data1" 
                                Orientation="Horizontal" 
                                Color="#333" 
                                TextAnchor="middle"
                                />
                        </ext:BarSeries>
                    </Series>
                </ext:Chart>
            </Items>
        </ext:Panel>
    </form>    
</body>
</html>
