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
    /// The version patch for 2.0.25
    /// </summary>
    public class Version_2_0_25 : ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_2_0_25()
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
                return new VersionModel.Version(2, 0, 25, 0);
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
/****** Object:  Schema [Eleflex]    ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Eleflex')
BEGIN
EXEC sys.sp_executesql N'CREATE SCHEMA [Eleflex]'
END
/****** Object:  Table [Eleflex].[Permission]    ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[Permission](
	[PermissionKey] [uniqueidentifier] NOT NULL,
	[ModuleKey] [uniqueidentifier] NULL,
	[Inactive] [bit] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

/****** Object:  Table [Eleflex].[Role]    ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[Role](
	[RoleKey] [uniqueidentifier] NOT NULL,
	[Inactive] [bit] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
	[ModuleKey] [uniqueidentifier] NULL,
	[StartDate] [datetimeoffset](7) NULL,
	[EndDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
/****** Object:  Table [Eleflex].[RolePermission]    ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[RolePermission](
	[RolePermissionKey] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleKey] [uniqueidentifier] NOT NULL,
	[PermissionKey] [uniqueidentifier] NOT NULL,
	[Inactive] [bit] NOT NULL,
	[StartDate] [datetimeoffset](7) NULL,
	[EndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
	[ModuleKey] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED 
(
	[RolePermissionKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
/****** Object:  Table [Eleflex].[RoleRole]    ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[RoleRole](
	[RoleRoleKey] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentRoleKey] [uniqueidentifier] NOT NULL,
	[ChildRoleKey] [uniqueidentifier] NOT NULL,
	[Inactive] [bit] NOT NULL,
	[StartDate] [datetimeoffset](7) NULL,
	[EndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
	[ModuleKey] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RoleRole] PRIMARY KEY CLUSTERED 
(
	[RoleRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
/****** Object:  Table [Eleflex].[User]    ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[User](
	[UserKey] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetimeoffset](7) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[Password] [nvarchar](1000) NOT NULL,
	[PasswordSalt] [nvarchar](100) NOT NULL,
	[PasswordLastChangeDate] [datetimeoffset](7) NOT NULL,
	[LoginFailedAttempts] [int] NOT NULL,
	[EnableLockout] [bit] NOT NULL,
	[LastLoginDate] [datetimeoffset](7) NULL,
	[LockoutReinstateDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
	[Inactive] [bit] NOT NULL,
	[EmailValid] [bit] NOT NULL,
	[EmailValidCode] [nvarchar](100) NULL,
	[Phone] [nvarchar](50) NULL,
	[PhoneValid] [bit] NOT NULL,
	[PhoneValidCode] [nvarchar](100) NULL,
	[TwoFactorAuth] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
/****** Object:  Table [Eleflex].[UserClaim]    ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
/****** Object:  Table [Eleflex].[UserLogin]    ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
/****** Object:  Table [Eleflex].[UserPermission]    ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[UserPermission](
	[UserPermissionKey] [bigint] IDENTITY(1,1) NOT NULL,
	[UserKey] [uniqueidentifier] NOT NULL,
	[PermissionKey] [uniqueidentifier] NOT NULL,
	[Inactive] [bit] NOT NULL,
	[StartDate] [datetimeoffset](7) NULL,
	[EndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserPermission] PRIMARY KEY CLUSTERED 
(
	[UserPermissionKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
/****** Object:  Table [Eleflex].[UserRole]    ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [Eleflex].[UserRole](
	[UserRoleKey] [bigint] IDENTITY(1,1) NOT NULL,
	[UserKey] [uniqueidentifier] NOT NULL,
	[RoleKey] [uniqueidentifier] NOT NULL,
	[Inactive] [bit] NOT NULL,
	[StartDate] [datetimeoffset](7) NULL,
	[EndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](2000) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
/****** Object:  Index [IX_EleflexRolePermission_RoleKey]    ******/
CREATE NONCLUSTERED INDEX [IX_EleflexRolePermission_RoleKey] ON [Eleflex].[RolePermission]
(
	[RoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
/****** Object:  Index [IX_EleflexRoleRole_ParentRoleKey]    ******/
CREATE NONCLUSTERED INDEX [IX_EleflexRoleRole_ParentRoleKey] ON [Eleflex].[RoleRole]
(
	[ParentRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
SET ANSI_PADDING ON
/****** Object:  Index [IX_EleflexUser_Email]    ******/
CREATE NONCLUSTERED INDEX [IX_EleflexUser_Email] ON [Eleflex].[User]
(
	[Email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
SET ANSI_PADDING ON
/****** Object:  Index [IX_EleflexUser_Username]    ******/
CREATE NONCLUSTERED INDEX [IX_EleflexUser_Username] ON [Eleflex].[User]
(
	[Username] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
/****** Object:  Index [IX_EleflexUserClaim_UserKey]    ******/
CREATE NONCLUSTERED INDEX [IX_EleflexUserClaim_UserKey] ON [Eleflex].[UserClaim]
(
	[UserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
/****** Object:  Index [IX_EleflexUserLogin_UserKey]    ******/
CREATE NONCLUSTERED INDEX [IX_EleflexUserLogin_UserKey] ON [Eleflex].[UserLogin]
(
	[UserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
/****** Object:  Index [IX_EleflexUserPermission_UserKey]    ******/
CREATE NONCLUSTERED INDEX [IX_EleflexUserPermission_UserKey] ON [Eleflex].[UserPermission]
(
	[UserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
/****** Object:  Index [IX_EleflexUserRole_UserKey]    ******/
CREATE NONCLUSTERED INDEX [IX_EleflexUserRole_UserKey] ON [Eleflex].[UserRole]
(
	[UserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)

ALTER TABLE [Eleflex].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_Permission] FOREIGN KEY([PermissionKey])
REFERENCES [Eleflex].[Permission] ([PermissionKey])
ALTER TABLE [Eleflex].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Permission]
ALTER TABLE [Eleflex].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY([RoleKey])
REFERENCES [Eleflex].[Role] ([RoleKey])
ALTER TABLE [Eleflex].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Role]
ALTER TABLE [Eleflex].[RoleRole]  WITH CHECK ADD  CONSTRAINT [FK_RoleRole_Role_Child] FOREIGN KEY([ChildRoleKey])
REFERENCES [Eleflex].[Role] ([RoleKey])
ALTER TABLE [Eleflex].[RoleRole] CHECK CONSTRAINT [FK_RoleRole_Role_Child]
ALTER TABLE [Eleflex].[RoleRole]  WITH CHECK ADD  CONSTRAINT [FK_RoleRole_Role_Parent] FOREIGN KEY([ParentRoleKey])
REFERENCES [Eleflex].[Role] ([RoleKey])
ALTER TABLE [Eleflex].[RoleRole] CHECK CONSTRAINT [FK_RoleRole_Role_Parent]
ALTER TABLE [Eleflex].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_UserClaim_User] FOREIGN KEY([UserKey])
REFERENCES [Eleflex].[User] ([UserKey])
ALTER TABLE [Eleflex].[UserClaim] CHECK CONSTRAINT [FK_UserClaim_User]
ALTER TABLE [Eleflex].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_User] FOREIGN KEY([UserKey])
REFERENCES [Eleflex].[User] ([UserKey])
ALTER TABLE [Eleflex].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_User]
ALTER TABLE [Eleflex].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_Permission] FOREIGN KEY([PermissionKey])
REFERENCES [Eleflex].[Permission] ([PermissionKey])
ALTER TABLE [Eleflex].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_Permission]
ALTER TABLE [Eleflex].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_User] FOREIGN KEY([UserKey])
REFERENCES [Eleflex].[User] ([UserKey])
ALTER TABLE [Eleflex].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_User]
ALTER TABLE [Eleflex].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleKey])
REFERENCES [Eleflex].[Role] ([RoleKey])
ALTER TABLE [Eleflex].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
ALTER TABLE [Eleflex].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserKey])
REFERENCES [Eleflex].[User] ([UserKey])
ALTER TABLE [Eleflex].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]

";

    }
}
