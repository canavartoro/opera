<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>
<%@ Import Namespace="Ext.Net.Gantt" %>

<!DOCTYPE HTML>
<html>
<head runat="server">
    <title>Basic Gantt demo</title>    
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/style3.css" rel="stylesheet" type="text/css" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/style2.css" rel="stylesheet" type="text/css" />
    <link href="/resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" />
        <p>
            This example shows some different ways to style the chart using just CSS. Click the toolbar buttons to switch styles.
        </p>        
        <br />

        <gnt:Gantt ID="Gantt1" runat="server"
            Height="600"
            Width="1000"
            LeftLabelField="Name"
            HighlightWeekends="true"
            ShowTodayLine="true"
            EnableDependencyDragDrop="false"
            StartDate="<%# new DateTime(2010, 1, 11) %>"
            EndDate="<%# new DateTime(2010, 6, 4) %>"
            AutoDataBind="true"
            ViewPreset="weekAndDayLetter">

            <View>
                <gnt:GanttView runat="server" TrackOver="false" />
            </View>
            
            <TaskStore ID="TaskStore1" runat="server">
                <Proxy>
                    <ext:AjaxProxy Url="tasks.json">
                        <ActionMethods Read="GET" />
                        <Reader>
                            <ext:JsonReader />
                        </Reader>
                    </ext:AjaxProxy>
                </Proxy>
            </TaskStore>

            <DependencyStore runat="server" ModelName="Gnt.model.Dependency">
                <Proxy>
                    <ext:AjaxProxy Url="dependencies.json">
                        <ActionMethods Read="GET" />
                        <Reader>
                            <ext:JsonReader />
                        </Reader>
                    </ext:AjaxProxy>
                </Proxy>
            </DependencyStore>

            <ColumnModel>
                <Columns>
                    <ext:TreeColumn 
                        runat="server" 
                        Text="Tasks" 
                        Sortable="true" 
                        DataIndex="Name" 
                        Width="250" />
                </Columns>
            </ColumnModel>

            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" Text="Style 1" IconCls="theme" Scale="Large">
                            <Listeners>
                                <Click Handler="Ext.getBody().removeCls(['style2', 'style3']).addCls('style1');" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" Text="Style 2" IconCls="theme" Scale="Large">
                            <Listeners>
                                <Click Handler="Ext.getBody().removeCls(['style1', 'style3']).addCls('style2');" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" Text="Style 3" IconCls="theme" Scale="Large">
                            <Listeners>
                                <Click Handler="Ext.getBody().removeCls(['style1', 'style2']).addCls('style3');" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </gnt:Gantt>

        <div style="display:none">
            <img src="images/bg-lines.gif" />
            <img src="images/transp-1px.png" />
            <img src="images/glow-bg.png" />
            <img src="images/halfcircle.png" />
            <img src="images/theme.png" />
        </div>
    </form>
</body>
</html>
