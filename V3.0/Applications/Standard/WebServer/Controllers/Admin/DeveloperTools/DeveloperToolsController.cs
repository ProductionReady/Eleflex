using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServer.Controllers.Admin.DeveloperTools
{

    [Authorize(Roles = "Admin")]
    public class DeveloperToolsController : Controller
    {
        //
        // GET: /Developer/        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InstalledComponents()
        {
            return View();
        }

        public ActionResult FontAwesomeCheatSheet()
        {
            return View();
        }

        public ActionResult FontAwesomeExamples()
        {
            return View();
        }

        public ActionResult BootstrapCss()
        {
            return View();
        }

        public ActionResult BootstrapComponents()
        {
            return View();
        }

        public ActionResult BootstrapJavascript()
        {
            return View();
        }

    }
}
