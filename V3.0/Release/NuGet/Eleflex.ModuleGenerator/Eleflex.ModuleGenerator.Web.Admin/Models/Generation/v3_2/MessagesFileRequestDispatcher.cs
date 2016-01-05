using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class MessagesFileRequestDispatcher : IGenerate
    {


        string _namespaceRoot;
        string _moduleName;

        public MessagesFileRequestDispatcher(string namespaceRoot, string moduleName)
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
    /// Represents an object for sending  request for all service command for the " + _moduleName + @" module.
    /// </summary>
    public class " + _moduleName + @"RequestDispatcher : WCFCommandRequestDispatcher, I" + _moduleName + @"RequestDispatcher
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public " + _moduleName + @"RequestDispatcher()
            : base(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name=""endpoint""></param>
        public " + _moduleName + @"RequestDispatcher(string endpoint)
            : base(endpoint)
        { }

    }
}

";
        }
    }
}
