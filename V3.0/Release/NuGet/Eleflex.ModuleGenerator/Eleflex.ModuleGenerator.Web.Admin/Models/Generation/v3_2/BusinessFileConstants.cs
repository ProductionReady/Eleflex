using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class BusinessFileConstants : IGenerate
    {

        string _moduleName;
        string _rootNamespace;

        public BusinessFileConstants(string rootNamespace, string moduleName)
        {
            _rootNamespace = rootNamespace;
            _moduleName = moduleName;
        }
        public string Generate()
        {
            return @"using System;

namespace " + _rootNamespace + @"
{
    /// <summary>
    /// Static class containing constants for the " + _moduleName + @" module.
    /// </summary>
    public static partial class " + _moduleName + @"Constants
    {        

        /// <summary>
        /// The module key for " + _moduleName + @".
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse(""" + Guid.NewGuid().ToString() + @""");

        /// <summary>
        /// The module name for " + _moduleName + @".
        /// </summary>
        public const string MODULE_NAME = """ + _moduleName + @" Module"";

        /// <summary>
        /// The module key for " + _moduleName + @".
        /// </summary>
        public static Guid STORAGE_SERVICE_MODULE_KEY = Guid.Parse(""" + Guid.NewGuid().ToString() + @""");

        /// <summary>
        /// The name used to distinguish the Example storage service from others.
        /// </summary>
        public const string STORAGE_SERVICE_NAME = """ + _rootNamespace.Replace(".","") + @"StorageService"";
        
    }
}

";
        }

    }
}
