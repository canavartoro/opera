<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            SchedulerGrid1.ResourceStore = new ResourceStore
            {
                Resources =
                {
                    new Resource {Id = "r1", Name = "Mike"},
                    new Resource {Id = "r2", Name = "Linda"},
                    new Resource {Id = "r3", Name = "Don"},
                    new Resource {Id = "r4", Name = "Karen"},
                    new Resource {Id = "r5", Name = "Doug"},
                    new Resource {Id = "r6", Name = "Peter"}
                }
            };

            SchedulerGrid1.EventStore = new Ext.Net.Scheduler.EventStore
            {
                Events = 
                { 
                    new Event { Id = "e10", ResourceId= "r1",  Name = "Assignment 1", StartDate = new DateTime(2010, 12, 02), EndDate = new DateTime(2010, 12, 03)},
                    new Event { Id = "e11", ResourceId= "r2",  Name = "Assignment 2", StartDate = new DateTime(2010, 12, 04), EndDate = new DateTime(2010, 12, 07)},
                    new Event { Id = "e21", ResourceId= "r3",  Name = "Assignment 3", StartDate = new DateTime(2010, 12, 01), EndDate = new DateTime(2010, 12, 04)},
                    new Event { Id = "e22", ResourceId= "r4",  Name = "Assignment 4", StartDate = new DateTime(2010, 12, 05), EndDate = new DateTime(2010, 12, 07)},
                    new Event { Id = "e32", ResourceId= "r5",  Name = "Assignment 5", StartDate = new DateTime(2010, 12, 07), EndDate = new DateTime(2010, 12, 11)},
                    new Event { Id = "e33", ResourceId= "r6",  Name = "Assignment 6", StartDate = new DateTime(2010, 12, 09), EndDate = new DateTime(2010, 12, 11)}
                }
            };
            
            SchedulerGrid1.StartDate = new DateTime(2010, 12, 1);
            SchedulerGrid1.EndDate = new DateTime(2010, 12, 14);
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Column summary - Ext.Net.Scheduler Examples</title> 
    <link href="columnsummary.css" rel="stylesheet" />
    <script type="text/javascript">
        function iterceptAnchorClick (sched) {
            var lockedSection = sched.lockedGrid,
                view = lockedSection.getView();

            lockedSection.el.on('click', function(e, t) {   
                var rowNode = view.findItemByChild(t),
                    resource = view.getRecord(rowNode);
                Ext.Msg.alert('Hey', 'You clicked ' + resource.get('Name'));
                e.stopEvent();
            }, null, { delegate : '.mylink' });
        }
    </script>
</head>
<body style="padding:20px;">
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Column summary</h1>

        <ext:Viewport runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel runat="server" Title="North Panel" Region="North" Height="100" BodyPadding="10">
                    <Content>
                        This example shows you the Sch.plugin.SummaryColumn plugin which can show either the amount of time or the percentage allocated within the visible view.                        
                    </Content>
                </ext:Panel>

                <ext:Panel runat="server" Title="West Panel" Region="West" Width="200" BodyPadding="10" Html="Some content..." />

                <ext:TabPanel runat="server" Region="Center" ActiveTabIndex="1">
                    <Items>
                        <ext:Panel runat="server" Title="Tab 1 - Some other component" />

                        <sch:SchedulerGrid ID="SchedulerGrid1" runat="server" 
                            EventBarTextField="Name" 
                            StandardViewPreset="DayAndWeek"
                            RowHeight="36"
                            Title="Tab 2 - Scheduler"
                            EventResizeHandles="Both">
                            <ColumnModel>
                                <Columns>
                                    <ext:Column runat="server" Text="Name" Width="200" DataIndex="Name" />
                                    <ext:Column runat="server" Text="Some link" Width="80" Locked="true">
                                        <Renderer Handler="return '<a class=\'mylink\' href=\'#\'>Click me!</a>';" />
                                    </ext:Column>
                                    <sch:SummaryColumn runat="server" Text="Time allocated" Width="80" ShowPercent="false" />
                                    <sch:SummaryColumn runat="server" Text="% allocated" Width="60" ShowPercent="true" Align="Center" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <DragCreateEnd Handler="newEventRecord.setName('New task...');" />
                                <AfterRender Fn="iterceptAnchorClick" />
                            </Listeners>
                        </sch:SchedulerGrid>
                    </Items>
                </ext:TabPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>