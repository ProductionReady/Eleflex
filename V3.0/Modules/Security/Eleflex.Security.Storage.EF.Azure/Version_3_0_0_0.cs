using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VersioningModel = Eleflex;

namespace Eleflex.Security.Storage.EF.Azure
{
    /// <summary>
    /// The base version patch for 3.0.0.0
    /// </summary>
    public class Version_3_0_0_0 : VersioningModel.ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_3_0_0_0()
            : base(SecurityAzureConstants.MODULE_KEY, SecurityAzureConstants.MODULE_NAME, SecurityAzureConstants.MODULE_DESCRIPTION, PATCH_INFO)
        {
        }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        public override bool IsAvailable
        {
            get { return ObjectLocator.Current.GetInstance<ISecurityStorageService>().VersioningStorageConfig == SecurityAzureConstants.VERSIONING_STORAGE_CONFIG; }
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
        public override VersioningModel.Version Version
        {
            get
            {
                return new VersioningModel.Version(3, 0, 0, 0);
            }
        }

        /// <summary>
        /// The versions prior to this version for which the patching process can be used.
        /// </summary>
        public override List<VersioningModel.Version> PriorVersions
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
            ISecurityStorageService service = ObjectLocator.Current.GetInstance<ISecurityStorageService>();
            IStorageServiceSession session = service.CreateSession();
            System.Data.Entity.DbContext context = session.Session as System.Data.Entity.DbContext;
            if (context == null)
                throw new Exception("Session is not DBContext");
            context.Database.ExecuteSqlCommand(SCRIPT);
        }

        /// <summary>
        /// Information regarding the patch.
        /// </summary>
        protected const string PATCH_INFO = "This patch creates the Security tables.";

        /// <summary>
        /// The script to execute.
        /// </summary>
        protected const string SCRIPT = @"
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'EleflexV3')
BEGIN
EXEC sys.sp_executesql N'CREATE SCHEMA [EleflexV3]'
END
/****** Object:  Table [EleflexV3].[SecurityPermission]    Script Date: 8/29/2015 11:28:08 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[SecurityPermission](
	[SecurityPermissionKey] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[IsSystem] [bit] NOT NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecurityPermission] PRIMARY KEY CLUSTERED 
(
	[SecurityPermissionKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Table [EleflexV3].[SecurityRole]    Script Date: 8/29/2015 11:28:08 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[SecurityRole](
	[SecurityRoleKey] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[IsSystem] [bit] NOT NULL,
	[EffectiveStartDate] [datetimeoffset](7) NULL,
	[EffectiveEndDate] [datetimeoffset](7) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecurityRole] PRIMARY KEY CLUSTERED 
(
	[SecurityRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Table [EleflexV3].[SecurityRolePermission]    Script Date: 8/29/2015 11:28:08 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[SecurityRolePermission](
	[SecurityRolePermissionKey] [bigint] IDENTITY(1,1) NOT NULL,
	[SecurityRoleKey] [uniqueidentifier] NOT NULL,
	[SecurityPermissionKey] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
	[EffectiveStartDate] [datetimeoffset](7) NULL,
	[EffectiveEndDate] [datetimeoffset](7) NULL,
	[IsSystem] [bit] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecurityRolePermission] PRIMARY KEY CLUSTERED 
(
	[SecurityRolePermissionKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Table [EleflexV3].[SecurityRoleRole]    Script Date: 8/29/2015 11:28:08 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[SecurityRoleRole](
	[SecurityRoleRoleKey] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentSecurityRoleKey] [uniqueidentifier] NOT NULL,
	[ChildSecurityRoleKey] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
	[EffectiveStartDate] [datetimeoffset](7) NULL,
	[EffectiveEndDate] [datetimeoffset](7) NULL,
	[IsSystem] [bit] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecurityRoleRole] PRIMARY KEY CLUSTERED 
(
	[SecurityRoleRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Table [EleflexV3].[SecurityUser]    Script Date: 8/29/2015 11:28:08 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[SecurityUser](
	[SecurityUserKey] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Username] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[Password] [nvarchar](1000) NOT NULL,
	[PasswordLastChangeDate] [datetimeoffset](7) NOT NULL,
	[SecurityStamp] [nvarchar](100) NOT NULL,
	[LoginFailedAttempts] [int] NOT NULL,
	[EnableLockout] [bit] NOT NULL,
	[LastLoginDate] [datetimeoffset](7) NULL,
	[LockoutReinstateDate] [datetimeoffset](7) NULL,
	[EmailValid] [bit] NOT NULL,
	[EmailValidCode] [nvarchar](100) NULL,
	[Phone] [nvarchar](50) NULL,
	[PhoneValid] [bit] NOT NULL,
	[PhoneValidCode] [nvarchar](100) NULL,
	[TwoFactorAuth] [bit] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecurityUser] PRIMARY KEY CLUSTERED 
(
	[SecurityUserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Table [EleflexV3].[SecurityUserClaim]    Script Date: 8/29/2015 11:28:08 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[SecurityUserClaim](
	[SecurityUserClaimKey] [bigint] IDENTITY(1,1) NOT NULL,
	[SecurityUserKey] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[EffectiveStartDate] [datetimeoffset](7) NULL,
	[EffectiveEndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](max) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecurityUserClaim] PRIMARY KEY CLUSTERED 
(
	[SecurityUserClaimKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Table [EleflexV3].[SecurityUserLogin]    Script Date: 8/29/2015 11:28:08 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[SecurityUserLogin](
	[SecurityUserLoginKey] [bigint] IDENTITY(1,1) NOT NULL,
	[SecurityUserKey] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](250) NULL,
	[ProviderKey] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[EffectiveStartDate] [datetimeoffset](7) NULL,
	[EffectiveEndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](max) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecurityUserLogin] PRIMARY KEY CLUSTERED 
(
	[SecurityUserLoginKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Table [EleflexV3].[SecurityUserPermission]    Script Date: 8/29/2015 11:28:08 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[SecurityUserPermission](
	[SecurityUserPermissionKey] [bigint] IDENTITY(1,1) NOT NULL,
	[SecurityUserKey] [uniqueidentifier] NOT NULL,
	[SecurityPermissionKey] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
	[EffectiveStartDate] [datetimeoffset](7) NULL,
	[EffectiveEndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](max) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecurityUserPermission] PRIMARY KEY CLUSTERED 
(
	[SecurityUserPermissionKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Table [EleflexV3].[SecurityUserRole]    Script Date: 8/29/2015 11:28:08 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[SecurityUserRole](
	[SecurityUserRoleKey] [bigint] IDENTITY(1,1) NOT NULL,
	[SecurityUserKey] [uniqueidentifier] NOT NULL,
	[SecurityRoleKey] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
	[EffectiveStartDate] [datetimeoffset](7) NULL,
	[EffectiveEndDate] [datetimeoffset](7) NULL,
	[Comment] [nvarchar](max) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_SecurityUserRole] PRIMARY KEY CLUSTERED 
(
	[SecurityUserRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Index [IX_SecurityRole_Effective]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityRole_Effective] ON [EleflexV3].[SecurityRole]
(
	[Active] ASC,
	[EffectiveStartDate] ASC,
	[EffectiveEndDate] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityRolePermission_Effective]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityRolePermission_Effective] ON [EleflexV3].[SecurityRolePermission]
(
	[Active] ASC,
	[EffectiveStartDate] ASC,
	[EffectiveEndDate] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityRolePermission_SecurityPermission]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityRolePermission_SecurityPermission] ON [EleflexV3].[SecurityRolePermission]
(
	[SecurityPermissionKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityRolePermission_SecurityRole]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityRolePermission_SecurityRole] ON [EleflexV3].[SecurityRolePermission]
(
	[SecurityRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityRoleRole_ChildSecurityRole]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityRoleRole_ChildSecurityRole] ON [EleflexV3].[SecurityRoleRole]
(
	[ChildSecurityRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityRoleRole_Effective]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityRoleRole_Effective] ON [EleflexV3].[SecurityRoleRole]
(
	[Active] ASC,
	[EffectiveStartDate] ASC,
	[EffectiveEndDate] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityRoleRole_ParentSecurityRole]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityRoleRole_ParentSecurityRole] ON [EleflexV3].[SecurityRoleRole]
(
	[ParentSecurityRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
SET ANSI_PADDING ON

;
/****** Object:  Index [IX_SecurityUser]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUser] ON [EleflexV3].[SecurityUser]
(
	[Email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
SET ANSI_PADDING ON

;
/****** Object:  Index [IX_SecurityUser_1]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUser_1] ON [EleflexV3].[SecurityUser]
(
	[Username] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserClaim_Effective]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserClaim_Effective] ON [EleflexV3].[SecurityUserClaim]
(
	[Active] ASC,
	[EffectiveStartDate] ASC,
	[EffectiveEndDate] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserClaim_SecurityUser]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserClaim_SecurityUser] ON [EleflexV3].[SecurityUserClaim]
(
	[SecurityUserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserLogin_Effective]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserLogin_Effective] ON [EleflexV3].[SecurityUserLogin]
(
	[Active] ASC,
	[EffectiveStartDate] ASC,
	[EffectiveEndDate] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserLogin_SecurityUser]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserLogin_SecurityUser] ON [EleflexV3].[SecurityUserLogin]
(
	[SecurityUserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserPermission_Effective]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserPermission_Effective] ON [EleflexV3].[SecurityUserPermission]
(
	[Active] ASC,
	[EffectiveStartDate] ASC,
	[EffectiveEndDate] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserPermission_SecurityPermission]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserPermission_SecurityPermission] ON [EleflexV3].[SecurityUserPermission]
(
	[SecurityPermissionKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserPermission_SecurityUser]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserPermission_SecurityUser] ON [EleflexV3].[SecurityUserPermission]
(
	[SecurityUserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserRole_Effective]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserRole_Effective] ON [EleflexV3].[SecurityUserRole]
(
	[Active] ASC,
	[EffectiveStartDate] ASC,
	[EffectiveEndDate] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserRole_SecurityRole]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserRole_SecurityRole] ON [EleflexV3].[SecurityUserRole]
(
	[SecurityRoleKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
/****** Object:  Index [IX_SecurityUserRole_SecurityUser]    Script Date: 8/29/2015 11:28:09 AM ******/
CREATE NONCLUSTERED INDEX [IX_SecurityUserRole_SecurityUser] ON [EleflexV3].[SecurityUserRole]
(
	[SecurityUserKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;
ALTER TABLE [EleflexV3].[SecurityRolePermission]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRolePermission_SecurityPermission] FOREIGN KEY([SecurityPermissionKey])
REFERENCES [EleflexV3].[SecurityPermission] ([SecurityPermissionKey])
;
ALTER TABLE [EleflexV3].[SecurityRolePermission] CHECK CONSTRAINT [FK_SecurityRolePermission_SecurityPermission]
;
ALTER TABLE [EleflexV3].[SecurityRolePermission]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRolePermission_SecurityRole] FOREIGN KEY([SecurityRoleKey])
REFERENCES [EleflexV3].[SecurityRole] ([SecurityRoleKey])
;
ALTER TABLE [EleflexV3].[SecurityRolePermission] CHECK CONSTRAINT [FK_SecurityRolePermission_SecurityRole]
;
ALTER TABLE [EleflexV3].[SecurityRoleRole]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRoleRole_SecurityRole] FOREIGN KEY([ParentSecurityRoleKey])
REFERENCES [EleflexV3].[SecurityRole] ([SecurityRoleKey])
;
ALTER TABLE [EleflexV3].[SecurityRoleRole] CHECK CONSTRAINT [FK_SecurityRoleRole_SecurityRole]
;
ALTER TABLE [EleflexV3].[SecurityRoleRole]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRoleRole_SecurityRole1] FOREIGN KEY([ChildSecurityRoleKey])
REFERENCES [EleflexV3].[SecurityRole] ([SecurityRoleKey])
;
ALTER TABLE [EleflexV3].[SecurityRoleRole] CHECK CONSTRAINT [FK_SecurityRoleRole_SecurityRole1]
;
ALTER TABLE [EleflexV3].[SecurityUserClaim]  WITH CHECK ADD  CONSTRAINT [FK_SecurityUserClaim_SecurityUser] FOREIGN KEY([SecurityUserKey])
REFERENCES [EleflexV3].[SecurityUser] ([SecurityUserKey])
;
ALTER TABLE [EleflexV3].[SecurityUserClaim] CHECK CONSTRAINT [FK_SecurityUserClaim_SecurityUser]
;
ALTER TABLE [EleflexV3].[SecurityUserLogin]  WITH CHECK ADD  CONSTRAINT [FK_SecurityUserLogin_SecurityUser] FOREIGN KEY([SecurityUserKey])
REFERENCES [EleflexV3].[SecurityUser] ([SecurityUserKey])
;
ALTER TABLE [EleflexV3].[SecurityUserLogin] CHECK CONSTRAINT [FK_SecurityUserLogin_SecurityUser]
;
ALTER TABLE [EleflexV3].[SecurityUserPermission]  WITH CHECK ADD  CONSTRAINT [FK_SecurityUserPermission_SecurityPermission] FOREIGN KEY([SecurityPermissionKey])
REFERENCES [EleflexV3].[SecurityPermission] ([SecurityPermissionKey])
;
ALTER TABLE [EleflexV3].[SecurityUserPermission] CHECK CONSTRAINT [FK_SecurityUserPermission_SecurityPermission]
;
ALTER TABLE [EleflexV3].[SecurityUserPermission]  WITH CHECK ADD  CONSTRAINT [FK_SecurityUserPermission_SecurityUser] FOREIGN KEY([SecurityUserKey])
REFERENCES [EleflexV3].[SecurityUser] ([SecurityUserKey])
;
ALTER TABLE [EleflexV3].[SecurityUserPermission] CHECK CONSTRAINT [FK_SecurityUserPermission_SecurityUser]
;
ALTER TABLE [EleflexV3].[SecurityUserRole]  WITH CHECK ADD  CONSTRAINT [FK_SecurityUserRole_SecurityRole] FOREIGN KEY([SecurityRoleKey])
REFERENCES [EleflexV3].[SecurityRole] ([SecurityRoleKey])
;
ALTER TABLE [EleflexV3].[SecurityUserRole] CHECK CONSTRAINT [FK_SecurityUserRole_SecurityRole]
;
ALTER TABLE [EleflexV3].[SecurityUserRole]  WITH CHECK ADD  CONSTRAINT [FK_SecurityUserRole_SecurityUser] FOREIGN KEY([SecurityUserKey])
REFERENCES [EleflexV3].[SecurityUser] ([SecurityUserKey])
;
ALTER TABLE [EleflexV3].[SecurityUserRole] CHECK CONSTRAINT [FK_SecurityUserRole_SecurityUser]
;

";

    }
}
