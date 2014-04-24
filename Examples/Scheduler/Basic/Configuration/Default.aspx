<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    public class PercentDoneEvent : Event
    {
        public int PercentDone
        {
            get;
            set;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {

            ResourceStore.Resources.AddRange(new[]
                {
                    new Resource {Id = "r1", Name = "Mike"},
                    new Resource {Id = "r2", Name = "Linda"},
                    new Resource {Id = "r3", Name = "Don"},
                    new Resource {Id = "r4", Name = "Karen"},
                    new Resource {Id = "r5", Name = "Doug"},
                    new Resource {Id = "r6", Name = "Peter"}
                });

            EventStore.Events.AddRange(new []
            {
                    new PercentDoneEvent { ResourceId = "r1", PercentDone=60,  StartDate = new DateTime(2011, 1, 1, 10, 0, 0), EndDate = new DateTime(2011, 1, 1, 12, 0, 0)},
                    new PercentDoneEvent { ResourceId = "r2", PercentDone=20,  StartDate = new DateTime(2011, 1, 1, 12, 0, 0), EndDate = new DateTime(2011, 1, 1, 13, 0, 0)},
                    new PercentDoneEvent { ResourceId = "r3", PercentDone=80,  StartDate = new DateTime(2011, 1, 1, 14, 0, 0), EndDate = new DateTime(2011, 1, 1, 16, 0, 0)},
                    new PercentDoneEvent { ResourceId = "r6", PercentDone=100, StartDate = new DateTime(2011, 1, 1, 16, 0, 0), EndDate = new DateTime(2011, 1, 1, 18, 0, 0)}
            });

            SchedulerGrid1.StartDate = new DateTime(2011, 1, 1, 6, 0, 0);
            SchedulerGrid1.EndDate = new DateTime(2011, 1, 1, 20, 0, 0);
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Configuration - Ext.Net.Scheduler Examples</title>     
    <link href="configuration.css" rel="stylesheet" />

    <script>
        function renderer (startDate, endDate, headerConfig, cellIdx) {
            // Setting align on the header config object
            headerConfig.align = 'center';

            if (startDate.getHours()===0) {
                // Setting a custom CSS on the header cell element
                headerConfig.headerCls = 'nightShift';
                return Ext.Date.format(startDate, 'M d') + ' Night Shift';
            }
            else {
                // Setting a custom CSS on the header cell element
                headerConfig.headerCls = 'dayShift';
                return Ext.Date.format(startDate, 'M d') + ' Day Shift';
            }
        }
    </script>
</head>
<body style="padding:20px;">
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Configuration</h1>
        <p>
            This example shows you the built-in view presets along with some custom made ones and zooming feature. Click the
            buttons in the toolbar to try them out. It is very easy to create your own custom view.
        </p>

        <sch:ViewPreset runat="server" Name="dayNightShift"
            TimeColumnWidth="35"
            RowHeight="32"
            DisplayDateFormat="H:mm"
            ShiftIncrement="1"
            ShiftUnit="DAY"
            DefaultSpan="24">
            <TimeResolution Unit="MINUTE" Increment="15" />
            <HeaderConfig>
                <Bottom Unit="HOUR" Increment="1" DateFormat="H" />
                <Middle Unit="HOUR" Increment="12">
                    <Renderer Fn="renderer" />
                </Middle>
                <Top Unit="DAY" Increment="1" DateFormat="dd MMM yyyy" />
            </HeaderConfig>
        </sch:ViewPreset>

        <sch:ResourceStore ID="ResourceStore" runat="server">
            <Sorters>
                <ext:DataSorter Property="Name" Direction="ASC" />
            </Sorters>            
        </sch:ResourceStore>

        <sch:EventStore ID="EventStore" runat="server">
            <Model>
                <ext:Model runat="server" Extend="Sch.model.Event">
                    <Fields>
                        <ext:ModelField Name="PercentDone" />
                    </Fields>
                </ext:Model>
            </Model>
        </sch:EventStore>

        <sch:SchedulerGrid ID="SchedulerGrid1" runat="server"
            Height="400"
            Width="800"
            RowHeight="35"
            StandardViewPreset="HourAndDay"
            EventStoreID="EventStore"
            ResourceStoreID="ResourceStore">
            <EventBodyTemplate>
                <Html>
                    <div class="value">{PercentDone}</div>
                </Html>
            </EventBodyTemplate>
            <View>
                <sch:SchedulerGridView runat="server" BarMargin="2" />
            </View>
            <ColumnModel>
                <Columns>
                    <ext:Column runat="server" Text="Name" Width="100" DataIndex="Name" />
                </Columns>
            </ColumnModel>
            <TopBar>
                <ext:Toolbar runat="server">
                    <Defaults>
                        <ext:Parameter Name="AllowDepress" Value="false" />
                    </Defaults>
                    <Items>
                        <ext:Button runat="server" 
                            Text="Hour" 
                            ToggleGroup="presets" 
                            EnableToggle="true" 
                            Pressed="true" 
                            Icon="Calendar"
                            Handler="this.up('schedulergrid').switchViewPreset('hourAndDay', new Date(2011, 0, 1, 8), new Date(2011, 0, 1, 18));" />

                        <ext:Button runat="server" 
                            Text="Days" 
                            ToggleGroup="presets" 
                            EnableToggle="true" 
                            Icon="Calendar"
                            Handler="this.up('schedulergrid').switchViewPreset('weekAndDay', new Date(2011, 0, 1), new Date(2011, 0, 21));" />

                        <ext:Button runat="server" 
                            Text="Weeks" 
                            ToggleGroup="presets" 
                            EnableToggle="true" 
                            Icon="Calendar"
                            Handler="this.up('schedulergrid').switchViewPreset('weekAndMonth');" />

                        <ext:Button runat="server" 
                            Text="Weeks 2" 
                            ToggleGroup="presets" 
                            EnableToggle="true" 
                            Icon="Calendar"
                            Handler="this.up('schedulergrid').switchViewPreset('weekAndDayLetter');" />

                        <ext:Button runat="server" 
                            Text="Weeks 3" 
                            ToggleGroup="presets" 
                            EnableToggle="true" 
                            Icon="Calendar"
                            Handler="this.up('schedulergrid').switchViewPreset('weekDateAndMonth');" />

                        <ext:Button runat="server" 
                            Text="Months" 
                            ToggleGroup="presets" 
                            EnableToggle="true" 
                            Icon="Calendar"
                            Handler="this.up('schedulergrid').switchViewPreset('monthAndYear');" />

                        <ext:Button runat="server" 
                            Text="Years" 
                            ToggleGroup="presets" 
                            EnableToggle="true" 
                            Icon="Calendar"
                            Handler="this.up('schedulergrid').switchViewPreset('year', new Date(2011, 0, 1), new Date(2015, 0, 1));" />
                        
                        <ext:ToolbarFill />
                        
                        <ext:Button runat="server" 
                            Text="Day and night shift (custom)" 
                            ToggleGroup="presets" 
                            EnableToggle="true" 
                            Icon="Calendar"
                            Handler="this.up('schedulergrid').switchViewPreset('dayNightShift', new Date(2011, 0, 1), new Date(2011, 0, 2));" />

                        <ext:Button runat="server" 
                            Text="+" 
                            Scale="Medium"
                            IconCls="zoomIn"
                            Handler="this.up('schedulergrid').zoomIn();" />

                        <ext:Button runat="server" 
                            Text="-" 
                            Scale="Medium"
                            IconCls="zoomOut"
                            Handler="this.up('schedulergrid').zoomOut();" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </sch:SchedulerGrid>
    </form>
</body>
</html>