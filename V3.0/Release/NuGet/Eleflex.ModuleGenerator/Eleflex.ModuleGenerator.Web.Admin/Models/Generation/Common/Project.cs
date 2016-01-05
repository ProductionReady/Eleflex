using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation
{
    class Project
    {

        public Project()
        {
            ProjectGuid = Guid.NewGuid();
        }


        public string NamespacePrefix { get; set; }

        public string NamespaceSuffix { get; set; }

        public string ModuleName { get; set; }

        public Guid ProjectGuid { get; set; }



        public string GetProjectNamespace()
        {
            string projectNamespace = NamespacePrefix + "." + ModuleName;
            if (!string.IsNullOrEmpty(NamespaceSuffix))
                projectNamespace += "." + NamespaceSuffix;
            return projectNamespace;
        }

    }
}
