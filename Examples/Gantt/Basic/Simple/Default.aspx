<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Basic Demo - Ext.NET Gantt</title>
    <link href="basic.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />

    <script type="text/javascript">
        var addTask = function () {
            var g = this.up("ganttpanel"),
                taskStore = g.taskStore,
                newTask = new taskStore.model({
                    Name : 'New task',
                    leaf : true,
                    PercentDone : 0
                });

            taskStore.getRootNode().appendChild(newTask);

            //edit the new task
            g.lockedGrid.editingPlugin.startEdit(newTask, g.lockedGrid.columns[0]);
        };
    </script>
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server" />

    <h1>Basic Ext.NET Gantt Demo</h1>
        
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
        Height="350"
        Width="1000"
        LeftLabelField="Name"
        HighlightWeekends="true"
        EnableDependencyDragDrop="true"
        EnableProgressBarResize="true"
        CascadeChanges="false"
        StartDate="01.04.2010"
        EndDate="03.18.2010"
        AutoDataBind="true"
        ViewPreset="weekAndDayLetter">
        <EventRenderer Handler="return { ctcls : taskRecord.get('Id') };" />
        <TooltipTpl runat="server">
            <Html>
                <ul class="taskTip">
                    <li><strong>Task:</strong>{Name}</li>
                    <li><strong>Start:</strong>{[Ext.Date.format(values.StartDate, "y-m-d")]}</li>
                    <li><strong>Duration:</strong> {Duration}d</li>
                    <li><strong>Progress:</strong>{PercentDone}%</li>
                </ul>
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
            <gnt:TreeCellEditing runat="server" ClicksToEdit="1">
                <Listeners>
                    <BeforeEdit Handler="return !#{ReadOnlyMode}.pressed;" />
                </Listeners>
            </gnt:TreeCellEditing>
        </Plugins>
        
        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:Button 
                        runat="server" 
                        Text="Add new task..." 
                        Icon="Add"
                        Handler="addTask" 
                        />

                    <ext:Button 
                        ID="ReadOnlyMode"
                        runat="server" 
                        EnableToggle="true" 
                        Text="Read only mode" 
                        Pressed="false"
                        Handler="#{Gantt1}.setReadOnly(this.pressed)"
                        />

                    <ext:ToolbarFill runat="server" />

                    <ext:Label runat="server" Text="Column Width" />

                    <ext:Slider runat="server"
                        Width="120"
                        Number="20"
                        MinValue="20"
                        MaxValue="240"
                        Increment="10">
                        <Listeners>
                            <Change Handler="#{Gantt1}.setTimeColumnWidth(newValue, true);" />
                            <ChangeComplete Handler="#{Gantt1}.setTimeColumnWidth(newValue);" />
                        </Listeners>
                    </ext:Slider>
                </Items>
            </ext:Toolbar>
        </TopBar>
    </gnt:Gantt>
</form>
</body>
</html>