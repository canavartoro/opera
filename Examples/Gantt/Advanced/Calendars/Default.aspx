<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>
<%@ Import Namespace="Ext.Net" %>
<%@ Import Namespace="Ext.Net.Gantt" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            this.InitRootCalendar();
            this.InitHolidaysCalendar();            
            this.InitAssignmentStore();
            this.InitResourcesStore();
            this.InitDependencyStore();

            var startDate = new DateTime(2010, 1, 11);
            Gantt1.StartDate = startDate;
            Gantt1.EndDate = startDate.AddDays(70);
        }
    }

    private void InitDependencyStore()
    {
        DependencyStore.Dependencies.AddRange(new []{
            new Dependency{
                From = 3,
                To = 4
            },
            
            new Dependency{
                From = 4,
                To = 5
            },
            
            new Dependency{
                From = 5,
                To = 13
            },
            
            new Dependency{
                From = 13,
                To = 14
            },
            
            new Dependency{
                From = 14,
                To = 15
            },
            
            new Dependency{
                From = 23,
                To = 24
            },
            
            new Dependency{
                From = 24,
                To = 25
            }
        });
    }

    private void InitAssignmentStore()
    {
        AssignmentStore.Assignments.AddRange(new []{
            new ResourceAssignment{
                Id = 1,
                TaskId = 4,
                ResourceId = 1,
                Units = 100
            },
            
            new ResourceAssignment{
                Id = 2,
                TaskId = 4,
                ResourceId = 2,
                Units = 80
            },
            
            new ResourceAssignment{
                Id = 3,
                TaskId = 11,
                ResourceId = 5,
                Units = 50
            },
            
            new ResourceAssignment{
                Id = 4,
                TaskId = 12,
                ResourceId = 6,
                Units = 50
            }
        });
    }

    private void InitResourcesStore()
    {
        ResourceStore.Resources.AddRange(new []{
            new Resource{
                Id = 1,
                Name = "Mats",
                CalendarId = "NightShift"                
            },
            
            new Resource { Id = 2, Name = "Nickolay"},
            new Resource { Id = 3, Name = "Goran"},
            new Resource { Id = 4, Name = "Dan"},
            new Resource { Id = 5, Name = "Jake"},
            new Resource { Id = 6, Name = "Kim"},
            new Resource { Id = 7, Name = "Bart"}
        });
    }
    
    private void InitRootCalendar()
    {
        RootCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = 1,
            Date = new DateTime(2010, 1, 14),
            Cls = "gnt-national-holiday",
            Name = "Some big holiday"
        });
    }

    private void InitHolidaysCalendar()
    {
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Date = new DateTime(2010, 1, 13),
            Cls = "gnt-national-holiday",
            Name = "Mats's birthday"
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Date = new DateTime(2010, 2, 1),
            Cls = "gnt-company-holiday",
            Name = "Bryntum company holiday"
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Date = new DateTime(2010, 12, 1),
            Name = "Bryntum 1st birthday",
            IsWorkingDay = false
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Date = new DateTime(2010, 3, 27),
            Name = "Half working day",
            IsWorkingDay = true,
            Availability = new[] { "08:00-12:00" }
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "0-2012/03/25-2012/03/31",
            Name = "Non standard week",
            IsWorkingDay = false
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "1-2012/03/25-2012/03/31",
            Name = "Non standard week",
            IsWorkingDay = true,
            Availability = new[] { "08:00-12:00" }
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "2-2012/03/25-2012/03/31",
            Name = "Non standard week",
            IsWorkingDay = true,
            Availability = new[] { "13:00-15:00" }
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "3-2012/03/25-2012/03/31",
            Name = "Non standard week",
            IsWorkingDay = false
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "4-2012/03/25-2012/03/31",
            Name = "Non standard week",
            IsWorkingDay = true,
            Availability = new[] { "08:00-12:00" }
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "5-2012/03/25-2012/03/31",
            Name = "Non standard week",
            IsWorkingDay = false
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "6-2012/03/25-2012/03/31",
            Name = "Non standard week",
            IsWorkingDay = false
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "0-2012/02/25-2012/02/28",
            Name = "Non standard feb week",
            IsWorkingDay = false
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "1-2012/02/25-2012/03/28",
            Name = "Non standard feb week",
            IsWorkingDay = true,
            Availability = new[] { "08:00-12:00" }
        });
        HolidaysCalendar.Calendars.Add(new Ext.Net.Gantt.CalendarDay
        {
            Id = "2-2012/02/25-2012/02/28",
            Name = "Non standard feb week",
            IsWorkingDay = true,
            Availability = new[] { "13:00-15:00" }
        });
    }

    protected void TasksLoad(object sender, NodeLoadEventArgs e)
    {
        e.Nodes.Add(new Ext.Net.Gantt.Task
        {
            Id = 1,
            Name = "Sencha Releases",
            StartDate = new DateTime(2010, 01, 18),
            Duration = 16,
            Expanded = true,
            PercentDone = 50,
            Children = { 
                new Ext.Net.Gantt.Task {
                    Expanded = true,
                    StartDate = new DateTime(2010,01, 18),
                    Duration = 16,
                    PercentDone = 50,
                    Id = 2,                    
                    Name = "Ext 4.x branch",
                    Children = {
                        new Ext.Net.Gantt.Task {
                            PercentDone = 100,
                            StartDate = new DateTime(2010, 01, 18),
                            Duration = 5,
                            Id = 3,
                            Leaf = true,
                            Name = "Ext JS 4.0.1",
                            CalendarId = "NightShift"   
                        },
                        
                        new Ext.Net.Gantt.Task {
                            PercentDone = 100,
                            StartDate = new DateTime(2010, 01, 25),
                            Duration = 5,
                            Id = 4,
                            Leaf = true,
                            Name = "Ext JS 4.0.2"
                        },
                        
                        new Ext.Net.Gantt.Task {
                            PercentDone = 30,
                            StartDate = new DateTime(2010, 02, 02),
                            Duration = 5,
                            Id = 5,
                            Leaf = true,
                            Name = "Ext JS 4.0.3"
                        }
                    }
                },
                
                new Ext.Net.Gantt.Task {
                    Expanded = true,
                    StartDate = new DateTime(2010,01, 20),
                    Duration = 12,
                    PercentDone = 10,
                    Id = 22,                    
                    Name = "Gxt 3 branch",
                    Children = {
                        new Ext.Net.Gantt.Task {
                            PercentDone = 30,
                            StartDate = new DateTime(2010, 01, 20),
                            Duration = 3,
                            Id = 23,
                            Leaf = true,
                            Name = "Gxt 3.0 Preview"   
                        },
                        
                        new Ext.Net.Gantt.Task {
                            PercentDone = 10,
                            StartDate = new DateTime(2010, 01, 26),
                            Duration = 4,
                            Id = 24,
                            Leaf = true,
                            Name = "Gxt 3.0 Beta"
                        },
                        
                        new Ext.Net.Gantt.Task {
                            PercentDone = 0,
                            StartDate = new DateTime(2010, 02, 02),
                            Duration = 3,
                            Id = 25,
                            Leaf = true,
                            Name = "Gxt 3.0 Final"
                        }
                    }
                }
            }
        });

        e.Nodes.Add(
            new Ext.Net.Gantt.Task
            {
                Expanded = true,
                StartDate = new DateTime(2010, 02, 09),
                Duration = 15,
                PercentDone = 50,
                Id = 11,
                Name = "Bryntum Releases",
                Children = {
                        new Ext.Net.Gantt.Task {
                            PercentDone = 50,
                            StartDate = new DateTime(2010, 02, 09),
                            Duration = 5,
                            Id = 13,
                            Leaf = true,
                            Name = "Product X"   
                        },
                        
                        new Ext.Net.Gantt.Task {
                            PercentDone = 80,
                            StartDate = new DateTime(2010, 02, 16),
                            Duration = 5,
                            Id = 14,
                            Leaf = true,
                            Name = "Ext Scheduler 2.0"
                        },
                        
                        new Ext.Net.Gantt.Task {
                            PercentDone = 50,
                            StartDate = new DateTime(2010, 02, 23),
                            Duration = 5,
                            Id = 15,
                            Leaf = true,
                            Name = "Ext Gantt 2.0"
                        }
                    }
            }    
        );
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Calendars demo - Ext.NET Gantt</title>
    <link href="calendar.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />

    <script type="text/javascript">
        var onCalendarLoad = function(){
            var result = [];
            Ext.Array.each(Gnt.data.Calendar.getAllCalendars(), function(cal){
                result.push({ 
                    Id      : cal.calendarId, 
                    Name    : cal.name || cal.calendarId 
                });
            });

            App.CalendarStore.add(result);
        };

        var renderer  = function(value, meta, record, col, index, store){
            if (!value) {
                value = store.calendar ? store.calendar.calendarId : "";
            }
                        
            var rec = App.CalendarStore.getById(value);
                        
            return rec ? rec.get('Name') : value;
        };
    </script>
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server">
        <Listeners>
            <DocumentReady Handler="onCalendarLoad();" />
        </Listeners>
    </ext:ResourceManager>

    <h1>Calendars</h1>
        
    <p>
        This is a simple example which demonstrates the capabilities of built-in calendar system. the calendar allows the Gantt to skip non-working days during calculation of task durations.
        By default the only non-working time in calendar are weekends. Its possible to add the additional holidays or exceptions (working day which is on weekend). 
            
        Holidays/exceptions should be represented as the `Gnt.model.CalendarDay` records in the calendar store. Its also possible to apply custom styling for each holiday period.
    </p>

    <br />    

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
        <Listeners>
            <BeforeSync Handler="return false;" />
        </Listeners>
    </gnt:BusinessTimeStore>

    <gnt:BusinessTimeStore ID="HolidaysCalendar" runat="server"
        CalendarId="Holidays"
        Name="Holidays">        
        <Listeners>
            <BeforeSync Handler="return false;" />
        </Listeners>
    </gnt:BusinessTimeStore>

    <gnt:BusinessTimeStore ID="NightShiftCalendar" runat="server"
        CalendarId="NightShift"
        Name="NightShift"
        DefaultAvailability="00:00-06:00,22:00-24:00">
    </gnt:BusinessTimeStore>

    <gnt:TaskStore ID="TaskStore" runat="server"
        CalendarStoreID="RootCalendar"
        OnReadData="TasksLoad">
        <Proxy>
            <ext:PageProxy />
        </Proxy>
        <Sorters>
            <ext:DataSorter Property="leaf" Direction="ASC" />
        </Sorters>
    </gnt:TaskStore>

    <gnt:ResourceStore ID="ResourceStore" runat="server"
        ModelName="Gnt.model.Resource"
        TaskStoreID="TaskStore">        
    </gnt:ResourceStore>

    <gnt:AssignmentStore ID="AssignmentStore" runat="server"
        ResourceStoreID="ResourceStore">
    </gnt:AssignmentStore>

    <gnt:DependencyStore ID="DependencyStore" runat="server">
    </gnt:DependencyStore>

    <gnt:Gantt ID="Gantt1" runat="server"
        Width="800"
        Height="400"
        LeftLabelField="Name"
        EnableProgressBarResize="true"
        EnableDependencyDragDrop="true"
        HighlightWeekends="true"
        ViewPreset="weekAndDayLetter"
        TaskStoreID="TaskStore"
        DependencyStoreID="DependencyStore"
        AssignmentStoreID="AssignmentStore"
        ResourceStoreID="ResourceStore">
        <LockedGridConfig Width="300" />
        <ColumnModel>
            <Columns>
                <ext:Column runat="server"
                    Text="Calendar"
                    DataIndex="CalendarId"
                    Width="100">
                    <Renderer Fn="renderer" />
                    <Editor>
                        <ext:ComboBox runat="server"
                            StoreID="CalendarStore"
                            QueryMode="Local"
                            DisplayField="Name"
                            ValueField="Id"
                            Editable="false"
                            AllowBlank="false" />
                    </Editor>
                </ext:Column>

                <ext:TreeColumn runat="server" Text="Tasks" DataIndex="Name" Width="180" />
                <gnt:StartDateColumn runat="server" Width="80" />
                <gnt:EndDateColumn runat="server" Width="80" />
                <gnt:DurationColumn runat="server" Width="70" />
                <ext:Column runat="server" Text="% Done" DataIndex="PercentDone" Width="50" Align="Center" />
                <gnt:ResourceAssignmentColumn runat="server" Text="Assigned Resources" Width="150" />
            </Columns>
        </ColumnModel>
        <Plugins>
            <gnt:TreeCellEditing runat="server" ClicksToEdit="1" />
        </Plugins>
        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:Button runat="server" 
                        IconCls="gnt-date" 
                        Text="Edit working time" 
                        Handler="new Gnt.widget.calendar.CalendarWindow({
                            calendar : App.RootCalendar
                        }).show();" />

                    <ext:Button runat="server" 
                        IconCls="gnt-date" 
                        Text="Resource calendars"
                        Handler="App.getResourcesWindow(null, true).show();"/>
                </Items>
            </ext:Toolbar>
        </TopBar>
    </gnt:Gantt>

    <ext:Window runat="server"
        TemplateWidget="true"
        TemplateWidgetFnName="getResourcesWindow"
        Title="Resource calendars"
        Modal="true"
        Width="300"
        Layout="FitLayout">
        <Buttons>
            <ext:Button runat="server" 
                Text="OK"
                Handler="this.up('window').close();"/>
            <ext:Button runat="server" 
                Text="Cancel"
                Handler="this.up('window').close();"/>
        </Buttons>
        <Items>
            <gnt:ResourceCalendarGrid runat="server"
                ResourceStoreID="ResourceStore"
                CalendarStoreID="CalendarStore" />
        </Items>
    </ext:Window>
</form>
</body>
</html>