using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class MessagesFileIRequestDispatcher : IGenerate
    {


        string _namespaceRoot;
        string _moduleName;

        public MessagesFileIRequestDispatcher(string namespaceRoot, string moduleName)
        {
            _namespaceRoot = namespaceRoot;
            _moduleName = moduleName;
        }

        public string Generate()
        {
            return @"using Eleflex;
using Eleflex.Services.WCF;

namespace " + _namespaceRoot + @".Services.WCF.Message
{
    /// <summary>
    /// Represents an object for sending  requests for all service command for the " + _moduleName + @" module.
    /// </summary>
    public partial interface I" + _moduleName + @"RequestDispatcher : IWCFCommandRequestDispatcher
    {
    }
}


";
        }
    }
}
