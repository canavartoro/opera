<%@ Page Language="C#" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Getting Started - Ext.NET Examples</title>
    <link href="/resources/css/examples.css"  rel="stylesheet" />
    <base target="_blank" />
</head>
<body>
    <h1>Welcome to the Ext.NET Examples Explorer</h1>
        
    <h2>Overview</h2>
        
    <p>Ext.NET is an advanced ASP.NET (WebForms + MVC) component framework integrating the cross-browser Sencha Ext JS JavaScript Library.</p>

    <p>Ext.NET is built for developers, by developers. We provide hundreds of demos with full code samples in the <a href="http://examples.ext.net/">Examples Explorer</a>. Need a little help? Check out our Premium Technical <a href="http://ext.net/store#premium">Support Subscription</a>.</p>

    <p>Direct access to the latest Ext.NET source code, via read-only SVN access, is available to all Ext.NET Pro 
        license holders with a valid <a href="http://ext.net/store#premium">Premium Support</a> Subscription.</p>
            
    <h2>System Requirements</h2>
        
    <ol>
        <li><a href="http://www.microsoft.com/visualstudio/eng/products/visual-studio-overview">Visual Studio</a> 2008, 2010 or 2012, or</li>
        <li><a href="http://www.microsoft.com/visualstudio/eng/products/visual-studio-express-products">Visual Studio Express</a> 2008, 2010 or 2012</li>
        <li>.NET Framework 3.5, 4.0* or 4.5</li>
    </ol>

    <p>*minimum required for Ext.NET MVC</p>
        
    <h2>Getting Started (NuGet)</h2>
        
    <p>The easiest and quickest way to install Ext.NET is using <a href="http://nuget.org/packages/Ext.NET">NuGet</a>. Run the following command in Visual Studio Package Manager Console, 
    or seach for "Ext.NET" in NuGet Package Manager.</p>

    <p><code>Install-Package Ext.NET</code></p>

    <h2>Getting Started (Manual)</h2>

    <ol>
        <li>First ensure you have Visual Studio or Visual Web Developer Express installed on your computer.
            <div class="information"><p>If you do not have a copy of Visual Studio already installed, the <a href="http://www.microsoft.com/visualstudio/eng/products/visual-studio-express-products/">Visual Studio Express</a> is free to use and 
            is a great way to get started with ASP.NET and Ext.NET. The Ext.NET Components work exactly the same in both environments.</p></div></li>
            
        <li>A Manual installation package (.zip) is available for download at <a href="http://www.ext.net/download/">http://www.ext.net/download/</a>.</li>
            
        <li>Create your first "Web Site" Project.
            <ol style="list-style-type: lower-roman;">
                <li>Open Visual Studio (or Visual Web Developer) and create a new "Web Site" project. From the File Menu, select New > Web Site.</li>
                <li>The "New Web Site" dialog will open, ensure "ASP.NET Web Site" is selected from the list of Templates.</li>
                <li>For your first project, the "Location" option of "File System" and default file path should be fine, or modify to fit your preference.</li>
                <li>Please select your "Language" preference. Whether you choose "Visual C#" or "Visual Basic" is ultimately just dependent on personal coding preferences. 
                    Ext.NET is written in C#, but can be used in any .NET language, including Visual Basic, C# and even ASP.NET MVC Razor.</li>
                <li>Click "OK".</li>
            </ol>
        </li>

        <li>Detailed manual installation steps are detailed at <a href="http://forums.ext.net/showthread.php?11027-Install-and-Setup-Guide-for-Visual-Studio">Install and Setup Guide for Visual Studio</a></li>
    </ol>

    <h2>Sample Web.config (WebForms and MVC)</h2>

<pre class="code">
&lt;?xml version="1.0"?>
&lt;configuration>
  &lt;configSections>
    &lt;section name="extnet" type="Ext.Net.GlobalConfig" requirePermission="false" />
  &lt;/configSections>

  &lt;extnet theme="Gray" />
  
  &lt;system.web>
    &lt;httpHandlers>
      &lt;add path="*/ext.axd" verb="*" type="Ext.Net.ResourceHandler" validate="false" />
    &lt;/httpHandlers>

    &lt;httpModules>
      &lt;add name="DirectRequestModule" type="Ext.Net.DirectRequestModule, Ext.Net" />
    &lt;/httpModules>

    &lt;pages>
      &lt;controls>
        &lt;add assembly="Ext.Net" namespace="Ext.Net" tagPrefix="ext" />
      &lt;/controls>
    &lt;/pages>
  &lt;/system.web>

  &lt;system.webServer>
    &lt;validation validateIntegratedModeConfiguration="false" />
		
    &lt;modules>
      &lt;add name="DirectRequestModule" preCondition="managedHandler" type="Ext.Net.DirectRequestModule, Ext.Net" />
    &lt;/modules>
		
    &lt;handlers>
      &lt;add name="DirectRequestHandler" verb="*" path="*/ext.axd" preCondition="integratedMode" type="Ext.Net.ResourceHandler" />
    &lt;/handlers>
  &lt;/system.webServer>
  
  &lt;runtime>
    &lt;assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      &lt;dependentAssembly>
        &lt;assemblyIdentity name="Newtonsoft.Json" publicKeyToken="30ad4fe6b2a6aeed" />
        &lt;bindingRedirect oldVersion="1.0.0.0-4.5.10" newVersion="4.5.11" />
      &lt;/dependentAssembly>
      &lt;dependentAssembly>
        &lt;assemblyIdentity name="Ext.Net.Utilities" publicKeyToken="2c34ac34702a3c23" />
        &lt;bindingRedirect oldVersion="0.0.0.0-2.2.0" newVersion="2.2.1" />
      &lt;/dependentAssembly>
      &lt;dependentAssembly>
        &lt;assemblyIdentity name="Transformer.NET" publicKeyToken="e274d618e7c603a7" />
        &lt;bindingRedirect oldVersion="0.0.0.0-2.1.0" newVersion="2.1.1" />
      &lt;/dependentAssembly>
    &lt;/assemblyBinding>
  &lt;/runtime>
&lt;/configuration>
</pre>

    <h2>Web.config &lt;extnet> Global Configuration Properties</h2>

<pre class="code">
    directEventUrl : string
        The url to request for all DirectEvents.
        Default is "".
                      
    directMethodProxy : ClientProxy
        Specifies whether server-side Methods marked with the [DirectMethod] attribute will output configuration script to the client. 
        If false, the DirectMethods can still be called, but the Method proxies are not automatically generated. 
        Specifies ajax method proxies creation. The Default value is to Create the proxy for each ajax method.
        Default is 'Default'. Options include [Default|Include|Ignore]
      
    ajaxViewStateMode : ViewStateMode
        Specifies whether the ViewState should be returned and updated on the client during an DirectEvent. 
        The Default value is to Exclude the ViewState from the Response.
        Default is 'Default'. Options include [Default|Exclude|Include]

    cleanResourceUrl : boolean
        The Ext.NET controls can clean up the autogenerate WebResource Url so they look presentable.        
        Default is 'true'. Options include [true|false]

    clientInitDirectMethods : boolean
        Specifies whether server-side Methods marked with the [DirectMethod] attribute will output configuration script to the client. 
        If false, the DirectMethods can still be called, but the Method proxies are not automatically generated. 
        Default is 'false'. Options include [true|false]
        
    glyphFontFamily : string
        Sets the default font-family to use for components that support a glyph config.
        Default is "".
    
    gzip : boolean
        Whether to automatically render scripts with gzip compression.        
        Only works when renderScripts="Embedded" and/or renderStyles="Embedded".       
        Default is true. Options include [true|false]

    scriptAdapter : string
        Gets or Sets the current script Adapter.     
        Default is "Ext". Options include [Ext|jQuery|Prototype|YUI]

    renderScripts : ResourceLocationType
        Whether to have the Ext.NET controls output the required JavaScript includes or not.       
        Gives developer option of manually including required &lt;script> files.        
        Default is Embedded. Options include [Embedded|File|None] 

    renderStyles : ResourceLocationType
        Whether to have the Ext.NET controls output the required StyleSheet includes or not.       
        Gives developer option of manually including required &lt;link> or &lt;style> files.       
        Default is Embedded. Options include [Embedded|File|None]

    resourcePath : string
        Gets the prefix of the Url path to the base ~/Ext.Net/ folder containing the resources files for this project. 
        The path can be Absolute or Relative.
        
    resetStyles : bool
        True to reset default browser styles
        Default is false. Options include [true|false]

    scriptMode : ScriptMode
        Whether to include the Release (condensed) or Debug (with inline documentation) or Development (with inline things to debug) Ext JavaScript files.
        Default is "Release". Options include [Release|Debug|Development]
         
    sourceFormatting : boolean
        Specifies whether the scripts rendered to the page should be formatted. 'True' = formatting, 'False' = minified/compressed. 
        Default is 'false'. Options include [true|false]
      
    stateProvider : StateProvider
        Gets or Sets the current script Adapter.
        Default is 'PostBack'. Options include [PostBack|Cookie|None]
          
    theme : Theme
        Which embedded theme to use.       
        Default is "Default". Options include [Default|Access|Gray|Neptune|Slate]
          
    themePath : string
	      Configure a path to a custom theme .css file gloablly across whole application. This will override any .Theme setting. 
	      Default is "". 
  
    quickTips : boolean
        Specifies whether to render the QuickTips. Provides attractive and customizable tooltips for any element.
        Default is 'true'. Options include [true|false]
</pre>

        <!--
            
        <li>Add the Ext.NET Controls to your Visual Studio (or Visual Web Developer) Toolbox, see also <a href="http://examples.ext.net/#/Getting_Started/Introduction/README/">README.txt</a>
            <ol style="list-style-type: lower-roman;">
                <li>Open Visual Studio or Visual Web Developer Express</li>
                <li>Open an existing web site or create a new web site project.</li>
                <li>Open or create a new .aspx page.</li>
                <li>Open the ToolBox panel, typically located on the left side in a fly-out panel (Ctrl + Alt + x).</li>
                <li>Create a new "Ext.NET" Tab:
                    <ol style="list-style-type: lower-alpha;">
                        <li>Right-Click in the ToolBox area</li>
                        <li>Select "Add Tab"</li>
                        <li>Enter "Ext.NET"</li>
                    </ol>
                </li>
		        <li>Inside the "Ext.NET" tab, Right-Click and select "Choose Items...".</li>
		        <li>Under the ".NET Framework Components" Tab select the "Browse" button.</li>
		        <li>Navigate to and select the Ext.Net.dll file, choose open.</li>
                <li>Ext.NET controls should now be added to the list and pre-checked. You can confirm by sorting the list by "Namespace" and scrolling to "Ext.Net"</li>
                <li>Click "OK". The icons should be added to your ToolBox. You should now be able to drag/drop a Ext.NET component onto your .aspx Page.</li>
            </ol>
        </li>
           
        <li>Create your first web page.
            <ol style="list-style-type: lower-roman;">
                <li>Open a .aspx Page</li>
                <li>Drag the Ext.NET "ResourceManager" control onto your Page. One &lt;ext:ResourceManager> is required on each .aspx Page</li>
                <li>Drag an Ext.NET "Window" Control onto your Page, then Save (Ctrl + s) your Page.</li>
                <li>Hit "F5" to start debugging, or Right-Click on the Page and select "View in Browser". Your Page should now render in the browser and the &lt;ext:Window> will be displayed.</li>
                <li>Enjoy.</li>
            </ol>
        </li>
        -->
  </body>
</html>    
