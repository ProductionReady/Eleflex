using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VersioningModel = Eleflex;

namespace Eleflex.Logging.Storage.EF.Azure
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
            : base(LoggingAzureConstants.MODULE_KEY, LoggingAzureConstants.MODULE_NAME, LoggingAzureConstants.MODULE_DESCRIPTION,PATCH_INFO)
        {
        }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        public override bool IsAvailable
        {
            get { return ObjectLocator.Current.GetInstance<ILoggingStorageService>().VersioningStorageConfig == LoggingAzureConstants.VERSIONING_STORAGE_CONFIG; }
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
            ILoggingStorageService service = ObjectLocator.Current.GetInstance<ILoggingStorageService>();
            IStorageServiceSession session = service.CreateSession();
            System.Data.Entity.DbContext context = session.Session as System.Data.Entity.DbContext;
            if (context == null)
                throw new Exception("Session is not DBContext");
            context.Database.ExecuteSqlCommand(SCRIPT);
        }

        /// <summary>
        /// Information regarding the patch.
        /// </summary>
        protected const string PATCH_INFO = "This patch creates the LogMessage table.";

        /// <summary>
        /// The script to execute.
        /// </summary>
        protected const string SCRIPT = @"
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'EleflexV3')
BEGIN
EXEC sys.sp_executesql N'CREATE SCHEMA [EleflexV3]'
END
/****** Object:  Table [EleflexV3].[LogMessage]    Script Date: 8/29/2015 3:19:07 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[LogMessage](
	[LogMessageKey] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[Application] [nvarchar](250) NULL,
	[Server] [nvarchar](250) NULL,
	[IsError] [bit] NOT NULL,
	[Severity] [nvarchar](250) NULL,
	[Source] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[ExtraData] [nvarchar](max) NULL,
 CONSTRAINT [PK_LogMessage] PRIMARY KEY CLUSTERED 
(
	[LogMessageKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

;
/****** Object:  Index [IX_LogMessage]    Script Date: 8/29/2015 3:19:07 AM ******/
CREATE NONCLUSTERED INDEX [IX_LogMessage] ON [EleflexV3].[LogMessage]
(
	[CreateDate] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
;

";

    }
}
