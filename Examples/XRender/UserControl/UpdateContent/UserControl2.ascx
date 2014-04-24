<%@ Control Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ResourceManager.RegisterGlobalScript("script.js");
    }   

    protected void HelloFromServer(object sender, DirectEventArgs e)
    {
        X.Msg.Alert("Server", "Hello from server - UserControl #2").Show();
    }        
</script>

<h1>#2</h1>

<ext:Label runat="server" Text="I am User control #2" />

<br /><br />

<ext:Button 
    ID="Button1"
    runat="server" 
    Text="User control #2: Ext.Net button" 
    OnDirectClick="HelloFromServer" />

<br />

<asp:Button 
    runat="server" 
    Text="User control #2: ASP.NET button" 
    OnClientClick="helloFromClient('UserControl #2'); return false;" />