<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            ResourceStore.Data = new[] 
            {
                new {Id = "c1", Name = "BMW #1", Seats = "4"},
                new {Id = "c2", Name = "BMW #2", Seats = "4"},
                new {Id = "c3", Name = "BMW #3", Seats = "2"},
                new {Id = "c4", Name = "BMW #4", Seats = "2"},
                new {Id = "c5", Name = "BMW #5", Seats = "2"},
                new {Id = "c6", Name = "BMW #6", Seats = "4"}
            };
            
            EventStore.Events.AddRange(new []{
                new Event {ResourceId = "c1", Name = "Mike", StartDate = new DateTime(2010, 12, 09, 09, 45, 0), EndDate = new DateTime(2010, 12, 09, 11, 0, 0)},
                new Event {ResourceId= "c2", Name = "Linda", StartDate = new DateTime(2010, 12, 09, 10, 15, 0), EndDate = new DateTime(2010, 12, 09, 12, 0, 0)},
                new Event {ResourceId= "c3", Name = "Don", StartDate = new DateTime(2010, 12, 09, 13, 0, 0), EndDate = new DateTime(2010, 12, 09, 15, 0, 0)},
                new Event {ResourceId= "c4", Name = "Karen", StartDate = new DateTime(2010, 12, 09, 16, 0, 0), EndDate = new DateTime(2010, 12, 09, 18, 0, 0)},
                new Event {ResourceId= "c5", Name = "Doug", StartDate = new DateTime(2010, 12, 09, 12, 0, 0), EndDate = new DateTime(2010, 12, 09, 13, 0, 0)},
                new Event {ResourceId= "c6", Name = "Peter", StartDate = new DateTime(2010, 12, 09, 14, 0, 0), EndDate = new DateTime(2010, 12 ,09, 16, 0, 0)}
            });

            SchedulerGrid1.StartDate = new DateTime(2010, 12, 9, 8, 0, 0);
            SchedulerGrid1.EndDate = new DateTime(2010, 12, 9, 17, 0, 0);
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Charting - Ext.Net.Scheduler Examples</title>    
    <link href="charting.css" rel="stylesheet" />
    <script type="text/javascript">
        function refreshChart() {
            var data = [],
                ta = App.SchedulerGrid1.getTimeAxis(),
                start = ta.getStart(),
                end = ta.getEnd(),
                totalAllocatedTime = 0;
        
            App.EventStore.queryBy(function(eRec) {
                totalAllocatedTime += eRec.getEndDate() - eRec.getStartDate(); 
            });

            App.ResourceStore.each(function(r) {
                var carAllocatedTime = 0;
            
                App.EventStore.queryBy(function(eRec) {
                    if (eRec.getResourceId() === r.get('Id') && Sch.util.Date.intersectSpans(start, end, eRec.getStartDate(), eRec.getEndDate())) {
                       carAllocatedTime += eRec.getEndDate() - eRec.getStartDate(); 
                    }
                });
            
                data.push({
                    name : r.get('Name'),
                    usage : Math.round(100 * carAllocatedTime / totalAllocatedTime)
                });
            });
            App.ChartStore.loadData(data);
        }
    </script>
</head>
<body style="padding:20px;">
    <form runat="server">
        <ext:ResourceManager runat="server">
            
        </ext:ResourceManager>

        <h1>Charting</h1>

        <p>Here's a simple way of utilizing the powerful charting package in Ext JS 4.</p>

        <sch:ResourceStore ID="ResourceStore" runat="server">
            <Model>
                <ext:Model runat="server" Extend="Sch.model.Resource" IDProperty="Id">
                    <Fields>
                        <ext:ModelField Name="Id" />
                        <ext:ModelField Name="Name" />
                        <ext:ModelField Name="Seats" />
                    </Fields>
                </ext:Model>
            </Model>
            <Sorters>
                <ext:DataSorter Property="Name" Direction="ASC" />
            </Sorters>
        </sch:ResourceStore>

        <sch:EventStore ID="EventStore" runat="server">
            <Listeners>
                <Update Fn="refreshChart" />
                <Add Fn="refreshChart" />
                <Load Fn="refreshChart" Delay="100" />
            </Listeners>
        </sch:EventStore>

        <ext:Store ID="ChartStore" runat="server">
            <Fields>
                <ext:ModelField Name="name" />
                <ext:ModelField Name="usage" />
            </Fields>
        </ext:Store>

        <ext:Container runat="server"
            Border="false"
            Width="800"
            Height="400">
            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
            <Items>
                <sch:SchedulerGrid ID="SchedulerGrid1" runat="server"
                    Flex="1" 
                    ForceFit="true"
                    Title="Charting Demo"
                    StandardViewPreset="HourAndDay"
                    EventBarTextField="Name"
                    ResourceStoreID="ResourceStore"
                    EventStoreID="EventStore"
                    RowHeight="50"
                    SnapToIncrement="false">
                    <View>
                        <sch:SchedulerGridView runat="server" BarMargin="4" />
                    </View>
                    <ColumnModel>
                        <Columns>
                            <ext:TemplateColumn runat="server" Text="Car" Width="170" Align="Center" DataIndex="Name">
                                <Tpl>
                                    <Html>
                                        <img class="carimg" src="{Id}.jpeg" />
                                        <dl class="cardescr">
                                            <dt>{Name}</dt>
                                            <dd>{Seats} seats</dd>
                                        </dl>'
                                    </Html>
                                </Tpl>
                            </ext:TemplateColumn>
                        </Columns>
                    </ColumnModel>
                    <Listeners>
                        <DragCreateEnd Handler="newEventRecord.set('Name', 'New booking');" />
                        <ViewChange Fn="refreshChart" Delay="100" />
                    </Listeners>
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button runat="server" Icon="ResultsetPrevious" Text="Previous day" Handler="this.up('schedulergrid').shiftPrevious();" />
                                <ext:Button runat="server" Icon="ResultsetNext" Text="Next day" Handler="this.up('schedulergrid').shiftNext();" />
                                <ext:Button runat="server" Icon="TableRow" Text="Horizontal view" Pressed="true" EnableToggle="true" ToggleGroup="orientation" Handler="this.up('schedulergrid').setOrientation('horizontal');" />
                                <ext:Button runat="server" Icon="TableColumn" Text="Vertical view" EnableToggle="true" ToggleGroup="orientation" Handler="this.up('schedulergrid').setOrientation('vertical');" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </sch:SchedulerGrid>

                <ext:Panel runat="server"
                    Width="250"
                    Layout="FitLayout"
                    Border="false"
                    Padding="10">
                    <Items>
                        <ext:Chart runat="server"
                            Shadow="true"
                            InsetPadding="5"
                            Theme="Base:gradients"
                            StoreID="ChartStore">                            
                            <Series>
                                <ext:PieSeries AngleField="usage" Donut="30">
                                    <Label Field="name" Display="Rotate" Contrast="true" Font="18px Arial" />
                                </ext:PieSeries>
                            </Series>
                        </ext:Chart>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Container>
    </form>
</body>
</html>