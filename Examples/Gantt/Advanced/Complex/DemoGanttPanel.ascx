<%@ Control Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<gnt:Gantt 
    runat="server"
    RightLabelField="Responsible"
    HighlightWeekends="true"
    ShowTodayLine="true"
    Region="Center"
    StartDate="01.04.2010"
    EndDate="06.04.2010"
    ViewPreset="weekAndDayLetter"
    EnableProgressBarResize="true">

    <SelectionModel>
        <ext:TreeSelectionModel runat="server" IgnoreRightMouseSelection="false" Mode="Multi" />
    </SelectionModel>

    <TaskStore runat="server">
        <Model>
            <ext:Model runat="server" Extend="Gnt.model.Task">
                <CustomConfig>
                    <ext:ConfigItem Name="clsField" Value="TaskType" Mode="Value" />
                </CustomConfig>
                <Fields>
                    <ext:ModelField Name="TaskType" Type="String" />
                </Fields>
            </ext:Model>
        </Model>

        <Sorters>
            <ext:DataSorter Property="StartDate" />
        </Sorters>

        <Proxy>
            <ext:AjaxProxy Url="tasks.json">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
    </TaskStore>

    <DependencyStore runat="server">
        <Proxy>
            <ext:AjaxProxy Url="dependencies.json">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
    </DependencyStore>
    
    <LockedGridConfig Width="300" Title="Tasks" Collapsible="true" />
    
    <SchedulerConfig Collapsible="true" Title="Schedule" />

    <LeftLabelFieldConfig DataIndex="Name">
        <Editor>
            <ext:TextField runat="server" />
        </Editor>
    </LeftLabelFieldConfig>

    <Plugins>
        <gnt:TaskContextMenu runat="server" />
        <gnt:Pan runat="server" />
        <gnt:TreeCellEditing runat="server" ClicksToEdit="2" />
    </Plugins>

    <TooltipTpl>
        <Html>
            <h4 class="tipHeader">{Name}</h4>

            <table class="taskTip">
                <tr><td>Start:</td> <td align="right">{[Ext.Date.format(values.StartDate, "y-m-d")]}</td></tr>
                <tr><td>End:</td> <td align="right">{[Ext.Date.format(Ext.Date.add(values.EndDate, Ext.Date.MILLI, -1), "y-m-d")]}</td></tr>
                <tr><td>Progress:</td><td align="right">{PercentDone}%</td></tr>
            </table>
        </Html>
    </TooltipTpl>

    <ColumnModel>
        <Columns>
            <ext:Column 
                runat="server" 
                DataIndex="Id" 
                Align="Center" 
                Width="40" 
                TdCls="id" />

            <ext:TreeColumn 
                runat="server" 
                Text="Tasks" 
                Sortable="true" 
                DataIndex="Name" 
                Width="200">
                <Editor>
                    <ext:TextField runat="server" AllowBlank="false" />
                </Editor>
                <Renderer Handler="if (!record.data.leaf){metadata.tdCls = 'sch-gantt-parent-cell';} return value;" />
            </ext:TreeColumn>

            <gnt:StartDateColumn runat="server" />
            <gnt:EndDateColumn runat="server" />
            <gnt:DurationColumn runat="server" />
            <gnt:PercentDoneColumn runat="server" Width="50" />
            <gnt:PredecessorColumn runat="server" />
            <gnt:AddNewColumn runat="server" />
        </Columns>
    </ColumnModel>

    <Listeners>
        <DependencyDblClick Handler="
                var from = this.taskStore.getNodeById(record.get('From')).get('Name'),
                    to   = this.taskStore.getNodeById(record.get('To')).get('Name');                    
                Ext.Msg.alert('Hey', Ext.String.format('You clicked the link between \'{0}\' and \'{1}\'', from, to));" />
    </Listeners>

    <TopBar>
        <ext:Toolbar runat="server">
            <Items>
                <ext:ButtonGroup runat="server" Title="View Tools" Columns="3">
                    <Items>
                        <ext:Button runat="server" IconCls="icon-prev" Text="Previous">
                            <Listeners>
                                <Click Handler="this.up('ganttpanel').shiftPrevious();" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" IconCls="icon-next" Text="Next">
                            <Listeners>
                                <Click Handler="this.up('ganttpanel').shiftNext();" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" IconCls="icon-collapseall" Text="Collapse all">
                            <Listeners>
                                <Click Handler="this.up('ganttpanel').collapseAll();" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" IconCls="icon-fullscreen" Text="View full screen">
                            <Listeners>
                                <Click Handler="showFullScreen(this.up('ganttpanel'));" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" IconCls="zoomfit" Text="Zoom to fit">
                            <Listeners>
                                <Click Handler="this.up('ganttpanel').zoomToFit();" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" IconCls="icon-expandall" Text="Expand all">
                            <Listeners>
                                <Click Handler="this.up('ganttpanel').expandAll();" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:ButtonGroup>

                <ext:ButtonGroup runat="server" Columns="2" Title="View resolution">
                    <Items>
                        <ext:Button runat="server" Text="6 weeks" Handler="this.up('ganttpanel').switchViewPreset('weekAndMonth');" />
                        <ext:Button runat="server" Text="10 weeks" Handler="this.up('ganttpanel').switchViewPreset('weekAndDayLetter');" />
                        <ext:Button runat="server" Text="1 year" Handler="this.up('ganttpanel').switchViewPreset('monthAndYear');" />
                        <ext:Button runat="server" Text="5 years" Handler="var g=this.up('ganttpanel'), start=new Date(g.getStart().getFullYear(), 0); g.switchViewPreset('monthAndYear', start, Ext.Date.add(start, Ext.Date.YEAR, 5));" />
                    </Items>
                </ext:ButtonGroup>

                <ext:ButtonGroup runat="server" Columns="5" Title="Set percent complete">
                    <Defaults>
                        <ext:Parameter Name="scale" Value="large" />
                    </Defaults>

                    <Items>
                        <ext:Button runat="server" Text="0%<div class='percent percent0'></div>">
                            <Listeners>
                                <Click Handler="applyPercentDone(this.up('ganttpanel'), 0);" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" Text="25%<div class='percent percent25'><div></div></div>">
                            <Listeners>
                                <Click Handler="applyPercentDone(this.up('ganttpanel'), 25);" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" Text="50%<div class='percent percent50'><div></div></div>">
                            <Listeners>
                                <Click Handler="applyPercentDone(this.up('ganttpanel'), 50);" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" Text="75%<div class='percent percent75'><div></div></div>">
                            <Listeners>
                                <Click Handler="applyPercentDone(this.up('ganttpanel'), 75);" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button runat="server" Text="100%<div class='percent percent100'><div></div></div>">
                            <Listeners>
                                <Click Handler="applyPercentDone(this.up('ganttpanel'), 100);" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:ButtonGroup>

                <ext:ToolbarFill runat="server" />

                <ext:ButtonGroup runat="server" Title="Try some features..." Columns="3">
                    <Items>
                        <ext:Button 
                            runat="server" 
                            Text="Highlight critical chain"
                            IconCls="togglebutton"
                            EnableToggle="true">
                            <Listeners>
                                <Click Handler="var v = this.up('ganttpanel').getSchedulingView(); this.pressed ?  v.highlightCriticalPaths(true) : v.unhighlightCriticalPaths(true);" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button 
                            runat="server" 
                            Text="Highlight tasks longer than 8 days"
                            IconCls="action">
                            <Listeners>
                                <Click Handler="
                                        var g = this.up('ganttpanel'); g.taskStore.getRootNode().cascadeBy(function(task) {
                                        
                                        if (Sch.util.Date.getDurationInDays(task.get('StartDate'), task.get('EndDate')) > 8) {
                                            var el = g.getSchedulingView().getElementFromEventRecord(task);
                                            el && el.frame('lime');
                                        }
                                    }, g);" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button 
                            runat="server" 
                            Text="Filter: Tasks with progress < 30%"
                            IconCls="togglebutton"
                            EnableToggle="true"
                            ToggleGroup="filter">
                            <Listeners>
                                <Click Handler="if (this.pressed) {
                                                    this.up('ganttpanel').taskStore.filterTreeBy(function(task) { return task.get('PercentDone') < 30; }); 
                                                } else { 
                                                    this.up('ganttpanel').taskStore.clearTreeFilter(); 
                                                }" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button 
                            runat="server" 
                            Text="Cascade changes"
                            IconCls="togglebutton"
                            EnableToggle="true">
                            <Listeners>
                                <Click Handler="this.up('ganttpanel').setCascadeChanges(this.pressed);" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button 
                            runat="server" 
                            Text="Scroll to last task"
                            IconCls="action">
                            <Listeners>
                                <Click Handler="var g = this.up('ganttpanel'),
                                                    latestEndDate = new Date(0),
                                                    latest;
                                                
                                                g.taskStore.getRootNode().cascadeBy(function(task) {
                                                    if (task.get('EndDate') >= latestEndDate) {
                                                        latestEndDate = task.get('EndDate');
                                                        latest = task;
                                                    }
                                                });

                                                g.getSchedulingView().scrollEventIntoView(latest, true);" />
                            </Listeners>
                        </ext:Button>

                         <ext:TextField 
                            runat="server" 
                            EmptyText="Search for task..."
                            Width="150"
                            EnableKeyEvents="true">
                            <Listeners>
                                <KeyUp Handler="var value = this.getValue();
                                                    gantt = this.up('ganttpanel');

                                                var regexp = new RegExp(Ext.String.escapeRegex(value), 'i')

                                                if (value) {
                                                    gantt.taskStore.filterTreeBy(function (task) {
                                                        return regexp.test(task.get('Name'))
                                                    });
                                                } else {
                                                    gantt.taskStore.clearTreeFilter();
                                                }" 
                                    Buffer="300" />

                                <SpecialKey Handler="if (e.getKey() === e.ESC) {
                                                         this.reset();

                                                         this.up('ganttpanel').taskStore.clearTreeFilter();
                                                     }" />
                            </Listeners>
                        </ext:TextField>
                    </Items>                
                </ext:ButtonGroup>               
            </Items>
        </ext:Toolbar>
    </TopBar>

    <HtmlBin>
        <script type="text/javascript">
            var applyPercentDone = function (g, value) {
                g.getSelectionModel().selected.each(function (task) { task.setPercentDone(value); });
            }

            var showFullScreen = function (g) {
                if(_fullScreenFn){
                    g.el.down('.x-panel-body').dom[_fullScreenFn]();
                }
                else{
                    Ext.Msg.alert("Error", "This operation is not supported by your browser");
                }
            }
    
            var _fullScreenFn = (function () {
                var docElm = document.documentElement;
        
                if (docElm.requestFullscreen) {
                    return "requestFullscreen";
                } else if (docElm.mozRequestFullScreen) {
                    return "mozRequestFullScreen";
                } else if (docElm.webkitRequestFullScreen) {
                    return "webkitRequestFullScreen";
                }
            })();
        </script>
    </HtmlBin>
</gnt:Gantt>