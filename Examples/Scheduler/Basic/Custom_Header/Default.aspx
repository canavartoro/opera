<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    public class PercentAllocatedEvent : Event
    {
        public int PercentAllocated
        {
            get;
            set;
        }
    }

    public class MyResource : Resource
    {
        public bool LikesBacon
        {
            get;
            set;
        }

        public bool LikesChrome
        {
            get;
            set;
        }

        public bool LikesIE6
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
                    new MyResource {Id = "r1", Name = "Mike", LikesChrome = true,  LikesIE6 = false, LikesBacon = true},
                    new MyResource {Id = "r2", Name = "Linda", LikesChrome = true,  LikesIE6 = false, LikesBacon = true},
                    new MyResource {Id = "r3", Name = "Don", LikesChrome = true,  LikesIE6 = false, LikesBacon = true},
                    new MyResource {Id = "r4", Name = "Karen", LikesChrome = true,  LikesIE6 = false, LikesBacon = true},
                    new MyResource {Id = "r5", Name = "Doug", LikesChrome = true,  LikesIE6 = false, LikesBacon = true},
                    new MyResource {Id = "r6", Name = "Crazy Pete", LikesChrome = false, LikesIE6 = true,  LikesBacon = false}
                });

            EventStore.Events.AddRange(new []
            {
                    new PercentAllocatedEvent { ResourceId = "r1", PercentAllocated=60,  StartDate = new DateTime(2010, 9, 1), EndDate = new DateTime(2011, 1, 1)},
                    new PercentAllocatedEvent { ResourceId = "r2", PercentAllocated=20,  StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 7, 1)},
                    new PercentAllocatedEvent { ResourceId = "r3", PercentAllocated=80,  StartDate = new DateTime(2011, 4, 1), EndDate = new DateTime(2011, 10, 1)},
                    new PercentAllocatedEvent { ResourceId = "r6", PercentAllocated=100, StartDate = new DateTime(2011, 7, 1), EndDate = new DateTime(2011, 10, 1)}
            });

            SchedulerGrid1.StartDate = new DateTime(2010, 10, 1);
            SchedulerGrid1.EndDate = new DateTime(2011, 10, 1);
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Custom header - Ext.Net.Scheduler Examples</title>     
    <link href="customheader.css" rel="stylesheet" />
    
    <script>
        function renderer (start, end, cfg) {
            var quarter = Math.floor(start.getMonth() / 3) + 1,
                fiscalQuarter = quarter === 4 ? 1 : (quarter + 1);
                        
            return Ext.String.format('FQ{0} {1}', fiscalQuarter, start.getFullYear() + (fiscalQuarter === 1 ? 1 : 0));
        }

        function cellGenerator (viewStart, viewEnd) {
            var cells = [];
                        
            // Simplified scenario, assuming view will always just show one US fiscal year
            return [{
                start : viewStart,
                end : viewEnd,
                header : 'Fiscal Year ' + (viewStart.getFullYear() + 1)
            }];
        }
    </script>
</head>
<body style="padding:20px;">
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Custom header</h1>

        <sch:ViewPreset runat="server" Name="fiscalYear"
            TimeColumnWidth="90"
            DisplayDateFormat="yyyy-MM-dd"
            ShiftIncrement="1"
            ShiftUnit="YEAR">
            <TimeResolution Unit="MONTH" Increment="1" />
            <HeaderConfig>
                <Bottom Unit="MONTH" DateFormat="MMM yyyy" />
                <Middle Unit="QUARTER">
                    <Renderer Fn="renderer" />
                </Middle>
                <Top Unit="YEAR">
                    <CellGenerator Fn="cellGenerator" />
                </Top>
            </HeaderConfig>
        </sch:ViewPreset>

        <sch:ResourceStore ID="ResourceStore" runat="server">
           <Model>
               <ext:Model runat="server" Extend="Sch.model.Resource">
                   <Fields>
                       <ext:ModelField Name="LikesBacon" />
                       <ext:ModelField Name="LikesChrome" />
                       <ext:ModelField Name="LikesIE6" />
                   </Fields>
               </ext:Model>
           </Model>     
        </sch:ResourceStore>

        <sch:EventStore ID="EventStore" runat="server">
            <Model>
                <ext:Model runat="server" Extend="Sch.model.Event">
                    <Fields>
                        <ext:ModelField Name="PercentAllocated" />
                    </Fields>
                </ext:Model>
            </Model>
        </sch:EventStore>

        <sch:SchedulerGrid ID="SchedulerGrid1" runat="server"
            Title="US Fiscal Year"
            Height="400"
            Width="800"
            RowHeight="35"
            ViewPreset="fiscalYear"
            EventStoreID="EventStore"
            ResourceStoreID="ResourceStore">
            <EventBodyTemplate>
                <Html>
                    <div class="sch-percent-allocated-bar" style="height:{PercentAllocated}%"></div>
                    <span class="sch-percent-allocated-text">{[values.PercentAllocated||0]}%</span>
                </Html>
            </EventBodyTemplate>
            <ColumnModel>
                <Columns>
                    <ext:Column runat="server" Text="Name" Width="100" DataIndex="Name" />
                    <ext:BooleanColumn runat="server" DataIndex="LikesBacon" Text="Likes Bacon" Align="Center" Cls="vertical" Width="40" TrueText="Yes" FalseText="No" />
                    <ext:BooleanColumn runat="server" DataIndex="LikesIE6" Text="Likes IE6" Align="Center" Cls="vertical" Width="40" TrueText="Yes" FalseText="No" />
                    <ext:BooleanColumn runat="server" DataIndex="LikesChrome" Text="Likes Chrome" Align="Center" Cls="vertical" Width="40" TrueText="Yes" FalseText="No" />
                </Columns>
            </ColumnModel>
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server"                             
                            Icon="ResultsetPrevious"
                            Handler="this.up('schedulergrid').shiftPrevious();" />

                        <ext:Button runat="server"                             
                            Icon="ResultsetNext"
                            Handler="this.up('schedulergrid').shiftNext();" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </sch:SchedulerGrid>
    </form>
</body>
</html>