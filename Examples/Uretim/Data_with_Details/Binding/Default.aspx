<!-- SistemParametreleri -->
<%@ Page Language="C#" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<script runat="server">
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
    protected void SqlDataSourceSistemParametreleri_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        insertedValue = e.Command.Parameters["@newId"].Value != null ? e.Command.Parameters["@newId"].Value.ToString() : "";
    }
    protected void StoreSistemParametreleri_RefershData(object sender, StoreReadDataEventArgs e)
    {
        this.StoreSistemParametreleri.DataBind();
    }
    protected void StoreSistemParametreleri_AfterRecordInserted(object sender, AfterRecordInsertedEventArgs e)
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
    <title>SistemParametreleri</title>
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
    <script src="../../../../resources/js/enums.js"></script>    
       <!-- STATE PROVIDER VIA COOKIES -->
    <script language="javascript" type="text/javascript">
    Ext.state.Manager.setProvider(new Ext.state.CookieProvider({
        expires: new Date(new Date().getTime()+(1000*60*60*24*7)), //7 days from now
    }));
    Ext.onReady(function () {
        Ext.QuickTips.init();
    });
</script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" StateProvider="Cookie" />
        <asp:SqlDataSource ID="SqlDataSourceSistemParametreleri" runat="server" ConnectionString="<%$ ConnectionStrings:MIKROBAR %>"
            DeleteCommand="DELETE FROM [dbo].[SistemParametreleri] WHERE ([OID] = @OID)"
            InsertCommand="INSERT INTO [dbo].[SistemParametreleri] ( [ErpTur], [Isonline], [Entegrasyon], [GirisKontrol], [EntegrasyonKullaniciKod], [EntegrasyonKullaniciParola], [FirmaId], [IsyeriId], [FirmaKod], [IsYeriKod], [Aciklama], [DbVersiyonu], [PcVersiyonu], [MobilVersiyon], [OptimisticLockField], [GCRecord], [ApkVersiyon], [PartiTakibi], [SunucuGirisKontrol], [Olusturan], [OlusturmaTarihi], [Guncelleyen], [GuncellemeTarihi], [KaynakModul], [KaynakProgram], [CihazNo], [Entegre], [Durum], [RafKontrol], [ModulVersiyonKontrol], [VeriEntegrasyon], [LogKaydi], [WebServis]) VALUES ( @ErpTur, @Isonline, @Entegrasyon, @GirisKontrol, @EntegrasyonKullaniciKod, @EntegrasyonKullaniciParola, @FirmaId, @IsyeriId, @FirmaKod, @IsYeriKod, @Aciklama, @DbVersiyonu, @PcVersiyonu, @MobilVersiyon, @OptimisticLockField, @GCRecord, @ApkVersiyon, @PartiTakibi, @SunucuGirisKontrol, @Olusturan, @OlusturmaTarihi, @Guncelleyen, @GuncellemeTarihi, @KaynakModul, @KaynakProgram, @CihazNo, @Entegre, @Durum, @RafKontrol, @ModulVersiyonKontrol, @VeriEntegrasyon, @LogKaydi, @WebServis);  SELECT @newId = 1; "
            SelectCommand="SELECT  [OID], [ErpTur], [Isonline], [Entegrasyon], [GirisKontrol], [EntegrasyonKullaniciKod], [EntegrasyonKullaniciParola], [FirmaId], [IsyeriId], [FirmaKod], [IsYeriKod], [Aciklama], [DbVersiyonu], [PcVersiyonu], [MobilVersiyon], [OptimisticLockField], [GCRecord], [ApkVersiyon], [PartiTakibi], [SunucuGirisKontrol], [Olusturan], [OlusturmaTarihi], [Guncelleyen], [GuncellemeTarihi], [KaynakModul], [KaynakProgram], [CihazNo], [Entegre], [Durum], [RafKontrol], [ModulVersiyonKontrol], [VeriEntegrasyon], [LogKaydi], [WebServis] FROM [dbo].[SistemParametreleri] " 
            UpdateCommand="UPDATE [dbo].[SistemParametreleri] SET  [ErpTur] = @ErpTur, [Isonline] = @Isonline, [Entegrasyon] = @Entegrasyon, [GirisKontrol] = @GirisKontrol, [EntegrasyonKullaniciKod] = @EntegrasyonKullaniciKod, [EntegrasyonKullaniciParola] = @EntegrasyonKullaniciParola, [FirmaId] = @FirmaId, [IsyeriId] = @IsyeriId, [FirmaKod] = @FirmaKod, [IsYeriKod] = @IsYeriKod, [Aciklama] = @Aciklama, [DbVersiyonu] = @DbVersiyonu, [PcVersiyonu] = @PcVersiyonu, [MobilVersiyon] = @MobilVersiyon, [OptimisticLockField] = @OptimisticLockField, [GCRecord] = @GCRecord, [ApkVersiyon] = @ApkVersiyon, [PartiTakibi] = @PartiTakibi, [SunucuGirisKontrol] = @SunucuGirisKontrol, [Olusturan] = @Olusturan, [OlusturmaTarihi] = @OlusturmaTarihi, [Guncelleyen] = @Guncelleyen, [GuncellemeTarihi] = @GuncellemeTarihi, [KaynakModul] = @KaynakModul, [KaynakProgram] = @KaynakProgram, [CihazNo] = @CihazNo, [Entegre] = @Entegre, [Durum] = @Durum, [RafKontrol] = @RafKontrol, [ModulVersiyonKontrol] = @ModulVersiyonKontrol, [VeriEntegrasyon] = @VeriEntegrasyon, [LogKaydi] = @LogKaydi, [WebServis] = @WebServis WHERE ([OID] = @OID) "
            OnInserted="SqlDataSourceSistemParametreleri_Inserted">
            <DeleteParameters>
                <asp:Parameter Name="OID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
			<asp:Parameter Name="OID" Type="Int32" />
			<asp:Parameter Name="ErpTur" Type="Int32" />
			<asp:Parameter Name="Isonline" Type="Boolean" />
			<asp:Parameter Name="Entegrasyon" Type="Boolean" />
			<asp:Parameter Name="GirisKontrol" Type="Boolean" />
			<asp:Parameter Name="EntegrasyonKullaniciKod" Type="String" />
			<asp:Parameter Name="EntegrasyonKullaniciParola" Type="String" />
			<asp:Parameter Name="FirmaId" Type="Int32" />
			<asp:Parameter Name="IsyeriId" Type="Int32" />
			<asp:Parameter Name="FirmaKod" Type="String" />
			<asp:Parameter Name="IsYeriKod" Type="String" />
			<asp:Parameter Name="Aciklama" Type="String" />
			<asp:Parameter Name="DbVersiyonu" Type="String" />
			<asp:Parameter Name="PcVersiyonu" Type="String" />
			<asp:Parameter Name="MobilVersiyon" Type="String" />
			<asp:Parameter Name="OptimisticLockField" Type="Int32" />
			<asp:Parameter Name="GCRecord" Type="Int32" />
			<asp:Parameter Name="ApkVersiyon" Type="String" />
			<asp:Parameter Name="PartiTakibi" Type="Boolean" />
			<asp:Parameter Name="SunucuGirisKontrol" Type="Boolean" />
			<asp:Parameter Name="Olusturan" Type="Int32" />
			<asp:Parameter Name="OlusturmaTarihi" Type="DateTime" />
			<asp:Parameter Name="Guncelleyen" Type="Int32" />
			<asp:Parameter Name="GuncellemeTarihi" Type="DateTime" />
			<asp:Parameter Name="KaynakModul" Type="String" />
			<asp:Parameter Name="KaynakProgram" Type="Int32" />
			<asp:Parameter Name="CihazNo" Type="String" />
			<asp:Parameter Name="Entegre" Type="Boolean" />
			<asp:Parameter Name="Durum" Type="Int32" />
			<asp:Parameter Name="RafKontrol" Type="Boolean" />
			<asp:Parameter Name="ModulVersiyonKontrol" Type="Boolean" />
			<asp:Parameter Name="VeriEntegrasyon" Type="Boolean" />
			<asp:Parameter Name="LogKaydi" Type="Int32" />
			<asp:Parameter Name="WebServis" Type="String" />
            </UpdateParameters>
            <InsertParameters>
            <asp:Parameter Name="ErpTur" Type="Int32" />
            <asp:Parameter Name="Isonline" Type="Boolean" />
            <asp:Parameter Name="Entegrasyon" Type="Boolean" />
            <asp:Parameter Name="GirisKontrol" Type="Boolean" />
            <asp:Parameter Name="EntegrasyonKullaniciKod" Type="String" />
            <asp:Parameter Name="EntegrasyonKullaniciParola" Type="String" />
            <asp:Parameter Name="FirmaId" Type="Int32" />
            <asp:Parameter Name="IsyeriId" Type="Int32" />
            <asp:Parameter Name="FirmaKod" Type="String" />
            <asp:Parameter Name="IsYeriKod" Type="String" />
            <asp:Parameter Name="Aciklama" Type="String" />
            <asp:Parameter Name="DbVersiyonu" Type="String" />
            <asp:Parameter Name="PcVersiyonu" Type="String" />
            <asp:Parameter Name="MobilVersiyon" Type="String" />
            <asp:Parameter Name="OptimisticLockField" Type="Int32" />
            <asp:Parameter Name="GCRecord" Type="Int32" />
            <asp:Parameter Name="ApkVersiyon" Type="String" />
            <asp:Parameter Name="PartiTakibi" Type="Boolean" />
            <asp:Parameter Name="SunucuGirisKontrol" Type="Boolean" />
            <asp:Parameter Name="Olusturan" Type="Int32" />
            <asp:Parameter Name="OlusturmaTarihi" Type="DateTime" />
            <asp:Parameter Name="Guncelleyen" Type="Int32" />
            <asp:Parameter Name="GuncellemeTarihi" Type="DateTime" />
            <asp:Parameter Name="KaynakModul" Type="String" />
            <asp:Parameter Name="KaynakProgram" Type="Int32" />
            <asp:Parameter Name="CihazNo" Type="String" />
            <asp:Parameter Name="Entegre" Type="Boolean" />
            <asp:Parameter Name="Durum" Type="Int32" />
            <asp:Parameter Name="RafKontrol" Type="Boolean" />
            <asp:Parameter Name="ModulVersiyonKontrol" Type="Boolean" />
            <asp:Parameter Name="VeriEntegrasyon" Type="Boolean" />
            <asp:Parameter Name="LogKaydi" Type="Int32" />
            <asp:Parameter Name="WebServis" Type="String" />
            <asp:Parameter Direction="Output" Name="newId" Type="Int64" />
            </InsertParameters>
        </asp:SqlDataSource>
        <ext:Store ID="StoreSistemParametreleri" runat="server" DataSourceID="SqlDataSourceSistemParametreleri" ShowWarningOnFailure="false"
        OnReadData="StoreSistemParametreleri_RefershData" OnAfterRecordInserted="StoreSistemParametreleri_AfterRecordInserted" PageSize="23">
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="0" Mode="Raw" />
                <ext:Parameter Name="limit" Value="23" Mode="Raw" />
            </AutoLoadParams>
            <Model>
                <ext:Model ID="Model2" runat="server" IDProperty="OID" Name="SistemParametreleri" >
                    <Fields>
                        <ext:ModelField Name="OID" />
                        <ext:ModelField Name="ErpTur" />
                        <ext:ModelField Name="Isonline" />
                        <ext:ModelField Name="Entegrasyon" />
                        <ext:ModelField Name="GirisKontrol" />
                        <ext:ModelField Name="EntegrasyonKullaniciKod" />
                        <ext:ModelField Name="EntegrasyonKullaniciParola" />
                        <ext:ModelField Name="FirmaId" />
                        <ext:ModelField Name="IsyeriId" />
                        <ext:ModelField Name="FirmaKod" />
                        <ext:ModelField Name="IsYeriKod" />
                        <ext:ModelField Name="Aciklama" />
                        <ext:ModelField Name="DbVersiyonu" />
                        <ext:ModelField Name="PcVersiyonu" />
                        <ext:ModelField Name="MobilVersiyon" />
                        <ext:ModelField Name="OptimisticLockField" />
                        <ext:ModelField Name="GCRecord" />
                        <ext:ModelField Name="ApkVersiyon" />
                        <ext:ModelField Name="PartiTakibi" />
                        <ext:ModelField Name="SunucuGirisKontrol" />
                        <ext:ModelField Name="Olusturan" />
                        <ext:ModelField Name="OlusturmaTarihi" />
                        <ext:ModelField Name="Guncelleyen" />
                        <ext:ModelField Name="GuncellemeTarihi" />
                        <ext:ModelField Name="KaynakModul" />
                        <ext:ModelField Name="KaynakProgram" />
                        <ext:ModelField Name="CihazNo" />
                        <ext:ModelField Name="Entegre" />
                        <ext:ModelField Name="Durum" />
                        <ext:ModelField Name="RafKontrol" />
                        <ext:ModelField Name="ModulVersiyonKontrol" />
                        <ext:ModelField Name="VeriEntegrasyon" />
                        <ext:ModelField Name="LogKaydi" />
                        <ext:ModelField Name="WebServis" />
                    </Fields>
                </ext:Model>
            </Model>
            <Listeners>
                <Exception Handler="Ext.Msg.alert('Hata!', operation.getError());" />
                <Write Handler="Ext.Msg.alert('Kaydedildi', 'Islem basariyla kaydedildi.');" />
            </Listeners>
        </ext:Store>
        <ext:Viewport ID="ViewportSistemParametreleri" Layout="BorderLayout" runat="server">
            <Items>
                <ext:Panel ID="PanelSistemParametreleri" runat="server" Region="Center" Height="300" Header="false" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="GridPanelSistemParametreleri" runat="server" Stateful="true"  Title="SistemParametreleri" StoreID="StoreSistemParametreleri" Border="false" Icon="Lorry">
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" />
                                    <ext:Column ID="ColumnErpTur" runat="server" DataIndex="ErpTur" Text="ErpTur">
                                        <Editor>
                                            <ext:ComboBox ID="cbErpTur" runat="server" >
                                                <Items>
                                                    <ext:ListItem Text="UyumSoft (WebErp)" Value="0" />
                                                    <ext:ListItem Text="UyumSoft (Progress)" Value="1" />
                                                    <ext:ListItem Text="Logo" Value="2" />
                                                    <ext:ListItem Text="Sap" Value="3" />
                                                    <ext:ListItem Text="Netsis" Value="4" />
                                                    <ext:ListItem Text="Mikro" Value="5" />
                                                    <ext:ListItem Text="Axapta" Value="6" />
                                                    <ext:ListItem Text="Diger" Value="7" />
                                                </Items>
                                                </ext:ComboBox>
                                        </Editor>
                                        <Renderer Fn="changeErpTur" />
                                    </ext:Column>
                                    <ext:CheckColumn ID="CheckColumnIsonline" runat="server" Text="Isonline?" DataIndex="Isonline" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnEntegrasyon" runat="server" Text="Entegrasyon?" DataIndex="Entegrasyon" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnGirisKontrol" runat="server" Text="GirisKontrol?" DataIndex="GirisKontrol" StopSelection="false" Editable="true" Width="70" />
                                    <ext:Column ID="ColumnEntegrasyonKullaniciKod" runat="server" DataIndex="EntegrasyonKullaniciKod" Text="EntegrasyonKullaniciKod">
                                        <Editor>
                                            <ext:TextField ID="TextFieldEntegrasyonKullaniciKod" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnEntegrasyonKullaniciParola" runat="server" DataIndex="EntegrasyonKullaniciParola" Text="EntegrasyonKullaniciParola">
                                        <Editor>
                                            <ext:TextField ID="TextFieldEntegrasyonKullaniciParola" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnFirmaId" runat="server" DataIndex="FirmaId" Text="FirmaId">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldFirmaId" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnIsyeriId" runat="server" DataIndex="IsyeriId" Text="IsyeriId">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldIsyeriId" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnFirmaKod" runat="server" DataIndex="FirmaKod" Text="FirmaKod">
                                        <Editor>
                                            <ext:TextField ID="TextFieldFirmaKod" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnIsYeriKod" runat="server" DataIndex="IsYeriKod" Text="IsYeriKod">
                                        <Editor>
                                            <ext:TextField ID="TextFieldIsYeriKod" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnAciklama" runat="server" DataIndex="Aciklama" Text="Aciklama">
                                        <Editor>
                                            <ext:TextField ID="TextFieldAciklama" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnDbVersiyonu" runat="server" DataIndex="DbVersiyonu" Text="DbVersiyonu">
                                        <Editor>
                                            <ext:TextField ID="TextFieldDbVersiyonu" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnPcVersiyonu" runat="server" DataIndex="PcVersiyonu" Text="PcVersiyonu">
                                        <Editor>
                                            <ext:TextField ID="TextFieldPcVersiyonu" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnMobilVersiyon" runat="server" DataIndex="MobilVersiyon" Text="MobilVersiyon">
                                        <Editor>
                                            <ext:TextField ID="TextFieldMobilVersiyon" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnOptimisticLockField" runat="server" DataIndex="OptimisticLockField" Text="OptimisticLockField">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldOptimisticLockField" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnGCRecord" runat="server" DataIndex="GCRecord" Text="GCRecord">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldGCRecord" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnApkVersiyon" runat="server" DataIndex="ApkVersiyon" Text="ApkVersiyon">
                                        <Editor>
                                            <ext:TextField ID="TextFieldApkVersiyon" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="CheckColumnPartiTakibi" runat="server" Text="PartiTakibi?" DataIndex="PartiTakibi" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnSunucuGirisKontrol" runat="server" Text="SunucuGirisKontrol?" DataIndex="SunucuGirisKontrol" StopSelection="false" Editable="true" Width="70" />
                                    <ext:Column ID="ColumnOlusturan" runat="server" DataIndex="Olusturan" Text="Olusturan">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldOlusturan" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnOlusturmaTarihi" runat="server" DataIndex="OlusturmaTarihi" Text="OlusturmaTarihi">
                                        <Editor>
                                            <ext:DateField ID="TextFieldOlusturmaTarihi" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnGuncelleyen" runat="server" DataIndex="Guncelleyen" Text="Guncelleyen">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldGuncelleyen" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnGuncellemeTarihi" runat="server" DataIndex="GuncellemeTarihi" Text="GuncellemeTarihi">
                                        <Editor>
                                            <ext:DateField ID="TextFieldGuncellemeTarihi" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnKaynakModul" runat="server" DataIndex="KaynakModul" Text="KaynakModul">
                                        <Editor>
                                            <ext:TextField ID="TextFieldKaynakModul" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnKaynakProgram" runat="server" DataIndex="KaynakProgram" Text="KaynakProgram">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldKaynakProgram" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnCihazNo" runat="server" DataIndex="CihazNo" Text="CihazNo">
                                        <Editor>
                                            <ext:TextField ID="TextFieldCihazNo" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="CheckColumnEntegre" runat="server" Text="Entegre?" DataIndex="Entegre" StopSelection="false" Editable="true" Width="70" />
                                    <ext:Column ID="ColumnDurum" runat="server" DataIndex="Durum" Text="Durum">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldDurum" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="CheckColumnRafKontrol" runat="server" Text="RafKontrol?" DataIndex="RafKontrol" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnModulVersiyonKontrol" runat="server" Text="ModulVersiyonKontrol?" DataIndex="ModulVersiyonKontrol" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnVeriEntegrasyon" runat="server" Text="VeriEntegrasyon?" DataIndex="VeriEntegrasyon" StopSelection="false" Editable="true" Width="70" />
                                    <ext:Column ID="ColumnLogKaydi" runat="server" DataIndex="LogKaydi" Text="LogKaydi">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldLogKaydi" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnWebServis" runat="server" DataIndex="WebServis" Text="WebServis">
                                        <Editor>
                                            <ext:TextField ID="TextFieldWebServis" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbarSistemParametreleri" runat="server" PageSize="23" StoreID="StoreSistemParametreleri" DisplayInfo="true" DisplayMsg="Gösterilen {0} - {1} / {2}" EmptyMsg="Kayýt yok."/>
                            </BottomBar>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModelSistemParametreleri" runat="server" Mode="Multi" />
                            </SelectionModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingSistemParametreleri" runat="server" />
                            </Plugins>
                        </ext:GridPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnSistemParametreleriSave" runat="server"  Text="Kaydet" Icon="Disk" ToolTip="Degisiklikleri kaydet" >
                            <Listeners>
                                <Click Handler="#{StoreSistemParametreleri}.sync();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnSistemParametreleriDelete" runat="server"  Text="Secileni Sil" Icon="Delete" >
                            <Listeners>
                                <Click Handler="#{GridPanelSistemParametreleri}.deleteSelected();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnSistemParametreleriInsert" runat="server"  Text="Ekle" Icon="Add" >
                            <Listeners>
                                <Click Handler="#{StoreSistemParametreleri}.insert(0, {});#{GridPanelSistemParametreleri}.editingPlugin.startEditByPosition({row:0, column:2});" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnSistemParametreleriRefresh" runat="server"  Text="Yenile" Icon="ArrowRefresh">
                            <Listeners>
                                <Click Handler="#{StoreSistemParametreleri}.reload();" />
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
