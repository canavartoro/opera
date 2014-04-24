<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            DependencyStore dependencyStore = new DependencyStore();
            dependencyStore.Data = new object[] 
            {
                new Dependency()
                {
                    From = 112,
                    To = 115,
                    Type = 2
                },
                
                new Dependency()
                {
                    From = 111,
                    To = 200,
                    Type = 0
                }
            };
            
            this.Gantt1.DependencyStore = dependencyStore;
        }
    }   
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Buffered rendering - Ext.NET Gantt</title>
    <link href="buffered.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Buffered rendering Ext.NET Gantt Demo</h1>

        <p>This is an examples showcasing a buffered view capable of rendering thousands of row without bringing the browser to a halt.</p>

        <br />

        <gnt:Gantt 
            ID="Gantt1" 
            runat="server"
            Height="350"
            Width="1000"
            RightLabelField="Name"
            HighlightWeekends="false"
            CascadeChanges="false"
            ViewPreset="weekAndDayLetter"
            StartDate="<%# new DateTime(2010, 1, 4) %>"
            EndDate="<%# new DateTime(2010, 1, 4).AddDays(140) %>"
            AutoDataBind="true">
            <TaskStore runat="server" SortOnLoad="false">
                <Proxy>
                    <ext:AjaxProxy Url="GetTaskData.ashx" />
                </Proxy>
            </TaskStore>
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button 
                            runat="server" 
                            Text="Scroll to 888"
                            Handler="var g = this.up('ganttpanel');
                                     g.getSchedulingView().scrollEventIntoView(g.taskStore.getById(888), true);" />

                        <ext:Button 
                            runat="server" 
                            Text="Scroll to 111"
                            Handler="var g = this.up('ganttpanel');
                                     g.getSchedulingView().scrollEventIntoView(g.taskStore.getById(111), true);" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel>
                <Columns>
                    <ext:TreeColumn 
                        runat="server"
                        Text="Tasks"
                        Sortable="true"
                        DataIndex="Name"
                        Width="200" />

                    <gnt:StartDateColumn runat="server" />
                    <gnt:EndDateColumn runat="server" />
                    <gnt:PercentDoneColumn runat="server" />
                </Columns>
            </ColumnModel>
            <Plugins>
                <gnt:TaskContextMenu runat="server" />
                <ext:BufferedRenderer runat="server" />
            </Plugins>
        </gnt:Gantt>
    </form>
</body>
</html>
