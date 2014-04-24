<%@ Page Language="C#" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>  

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Tips Chart - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />
    <style>
        .x-tip {
            background-color      : #fff;
            border-radius         : 0.5em;
            -moz-border-radius    : 0.5em;
            -webkit-border-radius : 0.5em;
            border-radius : 0.5em;
            border        : 1px solid rgba(134, 84, 41, 0.5);
            opacity       : 0.95;
        }
        
        .x-tip-header {
            margin-bottom : 5px;
        }
        
        .x-tip .x-panel .x-panel-body.x-layout-fit {
            border : none;
        }
        
        .x-tip .x-panel.x-grid-section.x-panel-noborder.x-fit-item {
            margin : 0;
        }
        
        .x-tip .x-panel.x-box-item {
            top : 0 !important;
        }
        
        .x-tip-header-body .x-component.x-box-item {
            width      : 100%;
            text-align : center;
        }
        
        .x-tip-body {
            text-shadow : none;
        }
        
        .x-panel {
            margin : 20px;
        }
        
        ul {
            margin-left : 10px;
        }
        
        ul li {
            display     : block;
            font-weight : normal;
            color       : #444;
            padding     : 2px;
        }
    </style>

    <script>
        var tipsRenderer = function (si, item) {
            var storeItem = item.storeItem,
                data = [{
                    name: 'Data1',
                    data: storeItem.get('Data1')
                }, {
                    name: 'Data2',
                    data: storeItem.get('Data2')
                }, {
                    name: 'Data3',
                    data: storeItem.get('Data3')
                }, {
                    name: 'Data4',
                    data: storeItem.get('Data4')
                }, {
                    name: 'Data5',
                    data: storeItem.get('Data5')
                }], i, l, html;

            this.setTitle("Information for " + storeItem.get('Name'));

            App.PieStore1.loadData(data);
            App.Grid1.store.loadData(data);
            App.Grid1.setSize(480, 130);
        };
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Chart in tips example</h1>

        <p>Showing a Pie Chart and a Grid Panel as elements in a tooltip.</p>

        <ext:Panel 
            runat="server"
            Title="Line Chart"
            Width="800"
            Height="400"
            Layout="FitLayout">
            <Items>
                <ext:Chart 
                    runat="server"
                    Animate="true"
                    Shadow="true">
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
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>

                    <Axes>
                        <ext:NumericAxis Fields="Data1" Grid="true" />
                        <ext:CategoryAxis Position="Bottom" Fields="Name" />
                    </Axes>

                    <Series>
                        <ext:LineSeries Axis="Left" XField="Name" YField="Data1">
                            <MarkerConfig Radius="5" Size="5" />
                            <Tips 
                                runat="server" 
                                TrackMouse="true" 
                                Width="580" 
                                Height="170" 
                                Layout="FitLayout">
                                <Items>
                                    <ext:Container runat="server" Layout="HBoxLayout">
                                        <Items>
                                            <ext:Chart 
                                                runat="server"
                                                Width="100"
                                                Height="100"
                                                Animate="false"
                                                Shadow="false"
                                                InsetPadding="0"
                                                Theme="Base:gradients">
                                                <Store>
                                                    <ext:Store ID="PieStore1" runat="server">
                                                        <Model>
                                                            <ext:Model runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="name" />
                                                                    <ext:ModelField Name="data" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>

                                                <Series>
                                                    <ext:PieSeries AngleField="data" ShowInLegend="false">
                                                        <Label 
                                                            Field="name" 
                                                            Display="Rotate" 
                                                            Contrast="true" 
                                                            Font="9px Arial" 
                                                            />                                                        
                                                    </ext:PieSeries>
                                                </Series>
                                            </ext:Chart>

                                            <ext:GridPanel ID="Grid1" runat="server" Width="480" Height="130">
                                                <Store>
                                                    <ext:Store runat="server">
                                                        <Model>
                                                            <ext:Model runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="name" />
                                                                    <ext:ModelField Name="data" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>

                                                <ColumnModel>
                                                    <Columns>
                                                        <ext:Column runat="server" Text="Name" DataIndex="name" />
                                                        <ext:Column runat="server" Text="Data" DataIndex="data" />
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                        </Items>
                                    </ext:Container>
                                </Items>
                                <Renderer Fn="tipsRenderer" />
                            </Tips>
                        </ext:LineSeries>
                    </Series>
                </ext:Chart>
            </Items>
        </ext:Panel>
    </form>    
</body>
</html>