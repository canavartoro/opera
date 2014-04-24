<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>
<%@ Import Namespace="Ext.Net" %>
<%@ Import Namespace="Ext.Net.Gantt" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            Gantt1.StartDate = new DateTime(2012, 5, 1);
            Gantt1.EndDate = new DateTime(2012, 6, 1);
        }
    }

</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Gantt column filters - Ext.NET Gantt</title>
    <link href="filtering.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />

    <script type="text/javascript">
        Ext.ns("App");
        App.FiltersFeature = {
            attachEvents: function() {
                var me = this,
                    view = me.view,
                    headerCt = view.headerCt,
                    grid = me.getGridPanel();

                me.bindStore(grid.getStore(), true);

                // Listen for header menu being created
                headerCt.on('menucreate', me.onMenuCreate, me);

                view.on('refresh', me.onRefresh, me);
                grid.on({
                    scope: me,
                    beforestaterestore: me.applyState,
                    beforestatesave: me.saveState,
                    beforedestroy: me.destroy
                });

                // Add event and filters shortcut on grid panel
                grid.filters = me;
                grid.addEvents('filterupdate');
            },

            getGridPanel: function() {
                return this.view.up('tablepanel');
            }       
        };
    </script>
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server" />

    <h1>Gantt column filters</h1>
        
    <p>
        This example demonstrates in-column filtering using a modified `Ext.ux.grid.FiltersFeature`. To use the filters, click the column menu and choose 'filters'.
    </p>

    <br />    

    <gnt:TaskStore ID="TaskStore" runat="server">
        <Proxy>
            <ext:AjaxProxy Url="tasks.js">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
        <Sorters>
            <ext:DataSorter Property="StartDate" />            
        </Sorters>
    </gnt:TaskStore>

    <gnt:Gantt ID="Gantt1" runat="server"
        Width="800"
        Height="400"
        TaskStoreID="TaskStore"
        ViewPreset="weekAndDayLetter"
        HighlightWeekends="true"
        ShowTodayLine="true"
        EnableProgressBarResize="true">
        <LockedGridConfig runat="server"
            Width="400" 
            Title="Releases" 
            Collapsible="false">
            <Features>
                <ext:GridFilters runat="server" Local="true">
                    <Filters>                        
                        <ext:StringFilter DataIndex="Name" />                        
                        <ext:DateFilter DataIndex="StartDate" />
                        <ext:DateFilter DataIndex="EndDate" />
                        <ext:NumericFilter DataIndex="PercentDone" />
                    </Filters>
                    <CustomConfig>
                        <ext:ConfigItem Name="attachEvents" Value="App.FiltersFeature.attachEvents" Mode="Raw" />
                        <ext:ConfigItem Name="getGridPanel" Value="App.FiltersFeature.getGridPanel" Mode="Raw" />                        
                    </CustomConfig>
                </ext:GridFilters>
            </Features>
        </LockedGridConfig>
        <SchedulerConfig Collapsible="false" Title="Timeline schedule" />
        <LeftLabelFieldConfig DataIndex="Name">
            <Editor>
                <ext:TextField runat="server" />
            </Editor>
        </LeftLabelFieldConfig>
        <Plugins>
            <gnt:TaskContextMenu runat="server" />
            <gnt:Pan runat="server" />
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
                <ext:TreeColumn runat="server"
                    Text="Tasks"
                    DataIndex="Name"                    
                    Width="180">
                    <Editor>
                        <ext:TextField runat="server" AllowBlank="false" />
                    </Editor>
                    <Renderer Handler="if (!record.data.leaf) { metadata.tdCls = 'sch-gantt-parent-cell';} return value;" />
                </ext:TreeColumn>

                <gnt:StartDateColumn runat="server" Width="80" />
                <gnt:EndDateColumn runat="server" Filterable="true" Width="80" />
                <gnt:PercentDoneColumn runat="server" Width="50" />
            </Columns>
        </ColumnModel>
        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:ButtonGroup runat="server" Title="View tools">
                        <Items>
                            <ext:Button runat="server" Icon="ResultsetPrevious" Text="Previous" Handler="this.up('ganttpanel').shiftPrevious();" />
                            <ext:Button runat="server" Icon="ResultsetNext" Text="Next" Handler="this.up('ganttpanel').shiftNext();" />
                        </Items>
                    </ext:ButtonGroup>
                </Items>
            </ext:Toolbar>
        </TopBar>
    </gnt:Gantt>
</form>
</body>
</html>