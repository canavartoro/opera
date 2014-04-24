<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            List<Resource> resources = new List<Resource>();

            for (int i = 0; i < 10; i++)
            {
                resources.Add(new Resource()
                    {
                        Id = "r" + i,
                        Name = "Machine" + i
                    });
            }

            for (int i = 11; i < 17; i++)
            {
                resources.Add(new Resource()
                {
                    Id = "r" + i,
                    Name = "Robot" + (i - 10)
                });
            }

            this.SchedulerGrid1.ResourceStore.Data = resources;

            List<Event> events = new List<Event>()
            {
                new Event()
                {
                    ResourceId = "r1",
                    Name = "Event-1",
                    StartDate = DateTime.Today.AddDays(2),
                    EndDate = DateTime.Today.AddDays(6)
                },
                new Event()
                {
                    ResourceId = "r2",
                    Name = "Event-2",
                    StartDate = DateTime.Today.AddDays(6),
                    EndDate = DateTime.Today.AddDays(11)
                },
                new Event()
                {
                    ResourceId = "r3",
                    Name = "Event-3",
                    StartDate = DateTime.Today.AddDays(8),
                    EndDate = DateTime.Today.AddDays(12)
                },
                new Event()
                {
                    ResourceId = "r12",
                    Name = "Event-4",
                    StartDate = DateTime.Today.AddDays(4),
                    EndDate = DateTime.Today.AddDays(13)
                },
                new Event()
                {
                    ResourceId = "r14",
                    Name = "Event-5",
                    StartDate = DateTime.Today.AddDays(9),
                    EndDate = DateTime.Today.AddDays(12)
                },
                new Event()
                {
                    ResourceId = "r15",
                    Name = "Event-6",
                    StartDate = DateTime.Today.AddDays(7),
                    EndDate = DateTime.Today.AddDays(13)
                }
            };

            this.SchedulerGrid1.EventStore.Data = events;

            this.EventCombo.Items.AddRange(events.ConvertAll<Ext.Net.ListItem>(ev => new Ext.Net.ListItem() { Text = ev.Name }));
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Scrolling Programmatically - Ext.Net.Scheduler Examples</title>    
    
    <link href="scrollto.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Scrolling Programmatically</h1>

        <p>Here's an example of locating elements in the grid programmatically. You can scroll to a point in time, and you can also scroll to a certain event in the eventStore.</p>

        <sch:SchedulerGrid 
            ID="SchedulerGrid1"
            runat="server"
            Height="400"
            Width="700"
            ReadOnly="true"
            EventBarTextField="Name"
            StartDate="<%# DateTime.Today %>"
            EndDate="<%# DateTime.Today.AddDays(11) %>"
            AutoDataBind="true"
            StandardViewPreset="DayAndWeek"
            Border="true">

            <ResourceStore runat="server" />
            <EventStore runat="server" />

            <ColumnModel>
                <Columns>
                    <ext:Column
                        runat="server"
                        Text="Machines"
                        Sortable="true"
                        Width="150"
                        DataIndex="Name" />
                </Columns>
            </ColumnModel>

            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button 
                            ID="ButtonHiglight" 
                            runat="server" 
                            Text="Highlight after scroll" 
                            EnableToggle="true" 
                            Pressed="true" />

                        <ext:ComboBox 
                            ID="EventCombo" 
                            runat="server" 
                            TriggerAction="All"
                            Editable="false">
                            <SelectedItems>
                                <ext:ListItem Value="Event-2" />
                            </SelectedItems>
                        </ext:ComboBox>

                        <ext:Button 
                            runat="server"
                            Text="Scroll to event"
                            Icon="BulletGo"
                            Handler="var val = App.EventCombo.getValue(),
                                         doHighlight = App.ButtonHiglight.pressed,
                                         sch = this.up('schedulergrid'),
                                         rec = sch.eventStore.getAt(sch.eventStore.find('Name', val));

                                     if (rec) {
                                         sch.getSchedulingView().scrollEventIntoView(rec, doHighlight);
                                     }" />

                        <ext:ToolbarFill runat="server" />

                        <ext:ComboBox 
                            ID="TimeCombo" 
                            runat="server" 
                            TriggerAction="All"
                            Editable="false">
                            <Items>
                                <ext:ListItem Value="0" Mode="Raw" Text="Today" />
                                <ext:ListItem Value="2" Mode="Raw" Text="2 days from now" />
                                <ext:ListItem Value="10" Mode="Raw" Text="Ten days from now" />
                            </Items>
                            <SelectedItems>
                                <ext:ListItem Value="2" Mode="Raw" />
                            </SelectedItems>
                        </ext:ComboBox>

                        <ext:Button 
                            runat="server"
                            Text="Scroll to time"
                            Icon="BulletGo"
                            Handler="var val = App.TimeCombo.getValue(),
                                         sch = this.up('schedulergrid');
                                                        
                                     sch.scrollToDate(Sch.util.Date.add(Ext.Date.clearTime(new Date()), Sch.util.Date.DAY, val), true);" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </sch:SchedulerGrid>
    </form>
</body>
</html>