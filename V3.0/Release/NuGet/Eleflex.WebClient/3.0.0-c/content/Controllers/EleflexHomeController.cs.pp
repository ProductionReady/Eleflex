using System.Web.Mvc;

namespace $rootnamespace$.Controllers
{
    /// <summary>
    /// Eleflex home controller.
    /// </summary>
    public class EleflexHomeController : Controller
    {

        /// <summary>
        /// Default.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// About page.
        /// </summary>
        /// <returns></returns>        
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// AGPL page.
        /// </summary>
        /// <returns></returns>
        public ActionResult AGPL()
        {
            return View();
        }

    }
}
