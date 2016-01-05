using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class MessagesFileConstants : IGenerate
    {

        string _namespaceName = null;
        public MessagesFileConstants(string namespaceName)
        {
            _namespaceName = namespaceName;
        }

        public string Generate()
        {
            return @"using System;

namespace " + _namespaceName + @"
{
    /// <summary>
    /// Static class containing constants used in the application.
    /// </summary>
    public static partial class " + _namespaceName.Replace(".","") + @"Constants
    {

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse(""" + Guid.NewGuid().ToString() + @""");

        /// <summary>
        /// The module name.
        /// </summary>
        public const string MODULE_NAME = """ + _namespaceName + @""";

        /// <summary>
        /// The module description.
        /// </summary>
        public const string MODULE_DESCRIPTION = ""Library providing web client for the " + _namespaceName +  @" Module."";

    }
}
";
        }
    }
}
