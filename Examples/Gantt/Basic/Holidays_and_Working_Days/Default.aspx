<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Holidays - Ext.NET Gantt</title>
    <link href="holidays.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />    
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Holidays Ext.NET Gantt Demo</h1>

        <p>
            This is a simple example which demonstrates the capabilities of built-in calendar system. the calendar allows the Gantt to skip non-working days during calculation of task durations.
            By default the only non-working time in calendar are weekends. Its possible to add the additional holidays or exceptions (working day which is on weekend). 
            
            Holidays/exceptions should be represented as the `Gnt.model.CalendarDay` records in the calendar store. Its also possible to apply custom styling for each holiday period.
        </p>

        <br />

        <gnt:CalendarStore ID="CalendarStore1" runat="server">
            <Proxy>
                <ext:AjaxProxy>
                    <API Read="holidaydata.js" />
                </ext:AjaxProxy>
            </Proxy>
            <Listeners>
                <BeforeSync Handler="return false;" />
            </Listeners>
        </gnt:CalendarStore>

        <gnt:Gantt 
            runat="server"
            Height="350"
            Width="1000"
            LeftLabelField="Name"
            EnableProgressBarResize="true"
            EnableDependencyDragDrop="false"
            HighlightWeekends="true"
            ViewPreset="weekAndDayLetter"
            StartDate="<%# new DateTime(2010, 1, 11) %>"
            EndDate="<%# new DateTime(2010, 1, 11).AddDays(70) %>"
            AutoDataBind="true">
            <TaskStore runat="server" CalendarStoreID="CalendarStore1">
                <Proxy>
                    <ext:AjaxProxy Url="tasks.xml">
                        <ActionMethods Read="GET" />
                        <Reader>
                            <ext:XmlReader Record=">Task" Root="Tasks" IDProperty="Id" />
                        </Reader>
                    </ext:AjaxProxy>
                </Proxy>
                <Sorters>
                    <ext:DataSorter Property="leaf" Direction="ASC" />
                </Sorters>
            </TaskStore>
            <DependencyStore runat="server">
                <Proxy>
                    <ext:AjaxProxy Url="dependencies.xml">
                        <ActionMethods Read="GET" />
                        <Reader>
                            <ext:XmlReader Record="Link" Root="Links" />
                        </Reader>
                    </ext:AjaxProxy>
                </Proxy>
            </DependencyStore>
            <TooltipTpl runat="server">
                <Html>
                    <ul class="taskTip">
                        <li><strong>Task:</strong>{Name}</li>
                        <li><strong>Start:</strong>{[Ext.Date.format(values.StartDate, "y-m-d")]}</li>
                        <li><strong>Duration:</strong> {[parseFloat(Ext.Number.toFixed(values.Duration, 1))]} {DurationUnit}</li>
                        <li><strong>Progress:</strong>{[Math.round(values.PercentDone)]}%</li>
                    </ul>
                </Html>
            </TooltipTpl>
            <ColumnModel>
                <Columns>
                    <ext:TreeColumn
                        runat="server"
                        Text="Tasks"
                        Sortable="true"
                        DataIndex="Name"
                        Width="180" />

                    <gnt:StartDateColumn runat="server" Width="80" />

                    <gnt:EndDateColumn runat="server" Width="80" />

                    <gnt:DurationColumn runat="server" Width="70" />

                    <ext:Column 
                        runat="server"
                        Text="% Done"
                        Sortable="true"
                        DataIndex="PercentDone"
                        Width="50"
                        Align="Center" />
                </Columns>
            </ColumnModel>
            <Plugins>
                <gnt:TreeCellEditing runat="server" />
            </Plugins>
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" Text="See calendar" Icon="Calendar">
                            <Menu>
                                <ext:Menu runat="server">
                                    <Items>
                                        <gnt:CalendarPicker 
                                            runat="server" 
                                            CalendarStoreID="CalendarStore1" 
                                            ShowToday="false"
                                            StartDate="<%# new DateTime(2010, 1, 11) %>"
                                            EndDate="<%# new DateTime(2010, 1, 11).AddDays(70) %>"
                                            AutoDataBind="true" />
                                    </Items>
                                </ext:Menu>
                            </Menu>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </gnt:Gantt>
    </form>
</body>
</html>
