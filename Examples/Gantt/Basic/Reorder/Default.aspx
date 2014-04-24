<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.Net.X.IsAjaxRequest)
        {
            Gantt1.StartDate = new DateTime(2010, 1, 4);
            Gantt1.EndDate = new DateTime(2010, 1, 4).AddDays(140);
        }
    }

</script>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Task reordering - Ext.NET Gantt</title>    
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />    

    <script type="text/javascript">
        var appendToExt = function() {
            var task = App.TaskStore.getRootNode().findChild('Name', 'Ext 4.x branch', true);

            if (task) {
                task.appendChild(new App.TaskStore.model({
                        Name: 'Woo, added dynamically!',
                        leaf : true,
                        PercentDone: 30
                    })
                );
            }
        };

        var appendToSencha = function() {
            var task = App.TaskStore.getRootNode().findChild('Name', 'Sencha Releases');
                    
            if (task) {
                task.insertChild(0, new App.TaskStore.model({
                        Name: 'Added as first child!',
                        leaf : true,
                        PercentDone: 60,
                        StartDate : new Date(2010, 0, 6),
                        EndDate : new Date(2010, 0, 8)
                    })
                );
            }
        };
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Task reordering</h1>

        <p>
            This is a simple example showing how you can reorder your tasks easily using drag and drop in the tree section.
        </p>

       <gnt:TaskStore ID="TaskStore" runat="server">
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
                <ext:AjaxProxy Url="dependencies.xml">
                    <ActionMethods Read="GET" />
                    <Reader>
                        <ext:XmlReader Root="Links" Record="Link" />
                    </Reader>
                </ext:AjaxProxy>
            </Proxy>
        </gnt:DependencyStore>

        <gnt:Gantt ID="Gantt1" runat="server"
            Width="800"
            Height="400"
            LeftLabelField="Name"
            HighlightWeekends="false"
            EnableProgressBarResize="true"
            EnableDependencyDragDrop="false"
            CascadeChanges="false"
            ViewPreset="weekAndDayLetter"
            TaskStoreID="TaskStore"
            DependencyStoreID="DependencyStore">
            <EventRenderer Handler="return {ctcls : taskRecord.get('Id')};" />
            <ColumnModel>
                <Columns>
                    <ext:TreeColumn runat="server" Text="Tasks" DataIndex="Name" Width="200">                       
                    </ext:TreeColumn>
                    <gnt:StartDateColumn runat="server" />
                    <gnt:EndDateColumn runat="server" />
                    <gnt:PercentDoneColumn runat="server" />
                </Columns>
            </ColumnModel>
            <LockedViewConfig>
                <ext:TreeView runat="server">
                    <Plugins>
                        <ext:TreeViewDragDrop runat="server" />
                    </Plugins>
                </ext:TreeView>
            </LockedViewConfig>
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" 
                            Icon="Add"
                            Text="Add task programmatically to 'Ext 4.x branch'"
                            Handler="appendToExt" />

                        <ext:ToolbarFill />

                        <ext:Button runat="server"
                            Icon="Add"
                            Text="Add task to 'Sencha releases'" 
                            Handler="appendToSencha" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </gnt:Gantt>
    </form>
</body>
</html>
