<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Scheduler" Namespace="Ext.Net.Scheduler" TagPrefix="sch" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Print - Ext.Net.Scheduler Examples</title>

    <link href="print.css" rel="stylesheet" />

    <script>
        
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <h1>Print Scheduler Demo</h1>

        <p>This shows you how to get a basic HTML print from the scheduler. For higher quality printing, we recommend
          using the export plugin to first export to PDF or PNG and then print the output.</p>

        <ext:TabPanel 
            runat="server"
            Width="900"
            Height="500">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarTextItem runat="server" Text="This example shows you how you can print the chart content produced by Ext Scheduler." />
                        <ext:ToolbarFill runat="server" />
                        <ext:Button 
                            runat="server" 
                            Icon="Printer" 
                            Text="Print" 
                            Handler="this.up('tabpanel').getActiveTab().print();" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <sch:SchedulerGrid
                    runat="server"
                    Title="Work schedule"
                    ReadOnly="true"
                    RowHeight="40"
                    StartDate="<%# new DateTime(2010, 5, 27) %>"
                    EndDate="<%# new DateTime(2010, 6, 3) %>"
                    AutoDataBind="true"
                    StandardViewPreset="DayAndWeek">

                    <ResourceStore runat="server">
                        <Proxy>
                            <ext:AjaxProxy Url="peopledata.js">
                                <Reader>
                                    <ext:JsonReader Root="people" />
                                </Reader>
                            </ext:AjaxProxy>
                        </Proxy>
                        <Sorters>
                            <ext:DataSorter Property="Name" />
                        </Sorters>
                    </ResourceStore>

                    <EventStore runat="server">
                        <Proxy>
                            <ext:AjaxProxy Url="taskdata.js">
                                <Reader>
                                    <ext:JsonReader Root="tasks" />
                                </Reader>
                            </ext:AjaxProxy>
                        </Proxy>
                    </EventStore>

                    <Plugins>
                        <sch:Printable runat="server">
                            <BeforePrint Handler="var v = scheduler.getSchedulingView();                                                  
                                                  
                                                  this.oldRenderer = v.eventRenderer;
                                                  v.eventRenderer = function (ev, res, tplData, row) {
                                                      if (row % 2 === 0) {
                                                          tplData.cls += ' specialEventType';
                                                      } else {
                                                          tplData.cls += ' normalEvent';
                                                      }
                                                      tplData.style = Ext.String.format('border-right-width:{0}px;', tplData.width);

                                                      return Ext.Date.format(ev.getStartDate(), 'M d');
                                                  }" />

                            <AfterPrint Handler="scheduler.getSchedulingView().eventRenderer = this.oldRenderer;" />
                        </sch:Printable>
                    </Plugins>

                    <EventRenderer Handler="if (resourceIndex % 2 === 0) {
                                                templateData.cls = 'specialEventType';
                                            } else {
                                                templateData.cls = 'normalEvent';
                                            }

                                            return Ext.Date.format(eventRecord.getStartDate(), 'M d');" />

                    <ColumnModel>
                        <Columns>
                            <ext:Column runat="server" Text="Name" DataIndex="Name" />
                        </Columns>
                    </ColumnModel>
                </sch:SchedulerGrid>
            </Items>
        </ext:TabPanel>
    </form>
</body>
</html>
