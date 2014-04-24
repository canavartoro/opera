<%@ Page Language="C#" %>

<%@ Import Namespace="Ext.Net.Gantt" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.Gantt" Namespace="Ext.Net.Gantt" TagPrefix="gnt" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Advanced Demo - Ext.NET Gantt</title>
    <link href="advanced.css" rel="stylesheet" type="text/css" />
    <link href="/resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form runat="server">
    <ext:ResourceManager runat="server" />

    <ext:Viewport runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" Region="North" BodyPadding="15">
                <Content>
                    <h2>This is a more advanced example demonstrating:</h2>
                    <ul style="list-style:disc;padding-left:20px">
                        <li>Several different View resolutions.</li>
                        <li>Any "static" fields on the left can be edited inline.</li>
                        <li>Fields next to the task bars can be edited inline.</li>
                        <li>You can drag/drop and resize tasks.</li>
                        <li>Click on a parent bar, or on the arrow in the first column to expand/collapse children.</li>
                    </ul>
                </Content>
            </ext:Panel>
            <ext:UserControlLoader runat="server" Path="DemoGanttPanel.ascx" />
        </Items>
    </ext:Viewport>
</form>
</body>
</html>