using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class BusinessFileStorageService : IGenerate
    {

        string _rootNamespace;
        string _moduleName;


        public BusinessFileStorageService(string rootNamespace, string moduleName)
        {
            _rootNamespace = rootNamespace;
            _moduleName = moduleName;
        }

        public string Generate()
        {
            return @"using Eleflex;

namespace " + _rootNamespace + @"
{
    /// <summary>
    /// The storage service used for the " + _moduleName + @" module.
    /// </summary>
    public interface I" + _moduleName + @"StorageService : IStorageService
    {
    }
}
";
        }
    }
}
