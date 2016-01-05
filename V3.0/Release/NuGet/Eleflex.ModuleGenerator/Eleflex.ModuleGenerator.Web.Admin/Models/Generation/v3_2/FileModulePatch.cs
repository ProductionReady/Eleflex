using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eleflex;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class FileModulePatch : IGenerate
    {


        string _namespace = null;
        Eleflex.Version _versionNumber = null;

        public FileModulePatch(string namespaceName, Eleflex.Version versionNumber)
        {
            _namespace = namespaceName;
            _versionNumber = versionNumber;
        }

        public string Generate()
        {
            string data = string.Empty;

            data += @"using System;
using System.Collections.Generic;
using VersioningModel = Eleflex;

namespace " + _namespace + @"
{
    /// <summary>
    /// The base version patch for " + _versionNumber.ToString() + @"
    /// </summary>
    public class Version_" + _versionNumber.ToString().Replace(".","_") + @" : VersioningModel.ModulePatch
    {

        /// <summary>
        /// Information regarding the patch.
        /// </summary>
        protected const string PATCH_INFO = ""This patch updates the " + _namespace + @" library."";

        /// <summary>
        /// Constructor.
        /// </summary>
        public Version_" + _versionNumber.ToString().Replace(".", "_") + @"()
            : base(" + _namespace.Replace(".","") + @"Constants.MODULE_KEY, " + _namespace.Replace(".", "") + @"Constants.MODULE_NAME, " + _namespace.Replace(".", "") + @"Constants.MODULE_DESCRIPTION, PATCH_INFO)
        {
        }

        /// <summary>
        /// Determine if this patch is currently available to be run.
        /// </summary>
        public override bool IsAvailable
        {
            get { return true; }
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
                return new VersioningModel.Version(" + _versionNumber.ToString().Replace(".",",") + @");
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
        }

    }
}
";

            return data;
        }
    }
}
