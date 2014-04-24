<%@ Page Language="C#" UICulture="en-US" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            SchedulerTree1.ResourceStore.Root.AddRange(Ext.Net.Scheduler.Demo.Airport.Events);
            SchedulerTree1.EventStore.Data = Ext.Net.Scheduler.Demo.Airport.Resources;

            SchedulerTree1.StartDate = new DateTime(2011, 12, 2, 8, 0, 0);
            SchedulerTree1.EndDate = new DateTime(2011, 12, 2, 18, 0, 0);

            Zone1.Store.Data = new System.Collections.Generic.List<Ext.Net.Scheduler.Range> 
            { 
                new Ext.Net.Scheduler.Range{ StartDate = new DateTime(2011, 12, 2, 11, 0, 0), EndDate=new DateTime(2011, 12, 2, 13, 0, 0), Cls="sch-cloud-thunder"},
                new Ext.Net.Scheduler.Range{ StartDate = new DateTime(2011, 12, 2, 15, 0, 0), EndDate=new DateTime(2011, 12, 2, 18, 0, 0), Cls="sch-cloud-sun"}
            };
        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Tree View  - Ext.Net.Scheduler Examples</title>
    <link href="tree.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var eventRenderer = function (flight, resource, meta) {
            if (resource.data.leaf) {
                meta.cls = 'leaf';
                return flight.get('Name');
            } else {
                meta.cls = 'group';
                return '&nbsp;';
            }
        };

        var getRowClass = function (r) {
            if (r.get('Id') === 3 || r.parentNode.get('Id') === 3) {
                return 'some-grouping-class';
            }

            if (r.get('Id') === 9 || r.parentNode.get('Id') === 9) {
                return 'some-other-grouping-class';
            }
        };
    </script>
</head>
<body style="padding:20px;">
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <h1>Tree Scheduler Demo</h1>
        <p>Here's an example of how you can use the scheduler tree view to visualize hierarchical data.</p>

        <sch:SchedulerTree 
            ID="SchedulerTree1" 
            runat="server"
            Title="Airport departures"
            Width="900"
            Height="500"          
            RowHeight="32"  
            ViewPreset="hourAndDay"
            MultiSelect="true"
            ColumnLines="false"
            RowLines="true">
            
            <EventStore runat="server" />

            <ResourceStore runat="server" FolderSort="true">
                <Model>
                    <ext:Model runat="server" Extend="Sch.model.Resource">
                        <Fields>                            
                            <ext:ModelField Name="Capacity" />
                        </Fields>
                    </ext:Model>
                </Model>          
            </ResourceStore>

            <EventRenderer Fn="eventRenderer" />

            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>

            <LockedGridConfig Width="300">
                <ResizableConfig Pinned="true" Handles="East" />
            </LockedGridConfig>

            <SchedulerConfig Scroll="Both" ColumnLines="false" Flex="1" />

            <ColumnModel>
                <Columns>
                    <ext:TreeColumn runat="server" Text="Name" Width="200" Sortable="true" DataIndex="Name" />
                    <ext:Column runat="server" Text="Capacity" Width="100" Sortable="true" DataIndex="Capacity" />
                    <ext:Column runat="server" Text="Some other property" Width="100" Sortable="true" DataIndex="Foo" />
                </Columns>
            </ColumnModel>

            <View>
                <sch:SchedulerTreeView runat="server">
                    <GetRowClass Fn="getRowClass" />
                </sch:SchedulerTreeView>
            </View>

            <Plugins>
                <sch:Zones ID="Zone1" runat="server">
                    <Store runat="server" ModelName="Sch.model.Range" />
                </sch:Zones>
            </Plugins>

            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:TriggerField runat="server" EmptyText="Filter resources" TriggerIcon="Clear">
                            <Listeners>
                                <TriggerClick Handler="this.setValue('');" />
                                <Change Handler="var resourceStore = this.up('schedulertree').resourceStore;
                                    
                                                 if (newValue) {
                                                    var regexps = Ext.Array.map(newValue.split(/\s+/), function (token) { return new RegExp(Ext.String.escapeRegex(token), 'i') }),
                                                        length = regexps.length;
                                                        
                            
                                                    resourceStore.filterTreeBy(function (resource) {
                                                        var name        = resource.get('Name')
                                
                                                        // blazing fast 'for' loop! :)
                                                        for (var i = 0; i < length; i++)
                                                            if (!regexps[ i ].test(name)) return false;
                                    
                                                        return true;
                                                    })
                                                } else {
                                                    resourceStore.clearTreeFilter()
                                                }" />

                                <SpecialKey Handler="if (e.keyCode === e.ESC) {
                                                         this.reset();
                                                     }" />
                            </Listeners>
                        </ext:TriggerField>
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </sch:SchedulerTree>
    </form>
</body>
</html>
