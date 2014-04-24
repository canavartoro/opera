<%@ Page Language="C#" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Xml" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<script runat="server">
    protected void ReloadData(object sender, DirectEventArgs e)
    {
        this.Chart1.GetStore().DataBind();
    }

    [DirectMethod]
    public void ExportSvg(string svg)
    {
        if (string.IsNullOrEmpty(svg))
        {
            X.Msg.Alert("SVG is empty", "Provided SVG is null or empty");
            return;
        }
        
        var encodedSvg = Server.HtmlDecode(svg);
        XmlDocument xd = new XmlDocument();
        xd.XmlResolver = null;
        xd.LoadXml(encodedSvg);
        var svgGraph = Svg.SvgDocument.Open(xd);
        
        // To save the chart, uncomment the line below
        //svgGraph.Draw().Save(@"c:\\chart.png", System.Drawing.Imaging.ImageFormat.Png);
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "attachment; filename=chart.png");
        Response.ContentType = "image/png";

        using (Bitmap image = svgGraph.Draw())
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.WriteTo(Response.OutputStream);
            }
        }
        
        Response.End();
    }
</script>    

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Export Chart - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />
    
    <script>
        var saveChart = function (btn) {
            Ext.MessageBox.confirm('Confirm Save', 'Save Chart to an image?', function (choice) {
                if (choice == 'yes') {
                    btn.up('panel').down('chart').save({
                        type: 'image/png'
                    });
                }
            });
        };

        var exportChart = function (btn) {
            Ext.MessageBox.confirm('Confirm Export', 'Export Chart to an image?', function (choice) {
                if (choice == 'yes') {
                    var svg = Ext.htmlEncode(Ext.draw.engine.SvgExporter.generate(btn.up('panel').down('chart').surface));

                    App.direct.ExportSvg(svg, { isUpload: true });
                }
            });
        };
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Export a Chart to the Server Sample</h1>

        <p>Exports the Chart's SVG code to the Server.</p>
        
        <p>Returns the result image file to the client.</p>

        <p>This sample uses the <a href="https://www.nuget.org/packages/Svg/">SVG Rendering Library</a> to create image instances on the server and export to disk or send back to the client browser.</p>

        <ext:Panel
            runat="server"
            Title="Area Chart"
            Width="800"
            Height="600"
            Layout="FitLayout">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button 
                            runat="server" 
                            Text="Save Chart" 
                            Icon="Disk"
                            Handler="saveChart"
                            />

                        <ext:Button 
                            runat="server" 
                            Text="Export Chart to the Server" 
                            Icon="DiskDownload"
                            Handler="exportChart"
                            />

                        <ext:Button 
                            runat="server" 
                            Text="Reload Data" 
                            Icon="ArrowRefresh" 
                            OnDirectClick="ReloadData" 
                            />

                        <ext:Button 
                            runat="server" 
                            Text="Animate" 
                            Icon="ShapesManySelect" 
                            EnableToggle="true" 
                            Pressed="true">
                            <Listeners>
                                <Toggle Handler="#{Chart1}.animate = pressed ? {easing: 'ease', duration: 500} : false;" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <ext:Chart 
                    ID="Chart1" 
                    runat="server"
                    Legend="true"
                    Animate="true">
                    <Background>
                        <Gradient Angle="90" GradientID="Gradient1">
                            <Stops>
                                <ext:GradientStop Color="#feffff" Offset="0" />
                                <ext:GradientStop Color="#d2ebf9" Offset="60" />
                            </Stops>
                        </Gradient>
                    </Background>
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
                                        <ext:ModelField Name="Data4" />
                                        <ext:ModelField Name="Data5" />
                                        <ext:ModelField Name="Data6" />
                                        <ext:ModelField Name="Data7" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Axes>
                        <ext:NumericAxis                             
                            Fields="Data1,Data2,Data3,Data4,Data5,Data6,Data7"
                            Title="Number of Hits"
                            Minimum="0"
                            />

                        <ext:CategoryAxis 
                            Position="Bottom"
                            Fields="Name"
                            Title="Month of the Year"
                            Grid="true">
                            <Label>
                                <Rotate Degrees="315" />
                            </Label>
                        </ext:CategoryAxis>
                    </Axes>
                    <Series>
                        <ext:AreaSeries 
                            Axis="Left"
                            XField="Name"
                            YField="Data1,Data2,Data3,Data4,Data5,Data6,Data7">
                                <Style Opacity="0.93" />
                            </ext:AreaSeries>
                    </Series>
                </ext:Chart>
            </Items>
        </ext:Panel>
    </form>    
</body>
</html>