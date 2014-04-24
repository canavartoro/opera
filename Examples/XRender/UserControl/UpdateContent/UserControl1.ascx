<%@ Control Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ResourceManager.RegisterGlobalScript("script.js");
    }   

    protected void HelloFromServer(object sender, DirectEventArgs e)
    {
        X.Msg.Alert("Server", "Hello from server - UserControl #1").Show();
    }        
</script>

<h1>#1</h1>

<ext:Label runat="server" Text="I am User control #1" />

<br /><br />

<ext:Button 
    ID="Button1"
    runat="server" 
    Text="User control #1: Ext.Net button" 
    OnDirectClick="HelloFromServer" />

<br />

<asp:Button 
    runat="server" 
    Text="User control #1: ASP.NET button" 
    OnClientClick="helloFromClient('UserControl #1'); return false;" />