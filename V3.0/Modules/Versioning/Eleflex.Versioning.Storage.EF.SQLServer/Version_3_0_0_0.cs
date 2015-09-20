using System;
using System.Collections.Generic;
using VersioningModel = Eleflex;

namespace Eleflex.Versioning.Storage.EF.SQLServer
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
            : base(VersioningSQLServerConstants.MODULE_KEY, VersioningSQLServerConstants.MODULE_NAME, VersioningSQLServerConstants.MODULE_DESCRIPTION, PATCH_INFO)
        {
        }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        public override bool IsAvailable
        {
            get { return ObjectLocator.Current.GetInstance<IVersioningStorageService>().VersioningStorageConfig == VersioningSQLServerConstants.VERSIONING_STORAGE_CONFIG; }
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
            IVersioningStorageService service = ObjectLocator.Current.GetInstance<IVersioningStorageService>();
            IStorageServiceSession session = service.CreateSession();
            System.Data.Entity.DbContext context = session.Session as System.Data.Entity.DbContext;
            if (context == null)
                throw new Exception("Session is not DBContext");
            context.Database.ExecuteSqlCommand(SCRIPT);
        }

        /// <summary>
        /// Information regarding the patch.
        /// </summary>
        protected const string PATCH_INFO = "This patch creates the Eleflex schema and Module table.";

        /// <summary>
        /// The script to execute.
        /// </summary>
        protected const string SCRIPT = @"
IF NOT EXISTS ( SELECT  1 FROM    sys.schemas WHERE   name = N'EleflexV3' )
BEGIN
	EXEC('CREATE SCHEMA [EleflexV3]')
END
/****** Object:  Table [EleflexV3].[Module]    Script Date: 8/29/2015 11:29:31 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[Module](
	[ModuleKey] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[VersionMajor] [int] NOT NULL,
	[VersionMinor] [int] NOT NULL,
	[VersionBuild] [int] NOT NULL,
	[VersionRevision] [int] NOT NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[ModuleKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

;
";

    }
}
