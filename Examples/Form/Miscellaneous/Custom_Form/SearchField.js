Ext.define('Ext.net.SearchField', {
    extend: 'Ext.form.field.Trigger',    
    alias: 'widget.searchfield',

    triggersConfig: [{
        hideTrigger: true,
        iconCls: Ext.form.field.Trigger.getIcon("Clear")
    }, {
        iconCls: Ext.form.field.Trigger.getIcon("Search")
    }],
    
    hasSearch : false,
    paramName : 'query',
    
    initComponent: function() {
        var me = this;

        me.callParent(arguments);

        me.on('specialkey', function(f, e){
            if (e.getKey() == e.ENTER) {
                me.doFilter();
            }
        });

        me.on("triggerclick", function(item, trigger, index, tag, e){
            index == 0 ? this.clearFilter() : this.doFilter();
        }, this);

        me.store = Ext.data.StoreManager.lookup(me.store)

        // We're going to use filtering
        me.store.remoteFilter = true;

        // Set up the proxy to encode the filter in the simplest way as a name/value pair
        
        // If the Store has not been *configured* with a filterParam property, then use our filter parameter name
        if (!me.store.proxy.hasOwnProperty('filterParam')) {
            me.store.proxy.filterParam = me.paramName;
        }
        me.store.proxy.encodeFilters = function(filters) {
            return filters[0].value;
        }
    },

    clearFilter : function(){
        var me = this;
            
        if (me.hasSearch) {
            me.setValue('');
            me.store.clearFilter();
            me.hasSearch = false;
            me.getTrigger(0).hide();
            me.updateLayout();
        }
    },

    doFilter : function(){
        var me = this,
            store = me.store,
            proxy = store.getProxy(),
            value = me.getValue();
            
        if (value.length > 0) {
            // Param name is ignored here since we use custom encoding in the proxy.
            // id is used by the Store to replace any previous filter
            me.store.filter({
                id: me.paramName,
                property: me.paramName,
                value: value
            });
            me.hasSearch = true;
            me.getTrigger(0).show();
            me.updateLayout();
        }
    }
});