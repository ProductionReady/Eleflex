#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Eleflex;
using Eleflex.Storage;
using Eleflex.Storage.EntityFramework;
using Eleflex.Security;
using Eleflex.Security.Storage.SqlServer.Model;
using Eleflex.Versioning;
using SecurityModel = Eleflex.Security;
using VersionModel = Eleflex.Versioning;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Security.Storage.SqlServer
{
    /// <summary>
    /// The base version patch for 2.0.11
    /// </summary>
    public class Version_2_0_11 : ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_2_0_11()
            : base(SecurityConstants.STORAGE_PROVIDER_MODULE_KEY, SecurityStorageConstants.MODULE_NAME)
        {
        }

        /// <summary>
        /// Dependent modules that must be executed prior to this version.
        /// </summary>
        public override List<Guid> DependentModules
        {
            get
            {
                return new List<Guid>() { VersionModel.VersioningConstants.MODULE_KEY };
            }
        }

        /// <summary>
        /// The current version of the patch.
        /// </summary>
        public override VersionModel.Version Version
        {
            get
            {
                return new VersionModel.Version(2, 0, 11, 0);
            }
        }

        /// <summary>
        /// The versions prior to this version for which the patching process can be used.
        /// </summary>
        public override List<VersionModel.Version> PriorVersions
        {
            get
            {
                return new List<VersionModel.Version>()
                {
                    new VersionModel.Version(2,0,10,0)
                };
            }
        }

        /// <summary>
        /// Custom logic to update the patch.
        /// </summary>
        /// <returns></returns>
        public override void Update()
        {
            SqlCommand command = null;
            try
            {
                SecurityStorageProvider provider = ServiceLocator.Current.GetInstance<ISecurityStorageProvider>() as SecurityStorageProvider;
                string connectionString = provider.ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlStorageSession session = new SqlStorageSession(connection, transaction);
                provider.Sessions.Add(session);
                command = new SqlCommand(SCRIPT_CREATE, connection, transaction);
                command.ExecuteNonQuery();
            }
            catch { throw; }
            finally
            {
                if (command != null)
                    command.Dispose();
            }
        }


        private const string SCRIPT_CREATE = @"
ALTER TABLE [Eleflex].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_Permission] FOREIGN KEY([PermissionKey]) REFERENCES [Eleflex].[Permission] ([PermissionKey]);
ALTER TABLE [Eleflex].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Permission];
ALTER TABLE [Eleflex].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY([RoleKey]) REFERENCES [Eleflex].[Role] ([RoleKey]);
ALTER TABLE [Eleflex].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Role];
ALTER TABLE [Eleflex].[RoleRole]  WITH CHECK ADD  CONSTRAINT [FK_RoleRole_Role_Child] FOREIGN KEY([ChildRoleKey]) REFERENCES [Eleflex].[Role] ([RoleKey]);
ALTER TABLE [Eleflex].[RoleRole] CHECK CONSTRAINT [FK_RoleRole_Role_Child];
ALTER TABLE [Eleflex].[RoleRole]  WITH CHECK ADD  CONSTRAINT [FK_RoleRole_Role_Parent] FOREIGN KEY([ParentRoleKey]) REFERENCES [Eleflex].[Role] ([RoleKey]);
ALTER TABLE [Eleflex].[RoleRole] CHECK CONSTRAINT [FK_RoleRole_Role_Parent];
ALTER TABLE [Eleflex].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_Permission] FOREIGN KEY([PermissionKey]) REFERENCES [Eleflex].[Permission] ([PermissionKey]);
ALTER TABLE [Eleflex].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_Permission];
ALTER TABLE [Eleflex].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_User] FOREIGN KEY([UserKey]) REFERENCES [Eleflex].[User] ([UserKey]);
ALTER TABLE [Eleflex].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_User];
ALTER TABLE [Eleflex].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleKey]) REFERENCES [Eleflex].[Role] ([RoleKey]);
ALTER TABLE [Eleflex].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role];
ALTER TABLE [Eleflex].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserKey]) REFERENCES [Eleflex].[User] ([UserKey]);
ALTER TABLE [Eleflex].[UserRole] CHECK CONSTRAINT [FK_UserRole_User];

INSERT INTO Eleflex.[Permission] (PermissionKey, ModuleKey, Inactive, Name, Description, ExtraData)
VALUES 
('3E820CF7-4E24-4C56-8325-BF19ECB70CD7','72E85BF1-8903-45C2-8DD7-CA25F76DC08F',0,'Admin','This permission grants a user complete control of the system.',null),
('A9C7918B-653C-4BFA-BF36-3C2CE2DC9489','72E85BF1-8903-45C2-8DD7-CA25F76DC08F',0,'User','This permission is assigned to all users of the system.',null);

INSERT INTO Eleflex.[Role] ([RoleKey],[Inactive],[Name],[Description],[ExtraData])
VALUES
('202BBB57-1411-4FC5-BE1A-832520AB78E3',0,'Administrators', 'This role is assigned to administrators of the system.', null),
('0C3623CB-5643-4FCD-8B8C-949D66C51AF2',0,'Users', 'This role is assigned to all users of the system.', null),
('7D99ADF1-83E6-45E6-9EF0-48E958D82945',0,'Anonymous', 'This role is assigned to all anonymous users of the system.', null);

INSERT INTO Eleflex.RolePermission([RoleKey],[PermissionKey],[Inactive],[StartDate],[EndDate],[Comment],[ExtraData])
VALUES
('202BBB57-1411-4FC5-BE1A-832520AB78E3','3E820CF7-4E24-4C56-8325-BF19ECB70CD7',0,null,null,'Default system assignment',null),
('0C3623CB-5643-4FCD-8B8C-949D66C51AF2','A9C7918B-653C-4BFA-BF36-3C2CE2DC9489',0,null,null,'Default system assignment',null);


";

    }
}
