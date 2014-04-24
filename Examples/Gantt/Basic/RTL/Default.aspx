<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            Gantt1.StartDate = new DateTime(2010, 1, 11);
            Gantt1.EndDate = new DateTime(2010, 1, 11).AddDays(70);
        }
    }

</script>


<!DOCTYPE html>

<html>
<head runat="server">
    <title>Right To Left Gantt demo - Ext.NET Gantt</title>
    <link href="rtl.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server" />

    <h1>Right To Left Gantt demo</h1>
        
    <p>This is a simple example with basic functionality only. Tasks titles can be edited inline and you can drag/drop and resize tasks.
    To see the data loaded into the gantt chart, click these links:</p>
        
    <ul>
        <li><a href="tasks.xml">Tasks</a></li>
        <li><a href="dependencies.xml">Dependencies</a></li>
    </ul>

    <br />

    <gnt:Gantt 
        ID="Gantt1" 
        runat="server"
        Height="400"
        Width="800"
        LeftLabelField="Name"
        RTL="true"        
        EnableDependencyDragDrop="true"
        EnableProgressBarResize="true"
        CascadeChanges="false"                
        ViewPreset="weekAndDayLetter">
        <LockedGridConfig Width="200" />
        <EventRenderer Handler="return { ctcls : taskRecord.get('Id') };" />
        <TooltipTpl runat="server">
            <Html>
               <table class="taskTip">
                    <tr><td>Task </td><td>{Name}</td></tr>
                    <tr><td>Start </td><td>{[Ext.Date.format(values.StartDate, "y-m-d")]}</td></tr>
                    <tr><td>Duration</td><td> {Duration}d</td></tr>
                    <tr><td>Progress</td><td>{[Math.round(values.PercentDone)]}%</td></tr>
                </table>
            </Html>
        </TooltipTpl>
        <TaskStore ID="TaskStore1" runat="server">
            <Proxy>
                <ext:AjaxProxy Url="tasks.xml">
                    <ActionMethods Read="GET" />
                    <Reader>
                        <ext:XmlReader Record=">Task" Root="Tasks" />
                    </Reader>
                </ext:AjaxProxy>
            </Proxy>
        </TaskStore>

        <DependencyStore runat="server">
            <Proxy>
                <ext:AjaxProxy Url="dependencies.xml">
                    <ActionMethods Read="GET" />
                    <Reader>
                        <ext:XmlReader Record="Link" Root="Links" />
                    </Reader>
                </ext:AjaxProxy>
            </Proxy>
        </DependencyStore>

        <ColumnModel>
            <Columns>
                <ext:TreeColumn 
                    runat="server" 
                    Text="Tasks" 
                    Sortable="true" 
                    DataIndex="Name" 
                    Width="200">
                    <Editor>
                        <ext:TextField runat="server" AllowBlank="false" />
                    </Editor>
                </ext:TreeColumn>
                <gnt:StartDateColumn runat="server" />
                <gnt:EndDateColumn runat="server" />
                <gnt:PercentDoneColumn runat="server" />
                <gnt:AddNewColumn runat="server" />
            </Columns>
        </ColumnModel>

        <Plugins>
            <gnt:TreeCellEditing runat="server" ClicksToEdit="1" />
        </Plugins>        
    </gnt:Gantt>
</form>
</body>
</html>