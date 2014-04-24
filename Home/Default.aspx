<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../resources/css/main.css" />

    <script>
        Ext.onReady(function () {
            if (top !== self) {
                Ext.select("a[class!=exclude]").on("click", function (e, el) { 
                    parent.location = el.href; 
                }, null, { preventDefault : true });
            }
        });
    </script>
</head>
<body class="welcome">
    <ext:ResourceManager runat="server" />

    <ext:Container runat="server" Height="260">
        <Content>
            <!--a class="exclude" href="http://www.ext.net/store/" target="_top">
                <img src="http://speed.ext.net/www/images/ext.net.sale.banner.png" alt="Ext.NET Sale Banner" class="banner" title="Ext.NET Sale Banner">
            </a-->
            <div id="welcome-title">
                Depo Yonetim Sistemi
	        </div>

	        <div id="welcome-links">
		        <div id="title">Kisayollar</div>
		        <div class="list">
                    <ext:Container runat="server" Title="links" Layout="HBoxLayout">
                        <Items>
                            <ext:Container runat="server" Flex="1">
                                <Content>
                                    <p><span>1</span><a href="/#/Getting_Started/Introduction/Overview/">Getting Started</a></p>
                                    <p><span>2</span><a href="/#/Events/DirectEvents/Overview/">DirectEvents</a></p>
                                    <p><span>3</span><a href="/#/Events/DirectMethods/Overview/">DirectMethods</a></p>
                                </Content>
                            </ext:Container>
                            <ext:Container runat="server" Flex="1">
                                <Content>
                                    <p><span>4</span><a href="/#/GridPanel/ArrayGrid/ArrayWithPaging/">GridPanel</a></p>
                                    <p><span>5</span><a href="/#/ViewPort/Basic/Built_in_Markup/">ViewPort</a></p>
                                    <p><span>6</span><a href="/#/XRender/Basic/Add_Items/">XRender</a></p>
                                </Content>
                            </ext:Container>
                        </Items>
                            
                    </ext:Container>
		        </div>
	        </div>
        </Content>
    </ext:Container>

    <ext:Container runat="server">
        <Content>
            <div id="welcome-grid">
		        <div id="screens">
                </div>
    		        <a class="button" href="/#/GridPanel/ArrayGrid/ArrayWithPaging/"><span class="f-icon-cog"></span> GridPanel</a>
    		        <a class="button" href="/#/Desktop/Introduction/Overview/"><span class="f-icon-cog"></span> Desktop</a>
    		        <a class="button" href="/#/Chart/Area/Basic/"><span class="f-icon-cog"></span> Charts</a>
    		        <a class="button" href="/#/Portal/Basic/Deluxe/"><span class="f-icon-cog"></span> Portal</span> </a>
    		        <a class="button" href="/#/Calendar/Overview/Basic/"><span class="f-icon-cog"></span> Calendar</a>
	        </div>
        </Content>
    </ext:Container>

    <ext:Container runat="server">
        <Content>
            <div id="welcome-footer">
		        <span>
			        <p><span class="f-icon-ok-circle f-icons-standout"></span> Barset Bilgi Sistemleri 2013 ®</p>
		        </span>
	        </div>
        </Content>
    </ext:Container>
</body>
</html>
