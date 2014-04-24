<%@ Page Language="C#" %>

<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="System.ComponentModel" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    public partial class SchEvent : Ext.Net.Scheduler.Event
    {           
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Title
        {
            get;
            set;
        }

        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Type
        {
            get;
            set;
        }
    }
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            this.Scheduler1.ResourceStore.Data = new System.Collections.Generic.List<Ext.Net.Scheduler.Resource> 
            { 
                new Ext.Net.Scheduler.Resource{ Id = "MadMike", Name = "Mike"},
                new Ext.Net.Scheduler.Resource{ Id = "JakeTheSnake", Name = "Jake"},
                new Ext.Net.Scheduler.Resource{ Id = "KingFu", Name = "King"},
                new Ext.Net.Scheduler.Resource{ Id = "BeerBrian", Name = "Brian"},
                new Ext.Net.Scheduler.Resource{ Id = "LindaAnderson", Name = "Linda"},
                new Ext.Net.Scheduler.Resource{ Id = "DonJohnson", Name = "Don"},
                new Ext.Net.Scheduler.Resource{ Id = "KarenJohnson", Name = "Karen"},
                new Ext.Net.Scheduler.Resource{ Id = "DougHendricks", Name = "Doug"},
                new Ext.Net.Scheduler.Resource{ Id = "PeterPan", Name = "Peter"}
            };

            this.Scheduler1.EventStore.Data = new System.Collections.Generic.List<SchEvent> 
            { 
                new SchEvent{ ResourceId = "MadMike", Type="Call", Title = "Assignment 1", StartDate = new DateTime(2011, 12, 9, 10, 0,0), EndDate = new DateTime(2011, 12, 9, 11, 0,0)},
                new SchEvent{ ResourceId = "KarenJohnson", Type="Call", Title = "Customer call", StartDate = new DateTime(2011, 12, 9, 14, 0,0), EndDate = new DateTime(2011, 12, 9, 16, 0,0)},
                new SchEvent{ ResourceId = "LindaAnderson", Type="Meeting", Title = "Assignment 2", StartDate = new DateTime(2011, 12, 9, 10, 0,0), EndDate = new DateTime(2011, 12, 9, 12, 0,0)}
            };

            this.Zone1.Store.Data = new System.Collections.Generic.List<Ext.Net.Scheduler.Range> 
            { 
                new Ext.Net.Scheduler.Range{ StartDate = new DateTime(2011, 12, 9, 12, 0, 0), EndDate=new DateTime(2011, 12, 9, 14, 0, 0), Cls="lunch-style"}
            };
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Orientation - Ext.Net.Scheduler Examples</title>    
    
    <link href="orientation.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var onEventCreated = function (newEventRecord) {
            // Overridden to provide some defaults before adding it to the store
            newEventRecord.set({
                Title : "Hey, let's meet",
                Type  : "Meeting"
            });
        };
    </script>
</head>
<body style="padding:20px;">
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Scheduler</h1>

        <p>Here's a simple example showing the vertical view orientation.</p>

        <sch:SchedulerGrid 
            ID="Scheduler1" 
            runat="server"
            Height="800"
            Width="900"
            RowHeight="40"
            EventBarTextField="Title"
            ViewPreset="hourAndDay"
            StartDate="<%# new DateTime(2011, 12, 9, 7, 0, 0) %>"
            EndDate="<%# new DateTime(2011, 12, 9, 20, 0, 0) %>"
            AutoDataBind="true"                        
            EventBarIconClsField="Type"            
            SnapToIncrement="true"
            EventResizeHandles="End"
            Orientation="Vertical"
            OnEventCreated="={onEventCreated}">

            <EventBodyTemplate runat="server">
                <Html>
                    <dl>
                        <dt>{[Ext.Date.format(values.StartDate, "G:i")]}</dt>
                        <dd>{Title}</dd>
                    </dl>
                </Html>
            </EventBodyTemplate>

            <EventRenderer Handler="templateData.cls = resourceRecord.data.Name; return eventRecord.data;" />
            
            <View>
                <sch:SchedulerGridView runat="server" ConstrainDragToResource="false" EventAnimations="true" />
            </View>

            <LockedViewConfig>
                <sch:SchedulerGridView runat="server" StripeRows="false">                
                    <GetRowClass Handler="return record.data.Name;" />
                </sch:SchedulerGridView>
            </LockedViewConfig>

            <ColumnModel runat="server">
                <Columns>
                    <ext:Column runat="server" Text="Name" Sortable="true" DataIndex="Name" Width="100" />
                </Columns>                
            </ColumnModel>

            <TimeAxisColumnCfg runat="server" Text="Time of day" />

            <ResourceStore runat="server" ModelName="Sch.model.Resource" />

            <EventStore runat="server">      
                <Model>
                    <ext:Model runat="server" Extend="Sch.model.Event">
                        <Fields>                            
                            <ext:ModelField Name="Title" />
                            <ext:ModelField Name="Type" />
                        </Fields>
                    </ext:Model>
                </Model>          
            </EventStore>

            <Plugins>
                <sch:Zones ID="Zone1" runat="server">
                    <Store runat="server" ModelName="Sch.model.Range" />
                </sch:Zones>
            </Plugins>

            <Listeners>
                <ColumnWidthChange Handler="#{Slider1}.setValue(0, width);" />
            </Listeners>

            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button 
                            runat="server" 
                            Text="Vertical view" 
                            Pressed="true" 
                            IconCls="icon-vertical" 
                            EnableToggle="true" 
                            ToggleGroup="orientation">
                            <Listeners>
                                <Click Handler="#{Scheduler1}.setOrientation('vertical');" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button 
                            runat="server" 
                            Text="Horizontal view" 
                            IconCls="icon-horizontal" 
                            EnableToggle="true" 
                            ToggleGroup="orientation">
                            <Listeners>
                                <Click Handler="#{Scheduler1}.setOrientation('horizontal');" />
                            </Listeners>
                        </ext:Button>

                        <ext:ToolbarFill runat="server" />

                        <ext:Button runat="server" Text="Fit Columns">
                            <Listeners>
                                <Click Handler="#{Slider1}.suspendEvents(); #{Scheduler1}.getSchedulingView().fitColumns(); #{Slider1}.resumeEvents();" />
                            </Listeners>
                        </ext:Button>

                        <ext:Label runat="server" Text="Column Width:" />

                        <ext:Slider 
                            ID="Slider1" 
                            runat="server"
                            Width="100"
                            Number="100"
                            Increment="10"
                            MinValue="30"
                            MaxValue="150">
                            <Listeners>
                                <Change Handler="#{Scheduler1}.setTimeColumnWidth(newValue, true);" />
                                <ChangeComplete Handler="#{Scheduler1}.setTimeColumnWidth(newValue);" />
                            </Listeners>
                        </ext:Slider>

                        <ext:ToolbarSpacer runat="server" />

                        <ext:Label runat="server" Text="Row Height:" />

                        <ext:Slider 
                            runat="server"
                            Width="100"
                            Number="60"
                            Increment="10"
                            MinValue="30"
                            MaxValue="150">
                            <Listeners>
                                <Change Handler="#{Scheduler1}.getSchedulingView().setRowHeight(newValue);" />
                            </Listeners>
                        </ext:Slider>
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </sch:SchedulerGrid>
    </form>
</body>
</html>