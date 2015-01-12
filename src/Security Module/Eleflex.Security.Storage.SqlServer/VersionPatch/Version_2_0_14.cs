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
    /// The version patch for 2.0.14
    /// </summary>
    public class Version_2_0_14 : ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_2_0_14()
            : base(SecurityModel.SecurityConstants.MODULE_KEY, SecurityModel.SecurityConstants.MODULE_NAME)
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
                return new VersionModel.Version(2, 0, 14, 0);
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
                    new VersionModel.Version(2,0,13,0)
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
UPDATE Eleflex.[Role] SET [Name] = 'Admin', [ModuleKey] = '72E85BF1-8903-45C2-8DD7-CA25F76DC08F' WHERE [RoleKey] = '202BBB57-1411-4FC5-BE1A-832520AB78E3';
UPDATE Eleflex.[Role] SET [Name] = 'User', [ModuleKey] = '72E85BF1-8903-45C2-8DD7-CA25F76DC08F' WHERE [RoleKey] = '0C3623CB-5643-4FCD-8B8C-949D66C51AF2';
DELETE FROM Eleflex.[Role] WHERE [RoleKey] = '7D99ADF1-83E6-45E6-9EF0-48E958D82945';
UPDATE Eleflex.[RolePermission] SET [ModuleKey] = '72E85BF1-8903-45C2-8DD7-CA25F76DC08F' WHERE [RoleKey] = '202BBB57-1411-4FC5-BE1A-832520AB78E3' AND [PermissionKey] = '3E820CF7-4E24-4C56-8325-BF19ECB70CD7';
UPDATE Eleflex.[RolePermission] SET [ModuleKey] = '72E85BF1-8903-45C2-8DD7-CA25F76DC08F' WHERE [RoleKey] = '0C3623CB-5643-4FCD-8B8C-949D66C51AF2' AND [PermissionKey] = 'A9C7918B-653C-4BFA-BF36-3C2CE2DC9489';

";

    }
}
