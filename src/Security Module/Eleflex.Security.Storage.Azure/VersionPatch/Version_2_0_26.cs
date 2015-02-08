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
using Eleflex.Security.Storage.Azure.Model;
using Eleflex.Versioning;
using SecurityModel = Eleflex.Security;
using VersionModel = Eleflex.Versioning;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Security.Storage.Azure
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
                    new VersionModel.Version(2,0,25,0)
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
INSERT INTO Eleflex.[Permission] (PermissionKey, ModuleKey, Inactive, Name, Description, ExtraData)
VALUES ('3E820CF7-4E24-4C56-8325-BF19ECB70CD7','8C727F8D-D891-4481-B879-D0926A18A53A',0,'Admin','This permission grants a user complete control of the system.',null)

INSERT INTO Eleflex.[Permission] (PermissionKey, ModuleKey, Inactive, Name, Description, ExtraData)
VALUES ('A9C7918B-653C-4BFA-BF36-3C2CE2DC9489','8C727F8D-D891-4481-B879-D0926A18A53A',0,'User','This permission is assigned to all users of the system.',null)

INSERT INTO Eleflex.[Role] ([RoleKey],[Inactive],[Name],[Description],[ExtraData],[ModuleKey],[StartDate],[EndDate])
VALUES ('202BBB57-1411-4FC5-BE1A-832520AB78E3',0,'Admin', 'This role is assigned to administrators of the system.', null,'8C727F8D-D891-4481-B879-D0926A18A53A',null,null)

INSERT INTO Eleflex.[Role] ([RoleKey],[Inactive],[Name],[Description],[ExtraData],[ModuleKey],[StartDate],[EndDate])
VALUES ('0C3623CB-5643-4FCD-8B8C-949D66C51AF2',0,'User', 'This role is assigned to all users of the system.', null,'8C727F8D-D891-4481-B879-D0926A18A53A',null,null)

INSERT INTO [Eleflex].[RolePermission]([RoleKey],[PermissionKey],[Inactive],[StartDate],[EndDate],[Comment],[ExtraData],[ModuleKey])
VALUES ('202BBB57-1411-4FC5-BE1A-832520AB78E3','3E820CF7-4E24-4C56-8325-BF19ECB70CD7',0,null,null,'Default system assignment',null,'8C727F8D-D891-4481-B879-D0926A18A53A')

INSERT INTO [Eleflex].[RolePermission]([RoleKey],[PermissionKey],[Inactive],[StartDate],[EndDate],[Comment],[ExtraData],[ModuleKey])
VALUES ('0C3623CB-5643-4FCD-8B8C-949D66C51AF2','A9C7918B-653C-4BFA-BF36-3C2CE2DC9489',0,null,null,'Default system assignment',null,'8C727F8D-D891-4481-B879-D0926A18A53A')
";

    }
}
