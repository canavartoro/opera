<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Mouse Sensor Distance Events - Ext.NET Examples</title>
    
    <script>
        var near = function (el, sensorEl) {
            var bar = App.Toolbar1,
                win = App.Window1;
              
            bar.setWidth(win.body.getWidth() - 2);
            bar.el.setStyle("zIndex", win.el.getStyle("zIndex")+1);
            bar.show();
            bar.getPositionEl().alignTo(sensorEl, "t-t", [0, 23]);
        };
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />
    
    <ext:Window 
        ID="Window1"
        runat="server"
        Title="Move Mouse to top of Window" 
        Width="350" 
        Height="215"
        Closable="false"
        Layout="FitLayout"
        PageX="100"
        PageY="100">
        <Items>
            <ext:TextArea runat="server" />
        </Items>
        <Plugins>
            <ext:MouseDistanceSensor 
                runat="server" 
                SensorElement="={App.Window1.header}" 
                ConstrainElement="Window1"
                Opacity="false"
                Threshold="25">
                <Listeners>
                    <Near Fn="near" />
                    <Far Handler="App.Toolbar1.hide();" />
                </Listeners>
            </ext:MouseDistanceSensor>
        </Plugins>
    </ext:Window>
    
    <ext:Toolbar 
        ID="Toolbar1" 
        runat="server" 
        Hidden="true" 
        StyleSpec="position:absolute;">
        <Items>
            <ext:Button runat="server" Text="Paste" Icon="PastePlain" />
            <ext:Button runat="server" Text="Copy" Icon="ApplicationDouble" />
            <ext:Button runat="server" Text="Cut" Icon="Cut" />
        </Items>
    </ext:Toolbar>
</body>
</html>
