﻿#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
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
using Eleflex.Versioning;
using VersionModel = Eleflex.Versioning;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Versioning.Storage.Azure
{
    /// <summary>
    /// The base version patch for 2.0.26
    /// </summary>
    public class Version_2_0_26 : VersionModel.ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_2_0_26()
            : base(VersioningConstants.STORAGE_PROVIDER_MODULE_KEY, VersioningStorageConstants.MODULE_NAME)
        {
        }

        /// <summary>
        /// Dependent modules that must be executed prior to this version.
        /// </summary>
        public override List<Guid> DependentModules
        {
            get
            {
                return null;
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
                return null;
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
                VersionModel.IVersioningStorageProvider provider = ServiceLocator.Current.GetInstance<VersionModel.IVersioningStorageProvider>();
                IStorageSession session = provider.GetSession();
                SqlConnection connection = session.Session as SqlConnection;
                SqlTransaction transaction = session.Transaction as SqlTransaction;
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


        protected const string SCRIPT_CREATE = @"
/****** Object:  Schema [Eleflex]    ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Eleflex')
BEGIN
EXEC sys.sp_executesql N'CREATE SCHEMA [Eleflex]'
END
/****** Object:  Table [Eleflex].[ModuleVersion]   ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[ModuleVersion](
	[ModuleKey] [uniqueidentifier] NOT NULL,
	[ModuleName] [nvarchar](500) NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[Major] [int] NOT NULL,
	[Minor] [int] NOT NULL,
	[Build] [int] NOT NULL,
	[Revision] [int] NOT NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_ModuleVersion] PRIMARY KEY CLUSTERED 
(
	[ModuleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
";

    }
}
