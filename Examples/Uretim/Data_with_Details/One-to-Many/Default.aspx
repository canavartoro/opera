<!-- Istasyonlar -->
<%@ Page Language="C#" %>
<%@ Import Namespace="DevExpress.Xpo" %>
<%@ Import Namespace="Opera" %>
<%@ Import Namespace="DevExpress.Data.Filtering" %>
<%@ Import Namespace="Mikrobar.Module.BusinessObjects" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script runat="server">

    [DirectMethod(Timeout=50000)]
    public object BindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        XPQuery<Istasyonlar> ists = new XPQuery<Istasyonlar>(Opera.Utillity.Session);
        object countObj = Opera.Utillity.Session.Evaluate(typeof(Istasyonlar), CriteriaOperator.Parse("Count()"), null);
        var data = (from x in ists
                    orderby x.IstasyonId
                    select new
                    {
                        x.IstasyonId,
                        x.IstasyonKod,
                        x.IstasyonAd,
                        x.Aciklama,
                        x.FasonIstasyon,
                        x.IsMerkeziId,
                        x.IsMerkeziKod,
                        x.IsMerkeziAd
                    }).Skip(prms.Start).Take(prms.Limit).ToList();
        int total = Convert.ToInt32(countObj);
        return new { data, total };               
    }  

    private bool cancel;
    private string message;
    private string insertedValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["user"] == null)
        //{
        //    Response.Write("<script type='text/javascript'>");
        //    Response.Write("top.document.location.href='../../Login.aspx?cik=1';");
        //    Response.Write("<" + "/" + "script>");
        //    Response.End();
        //    return;
        //}
        //YetkiEkle.Value = Request.QueryString["yetki_ekle"].ToString();
        //YetkiSil.Value = Request.QueryString["yetki_sil"].ToString();
        //YetkiDuzelt.Value = Request.QueryString["yetki_duzelt"].ToString();
    }
    protected void SqlDataSourceIstasyonlar_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        insertedValue = e.Command.Parameters["@newId"].Value != null ? e.Command.Parameters["@newId"].Value.ToString() : "";
    }

    protected void Store1_BeforeChange(object sender, BeforeStoreChangedEventArgs e)
    {
        string json = e.DataHandler.JsonData;
        StoreDataHandler dataHandler = new StoreDataHandler(json);
        var data = dataHandler.ObjectData<Istasyonlar>();
        using (UnitOfWork db = new UnitOfWork())
        {            
            foreach (Istasyonlar supplier in data)
            {
                Istasyonlar ist = null;
                switch (e.Action)
                {
                    case StoreAction.Destroy:
                        ist = Utillity.Session.GetObjectByKey<Istasyonlar>(supplier.IstasyonId);
                        if (ist != null) ist.Delete();                        
                        break;
                    case StoreAction.Update:
                        ist = (Istasyonlar)Opera.XpoHelper.CloneBaseObject(supplier, typeof(Istasyonlar), db);
                        if (ist != null) ist.Save();
                        break;
                    case StoreAction.Create:
                        ist = (Istasyonlar)Opera.XpoHelper.CloneBaseObject(supplier, typeof(Istasyonlar), db);
                        if (ist != null) ist.Save();
                        break;
                }
            }
            db.CommitChanges();
        }

        if (e.Action != StoreAction.Destroy)
        {
            foreach (Istasyonlar supplier in data)
            {
                e.ResponseRecords.Add(supplier);
            }
        }
        
        e.Cancel = true;
    }

    protected void Store_ReadData(object sender, StoreReadDataEventArgs e)
    {        
        //-- start filtering -----------------------------------------------------------
        FilterHeaderConditions fhc = new FilterHeaderConditions(e.Parameters["filterheader"]);
        if (fhc.Conditions.Count > 0)
        {
            CriteriaOperatorCollection coll = new CriteriaOperatorCollection();
            foreach (FilterHeaderCondition condition in fhc.Conditions)
            {
                string dataIndex = condition.DataIndex;
                FilterType type = condition.Type;
                string op = condition.Operator;
                object value = null;
                switch (condition.Type)
                {
                    case FilterType.Boolean:                        
                        value = condition.Value<bool>();
                        coll.Add(CriteriaOperator.Parse(string.Format("{0} = {1} ", dataIndex, value)));
                        break;

                    case FilterType.Date:
                        switch (condition.Operator)
                        {
                            case "=":
                                value = condition.Value<DateTime>();
                                coll.Add(CriteriaOperator.Parse(string.Format("{0} = {1} ", dataIndex, value)));
                                break;

                            case "compare":
                                value = FilterHeaderComparator<DateTime>.Parse(condition.JsonValue);
                                coll.Add(CriteriaOperator.Parse(string.Format("{0} = {1} ", dataIndex, value)));
                                break;
                        }
                        break;

                    case FilterType.Numeric:
                        switch (condition.Operator)
                        {
                            case "=":
                                value = condition.Value<double>();
                                    coll.Add(CriteriaOperator.Parse(string.Format("{0} = {1} ", dataIndex, value)));
                                break;

                            case "compare":
                                value = FilterHeaderComparator<double>.Parse(condition.JsonValue);
                                    coll.Add(CriteriaOperator.Parse(string.Format("{0} = {1} ", dataIndex, value)));
                                break;
                        }

                        break;
                    case FilterType.String:
                        value = condition.Value<string>();

                        if (op.Equals("="))
                            coll.Add(CriteriaOperator.Parse(string.Format("{0} = {1} ", dataIndex, value)));
                        else if (op.Equals("compare"))
                            coll.Add(CriteriaOperator.Parse(string.Format("{0} = {1} ", dataIndex, value)));
                        else if (op.Equals("+"))
                            coll.Add(new BinaryOperator(dataIndex, string.Format("{0}%", value), BinaryOperatorType.Like));
                        else if (op.Equals("-"))
                            coll.Add(new BinaryOperator( dataIndex, string.Format("%{0}", value), BinaryOperatorType.Like));
                        else if (op.Equals("!"))
                            coll.Add(new BinaryOperator(dataIndex, string.Format("%{0}%", value), BinaryOperatorType.Like).Not());
                        else if (op.Equals("*"))
                            coll.Add(new BinaryOperator(dataIndex , string.Format("%{0}%", value), BinaryOperatorType.Like));                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                } // end switch                
            }

            SortProperty[] sorts = null;
            if (e.Sort.Length > 0)
            {
                sorts = new SortProperty[e.Sort.Length];
                for (int i = 0; i < e.Sort.Length; i++)
                {
                    sorts[i] = new SortProperty(e.Sort[i].Property, e.Sort[i].Direction == Ext.Net.SortDirection.DESC ? DevExpress.Xpo.DB.SortingDirection.Descending : DevExpress.Xpo.DB.SortingDirection.Ascending);
                } 
            }

            var xdata = new XPCollection<Istasyonlar>(CriteriaOperator.And(coll), sorts).ToList();
            var data = (from x in xdata
                        orderby x.IstasyonId
                        select new
                        {
                            x.IstasyonId,
                            x.IstasyonKod,
                            x.IstasyonAd,
                            x.Aciklama,
                            x.FasonIstasyon,
                            x.IsMerkeziId,
                            x.IsMerkeziKod,
                            x.IsMerkeziAd
                        }).Skip(e.Start).Take(e.Limit).ToList();
            e.Total = xdata.Count;
            //if (e.Sort.Length > 0)
            //{
            //    data.Sort(delegate(object x, object y)
            //    {
            //        object a;
            //        object b;
            //        int direction = e.Sort[0].Direction == Ext.Net.SortDirection.DESC ? -1 : 1;
            //        a = x.GetType().GetProperty(e.Sort[0].Property).GetValue(x, null);
            //        b = y.GetType().GetProperty(e.Sort[0].Property).GetValue(y, null);
            //        return CaseInsensitiveComparer.Default.Compare(a, b) * direction;
            //    });
            //}            
            Store1.DataSource = data; // rangeData;
            Store1.DataBind();
        }
        else
        {           
            XPQuery<Istasyonlar> ists = new XPQuery<Istasyonlar>(Opera.Utillity.Session);
            object countObj = Opera.Utillity.Session.Evaluate(typeof(Istasyonlar), CriteriaOperator.Parse("Count()"), null);
            var data = (from x in ists
                        orderby x.IstasyonId
                        select new
                        {
                            x.IstasyonId,
                            x.IstasyonKod,
                            x.IstasyonAd,
                            x.Aciklama,
                            x.FasonIstasyon,
                            x.IsMerkeziId,
                            x.IsMerkeziKod,
                            x.IsMerkeziAd
                        }).Skip(e.Start).Take(e.Limit).ToList();
            e.Total = Convert.ToInt32(countObj);
            Store1.DataSource = data; // rangeData;
            Store1.DataBind();
        }
    }
    
    protected void StoreIstasyonlar_AfterRecordInserted(object sender, AfterRecordInsertedEventArgs e)
    {
        if (!string.IsNullOrEmpty(insertedValue))
        {
            insertedValue = "";
        }
    }
</script>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Istasyonlar</title>
    <link href="/resources/css/examples.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .x-grid-cell-fullName .x-grid-cell-inner {
            font-family : tahoma, verdana;
            display     : block;
            font-weight : normal;
            font-style  : normal;
            color       : #385F95;
            white-space : normal;
        }
        .x-grid-rowbody div {
            margin : 2px 5px 20px 5px !important;
            width  : 99%;
            color  : Gray;
        }
        .x-grid-row-expanded td.x-grid-cell{
            border-bottom-width:0px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        // <![CDATA[
        Ext.onReady(function () {

        });
        // ]]>
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />        

        <ext:Store ID="StoreIstasyonlar" runat="server" ShowWarningOnFailure="false"
        OnReadData="Store_ReadData" OnAfterRecordInserted="StoreIstasyonlar_AfterRecordInserted" RemoteSort="true" PageSize="10">
             <Proxy>
                        <ext:PageProxy  />
                    </Proxy>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="0" Mode="Raw" />
                <ext:Parameter Name="limit" Value="10" Mode="Raw" />
            </AutoLoadParams>
            <Model>
                <ext:Model ID="Model2" runat="server" IDProperty="IstasyonId" Name="Istasyonlar" >
                    <Fields>
                        <ext:ModelField Name="IstasyonId" />
                        <ext:ModelField Name="IstasyonKod" />
                        <ext:ModelField Name="IstasyonAd" />
                        <ext:ModelField Name="Aciklama" />
                        <ext:ModelField Name="FasonIstasyon" Type="Boolean" />
                        <ext:ModelField Name="IsMerkeziId" />
                        <ext:ModelField Name="IsMerkeziKod" />
                        <ext:ModelField Name="IsMerkeziAd" />
                    </Fields>
                </ext:Model>
            </Model>
            <Listeners>
                <Exception Handler="Ext.Msg.alert('Hata!1', operation.getError());" />
                <Write Handler="Ext.Msg.alert('Kaydedildi', 'Islem basariyla kaydedildi.');" />
            </Listeners>
        </ext:Store>

        <ext:Viewport ID="ViewportIstasyonlar" Layout="BorderLayout" runat="server">
            <Items>
                <ext:Panel ID="PanelIstasyonlar" runat="server" Region="Center" Height="300" Header="false" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="GridPanelIstasyonlar" runat="server"  Title="Istasyonlar" Border="false" Icon="Lorry">
                            <Store>
                        <ext:Store 
                            ID="Store1" 
                            runat="server" 
                            PageSize="10" 
                            RemoteSort="true"
                            OnReadData="Store_ReadData"
                            OnBeforeStoreChanged="Store1_BeforeChange">
                            <Model>
                                <ext:Model ID="Model1" runat="server" IDProperty="IstasyonId">
                                    <Fields>
                                        <ext:ModelField Name="IstasyonId" Type="Int" />
                                        <ext:ModelField Name="IstasyonKod" Type="String" />
                                        <ext:ModelField Name="IstasyonAd" Type="String" />
                                        <ext:ModelField Name="Aciklama" Type="String" />
                                        <ext:ModelField Name="FasonIstasyon" Type="Boolean" />
                                        <ext:ModelField Name="IsMerkeziId" Type="Int" />
                                        <ext:ModelField Name="IsMerkeziKod" Type="String" />
                                        <ext:ModelField Name="IsMerkeziAd" Type="String" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="IstasyonKod" Direction="ASC" />
                            </Sorters>  
                            <Proxy>
                                <ext:PageProxy />
                            </Proxy>      
                        </ext:Store>
                    </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" />
                                    <ext:Column ID="ColumnIstasyonId" runat="server" DataIndex="IstasyonId" Text="IstasyonId">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldIstasyonId" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnIstasyonKod" runat="server" DataIndex="IstasyonKod" Text="IstasyonKod">
                                        <Editor>
                                            <ext:TextField ID="TextFieldIstasyonKod" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnIstasyonAd" runat="server" DataIndex="IstasyonAd" Text="IstasyonAd" Flex="1">
                                        <Editor>
                                            <ext:TextField ID="TextFieldIstasyonAd" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnAciklama" runat="server" DataIndex="Aciklama" Text="Aciklama">
                                        <Editor>
                                            <ext:TextField ID="TextFieldAciklama" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="CheckColumnFasonIstasyon" runat="server" Text="FasonIstasyon?" DataIndex="FasonIstasyon" StopSelection="false" Editable="true" Width="70" />
                                    <ext:Column ID="ColumnIsMerkeziId" runat="server" DataIndex="IsMerkeziId" Text="IsMerkeziId">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldIsMerkeziId" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnIsMerkeziKod" runat="server" DataIndex="IsMerkeziKod" Text="IsMerkeziKod">
                                        <Editor>
                                            <ext:TextField ID="TextFieldIsMerkeziKod" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnIsMerkeziAd" runat="server" DataIndex="IsMerkeziAd" Text="IsMerkeziAd">
                                        <Editor>
                                            <ext:TextField ID="TextFieldIsMerkeziAd" runat="server" />
                                        </Editor>
                                    </ext:Column>
      
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbarIstasyonlar" runat="server" PageSize="10" StoreID="StoreIstasyonlar" DisplayInfo="true" DisplayMsg="Gösterilen {0} - {1} / {2}" EmptyMsg="Kayýt yok."/>
                            </BottomBar>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModelIstasyonlar" runat="server" Mode="Multi" >
                                    <Listeners>
                                        <Select Handler="#{btnIstasyonlarDelete}.enable();" />
                                        <Deselect Handler="if (!#{GridPanelIstasyonlar}.selModel.hasSelection()) {
                                                               #{btnIstasyonlarDelete}.disable();
                                                           }" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingIstasyonlar" runat="server" />
                                <ext:FilterHeader ID="FilterHeaderIstasyonlar" runat="server"  Remote="true" />
                            </Plugins>
                        </ext:GridPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnIstasyonlarSave" runat="server"  Text="Kaydet" Icon="Disk" ToolTip="Degisiklikleri kaydet" >
                            <Listeners>
                                <Click Handler="#{Store1}.sync();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnIstasyonlarDelete" runat="server"  Text="Secileni Sil" Disabled="true" Icon="Delete" >
                            <Listeners>
                                <Click Handler="#{GridPanelIstasyonlar}.deleteSelected();
                                                if (!#{GridPanelIstasyonlar}.hasSelection()) {
                                                    #{btnIstasyonlarDelete}.disable();
                                                }" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnIstasyonlarInsert" runat="server"  Text="Ekle" Icon="Add" >
                            <Listeners>
                                <Click Handler="#{Store1}.insert(0, {});#{GridPanelIstasyonlar}.editingPlugin.startEditByPosition({row:0, column:2});" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnIstasyonlarRefresh" runat="server"  Text="Yenile" Icon="ArrowRefresh">
                            <Listeners>
                                <Click Handler="#{Store1}.reload();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <asp:HiddenField ID="YetkiEkle" Value="false" runat="server" />
        <asp:HiddenField ID="YetkiSil" Value="false" runat="server" />
        <asp:HiddenField ID="YetkiDuzelt" Value="false" runat="server" />
    </form>
</body>
</html>
<!-- by Canavar.Toro -->
