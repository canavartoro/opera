<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>SVN Log - Ext.NET Examples</title>
</head>
<body>
    <ext:ResourceManager runat="server" />

    <ext:Viewport runat="server" Layout="FitLayout">
        <Items>
            <ext:GridPanel
                runat="server" 
                Title="Recent SVN Commits" 
                TrackMouseOver="true"
                Flex="2">
                <Store>
                    <ext:Store runat="server">
                        <Proxy>
                            <ext:JsonPProxy Url="http://api.ext.net/log/get/?version=2.">
                                <Reader>
                                    <ext:JsonReader Root="data" />
                                </Reader>
                            </ext:JsonPProxy>
                        </Proxy>
                        <Model>
                            <ext:Model runat="server">
                                <Fields>
                                    <ext:ModelField Name="version" />
                                    <ext:ModelField Name="revision" />
                                    <ext:ModelField Name="author" />
                                    <ext:ModelField Name="date" Type="Date" DateFormat="yyyy-MM-ddTHH:mm:ss" />
                                    <ext:ModelField Name="tag" />
                                    <ext:ModelField Name="message" />
                                    <ext:ModelField Name="actionCount" />
                                    <ext:ModelField Name="actions" Type="Object" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel runat="server">
                    <Columns>
                        <ext:Column runat="server" ColumnID="id" Header="Revision" DataIndex="revision" />
                        <ext:Column runat="server" ColumnID="version" Header="Version" DataIndex="version" />
                        <ext:Column runat="server" ColumnID="author" Header="Author" DataIndex="author" />
                        <ext:DateColumn runat="server" Header="Date" DataIndex="date" Format="yyyy-MM-dd" />
                        <ext:Column runat="server" Header="Tag" DataIndex="tag" />
                        <ext:Column runat="server" Header="Message" DataIndex="message" Flex="1" />
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel runat="server" />
                </SelectionModel>
                <Features>
                    <ext:GridFilters runat="server" Local="true">
                        <Filters>
                            <ext:NumericFilter DataIndex="revision" />
                            <ext:ListFilter DataIndex="author" Options="vladimir,Daniil.Veriga,geoffrey.mcgill" />
                            <ext:DateFilter DataIndex="date" />
                            <ext:ListFilter DataIndex="tag" Options="NEW,FIX,UPDATE" />
                            <ext:StringFilter DataIndex="message" />
                        </Filters>
                    </ext:GridFilters>
                </Features>
                <Plugins>
                    <ext:RowExpander runat="server">
                        <Template runat="server">
                            <Html>
                                <div style="padding:5px;">
						        <b>Description :</b><br /> 
                                    <div style="margin-left: 25px;">{message}</div>
                                <br />
						        <b>Actions :</b> [{actionCount} file<tpl if="actionCount &gt; 1">s</tpl> updated]
                                    <ol style="margin-left:25px !important; list-style-type: decimal !important;">
                                        <tpl for="actions"><li>{description}</li></tpl>
                                    </ol>
                                </div>
					        </Html>
                        </Template>
                    </ext:RowExpander>
                </Plugins>
            </ext:GridPanel>
        </Items>
    </ext:Viewport>
</body>
</html>
    