<%@ Page Language="C#" UICulture="en-US" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            SchedulerGrid1.ResourceStore.Data = new List<object> 
            { 
                new { Id = "1xtra",  Name = "BBC Radio 1 Xtra" },
                new { Id = "radio2",  Name = "BBC Radio 2" },
                new { Id = "radio3",  Name = "BBC Radio 3" },
                new { Id = "5live",  Name = "BBC Radio 5" }
            };

            SchedulerGrid1.StartDate = DateTime.Now.Date;
            SchedulerGrid1.EndDate = DateTime.Now.Date.AddDays(1);
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>BBC Radio Schedule - Ext.Net.Scheduler Examples</title>
    
    <link href="bbc.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        Ext.data.Connection.prototype.useDefaultXhrHeader = false;

        var loadStations = function (scheduler) {
            Ext.Function.defer(function () {
                var stationStore = scheduler.resourceStore,
                    programStore = scheduler.eventStore;

                loadRow(0);

                function loadRow(index) {
                    if (index >= stationStore.getCount()) {
                        // Scroll to current time
                        scheduler.scrollToDate(Ext.Date.add(new Date(), Sch.util.Date.HOUR, -1), { duration: 2000 });

                        return;
                    }

                    // Fire a load request for each station
                    programStore.proxy.url = Ext.String.format('http://www.bbc.co.uk/{0}/programmes/schedules.json', stationStore.getAt(index).get('Id'));
                    programStore.load({
                        addRecords : true,
                        callback   : function (rs, op, success) {
                            if (success && rs && rs.length > 0) {
                                var rowIndex = stationStore.indexOfId(rs[0].get('ResourceId'));
                                Ext.Function.defer(loadRow, this, 500, [index + 1]);
                            }
                        }
                    });
                }

            }, 1);
        }
    </script>
</head>
<body style="background:#ccc;padding:20px">
    <a href="http://bbc.com/"><img src="images/bbc.png" /></a>
    
    <br />
    <br />
    
    <form runat="server">
        <ext:ResourceManager runat="server">
            <Listeners>
                <DocumentReady Handler="loadStations(#{SchedulerGrid1});" />
            </Listeners>
        </ext:ResourceManager>
        
        <h1>BBC Radio Schedule</h1>
        <p>
            This example connects to the <b><a href="http://www.bbc.co.uk/programmes/developers">BBC API</a></b>, and loads today's schedule for a few stations. 
            This is done using <a href="https://developer.mozilla.org/en/http_access_control">HTTP Access Control</a> which means you can do cross domain ajax (though this does not work in neanderthal browsers).
        </p>

        <sch:ViewPreset 
            runat="server"
            Name="hour"
            DisplayDateFormat="H:mm"
            ShiftIncrement="1"
            ShiftUnit="DAY"
            TimeColumnWidth="150">
            <TimeResolution Unit="MINUTE" Increment="5" />
            <HeaderConfig>
                <Middle Unit="HOUR" DateFormat="H:mm" Align="Left" />
            </HeaderConfig>
        </sch:ViewPreset>

        <sch:SchedulerGrid 
            ID="SchedulerGrid1"
            runat="server"
            ReadOnly="true"
            Height="346"
            Width="1100"
            RowHeight="70"
            Border="false"
            RowLines="false"
            ViewPreset="hour"
            ColumnLines="false">            
            <ResourceStore runat="server">
                <Model>
                    <ext:Model runat="server" Name="Station" Extend="Sch.model.Resource">
                        <Fields>
                            <ext:ModelField Name="Id" />
                            <ext:ModelField Name="Name" />
                            <ext:ModelField Name="Image" />
                        </Fields>
                    </ext:Model>
                </Model>
            </ResourceStore>

            <EventStore runat="server" AutoLoad="false">
                <Model>
                    <ext:Model runat="server" Name="Program" Extend="Sch.model.Event">
                        <Fields>
                            <ext:ModelField Name="ResourceId" Mapping="programme.ownership.service.key" />
                            <ext:ModelField Name="StartDate" Type="Date" DateFormat="c" Mapping="start" />
                            <ext:ModelField Name="EndDate" Type="Date" DateFormat="c" Mapping="end" />
                            <ext:ModelField Name="text" Mapping="programme.display_titles.title" />
                            <ext:ModelField Name="duration" />
                            <ext:ModelField Name="pid" Mapping="programme.pid" />
                            <ext:ModelField Name="synopsis" Mapping="programme.short_synopsis" />
                        </Fields>
                    </ext:Model>
                </Model>

                <Proxy>
                    <ext:AjaxProxy NoCache="true">
                        <Reader>
                            <ext:JsonReader Root="schedule.day.broadcasts" />
                        </Reader>
                    </ext:AjaxProxy>
                </Proxy>
            </EventStore>

            <EventBodyTemplate>
                <Html>
                    <span class="startTime">{[fm.date(values.StartDate, "G:i")]}</span>
                    <span class="programName">{text}</span>
                    <span class="duration">{[((values.duration / 60) + " min")]}</span>
                </Html>
            </EventBodyTemplate>

            <TooltipTpl>
                <Html>
                    <span class="radiotip">{[fm.date(values.StartDate, "G:i")]}</span> {synopsis}
                </Html>
            </TooltipTpl>

            <ColumnModel>
                <Columns>
                    <ext:TemplateColumn runat="server" Text="Station" Align="Center" Width="150" DataIndex="Name">
                        <Tpl>
                            <Html>
                                <img class="station-img" src="images/{Id}.png" />
                            </Html>
                        </Tpl>
                    </ext:TemplateColumn>
                </Columns>
            </ColumnModel>

            <Plugins>
                <sch:CurrentTimeLine runat="server" />
            </Plugins>

            <View>
                <sch:SchedulerGridView runat="server" BarMargin="5" />
            </View>
        </sch:SchedulerGrid>
    </form>
</body>
</html>
