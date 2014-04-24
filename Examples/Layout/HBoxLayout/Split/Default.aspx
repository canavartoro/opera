<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>VBoxLayout - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />
</head>
<body>
    <ext:ResourceManager runat="server" />
    
    <ext:Window runat="server" Width="600" Height="150" Title="HBox window with splitters">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" DefaultMargins="2 0" />
        </LayoutConfig>
        <Items>
            <ext:Panel runat="server" Flex="1" Title="Panel 1" CollapseDirection="Left" />

            <ext:BoxSplitter runat="server" Collapsible="true" CollapseTarget="Prev" />

            <ext:Panel runat="server" Flex="1" Title="Panel 2" CollapseDirection="Left" />

            <ext:BoxSplitter runat="server" Collapsible="true" CollapseTarget="Prev" />

            <ext:Panel runat="server" Flex="1" Title="Panel 3" />

            <ext:BoxSplitter runat="server" Collapsible="true" />

            <ext:Panel runat="server" Flex="1" Title="Panel 4" CollapseDirection="Right" />

            <ext:BoxSplitter runat="server" Collapsible="true" />

            <ext:Panel ID="Panel5" runat="server" Flex="1" Title="Panel 5" CollapseDirection="Right" />
        </Items>        
    </ext:Window>
</body>
</html>
