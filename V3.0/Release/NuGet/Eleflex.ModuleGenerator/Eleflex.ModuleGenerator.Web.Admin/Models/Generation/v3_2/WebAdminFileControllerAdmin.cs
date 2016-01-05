using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebAdminFileControllerAdmin : IGenerate
    {

        string _rootNamespace;

        public WebAdminFileControllerAdmin(string rootNamespace)
        {
            _rootNamespace = rootNamespace;
        }


        public string Generate()
        {
            return @"using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using Eleflex;
using Eleflex.Web;

namespace " + _rootNamespace + @".Web.Admin.Controllers
{
    /// <summary>
    /// Admin controller.
    /// </summary>
    [Authorize(Roles = ""Admin"")]
    public class AdminController : Controller
    {

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
";
        }

    }
}
