<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Bin and HtmlBin with UserControl - Ext.NET Examples</title>    
    <link href="/resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <h1>Bin and HtmlBin with UserControl</h1>

        <p>Demonstrates how helpful can be the Bin and HtmlBin features with UserControls.</p>
        
        <p>In this example it allows to organize the things to get the Panels participated in the layout.</p>
        
        <ext:Viewport runat="server" Layout="VBoxLayout">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
            <Items>
                <ext:UserControlLoader runat="server" Path="UserControl.ascx" />
            </Items>
        </ext:Viewport>
    </form>    
</body>
</html>