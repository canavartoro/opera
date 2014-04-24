using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.PivotChart;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using MainDemo.Module.BusinessObjects;
using System;
using System.Data.SqlClient;

namespace MainDemo.Module.DatabaseUpdate {
	public class Updater : DevExpress.ExpressApp.Updating.ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
		public override void UpdateDatabaseAfterUpdateSchema() {
			base.UpdateDatabaseAfterUpdateSchema();
			UpdateAnalysisCriteriaColumn();

            CreateAnonymousAccess();
            SecurityRole defaultRole = CreateDefaultRole();
            defaultRole.Save();

			#region Create reports
			CreateReport("ContactsGroupedByPosition");
			CreateReport("TasksStateReport");
			#endregion

			Position developerPosition = ObjectSpace.FindObject<Position>(CriteriaOperator.Parse("Title == 'Developer'"));
			if(developerPosition == null) {
				developerPosition = ObjectSpace.CreateObject<Position>();
				developerPosition.Title = "Developer";
				developerPosition.Save();
			}
			Position managerPosition = ObjectSpace.FindObject<Position>(CriteriaOperator.Parse("Title == 'Manager'"));
			if(managerPosition == null) {
				managerPosition = ObjectSpace.CreateObject<Position>();
				managerPosition.Title = "Manager";
				managerPosition.Save();
			}

			Department devDepartment = ObjectSpace.FindObject<Department>(CriteriaOperator.Parse("Title == 'Development Department'"));
			if(devDepartment == null) {
				devDepartment = ObjectSpace.CreateObject<Department>();
				devDepartment.Title = "Development Department";
				devDepartment.Office = "205";
				devDepartment.Positions.Add(developerPosition);
				devDepartment.Positions.Add(managerPosition);
				devDepartment.Save();
			}

			Contact contactMary = ObjectSpace.FindObject<Contact>(CriteriaOperator.Parse("FirstName == 'Mary' && LastName == 'Tellitson'"));
			if(contactMary == null) {
				contactMary = ObjectSpace.CreateObject<Contact>();
				contactMary.FirstName = "Mary";
				contactMary.LastName = "Tellitson";
				contactMary.Email = "mary_tellitson@md.com";
				contactMary.Birthday = new DateTime(1980, 11, 27);
				contactMary.Department = devDepartment;
				contactMary.Position = managerPosition;
				contactMary.Save();
			}

			Contact contactJohn = ObjectSpace.FindObject<Contact>(CriteriaOperator.Parse("FirstName == 'John' && LastName == 'Nilsen'"));
			if(contactJohn == null) {
				contactJohn = ObjectSpace.CreateObject<Contact>();
				contactJohn.FirstName = "John";
				contactJohn.LastName = "Nilsen";
				contactJohn.Email = "john_nilsen@md.com";
				contactJohn.Birthday = new DateTime(1981, 10, 3);
				contactJohn.Department = devDepartment;
				contactJohn.Position = developerPosition;
				contactJohn.Save();
			}

			if(ObjectSpace.FindObject<DemoTask>(CriteriaOperator.Parse("Subject == 'Review reports'")) == null) {
				DemoTask task = ObjectSpace.CreateObject<DemoTask>();
				task.Subject = "Review reports";
				task.AssignedTo = contactJohn;
				task.StartDate = DateTime.Parse("May 03, 2008");
				task.DueDate = DateTime.Parse("September 06, 2008");
				task.Status = DevExpress.Persistent.Base.General.TaskStatus.InProgress;
				task.Priority = Priority.High;
				task.EstimatedWork = 60;
				task.Description = "Analyse the reports and assign new tasks to employees.";
				task.Save();
			}

			if(ObjectSpace.FindObject<DemoTask>(CriteriaOperator.Parse("Subject == 'Fix breakfast'")) == null) {
				DemoTask task = ObjectSpace.CreateObject<DemoTask>();
				task.Subject = "Fix breakfast";
				task.AssignedTo = contactMary;
				task.StartDate = DateTime.Parse("May 03, 2008");
				task.DueDate = DateTime.Parse("May 04, 2008");
				task.Status = DevExpress.Persistent.Base.General.TaskStatus.Completed;
				task.Priority = Priority.Low;
				task.EstimatedWork = 1;
				task.ActualWork = 3;
				task.Description = "The Development Department - by 9 a.m.\r\nThe R&QA Department - by 10 a.m.";
				task.Save();
			}
			if(ObjectSpace.FindObject<DemoTask>(CriteriaOperator.Parse("Subject == 'Task1'")) == null) {
				DemoTask task = ObjectSpace.CreateObject<DemoTask>();
				task.Subject = "Task1";
				task.AssignedTo = contactJohn;
				task.StartDate = DateTime.Parse("June 03, 2008");
				task.DueDate = DateTime.Parse("June 06, 2008");
				task.Status = DevExpress.Persistent.Base.General.TaskStatus.Completed;
				task.Priority = Priority.High;
				task.EstimatedWork = 10;
				task.ActualWork = 15;
				task.Description = "A task designed specially to demonstrate the PivotChart module. Switch to the Reports navigation group to view the generated analysis.";
				task.Save();
			}
			if(ObjectSpace.FindObject<DemoTask>(CriteriaOperator.Parse("Subject == 'Task2'")) == null) {
				DemoTask task = ObjectSpace.CreateObject<DemoTask>();
				task.Subject = "Task2";
				task.AssignedTo = contactJohn;
				task.StartDate = DateTime.Parse("July 03, 2008");
				task.DueDate = DateTime.Parse("July 06, 2008");
				task.Status = DevExpress.Persistent.Base.General.TaskStatus.Completed;
				task.Priority = Priority.Low;
				task.EstimatedWork = 8;
				task.ActualWork = 16;
				task.Description = "A task designed specially to demonstrate the PivotChart module. Switch to the Reports navigation group to view the generated analysis.";
				task.Save();
			}
			CreateDataToBeAnalysed();

			#region Create a User for the Simple Security Strategy
			//// If a simple user named 'Sam' doesn't exist in the database, create this simple user
            //SecuritySimpleUser adminUser = ObjectSpace.FindObject<SecuritySimpleUser>(new BinaryOperator("UserName", "Sam"));
			//if(adminUser == null) {
            //    adminUser = ObjectSpace.CreateObject<SecuritySimpleUser>();
			//    adminUser.UserName = "Sam";
			//}
			//// Make the user an administrator
			//adminUser.IsAdministrator = true;
			//// Set a password if the standard authentication type is used
			//adminUser.SetPassword("");
			//// Save the user to the database
			//adminUser.Save();
			#endregion

			#region Create Users for the Complex Security Strategy
			// If a user named 'Sam' doesn't exist in the database, create this user
			SecurityUser user1 = ObjectSpace.FindObject<SecurityUser>(new BinaryOperator("UserName", "Sam"));
			if(user1 == null) {
				user1 = ObjectSpace.CreateObject<SecurityUser>();
				user1.UserName = "Sam";
				// Set a password if the standard authentication type is used
				user1.SetPassword("");
			}
			// If a user named 'John' doesn't exist in the database, create this user
			SecurityUser user2 = ObjectSpace.FindObject<SecurityUser>(new BinaryOperator("UserName", "John"));
			if(user2 == null) {
				user2 = ObjectSpace.CreateObject<SecurityUser>();
				user2.UserName = "John";
				// Set a password if the standard authentication type is used
				user2.SetPassword("");
			}
			// If a role with the Administrators name doesn't exist in the database, create this role
			SecurityRole adminRole = ObjectSpace.FindObject<SecurityRole>(new BinaryOperator("Name", "Administrators"));
			if(adminRole == null) {
				adminRole = ObjectSpace.CreateObject<SecurityRole>();
				adminRole.Name = "Administrators";
			}
			// If a role with the Users name doesn't exist in the database, create this role
			SecurityRole userRole = ObjectSpace.FindObject<SecurityRole>(new BinaryOperator("Name", "Users"));
			if(userRole == null) {
				userRole = ObjectSpace.CreateObject<SecurityRole>();
				userRole.Name = "Users";
			}
            adminRole.BeginUpdate();
            adminRole.CanEditModel = true;
            adminRole.Permissions.GrantRecursive(typeof(object), SecurityOperations.FullAccess);
            adminRole.EndUpdate();
            // Save the Administrators role to the database
			adminRole.Save();

            userRole.BeginUpdate();
            userRole.Permissions.GrantRecursive(typeof(object), SecurityOperations.FullAccess);
            userRole.Permissions.DenyRecursive(typeof(SecurityUser), SecurityOperations.FullAccess);
            userRole.Permissions.DenyRecursive(typeof(SecurityRole), SecurityOperations.FullAccess);
            userRole.Permissions.DenyRecursive(typeof(PermissionDescriptorBase), SecurityOperations.FullAccess);
            userRole.Permissions.DenyRecursive(typeof(IPermissionData), SecurityOperations.FullAccess);
            userRole.Permissions.DenyRecursive(typeof(TypePermissionDetails), SecurityOperations.FullAccess);
            userRole.EndUpdate();
			// Save the Users role to the database
			userRole.Save();
			// Add the Administrators role to the user1
			user1.Roles.Add(adminRole);
			// Add the Users role to the user2
			user2.Roles.Add(userRole);
            user2.Roles.Add(defaultRole);
			// Save the users to the database
			user1.Save();
			user2.Save();
			#endregion

            ObjectSpace.CommitChanges();
		}
		private void CreateReport(string reportName) {
			ReportData reportdata = ObjectSpace.FindObject<ReportData>(new BinaryOperator("Name", reportName));
			if(reportdata == null) {
				reportdata = ObjectSpace.CreateObject<ReportData>();
				XafReport rep = new XafReport();
                rep.ObjectSpace = ObjectSpace;
				rep.LoadLayout(GetType().Assembly.GetManifestResourceStream(
				   "MainDemo.Module.EmbeddedReports." + reportName + ".repx"));
                rep.ReportName = reportName;
				reportdata.SaveXtraReport(rep);
				reportdata.IsInplaceReport = true;
				reportdata.Save();
			}
		}
		private void CreateDataToBeAnalysed() {
			Analysis taskAnalysis1 = ObjectSpace.FindObject<Analysis>(CriteriaOperator.Parse("Name='Completed tasks'"));
			if(taskAnalysis1 == null) {
				taskAnalysis1 = ObjectSpace.CreateObject<Analysis>();
				taskAnalysis1.Name = "Completed tasks";
				taskAnalysis1.ObjectTypeName = typeof(DemoTask).FullName;
				taskAnalysis1.Criteria = "[Status] = 'Completed'";
				taskAnalysis1.Save();
			}
			Analysis taskAnalysis2 = ObjectSpace.FindObject<Analysis>(CriteriaOperator.Parse("Name='Estimated and actual work comparison'"));
			if(taskAnalysis2 == null) {
				taskAnalysis2 = ObjectSpace.CreateObject<Analysis>();
				taskAnalysis2.Name = "Estimated and actual work comparison";
				taskAnalysis2.ObjectTypeName = typeof(DemoTask).FullName;
				taskAnalysis2.Save();
			}
		}
		private void UpdateAnalysisCriteriaColumn() {
            if(((ObjectSpace)ObjectSpace).Session.Connection is SqlConnection) {
				int length = (int)ExecuteScalarCommand(@"select CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.Columns WHERE TABLE_NAME = 'Analysis' AND COLUMN_NAME = 'Criteria'", true);
				if(length != -1) {
					ExecuteNonQueryCommand("alter table Analysis alter column Criteria nvarchar(max)", true);
				}
			}
		}
        private SecurityRole CreateDefaultRole() {
            SecurityRole defaultRole = ObjectSpace.FindObject<SecurityRole>(new BinaryOperator("Name", "Default"));
            if(defaultRole == null) {
                defaultRole = ObjectSpace.CreateObject<SecurityRole>();
                defaultRole.Name = "Default";
                ObjectOperationPermissionData myDetailsPermission = ObjectSpace.CreateObject<ObjectOperationPermissionData>();
                myDetailsPermission.TargetType = typeof(SecurityUser);
                myDetailsPermission.Criteria = "[Oid] = CurrentUserId()";
                myDetailsPermission.AllowNavigate = true;
                myDetailsPermission.AllowRead = true;
                myDetailsPermission.Save();
                defaultRole.PersistentPermissions.Add(myDetailsPermission);
                MemberOperationPermissionData userMembersPermission = ObjectSpace.CreateObject<MemberOperationPermissionData>();
                userMembersPermission.TargetType = typeof(SecurityUser);
                userMembersPermission.Members = "ChangePasswordOnFirstLogon, StoredPassword";
                userMembersPermission.AllowWrite = true;
                userMembersPermission.Save();
                defaultRole.PersistentPermissions.Add(userMembersPermission);
                ObjectOperationPermissionData defaultRolePermission = ObjectSpace.CreateObject<ObjectOperationPermissionData>();
                defaultRolePermission.TargetType = typeof(SecurityRole);
                defaultRolePermission.Criteria = "[Name] = 'Default'";
                defaultRolePermission.AllowNavigate = true;
                defaultRolePermission.AllowRead = true;
                defaultRolePermission.Save();
                defaultRole.PersistentPermissions.Add(defaultRolePermission);
                TypeOperationPermissionData auditDataItemPermission = ObjectSpace.CreateObject<TypeOperationPermissionData>();
                auditDataItemPermission.TargetType = typeof(AuditDataItemPersistent);
                auditDataItemPermission.AllowRead = true;
                auditDataItemPermission.AllowWrite = true;
                auditDataItemPermission.AllowCreate = true;
                auditDataItemPermission.Save();
                defaultRole.PersistentPermissions.Add(auditDataItemPermission);
            }
            return defaultRole;
        }
        private void CreateAnonymousAccess() {
            SecurityRole anonymousRole = ObjectSpace.FindObject<SecurityRole>(new BinaryOperator("Name", SecurityStrategy.AnonymousUserName));
            if(anonymousRole == null) {
                anonymousRole = ObjectSpace.CreateObject<SecurityRole>();
                anonymousRole.Name = SecurityStrategy.AnonymousUserName;
                anonymousRole.BeginUpdate();
                anonymousRole.Permissions[typeof(SecurityUser)].Grant(SecurityOperations.Read);
                anonymousRole.EndUpdate();
                anonymousRole.Save();
            }

            SecurityUser anonymousUser = ObjectSpace.FindObject<SecurityUser>(new BinaryOperator("UserName", SecurityStrategy.AnonymousUserName));
            if(anonymousUser == null) {
                anonymousUser = ObjectSpace.CreateObject<SecurityUser>();
                anonymousUser.UserName = SecurityStrategy.AnonymousUserName;
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
    }
	public abstract class TaskAnalysis1LayoutUpdaterBase {
		protected abstract IAnalysisControl CreateAnalysisControl();
		protected abstract IPivotGridSettingsStore CreatePivotGridSettingsStore(IAnalysisControl control);
		public void Update(Analysis analysis) {
			if(analysis != null && !PivotGridSettingsHelper.HasPivotGridSettings(analysis)) {
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
	public abstract class TaskAnalysis2LayoutUpdaterBase {
		protected abstract IAnalysisControl CreateAnalysisControl();
		protected abstract IPivotGridSettingsStore CreatePivotGridSettingsStore(IAnalysisControl control);
		public void Update(Analysis analysis) {
			if(analysis != null && !PivotGridSettingsHelper.HasPivotGridSettings(analysis)) {
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
