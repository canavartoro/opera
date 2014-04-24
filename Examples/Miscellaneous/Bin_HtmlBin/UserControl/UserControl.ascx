<%@ Control Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
 
<ext:Panel
    runat="server"
    Flex="1"
    Title="Panel1"
    BodyStyle="background-color: green;">
    <Bin>
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Title="Window" 
            Html="Hello! I am defined in the Bin of the Panel within the UserControl." 
            />
    </Bin>
    <HtmlBin>
        <ext:XScript runat="server">
            <script>
                 var show = function () { 
                    #{Window1}.show();
                 };
            </script>
        </ext:XScript>
    </HtmlBin>
    <Items>
        <ext:Button runat="server" Text="Show Window">
            <Listeners>
                <Click Fn="show" />
            </Listeners>
        </ext:Button>
    </Items>
</ext:Panel>
 
<ext:Panel
    runat="server"
    Flex="1"
    Title="Panel 2"
    BodyStyle="background-color: yellow;" 
    />