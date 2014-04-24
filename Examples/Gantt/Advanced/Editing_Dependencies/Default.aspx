<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Editing Dependencies - Ext.NET Gantt</title>
    <link rel="stylesheet" type="text/css" href="/resources/css/examples.css" />

    <style type="text/css">
        /* Custom styling of Special dependencies, see data set for more details */
        .Special-line {
            border-color: darkred;
            border-style: dashed;
        }

        .Special-arrow-ct .sch-dependency-arrow-left {
            border-color: transparent darkred transparent transparent;
        }

        .Special-arrow-ct .sch-dependency-arrow-right {
            border-color: transparent transparent transparent darkred;
        }

        .Special-arrow-ct .sch-dependency-arrow-down {
            border-color: darkred transparent;
        }
    </style>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <h1>Editing Dependencies Ext.NET Gantt Demo</h1>

        <p>This example shows you how you can edit dependencies by double clicking them.</p>

        <br />

        <gnt:Gantt 
            ID="Gantt1" 
            runat="server"
            Height="350"
            Width="1000"
            LeftLabelField="Name"
            HighlightWeekends="true"
            EnableDependencyDragDrop="true"
            CascadeChanges="true"
            StartDate="<%# new DateTime(2010, 1, 4) %>"
            EndDate="<%# new DateTime(2010, 1, 4).AddDays(140) %>"
            AutoDataBind="true"
            ViewPreset="weekAndDayLetter">
            <ColumnModel>
                <Columns>
                    <ext:TreeColumn 
                        runat="server"
                        Text="Tasks"
                        Sortable="true"
                        DataIndex="Name"
                        Width="200" />

                    <gnt:StartDateColumn runat="server" />
                </Columns>
            </ColumnModel>

            <TaskStore runat="server" >
                <Proxy>
                    <ext:AjaxProxy Url="tasks.xml">
                        <ActionMethods Read="GET" />
                        <Reader>
                            <ext:XmlReader Record=">Task" Root="Tasks" IDProperty="Id" />
                        </Reader>
                    </ext:AjaxProxy>
                </Proxy>
                <Sorters>
                    <ext:DataSorter Property="leaf" Direction="ASC" />
                </Sorters>
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

            <Plugins>
                <gnt:DependencyEditor runat="server" ShowLag="true" Constrain="true">
                    <FormPanel runat="server">
                        <Buttons>
                            <ext:Button 
                                runat="server" 
                                Text="Ok"
                                Handler="var formPanel = this.up('form');
                                         
                                         formPanel.getForm().updateRecord(formPanel.dependencyRecord);
                                         formPanel.collapse();"
                                />

                            <ext:Button 
                                runat="server" 
                                Text="Cancel"
                                Handler="this.up('form').collapse();" />

                            <ext:Button 
                                runat="server" 
                                Text="Delete"
                                Handler="var formPanel = this.up('form'),
                                             record = formPanel.dependencyRecord;
                                        
                                         formPanel.gantt.dependencyStore.remove(record);
                                         formPanel.collapse();" 
                                />

                        </Buttons>
                    </FormPanel>
                </gnt:DependencyEditor>
            </Plugins>
        </gnt:Gantt>
    </form>
</body>
</html>
