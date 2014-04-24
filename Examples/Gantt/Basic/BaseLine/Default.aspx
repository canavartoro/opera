<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Baseline Demo - Ext.NET Gantt</title>
    <link href="/resources/css/examples.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .sch-gantt-item,
        .sch-gantt-progress-bar {
            -moz-transition-property    : height, top;
            -moz-transition-duration    : 0.4s;
            
            -webkit-transition-property : height, top;
            -webkit-transition-duration : 0.4s;
            
            -ms-transition-property     : height, top;
            -ms-transition-duration     : 0.4s;
            
            transition-property         : height, top;
            transition-duration         : 0.4s;
        }
    </style>
</head>
<body style="padding:20px;">
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <p>This example shows both the current plan and the original baseline.</p>

        <gnt:Gantt 
            ID="Gantt1" 
            runat="server"
            Height="600"
            Width="1000"
            LeftLabelField="Name"
            HighlightWeekends="false"
            ShowTodayLine="true"
            EnableBaseline="true"
            BaselineVisible="true"
            EnableDependencyDragDrop="false"            
            ViewPreset="monthAndYear"            
            StartDate="<%# new DateTime(2010, 1, 1) %>"
            EndDate="<%# new DateTime(2010, 10, 1) %>"
            AutoDataBind="true">

            <ColumnModel runat="server">
                <Columns>
                    <ext:TreeColumn 
                        runat="server" 
                        Text="Tasks" 
                        Sortable="true" 
                        DataIndex="Name" 
                        Width="250" 
                        Locked="true">
                        <Editor>
                            <ext:TextField runat="server" EmptyText="Please give me a name" />
                        </Editor>
                    </ext:TreeColumn>                    
                </Columns>
            </ColumnModel>

            <TaskStore ID="TaskStore1" runat="server">
                <Model>
                    <ext:Model runat="server" Extend="Gnt.model.Task">
                        <Fields>
                            <ext:ModelField Name="BaselineStartDate" Type="Date" DateFormat="yyyy-MM-dd" />
                            <ext:ModelField Name="BaselineEndDate" Type="Date" DateFormat="yyyy-MM-dd" />
                            <ext:ModelField Name="BaselinePercentDone" />
                        </Fields>
                    </ext:Model>
                </Model>                

                <Proxy>
                    <ext:AjaxProxy Url="tasks.json">
                        <ActionMethods Read="GET" />
                        <Reader>
                            <ext:JsonReader />
                        </Reader>
                    </ext:AjaxProxy>
                </Proxy>
            </TaskStore>

            <DependencyStore runat="server">                
                <Proxy>
                    <ext:AjaxProxy Url="dependencies.json">
                        <ActionMethods Read="GET" />
                        <Reader>
                            <ext:JsonReader />
                        </Reader>
                    </ext:AjaxProxy>
                </Proxy>
            </DependencyStore>
            
            <Plugins>
                <gnt:Pan runat="server" />
            </Plugins>            

            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" Text="Show baseline" EnableToggle="true" Pressed="true">
                            <Listeners>
                                <Click Handler="#{Gantt1}.el.toggleCls('sch-ganttpanel-showbaseline');" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </gnt:Gantt>
    </form>
</body>
</html>
