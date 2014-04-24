<%@ Page Language="C#" %>

<%@ Import Namespace="ListItem=Ext.Net.ListItem" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack && ThemeSelector.SelectedItems.Count > 0)
        {
            ListItem themeItem = ThemeSelector.SelectedItem;
                
            switch (themeItem.Value)
            {
                case "0":
                    this.ResourceManager1.Theme = Ext.Net.Theme.Default;
                    break;
                case "1":
                    this.ResourceManager1.Theme = Ext.Net.Theme.Gray;
                    break;
                case "2":
                    this.ResourceManager1.Theme = Ext.Net.Theme.Neptune;
                    break;
            }
        }            
    }
</script>


<!DOCTYPE html>

<html>
<head runat="server">
    <title>Button UI Styles - Ext.NET Examples</title>
</head>
<body style="padding:30px;">
    <form runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" SeparateUIStyles="false" />

        <ext:ComboBox 
            ID="ThemeSelector" 
            runat="server" 
            FieldLabel="Theme" 
            AutoPostBack="true">
            <Items>
                <ext:ListItem Text="Classic" Value="0" />
                <ext:ListItem Text="Gray" Value="1" />
                <ext:ListItem Text="Neptune" Value="2" />
            </Items>
        </ext:ComboBox>

        <ext:Menu ID="TestMenu" runat="server">
            <Items>
                <ext:MenuItem runat="server" Text="Menu Item" />
            </Items>
        </ext:Menu>

        <p>To use color buttons styles just set UI property of Button: </p>

        <pre>
            &lt;ext:Button runat="server" Text="Small" UI="Primary" /&gt;
        </pre>

        <h1>Default</h1>

        <ext:Container runat="server">
            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Middle" DefaultMargins="5" />
            </LayoutConfig>
            <Items>
                <ext:Button runat="server" Text="Small" />
                <ext:Button runat="server" Text="Medium" Scale="Medium" />
                <ext:Button runat="server" Text="Large" Scale="Large" />

                <ext:Component runat="server" Width="50" />

                <ext:Button runat="server" Text="Menu Small" XMenu="TestMenu" />
                <ext:Button runat="server" Text="Menu Medium" Scale="Medium" XMenu="TestMenu" />
                <ext:Button runat="server" Text="Menu Large" Scale="Large"  XMenu="TestMenu" />

                <ext:Component runat="server" Width="50" />

                <ext:SplitButton runat="server" Text="Split Small" XMenu="TestMenu" />
                <ext:SplitButton runat="server" Text="Split Medium" Scale="Medium" XMenu="TestMenu" />
                <ext:SplitButton runat="server" Text="Split Large" Scale="Large"  XMenu="TestMenu" />
            </Items>
        </ext:Container>

        <h1>Primary (UI="Primary")</h1>

        <ext:Container runat="server">
            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Middle" DefaultMargins="5" />
            </LayoutConfig>
            <Items>
                <ext:Button runat="server" Text="Small" UI="Primary" />
                <ext:Button runat="server" Text="Medium" Scale="Medium" UI="Primary" />
                <ext:Button runat="server" Text="Large" Scale="Large" UI="Primary" />

                <ext:Component runat="server" Width="50" />

                <ext:Button runat="server" Text="Menu Small" XMenu="TestMenu" UI="Primary" />
                <ext:Button runat="server" Text="Menu Medium" Scale="Medium" XMenu="TestMenu" UI="Primary" />
                <ext:Button runat="server" Text="Menu Large" Scale="Large"  XMenu="TestMenu" UI="Primary" />

                <ext:Component runat="server" Width="50" />

                <ext:SplitButton runat="server" Text="Split Small" XMenu="TestMenu" UI="Primary" />
                <ext:SplitButton runat="server" Text="Split Medium" Scale="Medium" XMenu="TestMenu" UI="Primary" />
                <ext:SplitButton runat="server" Text="Split Large" Scale="Large"  XMenu="TestMenu" UI="Primary" />
            </Items>
        </ext:Container>

        <h1>Danger (UI="Danger")</h1>

        <ext:Container runat="server">
            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Middle" DefaultMargins="5" />
            </LayoutConfig>
            <Items>
                <ext:Button runat="server" Text="Small" UI="Danger" />
                <ext:Button runat="server" Text="Medium" Scale="Medium" UI="Danger" />
                <ext:Button runat="server" Text="Large" Scale="Large" UI="Danger" />

                <ext:Component runat="server" Width="50" />

                <ext:Button runat="server" Text="Menu Small" XMenu="TestMenu" UI="Danger" />
                <ext:Button runat="server" Text="Menu Medium" Scale="Medium" XMenu="TestMenu" UI="Danger" />
                <ext:Button runat="server" Text="Menu Large" Scale="Large"  XMenu="TestMenu" UI="Danger" />

                <ext:Component runat="server" Width="50" />

                <ext:SplitButton runat="server" Text="Split Small" XMenu="TestMenu" UI="Danger" />
                <ext:SplitButton runat="server" Text="Split Medium" Scale="Medium" XMenu="TestMenu" UI="Danger" />
                <ext:SplitButton runat="server" Text="Split Large" Scale="Large"  XMenu="TestMenu" UI="Danger" />
            </Items>
        </ext:Container>

        <h1>Info (UI="Info")</h1>

        <ext:Container runat="server">
            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Middle" DefaultMargins="5" />
            </LayoutConfig>
            <Items>
                <ext:Button runat="server" Text="Small" UI="Info" />
                <ext:Button runat="server" Text="Medium" Scale="Medium" UI="Info" />
                <ext:Button runat="server" Text="Large" Scale="Large" UI="Info" />

                <ext:Component runat="server" Width="50" />

                <ext:Button runat="server" Text="Menu Small" XMenu="TestMenu" UI="Info" />
                <ext:Button runat="server" Text="Menu Medium" Scale="Medium" XMenu="TestMenu" UI="Info" />
                <ext:Button runat="server" Text="Menu Large" Scale="Large"  XMenu="TestMenu" UI="Info" />

                <ext:Component runat="server" Width="50" />

                <ext:SplitButton runat="server" Text="Split Small" XMenu="TestMenu" UI="Info" />
                <ext:SplitButton runat="server" Text="Split Medium" Scale="Medium" XMenu="TestMenu" UI="Info" />
                <ext:SplitButton runat="server" Text="Split Large" Scale="Large"  XMenu="TestMenu" UI="Info" />
            </Items>
        </ext:Container>

        <h1>Success (UI="Success")</h1>

        <ext:Container runat="server">
            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Middle" DefaultMargins="5" />
            </LayoutConfig>
            <Items>
                <ext:Button runat="server" Text="Small" UI="Success" />
                <ext:Button runat="server" Text="Medium" Scale="Medium" UI="Success" />
                <ext:Button runat="server" Text="Large" Scale="Large" UI="Success" />

                <ext:Component runat="server" Width="50" />

                <ext:Button runat="server" Text="Menu Small" XMenu="TestMenu" UI="Success" />
                <ext:Button runat="server" Text="Menu Medium" Scale="Medium" XMenu="TestMenu" UI="Success" />
                <ext:Button runat="server" Text="Menu Large" Scale="Large"  XMenu="TestMenu" UI="Success" />

                <ext:Component runat="server" Width="50" />

                <ext:SplitButton runat="server" Text="Split Small" XMenu="TestMenu" UI="Success" />
                <ext:SplitButton runat="server" Text="Split Medium" Scale="Medium" XMenu="TestMenu" UI="Success" />
                <ext:SplitButton runat="server" Text="Split Large" Scale="Large"  XMenu="TestMenu" UI="Success" />
            </Items>
        </ext:Container>
        
        <h1>Warning (UI="Warning")</h1>

        <ext:Container runat="server">
            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Middle" DefaultMargins="5" />
            </LayoutConfig>
            <Items>
                <ext:Button runat="server" Text="Small" UI="Warning" />
                <ext:Button runat="server" Text="Medium" Scale="Medium" UI="Warning" />
                <ext:Button runat="server" Text="Large" Scale="Large" UI="Warning" />

                <ext:Component runat="server" Width="50" />

                <ext:Button runat="server" Text="Menu Small" XMenu="TestMenu" UI="Warning" />
                <ext:Button runat="server" Text="Menu Medium" Scale="Medium" XMenu="TestMenu" UI="Warning" />
                <ext:Button runat="server" Text="Menu Large" Scale="Large"  XMenu="TestMenu" UI="Warning" />

                <ext:Component runat="server" Width="50" />

                <ext:SplitButton runat="server" Text="Split Small" XMenu="TestMenu" UI="Warning" />
                <ext:SplitButton runat="server" Text="Split Medium" Scale="Medium" XMenu="TestMenu" UI="Warning" />
                <ext:SplitButton runat="server" Text="Split Large" Scale="Large"  XMenu="TestMenu" UI="Warning" />
            </Items>
        </ext:Container>
    </form>
</body>
</html>
