using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VersioningModel = Eleflex;

namespace Eleflex.Lookups.Storage.EF.SQLServer
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
            : base(LookupsSQLServerConstants.MODULE_KEY, LookupsSQLServerConstants.MODULE_NAME, LookupsSQLServerConstants.MODULE_DESCRIPTION, PATCH_INFO)
        {
        }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        public override bool IsAvailable
        {
            get { return ObjectLocator.Current.GetInstance<ILookupsStorageService>().VersioningStorageConfig == LookupsSQLServerConstants.VERSIONING_STORAGE_CONFIG; }
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
            ILookupsStorageService service = ObjectLocator.Current.GetInstance<ILookupsStorageService>();
            IStorageServiceSession session = service.CreateSession();
            System.Data.Entity.DbContext context = session.Session as System.Data.Entity.DbContext;
            if (context == null)
                throw new Exception("Session is not DBContext");
            context.Database.ExecuteSqlCommand(SCRIPT);
        }

        /// <summary>
        /// Information regarding the patch.
        /// </summary>
        protected const string PATCH_INFO = "This patch creates the Lookup table.";

        /// <summary>
        /// The script to execute.
        /// </summary>
        protected const string SCRIPT = @"
IF NOT EXISTS ( SELECT  1 FROM    sys.schemas WHERE   name = N'EleflexV3' )
BEGIN
	EXEC('CREATE SCHEMA [EleflexV3]')
END
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[Lookup](
	[LookupKey] [uniqueidentifier] NOT NULL,
	[ParentLookupKey] [uniqueidentifier] NULL,
	[Active] [bit] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[SortOrder] [int] NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[LookupKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

;
ALTER TABLE [EleflexV3].[Lookup]  WITH CHECK ADD  CONSTRAINT [FK_Lookup_Lookup] FOREIGN KEY([ParentLookupKey])
REFERENCES [EleflexV3].[Lookup] ([LookupKey])
;
ALTER TABLE [EleflexV3].[Lookup] CHECK CONSTRAINT [FK_Lookup_Lookup]
;
";

    }
}
