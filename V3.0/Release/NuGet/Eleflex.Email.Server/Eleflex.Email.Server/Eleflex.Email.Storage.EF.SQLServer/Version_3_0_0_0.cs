using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VersioningModel = Eleflex;

namespace Eleflex.Email.Storage.EF.SQLServer
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
            : base(EmailSQLServerConstants.MODULE_KEY, EmailSQLServerConstants.MODULE_NAME, EmailSQLServerConstants.MODULE_DESCRIPTION, PATCH_INFO)
        {
        }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        public override bool IsAvailable
        {
            get { return ObjectLocator.Current.GetInstance<IEmailStorageService>().VersioningStorageConfig == EmailSQLServerConstants.VERSIONING_STORAGE_CONFIG; }
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
            IEmailStorageService service = ObjectLocator.Current.GetInstance<IEmailStorageService>();
            IStorageServiceSession session = service.CreateSession();
            System.Data.Entity.DbContext context = session.Session as System.Data.Entity.DbContext;
            if (context == null)
                throw new Exception("Session is not DBContext");
            context.Database.ExecuteSqlCommand(SCRIPT);
        }

        /// <summary>
        /// Information regarding the patch.
        /// </summary>
        protected const string PATCH_INFO = "This patch creates the Email tables.";

        /// <summary>
        /// The script to execute.
        /// </summary>
        protected const string SCRIPT = @"
IF NOT EXISTS ( SELECT  1 FROM    sys.schemas WHERE   name = N'EleflexV3' )
BEGIN
	EXEC('CREATE SCHEMA [EleflexV3]')
END
/****** Object:  Table [EleflexV3].[EmailProcess]    Script Date: 10/11/2015 12:25:15 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [EleflexV3].[EmailProcess](
	[EmailProcessKey] [bigint] IDENTITY(1,1) NOT NULL,
	[FromAddress] [nvarchar](max) NOT NULL,
	[ToAddress] [nvarchar](max) NULL,
	[CcAddress] [nvarchar](max) NULL,
	[BccAddress] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[IsHtml] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[FutureSendDate] [datetimeoffset](7) NULL,
	[SentDate] [datetimeoffset](7) NULL,
	[IsError] [bit] NOT NULL,
	[ErrorDate] [datetimeoffset](7) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[IsProcessing] [bit] NOT NULL,
	[ProcessingDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_EmailProcess] PRIMARY KEY CLUSTERED 
(
	[EmailProcessKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

;
/****** Object:  Table [EleflexV3].[EmailProcessAttachment]    Script Date: 10/11/2015 12:25:15 AM ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE [EleflexV3].[EmailProcessAttachment](
	[EmailProcessAttachmentKey] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailProcessKey] [bigint] NOT NULL,
	[Filename] [nvarchar](max) NOT NULL,
	[FileData] [varbinary](max) NULL,
	[ContentType] [nvarchar](max) NULL,
 CONSTRAINT [PK_EmailProcessAttachment] PRIMARY KEY CLUSTERED 
(
	[EmailProcessAttachmentKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

;
SET ANSI_PADDING OFF
;
/****** Object:  Index [IX_EmailProcessAttachment]    Script Date: 10/11/2015 12:25:15 AM ******/
CREATE NONCLUSTERED INDEX [IX_EmailProcessAttachment] ON [EleflexV3].[EmailProcessAttachment]
(
	[EmailProcessKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
ALTER TABLE [EleflexV3].[EmailProcessAttachment]  WITH CHECK ADD  CONSTRAINT [FK_EmailProcessAttachment_EmailProcess] FOREIGN KEY([EmailProcessKey])
REFERENCES [EleflexV3].[EmailProcess] ([EmailProcessKey])
;
ALTER TABLE [EleflexV3].[EmailProcessAttachment] CHECK CONSTRAINT [FK_EmailProcessAttachment_EmailProcess]
;

";

    }
}
