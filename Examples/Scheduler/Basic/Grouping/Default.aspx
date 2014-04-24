<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Grouping - Ext.Net.Scheduler Examples</title>

    <link href="grouping.css" rel="stylesheet" />

    <script>
        var onEventContextMenu = function (g, rec, e) {
            e.stopEvent();

            if (!g.gCtx) {
                g.gCtx = new Ext.menu.Menu({
                    items : [
                        {
                            text    : 'Delete event',
                            iconCls : 'icon-delete',
                            handler : function () {
                                g.eventStore.remove(g.gCtx.rec);
                            }
                        },
                        {
                            text    : 'Delete selected events',
                            iconCls : 'icon-deleteall',
                            handler : function () {
                                g.getSelectionModel().selected.each(function (r) {
                                    g.eventStore.remove(r);
                                });
                            }
                        }
                    ]
                });
            }
            g.gCtx.rec = rec;
            g.gCtx.showAt(e.getXY());
        };
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <h1>Grouping Scheduler Demo</h1>

        <p>This example shows how you can group resources.</p>

        <div class="simple preload" style="height:0;width:0;overflow:hidden">
            <img src="images/event-big.png" />
            <img src="images/event-big4.png" />
            <img src="images/event-big2.png" />
            <img src="images/event-big3.png" />
        </div>

        <sch:SchedulerGrid
            runat="server"
            Width="900"
            Height="500"
            MultiSelect="true"
            StartDate="<%# new DateTime(2007, 1, 1) %>"
            EndDate="<%# new DateTime(2007, 2, 12) %>"
            AutoDataBind="true"
            StandardViewPreset="WeekAndMonth"
            RowHeight="55"
            Border="true">
            <EventRenderer Handler="templateData.cls = resourceRecord.get('Category');

                                    // Add data to be applied to the event template
                                    return Ext.Date.format(eventRecord.getStartDate(), 'Y-m-d');" />
            <Features>
                <ext:Grouping 
                    runat="server" 
                    GroupHeaderTplString="{name}" 
                    HideGroupedHeader="true" 
                    EnableGroupingMenu="false" />
            </Features>

            <ColumnModel>
                <Columns>
                    <ext:Column runat="server" Text="Projects" DataIndex="Category" />

                    <ext:Column 
                        runat="server" 
                        Text="Staff" 
                        Sortable="true" 
                        Width="140" 
                        DataIndex="Name" />

                    <ext:Column 
                        runat="server" 
                        Text="Employment type" 
                        Sortable="true" 
                        Width="140"
                        DataIndex="Type" />
                </Columns>
            </ColumnModel>

            <ResourceStore ID="ResourceStore1" runat="server" GroupField="Category">
                <Model>
                    <ext:Model runat="server" Extend="Sch.model.Resource">
                        <Fields>
                            <ext:ModelField Name="Category" />
                            <ext:ModelField Name="Type" />
                        </Fields>
                    </ext:Model>
                </Model>
                <Proxy>
                    <ext:AjaxProxy Url="resources.js" />
                </Proxy>
            </ResourceStore>

            <EventStore runat="server">
                <Proxy>
                    <ext:AjaxProxy Url="dummydata.js" />
                </Proxy>
            </EventStore>

            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button
                            runat="server"
                            Scale="Medium"
                            Icon="PreviousGreen"
                            Handler="this.up('schedulergrid').shiftPrevious();" />

                        <ext:ToolbarFill runat="server" />

                        <ext:Button
                            runat="server"
                            Scale="Medium"
                            IconCls="icon-cleardatabase"
                            ToolTip="Clear database"
                            Handler="this.up('schedulergrid').eventStore.removeAll();" />

                        <ext:Button 
                            runat="server"
                            IconCls="icon-medium"
                            EnableToggle="true"
                            ToggleGroup="rowsize"
                            ToolTip="Medium size"
                            Scale="Medium"
                            Handler="var g = this.up('schedulergrid');
                                     
                                     g.getSchedulingView().setRowHeight(26, true);
                                     g.getView().refresh();" />

                        <ext:Button 
                            runat="server"
                            IconCls="icon-large"
                            EnableToggle="true"
                            ToggleGroup="rowsize"
                            ToolTip="Full size"
                            Scale="Medium"
                            Handler="var g = this.up('schedulergrid');
                                     
                                     g.getSchedulingView().setRowHeight(55, true);
                                     g.getView().refresh();" />

                        <ext:Button
                            runat="server"
                            Scale="Medium"
                            Icon="NextGreen"
                            Handler="this.up('schedulergrid').shiftNext();" />
 
                    </Items>
                </ext:Toolbar>
            </TopBar>

            <Listeners>
                <EventContextMenu Fn="onEventContextMenu" />
            </Listeners>
        </sch:SchedulerGrid>
    </form>
</body>
</html>
