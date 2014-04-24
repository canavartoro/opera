<%@ Page Language="C#" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>MultiUpload Overview - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />    
</head>
<body>
     <form runat="server">
         <ext:ResourceManager runat="server" />        

        <p>MultiUpload is flash based uploader, it is based on <a href="https://code.google.com/p/swfupload/" target="_blank">SwfUpload</a> flash</p>

        <h3>Features: </h3>

        <ul>
            <li>Multiple File Selection</li>
            <li>File Upload Progress</li>
            <li>Custom Limits for File Size and Number of Uploads</li>
            <li>Filter by File Type ie. *.jpg</li>
            <li>File Queue</li>
            <li>Drag and Drop (it is supported in browsers that support the HTML5 File API and the Drag and Drop API only: FireFox 4.0+, Chrome, Safari 5.0+, IE 10)</li>
        </ul>

         
         <h3>SwfUpload links:</h3>

         <a href="https://code.google.com/p/swfupload/" target="_blank">SwfUpload site</a>
         <br />
         <a href="http://demo.swfupload.org/v250beta3/" target="_blank">Official SwfUpload demos</a>
         <br />
         <a href="http://demo.swfupload.org/Documentation/">SwfUpload documentation</a>
         <br />

         <h3>Web.config settings to change max upload size and timeout value</h3>   
         <pre class="code">
&lt;httpRuntime maxRequestLength="2151" executionTimeout="300" />
&lt;!-- 
	    maxRequestLength is in KB units.  2151 KB is just over 2 MB, this ensures that
	    a 2 MB plus some other POST overhead will be accepted. The default is 4 MB. The
	    maximum value is 2097151 for .Net 2.0 and 1048576 for .Net 1.x.
				
	    executionTimeout is in seconds.  It should be long enough for the entire to be uploaded
	    and for page execution to complete.
-->
         </pre>

         <h3>Such Global.cs.asax can be required for correct handling Session and Authentication for MultiUpload requests</h3>

         <ext:Panel 
             runat="server"
             Title="Global.cs.asax"
             Height="400">
             <Loader Mode="Frame" Url="Global.cs.txt" />                 
         </ext:Panel>
     </form>     
</body>
</html>
