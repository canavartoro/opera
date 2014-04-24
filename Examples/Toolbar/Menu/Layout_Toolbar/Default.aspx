<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Layout Toolbar - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />
</head>
<body>
    <ext:ResourceManager runat="server" />
    
    <h3>Vertical Flat Toolbar</h3>
    
    <ext:Toolbar runat="server" Layout="Container" Width="25" Flat="true">
        <Items>
            <ext:Button runat="server" Icon="Accept" />
            <ext:Button runat="server" Icon="Add" />
            <ext:Button runat="server" Icon="Application" />
            <ext:Button runat="server" Icon="Bell" />
            <ext:Button runat="server" Icon="Bomb" />
        </Items>
    </ext:Toolbar>
    
    <h3>Table Toolbar</h3>
    
    <ext:Panel runat="server" Title="Panel" Height="150" Width="348">
        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:Toolbar runat="server" Width="109" Flat="false" Layout="TableLayout">
                        <LayoutConfig>
                            <ext:TableLayoutConfig Columns="3" />
                        </LayoutConfig>
                        <Defaults>
                            <ext:Parameter Name="width" Value="33" Mode="Raw" />
                        </Defaults>
                        <Items>                            
                            <ext:Button runat="server" Text="1" StandOut="true" />
                            <ext:Button runat="server" Text="2" StandOut="true" />
                            <ext:Button runat="server" Text="3" StandOut="true" />
                            <ext:Button runat="server" Text="4" StandOut="true" />
                            <ext:Button runat="server" Text="5" StandOut="true" />
                            <ext:Button runat="server" Text="6" StandOut="true" />
                            <ext:Button runat="server" Text="7" StandOut="true" />
                            <ext:Button runat="server" Text="8" StandOut="true" />
                            <ext:Button runat="server" Text="9" StandOut="true" />
                        </Items>
                    </ext:Toolbar>
                    
                    <ext:ToolbarSeparator runat="server" />
                    
                    <ext:Toolbar runat="server" Width="109" Flat="false" Layout="TableLayout">
                        <LayoutConfig>
                            <ext:TableLayoutConfig Columns="3" />
                        </LayoutConfig>
                        <Defaults>
                            <ext:Parameter Name="width" Value="33" Mode="Raw" />
                        </Defaults>
                        <Items>                            
                            <ext:Button runat="server" Text="1" StandOut="true" />
                            <ext:Button runat="server" Text="2" StandOut="true" />
                            <ext:Button runat="server" Text="3" StandOut="true" />
                            <ext:Button runat="server" Text="4" StandOut="true" Width="103" ColSpan="3" />
                            <ext:Button runat="server" Text="5" StandOut="true" />
                            <ext:Button runat="server" Text="6" StandOut="true" />
                            <ext:Button runat="server" Text="7" StandOut="true" />
                        </Items>
                    </ext:Toolbar>
                    
                    <ext:ToolbarSeparator runat="server" />
                    
                    <ext:Toolbar runat="server" Width="105" Flat="false" Layout="TableLayout">
                        <LayoutConfig>
                            <ext:TableLayoutConfig Columns="1" />
                        </LayoutConfig>
                        <Defaults>
                            <ext:Parameter Name="width" Value="99" Mode="Raw" />
                        </Defaults>
                        <Items>                            
                            <ext:Button runat="server" Text="1" Icon="BulletTick" />
                            <ext:Button runat="server" Text="2" Icon="BulletTick" />
                            <ext:Button runat="server" Text="3" Icon="BulletTick" />
                        </Items>
                    </ext:Toolbar>
                </Items>
            </ext:Toolbar>
        </TopBar>
    </ext:Panel>
    
    <h3>Vertical Toolbar In Panel</h3>

    <ext:Panel 
        runat="server" 
        Title="Vertical Toolbar" 
        Width="300" 
        Height="150"
        BodyPadding="5"        
        Html="Your Content">
        <DockedItems>
            <ext:Toolbar runat="server" Dock="Left" Vertical="true">
                <Items>
                    <ext:Button runat="server" Icon="Accept" />
                    <ext:Button runat="server" Icon="Add" />
                    <ext:Button runat="server" Icon="Application" />
                    <ext:Button runat="server" Icon="Bell" />
                    <ext:Button runat="server" Icon="Bomb" />
                </Items>
            </ext:Toolbar>
        </DockedItems>
    </ext:Panel>
    
    <h3>Multiple Toolbars</h3>
    
    <ext:Panel runat="server" Title="Panel" Width="300" Height="150">
        <DockedItems>
            <ext:Toolbar runat="server" Dock="Top">
                <Items>
                    <ext:Button runat="server" Icon="Accept" />
                    <ext:Button runat="server" Icon="Add" />
                    <ext:Button runat="server" Icon="Application" />
                    <ext:Button runat="server" Icon="Bell" />
                    <ext:Button runat="server" Icon="Bomb" />
                </Items>
            </ext:Toolbar>
                    
            <ext:Toolbar runat="server" Dock="Top">
                <Items>
                    <ext:Button runat="server" Icon="Accept" />
                    <ext:Button runat="server" Icon="Add" />
                    <ext:Button runat="server" Icon="Application" />
                    <ext:Button runat="server" Icon="Bell" />
                    <ext:Button runat="server" Icon="Bomb" />
                </Items>
            </ext:Toolbar>
                    
            <ext:Toolbar runat="server" Dock="Top">
                <Items>
                    <ext:Button runat="server" Icon="Accept" />
                    <ext:Button runat="server" Icon="Add" />
                    <ext:Button runat="server" Icon="Application" />
                    <ext:Button runat="server" Icon="Bell" />
                    <ext:Button runat="server" Icon="Bomb" />
                </Items>
            </ext:Toolbar>
        </DockedItems>                   
    </ext:Panel>
</body>
</html>