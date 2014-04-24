<%@ Control Language="C#" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<ext:Panel runat="server"
    IDMode="Ignore"
    Closable="true"
    Layout="FitLayout"
    TemplateWidget="true"
    TemplateWidgetFnName="getTab">
    <ResourceItems>
        <ext:ClientResourceItem Path="Tab.css" IsCss="true" />
    </ResourceItems>
    <Items>
        <ext:DataView runat="server"
            TrackOver="true"
            ItemSelector=".x-newtab-item"
            OverItemCls="x-newtab-over-item">
            <Tpl>
                <Html>
                    <tpl for=".">
                        <div class="x-newtab-item">
                            <h1>{name}</h1>
                        </div>
                    </tpl>
                </Html>
            </Tpl>
            <Store>
                <ext:Store runat="server" AutoLoad="false">
                    <Model>
                        <ext:Model runat="server">
                           <Fields>
                               <ext:ModelField Name="name" />
                           </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>
            <Listeners>
                <ItemClick Handler="Ext.Msg.alert('Item', '#' + record.data.name);" />
            </Listeners>
        </ext:DataView>        
    </Items>    
</ext:Panel>