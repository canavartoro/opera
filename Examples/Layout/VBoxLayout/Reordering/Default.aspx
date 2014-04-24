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
    
    <ext:Window runat="server" Width="250" Height="500" Title="VBox window (drag below panels)">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="2" />
        </LayoutConfig>
        <Items>
            <ext:Panel runat="server" Frame="true" Flex="1" Title="Panel 1" />
            <ext:Panel runat="server" Frame="true" Flex="1" Title="Panel 2" />
            <ext:Panel runat="server" Frame="true" Flex="1" Title="Panel 3" />
            <ext:Panel runat="server" Frame="true" Flex="1" Title="Panel 4" />
            <ext:Panel runat="server" Frame="true" Flex="1" Title="Panel 5" />
        </Items>
        <Plugins>
            <ext:BoxReorderer runat="server" />
        </Plugins>
    </ext:Window>
</body>
</html>
