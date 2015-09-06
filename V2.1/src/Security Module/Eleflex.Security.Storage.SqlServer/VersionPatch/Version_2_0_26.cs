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
    /// The version patch for 2.0.26
    /// </summary>
    public class Version_2_0_26 : ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_2_0_26()
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
                return new VersionModel.Version(2, 0, 26, 0);
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
                    new VersionModel.Version(2,0,21,0)
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
UPDATE Eleflex.RolePermission SET ModuleKey = '8C727F8D-D891-4481-B879-D0926A18A53A' WHERE ModuleKey = '72E85BF1-8903-45C2-8DD7-CA25F76DC08F';
UPDATE Eleflex.Role SET ModuleKey = '8C727F8D-D891-4481-B879-D0926A18A53A' WHERE ModuleKey = '72E85BF1-8903-45C2-8DD7-CA25F76DC08F';
UPDATE Eleflex.Permission SET ModuleKey = '8C727F8D-D891-4481-B879-D0926A18A53A' WHERE ModuleKey = '72E85BF1-8903-45C2-8DD7-CA25F76DC08F';

CREATE INDEX IX_EleflexRolePermission_RoleKey ON [Eleflex].[RolePermission] (RoleKey);
CREATE INDEX IX_EleflexRoleRole_ParentRoleKey ON [Eleflex].[RoleRole] (ParentRoleKey);
CREATE INDEX IX_EleflexUser_Username ON [Eleflex].[User] (Username);
CREATE INDEX IX_EleflexUser_Email ON [Eleflex].[User] (Email);
CREATE INDEX IX_EleflexUserClaim_UserKey ON [Eleflex].[UserClaim] (UserKey);
CREATE INDEX IX_EleflexUserLogin_UserKey ON [Eleflex].[UserLogin] (UserKey);
CREATE INDEX IX_EleflexUserPermission_UserKey ON [Eleflex].[UserPermission] (UserKey);
CREATE INDEX IX_EleflexUserRole_UserKey ON [Eleflex].[UserRole] (UserKey);
";

    }
}
