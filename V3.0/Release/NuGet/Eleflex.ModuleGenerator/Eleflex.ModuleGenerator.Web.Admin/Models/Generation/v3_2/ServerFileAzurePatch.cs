using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class ServerFileAzurePatch : IGenerate
    {


        string _namespace = null;
        string _moduleName;
        Eleflex.Version _versionNumber = null;

        public ServerFileAzurePatch(string namespaceName, Eleflex.Version versionNumber, string moduleName)
        {
            _namespace = namespaceName;
            _versionNumber = versionNumber;
            _moduleName = moduleName;
        }

        public string Generate()
        {
            return @"using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VersioningModel = Eleflex;
using Eleflex;

namespace " + _namespace + @"
{
    /// <summary>
    /// The base version patch for " + _versionNumber.ToString() + @"
    /// </summary>
    public class Version_" + _versionNumber.ToString().Replace(".", "_") + @" : VersioningModel.ModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_" + _versionNumber.ToString().Replace(".", "_") + @"()
            : base(" + _moduleName + @"AzureConstants.MODULE_KEY, " + _moduleName + @"AzureConstants.MODULE_NAME, " + _moduleName + @"AzureConstants.MODULE_DESCRIPTION, PATCH_INFO)
        {
        }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        public override bool IsAvailable
        {
            get { return ObjectLocator.Current.GetInstance<I" + _moduleName + @"StorageService>().VersioningStorageConfig == " + _moduleName + @"AzureConstants.VERSIONING_STORAGE_CONFIG; }
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
                return new VersioningModel.Version(" + _versionNumber.ToString().Replace(".", ",") + @");
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
            I" + _moduleName + @"StorageService service = ObjectLocator.Current.GetInstance<I" + _moduleName + @"StorageService>();
            IStorageServiceSession session = service.CreateSession();
            System.Data.Entity.DbContext context = session.Session as System.Data.Entity.DbContext;
            if (context == null)
                throw new Exception(""Session is not DBContext"");
            context.Database.ExecuteSqlCommand(SCRIPT);
        }

        /// <summary>
        /// Information regarding the patch.
        /// </summary>
        protected const string PATCH_INFO = ""This patch creates tables for the " + _moduleName + @" Module."";

        /// <summary>
        /// The script to execute.
        /// </summary>
        protected const string SCRIPT = @""
;
"";

    }
}
";
        }
    }
}
