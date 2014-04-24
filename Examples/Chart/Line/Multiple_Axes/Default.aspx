<%@ Page Language="C#" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<script runat="server">
    private List<object> GenerateData()
    {
        List<object> data = new List<object>();
        Random random = new Random();
        double p = (random.NextDouble() * 11) + 1;
        DateTime date = DateTime.Today;

        for (int i = 0; i < 15; i++)
        {
            data.Add(new
            {
                Date = date.AddDays(i),
                Data1 = Math.Round(random.NextDouble() * 10),
                Data2 = Math.Round(random.NextDouble() * 100)
            });
        }

        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.Chart1.GetStore().DataSource = this.GenerateData();
        }
    }

    protected void ReloadData(object sender, DirectEventArgs e)
    {
        Store store = this.Chart1.GetStore();

        store.DataSource = GenerateData();
        store.DataBind();
    }
</script>    

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Line Chart With Multiple Axes - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />

    <%-- Workaround to get LegendConfig working. Needs to remove after final 4.2.0 release. --%>

    <script>
        Ext.override(Ext.chart.LegendItem, {
            createSeriesMarkers: function(config) {
                var me = this,
                    index = config.yFieldIndex,
                    series = me.series,
                    seriesType = series.type,
                    surface = me.surface,
                    z = me.zIndex;
 
                // Line series - display as short line with optional marker in the middle
                if (seriesType === 'line' || seriesType === 'scatter') {
                    if(seriesType === 'line') {
                        var seriesStyle = Ext.apply(series.seriesStyle, series.style);
                        me.drawLine(0.5, 0.5, 16.5, 0.5, z, seriesStyle, index);
                    };
         
                    if (series.showMarkers || seriesType === 'scatter') {
                        var markerConfig = Ext.apply(series.markerStyle, series.markerConfig || {}, {
                            fill: series.getLegendColor(index)
                        });
                        me.drawMarker(8.5, 0.5, z, markerConfig);
                    }
                }
                // All other series types - display as filled box
                else {
                    me.drawFilledBox(12, 12, z, index);
                }
            },
 
            /**
             * @private Creates line sprite for Line series.
             */
            drawLine: function(fromX, fromY, toX, toY, z, seriesStyle, index) {
                var me = this,
                    surface = me.surface,
                    series = me.series;
     
                return me.add('line', surface.add({
                    type: 'path',
                    path: 'M' + fromX + ',' + fromY + 'L' + toX + ',' + toY,
                    zIndex: (z || 0) + 2,
                    "stroke-width": series.lineWidth,
                    "stroke-linejoin": "round",
                    "stroke-dasharray": series.dash,
                    stroke: seriesStyle.stroke || series.getLegendColor(index) || '#000',
                    style: {
                        cursor: 'pointer'
                    }
                }));
            }
 
        });
    </script>

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

        <h1>Line Chart With Multiple Axes</h1>

	    <p>Display 2 sets of random data in the two line series which are referring different axes (Y). Reload data will randomly generate a new set of data in the store. Click on the legend items to remove them from the chart.</p>

        <ext:Panel 
            runat="server"
            Title="Line Chart"
            Width="800"
            Height="400"
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
                    Animate="true">
                    <Store>
                        <ext:Store runat="server">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Date" Type="Date" />
                                        <ext:ModelField Name="Data1" />
                                        <ext:ModelField Name="Data2" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Axes>
                        <ext:TimeAxis 
                            Title="Date" 
                            Fields="Date" 
                            Position="Bottom" 
                            DateFormat="MMM dd"
                            Constrain="true"
                            FromDate="<%# DateTime.Today %>"
                            ToDate="<%# DateTime.Today.AddDays(14) %>"
                            AutoDataBind="true" />

                        <ext:NumericAxis 
                            Title="Data (blue)" 
                            Fields="Data1" 
                            Position="Left" 
                            Maximum="10">
                            <LabelTitle Fill="#115fa6" />
                            <Label Fill="#115fa6" />
                        </ext:NumericAxis>

                        <ext:NumericAxis 
                            Title="Data (green)"
                            Fields="Data2" 
                            Position="Right" 
                            Maximum="100">
                            <LabelTitle Fill="#94ae0a" />
                            <Label Fill="#94ae0a" />
                        </ext:NumericAxis>
                    </Axes>
                    <Series>
                        <ext:LineSeries 
                            Titles="Blue Line" 
                            XField="Date" 
                            YField="Data1" 
                            Axis="Left" 
                            Smooth="3">
                            <HighlightConfig Size="7" Radius="7" />
                            <MarkerConfig Size="4" Radius="4" StrokeWidth="0" />
                        </ext:LineSeries>

                        <ext:LineSeries 
                            Titles="Green Line" 
                            XField="Date" 
                            YField="Data2" 
                            Axis="Right" 
                            Smooth="3">
                            <HighlightConfig Size="7" Radius="7" />
                            <MarkerConfig Size="4" Radius="4" StrokeWidth="0" />
                        </ext:LineSeries>
                    </Series>
                    <Plugins>
                        <ext:VerticalMarker runat="server">
                            <XLabelRenderer Handler="return Ext.util.Format.date(value, 'M d');" />
                        </ext:VerticalMarker>
                    </Plugins>
                    <LegendConfig Position="Bottom" />
                </ext:Chart>
            </Items>
        </ext:Panel>
    </form>    
</body>
</html>