using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebAdminFileViewsAdminIndex : IGenerate
    {

        string _moduleName;

        public WebAdminFileViewsAdminIndex(string moduleName)
        {
            _moduleName = moduleName;
        }

        public string Generate()
        {
            return @"
@{
    ViewBag.Title = """ + _moduleName + @"Admin"";
}
<h2>This is a test page for the " + _moduleName + @" Module.</h2>

";
        }

    }
}
