using System;
using System.Linq;
using System.Web.Mvc;
using Eleflex.Web;
using Eleflex.ModuleGenerator.Web.Admin.Models;
using Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2;

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
            return View(new ModuleInfoModel());
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ModuleInfoModel model)
        {
            if (model.ModuleName.Contains("."))
            {
                ModelState.AddModelError("", "Module Name cannot contain dots (.)");
                return View(model);
            }

            Generator gen = new Generator();
            byte[] zipFile = gen.GenerateArchive(model.NamespacePrefix, model.ModuleName, model.EntityModelName);

            return File(zipFile, "application/zip");
        }
    }
}
