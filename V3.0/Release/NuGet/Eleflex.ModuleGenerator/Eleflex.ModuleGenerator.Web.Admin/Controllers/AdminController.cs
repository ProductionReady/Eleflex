using System;
using System.Linq;
using System.Web.Mvc;
using Eleflex.Web;

namespace Eleflex.ModuleGenerator.Web.Admin.Controllers
{
    /// <summary>
    /// Email admin controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
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
