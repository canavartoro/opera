using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.PivotChart;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Mikrobar.Module.BusinessObjects;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Security.Strategy;

namespace Mikrobar.Module.DatabaseUpdate
{
    public class Updater : DevExpress.ExpressApp.Updating.ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            CreateAnonymousAccess();
            SecuritySystemRole defaultRole = CreateDefaultRole();
            defaultRole.Save();

            #region Create Users for the Complex Security Strategy
            SecuritySystemRole adminRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
                adminRole.CanEditModel = true;
                adminRole.IsAdministrative = true;
                adminRole.Save();
            }

            SistemKullanicilari user1 = ObjectSpace.FindObject<SistemKullanicilari>(new BinaryOperator("UserName", "uroot"));
            if (user1 == null)
            {
                user1 = ObjectSpace.CreateObject<SistemKullanicilari>();
                user1.Kullanici = ObjectSpace.CreateObject<Kullanicilar>();
                user1.Kullanici.KullaniciId = 1;
                user1.Kullanici.KullaniciKod = "uroot";
                user1.Kullanici.KullaniciKod2 = "uroot";
                user1.Kullanici.Parola = "1";
                user1.IsActive = true;
                user1.Kullanici.Save();
                user1.UserName = "uroot";
                user1.KullaniciKod2 = "uroot";
                user1.Durum = KayitDurumu.Yeni;
                user1.SetPassword("1");
                user1.Roles.Add(adminRole);
                user1.Save();
            }

            SecuritySystemRole userRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Users"));
            if (userRole == null)
            {
                userRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                userRole.Name = "Users";
                userRole.EnsureTypePermissions<SistemKullanicilari>(SecurityOperations.ReadOnlyAccess);
                userRole.EnsureTypePermissions<Kullanicilar>(SecurityOperations.ReadOnlyAccess);
                userRole.EnsureTypePermissions<KullaniciDetaylari>(SecurityOperations.ReadOnlyAccess);
                userRole.Save();
            }

            SistemKullanicilari user2 = ObjectSpace.FindObject<SistemKullanicilari>(new BinaryOperator("UserName", "rapor"));
            if (user2 == null)
            {
                user2 = ObjectSpace.CreateObject<SistemKullanicilari>();
                user2.Kullanici = ObjectSpace.CreateObject<Kullanicilar>();
                user2.Kullanici.KullaniciId = 2;
                user2.Kullanici.KullaniciKod = "rapor";
                user2.Kullanici.KullaniciKod2 = "uroot";
                user2.Kullanici.Parola = "1";
                user2.IsActive = true;
                user2.Kullanici.Save();
                user2.UserName = "rapor";
                user2.KullaniciKod2 = "uroot";
                user2.Parola = "1";
                user2.Durum = KayitDurumu.Yeni;
                user2.SetPassword("1");
                user2.Roles.Add(defaultRole);
                user2.Roles.Add(userRole);
                user2.Save();
            }

            SistemKullanicilari user3 = ObjectSpace.FindObject<SistemKullanicilari>(new BinaryOperator("UserName", "Anonymous"));
            if (user3 == null)
            {
                user3 = ObjectSpace.CreateObject<SistemKullanicilari>();
                user3.UserName = "Anonymous";
                user3.KullaniciKod2 = "uroot";
                user3.Kullanici = ObjectSpace.CreateObject<Kullanicilar>();
                user3.Kullanici.KullaniciId = 3;
                user3.Kullanici.KullaniciKod = "Anonymous";
                user3.Kullanici.KullaniciKod2 = "uroot";
                user3.Kullanici.Parola = "1";
                user3.Kullanici.Save();
                user3.Durum = KayitDurumu.Yeni;
                user3.SetPassword("1");
                user3.Roles.Add(userRole);
                user3.Roles.Add(defaultRole);
                user3.Save();
            }
            #endregion

            //MikrobarUpdate(false);
            ObjectSpace.CommitChanges();
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();

            //MikrobarUpdate(true);
            ObjectSpace.CommitChanges();
        }
        private SecuritySystemRole CreateDefaultRole()
        {
            SecuritySystemRole defaultRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Default"));
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                defaultRole.Name = "Default";
                defaultRole.AddObjectAccessPermission<SecuritySystemUser>("[Oid] = CurrentUserId()", SecurityOperations.ReadOnlyAccess);
                defaultRole.AddMemberAccessPermission<SecuritySystemUser>("ChangePasswordOnFirstLogon", SecurityOperations.Write);
                defaultRole.AddMemberAccessPermission<SecuritySystemUser>("StoredPassword", SecurityOperations.Write);
                defaultRole.SetTypePermissionsRecursively<SecuritySystemRole>(SecurityOperations.Read, SecuritySystemModifier.Allow);
                defaultRole.SetTypePermissionsRecursively<AuditDataItemPersistent>(SecurityOperations.CRUDAccess, SecuritySystemModifier.Allow);
            }
            return defaultRole;
        }
        private void CreateAnonymousAccess()
        {
            //SecurityStrategy.AnonymousUserName
            SecuritySystemRole anonymousRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Anonymous"));
            if (anonymousRole == null)
            {
                anonymousRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                anonymousRole.Name = "Anonymous";
                anonymousRole.AddMemberAccessPermission<SecuritySystemUser>("ChangePasswordOnFirstLogon", SecurityOperations.Write);
                anonymousRole.Save();
            }

            SistemKullanicilari anonymousUser = ObjectSpace.FindObject<SistemKullanicilari>(new BinaryOperator("UserName", "Anonymous"));
            if (anonymousUser == null)
            {
                anonymousUser = ObjectSpace.CreateObject<SistemKullanicilari>();
                anonymousUser.UserName = "Anonymous";
                anonymousUser.IsActive = true;
                anonymousUser.SetPassword("");
                anonymousUser.Roles.Add(anonymousRole);
                anonymousUser.Save();
            }

            #region Create an Anonimous User for the Simple Security Strategy
            //SecuritySimpleUser anonymousUser = ObjectSpace.FindObject<SecuritySimpleUser>(new BinaryOperator("UserName", SecurityStrategy.AnonymousUserName));
            //if(anonymousUser == null) {
            //    anonymousUser = ObjectSpace.CreateObject<SecuritySimpleUser>();
            //    anonymousUser.UserName = SecurityStrategy.AnonymousUserName;
            //    anonymousUser.IsActive = true;
            //    anonymousUser.SetPassword("");
            //    anonymousUser.Save();
            //}
            #endregion
        }

        private void MikrobarUpdate(bool before)
        {
            //if (CurrentDBVersion < Version.Parse(strCurrentDBVersion))
            {
                string data = "";
                string xClient = ".SqlClient.";
                if (Session.DefaultSession != null && Session.DefaultSession.Connection != null)
                {
                    switch (Session.DefaultSession.Connection.ToString())
                    {
                        case "System.Data.SqlClient.SqlConnection":
                            xClient = ".SqlClient.";
                            break;
                        case "System.Data.OracleClient.OracleConnection":
                            xClient = ".OracleClient.";
                            break;
                        case "MySql.Data.MySqlClient.MySqlConnection":
                            xClient = ".MySqlClient.";
                            break;
                        default:
                            break;
                    }
                }
                string[] resNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                if (resNames != null && resNames.Length > 0)
                {
                    foreach (string strName in resNames)
                    {
                        if (!string.IsNullOrEmpty(strName))
                        {
                            if (before)
                            {
                                /*if ((strName.ToLower().EndsWith("once.qry") || strName.ToLower().EndsWith("genelsorgular.qry")) && strName.IndexOf(xClient) != -1)
                                {
                                    using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(strName)))
                                    {
                                        data = reader.ReadToEnd();
                                        reader.Close();

                                        data = data.Replace("#ErpTur#", Mikrobar.MikrobarApp.ErpTur.ToString());
                                        System.Diagnostics.Trace.WriteLine("DbUpdater --------------------------------------->>>");
                                        System.Diagnostics.Trace.WriteLine(strName);
                                        System.Diagnostics.Trace.WriteLine(data);
                                        System.Diagnostics.Trace.WriteLine("DbUpdater --------------------------------------->>>");

                                        ExecuteNonQueryCommand(data, false);
                                    }
                                }*/
                            }
                            else
                            {
                                /*if (strName.ToLower().EndsWith(".qry") && strName.IndexOf(xClient) != -1 && !strName.ToLower().EndsWith("once.qry") && !strName.ToLower().EndsWith("GenelSorgular.qry"))
                                {
                                    using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(strName)))
                                    {
                                        data = reader.ReadToEnd();
                                        reader.Close();

                                        data = data.Replace("#ErpTur#", Mikrobar.MikrobarApp.ErpTur.ToString());
                                        System.Diagnostics.Trace.WriteLine("DbUpdater --------------------------------------->>>");
                                        System.Diagnostics.Trace.WriteLine(strName);
                                        System.Diagnostics.Trace.WriteLine(data);
                                        System.Diagnostics.Trace.WriteLine("DbUpdater --------------------------------------->>>");

                                        ExecuteNonQueryCommand(data, false);
                                    }
                                }*/
                            }
                        }
                    }
                }

            }
        }
    }
    public abstract class TaskAnalysis1LayoutUpdaterBase
    {
        protected abstract IAnalysisControl CreateAnalysisControl();
        protected abstract IPivotGridSettingsStore CreatePivotGridSettingsStore(IAnalysisControl control);
        public void Update(Analysis analysis)
        {
            if (analysis != null && !PivotGridSettingsHelper.HasPivotGridSettings(analysis))
            {
                IAnalysisControl control = CreateAnalysisControl();
                control.DataSource = new AnalysisDataSource(analysis, new XPCollection<DemoTask>(analysis.Session));
                control.Fields["Priority"].Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                control.Fields["Subject"].Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
                control.Fields["AssignedTo.DisplayName"].Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                PivotGridSettingsHelper.SavePivotGridSettings(CreatePivotGridSettingsStore(control), analysis);
                analysis.Save();
            }
        }
    }
    public abstract class TaskAnalysis2LayoutUpdaterBase
    {
        protected abstract IAnalysisControl CreateAnalysisControl();
        protected abstract IPivotGridSettingsStore CreatePivotGridSettingsStore(IAnalysisControl control);
        public void Update(Analysis analysis)
        {
            if (analysis != null && !PivotGridSettingsHelper.HasPivotGridSettings(analysis))
            {
                IAnalysisControl control = CreateAnalysisControl();
                control.DataSource = new AnalysisDataSource(analysis, new XPCollection<DemoTask>(analysis.Session));
                control.Fields["Status"].Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                control.Fields["Priority"].Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                control.Fields["EstimatedWork"].Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
                control.Fields["ActualWork"].Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
                control.Fields["AssignedTo.DisplayName"].Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                PivotGridSettingsHelper.SavePivotGridSettings(CreatePivotGridSettingsStore(control), analysis);
                analysis.Save();
            }
        }
    }
}
