<%@ Page Language="C#" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Xml" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<script runat="server">
    protected void ReloadData(object sender, DirectEventArgs e)
    {
        this.Chart1.GetStore().DataBind();
    }

    [DirectMethod]
    public void ExportTwoSvg(string svg1, string svg2)
    {
        if (string.IsNullOrEmpty(svg1))
        {
            X.Msg.Alert("SVG is empty", "Provided SVG for the first chart is null or empty");
            return;
        }

        if (string.IsNullOrEmpty(svg2))
        {
            X.Msg.Alert("SVG is empty", "Provided SVG for the second chart is null or empty");
            return;
        }

        XmlDocument xd = new XmlDocument();
        xd.XmlResolver = null;
        xd.LoadXml(Server.HtmlDecode(svg1));
        var svgGraph = Svg.SvgDocument.Open(xd);
        Bitmap firstImage = svgGraph.Draw();

        xd = new XmlDocument();
        xd.XmlResolver = null;
        xd.LoadXml(Server.HtmlDecode(svg2));
        svgGraph = Svg.SvgDocument.Open(xd);
        Bitmap secondImage = svgGraph.Draw();

        using (Bitmap result = new Bitmap(firstImage.Width + secondImage.Width, Math.Max(firstImage.Height, secondImage.Height)))
        {
            using (var canvas = Graphics.FromImage(result))
            {
                canvas.Clear(System.Drawing.Color.White);
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                canvas.DrawImage(firstImage, new Rectangle(0, 0, firstImage.Width, firstImage.Height), new Rectangle(0, 0, firstImage.Width, firstImage.Height), GraphicsUnit.Pixel);
                canvas.DrawImage(secondImage, new Rectangle(firstImage.Width, 0, secondImage.Width, secondImage.Height));
                canvas.Save();
            }
            // To save the chart, uncomment the line below
            //result.Save(@"c:\\chart.png", System.Drawing.Imaging.ImageFormat.Png);

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment; filename=chart.png");
            Response.ContentType = "image/png";

            using (MemoryStream ms = new MemoryStream())
            {
                result.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.WriteTo(Response.OutputStream);
            }

            Response.End();
        }
    }

    [DirectMethod]
    public void CombineSvg(string svg1, string svg2)
    {
        if (string.IsNullOrEmpty(svg1))
        {
            X.Msg.Alert("SVG is empty", "Provided SVG for the first chart is null or empty");
            return;
        }

        if (string.IsNullOrEmpty(svg2))
        {
            X.Msg.Alert("SVG is empty", "Provided SVG for the second chart is null or empty");
            return;
        }

        XmlDocument xd = new XmlDocument();
        xd.XmlResolver = null;
        xd.LoadXml(Server.HtmlDecode(svg1));
        var svgGraph = Svg.SvgDocument.Open(xd);
        Bitmap firstImage = svgGraph.Draw();

        xd = new XmlDocument();
        xd.XmlResolver = null;
        xd.LoadXml(Server.HtmlDecode(svg2));
        svgGraph = Svg.SvgDocument.Open(xd);
        Bitmap secondImage = svgGraph.Draw();

        using (Bitmap result = new Bitmap(Math.Max(firstImage.Width, secondImage.Width), Math.Max(firstImage.Height, secondImage.Height)))
        {
            using (var canvas = Graphics.FromImage(result))
            {
                canvas.Clear(System.Drawing.Color.White);
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                canvas.DrawImage(firstImage, new Rectangle(0, 0, firstImage.Width, firstImage.Height));
                canvas.DrawImage(secondImage, new Rectangle(0, 0, secondImage.Width, secondImage.Height));
                canvas.Save();
            }
            // To save the chart, uncomment the line below
            //result.Save(@"c:\\chart.png", System.Drawing.Imaging.ImageFormat.Png);

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment; filename=chart.png");
            Response.ContentType = "image/png";

            using (MemoryStream ms = new MemoryStream())
            {
                result.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.WriteTo(Response.OutputStream);
            }

            Response.End();
        }
    }
</script>    

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Export Chart - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />
    
    <ext:XScript runat="server">
        <script>
            var exportTwoCharts = function (btn) {
                Ext.MessageBox.confirm('Confirm Download', 'Would you like to upload the chart to the server?', function (choice) {
                    if (choice == 'yes') {
                        var svg1 = Ext.htmlEncode(Ext.draw.engine.SvgExporter.generate(#{Chart1}.surface));
                        var svg2 = Ext.htmlEncode(Ext.draw.engine.SvgExporter.generate(#{Chart2}.surface));
                        #{DirectMethods}.ExportTwoSvg(svg1, svg2, { isUpload: true });
                    }
                });
            };
        
            var combineTwoChart = function (btn) {
                Ext.MessageBox.confirm('Confirm Download', 'Would you like to upload the chart to the server?', function (choice) {
                    if (choice == 'yes') {
                        var svg1 = Ext.htmlEncode(Ext.draw.engine.SvgExporter.generate(#{Chart1}.surface));
                        var svg2 = Ext.htmlEncode(Ext.draw.engine.SvgExporter.generate(#{Chart2}.surface));
                        #{DirectMethods}.CombineSvg(svg1, svg2, { isUpload: true });
                    }
                });
            };
        </script>
    </ext:XScript>

    <script>
        var saveChart = function (btn) {
            Ext.MessageBox.confirm('Confirm Download', 'Would you like to download the chart as an image?', function (choice) {
                if (choice == 'yes') {
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

        <h1>Export Multiple Charts to the Server Sample</h1>

        <p>Exports the Charts' SVG code to the Server to merge them into one image.</p>
        
        <p>Returns the result image file to the client.</p>

        <ext:Panel
            runat="server"
            Title="Area Chart"
            Layout="VBox"
            Width="600">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button 
                            runat="server" 
                            Text="Export Two Charts to Server" 
                            Icon="Images"
                            Handler="exportTwoCharts"
                            />

                        <ext:Button 
                            runat="server" 
                            Text="Merge Two Charts on Server" 
                            Icon="Images"
                            Handler="combineTwoChart"
                            />
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <ext:Panel ID="Panel1"
                    runat="server"
                    Width="600"
                    Height="300"
                    Title="Trends, 2007"
                    Layout="FitLayout">
                    <Items>
                        <ext:Chart ID="Chart1" runat="server" InsetPadding="30">
                            <Store>
                                <ext:Store ID="Store1" 
                                    runat="server" 
                                    Data="<%# Ext.Net.Examples.ChartData.GenerateData() %>" 
                                    AutoDataBind="true">                           
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="Name" />
                                                <ext:ModelField Name="Data1" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            <Axes>
                                <ext:NumericAxis Minimum="0" Maximum="100" Fields="Data1">
                                    <Label Font="10px Arial">
                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0');" />
                                    </Label>
                                </ext:NumericAxis>

                                <ext:CategoryAxis Position="Bottom" Fields="Name">
                                    <Label Font="11px Arial">
                                        <Renderer Handler="return value.substr(0, 3);" />
                                    </Label>
                                </ext:CategoryAxis>
                            </Axes>

                            <Series>
                                <ext:LineSeries Axis="Left" XField="Name" YField="Data1">
                                    <Listeners>
                                        <ItemMouseUp Handler="Ext.net.Notification.show({title:'Item Selected', html:item.value[1] + ' visits on ' + Ext.Date.monthNames[item.value[0]]});" />
                                    </Listeners>

                                    <Tips ID="Tips3" runat="server" TrackMouse="true" Width="110" Height="25">                            
                                        <Renderer Handler="this.setTitle(storeItem.get('Data1') + ' visits in ' + storeItem.get('Name').substr(0, 3));"></Renderer>
                                    </Tips>

                                    <Style Fill="#18428E" Stroke="#18428E" StrokeWidth="3" />

                                    <MarkerConfig 
                                        Type="Circle" 
                                        Size="4" 
                                        Radius="4" 
                                        StrokeWidth="0" 
                                        Fill="#18428E" 
                                        Stroke="#18428E" 
                                        />
                                </ext:LineSeries>
                            </Series>
                        </ext:Chart>
                    </Items>
                </ext:Panel>
        
                <ext:Panel ID="Panel2"
                    runat="server"
                    Width="600"
                    Height="300"
                    Title="Trends, 2007"
                    Layout="FitLayout">
                    <Items>
                        <ext:Chart ID="Chart2" runat="server" InsetPadding="30">
                            <Store>
                                <ext:Store ID="Store2" 
                                    runat="server" 
                                    Data="<%# Ext.Net.Examples.ChartData.GenerateData() %>" 
                                    AutoDataBind="true">                           
                                    <Model>
                                        <ext:Model ID="Model2" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="Name" />
                                                <ext:ModelField Name="Data2" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>

                            <Axes>
                                <ext:NumericAxis Minimum="0" Maximum="100" Fields="Data2">
                                    <Label Font="10px Arial">
                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0');" />
                                    </Label>
                                </ext:NumericAxis>

                                <ext:CategoryAxis Position="Bottom" Fields="Name">
                                    <Label Font="11px Arial">
                                        <Renderer Handler="return value.substr(0, 3);" />
                                    </Label>
                                </ext:CategoryAxis>
                            </Axes>

                            <Series>
                                <ext:LineSeries Axis="Left" XField="Name" YField="Data2">
                                    <Listeners>
                                        <ItemMouseUp Handler="Ext.net.Notification.show({title:'Item Selected', html:item.value[1] + ' visits on ' + Ext.Date.monthNames[item.value[0]]});" />
                                    </Listeners>

                                    <Tips ID="Tips2" runat="server" TrackMouse="true" Width="110" Height="25">                            
                                        <Renderer Handler="this.setTitle(storeItem.get('Data2') + ' visits in ' + storeItem.get('Name').substr(0, 3));"></Renderer>
                                    </Tips>

                                    <Style Fill="#18428E" Stroke="#18428E" StrokeWidth="3" />

                                    <MarkerConfig 
                                        Type="Circle" 
                                        Size="4" 
                                        Radius="4" 
                                        StrokeWidth="0" 
                                        Fill="#18428E" 
                                        Stroke="#18428E" 
                                        />
                                </ext:LineSeries>
                            </Series>
                        </ext:Chart>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Panel>
    </form>    
</body>
</html>