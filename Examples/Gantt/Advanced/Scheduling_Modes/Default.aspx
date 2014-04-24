<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            ResourceStore.Resources.AddRange(new []{
                new Resource { Id = 1, Name = "Mats"},
                new Resource { Id = 2, Name = "Nickolay"},
                new Resource { Id = 3, Name = "Goran"},
                new Resource { Id = 4, Name = "Alex"},
                new Resource { Id = 5, Name = "Jakub"},
                new Resource { Id = 7, Name = "Juan"}
            });


            AssignmentStore.Assignments.AddRange(new [] {
                new ResourceAssignment { Id = 1,  TaskId = 1, ResourceId = 1, Units = 100},
                new ResourceAssignment { Id = 2,  TaskId = 1, ResourceId = 2, Units = 50},
                new ResourceAssignment { Id = 3,  TaskId = 2, ResourceId = 2, Units = 50},
                new ResourceAssignment { Id = 4,  TaskId = 4, ResourceId = 4, Units = 50},
                new ResourceAssignment { Id = 5,  TaskId = 6, ResourceId = 5, Units = 50}
            });

            BusinessTimeStore.Calendars.AddRange(new[] { 
                new Ext.Net.Gantt.CalendarDay { Date = new DateTime(2011, 7, 16), IsWorkingDay = true, Availability = new [] { "11:00-16:00", "17:00-20:00" } },
                new Ext.Net.Gantt.CalendarDay { Date = new DateTime(2011, 7, 17), IsWorkingDay = true, Availability = new [] { "12:00-16:00" } }
            });

            Gantt1.StartDate = new DateTime(2011, 6, 28);
            Gantt1.EndDate = new DateTime(2011, 7, 30);
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Scheduling modes- Ext.NET Gantt</title>
    <link href="scheduling_modes.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />

    <script type="text/javascript">
        
    </script>
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server" />    

    <gnt:ViewPreset runat="server"
        Name="weekAndDayNarrow"
        TimeColumnWidth="35"
        RowHeight="24"
        ResourceColumnWidth="100"
        DisplayDateFormat="yyyy-MM-dd"
        ShiftUnit="WEEK"
        ShiftIncrement="1"
        DefaultSpan="1">
        <TimeResolution Unit="DAY" Increment="1" />
        <HeaderConfig>
            <Bottom Unit="DAY" Increment="1" DateFormat="dd" />
            <Middle Unit="WEEK" DateFormat="ddd dd MMM" Align="Left" />
        </HeaderConfig>
    </gnt:ViewPreset>

    <gnt:ResourceStore ID="ResourceStore" runat="server">
    </gnt:ResourceStore>

    <gnt:AssignmentStore ID="AssignmentStore" runat="server"
        ResourceStoreID="ResourceStore">
    </gnt:AssignmentStore>

    <gnt:BusinessTimeStore ID="BusinessTimeStore" runat="server"
        CalendarId="Project">
    </gnt:BusinessTimeStore>

    <gnt:TaskStore ID="TaskStore" runat="server"
        CalendarStoreID="BusinessTimeStore">
        <Sorters>
            <ext:DataSorter Property="StartDate" />
        </Sorters>
        <Proxy>
            <ext:AjaxProxy Url="tasks.json">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
    </gnt:TaskStore>

    <gnt:DependencyStore ID="DependencyStore" runat="server">
        <Proxy>
            <ext:AjaxProxy Url="dependencies.json">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
    </gnt:DependencyStore>

    <ext:Viewport runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" Region="North" BodyStyle="padding:15px">
                <Content>
                    <h1>Scheduling modes</h1>
        
                    <h2>
                        This is a more advanced example demonstrating various scheduling modes supported by Gantt component:
                    </h2>
                    <ul style="list-style:disc;padding-left:20px">
                        <li><b>Manual</b> - task bypasses any holidays/availability rules, user can manually specify any start/end date for it</li>
                        <li><b>Normal</b> - backward compatibility mode with Gantt 2.0.x, task only takes into account full days (holidays/weekends), but not hours. Resource calendars does not affect task schedule</li>
                        <li><b>Fixed duration</b> - scheduling mode with fixed duration (the effort of task will change when assigning/removing resources)</li>
                        <li><b>Effort driven</b> - scheduling mode with fixed effort (the duration of task will change when assigning/removing resources)</li>
                        <li><b>Dynamic assignment</b> - scheduling mode with fixed duration and effort (the resources allocation will change when assigning/removing resources)</li>
                    </ul>
                </Content>
            </ext:Panel>

            <gnt:Gantt ID="Gantt1" runat="server"
                Region="Center"
                TaskStoreID="TaskStore"
                DependencyStoreID="DependencyStore"
                ResourceStoreID="ResourceStore"
                AssignmentStoreID="AssignmentStore"
                ViewPreset="weekAndDayNarrow"
                RightLabelField="Responsible"
                HighlightWeekends="true"
                ShowTodayLine="true"                   
                EnableProgressBarResize="true">
                <LeftLabelFieldConfig DataIndex="Name">
                    <Editor>
                        <ext:TextField runat="server" />
                    </Editor>
                </LeftLabelFieldConfig>
                <SelectionModel>
                    <ext:TreeSelectionModel runat="server" IgnoreRightMouseSelection="false" Mode="Multi" />
                </SelectionModel>
                <Plugins>
                    <gnt:TaskContextMenu runat="server" />
                    <gnt:Pan runat="server" />
                    <gnt:TreeCellEditing runat="server" ClicksToEdit="1" />
                </Plugins>
                <TooltipTpl>
                    <Html>
                        <h4 class="tipHeader">{Name}</h4>
                        <table class="taskTip">
                            <tr><td>Start:</td> <td align="right">{[Ext.Date.format(values.StartDate, "y-m-d")]}</td></tr>
                            <tr><td>End:</td> <td align="right">{[Ext.Date.format(Ext.Date.add(values.EndDate, Ext.Date.MILLI, -1), "y-m-d")]}</td></tr>
                            <tr><td>Progress:</td><td align="right">{[Math.round(values.PercentDone)]}%</td></tr>
                        </table>
                    </Html>
                </TooltipTpl>
                <ColumnModel>
                    <Columns>
                        <ext:TreeColumn runat="server" Text="Tasks" DataIndex="Name" Width="110">
                            <Editor>
                                <ext:TextField runat="server" AllowBlank="false" />
                            </Editor>
                        </ext:TreeColumn>

                        <gnt:StartDateColumn runat="server" Format="ddd MM/dd" Width="70" />
                        <gnt:EndDateColumn runat="server" Format="ddd MM/dd" Width="70" />
                        <gnt:DurationColumn runat="server" Width="60" />
                        <gnt:EffortColumn runat="server" Width="60" />
                        <gnt:SchedulingMode runat="server" Width="60" />
                        <gnt:ResourceAssignmentColumn runat="server" Text="Assigned Resources" Width="150" />
                    </Columns>
                </ColumnModel>

                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:ButtonGroup runat="server" Title="Navigation" Columns="2">
                                <Items>
                                    <ext:Button runat="server" Icon="ResultsetPrevious" Scale="Large" Text="&nbsp;" Handler="this.up('ganttpanel').shiftPrevious();" />
                                    <ext:Button runat="server" Icon="ResultsetNext" Scale="Large" Text="&nbsp;" Handler="this.up('ganttpanel').shiftNext();" />
                                </Items>
                            </ext:ButtonGroup>

                            <ext:ButtonGroup runat="server" Title="View tools" Columns="2">
                                <Items>
                                    <ext:Button runat="server" IconCls="icon-collapseall" Text="Collapse all" Handler="this.up('ganttpanel').collapseAll();" />
                                    <ext:Button runat="server" IconCls="zoomfit" Text="Zoom to fit" Handler="this.up('ganttpanel').zoomToFit();" />
                                    <ext:Button runat="server" IconCls="icon-expandall" Text="Expand all" Handler="this.up('ganttpanel').expandAll();" />
                                </Items>
                            </ext:ButtonGroup>

                            <ext:ButtonGroup runat="server" Title="View resolution" Columns="2">
                                <Items>
                                    <ext:Button runat="server" Text="10 weeks" Scale="Large" Handler="this.up('ganttpanel').switchViewPreset('weekAndDayLetter');" />
                                    <ext:Button runat="server" Text="1 year" Scale="Large" Handler="this.up('ganttpanel').switchViewPreset('monthAndYear');" />
                                </Items>
                            </ext:ButtonGroup>

                            <ext:ToolbarFill />

                            <ext:ButtonGroup runat="server" Title="Try some features..." Columns="2">
                                <Items>
                                    <ext:Button runat="server" IconCls="togglebutton" Text="Cascade changes" EnableToggle="true" Handler="this.up('ganttpanel').setCascadeChanges(this.pressed);" />
                                </Items>
                            </ext:ButtonGroup>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
            </gnt:Gantt>
        </Items>
    </ext:Viewport>
</form>
</body>
</html>