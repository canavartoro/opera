<%@ Page Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Grouped GridPanel - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />

    <script>
        var onToggleButtonBeforeRender = function(btn) {
            var grid = btn.up("gridpanel"),
                menu = btn.menu,
                store = grid.getStore(),
                groups = store.getGroups(),
                len = groups.length, i = 0,
                toggleMenu = [];

            // Create checkbox menu items to toggle associated group
            for (; i < len; i++) {
                menu.add({
                    xtype: 'menucheckitem',
                    text: groups[i].name,
                    handler: toggleGroup,
                    scope: grid.groupingFeature
                });
            }
        };

        var toggleGroup = function(item) {
            var groupName = item.text;

            if (item.checked) {
                this.expand(groupName, true);
            } else {
                this.collapse(groupName, true);
            }
        };

        var onClearGroupingClick = function() {
            App.GridPanel1.groupingFeature.disable();
        };

        var onGroupChange = function(store, groupers) {
            var grouped = store.isGrouped(),
                groupBy = groupers.items[0] ? groupers.items[0].property : '',
                toggleMenuItems, 
                len, 
                i = 0,
                me = App.GridPanel1;

            if (!me) {
                return;
            }

            // Clear grouping button only valid if the store is grouped
            me.down('[text=Clear Grouping]').setDisabled(!grouped);

            // Sync state of group toggle checkboxes
            if (grouped && groupBy === 'cuisine') {
                toggleMenuItems = me.down('button[text=Toggle groups...]').menu.items.items;
                for (len = toggleMenuItems.length; i < len; i++) {
                    toggleMenuItems[i].setChecked(
                        me.groupingFeature.isExpanded(toggleMenuItems[i].text)
                    );
                }
                me.down('[text=Toggle groups...]').enable();
            } else {
                me.down('[text=Toggle groups...]').disable();
            }
        };

        var onGroupCollapse = function(v, n, groupName) {
            if (!this.panel.down('[text=Toggle groups...]').disabled) {
                this.panel.down('menucheckitem[text=' + groupName + ']').setChecked(false, true);
            }
        };

        var onGroupExpand = function(v, n, groupName) {
            if (!this.panel.down('[text=Toggle groups...]').disabled) {
                this.panel.down('menucheckitem[text=' + groupName + ']').setChecked(true, true);
            }
        };
    </script>
</head>
<body>
    <ext:ResourceManager runat="server" />

    <ext:Viewport runat="server"  Margins="0 0 10 0">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:GridPanel
                ID="GridPanel1" 
                runat="server" 
                Title="Restaurants" 
                Icon="Table"
                Frame="true"
                Width="600"
                Height="400"
                MinHeight="200"
                Resizable="true"
                Collapsible="true">
                <Store>
                    <ext:Store 
                        runat="server" 
                        Data="<%# Ext.Net.Examples.KitchenSink.Restaurants.GetAllRestaurants() %>"
                        GroupField="cuisine">
                        <Model>
                            <ext:Model runat="server">
                                <Fields>
                                    <ext:ModelField Name="name" />
                                    <ext:ModelField Name="cuisine" />
                                    <ext:ModelField Name="description" />
                                    <ext:ModelField Name="rating" Type="Int" />
                                </Fields>
                            </ext:Model>
                        </Model>
                        <Sorters>
                            <ext:DataSorter Property="cuisine" />
                            <ext:DataSorter Property="name" />
                        </Sorters>
                        <Listeners>
                            <GroupChange Fn="onGroupChange" />
                        </Listeners>
                    </ext:Store>
                </Store>
                <ColumnModel runat="server">
                    <Columns>
                        <ext:Column runat="server" Text="Name" Flex="1" DataIndex="name" />
                        <ext:Column runat="server" Text="Cuisine" Flex="1" DataIndex="cuisine" />
                    </Columns>
                </ColumnModel>
                <Features>
                    <ext:Grouping 
                        runat="server" 
                        HideGroupedHeader="true"
                        GroupHeaderTplString="Cuisine: {name} ({rows.length} Item{[values.rows.length > 1 ? 's' : '']})"
                        StartCollapsed="true" />
                </Features>
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:ToolbarFill runat="server" />
                            <ext:Button runat="server" Text="Toggle groups..." DestroyMenu="true">
                                <Menu>
                                    <ext:Menu runat="server" />
                                </Menu>
                                <Listeners>
                                    <BeforeRender Fn="onToggleButtonBeforeRender" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <FooterBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:ToolbarFill runat="server" />
                            <ext:Button 
                                runat="server" 
                                Text="Clear Grouping" 
                                Icon="Bin" 
                                Handler="onClearGroupingClick" />
                        </Items>
                    </ext:Toolbar>
                </FooterBar>
                <View>
                    <ext:GridView runat="server">
                        <Listeners>
                            <GroupCollapse Fn="onGroupCollapse" />
                            <GroupExpand Fn="onGroupExpand" />
                        </Listeners>
                    </ext:GridView>
                </View>
            </ext:GridPanel>
        </Items>
    </ext:Viewport>
</body>
</html>