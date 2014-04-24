<!-- Istasyonlar -->
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
    protected void SqlDataSourceIstasyonlar_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        insertedValue = e.Command.Parameters["@newId"].Value != null ? e.Command.Parameters["@newId"].Value.ToString() : "";
    }
    protected void StoreIstasyonlar_RefershData(object sender, StoreReadDataEventArgs e)
    {
        this.StoreIstasyonlar.DataBind();
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
        <asp:SqlDataSource ID="SqlDataSourceIstasyonlar" runat="server" ConnectionString="<%$ ConnectionStrings:MIKROBAR %>"
            DeleteCommand="DELETE FROM [dbo].[Istasyonlar] WHERE ([IstasyonId] = @IstasyonId)"
            InsertCommand="INSERT INTO [dbo].[Istasyonlar] ( [IstasyonId], [IstasyonKod], [IstasyonAd], [Aciklama], [FasonIstasyon], [IsMerkeziId], [IsMerkeziKod], [IsMerkeziAd], [DepoId], [DepoKod], [DepoId2], [DepoKod2], [IsIstasyonTipId], [CokluUretim], [ArayaIsSokma], [Olusturan], [OlusturmaTarihi], [Guncelleyen], [GuncellemeTarihi], [KaynakModul], [CihazNo], [Entegre], [OptimisticLockField], [GCRecord], [CariId], [DepoId3], [KaynakProgram], [Durum], [MalzemeCikisDepo], [UrunGirisDepo], [YariMamulCikisDepo], [YariMamulGirisDepo], [IscilikTakibi], [DurusSuresi], [SeriOlustur], [Yazdir], [TopluKapat], [Otomasyon], [UretimKatsayiSor], [MaliyetKatsayiSor], [SurecKaliteKontrolVar], [CariBazindaUretim], [HurdayiEtiketle], [VarsayilanUretimMiktari], [EsitTuketim], [EkAlanlar]) VALUES ( @IstasyonId, @IstasyonKod, @IstasyonAd, @Aciklama, @FasonIstasyon, @IsMerkeziId, @IsMerkeziKod, @IsMerkeziAd, @DepoId, @DepoKod, @DepoId2, @DepoKod2, @IsIstasyonTipId, @CokluUretim, @ArayaIsSokma, @Olusturan, @OlusturmaTarihi, @Guncelleyen, @GuncellemeTarihi, @KaynakModul, @CihazNo, @Entegre, @OptimisticLockField, @GCRecord, @CariId, @DepoId3, @KaynakProgram, @Durum, @MalzemeCikisDepo, @UrunGirisDepo, @YariMamulCikisDepo, @YariMamulGirisDepo, @IscilikTakibi, @DurusSuresi, @SeriOlustur, @Yazdir, @TopluKapat, @Otomasyon, @UretimKatsayiSor, @MaliyetKatsayiSor, @SurecKaliteKontrolVar, @CariBazindaUretim, @HurdayiEtiketle, @VarsayilanUretimMiktari, @EsitTuketim, @EkAlanlar);  SELECT @newId = 1; "
            SelectCommand="SELECT  [IstasyonId], [IstasyonKod], [IstasyonAd], [Aciklama], [FasonIstasyon], [IsMerkeziId], [IsMerkeziKod], [IsMerkeziAd], [DepoId], [DepoKod], [DepoId2], [DepoKod2], [IsIstasyonTipId], [CokluUretim], [ArayaIsSokma], [Olusturan], [OlusturmaTarihi], [Guncelleyen], [GuncellemeTarihi], [KaynakModul], [CihazNo], [Entegre], [OptimisticLockField], [GCRecord], [CariId], [DepoId3], [KaynakProgram], [Durum], [MalzemeCikisDepo], [UrunGirisDepo], [YariMamulCikisDepo], [YariMamulGirisDepo], [IscilikTakibi], [DurusSuresi], [SeriOlustur], [Yazdir], [TopluKapat], [Otomasyon], [UretimKatsayiSor], [MaliyetKatsayiSor], [SurecKaliteKontrolVar], [CariBazindaUretim], [HurdayiEtiketle], [VarsayilanUretimMiktari], [EsitTuketim], [EkAlanlar] FROM [dbo].[Istasyonlar] " 
            UpdateCommand="UPDATE [dbo].[Istasyonlar] SET  [IstasyonId] = @IstasyonId, [IstasyonKod] = @IstasyonKod, [IstasyonAd] = @IstasyonAd, [Aciklama] = @Aciklama, [FasonIstasyon] = @FasonIstasyon, [IsMerkeziId] = @IsMerkeziId, [IsMerkeziKod] = @IsMerkeziKod, [IsMerkeziAd] = @IsMerkeziAd, [DepoId] = @DepoId, [DepoKod] = @DepoKod, [DepoId2] = @DepoId2, [DepoKod2] = @DepoKod2, [IsIstasyonTipId] = @IsIstasyonTipId, [CokluUretim] = @CokluUretim, [ArayaIsSokma] = @ArayaIsSokma, [Olusturan] = @Olusturan, [OlusturmaTarihi] = @OlusturmaTarihi, [Guncelleyen] = @Guncelleyen, [GuncellemeTarihi] = @GuncellemeTarihi, [KaynakModul] = @KaynakModul, [CihazNo] = @CihazNo, [Entegre] = @Entegre, [OptimisticLockField] = @OptimisticLockField, [GCRecord] = @GCRecord, [CariId] = @CariId, [DepoId3] = @DepoId3, [KaynakProgram] = @KaynakProgram, [Durum] = @Durum, [MalzemeCikisDepo] = @MalzemeCikisDepo, [UrunGirisDepo] = @UrunGirisDepo, [YariMamulCikisDepo] = @YariMamulCikisDepo, [YariMamulGirisDepo] = @YariMamulGirisDepo, [IscilikTakibi] = @IscilikTakibi, [DurusSuresi] = @DurusSuresi, [SeriOlustur] = @SeriOlustur, [Yazdir] = @Yazdir, [TopluKapat] = @TopluKapat, [Otomasyon] = @Otomasyon, [UretimKatsayiSor] = @UretimKatsayiSor, [MaliyetKatsayiSor] = @MaliyetKatsayiSor, [SurecKaliteKontrolVar] = @SurecKaliteKontrolVar, [CariBazindaUretim] = @CariBazindaUretim, [HurdayiEtiketle] = @HurdayiEtiketle, [VarsayilanUretimMiktari] = @VarsayilanUretimMiktari, [EsitTuketim] = @EsitTuketim, [EkAlanlar] = @EkAlanlar WHERE ([IstasyonId] = @IstasyonId) "
            OnInserted="SqlDataSourceIstasyonlar_Inserted" >
            <DeleteParameters>
                <asp:Parameter Name="IstasyonId" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
			<asp:Parameter Name="IstasyonId" Type="Int32" />
			<asp:Parameter Name="IstasyonKod" Type="String" />
			<asp:Parameter Name="IstasyonAd" Type="String" />
			<asp:Parameter Name="Aciklama" Type="String" />
			<asp:Parameter Name="FasonIstasyon" Type="Boolean" />
			<asp:Parameter Name="IsMerkeziId" Type="Int32" />
			<asp:Parameter Name="IsMerkeziKod" Type="String" />
			<asp:Parameter Name="IsMerkeziAd" Type="String" />
			<asp:Parameter Name="DepoId" Type="Int32" />
			<asp:Parameter Name="DepoKod" Type="String" />
			<asp:Parameter Name="DepoId2" Type="Int32" />
			<asp:Parameter Name="DepoKod2" Type="String" />
			<asp:Parameter Name="IsIstasyonTipId" Type="Int32" />
			<asp:Parameter Name="CokluUretim" Type="Boolean" />
			<asp:Parameter Name="ArayaIsSokma" Type="Boolean" />
			<asp:Parameter Name="Olusturan" Type="Int32" />
			<asp:Parameter Name="OlusturmaTarihi" Type="DateTime" />
			<asp:Parameter Name="Guncelleyen" Type="Int32" />
			<asp:Parameter Name="GuncellemeTarihi" Type="DateTime" />
			<asp:Parameter Name="KaynakModul" Type="String" />
			<asp:Parameter Name="CihazNo" Type="String" />
			<asp:Parameter Name="Entegre" Type="Boolean" />
			<asp:Parameter Name="OptimisticLockField" Type="Int32" />
			<asp:Parameter Name="GCRecord" Type="Int32" />
			<asp:Parameter Name="CariId" Type="Int32" />
			<asp:Parameter Name="DepoId3" Type="Int32" />
			<asp:Parameter Name="KaynakProgram" Type="Int32" />
			<asp:Parameter Name="Durum" Type="Int32" />
			<asp:Parameter Name="MalzemeCikisDepo" Type="Int32" />
			<asp:Parameter Name="UrunGirisDepo" Type="Int32" />
			<asp:Parameter Name="YariMamulCikisDepo" Type="Int32" />
			<asp:Parameter Name="YariMamulGirisDepo" Type="Int32" />
			<asp:Parameter Name="IscilikTakibi" Type="Int32" />
			<asp:Parameter Name="DurusSuresi" Type="String" />
			<asp:Parameter Name="SeriOlustur" Type="Boolean" />
			<asp:Parameter Name="Yazdir" Type="Boolean" />
			<asp:Parameter Name="TopluKapat" Type="Boolean" />
			<asp:Parameter Name="Otomasyon" Type="Boolean" />
			<asp:Parameter Name="UretimKatsayiSor" Type="Boolean" />
			<asp:Parameter Name="MaliyetKatsayiSor" Type="Boolean" />
			<asp:Parameter Name="SurecKaliteKontrolVar" Type="Boolean" />
			<asp:Parameter Name="CariBazindaUretim" Type="Boolean" />
			<asp:Parameter Name="HurdayiEtiketle" Type="Boolean" />
			<asp:Parameter Name="VarsayilanUretimMiktari" Type="String" />
			<asp:Parameter Name="EsitTuketim" Type="Boolean" />
			<asp:Parameter Name="EkAlanlar" Type="Boolean" />
            </UpdateParameters>
            <InsertParameters>
            <asp:Parameter Name="IstasyonId" Type="Int32" />
            <asp:Parameter Name="IstasyonKod" Type="String" />
            <asp:Parameter Name="IstasyonAd" Type="String" />
            <asp:Parameter Name="Aciklama" Type="String" />
            <asp:Parameter Name="FasonIstasyon" Type="Boolean" />
            <asp:Parameter Name="IsMerkeziId" Type="Int32" />
            <asp:Parameter Name="IsMerkeziKod" Type="String" />
            <asp:Parameter Name="IsMerkeziAd" Type="String" />
            <asp:Parameter Name="DepoId" Type="Int32" />
            <asp:Parameter Name="DepoKod" Type="String" />
            <asp:Parameter Name="DepoId2" Type="Int32" />
            <asp:Parameter Name="DepoKod2" Type="String" />
            <asp:Parameter Name="IsIstasyonTipId" Type="Int32" />
            <asp:Parameter Name="CokluUretim" Type="Boolean" />
            <asp:Parameter Name="ArayaIsSokma" Type="Boolean" />
            <asp:Parameter Name="Olusturan" Type="Int32" />
            <asp:Parameter Name="OlusturmaTarihi" Type="DateTime" />
            <asp:Parameter Name="Guncelleyen" Type="Int32" />
            <asp:Parameter Name="GuncellemeTarihi" Type="DateTime" />
            <asp:Parameter Name="KaynakModul" Type="String" />
            <asp:Parameter Name="CihazNo" Type="String" />
            <asp:Parameter Name="Entegre" Type="Boolean" />
            <asp:Parameter Name="OptimisticLockField" Type="Int32" />
            <asp:Parameter Name="GCRecord" Type="Int32" />
            <asp:Parameter Name="CariId" Type="Int32" />
            <asp:Parameter Name="DepoId3" Type="Int32" />
            <asp:Parameter Name="KaynakProgram" Type="Int32" />
            <asp:Parameter Name="Durum" Type="Int32" />
            <asp:Parameter Name="MalzemeCikisDepo" Type="Int32" />
            <asp:Parameter Name="UrunGirisDepo" Type="Int32" />
            <asp:Parameter Name="YariMamulCikisDepo" Type="Int32" />
            <asp:Parameter Name="YariMamulGirisDepo" Type="Int32" />
            <asp:Parameter Name="IscilikTakibi" Type="Int32" />
            <asp:Parameter Name="DurusSuresi" Type="String" />
            <asp:Parameter Name="SeriOlustur" Type="Boolean" />
            <asp:Parameter Name="Yazdir" Type="Boolean" />
            <asp:Parameter Name="TopluKapat" Type="Boolean" />
            <asp:Parameter Name="Otomasyon" Type="Boolean" />
            <asp:Parameter Name="UretimKatsayiSor" Type="Boolean" />
            <asp:Parameter Name="MaliyetKatsayiSor" Type="Boolean" />
            <asp:Parameter Name="SurecKaliteKontrolVar" Type="Boolean" />
            <asp:Parameter Name="CariBazindaUretim" Type="Boolean" />
            <asp:Parameter Name="HurdayiEtiketle" Type="Boolean" />
            <asp:Parameter Name="VarsayilanUretimMiktari" Type="String" />
            <asp:Parameter Name="EsitTuketim" Type="Boolean" />
            <asp:Parameter Name="EkAlanlar" Type="Boolean" />
            <asp:Parameter Direction="Output" Name="newId" Type="Int64" />
            </InsertParameters>
        </asp:SqlDataSource>

        <ext:Store ID="StoreIstasyonlar" runat="server" DataSourceID="SqlDataSourceIstasyonlar" ShowWarningOnFailure="false"
        OnReadData="StoreIstasyonlar_RefershData" OnAfterRecordInserted="StoreIstasyonlar_AfterRecordInserted" PageSize="23">
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="0" Mode="Raw" />
                <ext:Parameter Name="limit" Value="23" Mode="Raw" />
            </AutoLoadParams>
            <Model>
                <ext:Model ID="Model2" runat="server" IDProperty="IstasyonId" Name="Istasyonlar" >
                    <Fields>
                        <ext:ModelField Name="IstasyonId" />
                        <ext:ModelField Name="IstasyonKod" />
                        <ext:ModelField Name="IstasyonAd" />
                        <ext:ModelField Name="Aciklama" />
                        <ext:ModelField Name="FasonIstasyon" />
                        <ext:ModelField Name="IsMerkeziId" />
                        <ext:ModelField Name="IsMerkeziKod" />
                        <ext:ModelField Name="IsMerkeziAd" />
                        <ext:ModelField Name="DepoId" />
                        <ext:ModelField Name="DepoKod" />
                        <ext:ModelField Name="DepoId2" />
                        <ext:ModelField Name="DepoKod2" />
                        <ext:ModelField Name="IsIstasyonTipId" />
                        <ext:ModelField Name="CokluUretim" />
                        <ext:ModelField Name="ArayaIsSokma" />
                        <ext:ModelField Name="Olusturan" />
                        <ext:ModelField Name="OlusturmaTarihi" />
                        <ext:ModelField Name="Guncelleyen" />
                        <ext:ModelField Name="GuncellemeTarihi" />
                        <ext:ModelField Name="KaynakModul" />
                        <ext:ModelField Name="CihazNo" />
                        <ext:ModelField Name="Entegre" />
                        <ext:ModelField Name="OptimisticLockField" />
                        <ext:ModelField Name="GCRecord" />
                        <ext:ModelField Name="CariId" />
                        <ext:ModelField Name="DepoId3" />
                        <ext:ModelField Name="KaynakProgram" />
                        <ext:ModelField Name="Durum" />
                        <ext:ModelField Name="MalzemeCikisDepo" />
                        <ext:ModelField Name="UrunGirisDepo" />
                        <ext:ModelField Name="YariMamulCikisDepo" />
                        <ext:ModelField Name="YariMamulGirisDepo" />
                        <ext:ModelField Name="IscilikTakibi" />
                        <ext:ModelField Name="DurusSuresi" />
                        <ext:ModelField Name="SeriOlustur" />
                        <ext:ModelField Name="Yazdir" />
                        <ext:ModelField Name="TopluKapat" />
                        <ext:ModelField Name="Otomasyon" />
                        <ext:ModelField Name="UretimKatsayiSor" />
                        <ext:ModelField Name="MaliyetKatsayiSor" />
                        <ext:ModelField Name="SurecKaliteKontrolVar" />
                        <ext:ModelField Name="CariBazindaUretim" />
                        <ext:ModelField Name="HurdayiEtiketle" />
                        <ext:ModelField Name="VarsayilanUretimMiktari" />
                        <ext:ModelField Name="EsitTuketim" />
                        <ext:ModelField Name="EkAlanlar" />
                    </Fields>
                </ext:Model>
            </Model>
            <Listeners>
                <Exception Handler="Ext.Msg.alert('Hata!', operation.getError());" />
                <Write Handler="Ext.Msg.alert('Kaydedildi', 'Islem basariyla kaydedildi.');" />
            </Listeners>
        </ext:Store>

        <ext:Viewport ID="ViewportIstasyonlar" Layout="BorderLayout" runat="server">
            <Items>
                <ext:Panel ID="PanelIstasyonlar" runat="server" Region="Center" Height="300" Header="false" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="GridPanelIstasyonlar" runat="server"  Title="Istasyonlar" StoreID="StoreIstasyonlar" Border="false" Icon="Lorry">
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
                                    <ext:Column ID="ColumnIstasyonAd" runat="server" DataIndex="IstasyonAd" Text="IstasyonAd">
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
                                    <ext:Column ID="ColumnDepoId" runat="server" DataIndex="DepoId" Text="DepoId">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldDepoId" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnDepoKod" runat="server" DataIndex="DepoKod" Text="DepoKod">
                                        <Editor>
                                            <ext:TextField ID="TextFieldDepoKod" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnDepoId2" runat="server" DataIndex="DepoId2" Text="DepoId2">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldDepoId2" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnDepoKod2" runat="server" DataIndex="DepoKod2" Text="DepoKod2">
                                        <Editor>
                                            <ext:TextField ID="TextFieldDepoKod2" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnIsIstasyonTipId" runat="server" DataIndex="IsIstasyonTipId" Text="IsIstasyonTipId">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldIsIstasyonTipId" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="CheckColumnCokluUretim" runat="server" Text="CokluUretim?" DataIndex="CokluUretim" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnArayaIsSokma" runat="server" Text="ArayaIsSokma?" DataIndex="ArayaIsSokma" StopSelection="false" Editable="true" Width="70" />
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
                                    <ext:Column ID="ColumnCihazNo" runat="server" DataIndex="CihazNo" Text="CihazNo">
                                        <Editor>
                                            <ext:TextField ID="TextFieldCihazNo" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="CheckColumnEntegre" runat="server" Text="Entegre?" DataIndex="Entegre" StopSelection="false" Editable="true" Width="70" />
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
                                    <ext:Column ID="ColumnCariId" runat="server" DataIndex="CariId" Text="CariId">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldCariId" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnDepoId3" runat="server" DataIndex="DepoId3" Text="DepoId3">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldDepoId3" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnKaynakProgram" runat="server" DataIndex="KaynakProgram" Text="KaynakProgram">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldKaynakProgram" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnDurum" runat="server" DataIndex="Durum" Text="Durum">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldDurum" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnMalzemeCikisDepo" runat="server" DataIndex="MalzemeCikisDepo" Text="MalzemeCikisDepo">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldMalzemeCikisDepo" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnUrunGirisDepo" runat="server" DataIndex="UrunGirisDepo" Text="UrunGirisDepo">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldUrunGirisDepo" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnYariMamulCikisDepo" runat="server" DataIndex="YariMamulCikisDepo" Text="YariMamulCikisDepo">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldYariMamulCikisDepo" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnYariMamulGirisDepo" runat="server" DataIndex="YariMamulGirisDepo" Text="YariMamulGirisDepo">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldYariMamulGirisDepo" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnIscilikTakibi" runat="server" DataIndex="IscilikTakibi" Text="IscilikTakibi">
                                        <Editor>
                                            <ext:NumberField ID="TextFieldIscilikTakibi" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ColumnDurusSuresi" runat="server" DataIndex="DurusSuresi" Text="DurusSuresi">
                                        <Editor>
                                            <ext:TextField ID="TextFieldDurusSuresi" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="CheckColumnSeriOlustur" runat="server" Text="SeriOlustur?" DataIndex="SeriOlustur" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnYazdir" runat="server" Text="Yazdir?" DataIndex="Yazdir" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnTopluKapat" runat="server" Text="TopluKapat?" DataIndex="TopluKapat" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnOtomasyon" runat="server" Text="Otomasyon?" DataIndex="Otomasyon" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnUretimKatsayiSor" runat="server" Text="UretimKatsayiSor?" DataIndex="UretimKatsayiSor" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnMaliyetKatsayiSor" runat="server" Text="MaliyetKatsayiSor?" DataIndex="MaliyetKatsayiSor" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnSurecKaliteKontrolVar" runat="server" Text="SurecKaliteKontrolVar?" DataIndex="SurecKaliteKontrolVar" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnCariBazindaUretim" runat="server" Text="CariBazindaUretim?" DataIndex="CariBazindaUretim" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnHurdayiEtiketle" runat="server" Text="HurdayiEtiketle?" DataIndex="HurdayiEtiketle" StopSelection="false" Editable="true" Width="70" />
                                    <ext:Column ID="ColumnVarsayilanUretimMiktari" runat="server" DataIndex="VarsayilanUretimMiktari" Text="VarsayilanUretimMiktari">
                                        <Editor>
                                            <ext:TextField ID="TextFieldVarsayilanUretimMiktari" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="CheckColumnEsitTuketim" runat="server" Text="EsitTuketim?" DataIndex="EsitTuketim" StopSelection="false" Editable="true" Width="70" />
                                    <ext:CheckColumn ID="CheckColumnEkAlanlar" runat="server" Text="EkAlanlar?" DataIndex="EkAlanlar" StopSelection="false" Editable="true" Width="70" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbarIstasyonlar" runat="server" PageSize="23" StoreID="StoreIstasyonlar" DisplayInfo="true" DisplayMsg="Gösterilen {0} - {1} / {2}" EmptyMsg="Kayýt yok."/>
                            </BottomBar>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModelIstasyonlar" runat="server" Mode="Multi" />
                            </SelectionModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingIstasyonlar" runat="server" />
                            </Plugins>
                        </ext:GridPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnIstasyonlarSave" runat="server"  Text="Kaydet" Icon="Disk" ToolTip="Degisiklikleri kaydet" >
                            <Listeners>
                                <Click Handler="#{StoreIstasyonlar}.sync();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnIstasyonlarDelete" runat="server"  Text="Secileni Sil" Icon="Delete" >
                            <Listeners>
                                <Click Handler="#{GridPanelIstasyonlar}.deleteSelected();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnIstasyonlarInsert" runat="server"  Text="Ekle" Icon="Add" >
                            <Listeners>
                                <Click Handler="#{StoreIstasyonlar}.insert(0, {});#{GridPanelIstasyonlar}.editingPlugin.startEditByPosition({row:0, column:2});" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnIstasyonlarRefresh" runat="server"  Text="Yenile" Icon="ArrowRefresh">
                            <Listeners>
                                <Click Handler="#{StoreIstasyonlar}.reload();" />
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
