﻿#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
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
using Eleflex.Lookups;
using Eleflex.Lookups.Storage.SqlServer;
using VersionModel = Eleflex.Versioning;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Lookups.Storage.SqlServer
{
    /// <summary>
    /// The base version patch for 2.0.0.0
    /// </summary>
    public class Version_2_0_0_0 : VersionModel.ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_2_0_0_0()
            : base(LookupsConstants.MODULE_KEY, LookupsConstants.MODULE_NAME)
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
                return new VersionModel.Version(2, 0, 0, 0);
            }
        }

        /// <summary>
        /// The versions prior to this version for which the patching process can be used.
        /// </summary>
        public override List<VersionModel.IModulePatch> PriorVersions
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
        public override bool Update()
        {            
            SqlCommand command = null;
            try
            {
                LookupsStorageProvider provider = ServiceLocator.Current.GetInstance<ILookupsStorageProvider>() as LookupsStorageProvider;
                string connectionString = provider.ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlStorageSession session = new SqlStorageSession(connection, transaction);
                provider.Sessions.Add(session);
                command = new SqlCommand(SCRIPT_CREATE, connection, transaction);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetCurrentClassLogger().Error(ex);
                return false;
            }
            finally
            {
                if (command != null)
                    command.Dispose();
            }
        }


        private const string SCRIPT_CREATE = @"
/****** Object:  Schema [Eleflex]    ******/
IF NOT EXISTS ( SELECT  1 FROM    sys.schemas WHERE   name = N'Eleflex' )
BEGIN
	EXEC('CREATE SCHEMA [Eleflex]')
END
/****** Object:  Table [Eleflex].[Lookup]    ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[Lookup](
	[LookupKey] [int] IDENTITY(1,1) NOT NULL,
	[Code] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[CategoryKey] [int] NULL,
	[Inactive] [bit] NOT NULL,
	[SortOrder] [int] NULL,
	[Abbreviation] [nvarchar](50) NULL,
	[Description] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[LookupKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [Eleflex].[Lookup]  WITH CHECK ADD  CONSTRAINT [FK_Lookup_Lookup] FOREIGN KEY([CategoryKey])
REFERENCES [Eleflex].[Lookup] ([LookupKey])

ALTER TABLE [Eleflex].[Lookup] CHECK CONSTRAINT [FK_Lookup_Lookup]

";

    }
}
