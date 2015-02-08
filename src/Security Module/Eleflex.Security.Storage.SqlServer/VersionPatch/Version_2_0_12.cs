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
    /// The version patch for 2.0.12
    /// </summary>
    public class Version_2_0_12 : ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_2_0_12()
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
                return new VersionModel.Version(2, 0, 12, 0);
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
                    new VersionModel.Version(2,0,11,0)
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
ALTER TABLE Eleflex.[User] ALTER COLUMN Email NVARCHAR(256);
ALTER TABLE Eleflex.[User] ADD EmailValid BIT NOT NULL;
ALTER TABLE Eleflex.[User] ADD EmailValidCode NVARCHAR(100) NULL;
ALTER TABLE Eleflex.[User] ADD Phone NVARCHAR(50) NULL;
ALTER TABLE Eleflex.[User] ADD PhoneValid BIT NOT NULL;
ALTER TABLE Eleflex.[User] ADD PhoneValidCode NVARCHAR(100) NULL;
ALTER TABLE Eleflex.[User] ADD TwoFactorAuth BIT NOT NULL;

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[UserClaim](
	[UserClaimKey] [bigint] IDENTITY(1,1) NOT NULL,
	[UserKey] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
    [ClaimValue] [nvarchar](max) NULL,
	[Inactive] [bit] NOT NULL,
	[StartDate] [datetimeoffset](7) NULL,
	[EndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[UserClaimKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [Eleflex].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_UserClaim_User] FOREIGN KEY([UserKey]) REFERENCES [Eleflex].[User] ([UserKey]);
ALTER TABLE [Eleflex].[UserClaim] CHECK CONSTRAINT [FK_UserClaim_User];

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[UserLogin](
	[UserLoginKey] [bigint] IDENTITY(1,1) NOT NULL,
	[UserKey] [uniqueidentifier] NOT NULL,
    [LoginProvider] [nvarchar](250) NULL,	
    [ProviderKey] [nvarchar](250) NULL,
	[Inactive] [bit] NOT NULL,
	[StartDate] [datetimeoffset](7) NULL,
	[EndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[UserLoginKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [Eleflex].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_User] FOREIGN KEY([UserKey]) REFERENCES [Eleflex].[User] ([UserKey]);
ALTER TABLE [Eleflex].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_User];
";

    }
}
