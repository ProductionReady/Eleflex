using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class ServerFileSQLServerConstants : IGenerate
    {

        string _rootNamespace;
        string _moduleName;

        public ServerFileSQLServerConstants(string rootNamespace, string moduleName)
        {
            _rootNamespace = rootNamespace;
            _moduleName = moduleName;
        }

        public string Generate()
        {

            return @"using System;

namespace " + _rootNamespace +  @".Storage.EF.SQLServer
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class " + _moduleName + @"SQLServerConstants
    {

        /// <summary>
        /// The module key.
        /// </summary>
        public static Guid MODULE_KEY = " + _moduleName + @"Constants.STORAGE_SERVICE_MODULE_KEY;

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = """ + _moduleName + @" Microsoft SQL Server"";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = """ + _moduleName + @" Microsoft SQL Server."";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string VERSIONING_STORAGE_CONFIG = MODULE_NAME;
    }
}
";
        }
    }
}
