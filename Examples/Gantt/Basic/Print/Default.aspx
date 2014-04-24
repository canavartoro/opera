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
            Gantt1.StartDate = new DateTime(2010, 1, 1);
            Gantt1.EndDate = new DateTime(2010, 1, 1).AddMonths(20);
        }
    }

</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Ext Gantt Print - Ext.NET Gantt</title>
    <link href="gantt-print.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />

    <script type="text/javascript">
       var printRenderer = function(task, tplData) {
            if (task.isMilestone()) {
                return;
            } else if (task.isLeaf()) {
                var availableWidth = tplData.width - 4,
                    progressWidth = Math.floor(availableWidth*task.get('PercentDone')/100);
                
                return {
                    // Style borders to act as background/progressbar
                    progressBarStyle : Ext.String.format('width:{2}px;border-left:{0}px solid #7971E2;border-right:{1}px solid #E5ECF5;', progressWidth, availableWidth - progressWidth, availableWidth)
                };
            } else {
                var availableWidth = tplData.width - 2,
                    progressWidth = Math.floor(availableWidth*task.get('PercentDone')/100);
                
                return {
                    // Style borders to act as background/progressbar
                    progressBarStyle : Ext.String.format('width:{2}px;border-left:{0}px solid #FFF3A5;border-right:{1}px solid #FFBC00;', progressWidth, availableWidth - progressWidth, availableWidth)
                };
            }
        };
    </script>
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server" />

    <h1>Ext Gantt Print</h1>
        
    <p>
        This example enables you to print the gantt chart by using a 'printable' plugin.
    </p>

    <br />    

   <gnt:TaskStore ID="TaskStore" runat="server" ModelName="Gnt.model.Task">
        <Proxy>
            <ext:AjaxProxy Url="tasks.js">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>        
    </gnt:TaskStore>

    <gnt:DependencyStore ID="DependencyStore" runat="server">
        <Proxy>
            <ext:AjaxProxy Url="dependencies.js">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>        
    </gnt:DependencyStore>

    <gnt:Gantt ID="Gantt1" runat="server"
        Width="800"
        Height="400"
        LeftLabelField="Name"
        HighlightWeekends="false"
        ViewPreset="monthAndYear"
        TaskStoreID="TaskStore"
        DependencyStoreID="DependencyStore">
        <ColumnModel>
            <Columns>
                <ext:TreeColumn runat="server" Text="Tasks" DataIndex="Name" Locked="true" Width="250" />
            </Columns>
        </ColumnModel>        
        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:ToolbarTextItem runat="server" Text="This example shows you how you can print the chart content produced by Ext Gantt." />
                    <ext:ToolbarFill />
                    <ext:Button runat="server" IconCls="icon-print" Scale="Large" Text="Print" Handler="var g=this.up('ganttpanel'); g.zoomToFit(); g.print();" />
                </Items>
            </ext:Toolbar>
        </TopBar>

        <Plugins>
            <gnt:Printable runat="server">
                <BeforePrint Handler="var v = scheduler.getSchedulingView(); this.oldRenderer = v.eventRenderer; v.eventRenderer = printRenderer;" />
                <AfterPrint Handler="var v = scheduler.getSchedulingView(); v.eventRenderer = this.oldRenderer;" />
            </gnt:Printable>
        </Plugins>
    </gnt:Gantt>
</form>
</body>
</html>