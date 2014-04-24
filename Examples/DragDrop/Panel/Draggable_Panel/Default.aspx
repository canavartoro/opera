<%@ Page Language="C#" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Draggable Panel - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <ext:Panel 
            runat="server" 
            Title="Drag Me" 
            Icon="ArrowNsew"
            Frame="true" 
            Width="150" 
            Height="150" 
            Floating="true"
            Draggable="true"
            X="50"
            Y="50">            
        </ext:Panel>
        
        <ext:Panel 
            runat="server" 
            Title="Drag Me Too" 
            Icon="ArrowNsew"
            Frame="true"             
            Width="150" 
            Height="150" 
            Floating="true"
            Draggable="true"
            X="100"
            Y="100">
        </ext:Panel>
    </form>
</body>
</html>
