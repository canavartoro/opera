<%@ Page Language="C#" UICulture="en-US" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="System.ComponentModel" %>

<script runat="server">
    public partial class SchResource : Ext.Net.Scheduler.Resource
    {
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Category
        {
            get;
            set;
        }

        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual bool? Available
        {
            get;
            set;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            this.SchedulerGrid1.ResourceStore.Data = new SchResource[]
            {
                new SchResource()
                {
                    Id = "r1",   
                    Name = "Machine 1",
                    Category = "Machines",
                    Available = true
                }, 
                new SchResource()
                {
                    Id = "r2",   
                    Name = "Machine 2",
                    Category = "Machines",
                    Available = true
                },
                new SchResource()
                {
                    Id = "r3",   
                    Name = "Machine 3",
                    Category = "Machines",
                    Available = true
                },
                new SchResource()
                {
                    Id = "r10",   
                    Name = "Robot 1",
                    Category = "Robots",
                    Available = true
                },
                new SchResource()
                {
                    Id = "r11",   
                    Name = "Robot 2",
                    Category = "Robots",
                    Available = true
                },
                new SchResource()
                {
                    Id = "r12",   
                    Name = "Robot 3 (BROKEN!)",
                    Category = "Robots",
                    Available = false
                },
            };

        }
    }
</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Validation  - Ext.Net.Scheduler Examples</title>
    <link href="validation.css" rel="stylesheet" type="text/css" />

    <script>
        Ext.ns("App.Scheduler");

        Ext.apply(App.Scheduler, {
            getScheduler: function () {
                return App.SchedulerGrid1;
            },

            onDropConfirm: function (btn) {
                this.getScheduler().__ddContext.finalize(btn === 'yes');
            },

            onResizeConfirm: function (btn) {
                this.getScheduler().__resizeContext.finalize(btn === 'yes');
            },

            onCreateConfirm: function (btn) {
                this.getScheduler().__createContext.finalize(btn === 'yes');
            },

            beforeDragCreate: function (s, resource) {
                if (!resource.get('Available')) {
                    Ext.Msg.alert('Oops', "This machine is not available");
                    return false;
                }
            },

            beforeEventDrag: function (s, r) {
                return r.get('Group') !== 'non-movable';
            },

            beforeEventResize: function (s, r) {
                if (r.get('Group') === 'non-resizable') {
                    Ext.Msg.alert('Oops', "DOH!");
                    return false;
                }
            },

            beforeEventDropFinalize: function (s, dragContext) {
                if (dragContext.resourceRecord !== dragContext.newResource) {
                    // Save reference to drag drop context to be able to finalize drop operation after user clicks yes/no button.
                    this.getScheduler().__ddContext = dragContext;

                    Ext.Msg.confirm('Please confirm',
                                    'Do you want to move the task to ' + dragContext.newResource.getName(),
                                    App.Scheduler.onDropConfirm, // The button callback
                                    App.Scheduler);          // scope
                    return false;
                }
            },

            beforeEventResizeFinalize: function (s, resizeContext) {
                // Save reference to context to be able to finalize drop operation after user clicks yes/no button.
                this.getScheduler().__resizeContext = resizeContext;

                Ext.Msg.confirm('Please confirm',
                                'Do you want to update the task: ' + resizeContext.eventRecord.get('Title'),
                                App.Scheduler.onResizeConfirm,  // The button callback
                                App.Scheduler);                 // scope
                return false;
            },

            beforeDragCreateFinalize: function (s, createContext) {
                // Save reference to context to be able to finalize drop operation after user clicks yes/no button.
                this.getScheduler().__createContext = createContext;

                Ext.Msg.confirm('Please confirm',
                                'Do you want to create a new task?',
                                App.Scheduler.onCreateConfirm,  // The button callback
                                App.Scheduler);                 // scope
                return false;
            }
        });
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <h1>Validation Scheduler Demo</h1>

        <p>This demo shows you how you can validate and control what's allowed in your schedule. Try moving, resizing and creating bars in the chart below and see what happens. 'allowOverlap' is set to false, this means events are not 
            allowed to overlap. This will be prevented when you create, drag and resize events.</p>

        <sch:SchedulerGrid 
            ID="SchedulerGrid1"
            runat="server"
            Width="900"
            Height="500"
            AllowOverlap="false"
            ViewPreset="dayAndWeek"
            StartDate="<%# new DateTime(2011, 5, 2) %>"
            EndDate="<%# new DateTime(2011, 5, 15) %>"
            AutoDataBind="true"
            RowHeight="25"
            Border="true">
            <EventRenderer Handler="templateData.cls = 'evt-' + resourceRecord.get('Category');
                                    return eventRecord.get('Title');" />
            <ResizeValidatorFn Handler="if (eventRecord.get('Group') === 'min-one-day') {
                                            return Sch.util.Date.getDurationInDays(startDate, endDate) >= 1;
                                        }
                                        return true;" />
            <DndValidatorFn Handler="return targetResourceRecord.get('Available');" />

            <ColumnModel>
                <Columns>
                    <ext:Column 
                        runat="server"
                        Text="Machines"
                        Sortable="true"
                        Width="140"
                        DataIndex="Name">
                        <Renderer Handler="metadata.tdCls = record.get('Category');
                                           return value;" />
                    </ext:Column>
                </Columns>
            </ColumnModel>

            <View>
                <sch:SchedulerGridView runat="server">
                    <GetRowClass Handler="if (!record.get('Available')) { 
                                              return 'unavailable';
                                          }
                                          return '';" />
                </sch:SchedulerGridView>
            </View>

            <ResourceStore runat="server">
                <Model>
                    <ext:Model runat="server" Extend="Sch.model.Resource">
                        <Fields>
                            <ext:ModelField Name="Category" />
                            <ext:ModelField Name="Available" />
                        </Fields>
                    </ext:Model>
                </Model>
                <Sorters>
                    <ext:DataSorter Property="Name" Direction="ASC" />
                </Sorters>
            </ResourceStore>

            <EventStore runat="server">
                <Model>
                    <ext:Model runat="server" Extend="Sch.model.Event">
                        <Fields>
                            <ext:ModelField Name="Group" />
                            <ext:ModelField Name="Title" />
                        </Fields>
                    </ext:Model>
                </Model>
                <Proxy>
                    <ext:AjaxProxy Url="data.js" />
                </Proxy>
            </EventStore>

            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button 
                            runat="server" 
                            Icon="PreviousGreen" 
                            Scale="Medium" 
                            Handler="this.up('schedulergrid').shiftPrevious();" />

                        <ext:ToolbarFill runat="server" />

                        <ext:Button 
                            runat="server" 
                            Icon="NextGreen" 
                            Scale="Medium" 
                            Handler="this.up('schedulergrid').shiftNext();" />
                    </Items>
                </ext:Toolbar>
            </TopBar>

            <Listeners>
                <BeforeDragCreate Fn="App.Scheduler.beforeDragCreate" Scope="App.Scheduler" />
                <BeforeEventDrag Fn="App.Scheduler.beforeEventDrag" Scope="App.Scheduler" />
                <BeforeEventResize Fn="App.Scheduler.beforeEventResize" Scope="App.Scheduler" />
                <BeforeEventDropFinalize Fn="App.Scheduler.beforeEventDropFinalize" Scope="App.Scheduler" />
                <BeforeEventResizeFinalize Fn="App.Scheduler.beforeEventResizeFinalize" Scope="App.Scheduler" />
                <BeforeDragCreateFinalize Fn="App.Scheduler.beforeDragCreateFinalize" Scope="App.Scheduler" />
            </Listeners>
        </sch:SchedulerGrid>
    </form>
</body>
</html>
