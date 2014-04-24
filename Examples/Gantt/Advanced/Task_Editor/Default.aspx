<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            var startDate = new DateTime(2010, 1, 11);
            Gantt1.StartDate = startDate;
            Gantt1.EndDate = startDate.AddDays(140);
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Task editor- Ext.NET Gantt</title>
    <link href="taskeditor.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />

    <script type="text/javascript">
        var calendarsCount = 2;
        var onCalendarLoad = function() {
            if (--calendarsCount > 0) return;

            var result = [];
            Ext.Array.each(Gnt.data.Calendar.getAllCalendars(), function (cal) {
                result.push({
                    Id      : cal.calendarId,
                    Name    : cal.name || cal.calendarId
                });
            });

            App.CalendarStore.add(result);
        };
    </script>
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server">
    </ext:ResourceManager> 

    <h1>Task editor</h1>
    <p>
        This is a simple example of the task editing window functionality.
        To open the task editing window please select the task in the tasks tree panel and click the &quot;Show Editor&quot; button in a toolbar.
        Or you can get the same result by double clicking corresponding task bar in the gantt chart.
    </p>

    <ext:Store ID="CalendarStore" runat="server">
        <Model>
            <ext:Model runat="server" 
                Name="Calendar"
                IDProperty="Id">
                <Fields>
                    <ext:ModelField Name="Id" />
                    <ext:ModelField Name="Name" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>

    <gnt:BusinessTimeStore ID="RootCalendar" runat="server"
        CalendarId="General"
        Name="General">
        <Proxy>
            <ext:AjaxProxy Url="rootCalendarData.js">
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
        <Listeners>
            <Load Fn="onCalendarLoad" />
            <BeforeSync Handler="return false;" />
        </Listeners>
    </gnt:BusinessTimeStore>

    <gnt:BusinessTimeStore ID="HolidaysCalendar" runat="server"
        CalendarId="Holidays"
        Name="Holidays">
        <Proxy>
            <ext:AjaxProxy Url="holidaysCalendarData.js">
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
        <Listeners>
            <Load Fn="onCalendarLoad" />
            <BeforeSync Handler="return false;" />
        </Listeners>
    </gnt:BusinessTimeStore>

    <gnt:BusinessTimeStore ID="NightShiftCalendar" runat="server"
        CalendarId="NightShift"
        Name="NightShift"
        DefaultAvailability="00:00-06:00,22:00-24:00">
    </gnt:BusinessTimeStore>

    <gnt:ResourceStore ID="ResourceStore" runat="server"
        ModelName="Gnt.model.Resource">
    </gnt:ResourceStore>

    <gnt:AssignmentStore ID="AssignmentStore" runat="server"
        ResourceStoreID="ResourceStore">
        <Proxy>
            <ext:AjaxProxy Url="assignmentdata.js">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader Root="assignments" />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
        <Listeners>
            <Load Handler="App.ResourceStore.loadData(this.proxy.reader.jsonData.resources);" />
        </Listeners>
    </gnt:AssignmentStore>

    <gnt:DependencyStore ID="DependencyStore" runat="server">
        <Proxy>
            <ext:AjaxProxy Url="dependencies.js">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
    </gnt:DependencyStore>

    <gnt:TaskStore ID="TaskStore" runat="server"
        CalendarStoreID="RootCalendar">
        <Proxy>
            <ext:AjaxProxy Url="taskdata.js">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>        
    </gnt:TaskStore>

    <gnt:Gantt ID="Gantt1" runat="server"
        Height="400"
        MultiSelect="true"
        ShowTodayLine="true"
        EnableDependencyDragDrop="false"
        HighlightWeekends="true"
        EnableBaseline="true"
        SnapToIncrement="true"
        ViewPreset="weekAndDayLetter"
        TaskStoreID="TaskStore"
        DependencyStoreID="DependencyStore"
        AssignmentStoreID="AssignmentStore"
        ResourceStoreID="ResourceStore">
        <LeftLabelFieldConfig DataIndex="Name">
            <Editor>
                <ext:TextField runat="server" />
            </Editor>
        </LeftLabelFieldConfig>
        <RightLabelFieldConfig DataIndex="Id">
            <Renderer Handler="return 'Id: #' + value;" />
        </RightLabelFieldConfig>
        <EventRenderer Handler="if (App.AssignmentStore.findExact('TaskId', taskRecord.data.Id) >= 0) { return { ctcls : 'resources-assigned'};}" />
        <ColumnModel>
            <Columns>                
                <ext:TreeColumn runat="server" Text="Tasks" DataIndex="Name" Width="250" />                
                <gnt:ResourceAssignmentColumn runat="server" Text="Assigned Resources" Width="150" />
            </Columns>
        </ColumnModel>
        <Plugins>
            <gnt:TaskEditor ID="TaskEditor" runat="server" />
        </Plugins>
        <Listeners>
            <SelectionChange Handler="var el = this.down('[iconCls=indent]'); if(el){el.setDisabled(!selected.length);}" />
        </Listeners>
        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:Button runat="server" 
                        IconCls="indent" 
                        Text="Show Editor" 
                        Disabled="true"
                        Handler="var sm = this.up('ganttpanel').lockedGrid.getSelectionModel(); if (sm.selected.length) {App.TaskEditor.showTask(sm.selected.items[0]);}" />
                </Items>
            </ext:Toolbar>
        </TopBar>
    </gnt:Gantt>
</form>
</body>
</html>