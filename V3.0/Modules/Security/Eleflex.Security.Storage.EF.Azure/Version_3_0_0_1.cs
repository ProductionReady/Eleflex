using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VersioningModel = Eleflex;

namespace Eleflex.Security.Storage.EF.Azure
{
    /// <summary>
    /// The version patch for 3.0.0.1
    /// </summary>
    public class Version_3_0_0_1 : VersioningModel.ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_3_0_0_1()
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
                return new VersioningModel.Version(3, 0, 0, 1);
            }
        }

        /// <summary>
        /// The versions prior to this version for which the patching process can be used.
        /// </summary>
        public override List<VersioningModel.Version> PriorVersions
        {
            get
            {
                return new List<Version>() { new VersioningModel.Version(3, 0, 0, 0) };
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
        protected const string PATCH_INFO = "This patch creates the default roles, users and userrole links.";

        /// <summary>
        /// The script to execute.
        /// </summary>
        protected string SCRIPT = @"
INSERT INTO [EleflexV3].[SecurityRole]([SecurityRoleKey],[Active],[Name],[Description],[IsSystem],[EffectiveStartDate],[EffectiveEndDate],[ExtraData])
VALUES ('" + SecurityConstants.ROLE_ADMINKEY.ToString().Replace("{", "").Replace("}", "") + @"',1,'" + SecurityConstants.ROLE_ADMIN + @"', 'This role is assigned to administrators of the system.',1,null,null,null)

INSERT INTO [EleflexV3].[SecurityRole]([SecurityRoleKey],[Active],[Name],[Description],[IsSystem],[EffectiveStartDate],[EffectiveEndDate],[ExtraData])
VALUES ('" + SecurityConstants.ROLE_USERKEY.ToString().Replace("{", "").Replace("}", "") + @"',1,'" + SecurityConstants.ROLE_USER + @"', 'This role is assigned to users of the system.',1,null,null,null)

INSERT INTO [EleflexV3].[SecurityUser]([SecurityUserKey],[Active],[CreateDate],[FirstName],[LastName],[Username],[Email],[Password],[PasswordLastChangeDate],[SecurityStamp],[LoginFailedAttempts],[EnableLockout],[LastLoginDate],[LockoutReinstateDate],[EmailValid],[EmailValidCode],[Phone],[PhoneValid],[PhoneValidCode],[TwoFactorAuth],[Comment],[ExtraData])
VALUES('" + SecurityConstants.USERKEY_SYSTEM_ADMIN.ToString().Replace("{", "").Replace("}", "") + @"',1,GETDATE(),'System','Administrator','system','system@eleflex.com','',GETDATE(),'',0,0,null,null,0,null,null,0,null,0,'This is the default ELEFLEX system account',null)

INSERT INTO [EleflexV3].[SecurityUserRole]([SecurityUserKey],[SecurityRoleKey],[Active],[EffectiveStartDate],[EffectiveEndDate],[Comment],[ExtraData])
VALUES ('" + SecurityConstants.USERKEY_SYSTEM_ADMIN.ToString().Replace("{", "").Replace("}", "") + @"','" + SecurityConstants.ROLE_ADMINKEY.ToString().Replace("{", "").Replace("}", "") + @"',1,null,null,null,null)

INSERT INTO [EleflexV3].[SecurityUserRole]([SecurityUserKey],[SecurityRoleKey],[Active],[EffectiveStartDate],[EffectiveEndDate],[Comment],[ExtraData])
VALUES ('" + SecurityConstants.USERKEY_SYSTEM_ADMIN.ToString().Replace("{", "").Replace("}", "") + @"','" + SecurityConstants.ROLE_USERKEY.ToString().Replace("{", "").Replace("}", "") + @"',1,null,null,null,null)
";

    }
}
