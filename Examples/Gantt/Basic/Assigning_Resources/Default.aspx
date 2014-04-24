<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Resource Assignment Demo - Ext.NET Gantt</title>
    
    <link href="/resources/css/examples.css" rel="stylesheet" type="text/css" />
    <link href="resources.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server" />
        
    <p>This is a simple example with basic resource assignment functionality. Select the resources in the resource column by clicking and selecting in the editor.</p>

    <gnt:ResourceStore ID="ResourceStore1" runat="server" />

    <gnt:AssignmentStore ID="AssignmentStore1" runat="server" ResourceStoreID="ResourceStore1">            
        <Proxy>
            <ext:AjaxProxy Url="assignmentdata.json">
                <ActionMethods Read="GET" />
                <Reader>
                    <ext:JsonReader Root="assignments" />
                </Reader>
            </ext:AjaxProxy>
        </Proxy>
        <Listeners>
            <Load Handler="#{ResourceStore1}.loadData(this.proxy.reader.jsonData.resources);"></Load>
        </Listeners>
    </gnt:AssignmentStore>        

    <gnt:Gantt ID="Gantt1" runat="server"
        AssignmentStoreID="AssignmentStore1"
        ResourceStoreID="ResourceStore1"
        Height="400"
        Width="1000"
        MultiSelect="true"            
        HighlightWeekends="true"
        ShowTodayLine="true"            
        EnableDependencyDragDrop="false"            
        ViewPreset="weekAndDayLetter"            
        SnapToIncrement="true"                     
        StartDate="<%# new DateTime(2010, 1, 11) %>"
        EndDate="<%# new DateTime(2010, 6, 11) %>"
        AutoDataBind="true">

        <TaskStore runat="server">
            <Proxy>
                <ext:AjaxProxy Url="taskdata.json">
                    <ActionMethods Read="GET" />
                    <Reader>
                        <ext:JsonReader />
                    </Reader>
                </ext:AjaxProxy>
            </Proxy>
        </TaskStore>

        <LeftLabelFieldConfig DataIndex="Name">
            <Editor>
                <ext:TextField runat="server" />
            </Editor>
        </LeftLabelFieldConfig>
            
        <RightLabelFieldConfig DataIndex="Id">
            <Renderer Handler="return 'Id: #' + value;" />
        </RightLabelFieldConfig>

        <EventRenderer Handler="if (#{AssignmentStore1}.findExact('TaskId', taskRecord.data.Id) >= 0) { return {ctcls : 'resources-assigned'};}" />

        <ColumnModel>
            <Columns>
                <ext:TreeColumn runat="server" Text="Tasks" DataIndex="Name" Width="250" />
                <gnt:ResourceAssignmentColumn runat="server" Text="Assigned Resources" Width="150">
                    <EditorOptions InstanceName="Gnt.widget.AssignmentCellEditor">
                        <CustomConfig>
                            <ext:ConfigItem Name="assignmentStore" Value="#{AssignmentStore1}" Mode="Raw" />
                            <ext:ConfigItem Name="resourceStore" Value="#{ResourceStore1}" Mode="Raw" />
                        </CustomConfig>
                    </EditorOptions>
                </gnt:ResourceAssignmentColumn>
            </Columns>
        </ColumnModel>            
            
        <Plugins>
            <gnt:TreeCellEditing runat="server" ClicksToEdit="1" />
        </Plugins>            

        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:Button runat="server" Text="Indent" IconCls="indent">
                        <Listeners>
                            <Click Handler="
                                var sm = #{Gantt1}.lockedGrid.getSelectionModel();
                                    
                                sm.selected.each(function(t) {
                                    t.indent();
                                });" />
                        </Listeners>
                    </ext:Button>

                    <ext:Button runat="server" Text="Outdent" IconCls="outdent">
                        <Listeners>
                            <Click Handler="
                                var sm = #{Gantt1}.lockedGrid.getSelectionModel();
                                    
                                sm.selected.each(function(t) {
                                    t.outdent();
                                });" />
                        </Listeners>
                    </ext:Button>
                </Items>
            </ext:Toolbar>
        </TopBar>
    </gnt:Gantt>
</form>
</body>
</html>
